using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
                int[,] GrayImage= new int[width,height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = ColorImage.GetPixel(x, y);
                        int GrayValue = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                        GrayImage[x, y]= GrayValue;
                    }  
                }  
                       
                return GrayImage;
            }
            private static int[,] Blur_Image(int[,] GrayImage)
            {
                int width = GrayImage.GetLength(0);
                int height =GrayImage.GetLength(1);

                int[,] BlurImage=new int[width,height];

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
                            for(int j =-2;j<2;j++)
                            {
                                sum += GaussianKernel[i+2,j+2]*GrayImage[x+i,y+j];

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
                int[,] gradientMagnitude= new int [BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
                int[,] gradientAngle = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
                int[,] Result  = new int[BlurredImage.GetLength(0), BlurredImage.GetLength(1)];
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
                        gradientMagnitude[x,y] = (int)Math.Sqrt(sumX * sumX + sumY * sumY);
                        if (gradientMagnitude[x, y]>=255)
                        {
                            gradientMagnitude[x, y] = 255;
                        }    
                        else if (gradientMagnitude[x,y]<=0)
                        {
                            gradientMagnitude[x, y] = 0;
                        }
                        gradientAngle[x,y] = (int)Math.Abs((Math.Atan2(sumY, sumX) * 180 / Math.PI));

                    }
                }

                for (int x = 1; x < BlurredImage.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < BlurredImage.GetLength(1) - 1; y++)
                    {
                        double q = 255;
                        double r = 255;
                        if ((gradientAngle[x, y] >= 0 && gradientAngle[x, y] < 22.5)|| (gradientAngle[x, y] <= 180 && gradientAngle[x, y] >= 157.5))
                        {
                            q = gradientMagnitude[x, y-1];
                            r = gradientMagnitude[x, y+1];
                        }
                        else if (gradientAngle[x, y] >= 22.5 && gradientAngle[x, y] < 67.5)
                        {
                            q = gradientMagnitude[x+1, y - 1];
                            r = gradientMagnitude[x-1, y + 1];
                        }
                        else if (gradientAngle[x, y] >= 67.5 && gradientAngle[x, y] < 112.5)
                        {
                            q = gradientMagnitude[x + 1, y ];
                            r = gradientMagnitude[x - 1, y ];
                        }
                        else if (gradientAngle[x, y] >= 112.5 && gradientAngle[x, y] < 157.5)
                        {
                            q = gradientMagnitude[x - 1, y - 1];
                            r = gradientMagnitude[x + 1, y + 1];
                        }
                        if(gradientMagnitude[x,y]>=q && gradientMagnitude[x,y]>=r)
                        {
                            Result[x, y] = gradientMagnitude[x,y];
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
                return Result;
            }
            public static Bitmap DeTectEdgeByCannyMethod(string imagePath, int high_threshold, int low_threshold)
            {
                Bitmap Import_picture = new Bitmap(imagePath);
                
                int[,] gray = CannyEdgeDetection.RGB2Gray(Import_picture);
                int[,] blur = CannyEdgeDetection.Blur_Image(gray);
                int[,] edges = CannyEdgeDetection.Canny_Detect(blur, high_threshold, low_threshold);
                // Kích thước hình ảnh
                int width = edges.GetLength(0);
                int height = edges.GetLength(1);

                // Tạo bitmap từ mảng hai chiều
                Bitmap Result = new Bitmap(width, height);
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        if (edges[x, y] >= 255)
                            edges[x, y] = 255;
                        else if (edges[x, y] <= 0)
                            edges[x, y] = 0;
                        int pixelValue = (int)edges[x, y];
                        Color color = Color.FromArgb(pixelValue, pixelValue, pixelValue);
                        Result.SetPixel(x, y, color);
                    }
                }
                return Result;
            }
        }

        private void Open_Image_Click(object sender, EventArgs e)
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
        }

        private void Active_Image_Click(object sender, EventArgs e)
        {
            if ((!string.IsNullOrEmpty(HIGH_Threshold.Text)) || (!string.IsNullOrEmpty(LOW_Threshold.Text)))
            {
                int high_threshold = Convert.ToInt16(HIGH_Threshold.Text);
                int low_threshold = Convert.ToInt16(LOW_Threshold.Text);
                Processed_Pic.Image = CannyEdgeDetection.DeTectEdgeByCannyMethod(imagePath,high_threshold,low_threshold);

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

        private void LOW_Threshold_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
