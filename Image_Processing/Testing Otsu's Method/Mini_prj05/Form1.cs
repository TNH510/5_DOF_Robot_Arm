using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Mini_prj05
{
    public partial class Form1 : Form
    {
        Bitmap hinhgoc;
        static string imagePath;
        public Form1()
        {
            InitializeComponent();


        }
        public Bitmap ChuyenhinhRBGsanghinhxamLuminance(Bitmap hinhgoc)
        {
            Bitmap HinhMucXam = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //lấy điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm (x,y)
                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    // gán giá trị mức xám vừa tính
                    HinhMucXam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return HinhMucXam;
        }
        public int[] TinhHistogram(Bitmap HinhMucXam)
        {

            // khai báo mảng có 256 phần tử
            int[] histogram = new int[256];
            for (int x = 0; x < HinhMucXam.Width; x++)
                for (int y = 0; y < HinhMucXam.Height; y++)
                {

                    Color color = HinhMucXam.GetPixel(x, y);
                    byte gray = color.R;//trong hình  mức xám giá trị kênh R cũng giống  G hoặc B
                    // giá trị gray cũng chính là  phần tử thứ gray  trong mảng histogram dã khai báo 
                    //  Sẽ Tăng số đểm của phần tử thứ gray lên 1
                    histogram[gray]++;

                }
            return histogram;

        }
        public int ComputeThresholdByOtsuMethod(Bitmap HinhMucXam)
        {
            int[] histogram = TinhHistogram(HinhMucXam);
            int threshold = 1;
            int varience_max = 0;
            int m_1 = 0;
            int P1 = 0;
            int m_G = 0;
            //compute the global intensity mean
            for (int j = 0; j < 256; j++)
            {
                m_G = m_G + j * (histogram[j] / (HinhMucXam.Width * HinhMucXam.Height));
            }
            for (int i = 1; i < 256; i++)
            {
                //compute the cumulative sum and mean
                for (int j = 0; j < i; j++)
                {
                    P1 = P1 + histogram[j] / (HinhMucXam.Width * HinhMucXam.Height);
                    m_1 = m_1 + j * (histogram[j] / (HinhMucXam.Width * HinhMucXam.Height));
                }
                //compute the between class varience and finf varience max
                int varience=0;
                if (P1!=0)
                {
                    varience = (m_G * P1 - m_1) ^ 2 / (P1 * (1 - P1));
                }
                //int varience = (m_G * P1 - m_1) ^ 2 / (P1 * (1 - P1));
                if (varience > varience_max)
                {
                    varience_max = varience;
                    threshold = i;
                }

            }
            return threshold;
        }
        public static int CalculateThreshold(Bitmap image)
        {
            // Tính histogram
            int[] histogram = new int[256];
            int totalPixels = 0;

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int grayLevel = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    histogram[grayLevel]++;
                    totalPixels++;
                }
            }

            // Tính tổng trọng số
            double[] weights = new double[256];
            for (int i = 0; i < 256; i++)
            {
                weights[i] = (double)histogram[i] / totalPixels;
            }

            // Tìm ngưỡng tối ưu
            double maxVariance = 0;
            int threshold = 0;

            for (int t = 0; t < 256; t++)
            {
                double w1 = 0, w2 = 0;
                double mean1 = 0, mean2 = 0;
                double variance = 0;

                for (int i = 0; i <= t; i++)
                {
                    w1 += weights[i];
                    mean1 += i * weights[i];
                }

                for (int i = t + 1; i < 256; i++)
                {
                    w2 += weights[i];
                    mean2 += i * weights[i];
                }

                if (w1 != 0 && w2 != 0)
                {
                    mean1 /= w1;
                    mean2 /= w2;
                    variance = w1 * w2 * Math.Pow(mean1 - mean2, 2);
                }

                if (variance > maxVariance)
                {
                    maxVariance = variance;
                    threshold = t;
                }
            }

            return threshold;
        }
        PointPairList ChuyenDoiHistogram(int[] histogram)
        {

            PointPairList points = new PointPairList();
            for (int i = 0; i < histogram.Length; i++)
            {
                //i chạy từ 0-->255 vì nó là giá trị gray
                //histogram[i] tương ứng  trục đứng, là số pixels cùng mức xám
                points.Add(i, histogram[i]);
            }
            return points;



        }
        //đưa các thông số hiển thị lên biểu đồ 
        public GraphPane BieuDoHistogram(PointPairList histogram)
        {
            //GraphPane lad đối tượng  biểu đồ trong  Zedgraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ  Histogram";//tên biểu đồ
            //gp.Rect = new Rectangle(0, 0, 1000, 900);// khung chứa biểu đồ
            // thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của điểm ảnh";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 255;
            gp.XAxis.Scale.MajorStep = 5;
            gp.XAxis.Scale.MinorStep = 1;
            //Thiết lập trục đứng
            gp.YAxis.Title.Text = @"Giá trị mức xám của điểm ảnh";
            gp.YAxis.Scale.Min = 0;
            gp.YAxis.Scale.Max = 25000;// cho giá trị lớn hơn giá trị điểm ảnh có nhiều nhất
            gp.YAxis.Scale.MajorStep = 5;
            gp.YAxis.Scale.MinorStep = 1;
            // dùng biểu đồ dạng Bar để  biểu diễn Histogram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);
            return gp;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            // Tạo một OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png; *.jpg; *.jpeg; *.gif; *.bmp)|*.png; *.jpg; *.jpeg; *.gif; *.bmp";
            openFileDialog.Title = "Select an Image";

            // Hiển thị hộp thoại và xử lý kết quả
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                // Lấy đường dẫn của tệp ảnh đã chọn
                imagePath = openFileDialog.FileName;

                // Hiển thị ảnh trong PictureBox
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                pic_goc.Image = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hinhgoc = new Bitmap(imagePath);
            //hiển thị hình gốc
            pic_goc.Image = hinhgoc;
            //hiển thị hình xám
            Bitmap HinhMucXam = ChuyenhinhRBGsanghinhxamLuminance(hinhgoc);
            pic_xam.Image = HinhMucXam;

            // gọi hàm đã viết để chạy biểu đồ
            int[] histogram = TinhHistogram(HinhMucXam);
            PointPairList points = ChuyenDoiHistogram(histogram);
            // vẽ và hiển thị biểu đồ
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }


            private void button3_Click(object sender, EventArgs e)
        {
            hinhgoc = new Bitmap(imagePath);
            Bitmap HinhMucXam = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //lấy điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm (x,y)
                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    if (gray < Convert.ToInt16(textBox1.Text))
                    {
                        HinhMucXam.SetPixel(x, y, Color.FromArgb(0, 0, 0));
                    }
                    else
                    {
                        HinhMucXam.SetPixel(x, y, Color.FromArgb(255, 255, 255));
                    }
                    // gán giá trị mức xám vừa tính
                }
            }

            pictureBox1.Image = HinhMucXam;
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Bitmap hinhgoc1 = new Bitmap(imagePath);
            //chuyen doi sang hinh muc xam
            Bitmap Hinhmucxam = ChuyenhinhRBGsanghinhxamLuminance(hinhgoc1);
            //tinh threshold
            int threshold = CalculateThreshold(Hinhmucxam);
            textBox1.Text = threshold.ToString();
        }
    }
}
