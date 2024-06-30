using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Lokdeptrai
{
    public class Image_Processing
    {
        static int[,] gradient;
        public static int[,] MidPoint = new int[1, 2];
        public static int[] angle = new int[4];
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

        private static int[,] Erosion(int[,] image)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            int[,] result = new int[width, height];
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
        public static int[,] Dilation(int[,] image)
        {
            int width = image.GetLength(0);
            int height = image.GetLength(1);
            int[,] result = new int[width, height];
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
        public static int[,] Erosion_Dilation(int[,] image, int iterations_Erosion, int iterations_Dilation)
        {
            int[,] result = image;
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
        public static int[,] DeTectEdgeByCannyMethod(int[,] gray, int high_threshold, int low_threshold, string thres)
        {
            gray = Blur_Image(gray);
            //int[,] blur = gray;
            //Bitmap img_blur = IntToBitmap(blur);
            int[] threhold = new int[2];
            threhold = CalculateTwoThresholds(gray);
            for (int i = 0; i < gray.GetLength(0); i++)
            {
                for (int j = 0; j < gray.GetLength(1); j++)
                {
                    if ((gray[i, j] >= threhold[0]))
                    {
                        gray[i, j] = 255;
                    }
                    //else if ((gray[i, j] >= threhold[1] && gray[i, j] <= 255))
                    //{
                    //    gray[i, j] = 255;
                    //}                        
                    else
                    {
                        gray[i, j] = 0;
                    }
                }
            }
            //loại bỏ n

            //gray = MedianBlur(gray);
            gray = RemoveSmallWhiteRegions(gray);
            gray = Erosion_Dilation(gray, 7, 7);

            //string a =  imagePath + "_1.jpg";
            //Bitmap SaveImage = IntToBitmap(gray);
            //SaveImage.Save(a);
            int[,] edges = Canny_Detect(gray, high_threshold, low_threshold);

            return edges;
        }

        public static int[,] PerformHoughTransform_Rectangle(int[,] image)
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
        public static int CalculateThreshold(int[,] image)
        {
            // Tính histogram
            int[] histogram = new int[256];
            int totalPixels = 0;

            for (int x = 0; x < image.GetLength(0); x++)
            {
                for (int y = 0; y < image.GetLength(1); y++)
                {

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
        public static int[] CalculateTwoThresholds(int[,] grayImage)
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
        public static int[,] Find_line_info(int[,] hough, out int count_hough_point)
        {
            //tạo ra 1 biến 3D để thêm thông tin là điểm ảnh đã được duyệt qua chưa
            //đếm xem có bao nhiêu điểm >=55
            count_hough_point = 0;
            int threshold = 80;
            for (int i = 0; i < hough.GetLength(0); i++)
            {
                for (int j = 0; j < hough.GetLength(1); j++)
                {
                    if (hough[i, j] >= threshold)
                    {
                        count_hough_point++;
                    }
                }
            }
            //lưu thông tin của ma trận hough có điểm >=55 vào Temp_info
            int[,] Temp_info = new int[count_hough_point, 5];
            int count1 = 0;
            for (int i = 0; i < hough.GetLength(0); i++)
            {
                for (int j = 0; j < hough.GetLength(1); j++)
                {
                    if (hough[i, j] >= threshold)
                    {
                        Temp_info[count1, 0] = i;//x
                        Temp_info[count1, 1] = j;//y
                        Temp_info[count1, 2] = 0;//nhom
                        Temp_info[count1, 3] = 0;//check xem da phan loai hay chua
                        Temp_info[count1, 4] = hough[i, j];//giá trị nổi bật
                        count1++;
                    }

                }
            }
            //phân nhóm
            //bốc tk đầu tiên
            int x = 0;
            int y = 0;
            int nhom = 0;
            int count = 0;
            while (count < count_hough_point)
            {
                int distance = 0;
                for (int i = 0; i < count_hough_point; i++)
                {
                    if (Temp_info[i, 3] == 0)//check xem da phan loai hay chua
                    {
                        x = Temp_info[i, 0];
                        y = Temp_info[i, 1];
                        distance = Math.Abs(y - 800);
                        break;
                    }
                }
                //tính toán khoản cách để phâm nhóm 
                for (int i = 0; i < count_hough_point; i++)
                {
                    if (Temp_info[i, 3] == 0)//check xem da phan loai hay chua
                    {
                        if ((((Math.Abs(x - Temp_info[i, 0]) < 25) && (Math.Abs(y - Temp_info[i, 1]) < 120))) || ((Math.Abs(x - Temp_info[i, 0]) > 170) && (Math.Abs(Math.Abs(Temp_info[i, 1] - 800) - distance) < 120)))
                        {
                            Temp_info[i, 2] = nhom;//nhom
                            Temp_info[i, 3] = 1;//da check
                            count++;
                        }
                    }
                }
                nhom++;
            }
            // tìm ra giá trị vote cao nhất ở mỗi nhóm
            int max_value;
            int[,] Temp_Result = new int[nhom, 2];//x,y
            for (int i = 0; i < nhom; i++)
            {
                //tìm ra giá trị lớn nhất
                max_value = 0;
                int theta = 0;
                for (int j = 0; j < count_hough_point; j++)
                {
                    if (Temp_info[j, 4] > max_value && Temp_info[j, 2] == i)
                    {
                        max_value = Temp_info[j, 4];
                        theta = Temp_info[j, 0];
                    }
                }
                //tìm những đường thẳng có giá trị bằng với max_value
                int avr_x = 0;
                int avr_y = 0;
                int count2 = 0;
                for (int j = 0; j < count_hough_point; j++)
                {
                    if (Temp_info[j, 4] == max_value && Temp_info[j, 2] == i && Math.Abs(theta - Temp_info[j, 0]) < 10)
                    {
                        avr_x += Temp_info[j, 0];
                        avr_y += Temp_info[j, 1];
                        count2++;
                    }
                }
                Temp_Result[i, 0] = avr_x / count2;
                Temp_Result[i, 1] = avr_y / count2;
            }
            //Lọc ra các đường dùng được
            int number_lines = Temp_Result.GetLength(0);

            //int count3 = 0;//đếm số lượng đường có alpha < 5 độ
            //for (int i =0; i<number_lines;i++)
            //{
            //    if(Temp_Result[i, 0]<3)
            //    {
            //        count3++;
            //    }    
            //}

            int[,] Final_Result = new int[number_lines, 2];
            //int count4 = 0;
            for (int i = 0; i < number_lines; i++)
            {
                //if (Temp_Result[i, 0] < 3)
                //{
                //    //bỏ qua những điểm có alpha < 5 độ
                //}
                //else
                //{
                Final_Result[i, 0] = Temp_Result[i, 0];
                Final_Result[i, 1] = Temp_Result[i, 1];
                //count4++;
                //}
            }
            return Final_Result;
        }
        public static int[,] Find_corner_info(int[,] Lines)
        {// tìm ra tọa độ của các điểm góc
            int[,] corner = new int[Lines.GetLength(0) + 1, 2];
            int count = 0;
            int center_X = 0;
            int center_Y = 0;
            if (Lines.GetLength(0) == 4)
            {
                int diagonal = 800;// (int)Math.Sqrt(width * width + height * height), width=480, height=640
                double[] Radian_Theta = new double[2];
                Radian_Theta[0] = Lines[0, 0] * Math.PI / 180;
                int[] rho = new int[2];
                rho[0] = Lines[0, 1];
                int indexx = 0;
                for (int i = 1; i < 4; i++)
                {
                    if (Math.Abs(Lines[0, 0] - Lines[i, 0]) < 5)
                    {
                        Radian_Theta[1] = Lines[i, 0] * Math.PI / 180;
                        rho[1] = Lines[i, 1];
                        indexx = i;
                    }
                }
                for (int i = 0; i < 2; i++)
                {

                    for (int j = 0; j < 4; j++)
                    {


                        if (j != 0 && j != indexx)//
                        {
                            double Radian_Theta1 = Lines[j, 0] * Math.PI / 180;
                            int rho1 = Lines[j, 1];
                            //giải phương trình
                            //int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
                            double tu = ((rho[i] - diagonal) * Math.Sin(Radian_Theta1)) - ((rho1 - diagonal) * Math.Sin(Radian_Theta[i]));
                            double mau = Math.Cos(Radian_Theta[i]) * Math.Sin(Radian_Theta1) - Math.Cos(Radian_Theta1) * Math.Sin(Radian_Theta[i]);
                            int x = (int)((tu / mau));
                            int y = (int)(((rho1 - diagonal) - x * Math.Cos(Radian_Theta1)) / Math.Sin(Radian_Theta1));
                            corner[count, 0] = x;
                            center_X += x;

                            corner[count, 1] = y;
                            center_Y += y;
                            count++;
                        }
                    }
                }
                corner[4, 0] = center_X / count;
                corner[4, 1] = center_Y / count;
            }
            else if (Lines.GetLength(0) == 3)// tam giác
            {
                for (int i = 0; i < 3; i++)
                {
                    int diagonal = 800;// (int)Math.Sqrt(width * width + height * height), width=480, height=640
                    double Radian_Theta = Lines[i, 0] * Math.PI / 180;
                    int rho = Lines[i, 1];
                    double Radian_Theta1;
                    int rho1;
                    if (i == 2)
                    {
                        Radian_Theta1 = Lines[0, 0] * Math.PI / 180;
                        rho1 = Lines[0, 1];
                    }
                    else
                    {
                        Radian_Theta1 = Lines[i + 1, 0] * Math.PI / 180;
                        rho1 = Lines[i + 1, 1];
                    }

                    //giải phương trình
                    //int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
                    double tu = ((rho - diagonal) * Math.Sin(Radian_Theta1)) - ((rho1 - diagonal) * Math.Sin(Radian_Theta));
                    double mau = Math.Cos(Radian_Theta) * Math.Sin(Radian_Theta1) - Math.Cos(Radian_Theta1) * Math.Sin(Radian_Theta);
                    int x = (int)((tu / mau));
                    int y = (int)(((rho1 - diagonal) - x * Math.Cos(Radian_Theta1)) / Math.Sin(Radian_Theta1));
                    corner[count, 0] = x;
                    center_X += x;
                    corner[count, 1] = y;
                    center_Y += y;
                    count++;

                }
                corner[3, 0] = center_X / count;
                corner[3, 1] = center_Y / count;
            }
            return corner;
        }
        public static (double length, double width, double angle) GetRectangleDimensions(double[,] points)
        {
            // Calculate distances between all pairs of points
            double[,] distances = new double[4, 4];
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    distances[i, j] = Math.Sqrt(Math.Pow(points[i, 0] - points[j, 0], 2) + Math.Pow(points[i, 1] - points[j, 1], 2));
                    distances[j, i] = distances[i, j];
                }
            }

            // Find the maximum distance which is the diagonal of the rectangle
            double maxDistance = 0;
            int p1 = 0, p2 = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = i + 1; j < 4; j++)
                {
                    if (distances[i, j] > maxDistance)
                    {
                        maxDistance = distances[i, j];
                        p1 = i;
                        p2 = j;
                    }
                }
            }

            // The points opposite to p1 and p2
            int p3 = -1, p4 = -1;
            for (int i = 0; i < 4; i++)
            {
                if (i != p1 && i != p2)
                {
                    if (p3 == -1)
                        p3 = i;
                    else
                        p4 = i;
                }
            }

            // Determine the shorter sides
            double side1 = distances[p1, p3];
            double side2 = distances[p1, p4];

            double length, width, dx, dy;

            if (side1 > side2)
            {
                length = side1;
                width = side2;
                dx = points[p1, 0] - points[p3, 0];
                dy = points[p1, 1] - points[p3, 1];
            }
            else
            {
                length = side2;
                width = side1;
                dx = points[p1, 0] - points[p4, 0];
                dy = points[p1, 1] - points[p4, 1];
            }

            // Ensure we are calculating the angle for the longer side
            if (length < width)
            {
                double temp = length;
                length = width;
                width = temp;
                dx = points[p1, 0] - points[p4, 0];
                dy = points[p1, 1] - points[p4, 1];
            }

            // Calculate angle with Ox
            double angle = Math.Atan2(dy, dx) * (180.0 / Math.PI);

            return (length, width, angle);
        }
        static double CalculateDistance(double x1, double y1, double x2, double y2)
        {
            double deltaX = x2 - x1;
            double deltaY = y2 - y1;
            return Math.Sqrt(deltaX * deltaX + deltaY * deltaY);
        }
        public static (double[] length, double[] angle) GetTriangleDimensions(double[,] points)
        {
            double[] length = new double[3];
            double[] angle = new double[3];
            double[,] Temp_point = new double[3, 2];
            for (int i = 0; i < 3; i++)
            {
                Temp_point[i, 0] = points[i, 0];
                Temp_point[i, 1] = points[i, 1];
            }
            //tính khoảng cách
            for (int i = 0; i < 3; i++)
            {
                if (i == 2)
                {
                    length[i] = CalculateDistance(Temp_point[2, 0], Temp_point[2, 1], Temp_point[0, 0], Temp_point[0, 1]);
                    angle[i] = Math.Atan2(Temp_point[0, 1] - Temp_point[2, 1], Temp_point[0, 0] - Temp_point[2, 0]) * (180.0 / Math.PI);
                }
                else
                {
                    length[i] = CalculateDistance(Temp_point[i, 0], Temp_point[i, 1], Temp_point[i + 1, 0], Temp_point[i + 1, 1]);
                    angle[i] = Math.Atan2(Temp_point[i + 1, 1] - Temp_point[i, 1], Temp_point[i + 1, 0] - Temp_point[i, 0]) * (180.0 / Math.PI);
                }

            }
            return (length, angle);
        }

        public static (string shape, int[,] Center, int Radius) Circle(int[,] edges)
        {
            int width = edges.GetLength(0);
            int height = edges.GetLength(1);
            int[,] center = { { 0, 0 } };
            int count = 0;
            int Radius = 0;
            string shape = "Unknown";
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (edges[i, j] == 255)
                    {
                        center[0, 0] += i;
                        center[0, 1] += j;
                        count++;
                    }

                }
            }
            center[0, 0] /= count;//x
            center[0, 1] /= count;//y
                                  //min radius
            int min_radius = 1000;
            double[] radius = new double[count + 1];
            int count1 = 0;
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (edges[i, j] == 255)
                    {
                        radius[count1] = CalculateDistance(center[0, 0], center[0, 1], i, j);

                        if (radius[count1] < min_radius)
                        {
                            min_radius = (int)radius[count1];
                        }
                        count1++;
                    }
                }
            }
            //
            int count2 = 0;
            for (int i = 0; i < count1; i++)
            {
                if (radius[i] - min_radius < 10)/////////////////
                {
                    count2++;
                }
            }
            if (count2 > 0.8 * count)
            {
                Radius = min_radius + 3;
                shape = "Circle";
            }
            else
            {
                Radius = 0;
                shape = "Unknown";
            }
            return (shape, center, Radius);
        }
        public static void Detect_Shape_dimention(int[,] edges, int[,] line_info, int[,] point_info, out string shape, out int[,] dimention, out int[,] Center_Point)
        {
            //từ line_info và point_info 
            int number_edges = line_info.GetLength(0);
            int number_point = point_info.GetLength(0);
            Center_Point = new int[1, 2];
            Center_Point[0, 0] = point_info[number_point - 1, 0];
            Center_Point[0, 1] = point_info[number_point - 1, 1];
            shape = "Unknown";
            dimention = new int[3, 2];

            if (number_edges == 4 && number_point == 5)
            {

                //tính toán kích thước các cạnh
                //tìm điểm gần gốc tọa độ nhất
                double[,] Temp_point = new double[4, 2];
                for (int i = 0; i < number_point - 1; i++)
                {
                    Temp_point[i, 0] = point_info[i, 0];
                    Temp_point[i, 1] = point_info[i, 1];
                }
                (double length, double width, double angle) = GetRectangleDimensions(Temp_point);
                if (angle < 0)
                {
                    angle = angle + 180;
                }
                if (length - width > 10)
                {
                    //dimention = canh
                    shape = "Rectangle";
                    dimention[0, 0] = (int)length;//canh dai
                    dimention[0, 1] = (int)angle;//canh goc canh dai - ox
                    dimention[1, 0] = (int)width;//canh ngan
                                                 //dimention[1, 1] = D2_angle;
                }
                //else if (D1 - D2 < -20)
                //{
                //    shape = "Rectangle";
                //    dimention[0, 0] = D2;
                //    dimention[0, 1] = D2_angle;
                //    dimention[1, 0] = D1;
                //    dimention[1, 1] = D1_angle;
                //}
                else if (length - width > -15 && length - width < 15)
                {

                    shape = "Square";
                    dimention[0, 0] = (int)length;//canh 
                    dimention[0, 1] = (int)angle;// goc canh với Ox


                }

            }
            else if (number_edges == 3 && number_point == 4)
            {
                double[,] Temp_point = new double[3, 2];
                for (int i = 0; i < number_point - 1; i++)
                {
                    Temp_point[i, 0] = point_info[i, 0];
                    Temp_point[i, 1] = point_info[i, 1];
                }
                shape = "Triangle";
                (double[] length, double[] angle) = GetTriangleDimensions(Temp_point);
                double max_value = length[0];
                int index = 0;
                for (int i = 1; i < 3; i++)
                {
                    if (length[i] > max_value)
                    {
                        max_value = length[i];
                        index = i;
                    }
                }
                dimention[0, 0] = (int)max_value;//canh day
                if (angle[index] < 0)
                {
                    angle[index] = angle[index] + 180;
                }
                dimention[0, 1] = (int)angle[index]; //goc cua Ox voi canh day

            }
            else if (number_edges <= 2 && number_point <= 2)
            {
                (shape, Center_Point, dimention[0, 0]) = Circle(edges);//canh day

            }
            else shape = "unknown";



        }

        //// tạo ảnh chứa kết quả
        ////int[,]Pointed_image= new int[image.GetLength(0), image.GetLength(1)];
        //// ảnh cạnh được phân bởi phương pháp canny
        //int[,] edges = DeTectEdgeByCannyMethod(image, 50, 200);
        ////biểu đồ hough để tìm ra đường thẳng
        //int[,] hough = PerformHoughTransform(edges);
        ////tìm ra các phương trình đường thẳng từ biểu đồ hough
        //int[,] lines = Find_line_info(hough, out int count_hough_point);
        //// tìm điểm giao của các phương trình.
        //int[,] corner = Find_corner_info(lines);
        //Detect_Shape_dimention(int[,] edges, int[,] lines, int[,] corner, out string shape, out int[,] dimention, out int[,] Center_Point)



        private static int width;
        private static int height;
        // Hàm để kiểm tra xem tọa độ có nằm trong ảnh không
        private static bool IsValid(int x, int y)
        {
            return x >= 0 && x < width && y >= 0 && y < height;
        }
        // Hàm để gán nhãn các vùng liên thông bằng thuật toán BFS
        private static void LabelComponent(int[,] image, int[,] labels, int startX, int startY, int label)
        {
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };
            Queue<(int, int)> queue = new Queue<(int, int)>();
            queue.Enqueue((startX, startY));
            labels[startX, startY] = label;

            while (queue.Count > 0)
            {
                var (x, y) = queue.Dequeue();

                for (int d = 0; d < 4; d++)
                {
                    int newX = x + dx[d];
                    int newY = y + dy[d];

                    if (IsValid(newX, newY) && image[newX, newY] == 255 && labels[newX, newY] == 0)
                    {
                        labels[newX, newY] = label;
                        queue.Enqueue((newX, newY));
                    }
                }
            }
        }
        public static int[,] RemoveSmallWhiteRegions(int[,] image)
        {
            width = image.GetLength(0);
            height = image.GetLength(1);

            int[,] labels = new int[width, height];
            int label = 1;
            Dictionary<int, int> labelSizes = new Dictionary<int, int>();

            // Gán nhãn cho các vùng liên thông
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (image[x, y] == 255 && labels[x, y] == 0)
                    {
                        LabelComponent(image, labels, x, y, label);
                        label++;
                    }
                }
            }

            // Tính diện tích của mỗi vùng
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (labels[x, y] != 0)
                    {
                        if (!labelSizes.ContainsKey(labels[x, y]))
                        {
                            labelSizes[labels[x, y]] = 0;
                        }
                        labelSizes[labels[x, y]]++;
                    }
                }
            }

            // Tìm nhãn của vùng lớn nhất
            int maxLabel = -1;
            int maxSize = -1;

            foreach (var kvp in labelSizes)
            {
                if (kvp.Value > maxSize)
                {
                    maxSize = kvp.Value;
                    maxLabel = kvp.Key;
                }
            }

            // Tạo ảnh kết quả chỉ giữ lại vùng lớn nhất
            int[,] result = new int[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    if (labels[x, y] == maxLabel)
                    {
                        result[x, y] = 255;
                    }
                    else
                    {
                        result[x, y] = 0;
                    }
                }
            }

            return result;
        }

    }
}
