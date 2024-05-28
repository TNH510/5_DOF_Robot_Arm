using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;


//int[,] gradient;
internal class Program
{
    
    static void Main()
    {
        // Khởi tạo đối tượng Stopwatch
        Stopwatch stopwatch = new Stopwatch();
        // Bắt đầu đo thời gian
        stopwatch.Start();
        int high_threshold = 200;//ngưỡng trên cho canny detect
        int low_threshold = 40;//ngưỡng dưới cho canny detect 
        int threshold = 50;  // Ngưỡng để chọn các đỉnh trong ma trận Hough
                             // int thre = Convert.ToInt16(text_thres.Text);
                             //ảnh binary cho canny detect
                             //string inputFilePath = @"C:\Users\Loc\Desktop\XLA_10 (2).jpg";
        string filePath = @"C:\Users\Loc\Desktop\outputfile5.csv";
        string[] lines = File.ReadAllLines(filePath);

        int rowCount = lines.Length;
        int columnCount = lines[1].Split(',').Length;

        int[,] array2D = new int[rowCount, columnCount];

        for (int i = 1; i < rowCount; i++)
        {
            string[] fields = lines[i].Split(',');

            for (int j = 0; j < columnCount; j++)
            {
                if (int.TryParse(fields[j], out int value))
                {
                    array2D[i, j] = value;
                }
                else
                {
                    // Xử lý lỗi định dạng không hợp lệ nếu cần thiết
                }
            }
        }
        //int[,] gray= LoadGrayImageFromFile(inputFilePath);
        int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(array2D, high_threshold, low_threshold);
        int a = EdgeDetection.Point_corner(edges);
        // Lấy số hàng và số cột của mảng
        // Khởi tạo một mảng 2 chiều
        int width = edges.GetLength(0);
        int height = edges.GetLength(1);

        //biểu đồ hough
        int[,] hough = EdgeDetection.PerformHoughTransform(edges);

        //vẽ đường thẳng từ biểu đồ hough
        int[,] result_2D = EdgeDetection.DrawLines3(hough, threshold, width, height);
        EdgeDetection.Point_corner(result_2D);
        int x = MidPoint[0, 0];
        Console.WriteLine("X: " + x);
        int y = MidPoint[0, 1];
        Console.WriteLine("Y: " + y);
        Console.WriteLine("angle1: " + angle[0]);
        Console.WriteLine("angle2: " + angle[2]);
        //// Dừng đồng hồ đo thời gian
        stopwatch.Stop();

        // Lấy thời gian đã trôi qua
        TimeSpan elapsedTime = stopwatch.Elapsed;

        // In ra thời gian đã trôi qua
        Console.WriteLine("Thời gian thực thi: " + elapsedTime);
        Console.WriteLine("Chuyển đổi thành công.");
    }
    static void SaveGrayImageToFile(int[,] grayImage, string filePath)
    {
        int height = grayImage.GetLength(0);
        int width = grayImage.GetLength(1);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Ghi kích thước ảnh vào file (định dạng: height,width)
            writer.WriteLine($"{height},{width}");

            // Ghi giá trị mức xám của từng pixel vào file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    writer.Write(grayImage[y, x]);

                    // Ngăn cách giá trị bằng dấu phẩy
                    if (x < width - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }
    static int[,] LoadIntCsvToArray(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int rowCount = lines.Length;
        int columnCount = lines[0].Split(',').Length;
        int[,] dataArray = new int[rowCount, columnCount];

        for (int i = 1; i < rowCount; i++)
        {
            string[] values = lines[i].Split(',');

            for (int j = 1; j < columnCount; j++)
            {
                if (int.TryParse(values[j], out int intValue))
                {
                    dataArray[i, j] = intValue;
                }
                else
                {
                    // Handle invalid format errors if necessary
                }
            }
        }

        return dataArray;
    }
    public static int[,] MidPoint = new int[1, 2];
    public static int[] angle = new int[4];
    class EdgeDetection
    {
        static int[,] gradient;
        public static int[,] Blur_Image(int[,] GrayImage)
        {
            int width = GrayImage.GetLength(0);
            int height = GrayImage.GetLength(1);

            int[,] BlurImage = new int[width, height];

            double[,] GaussianKernel =  {   { 2,  4,  5,  4,  2 },
                                                    { 4,  9,  12, 9,  4 },
                                                    { 5,  12, 15, 12, 5 },
                                                    { 4,  9,  12, 9,  4 },
                                                    { 2,  4,  5,  4,  2 }   };
            for (int x = 2; x < width - 2; x++)
            {
                for (int y = 2; y < height - 2; y++)
                {
                    double sum = 0;
                    for (int i = -2; i < 2; i++)
                    {
                        for (int j = -2; j < 2; j++)
                        {
                            sum += GaussianKernel[i + 2, j + 2] * GrayImage[x + i, y + j];

                        }
                    }
                    BlurImage[x, y] = (int)(sum / 159);
                }
            }

            return BlurImage;
        }
        public static int[,] Canny_Detect(int[,] BlurredImage, int high_threshold, int low_threshold)
        {
            //int[,] GradientX = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
            //int[,] GradientY = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
            int[,] gradientMagnitude = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
            int[,] gradientAngle = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
            int[,] Result = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
            int[,] SobelX = {{ -1, 0, 1 },
                                        { -2, 0, 2 },
                                        { -1, 0, 1 }};
            int[,] SobelY = {{ -1, -2, -1 },
                                        { 0, 0, 0 },
                                        { 1, 2, 1 }};
            int white_point = 255;
            int gray_point = 50;
            int max_gradient = 0;
            //computting gradient magnitude and gradient angle
            for (int x = 1; x < BlurredImage.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < BlurredImage.GetLength(1) - 1; y++)
                {
                    int sumX = 0;
                    int sumY = 0;
                    for (int i = -1; i <= 1; i++)
                    {
                        for (int j = -1; j <= 1; j++)
                        {
                            sumX += (int)(SobelX[i + 1, j + 1] * BlurredImage[x + i, y + j]);
                            sumY += (int)(SobelY[i + 1, j + 1] * BlurredImage[x + i, y + j]);
                        }
                    }
                    //GradientX[x, y] = sumX;
                    //GradientY[x, y] = sumY;
                    gradientMagnitude[x, y] = (int)Math.Sqrt(sumX * sumX + sumY * sumY);
                    if (gradientMagnitude[x, y] > max_gradient)
                    {
                        max_gradient = gradientMagnitude[x, y];
                    }
                    if (gradientMagnitude[x, y] >= 255)
                    {
                        gradientMagnitude[x, y] = 255;
                    }
                    else if (gradientMagnitude[x, y] <= 0)
                    {
                        gradientMagnitude[x, y] = 0;
                    }
                    gradientAngle[x, y] = (int)Math.Abs((Math.Atan2(sumY, sumX) * 180 / Math.PI));

                }
            }

            for (int x = 1; x < BlurredImage.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < BlurredImage.GetLength(1) - 1; y++)
                {
                    double q = 255;
                    double r = 255;
                    if ((gradientAngle[x, y] >= 0 && gradientAngle[x, y] < 22.5) || (gradientAngle[x, y] <= 180 && gradientAngle[x, y] >= 157.5))
                    {
                        q = gradientMagnitude[x, y - 1];
                        r = gradientMagnitude[x, y + 1];
                    }
                    else if (gradientAngle[x, y] >= 22.5 && gradientAngle[x, y] < 67.5)
                    {
                        q = gradientMagnitude[x + 1, y - 1];
                        r = gradientMagnitude[x - 1, y + 1];
                    }
                    else if (gradientAngle[x, y] >= 67.5 && gradientAngle[x, y] < 112.5)
                    {
                        q = gradientMagnitude[x + 1, y];
                        r = gradientMagnitude[x - 1, y];
                    }
                    else if (gradientAngle[x, y] >= 112.5 && gradientAngle[x, y] < 157.5)
                    {
                        q = gradientMagnitude[x - 1, y - 1];
                        r = gradientMagnitude[x + 1, y + 1];
                    }
                    if (gradientMagnitude[x, y] >= q && gradientMagnitude[x, y] >= r)
                    {
                        Result[x, y] = gradientMagnitude[x, y];
                    }
                    else
                    {
                        Result[x, y] = 0;
                    }

                    if (Result[x, y] >= high_threshold)
                    {
                        Result[x, y] = white_point;
                    }
                    else if (Result[x, y] >= low_threshold && Result[x, y] < high_threshold)
                    {
                        Result[x, y] = gray_point;
                    }
                    else
                    {
                        Result[x, y] = 0;
                    }
                }
            }
            //int[,] output=new int[gradient.GetLength(0) , gradient.GetLength(1) ];
            for (int x = 1; x < Result.GetLength(0) - 1; x++)
            {
                for (int y = 1; y < Result.GetLength(1) - 1; y++)
                {
                    if (Result[x, y] == gray_point)
                    {
                        if ((Result[x + 1, y - 1] == white_point) ||
                            (Result[x + 1, y] == white_point) ||
                            (Result[x + 1, y + 1] == white_point) ||
                            (Result[x, y - 1] == white_point) ||
                            (Result[x, y + 1] == white_point) ||
                            (Result[x - 1, y - 1] == white_point) ||
                            (Result[x - 1, y] == white_point) ||
                            (Result[x - 1, y + 1] == white_point))
                        {
                            Result[x, y] = white_point;
                        }
                        else
                            Result[x, y] = 0;
                    }
                }
            }
            for (int x = 0; x < Result.GetLength(0); x++)
            {
                for (int y = 0; y < Result.GetLength(1); y++)
                {
                    if (Result[x, y] >= 255)
                        Result[x, y] = 255;
                    else if (Result[x, y] <= 0)
                        Result[x, y] = 0;
                    if ((y < 5) || (y > Result.GetLength(1) - 5))
                        Result[x, y] = 0;
                    if ((x < 5) || (x > Result.GetLength(0) - 5))
                        Result[x, y] = 0;
                }
            }
            gradient = gradientMagnitude;
            return Result;
        }
        public static int[,] DeTectEdgeByCannyMethod(int[,] gray, int high_threshold, int low_threshold)
        {
            //Bitmap Import_picture = new Bitmap(imagePath);

            //int[,] gray = EdgeDetection.RGB2Gray(Import_picture);
            int[,] blur = EdgeDetection.Blur_Image(gray);
            //Bitmap img_blur = IntToBitmap(blur);
            int threhold = CalculateThreshold(blur);
            for (int i = 0; i < blur.GetLength(0); i++)
            {
                for (int j = 0; j < blur.GetLength(1); j++)
                {
                    if (blur[i, j] < threhold)
                    {
                        blur[i, j] = 0;
                    }
                    else
                    {
                        blur[i, j] = 255;
                    }
                }
            }
            int[,] edges = EdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);

