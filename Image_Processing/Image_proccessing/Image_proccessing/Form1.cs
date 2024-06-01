using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Security.Cryptography.X509Certificates;
using System.Windows.Forms;

namespace Image_proccessing
{
    public partial class Form1 : Form
    {
        static string imagePath;
        public Form1()
        {
            InitializeComponent();

        }
        static int[,] gradient;
        public static int[,] MidPoint= new int[1,2];
        public static int[] angle= new int [4];
        class EdgeDetection
        {
            
            public static int[,] RGB2Gray(Bitmap ColorImage)
            {

                int width = ColorImage.Width;
                int height = ColorImage.Height;
                int[,] GrayImage = new int[width, height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = ColorImage.GetPixel(x, y);
                        int GrayValue = (int)(pixelColor.R );
                        GrayImage[x, y] = GrayValue;
                    }
                }

                return GrayImage;
            }
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
            public static int[,] MedianBlur(int[,] image)
            {
                int width = image.GetLength(0);
                int height = image.GetLength(1);
                int[,] result = new int[width, height];

                // Duyệt qua từng pixel trong ảnh
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Lấy giá trị của các pixel lân cận
                        List<int> neighbors = new List<int>();

                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                int neighborX = x + i;
                                int neighborY = y + j;

                                // Kiểm tra xem pixel lân cận có nằm trong phạm vi ảnh không
                                if (neighborX >= 0 && neighborX < width && neighborY >= 0 && neighborY < height)
                                {
                                    neighbors.Add(image[neighborX, neighborY]);
                                }
                            }
                        }

                        // Sắp xếp các giá trị pixel lân cận theo thứ tự tăng dần
                        for (int k = 0; k < neighbors.Count - 1; k++)
                        {
                            for (int l = k + 1; l < neighbors.Count; l++)
                            {
                                if (neighbors[k] > neighbors[l])
                                {
                                    int temp = neighbors[k];
                                    neighbors[k] = neighbors[l];
                                    neighbors[l] = temp;
                                }
                            }
                        }

                        // Lấy trung vị của các giá trị pixel lân cận
                        int median = neighbors[neighbors.Count / 2];

