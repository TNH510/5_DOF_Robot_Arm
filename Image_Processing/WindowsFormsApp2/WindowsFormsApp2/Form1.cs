using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using ZedGraph;
using System.Linq;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static string imagePath;
        public Form1()
        {
            InitializeComponent();

        }
        static int[,] gradient;
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
                        int GrayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
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
                        if (gradientMagnitude[x, y]>max_gradient)
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
                int[,] blur = EdgeDetection.Blur_Image(gray);
                int[,] edges = EdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);

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
                        int pixelValue = (int)Binary_Image[x, y];
                        Color color = Color.FromArgb((byte)pixelValue, (byte)pixelValue, (byte)pixelValue);
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
            public static int[,] LinkEdges(string imagePath, int high_threshold, int low_threshold)
            {
                Bitmap Import_picture = new Bitmap(imagePath);

                int[,] gray = EdgeDetection.RGB2Gray(Import_picture);
                int[,] blur = EdgeDetection.Blur_Image(gray);
                int[,] edges = EdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);
                // Kích thước hình ảnh
                int width = edges.GetLength(0);
                int height = edges.GetLength(1);

                int[,] gradientMagnitude = new int[width, height];
                int[,] gradientAngle = new int[width, height];

                int[,] SobelX = {{ -1, 0, 1 },
                                    { -2, 0, 2 },
                                    { -1, 0, 1 }};
                int[,] SobelY = {{ -1, -2, -1 },
                                    { 0, 0, 0 },
                                    { 1, 2, 1 }};
                int white_point = 255;
                int gray_point = 50;
                //computting gradient magnitude and gradient angle
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        int sumX = 0;
                        int sumY = 0;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                sumX += (int)(SobelX[i + 1, j + 1] * blur[x + i, y + j]);
                                sumY += (int)(SobelY[i + 1, j + 1] * blur[x + i, y + j]);
                            }
                        }
                        //GradientX[x, y] = sumX;
                        //GradientY[x, y] = sumY;
                        gradientMagnitude[x, y] = (int)Math.Sqrt(sumX * sumX + sumY * sumY);
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
                //
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        int edge_point = edges[x, y];
                        if (edge_point == 255)
                        {
                            for (int i = -1; i <= 1; i++)
                            {
                                for (int j = -1; j <= 1; j++)
                                {
                                    if ((Math.Abs(gradientMagnitude[x + i, y + j] - gradientMagnitude[x, y]) < 50) & (Math.Abs(gradientAngle[x + i, y + j] - gradientAngle[x, y]) < 1) & (Math.Abs(gradientAngle[x + i, y + j] - gradientAngle[x, y]) > 0.1))
                                    {
                                        edges[x + i, y + j] = 255;
                                    }
                                }
                            }
                        }
                    }

                }
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
                                houghMatrix[theta, rho]= houghMatrix[theta,rho] + 1;
                                if(houghMatrix[theta, rho]>255)
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
                int[,] Point = new int[100, 2];
                int count = 0;
                int[,] theta_rho = new int[200, 3];
                int[,] result = new int [10, 3];
                // Duyệt qua ma trận Hough
                for (int theta = 0; theta < 180; theta++)
                {
                    for (int rho = 0; rho < 2 * diagonal; rho++)
                    {
                        if (houghMatrix[theta, rho] >= threshold)//giảm dần
                        {
                            //// lấy toàn bộ số điểm vượt ngưỡng 
                            theta_rho[count, 0] = theta;
                            theta_rho[count, 1] = rho;
                            theta_rho[count, 2] = 0;
                            count++;
                            
                        }
                    }
                }
                //phân loại đường thẳng 
                int delta_a = 0;
                int delta_p = 0;
                int delta_a_threshold = 10;
                int delta_p_threshold = 50;
                int nhom = 10;
                //int[,] phanloai = new int[count, 2];
                
                int[,,] ketqua = new int [count,2,nhom];

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
                    if( index!= 0)
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
                                    }
                                    else if (resultImage[x, y] == 255)
                                    {
                                        resultImage[x, y] = 10;
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
                                    }
                                    else if (resultImage[x, y] == 255)
                                    {
                                        resultImage[x, y] = 10;//giao điểm
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
            public static int[,] FindCorner(int[,]line )
            {
                int[,] PointMap = new int[line.GetLength(0), line.GetLength(1)];
                int distance = 0;
                int max_distance = 0;
                int Xmax=0;
                int Ymax=0;
                int Xmin = 0; 
                int Ymin=0;
                //tìm ra 2 điểm của 1 đường chéo 
                for (int i =0; i<line.GetLength(0);i++)
                {
                    for (int j =0;j<line.GetLength(1);j++)
                    {
                        if (line[i,j]==10)
                        {
                            Xmin = i;
                            Ymin = j;
                            PointMap[Xmin, Ymin] = 150;

                            for (int a =i; a< line.GetLength(0); a++)
                            {
                                for (int b =j; b< line.GetLength(1);b++)
                                {
                                    if (line[a,b]==10)
                                    {
                                        distance =(int)Math.Sqrt((i - a) * (i - a) + (j - b) * (j - b));
                                        if(distance> max_distance)
                                        {
                                            max_distance = distance;
                                            Xmax= a;
                                            Ymax= b;    
                                        }    
                                    }    
                                }    
                            }
                            PointMap[Xmax, Ymax]=150;
                        }    
                    }    
                }
                //Ax + By + C =0
                int A = Ymax - Ymin;
                int B = Xmax - Xmin;
                int C;
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
        }
        public class CannyThresholdSelector
        {
            public static (int, int) AutoSelectThreshold(Bitmap image)
            {
                // Chuyển đổi ảnh sang ảnh grayscale
                Bitmap grayscaleImage = ConvertToGrayscale(image);

                // Tính toán gradient theo hướng x và y
                int[,] gradientX = CalculateGradient(grayscaleImage, true);
                int[,] gradientY = CalculateGradient(grayscaleImage, false);

                // Tính toán magnitute gradient
                double[,] gradientMagnitude = CalculateGradientMagnitude(gradientX, gradientY);

                // Tính toán histogram gradient
                int[] histogram = CalculateGradientHistogram(gradientMagnitude);

                // Tính toán phân vị dựa trên histogram
                double percentile1 = Percentile(histogram, 0.1);
                double percentile2 = Percentile(histogram, 0.9);

                // Chọn ngưỡng dựa trên phân vị
                int threshold1 = (int)(percentile1 * 255);
                int threshold2 = (int)(percentile2 * 255);

                return (threshold1, threshold2);
            }

            private static Bitmap ConvertToGrayscale(Bitmap image)
            {
                Bitmap grayscaleImage = new Bitmap(image.Width, image.Height);

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        Color pixelColor = image.GetPixel(x, y);
                        int average = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                        grayscaleImage.SetPixel(x, y, Color.FromArgb(average, average, average));
                    }
                }

                return grayscaleImage;
            }

            private static int[,] CalculateGradient(Bitmap image, bool useXGradient)
            {
                int[,] gradient = new int[image.Width, image.Height];

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int pixel1 = image.GetPixel(x, y).R;
                        int pixel2;

                        if (useXGradient)
                        {
                            if (x < image.Width - 1)
                                pixel2 = image.GetPixel(x + 1, y).R;
                            else
                                pixel2 = image.GetPixel(x - 1, y).R;
                        }
                        else
                        {
                            if (y < image.Height - 1)
                                pixel2 = image.GetPixel(x, y + 1).R;
                            else
                                pixel2 = image.GetPixel(x, y - 1).R;
                        }

                        gradient[x, y] = Math.Abs(pixel1 - pixel2);
                    }
                }

                return gradient;
            }

            private static double[,] CalculateGradientMagnitude(int[,] gradientX, int[,] gradientY)
            {
                int width = gradientX.GetLength(0);
                int height = gradientX.GetLength(1);
                double[,] magnitude = new double[width, height];

                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        magnitude[x, y] = Math.Sqrt(Math.Pow(gradientX[x, y], 2) + Math.Pow(gradientY[x, y], 2));
                    }
                }

                return magnitude;
            }

            private static int[] CalculateGradientHistogram(double[,] gradientMagnitude)
            {
                int[] histogram = new int[1000];

                foreach (double value in gradientMagnitude)
                {
                    int intensity = (int)(value * 255);
                    // Kiểm tra xem intensity có nằm trong phạm vi của mảng histogram không
                    if (intensity >= 0 && intensity < histogram.Length)
                    {
                        histogram[intensity]++;
                    }
                    //histogram[intensity]++;
                }

                return histogram;
            }

            private static double Percentile(int[] histogram, double percentile)
            {
                int sum = histogram.Sum();
                int threshold = (int)(sum * percentile);

                int cumulativeSum = 0;
                for (int i = 0; i < histogram.Length; i++)
                {
                    cumulativeSum += histogram[i];
                    if (cumulativeSum >= threshold)
                        return i / 255.0;
                }

                return 1.0;
            }
        }
        public int[] TinhHistogram(int[,] gradient)
        {

            // khai báo mảng có 256 phần tử
            int[] histogram = new int[256];
            for (int x = 0; x < gradient.GetLength(0); x++)
                for (int y = 0; y < gradient.GetLength(1); y++)
                {

                    int color = gradient[x, y];
                    //byte gray = color.R;//trong hình  mức xám giá trị kênh R cũng giống  G hoặc B
                    // giá trị gray cũng chính là  phần tử thứ gray  trong mảng histogram dã khai báo 
                    //  Sẽ Tăng số đểm của phần tử thứ gray lên 1
                    histogram[color]++;

                }
            return histogram;

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

            gp.Title.Text = @"";//tên biểu đồ
            //gp.Rect = new Rectangle(0, 0, 1000, 900);// khung chứa biểu đồ
            // thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị đạo hàm";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 255;
            gp.XAxis.Scale.MajorStep = 5;
            gp.XAxis.Scale.MinorStep = 1;
            //Thiết lập trục đứng
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng Giá trị đạo hàm";
            gp.YAxis.Scale.Min = 0;
            gp.YAxis.Scale.Max = 25000;// cho giá trị lớn hơn giá trị điểm ảnh có nhiều nhất
            gp.YAxis.Scale.MajorStep = 5;
            gp.YAxis.Scale.MinorStep = 1;
            // dùng biểu đồ dạng Bar để  biểu diễn Histogram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);
            return gp;
        }

        private void open_Click_1(object sender, EventArgs e)
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
                picture1.Image = image;
            }
        }
        
        private void high_TextChanged(object sender, EventArgs e)
        {

        }

        private void process_Click_1(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(high.Text)) || (!string.IsNullOrEmpty(low.Text)) || (!string.IsNullOrEmpty(text1.Text)))
            {
                int high_threshold = Convert.ToInt16(high.Text);//ngưỡng trên cho canny detect
                int low_threshold = Convert.ToInt16(low.Text);//ngưỡng dưới cho canny detect 
                int threshold = Convert.ToInt16(text1.Text);  // Ngưỡng để chọn các đỉnh trong ma trận Hough
                
                //// 
                Bitmap image = new Bitmap(imagePath);
                (int threshold1, int threshold2) = CannyThresholdSelector.AutoSelectThreshold(image);
                ////
                //ảnh binary cho canny detect
                int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(imagePath, high_threshold, low_threshold);

                // Lấy số hàng và số cột của mảng
                // Khởi tạo một mảng 2 chiều
                int width = edges.GetLength(0);
                int height = edges.GetLength(1);

                //mở rộng các cạnh để triệt tiêu bớt nhiễu    
                //int[,] dilation = EdgeDetection.Dilation(edges, 6);

                //đưa độ dài cạnh về 1 để giảm dung lượng cho biểu đồ hough
                //int[,] skeleton = EdgeDetection.EdgeThinning(dilation);

                //biểu đồ hough
                int[,] hough = EdgeDetection.PerformHoughTransform(edges);

                //vẽ đường thẳng từ biểu đồ hough
                int[,] resultImage = EdgeDetection.DrawLines(hough, threshold, width, height);

                //int[,] PointMap = EdgeDetection.CheckPoint(resultImage);
                //resultImage = EdgeDetection.Dilation(resultImage, 3);
                //resultImage = EdgeDetection.EdgeThinning(resultImage);

                // thể hiện lên GUI
                picture2.Image = EdgeDetection.IntToBitmap(resultImage);
                picture3.Image = EdgeDetection.IntToBitmap(hough);
                picture4.Image = EdgeDetection.IntToBitmap(edges);

                // gọi hàm đã viết để chạy biểu đồ
                int[] histogram = TinhHistogram(gradient);
                PointPairList points = ChuyenDoiHistogram(histogram);
                // vẽ và hiển thị biểu đồ
                zGHistogram.GraphPane = BieuDoHistogram(points);
                zGHistogram.Refresh();


            }
            
                
            else
            {
                string message = "Nhập thiếu ngưỡng";
                string caption = "Thông báo";
                MessageBoxButtons buttons = MessageBoxButtons.OK;
                MessageBoxIcon icon = MessageBoxIcon.Information;

                MessageBox.Show(message, caption, buttons, icon);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
