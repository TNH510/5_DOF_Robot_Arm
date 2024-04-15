using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using static System.Net.Mime.MediaTypeNames;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        static string imagePath;
        public Form1()
        {
            InitializeComponent();

        }
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
            private static int[,] Blur_Image(int[,] GrayImage)
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
            private static int[,] Canny_Detect(int[,] BlurredImage, int high_threshold, int low_threshold)
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
                                houghMatrix[theta, rho]++;

                            }
                        }
                    }
                }

                return houghMatrix;
            }
            public static Bitmap DrawingEdges(int[,] HoughMatrix, Bitmap Gray_Image)
            {
                int width = HoughMatrix.GetLength(0);
                int height = HoughMatrix.GetLength(1);
                for (int theta = 0; theta < width; theta++)
                {
                    for (int rho = 0; rho < height; rho++)
                    {
                        if (HoughMatrix[theta, rho] > 70)
                        {
                            for (int x = 0; x < Gray_Image.Width; x++)
                            {
                                double theta_radian = theta * Math.PI / 180;
                                double y = rho / Math.Cos(theta_radian) - (x - Gray_Image.Width / 2) * Math.Tan(theta_radian);
                                if (y >= 0 && y < Gray_Image.Height)
                                {
                                    Gray_Image.SetPixel(x, (int)y, Color.Red);
                                }
                            }

                        }

                    }
                }
                return Gray_Image;
            }
            public static int[,] ComputeHoughMatrix(int[,] edgeMatrix)
            {
                int width = edgeMatrix.GetLength(1);
                int height = edgeMatrix.GetLength(0);
                int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

                // Khởi tạo ma trận Hough
                int[,] houghMatrix = new int[180, 2 * diagonal];

                // Duyệt qua từng điểm cạnh trong ma trận cạnh
                for (int y = 0; y < height; y++)
                {
                    for (int x = 0; x < width; x++)
                    {
                        if (edgeMatrix[y, x] == 255) // Kiểm tra nếu điểm là điểm cạnh
                        {
                            // Duyệt qua mọi giá trị của theta (0-179)
                            for (int theta = 0; theta < 180; theta++)
                            {
                                double radianTheta = theta * Math.PI / 180;
                                int rho = (int)(x * Math.Cos(radianTheta) + y * Math.Sin(radianTheta));
                                houghMatrix[theta, rho + diagonal]++;
                            }
                        }
                    }
                }

                return houghMatrix;
            }
            public static Bitmap DrawLines(int[,] houghMatrix, int threshold, int width, int height)
            {
                int diagonal = (int)Math.Sqrt(width * width + height * height); // Đường chéo của ảnh

                Bitmap resultImage = new Bitmap(width, height);

                // Duyệt qua ma trận Hough
                for (int theta = 0; theta < 180; theta++)
                {
                    for (int rho = 0; rho < 2 * diagonal; rho++)
                    {
                        if (houghMatrix[theta, rho] >= threshold)
                        {
                            double radianTheta = theta * Math.PI / 180;

                            if (theta >= 45 && theta < 135)
                            {
                                for (int x = 0; x < width; x++)
                                {
                                    int y = (int)(((rho - diagonal) - x * Math.Cos(radianTheta)) / Math.Sin(radianTheta));
                                    if (y >= 0 && y < height)
                                    {
                                        resultImage.SetPixel(x, y, Color.Red);
                                    }

                                }
                            }
                            else
                            {
                                for (int y = 0; y < height; y++)
                                {
                                    int x = (int)(((rho - diagonal) - y * Math.Sin(radianTheta)) / Math.Cos(radianTheta));
                                    if (x >= 0 && x < width)
                                    {
                                        resultImage.SetPixel(x, y, Color.Red);
                                    }

                                }
                            }
                        }
                    }
                }

                return resultImage;
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
                
                //ảnh binary cho canny detect
                int[,] edges = EdgeDetection.DeTectEdgeByCannyMethod(imagePath, high_threshold, low_threshold);
                
                // Lấy số hàng và số cột của mảng
                // Khởi tạo một mảng 2 chiều

                int width = edges.GetLength(0);
                int height = edges.GetLength(1);

                //mở rộng các cạnh để triệt tiêu bớt nhiễu    
                int[,] dilation = EdgeDetection.Dilation(edges, 6);
                //đưa độ dài cạnh về 1 để giảm dung lượng cho biểu đồ hough
                int[,] skeleton = EdgeDetection.EdgeThinning(dilation);
                //biểu đồ hough
                int[,] hough = EdgeDetection.PerformHoughTransform(skeleton);
                //vẽ đường thẳng từ biểu đồ hough
                Bitmap resultImage = EdgeDetection.DrawLines(hough, threshold, width, height);

                picture2.Image = resultImage;
                picture3.Image = EdgeDetection.IntToBitmap(dilation);
                picture4.Image = EdgeDetection.IntToBitmap(skeleton);
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
    }
}