                        // Gán giá trị trung vị cho pixel hiện tại
                        result[x, y] = median;
                    }
                }

                return result;
            }
            public static int[,] Erosion(int[,] image)
            {
                int width = image.GetLength(0);
                int height = image.GetLength(1);
                int[,] result = new int[width, height];

                // Duyệt qua từng pixel trong ảnh
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        // Kiểm tra xem pixel hiện tại có giá trị 0 hay không
                        if (image[x, y] == 0)
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
                                        // Kiểm tra xem pixel lân cận có giá trị 0 hay không
                                        if (image[neighborX, neighborY] != 0)
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

                            // Gán giá trị 0 cho pixel hiện tại nếu nó bị co
                            if (isEroded)
                            {
                                result[x, y] = 0;
                            }
                            else
                            {
                                result[x, y] = 255;
                            }
                        }
                        else
                        {
                            result[x, y] = 255;
                        }
                    }
                }

                return result;
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
            public static int[,] DeTectEdgeByCannyMethod(string imagePath, int high_threshold, int low_threshold)
            {
                Bitmap Import_picture = new Bitmap(imagePath);

                int[,] gray = EdgeDetection.RGB2Gray(Import_picture);
                //int[,] blur = EdgeDetection.Blur_Image(gray);
                int[,] blur = gray;
                //Bitmap img_blur = IntToBitmap(blur);
                //int threhold = CalculateThreshold(img_blur);
                int threhold1 = 63;
                int threhold2 = 65;
                for (int i = 0; i < blur.GetLength(0); i++)
                {
                    for (int j = 0; j < blur.GetLength(1); j++)
                    {
                        if (blur[i, j] == 63)
                        {
                            blur[i, j] = 255;
                        }
                        else
                        {
                            blur[i, j] = 0;
                        }
                    }
                }
                blur = MedianBlur(blur);
               // blur = Erosion(blur);
                //int[,] edges = EdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);

                return blur;
            }
            public static Bitmap IntToBitmap(int[,] Binary_Image)
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
                        //if (pixelValue == 10)
                        //{
                        //    color = Color.FromArgb(255, 0, 0);
                        //}  
                        //else
                        //{
                        //    color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
                        //}    
                        color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
                        Result.SetPixel(x, y, color);
                    }
                }
                return Result;
            }
            public static int[,] BitmapToInt(Bitmap bitmap)
            {
                int width = bitmap.Width;
                int height = bitmap.Height;

                int[,] imageArray = new int[width, height];

                for (int i = 0; i < width; i++)
                {
                    for (int j = 0; j < height; j++)
                    {
                        Color pixelColor = bitmap.GetPixel(i, j);
                        int grayscaleValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        imageArray[i, j] = grayscaleValue;
                    }
                }

                return imageArray;
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
            //public static int[,] DrawLines(int[,] houghMatrix, int threshold, int width, int height)
            //{
            //    int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

            //    int[,] resultImage = new int[width, height];
            //    int count = 0;
            //    int[,] theta_rho = new int[1000, 3];

            //    for (int theta = 0; theta < 180; theta++)
            //    {
            //        for (int rho = 0; rho < 2 * diagonal; rho++)
            //        {
            //            if (houghMatrix[theta, rho] >= threshold)
            //            {
            //                // lấy toàn bộ số điểm vượt ngưỡng 
            //                theta_rho[count, 0] = theta;
            //                theta_rho[count, 1] = rho;
            //                theta_rho[count, 2] = 0;
            //                count++;
            //            }
            //        }
            //    }

            //    //phân loại đường thẳng                
            //    int nhom = 4;
            //    int[,,] ketqua = new int[count, 2, nhom];

            //    for (int i = 0; i < nhom; i++)
            //    {
            //        int index = 0;

            //        //lấy số chuẩn
            //        for (int j = 0; j < count; j++)
            //        {
            //            if (theta_rho[j, 2] == 0)
            //            {
            //                ketqua[index, 0, i] = theta_rho[j, 0];
            //                ketqua[index, 1, i] = theta_rho[j, 1];
            //                theta_rho[j, 2] = 1;
            //                index++;
            //                j = count;//thoát vòng lặp

            //            }
            //        }
            //        //phân nhóm
            //        int delta_a = 0;
            //        int delta_p = 0;
            //        int delta_a_threshold = 30;
            //        int delta_p_threshold = 70;
            //        for (int j = 0; j < count; j++)
            //        {
            //            if (theta_rho[j, 2] == 0)
            //            {
            //                delta_a = Math.Abs(ketqua[0, 0, i] - theta_rho[j, 0]);
            //                delta_p = Math.Abs(ketqua[0, 1, i] - theta_rho[j, 1]);
            //                if (((delta_a < delta_a_threshold) || (delta_a > 180 - delta_a_threshold)) && (delta_p < delta_p_threshold))
            //                {
            //                    ketqua[index, 0, i] = theta_rho[j, 0];
            //                    ketqua[index, 1, i] = theta_rho[j, 1];
            //                    theta_rho[j, 2] = 1;
            //                    index++;
            //                }
            //            }
            //        }

            //        //tìm đường trung bình trong các nhóm
            //        int avr_theta1 = 0;
            //        int avr_rho1 = 0;
            //        if (index != 0)
            //        {
            //            for (int j = 0; j < index; j++)
            //            {
            //                avr_theta1 = (avr_theta1 + ketqua[j, 0, i]);
            //                avr_rho1 = (avr_rho1 + ketqua[j, 1, i]);
            //            }
            //            int avr_theta = avr_theta1 / (index);
            //            int avr_rho = avr_rho1 / (index);
            //            //vẽ đường thẳng
            //            double radianTheta = avr_theta * Math.PI / 180;
            //            if (avr_theta >= 45 && avr_theta < 135)
            //            {
            //                for (int x = 0; x < width; x++)
            //                {
            //                    int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
            //                    if (y >= 0 && y < height)
            //                    {
            //                        //resultImage[x, y] = 255;
            //                        if (resultImage[x, y] == 0)
            //                        {
            //                            resultImage[x, y] = 255;
            //                            if (y + 1 < resultImage.GetLength(1))
            //                            {
            //                                resultImage[x, y + 1] = 255;
            //                            }

            //                        }
            //                        else if (resultImage[x, y] == 255)
            //                        {
            //                            resultImage[x, y] = 10;

            //                            if (y + 1 < resultImage.GetLength(1))
            //                            {
            //                                resultImage[x, y + 1] = 10;
            //                            }
            //                        }
            //                        else if (resultImage[x, y] == 10)
            //                        {
            //                            //DO NOTHING
            //                        }
            //                    }
            //                }
            //            }
            //            else
            //            {
            //                for (int y = 0; y < height; y++)
            //                {
            //                    int x = (int)(((avr_rho - diagonal) - y * Math.Sin(radianTheta)) / Math.Cos(radianTheta));
            //                    if (x >= 0 && x < width)
            //                    {
            //                        //resultImage[x, y] = 255;
            //                        if (resultImage[x, y] == 0)
            //                        {
            //                            resultImage[x, y] = 255;//vẽ line 

            //                            if (x + 1 < resultImage.GetLength(0))
            //                            {
            //                                resultImage[x + 1, y] = 255;
            //                            }
            //                        }
            //                        else if (resultImage[x, y] == 255)
            //                        {
            //                            resultImage[x, y] = 10;//giao điểm

            //                            if (x + 1 < resultImage.GetLength(0))
            //                            {
            //                                resultImage[x + 1, y] = 10;
            //                            }
            //                        }
            //                        else if (resultImage[x, y] == 10)
            //                        {
            //                            //DO NOTHING
            //                            //nền
            //                        }
            //                    }
            //                }
            //            }
            //            //
            //        }

            //    }
            //    return resultImage;
            //}
            //public static int[,] DrawLines2(int[,] houghMatrix, int threshold, int width, int height)
            //{
            //    int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

            //    int[,] resultImage = new int[width, height];
            //    int count = 0;
            //    int[,] theta_rho = new int[5000, 4];
            //    // lấy toàn bộ số điểm vượt ngưỡng 
            //    for (int theta = 0; theta < 180; theta++)
            //    {
            //        for (int rho = 0; rho < 2 * diagonal; rho++)
            //        {
            //            if (houghMatrix[theta, rho] >= threshold)
            //            {
            //                theta_rho[count, 0] = theta;//góc theta
            //                theta_rho[count, 1] = rho;//khoảng cách từ đường thẳng đến gốc tọa độ
            //                theta_rho[count, 2] = 0;//kiểm tra thử đã được chọn hay chưa
            //                theta_rho[count, 3] = houghMatrix[theta, rho];// giá trị điểm vote
            //                count++;
            //            }
            //        }
            //    }
            //    //phân loại đường thẳng                
            //    int nhom = 4;
            //    int[,,] ketqua = new int[count, 2, nhom];
            //    int[,] line = new int[4, 2];
            //    int soluong_nhom = 0;
            //    // phân nhóm
            //    for (int i = 0; i < nhom; i++)
            //    {
            //        int index = 0;


            //        //lấy số chuẩn cho mỗi nhóm điều kiện phải là số có hough lớn nhất còn lại
            //        int max_value_hough = 0;
            //        int max_index = 0;
            //        for (int j = 0; j < count; j++)
            //        {
            //            if (theta_rho[j, 2] == 0)
            //            {
            //                if (theta_rho[j, 3] > max_value_hough)
            //                {
            //                    max_value_hough = theta_rho[j, 3];
            //                    max_index = j;
            //                }
            //            }
            //        }
            //        ketqua[index, 0, i] = theta_rho[max_index, 0];// lấy giá trị theta
            //        ketqua[index, 1, i] = theta_rho[max_index, 1];// lấy giá trị rho
            //        theta_rho[max_index, 2] = 1;
            //        index++;
            //        soluong_nhom++;
            //        //phân nhóm
            //        int delta_a = 0;
            //        int delta_p = 0;
            //        int delta_a_threshold = 30;
            //        int delta_p_threshold = 70;
            //        for (int j = 0; j < count; j++)
            //        {
            //            if (theta_rho[j, 2] == 0)
            //            {
            //                delta_a = Math.Abs(ketqua[0, 0, i] - theta_rho[j, 0]);
            //                delta_p = Math.Abs(ketqua[0, 1, i] - theta_rho[j, 1]);
            //                if (((delta_a < delta_a_threshold) || (delta_a > 180 - delta_a_threshold)) && (delta_p < delta_p_threshold))
            //                {
            //                    ketqua[index, 0, i] = theta_rho[j, 0];
            //                    ketqua[index, 1, i] = theta_rho[j, 1];
            //                    theta_rho[j, 2] = 1;
            //                    index++;
            //                }
            //            }
            //        }

            //        //tìm đường trung bình trong các nhóm
            //        int avr_theta1 = 0;
            //        int avr_rho1 = 0;
            //        if (index != 0)
            //        {
            //            for (int j = 0; j < index; j++)
            //            {
            //                avr_theta1 = (avr_theta1 + ketqua[j, 0, i]);
            //                avr_rho1 = (avr_rho1 + ketqua[j, 1, i]);
            //            }
            //            line[i, 0] = avr_theta1 / (index);
            //            line[i, 1] = avr_rho1 / (index);
            //        }
            //        // chỉ lấy 4 đường có số lượng đường thẳng nhiều nhất                   
            //        if (soluong_nhom >= 4)// điều kiện thoát vòng lặp
            //        {
            //            i = nhom;
            //        }

            //    }
            //    for (int i = 0; i < 4; i++)
            //    {
            //        int avr_theta = line[i, 0];
            //        int avr_rho = line[i, 1];
            //        //vẽ đường thẳng
            //        double radianTheta = avr_theta * Math.PI / 180;
            //        if (avr_theta >= 45 && avr_theta < 135)
            //        {
            //            for (int x = 0; x < width; x++)
            //            {
            //                int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
            //                if (y >= 0 && y < height)
            //                {
            //                    //resultImage[x, y] = 255;
            //                    if (resultImage[x, y] == 0)
            //                    {
            //                        resultImage[x, y] = 255;
            //                        if (y + 1 < resultImage.GetLength(1))
            //                        {
            //                            resultImage[x, y + 1] = 255;
            //                        }

            //                    }
            //                    else if (resultImage[x, y] == 255)
            //                    {
            //                        resultImage[x, y] = 10;

            //                        if (y + 1 < resultImage.GetLength(1))
            //                        {
            //                            resultImage[x, y + 1] = 10;
            //                        }
            //                    }
            //                    else if (resultImage[x, y] == 10)
            //                    {
            //                        //DO NOTHING
            //                    }
            //                }
            //            }
            //        }
            //        else
            //        {
            //            for (int y = 0; y < height; y++)
            //            {
            //                int x = (int)(((avr_rho - diagonal) - y * Math.Sin(radianTheta)) / Math.Cos(radianTheta));
            //                if (x >= 0 && x < width)
            //                {
            //                    //resultImage[x, y] = 255;
            //                    if (resultImage[x, y] == 0)
            //                    {
            //                        resultImage[x, y] = 255;//vẽ line 

            //                        if (x + 1 < resultImage.GetLength(0))
            //                        {
            //                            resultImage[x + 1, y] = 255;
            //                        }
            //                    }
            //                    else if (resultImage[x, y] == 255)
            //                    {
            //                        resultImage[x, y] = 10;//giao điểm

            //                        if (x + 1 < resultImage.GetLength(0))
            //                        {
            //                            resultImage[x + 1, y] = 10;
            //                        }
            //                    }
            //                    else if (resultImage[x, y] == 10)
            //                    {
            //                        //DO NOTHING
            //                        //nền
            //                    }
            //                }
            //            }
            //        }
            //        //
            //    }

            //    return resultImage;
            //}
            public static int[,] DrawLines3(int[,] houghMatrix, int threshold, int width, int height)
            {
                int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

                int[,] resultImage = new int[width, height];
                int count = 0;
                int[,] theta_rho = new int[1000000, 4];
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
                    if (soluong_nhom >= 4)// điều kiện thoát vòng lặp
                    {
                        i = nhom;
                    }

                }
                for (int i = 0; i < 4; i++)
                {
                    int avr_theta = ketqua[0, 0,i];
                    angle[i] = ketqua[0, 0, i];
                    int avr_rho = ketqua[0, 1,i];
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
            //public static int[,] FindCorner(int[,] line)
            //{
            //    int[,] PointMap = new int[line.GetLength(0), line.GetLength(1)];
            //    int distance = 0;
            //    int max_distance = 0;
            //    int Xmax = 0;
            //    int Ymax = 0;
            //    int Xmin = 0;
            //    int Ymin = 0;
            //    //tìm ra 2 điểm của 1 đường chéo 
            //    for (int i = 0; i < line.GetLength(0); i++)
            //    {
            //        for (int j = 0; j < line.GetLength(1); j++)
            //        {
            //            if (line[i, j] == 10)
            //            {
            //                Xmin = i;
            //                Ymin = j;
            //                PointMap[Xmin, Ymin] = 150;

            //                for (int a = i; a < line.GetLength(0); a++)
            //                {
            //                    for (int b = j; b < line.GetLength(1); b++)
            //                    {
            //                        if (line[a, b] == 10)
            //                        {
            //                            distance = (int)Math.Sqrt((i - a) * (i - a) + (j - b) * (j - b));
            //                            if (distance > max_distance)
            //                            {
            //                                max_distance = distance;
            //                                Xmax = a;
            //                                Ymax = b;
            //                            }
            //                        }
            //                    }
            //                }
            //                PointMap[Xmax, Ymax] = 150;
            //            }
            //        }
            //    }
            //    //Ax + By + C =0
            //    int A = Ymax - Ymin;
            //    int B = Xmax - Xmin;

            //    return PointMap;
            //}
            // Hàm dilation với số lần lặp lại xác định
            //public static int[,] Dilation(int[,] image, int iterations)
            //{
            //    int width = image.GetLength(0);
            //    int height = image.GetLength(1);
            //    int[,] dilatedImage = new int[width, height];

            //    for (int iter = 0; iter < iterations; iter++)
            //    {
            //        for (int y = 0; y < height; y++)
            //        {
            //            for (int x = 0; x < width; x++)
            //            {
            //                int maxValue = 0;
            //                for (int ky = -1; ky <= 1; ky++)
            //                {
            //                    for (int kx = -1; kx <= 1; kx++)
            //                    {
            //                        int nx = x + kx;
            //                        int ny = y + ky;
            //                        if (nx >= 0 && nx < width && ny >= 0 && ny < height)
            //                        {
            //                            maxValue = Math.Max(maxValue, image[nx, ny]);
            //                        }
            //                    }
            //                }
            //                dilatedImage[x, y] = maxValue;
            //            }
            //        }
            //        // Update ảnh đầu vào bằng ảnh đã dilation để tiếp tục lặp lại
            //        image = dilatedImage.Clone() as int[,];
            //    }

            //    return dilatedImage;
            //}
            //public static int[,] EdgeThinning(int[,] image)
            //{
            //    int width = image.GetLength(0);
            //    int height = image.GetLength(1);
            //    int[,] thinnedImage = (int[,])image.Clone(); // Tạo một bản sao của ảnh để thực hiện mỏng cạnh viền

            //    bool hasChanged = true; // Đặt biến hasChanged là true để bắt đầu vòng lặp

            //    while (hasChanged)
            //    {
            //        hasChanged = false; // Đặt lại hasChanged là false trước khi bắt đầu mỗi vòng lặp

            //        // Bước 1: Xác định và loại bỏ các điểm ảnh để làm mỏng cạnh viền
            //        for (int y = 1; y < height - 1; y++)
            //        {
            //            for (int x = 1; x < width - 1; x++)
            //            {
            //                if (thinnedImage[x, y] == 255) // Nếu điểm ảnh đang xét là một điểm cạnh
            //                {
            //                    int count = CountNeighbors(x, y, thinnedImage);

            //                    if (count >= 2 && count <= 6) // Nếu số lượng điểm lân cận thỏa mãn
            //                    {
            //                        int transitions = Transitions(x, y, thinnedImage);

            //                        if (transitions == 1) // Nếu chỉ có một điểm chuyển đổi
            //                        {
            //                            int[] pixels = GetNeighborPixels(x, y, thinnedImage);

            //                            if (pixels[0] * pixels[2] * pixels[4] == 0 && pixels[2] * pixels[4] * pixels[6] == 0) // Kiểm tra điều kiện đặc biệt
            //                            {
            //                                thinnedImage[x, y] = 0;
            //                                hasChanged = true;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }

            //        // Bước 2: Lặp lại bước 1 với các điểm ảnh đang xét được đảo ngược
            //        for (int y = 1; y < height - 1; y++)
            //        {
            //            for (int x = 1; x < width - 1; x++)
            //            {
            //                if (thinnedImage[x, y] == 255)
            //                {
            //                    int count = CountNeighbors(x, y, thinnedImage);

            //                    if (count >= 2 && count <= 6)
            //                    {
            //                        int transitions = Transitions(x, y, thinnedImage);

            //                        if (transitions == 1)
            //                        {
            //                            int[] pixels = GetNeighborPixels(x, y, thinnedImage);

            //                            if (pixels[0] * pixels[2] * pixels[6] == 0 && pixels[0] * pixels[4] * pixels[6] == 0)
            //                            {
            //                                thinnedImage[x, y] = 0;
            //                                hasChanged = true;
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }

            //    return thinnedImage;
            //}
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
            public static Bitmap Point_corner(Bitmap Image_Original , int[,] Image_Egde)
            {
                Bitmap Image_Result = Image_Original;
                int Mid_Point_X = 0;
                int Mid_Point_Y = 0;
                int X = -80;
                int Y = -80;
                for(int i =0; i< Image_Egde.GetLength(0);i++)
                {
                    for(int j=0; j<Image_Egde.GetLength(1);j++)
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
                            // 
                            for(int x = i-5;x<i+5;x++)
                            {
                                if (x > 0 && x < Image_Egde.GetLength(0))
                                {
                                    for (int y = j - 5; y < j + 5; y++)
                                    {
                                       if (y > 0 && y < Image_Egde.GetLength(1))
                                       {
                                           Image_Result.SetPixel(x, y, Color.Red); 
                                       }
                                    }
                                }    
                            }
                        }                              
                         
                    }    
                }
                Mid_Point_X /= 4;
                Mid_Point_Y /= 4;
                MidPoint[0,0]= Mid_Point_X;
                MidPoint[0,1]= Mid_Point_Y;
                for (int x = Mid_Point_X - 5; x < Mid_Point_X + 5; x++)
                {
                    if (x > 0 && x < Image_Egde.GetLength(0))
                    {
                        for (int y = Mid_Point_Y - 5; y < Mid_Point_Y + 5; y++)
                        {
                            if (y > 0 && y < Image_Egde.GetLength(1))
                            {
                                Image_Result.SetPixel(x, y, Color.Red);
                            }
                        }
                    }
                }
                return Image_Result;
            }
        }
        class DetectShape
        {
            public double Moment_Calculate(int[,] Binary_image, int i, int j)
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

            public double CentralMoment1(int[,] Binary_image, int i, int j)
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
            public double CentralMoment2(int[,] Binary_image, int i, int j)
            {
                double U_ij = CentralMoment1(Binary_image, i, j);
                double U_00 = CentralMoment1(Binary_image, 0, 0);
                double N_ij = U_ij / Math.Pow(U_00, (i + j + 2) / 2);
                return N_ij;
            }
            public double[] HuMoment(int[,] Binary_image)
            {
                double[] H = new double[7];
                double N_20 = CentralMoment2(Binary_image, 2, 0);
                double N_02 = CentralMoment2(Binary_image, 0, 2);
                double N_11 = CentralMoment2(Binary_image, 1, 1);
                double N_30 = CentralMoment2(Binary_image, 3, 0);
                double N_12 = CentralMoment2(Binary_image, 1, 2);
                double N_03 = CentralMoment2(Binary_image, 0, 3);
                double N_21 = CentralMoment2(Binary_image, 2, 1);
                double U_03 = CentralMoment1(Binary_image, 0, 3);
                H[0] = N_20 + N_02;//
                H[1] = Math.Pow((N_20 - N_02), 2) + 4 * Math.Pow(N_11, 2);//
                H[2] = Math.Pow((N_30 - 3 * N_12), 2) + Math.Pow((3 * N_21 - N_03), 2);//
                H[3] = Math.Pow((N_30 + N_12), 2) + Math.Pow((N_21 + N_03), 2);//
                H[4] = (N_30 - 3 * N_12) * (N_30 + N_12) * (Math.Pow((N_30 + N_12), 2) - 3 * Math.Pow((N_21 + N_03), 2)) + (3 * N_21 - N_03) * (3 * Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2));
                H[5] = (N_20 - N_02) * (Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2) + 4 * N_11 * (N_30 + N_12) * (N_21 + N_03));
                H[6] = (3 * N_21 - N_03) * (N_30 + N_12) * (Math.Pow((N_30 + N_12), 2) - 3 * Math.Pow((N_21 + N_03), 2)) + (N_30 - 3 * N_12) * (N_21 + N_03) * (3 * Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2));

                for (int i = 0; i < 7; i++)
                {
                    H[i] = -1 * Math.Sign(H[i]) * Math.Log10(Math.Abs(H[i]));
                }

                return H;
            }
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
                Image image = Image.FromFile(imagePath);
                picture1.Image = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int high_threshold = 200;//ngưỡng trên cho canny detect
            int low_threshold = 60;//ngưỡng dưới cho canny detect 
            int threshold = 50;  // Ngưỡng để chọn các đỉnh trong ma trận Hough
                                 // int thre = Convert.ToInt16(text_thres.Text);
                                 //ảnh binary cho canny detect
            int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(imagePath, high_threshold, low_threshold);
            //DetectShape detectShape = new DetectShape();
            //double[] H = detectShape.HuMoment(edges);
            // Lấy số hàng và số cột của mảng
            // Khởi tạo một mảng 2 chiều
            int width = edges.GetLength(0);
            int height = edges.GetLength(1);

            //biểu đồ hough
            int[,] hough = EdgeDetection.PerformHoughTransform(edges);

            //vẽ đường thẳng từ biểu đồ hough
            int[,] result = EdgeDetection.DrawLines3(hough, threshold, width, height);

            // thể hiện lên GUI
            Bitmap Import_picture = new Bitmap(imagePath);
            picture1.Image = EdgeDetection.Point_corner(Import_picture, result);
            picture2.Image = EdgeDetection.IntToBitmap(result);
            picture3.Image = EdgeDetection.IntToBitmap(hough);
            picture4.Image = EdgeDetection.IntToBitmap(edges);
            int x = MidPoint[0,0];
            Mid_Point_X.Text = Convert.ToString(x);
            int y = MidPoint[0, 1];
            Mid_Point_Y.Text = Convert.ToString(y);
            angle1.Text = Convert.ToString(angle[0]);
            //angle2.Text = Convert.ToString(angle[1]);
            angle3.Text = Convert.ToString(angle[2]);
            //angle4.Text = Convert.ToString(angle[3]);

            
            //textBox1.Text = H[0].ToString();
            //textBox2.Text = H[1].ToString();
            //textBox3.Text = H[2].ToString();
            //textBox4.Text = H[3].ToString();
            //textBox5.Text = H[4].ToString();
            //textBox6.Text = H[5].ToString();
            //textBox7.Text = H[6].ToString();

            //// Dữ liệu hình chữ nhật,tròn, vuông, tam giác
            //double[,] H1 = new double[4, 7]
            //{
            //    { 3.12312876539246, 6.85381516209359, 9.88575150311941, 10.7838647822104, 15.6771151736907, -14.5406929254356, -21.1913516631291  } ,
            //    { 3.20469343954819, 10.346963490014, 10.2131978348198, 13.2563928897252, -18.6600719397016, 18.5286332390215, 24.991593246933 } ,
            //    { 3.18467661863633, 17.3395943883656, 9.90880841836875, 10.8155095284822, -21.1776685117439, -19.485306722665, 34.4426325862341},
            //    { 3.10917495216815, 7.45148166979752 , 3.07362704873088, 5.00704032046893, 6.31108372681061, -9.73140421171348, -9.36694031488709} ,
            //};
            //// tính  giá trị chênh lệch, chênh lệch thấp => tương thích cao.
            //double[] D = new double[4] { 0, 0, 0, 0 };
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 2; j++)
            //    {
            //        D[i] = D[i] + Math.Sqrt((H1[i, j] - H[j]) * (H1[i, j] - H[j]));
            //        //D[i] = D[i] + Math.Abs((H1[i, j] - H[j]));
            //    }
            //}
            //// tìm giá trị nhỏ nhất
            //double minValue = D[0]; // Giả sử giá trị đầu tiên là giá trị nhỏ nhất
            //int minIndex = 0; // Giả sử vị trí đầu tiên là vị trí của giá trị nhỏ nhất

            //for (int i = 1; i < D.Length; i++)
            //{
            //    if (D[i] < minValue)
            //    {
            //        minValue = D[i];
            //        minIndex = i;
            //    }
            //}
            //// cho kết quả
            //switch (minIndex)
            //{
            //    case 0:
            //        textBox8.Text = "HÌNH CHỮ NHẬT";
            //        break;
            //    case 1:
            //        textBox8.Text = "HÌNH TRÒN";
            //        break;
            //    case 2:
            //        textBox8.Text = "HÌNH VUÔNG";
            //        break;
            //    case 3:
            //        textBox8.Text = "HÌNH TAM GIÁC";
            //        break;
            //}
        }

        private void Mid_Point_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


    }
}