            return edges;
        }
        public static int[,] PerformHoughTransform(int[,] image)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);

            // Tính số cột của ma trận Hough dựa trên đường chéo dài nhất trong ảnh
            int diagonalLength = (int)Math.Ceiling(Math.Sqrt(width * width + height * height));
            int houghWidth = 180; // Số cột của ma trận Hough
            int houghHeight = diagonalLength * 2; // Số hàng của ma trận Hough

            int[,] houghMatrix = new int[houghWidth, houghHeight];

            // Thực hiện Hough Transform
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (image[x, y] > 0) // Nếu điểm ảnh là điểm biên
                    {
                        for (int theta = 0; theta < houghWidth; theta++)
                        {
                            double radian = (theta * Math.PI) / 180.0;
                            int rho = (int)(x * Math.Cos(radian) + y * Math.Sin(radian));
                            rho += diagonalLength; // Dịch chuyển rho để không có giá trị âm
                            houghMatrix[theta, rho] = houghMatrix[theta, rho] + 1;
                            if (houghMatrix[theta, rho] > 255)
                            {
                                houghMatrix[theta, rho] = 255;
                            }

                        }
                    }
                }
            }

            return houghMatrix;
        }
        public static int[,] DrawLines3(int[,] houghMatrix, int threshold, int width, int height)
        {
            int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

            int[,] resultImage = new int[width, height];
            int count = 0;
            int[,] theta_rho = new int[2000, 4];
            // lấy toàn bộ số điểm vượt ngưỡng 
            for (int theta = 0; theta < 180; theta++)
            {
                for (int rho = 0; rho < 2 * diagonal; rho++)
                {
                    if (houghMatrix[theta, rho] >= threshold)
                    {
                        theta_rho[count, 0] = theta;//góc theta
                        theta_rho[count, 1] = rho;//khoảng cách từ đường thẳng đến gốc tọa độ
                        theta_rho[count, 2] = 0;//kiểm tra thử đã được chọn hay chưa
                        theta_rho[count, 3] = houghMatrix[theta, rho];// giá trị điểm vote
                        count++;
                    }
                }
            }
            //phân loại đường thẳng                
            int nhom = 10;
            //khac
            int[,,] ketqua = new int[count+1, 2, nhom];
            int[,] line = new int[4, 2];
            int soluong_nhom = 0;
            // phân nhóm
            for (int i = 0; i < nhom; i++)
            {
                int index = 0;
                //lấy số chuẩn cho mỗi nhóm điều kiện phải là số có hough lớn nhất còn lại
                int max_value_hough = 0;
                int max_index = 0;
                for (int j = 0; j < count; j++)
                {
                    if (theta_rho[j, 2] == 0)
                    {
                        if (theta_rho[j, 3] > max_value_hough)
                        {
                            max_value_hough = theta_rho[j, 3];
                            max_index = j;
                        }
                    }
                }
                ketqua[index, 0, i] = theta_rho[max_index, 0];// lấy giá trị theta
                ketqua[index, 1, i] = theta_rho[max_index, 1];// lấy giá trị rho
                theta_rho[max_index, 2] = 1;
                index++;
                soluong_nhom++;
                //phân nhóm
                int delta_a = 0;
                int delta_p = 0;
                int delta_a_threshold = 30;
                int delta_p_threshold = 70;
                for (int j = 0; j < count; j++)
                {
                    if (theta_rho[j, 2] == 0)
                    {
                        delta_a = Math.Abs(ketqua[0, 0, i] - theta_rho[j, 0]);
                        delta_p = Math.Abs(ketqua[0, 1, i] - theta_rho[j, 1]);
                        if (((delta_a < delta_a_threshold) || (delta_a > 180 - delta_a_threshold)) && (delta_p < delta_p_threshold))
                        {
                            ketqua[index, 0, i] = theta_rho[j, 0];
                            ketqua[index, 1, i] = theta_rho[j, 1];
                            theta_rho[j, 2] = 1;
                            index++;
                        }
                    }
                }

                ////tìm đường trung bình trong các nhóm (chỗ này hơi ngu nha)
                //int avr_theta1 = 0;
                //int avr_rho1 = 0;
                //if (index != 0)
                //{
                //    for (int j = 0; j < index; j++)
                //    {
                //        avr_theta1 = (avr_theta1 + ketqua[j, 0, i]);
                //        avr_rho1 = (avr_rho1 + ketqua[j, 1, i]);
                //    }
                //    line[i, 0] = avr_theta1 / (index);
                //    line[i, 1] = avr_rho1 / (index);
                //}
                // chỉ lấy 4 đường có số lượng đường thẳng nhiều nhất                   
                if (soluong_nhom >= 4)// điều kiện thoát vòng lặp
                {
                    i = nhom;
                }

            }
            for (int i = 0; i < 4; i++)
            {
                int avr_theta = ketqua[0, 0, i];
                angle[i] = ketqua[0, 0, i];
                int avr_rho = ketqua[0, 1, i];
                //vẽ đường thẳng
                double radianTheta = avr_theta * Math.PI / 180;
                if (avr_theta >= 45 && avr_theta < 135)
                {
                    for (int x = 0; x < width; x++)
                    {
                        int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
                        if (y >= 0 && y < height)
                        {
                            //resultImage[x, y] = 255;
                            if (resultImage[x, y] == 0)
                            {
                                resultImage[x, y] = 255;
                                if (y + 1 < resultImage.GetLength(1))
                                {
                                    resultImage[x, y + 1] = 255;
                                }

                            }
                            else if (resultImage[x, y] == 255)
                            {
                                resultImage[x, y] = 10;

                                if (y + 1 < resultImage.GetLength(1))
                                {
                                    resultImage[x, y + 1] = 10;
                                }
                            }
                            else if (resultImage[x, y] == 10)
                            {
                                //DO NOTHING
                            }
                        }
                    }
                }
                else
                {
                    for (int y = 0; y < height; y++)
                    {
                        int x = (int)(((avr_rho - diagonal) - y * Math.Sin(radianTheta)) / Math.Cos(radianTheta));
                        if (x >= 0 && x < width)
                        {
                            //resultImage[x, y] = 255;
                            if (resultImage[x, y] == 0)
                            {
                                resultImage[x, y] = 255;//vẽ line 

                                if (x + 1 < resultImage.GetLength(0))
                                {
                                    resultImage[x + 1, y] = 255;
                                }
                            }
                            else if (resultImage[x, y] == 255)
                            {
                                resultImage[x, y] = 10;//giao điểm

                                if (x + 1 < resultImage.GetLength(0))
                                {
                                    resultImage[x + 1, y] = 10;
                                }
                            }
                            else if (resultImage[x, y] == 10)
                            {
                                //DO NOTHING
                                //nền
                            }
                        }
                    }
                }
                //
            }

            return resultImage;
        }
        
        // Đếm số lượng điểm lân cận
        private static int CountNeighbors(int x, int y, int[,] image)
        {
            int count = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    if (image[x + i, y + j] == 255)
                    {
                        count++;
                    }
                }
            }
            return count - 1;
        }
        // Đếm số lượng chuyển đổi
        private static int Transitions(int x, int y, int[,] image)
        {
            int count = 0;
            int[] values = { image[x, y + 1], image[x - 1, y + 1], image[x - 1, y],
                             image[x - 1, y - 1], image[x, y - 1], image[x + 1, y - 1],
                             image[x + 1, y], image[x + 1, y + 1], image[x, y + 1] };

            for (int i = 0; i < values.Length - 1; i++)
            {
                if (values[i] == 0 && values[i + 1] == 255)
                {
                    count++;
                }
            }
            return count;
        }
        // Lấy giá trị các điểm lân cận
        private static int[] GetNeighborPixels(int x, int y, int[,] image)
        {
            int[] pixels = new int[9];
            int index = 0;
            for (int j = -1; j <= 1; j++)
            {
                for (int i = -1; i <= 1; i++)
                {
                    pixels[index++] = image[x + i, y + j];
                }
            }
            return pixels;
        }
        public static int CalculateThreshold(int[,] image)
        {
            // Tính histogram
            int[] histogram = new int[256];
            int totalPixels = 0;

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {
                    //int pixel = image[x, y];
                    if (image[x,y]>255)
                    {
                        image[x,y] = 255;
                    }    
                    int grayLevel = image[x, y];
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
        public static int Point_corner(int[,] Image_Egde)
        {
            int Mid_Point_X = 0;
            int Mid_Point_Y = 0;
            int X = -80;
            int Y = -80;
            for (int i = 0; i < Image_Egde.GetLength(0); i++)
            {
                for (int j = 0; j < Image_Egde.GetLength(1); j++)
                {
                    if (Image_Egde[i, j] == 10)
                    {
                        //tìm trọng tâm
                        if (Math.Sqrt((X - i) * (X - i) + (Y - j) * (Y - j)) > 25)
                        {
                            Mid_Point_X += i;
                            Mid_Point_Y += j;
                            X = i; Y = j;
                        }
                        
                    }

                }
            }
            Mid_Point_X /= 4;
            Mid_Point_Y /= 4;
            MidPoint[0, 0] = Mid_Point_X;
            MidPoint[0, 1] = Mid_Point_Y;
            return 0;
        }
    }
}

