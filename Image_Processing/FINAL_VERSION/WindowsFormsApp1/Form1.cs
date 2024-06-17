using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Drawing.Imaging;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string response_client;
        static byte[,] byteArray2D;
        public static int[] angle = new int[4];
        string imagePath;
        public Form1()
        {
            InitializeComponent();
        }
        class DetectShape
        {
            private static byte[,] Erosion(byte[,] image)
            {
                int width = image.GetLength(0);
                int height = image.GetLength(1);
                byte[,] result = new byte[width, height];
                //for (int a =0; a< interations; a++)
                //{
                // Duyệt qua từng pixel trong ảnh
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Kiểm tra xem pixel hiện tại có giá trị 255 hay không
                        if (image[x, y] == 255)
                        {
                            bool isEroded = true;

                            // Duyệt qua các pixel lân cận
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    int neighborX = x + i;
                                    int neighborY = y + j;

                                    // Kiểm tra xem pixel lân cận có nằm trong phạm vi ảnh không
                                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                                    {
                                        // Kiểm tra xem pixel lân cận có giá trị 255 hay không
                                        if (image[neighborX, neighborY] != 255)
                                        {
                                            isEroded = false;
                                            break;
                                        }
                                    }
                                }

                                if (!isEroded)
                                {
                                    break;
                                }
                            }

                            // Gán giá trị 255 cho pixel hiện tại nếu nó không bị co
                            if (isEroded)
                            {
                                result[x, y] = 255;
                            }
                            else
                            {
                                result[x, y] = 0;
                            }
                        }
                        else
                        {
                            result[x, y] = 0;
                        }
                    }
                }
                //}    
                return result;
            }
            private static byte[,] Dilation(byte[,] image)
            {
                int width = image.GetLength(0);
                int height = image.GetLength(1);
                byte[,] result = new byte[width, height];
                //for (int a=0; a<interations; a++)
                //{
                // Duyệt qua từng pixel trong ảnh
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Kiểm tra xem pixel hiện tại có giá trị 255 hay không
                        if (image[x, y] == 255)
                        {
                            // Duyệt qua các pixel lân cận
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    int neighborX = x + i;
                                    int neighborY = y + j;

                                    // Kiểm tra xem pixel lân cận có nằm trong phạm vi ảnh không
                                    if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                                    {
                                        // Gán giá trị 255 cho pixel lân cận
                                        result[neighborX, neighborY] = 255;
                                    }
                                }
                            }
                        }
                    }
                }
                //}    
                return result;
            }
            public static byte[,] Erosion_Dilation(byte[,] image, int iterations_Erosion, int iterations_Dilation)
            {
                byte[,] result = image;
                // Thực hiện phép mở rộng (dilation)
                for (int i = 0; i < iterations_Dilation; i++)
                {
                    result = Dilation(result);
                }
                // Thực hiện phép co (erosion) 
                for (int i = 0; i < iterations_Erosion; i++)
                {
                    result = Erosion(result);
                }

                return result;
            }
            public static byte[,] BitmapToInt(Bitmap bitmap)
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                byte[,] imageArray = new byte[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color pixelColor = bitmap.GetPixel(i, j);
                        byte grayscaleValue = pixelColor.R;
                        imageArray[i, j] = grayscaleValue;
                    }
                }

                return imageArray;
            }
            public static Bitmap IntToBitmap(byte[,] Binary_Image)
            {
                // Kích thước hình ảnh
                int width = Binary_Image.GetLength(0);
                int height = Binary_Image.GetLength(1);

                // Tạo bitmap từ mảng hai chiều
                Bitmap Result = new Bitmap(width, height, PixelFormat.Format16bppRgb565);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color color;
                        int pixelValue = (int)Binary_Image[x, y];
                        color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
                        Result.SetPixel(x, y, color);
                    }
                }
                return Result;
            }
            public static int[] CalculateTwoThresholds(byte[,] grayImage)
            {
                int[] result = new int[2];
                int width = grayImage.GetLength(0);
                int height = grayImage.GetLength(1);
                int totalPixels = width * height;

                // Tính histogram
                int[] histogram = new int[256];
                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        histogram[grayImage[i, j]]++;
                    }
                }

                // Tính xác suất
                double[] probability = new double[256];
                for (int i = 0; i < 256; i++)
                {
                    probability[i] = (double)histogram[i] / totalPixels;
                }

                // Tìm ngưỡng tối ưu
                double maxVariance = double.MinValue;
                int threshold1 = 0;
                int threshold2 = 0;

                for (int t1 = 0; t1 < 256; t1++)
                {
                    for (int t2 = t1 + 1; t2 < 256; t2++)
                    {
                        // Các lớp
                        double w0 = 0, w1 = 0, w2 = 0;
                        double m0 = 0, m1 = 0, m2 = 0;

                        for (int i = 0; i < t1; i++)
                        {
                            w0 += probability[i];
                            m0 += i * probability[i];
                        }

                        for (int i = t1; i < t2; i++)
                        {
                            w1 += probability[i];
                            m1 += i * probability[i];
                        }

                        for (int i = t2; i < 256; i++)
                        {
                            w2 += probability[i];
                            m2 += i * probability[i];
                        }

                        if (w0 > 0) m0 /= w0;
                        if (w1 > 0) m1 /= w1;
                        if (w2 > 0) m2 /= w2;

                        double betweenClassVariance = w0 * w1 * Math.Pow(m0 - m1, 2) + w1 * w2 * Math.Pow(m1 - m2, 2) + w0 * w2 * Math.Pow(m0 - m2, 2);

                        if (betweenClassVariance > maxVariance)
                        {
                            maxVariance = betweenClassVariance;
                            threshold1 = t1;
                            threshold2 = t2;
                        }
                    }
                }
                result[0] = threshold1;
                result[1] = threshold2;
                return result;
            }
            public static byte[,] Threshold_Image(byte[,] grayImage, int T1, int T2)
            {
                byte[,] thres_image =grayImage;
                for (int i = 0; i < thres_image.GetLength(0); i++)
                {
                    for (int j = 0; j < thres_image.GetLength(1); j++)
                    {
                        if ((thres_image[i, j] >= T1 - 1 && thres_image[i, j] <= T2))
                        {
                            thres_image[i, j] = 255;
                        }
                        else if ((thres_image[i, j] >= T2 && thres_image[i, j] <= 255))
                        {
                            thres_image[i, j] = 255;
                        }
                        else
                        {
                            thres_image[i, j] = 0;
                        }
                    }
                }
                return thres_image;
            }
            public static byte[,] Canny_Detect(byte[,] BlurredImage, int high_threshold, int low_threshold)
            {
                //int[,] GradientX = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                //int[,] GradientY = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                int[,] gradientMagnitude = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
                int[,] gradientAngle = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
                byte[,] Result = new byte[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
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
                            Result[x, y] = (byte)gradientMagnitude[x, y];
                        }
                        else
                        {
                            Result[x, y] = 0;
                        }

                        if (Result[x, y] >= high_threshold)
                        {
                            Result[x, y] = (byte)white_point;
                        }
                        else if (Result[x, y] >= low_threshold && Result[x, y] < high_threshold)
                        {
                            Result[x, y] = (byte)gray_point;
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
                                Result[x, y] = (byte)white_point;
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
                return Result;
            }
            public static double Moment_Calculate(byte[,] Binary_image, int i, int j)
            {
                double Moment = 0;
                for (int x = 0; x < Binary_image.GetLength(0); x++)
                {
                    for (int y = 0; y < Binary_image.GetLength(1); y++)
                    {
                        Moment = Moment + Math.Pow(x, i) * Math.Pow(y, j) * Binary_image[x, y];
                    }
                }
                return Moment;
            }
            public static double CentralMoment1(byte[,] Binary_image, int i, int j)
            {
                double U_ij = 0;
                double avr_X = (Moment_Calculate(Binary_image, 1, 0)) / (Moment_Calculate(Binary_image, 0, 0));
                double avr_Y = (Moment_Calculate(Binary_image, 0, 1)) / (Moment_Calculate(Binary_image, 0, 0));
                for (int x = 0; x < Binary_image.GetLength(0); x++)
                {
                    for (int y = 0; y < Binary_image.GetLength(1); y++)
                    {
                        U_ij = U_ij + Math.Pow((x - avr_X), i) * Math.Pow((y - avr_Y), j) * Binary_image[x, y];
                    }
                }
                return U_ij;
            }
            public static double CentralMoment2(byte[,] Binary_image, int i, int j)
            {
                double U_ij = CentralMoment1(Binary_image, i, j);
                double U_00 = CentralMoment1(Binary_image, 0, 0);
                double N_ij = U_ij / Math.Pow(U_00, (i + j + 2) / 2);
                return N_ij;
            }
            public static double HuMoment(byte[,] Binary_image)
            {
                double H = 0.0;
                double N_20 = CentralMoment2(Binary_image, 2, 0);
                double N_02 = CentralMoment2(Binary_image, 0, 2);

                H = N_20 + N_02;
                
                return H;
            }           
            public static string Detect_Shape(byte[,]Binary_image)
            {
                string Result;
                double H = HuMoment(Binary_image);
                // Dữ liệu hình chữ nhật,tròn, vuông
                if (H > 0.000623 && H < 0.000628)
                { Result = "HÌNH TRÒN"; }
                else if (H > 0.000650 && H < 0.000658)
                { Result = "HÌNH VUÔNG"; }
                else if (H > 0.000691 && H < 0.000851)
                { Result = "HÌNH CHỮ NHẬT"; }
                else
                { Result = "KHÔNG NHẬN DIỆN "; }
                return Result;
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
            //dev ở hàm này về việc nhận ra kích thước(px), xác định cạnh đáy và (góc) của hình chữ nhật, hình vuông
            public static int[,] Lines(int[,] houghMatrix, int threshold, int width, int height, int edges)
            {
                int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

                int[,] resultImage = new int[width, height];
                int count = 0;
                int[,] theta_rho = new int[5000, 4];
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
                int[,,] ketqua = new int[count + 1, 2, nhom];
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
                    int delta_a_threshold = 20;
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
                    if (soluong_nhom >= edges)// điều kiện thoát vòng lặp
                    {
                        i = nhom;
                    }

                }
                for (int i = 0; i < edges; i++)
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
            //dev hàm tìm tâm, bán kính của hình tròn
            public static int Circle(byte[,]Binary_image, int avr_X, int avr_Y)
            {
                int Radius=0;
                int count = 0;
                for (int i=0;i< Binary_image.GetLength(0); i++)
                {
                    for(int j=0; j < Binary_image.GetLength(1); j++)
                    {
                        if (Binary_image[i,j]==255)
                        {
                            int Distance = (int)Math.Sqrt((i - avr_X) * (i - avr_X) + (j - avr_Y) * (j - avr_Y));
                            count++;
                            Radius += Distance;
                        }    
                        
                    }    
                }
                Radius /= count;
                return Radius;
            }
            public static void Active(out byte[,]Final_image, out string shape, out double avr_X, out double avr_Y, out int Radiuss)
            {
                int[] threshold = new int[2];
                //tinh nguong bang phuong phap Otsu cho ra 2 nguong phan doan anh
                threshold = CalculateTwoThresholds(byteArray2D);
                //phan nguong anh 
                //cần lọc nhiễu ở bước này
                Final_image = Threshold_Image(byteArray2D, threshold[0], threshold[1]);
                Final_image = Erosion_Dilation(Final_image,5,5);
                //Trong tam cua vat
                avr_X = (Moment_Calculate(Final_image, 1, 0)) / (Moment_Calculate(Final_image, 0, 0));
                avr_Y = (Moment_Calculate(Final_image, 0, 1)) / (Moment_Calculate(Final_image, 0, 0));
                //phan biet hinh dang
                shape = Detect_Shape(Final_image);
                //cho ra anh canh bang phuong phap canny.
                Final_image = Canny_Detect(Final_image, 50, 200);

                Radiuss = 0;
                if (shape == "HÌNH CHỮ NHẬT" || shape == "HÌNH VUÔNG")
                {
                    // hàm Lines
                }
                else if (shape == "HÌNH TRÒN" )
                {
                    Radiuss = Circle(Final_image,(int)avr_X,(int)avr_Y);
                }
                else
                {
                    //do nothing
                }

            }

        }
        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void OPEN_Click(object sender, EventArgs e)
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
                Original_Pic.Image = image;
            }
            Bitmap bitmap = new Bitmap (imagePath);
            byteArray2D = DetectShape.BitmapToInt(bitmap);
        }
        private void PHOTO_Click(object sender, EventArgs e)
        {
            // Connect to the server
            string host = "192.168.000.49";
            int port = Convert.ToInt32("50010");//50010

            try
            {
                clientSocket.Connect(host, port);
                Console.WriteLine("Connected");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to connect");
            }
            // Perform your desired action here
            string CaptureImageMessage = "1003t\r\n";
            byte[] CaptureImageBytes = Encoding.ASCII.GetBytes(CaptureImageMessage);
            clientSocket.Send(CaptureImageBytes);
            // Receive the response from the server
            var buffer = new byte[308295];
            int bytesRead = clientSocket.Receive(buffer);

            System.Threading.Thread.Sleep(100); // Simulating 100ms delay

            string RequestImageMessage = "1003I?\r\n";
            // Send the command to the server
            byte[] RequestImageBytes = Encoding.ASCII.GetBytes(RequestImageMessage);
            clientSocket.Send(RequestImageBytes);

            string sentencetosend = "1003I?\r\n";

            // Receive the response from the server
            buffer = new byte[308295];
            bytesRead = clientSocket.Receive(buffer);

            if (RequestImageMessage == sentencetosend)
            {
                response_client = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //PrintLog("", "", response_client);
                //Console.WriteLine(response_client);
                while (bytesRead < 308291)
                {
                    bytesRead += clientSocket.Receive(buffer, bytesRead, 308291 - bytesRead, SocketFlags.None);
                }
            }

            response_client = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //PrintLog("Data received", "", response_client);

            // Sentences to remove
            string[] sentencesToRemove = { "1003000308278", "1003*", "1003?", "1003000307200" };
            //Console.WriteLine("hello");
            // Loop through each sentence and replace it with an empty string
            foreach (string sentence in sentencesToRemove)
            {
                response_client = response_client.Replace(sentence, "");
            }

            // Convert the modified response string back to bytes
            byte[] modifiedBuffer = Encoding.ASCII.GetBytes(response_client);
            // Convert each byte to its string representation
            string[] byteStrings = new string[modifiedBuffer.Length];
            for (int i = 0; i < modifiedBuffer.Length; i++)
            {
                byteStrings[i] = modifiedBuffer[i].ToString();
            }
            // Join the string representations of bytes
            string bmpString = string.Join(" ", byteStrings);
            // Split the byte string into individual byte values
            string[] byteValues = bmpString.Split();

            // Convert each byte value from string to integer
            List<byte> byteData = new List<byte>();


            foreach (string byteString in byteValues)
            {
                byteData.Add(Convert.ToByte(byteString));
            }

            // Define the number of bytes to delete from the beginning

            // Bitmap Header(14 bytes) + Bitmap Information (40 bytes) + Color Palette (4 * 256) = 1078 bytes to delete

            // The kinds of image format (RAW or bitmap) is based on the configuration on E2D200.exe
            int bytesToDelete = 1125; // Adjust this number according to your requirement

            // Delete the specified number of bytes from the beginning
            byteData.RemoveRange(0, bytesToDelete);

            // Convert the list of bytes back to byte array
            byte[] byteArrayModified = byteData.ToArray();

            // Determine the dimensions of the original image
            int width = 640;  // Adjust according to your image width
            int height = 480;  // Adjust according to your image height

            // Calculate the new dimensions of the image after removing bytes
            int newWidth = width;  // Since bytes removed from the beginning don't affect width
            int newHeight = height;  // Adjust height accordingly

            // convert from 1-D array to 2-D array
            byteArray2D = new byte[newHeight, newWidth];
            byteArray2D = ConvertTo2DArray(byteArrayModified, newHeight, newWidth);
            Bitmap Gray_Image = new Bitmap(newWidth, newHeight);
            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {
                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Color color;
                    int pixelValue = byteArray2D[x, y];
                    color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
                    Gray_Image.SetPixel(x, y, color);

                }
            }
            //Hien thi hinh goc trong picBox_Hinhgoc da tao
            Original_Pic.Image = Gray_Image;        
        }
        public static byte[,] ConvertTo2DArray(byte[] array1D, int rows, int columns)
        {
            byte[,] array2D = new byte[rows, columns];
            int index = 0;

            // Iterate over the elements of the 1D array and assign them to the 2D array
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Check if there are enough elements in the 1D array
                    if (index < array1D.Length)
                    {
                        array2D[i, j] = array1D[index];
                        index++;
                    }
                    else
                    {
                        array2D[i, j] = 0;
                    }
                }
            }
            return array2D;
        }
        private void ACTIVE_Click(object sender, EventArgs e)
        {
            byte[,] Result = new byte[640, 480];
            DetectShape.Active(out Result, out string shape,out double avr_x,out double avr_y,out int Radius);
            Detect_shape.Text = shape;
            Radius_circle.Text = Convert.ToString(Radius);
            Center_X.Text = Convert.ToString((int)avr_x);
            Center_Y.Text = Convert.ToString((int)avr_y);
            Processed_Pic.Image = DetectShape.IntToBitmap(Result);
        }
    }
}
