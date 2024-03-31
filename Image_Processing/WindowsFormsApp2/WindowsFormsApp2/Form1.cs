using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
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
        class CannyEdgeDetection
        {
            private static int[,] RGB2Gray(Bitmap ColorImage)
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

                int[,] gray = CannyEdgeDetection.RGB2Gray(Import_picture);
                int[,] blur = CannyEdgeDetection.Blur_Image(gray);
                int[,] edges = CannyEdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);
                
                return edges;
            }
            public static Bitmap IntToBitmap(int[,] Binary_Image)
            {
                // Kích thước hình ảnh
                int width = Binary_Image.GetLength(0);
                int height = Binary_Image.GetLength(1);

                // Tạo bitmap từ mảng hai chiều
                Bitmap Result = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        int pixelValue = (int)Binary_Image[x, y];
                        Color color = Color.FromArgb(pixelValue, pixelValue, pixelValue);
                        Result.SetPixel(x, y, color);
                    }
                }
                return Result;
            }
            public static int[,] LinkEdges(string imagePath, int high_threshold, int low_threshold)
            {
                Bitmap Import_picture = new Bitmap(imagePath);

                int[,] gray = CannyEdgeDetection.RGB2Gray(Import_picture);
                int[,] blur = CannyEdgeDetection.Blur_Image(gray);
                int[,] edges = CannyEdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);
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
                                    if ((Math.Abs(gradientMagnitude[x+i, y+j] - gradientMagnitude[x , y ]) < 50) & (Math.Abs(gradientAngle[x+i, y+j] - gradientAngle[x, y]) < 1) & (Math.Abs(gradientAngle[x+i, y+j] - gradientAngle[x , y]) > 0.1) )
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
        }
        class ClosingAlgorithm
        {
            // Hàm đóng (closing) cho ảnh xám
            public static Bitmap Closing(int[,]image, int iterations)
            {
                Bitmap image1 = CannyEdgeDetection.IntToBitmap(image); 
                // Bước 1: Dilate (trải rộng) ảnh
                Bitmap dilatedImage = Dilate(image1, iterations);

                // Bước 2: Erode (co lại) ảnh kết quả từ bước 1
                Bitmap closedImage = Erode(dilatedImage, iterations);

                return closedImage;
            }

            // Hàm Dilate (trải rộng) cho ảnh xám
            public static Bitmap Dilate(Bitmap image, int iterations)
            {
                Bitmap result = new Bitmap(image.Width, image.Height);

                for (int i = 0; i < iterations; i++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            // Kiểm tra xem có pixel trắng nào xung quanh không
                            if (CheckNeighbors(image, x, y))
                            {
                                result.SetPixel(x, y, Color.White);
                            }
                            else
                            {
                                result.SetPixel(x, y, Color.Black);
                            }
                        }
                    }

                    // Copy kết quả dilate vào ảnh gốc để tiếp tục lặp
                    CopyBitmap(result, image);
                }

                return result;
            }

            // Hàm Erode (co lại) cho ảnh xám
            public static Bitmap Erode(Bitmap image, int iterations)
            {
                Bitmap result = new Bitmap(image.Width, image.Height);

                for (int i = 0; i < iterations; i++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        for (int y = 0; y < image.Height; y++)
                        {
                            // Kiểm tra xem có pixel đen nào xung quanh không
                            if (CheckNeighbors(image, x, y, true))
                            {
                                result.SetPixel(x, y, Color.Black);
                            }
                            else
                            {
                                result.SetPixel(x, y, Color.White);
                            }
                        }
                    }

                    // Copy kết quả erode vào ảnh gốc để tiếp tục lặp
                    CopyBitmap(result, image);
                }

                return result;
            }

            // Hàm kiểm tra xem có pixel trắng hoặc đen xung quanh không
            private static bool CheckNeighbors(Bitmap image, int x, int y, bool black = false)
            {
                int[] dx = { -1, 0, 1, -1, 1, -1, 0, 1 };
                int[] dy = { -1, -1, -1, 0, 0, 1, 1, 1 };

                for (int i = 0; i < 8; i++)
                {
                    int nx = x + dx[i];
                    int ny = y + dy[i];

                    if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height)
                    {
                        Color pixel = image.GetPixel(nx, ny);

                        if (!black && pixel.R == 255)
                        {
                            return true;
                        }
                        else if (black && pixel.R == 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            // Hàm copy dữ liệu từ một bitmap sang bitmap khác
            private static void CopyBitmap(Bitmap source, Bitmap destination)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    for (int y = 0; y < source.Height; y++)
                    {
                        destination.SetPixel(x, y, source.GetPixel(x, y));
                    }
                }
            }
        }

            private static bool IsToBeDeleted(int[,] image, int x, int y)
        {
            int backgroundCount = 0;
            int transitionCount = 0;

            int[] neighborValues = new int[9]
            {
        image[y - 1, x - 1], image[y - 1, x], image[y - 1, x + 1],
        image[y, x - 1], image[y, x], image[y, x + 1],
        image[y + 1, x - 1], image[y + 1, x], image[y + 1, x + 1]
            };

            for (int i = 0; i < neighborValues.Length - 1; i++)
            {
                if (neighborValues[i] == 0 && neighborValues[i + 1] == 1)
                {
                    transitionCount++;
                }

                if (neighborValues[i] == 0)
                {
                    backgroundCount++;
                }
            }

            if (neighborValues[neighborValues.Length - 1] == 0 && neighborValues[0] == 1)
            {
                transitionCount++;
            }

            if (neighborValues[neighborValues.Length - 1] == 0)
            {
                backgroundCount++;
            }

            return (backgroundCount >= 2 && backgroundCount <= 6 && transitionCount == 1 &&
                    image[y - 1, x] * image[y, x + 1] * image[y + 1, x] == 0 &&
                    image[y, x + 1] * image[y + 1, x] * image[y, x - 1] == 0);
        }
        private void open_Click(object sender, EventArgs e)
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

        private void process_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(high.Text)) || (!string.IsNullOrEmpty(low.Text)))
            {
                int high_threshold = Convert.ToInt16(high.Text);
                int low_threshold = Convert.ToInt16(low.Text);
                int[,] edges = CannyEdgeDetection.DeTectEdgeByCannyMethod(imagePath, high_threshold, low_threshold);
                //int[,] result = CannyEdgeDetection.LinkEdges(imagePath, high_threshold, low_threshold);

                //Bitmap bitmap = ClosingAlgorithm.Closing(edges,1);
                //Bitmap dilation = ClosingAlgorithm.Dilate(CannyEdgeDetection.IntToBitmap(edges), 3);
                Bitmap bitmap = CannyEdgeDetection.IntToBitmap(edges);

                picture2.Image = bitmap;
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
