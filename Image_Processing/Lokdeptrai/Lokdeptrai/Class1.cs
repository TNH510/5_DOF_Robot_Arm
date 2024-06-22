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
        public static int[,] Erosion(int[,] image)
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

            return Result;
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
        public static int[,] DeTectEdgeByCannyMethod(int[,] gray, int high_threshold, int low_threshold)
        {
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
                     
                    else
                    {
                        gray[i, j] = 0;
                    }
                }
            }

            gray = Erosion_Dilation(gray, 3, 3);

            int[,] edges = Canny_Detect(gray, high_threshold, low_threshold);

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
        public static int[,] Find_line_info1(int[,] hough)
        {
            //tạo ra 1 biến 3D để thêm thông tin là điểm ảnh đã được duyệt qua chưa
            //đếm xem có bao nhiêu điểm >=60
            int count_hough_point = 0;

            for (int i = 0; i < hough.GetLength(0); i++)
            {
                for (int j = 0; j < hough.GetLength(1); j++)
                {
                    if (hough[i, j] >= 60)
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
                    if (hough[i, j] >= 60)
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
                        if ((((Math.Abs(x - Temp_info[i, 0]) < 20) && Math.Abs(y - Temp_info[i, 1]) < 90)) || ((Math.Abs(x - Temp_info[i, 0]) > 170) && (Math.Abs(Math.Abs(Temp_info[i, 1] - 800) - distance) < 80)))
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
            int[,] Final_Result = new int[number_lines, 2];
            //int count4 = 0;
            for (int i = 0; i < number_lines; i++)
            {
                Final_Result[i, 0] = Temp_Result[i, 0];
                Final_Result[i, 1] = Temp_Result[i, 1];
            }
            return Final_Result;
        }
        public static int[,] Find_corner_info(int[,] Lines)
        {// tìm ra tọa độ của các điểm góc
            int[,] corner = new int[Lines.GetLength(0) + 1, 2];
            int count = 0;
            if (Lines.GetLength(0) == 4)
            {
                int center_X = 0;
                int center_Y = 0;
                for (int i = 0; i < 2; i++)
                {
                    int diagonal = 800;// (int)Math.Sqrt(width * width + height * height), width=480, height=640
                    double Radian_Theta = Lines[i, 0] * Math.PI / 180;
                    int rho = Lines[i, 1];

                    for (int j = 2; j < 4; j++)
                    {
                        double Radian_Theta1 = Lines[j, 0] * Math.PI / 180;
                        int rho1 = Lines[j, 1];

                        if ((Math.Abs(rho - rho1) > 10) && (i != j))//
                        {
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

                    if ((Math.Abs(rho - rho1) > 10))//
                    {
                        //giải phương trình
                        //int y = (int)(((avr_rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
                        double tu = ((rho - diagonal) * Math.Sin(Radian_Theta1)) - ((rho1 - diagonal) * Math.Sin(Radian_Theta));
                        double mau = Math.Cos(Radian_Theta) * Math.Sin(Radian_Theta1) - Math.Cos(Radian_Theta1) * Math.Sin(Radian_Theta);
                        int x = (int)((tu / mau));
                        int y = (int)(((rho1 - diagonal) - x * Math.Cos(Radian_Theta1)) / Math.Sin(Radian_Theta1));
                        corner[count, 0] = x;
                        corner[count, 1] = y;
                        count++;
                    }

                }
            }
            return corner;
        }
        public static int[,] Point_corner(int[,]image)
        {
            // tạo ảnh chứa kết quả
            //int[,]Pointed_image= new int[image.GetLength(0), image.GetLength(1)];
            // ảnh cạnh được phân bởi phương pháp canny
            int[,] edges = DeTectEdgeByCannyMethod(image, 50, 200);
            //biểu đồ hough để tìm ra đường thẳng
            int[,] hough = PerformHoughTransform(edges);
            //tìm ra các phương trình đường thẳng từ biểu đồ hough
            int[,] lines = Find_line_info1(hough);
            // tìm điểm giao của các phương trình.
            int[,] corner = Find_corner_info(lines);

            return corner;
        }
    }
}
