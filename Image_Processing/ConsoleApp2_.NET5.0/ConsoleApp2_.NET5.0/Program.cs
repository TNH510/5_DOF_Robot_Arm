using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

//int[,] gradient;
internal class Program
{
    static void Main()
    {
        // Khởi tạo đối tượng Stopwatch
        Stopwatch stopwatch = new Stopwatch();
        // Bắt đầu đo thời gian
        stopwatch.Start();
        //// Đường dẫn đến file ảnh BMP
        //string bmpPath = @"C:\Users\Loc\Desktop\XLA_10-_2_.bmp";
        //string output = @"C:\Users\Loc\Desktop\ouput.bmp";
        //int high_threshold = 200;//ngưỡng trên cho canny detect
        //int low_threshold = 40;//ngưỡng dưới cho canny detect 
        //int threshold = 50;  // Ngưỡng để chọn các đỉnh trong ma trận Hough
        //                     // int thre = Convert.ToInt16(text_thres.Text);
        //                     //ảnh binary cho canny detect
        //int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(bmpPath, high_threshold, low_threshold);

        //// Lấy số hàng và số cột của mảng
        //// Khởi tạo một mảng 2 chiều
        //int width = edges.GetLength(0);
        //int height = edges.GetLength(1);

        ////mở rộng các cạnh để triệt tiêu bớt nhiễu    
        ////int[,] dilation = EdgeDetection.Dilation(edges, 6);

        ////đưa độ dài cạnh về 1 để giảm dung lượng cho biểu đồ hough
        ////int[,] skeleton = EdgeDetection.EdgeThinning(dilation);

        ////biểu đồ hough
        //int[,] hough = EdgeDetection.PerformHoughTransform(edges);

        ////vẽ đường thẳng từ biểu đồ hough
        //int[,] result_2D = EdgeDetection.DrawLines2(hough, threshold, width, height);
        ////Bitmap result_3D = EdgeDetection.IntToBitmap(result_2D);
        ////result_3D.Save(output);
        //// Dừng đồng hồ đo thời gian
        stopwatch.Stop();

        // Lấy thời gian đã trôi qua
        TimeSpan elapsedTime = stopwatch.Elapsed;

        // In ra thời gian đã trôi qua
        Console.WriteLine("Thời gian thực thi: " + elapsedTime);
        Console.WriteLine("Chuyển đổi thành công.");
    }
}

class EdgeDetection
{
    static int[,] gradient;
    //public static int[,] RGB2Gray(Bitmap ColorImage)
    //{

    //    int width = ColorImage.Width;
    //    int height = ColorImage.Height;
    //    int[,] GrayImage = new int[width, height];

    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            Color pixelColor = ColorImage.GetPixel(x, y);
    //            int GrayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
    //            GrayImage[x, y] = GrayValue;
    //        }
    //    }

    //    return GrayImage;
    //}
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
    //public static Bitmap IntToBitmap(int[,] Binary_Image)
    //{
    //    // Kích thước hình ảnh
    //    int width = Binary_Image.GetLength(0);
    //    int height = Binary_Image.GetLength(1);

    //    // Tạo bitmap từ mảng hai chiều
    //    Bitmap Result = new Bitmap(width, height, PixelFormat.Format16bppRgb565);
    //    for (int x = 0; x < width; x++)
    //    {
    //        for (int y = 0; y < height; y++)
    //        {
    //            Color color;
    //            int pixelValue = (int)Binary_Image[x, y];
    //            //if (pixelValue == 10)
    //            //{
    //            //    color = Color.FromArgb(255, 0, 0);
    //            //}  
    //            //else
    //            //{
    //            //    color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
    //            //}    
    //            color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
    //            Result.SetPixel(x, y, color);
    //        }
    //    }
    //    return Result;
    //}
    //public static int[,] BitmapToInt(Bitmap bitmap)
    //{
    //    int width = bitmap.Width;
    //    int height = bitmap.Height;

    //    int[,] imageArray = new int[width, height];

    //    for (int i = 0; i < width; i++)
    //    {
    //        for (int j = 0; j < height; j++)
    //        {
    //            Color pixelColor = bitmap.GetPixel(i, j);
    //            int grayscaleValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
    //            imageArray[i, j] = grayscaleValue;
    //        }
    //    }

