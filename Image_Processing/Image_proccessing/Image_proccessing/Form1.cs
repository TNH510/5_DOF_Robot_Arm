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
            public static int[,] Thresh_image(string imagePath)
            {
                Bitmap bitmap = new Bitmap(imagePath);
                int[,] thresh_image = EdgeDetection.BitmapToInt(bitmap);
                for (int i = 0; i < thresh_image.GetLength(0); i++)
                {
                    for (int j = 0; j < thresh_image.GetLength(1); j++)
                    {
                        if (thresh_image[i, j] >= 60)
                        {
                            thresh_image[i, j] = 255;
                        }
                        else
                            thresh_image[i, j] = 0;
                    }
                }
                return thresh_image;
            }
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
            private static int[,] Dilation(int[,] image)
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
                for (int i = 0; i < 1; i++)
                {
                    result = Dilation(result);
                }
                // Thực hiện phép co (erosion) 
                for (int i = 0; i < iterations_Erosion+2; i++)
                {
                    result = Erosion(result);
                }
                // Thực hiện phép mở rộng (dilation)
                for (int i = 0; i < iterations_Dilation; i++)
                {
                    result = Dilation(result);
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
                //gray = EdgeDetection.Blur_Image(gray);
                //int[,] blur = gray;
                //Bitmap img_blur = IntToBitmap(blur);
                int threhold = CalculateThreshold(IntToBitmap(gray));
                //int threhold1 = 63;
                //int threhold2 = 65;
                for (int i = 0; i < gray.GetLength(0); i++)
                {
                    for (int j = 0; j < gray.GetLength(1); j++)
                    {
                        if (gray[i, j] >= 60 )
                        {
                            gray[i, j] = 255;
                        }

                        else
                        {
                            gray[i, j] = 0;
                        }
                    }
                }
                //gray = MedianBlur(gray);
                gray = Erosion_Dilation(gray, 5, 5);
                string a =  imagePath + "_1.jpg";
                Bitmap SaveImage = IntToBitmap(gray);
                SaveImage.Save(a);
                int[,] edges = EdgeDetection.Canny_Detect(gray, high_threshold, low_threshold);

                return edges;
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
                        int grayscaleValue = pixelColor.R ;
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
            
            public static int[,] DrawLines(int[,] houghMatrix, int threshold, int width, int height)
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
                        int grayLevel = (int)(pixel.R);
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
            public double HuMoment(int[,] Binary_image)
            {
                double H = 0.0;
                double N_20 = CentralMoment2(Binary_image, 2, 0);
                double N_02 = CentralMoment2(Binary_image, 0, 2);
                //double N_11 = CentralMoment2(Binary_image, 1, 1);
                //double N_30 = CentralMoment2(Binary_image, 3, 0);
                //double N_12 = CentralMoment2(Binary_image, 1, 2);
                //double N_03 = CentralMoment2(Binary_image, 0, 3);
                //double N_21 = CentralMoment2(Binary_image, 2, 1);
                //double U_03 = CentralMoment1(Binary_image, 0, 3);
                H = N_20 + N_02;//
                //H[1] = Math.Pow((N_20 - N_02), 2) + 4 * Math.Pow(N_11, 2);//
                //H[2] = Math.Pow((N_30 - 3 * N_12), 2) + Math.Pow((3 * N_21 - N_03), 2);//
                //H[3] = Math.Pow((N_30 + N_12), 2) + Math.Pow((N_21 + N_03), 2);//
                //H[4] = (N_30 - 3 * N_12) * (N_30 + N_12) * (Math.Pow((N_30 + N_12), 2) - 3 * Math.Pow((N_21 + N_03), 2)) + (3 * N_21 - N_03) * (3 * Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2));
                //H[5] = (N_20 - N_02) * (Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2) + 4 * N_11 * (N_30 + N_12) * (N_21 + N_03));
                //H[6] = (3 * N_21 - N_03) * (N_30 + N_12) * (Math.Pow((N_30 + N_12), 2) - 3 * Math.Pow((N_21 + N_03), 2)) + (N_30 - 3 * N_12) * (N_21 + N_03) * (3 * Math.Pow((N_30 + N_12), 2) - Math.Pow((N_21 + N_03), 2));

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
            int high_threshold = 30;//ngưỡng trên cho canny detect
            int low_threshold = 15;//ngưỡng dưới cho canny detect 
            int threshold = 50;  // Ngưỡng để chọn các đỉnh trong ma trận Hough
                                 // int thre = Convert.ToInt16(text_thres.Text);
                                 //ảnh binary cho canny detect
            int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(imagePath, high_threshold, low_threshold);
            
            int[,] thresh_image = EdgeDetection.Thresh_image(imagePath);
              
            DetectShape detectShape = new DetectShape();
            double H = detectShape.HuMoment(thresh_image);
            // Lấy số hàng và số cột của mảng
            // Khởi tạo một mảng 2 chiều
            int width = edges.GetLength(0);
            int height = edges.GetLength(1);

            //biểu đồ hough
            int[,] hough = EdgeDetection.PerformHoughTransform(edges);

            //vẽ đường thẳng từ biểu đồ hough
            int[,] result = EdgeDetection.DrawLines(hough, threshold, width, height);

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

            textBox1.Text = H.ToString();

            // Dữ liệu hình chữ nhật,tròn, vuông
            if(H>0.000623 && H<0.000628)
            { textBox8.Text = "HÌNH TRÒN"; }   
            else if (H>0.000650 && H<0.000658)
            { textBox8.Text = "HÌNH VUÔNG"; }
            else if (H > 0.000691 && H < 0.000851)
            { textBox8.Text = "HÌNH CHỮ NHẬT"; }
            else
            { textBox8.Text = "KHÔNG NHẬN DIỆN "; }
           
            
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
