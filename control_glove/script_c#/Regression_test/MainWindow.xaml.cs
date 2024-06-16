using HelixToolkit.Wpf;
using System;
using System.Linq;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using Accord.Math;
using MathNet.Numerics.LinearAlgebra.Double;
using System.Windows.Controls;
using CsvHelper;
using System.Globalization;
using System.IO;
using CsvHelper.Configuration;
using System.Collections.Generic;

namespace Regression_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {

        public MainWindow()
        {
            InitializeComponent();
            // Path to the CSV file
            string csvFilePath = @"C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv";
            PlotData(csvFilePath);

        }

        private void PlotData(string csvFilePath)
        {

            string savePath = @"C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/good.csv";
            // Đọc dữ liệu từ tệp CSV
            var data = ReadDataFromCsv(csvFilePath);
            var save_data = ReadDataFromCsv(savePath);

            var length_data = data.Length;
            // Chia dữ liệu thành các đoạn nhỏ hơn, ví dụ: mỗi đoạn chứa 30 điểm
            int segmentSize = 30;
            var segments = SplitDataIntoSegments(data, segmentSize);

            var poly_segments = SplitDataIntoSegments(save_data, segmentSize);

            // Khởi tạo một danh sách để tích lũy tất cả các điểm
            List<Point3D> allPoints = new List<Point3D>();

            foreach (var segment in segments)
            {
                // Tạo biến t (biến tham số)
                double[] t = Enumerable.Range(0, segment.Length).Select(i => (double)i / (segment.Length - 1)).ToArray();

                // Tách các biến độc lập và biến phụ thuộc
                double[] X = segment.Select(p => p.X).ToArray();
                double[] Y = segment.Select(p => p.Y).ToArray();
                double[] Z = segment.Select(p => p.Z).ToArray();

                // Hồi quy đa thức bậc 5 cho từng biến x, y, z theo t
                double[] coeffsX = PolyFit(t, X, 10);
                double[] coeffsY = PolyFit(t, Y, 10);
                double[] coeffsZ = PolyFit(t, Z, 10);

                // Tạo giá trị dự đoán cho x, y, z
                int numPoints = segmentSize;
                double[] tFit = Enumerable.Range(0, numPoints).Select(i => (double)i / (numPoints - 1)).ToArray();
                Point3D[] curve = tFit.Select(tt => new Point3D(
                    EvalPoly(coeffsX, tt),
                    EvalPoly(coeffsY, tt),
                    EvalPoly(coeffsZ, tt))).ToArray();

                // Thêm các điểm của segment vào danh sách tất cả các điểm
                allPoints.AddRange(curve);

                // Vẽ các điểm dữ liệu
                var pointsVisual3D = new PointsVisual3D
                {
                    Color = Colors.Red,
                    Size = 5,
                    Points = new Point3DCollection(segment)
                };
                helixViewport.Children.Add(pointsVisual3D);

                // Vẽ đường cong hồi quy
                var curveVisual3D = new LinesVisual3D
                {
                    Color = Colors.Blue,
                    Thickness = 2,
                    Points = new Point3DCollection(curve)
                };
                helixViewport.Children.Add(curveVisual3D);
            }

            foreach (var poly_segment in poly_segments)
            {
                // Tạo biến t (biến tham số)
                double[] t = Enumerable.Range(0, poly_segment.Length).Select(i => (double)i / (poly_segment.Length - 1)).ToArray();

                // Vẽ các điểm dữ liệu
                var polypointsVisual3D = new PointsVisual3D
                {
                    Color = Colors.Black,
                    Size = 5,
                    Points = new Point3DCollection(poly_segment)
                };

                helixViewport.Children.Add(polypointsVisual3D);
            }
            // SaveRegressionResultToCsv(allPoints, savePath, 300);
        }

        private void SaveRegressionResultToCsv(List<Point3D> allPoints, string filePath, int size)
        {

            // Chuyển đổi List<Point3D> thành Point3D[]
            Point3D[] data = allPoints.ToArray();

            // Số điểm mục tiêu (300 điểm)
            int targetCount = size;
            int originalCount = data.Length;

            var config = new CsvConfiguration(CultureInfo.InvariantCulture);
            config.HasHeaderRecord = false; // Thiết lập HasHeaderRecord tại đây

            if (originalCount >= targetCount)
            {
            }
            else
            {
                int delta = 1;
                while ((originalCount + delta) != targetCount) { delta += 1; }; 
                // Tăng số lượng điểm để đạt 300 điểm bằng cách chia nhỏ dữ liệu
                int segmentSize = originalCount + delta;
                var extendedData = ExtendData(data, segmentSize);

                using (var writer = new StreamWriter(filePath))
                using (var csv = new CsvWriter(writer, config)) // Sử dụng config đã thiết lập
                {
                    foreach (var point in extendedData)
                    {
                        csv.WriteRecord(point); // Ghi mỗi điểm vào file CSV
                        csv.NextRecord();
                    }
                }
            }
        }

        private IEnumerable<Point3D> ExtendData(Point3D[] data, int targetCount)
        {
            int originalCount = data.Length;
            int numSegments = (int)Math.Ceiling((double)targetCount / originalCount);
            int segmentSize = originalCount * numSegments;
            var extendedData = new List<Point3D>();

            for (int i = 0; i < numSegments; i++)
            {
                foreach (var point in data)
                {
                    extendedData.Add(point);
                }
            }

            return extendedData.Take(targetCount);
        }

        private IEnumerable<Point3D[]> SplitDataIntoSegments(Point3D[] data, int segmentSize)
        {
            for (int i = 0; i < data.Length; i += segmentSize)
            {
                yield return data.Skip(i).Take(segmentSize).ToArray();
            }
        }

        private Point3D[] ReadDataFromCsv(string filePath)
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false,
            };

            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, config))
            {
                var records = csv.GetRecords<CsvPoint>().ToList();
                return records.Select(r => new Point3D(r.X, r.Y, r.Z)).ToArray();
            }
        }

        public class CsvPoint
        {
            public double X { get; set; }
            public double Y { get; set; }
            public double Z { get; set; }
        }

        private double[] PolyFit(double[] x, double[] y, int degree)
        {
            var vandermonde = new DenseMatrix(x.Length, degree + 1);
            var coefficients = new double[degree + 1];
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j <= degree; j++)
                {
                    vandermonde[i, j] = Math.Pow(x[i], j);
                }
            }
            var yVector = new DenseVector(y);
            var coefficientsVector = vandermonde.QR().Solve(yVector);
            return coefficientsVector.ToArray();
        }

        private double EvalPoly(double[] coefficients, double x)
        {
            double result = 0;
            for (int i = 0; i < coefficients.Length; i++)
            {
                result += coefficients[i] * Math.Pow(x, i);
            }
            return result;
        }

    }



}