    //    return imageArray;
    //}
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
    public static int[,] DrawLines(int[,] houghMatrix, int threshold, int width, int height)
    {
        int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

        int[,] resultImage = new int[width, height];
        int count = 0;
        int[,] theta_rho = new int[1000, 3];

        for (int theta = 0; theta < 180; theta++)
        {
            for (int rho = 0; rho < 2 * diagonal; rho++)
            {
                if (houghMatrix[theta, rho] >= threshold)
                {
                    // lấy toàn bộ số điểm vượt ngưỡng 
                    theta_rho[count, 0] = theta;
                    theta_rho[count, 1] = rho;
                    theta_rho[count, 2] = 0;
                    count++;
                }
            }
        }

        //phân loại đường thẳng                
        int nhom = 4;
        int[,,] ketqua = new int[count, 2, nhom];

        for (int i = 0; i < nhom; i++)
        {
            int index = 0;

            //lấy số chuẩn
            for (int j = 0; j < count; j++)
            {
                if (theta_rho[j, 2] == 0)
                {
                    ketqua[index, 0, i] = theta_rho[j, 0];
                    ketqua[index, 1, i] = theta_rho[j, 1];
                    theta_rho[j, 2] = 1;
                    index++;
                    j = count;//thoát vòng lặp

                }
            }
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

            //tìm đường trung bình trong các nhóm
            int avr_theta1 = 0;
            int avr_rho1 = 0;
            if (index != 0)
            {
                for (int j = 0; j < index; j++)
                {
                    avr_theta1 = (avr_theta1 + ketqua[j, 0, i]);
                    avr_rho1 = (avr_rho1 + ketqua[j, 1, i]);
                }
                int avr_theta = avr_theta1 / (index);
                int avr_rho = avr_rho1 / (index);
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

        }
        return resultImage;
    }
    public static int[,] DrawLines2(int[,] houghMatrix, int threshold, int width, int height)
    {
        int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

        int[,] resultImage = new int[width, height];
        int count = 0;
        int[,] theta_rho = new int[1000, 4];
        // lấy toàn bộ số điểm vượt ngưỡng 
        for (int theta = 0; theta < 180; theta++)
        {
            for (int rho = 0; rho < 2 * diagonal; rho++)
            {
                if (houghMatrix[theta, rho] >= threshold)
                {
                    theta_rho[count, 0] = theta;
                    theta_rho[count, 1] = rho;
                    theta_rho[count, 2] = 0;
                    theta_rho[count, 3] = houghMatrix[theta, rho];
                    count++;
                }
            }
        }
        //phân loại đường thẳng                
        int nhom = 6;
        int[,,] ketqua = new int[count, 2, nhom];
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
            ketqua[index, 0, i] = theta_rho[max_index, 0];
            ketqua[index, 1, i] = theta_rho[max_index, 1];
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

            //tìm đường trung bình trong các nhóm
            int avr_theta1 = 0;
            int avr_rho1 = 0;
            if (index != 0)
            {
                for (int j = 0; j < index; j++)
                {
                    avr_theta1 = (avr_theta1 + ketqua[j, 0, i]);
                    avr_rho1 = (avr_rho1 + ketqua[j, 1, i]);
                }
                line[i, 0] = avr_theta1 / (index);
                line[i, 1] = avr_rho1 / (index);
            }
            if (soluong_nhom >= 4)
            {
                i = nhom;
            }

        }
        for (int i = 0; i < 4; i++)
        {
            int avr_theta = line[i, 0];
            int avr_rho = line[i, 1];
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
    public static int[,] FindCorner(int[,] line)
    {
        int[,] PointMap = new int[line.GetLength(0), line.GetLength(1)];
        int distance = 0;
        int max_distance = 0;
        int Xmax = 0;
        int Ymax = 0;
        int Xmin = 0;
        int Ymin = 0;
        //tìm ra 2 điểm của 1 đường chéo 
        for (int i = 0; i < line.GetLength(0); i++)
        {
            for (int j = 0; j < line.GetLength(1); j++)
            {
                if (line[i, j] == 10)
                {
                    Xmin = i;
                    Ymin = j;
                    PointMap[Xmin, Ymin] = 150;

                    for (int a = i; a < line.GetLength(0); a++)
                    {
                        for (int b = j; b < line.GetLength(1); b++)
                        {
                            if (line[a, b] == 10)
                            {
                                distance = (int)Math.Sqrt((i - a) * (i - a) + (j - b) * (j - b));
                                if (distance > max_distance)
                                {
                                    max_distance = distance;
                                    Xmax = a;
                                    Ymax = b;
                                }
                            }
                        }
                    }
                    PointMap[Xmax, Ymax] = 150;
                }
            }
        }
        //Ax + By + C =0
        int A = Ymax - Ymin;
        int B = Xmax - Xmin;

        return PointMap;
    }
    // Hàm dilation với số lần lặp lại xác định
    public static int[,] Dilation(int[,] image, int iterations)
    {
        int width = image.GetLength(0);
        int height = image.GetLength(1);
        int[,] dilatedImage = new int[width, height];

        for (int iter = 0; iter < iterations; iter++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int maxValue = 0;
                    for (int ky = -1; ky <= 1; ky++)
                    {
                        for (int kx = -1; kx <= 1; kx++)
                        {
                            int nx = x + kx;
                            int ny = y + ky;
                            if (nx >= 0 && nx < width && ny >= 0 && ny < height)
                            {
                                maxValue = Math.Max(maxValue, image[nx, ny]);
                            }
                        }
                    }
                    dilatedImage[x, y] = maxValue;
                }
            }
            // Update ảnh đầu vào bằng ảnh đã dilation để tiếp tục lặp lại
            image = dilatedImage.Clone() as int[,];
        }

        return dilatedImage;
    }
    public static int[,] EdgeThinning(int[,] image)
    {
        int width = image.GetLength(0);
        int height = image.GetLength(1);
        int[,] thinnedImage = (int[,])image.Clone(); // Tạo một bản sao của ảnh để thực hiện mỏng cạnh viền

        bool hasChanged = true; // Đặt biến hasChanged là true để bắt đầu vòng lặp

        while (hasChanged)
        {
            hasChanged = false; // Đặt lại hasChanged là false trước khi bắt đầu mỗi vòng lặp

            // Bước 1: Xác định và loại bỏ các điểm ảnh để làm mỏng cạnh viền
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (thinnedImage[x, y] == 255) // Nếu điểm ảnh đang xét là một điểm cạnh
                    {
                        int count = CountNeighbors(x, y, thinnedImage);

                        if (count >= 2 && count <= 6) // Nếu số lượng điểm lân cận thỏa mãn
                        {
                            int transitions = Transitions(x, y, thinnedImage);

                            if (transitions == 1) // Nếu chỉ có một điểm chuyển đổi
                            {
                                int[] pixels = GetNeighborPixels(x, y, thinnedImage);

                                if (pixels[0] * pixels[2] * pixels[4] == 0 && pixels[2] * pixels[4] * pixels[6] == 0) // Kiểm tra điều kiện đặc biệt
                                {
                                    thinnedImage[x, y] = 0;
                                    hasChanged = true;
                                }
                            }
                        }
                    }
                }
            }

            // Bước 2: Lặp lại bước 1 với các điểm ảnh đang xét được đảo ngược
            for (int y = 1; y < height - 1; y++)
            {
                for (int x = 1; x < width - 1; x++)
                {
                    if (thinnedImage[x, y] == 255)
                    {
                        int count = CountNeighbors(x, y, thinnedImage);

                        if (count >= 2 && count <= 6)
                        {
                            int transitions = Transitions(x, y, thinnedImage);

                            if (transitions == 1)
                            {
                                int[] pixels = GetNeighborPixels(x, y, thinnedImage);

                                if (pixels[0] * pixels[2] * pixels[6] == 0 && pixels[0] * pixels[4] * pixels[6] == 0)
                                {
                                    thinnedImage[x, y] = 0;
                                    hasChanged = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        return thinnedImage;
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

        for (int y = 0; y < image.GetLength(0); y++)
        {
            for (int x = 0; x < image.GetLength(1); x++)
            {
                //int pixel = image[x, y];
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
}