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
        Bitmap Import_picture;
        public Form1()
        {
            InitializeComponent();
            
        }
        class CannyEdgeDetection
        {
            public static int[,] RGB2Gray(Bitmap Image_Input)
            {

                int width = Image_Input.Width;
                int height = Image_Input.Height;
                int[,] Gray_Image= new int[width,height];

                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        Color pixelColor = Image_Input.GetPixel(x, y);
                        int Gray_Value = (int)(pixelColor.R * 0.299 + pixelColor.G * 0.587 + pixelColor.B * 0.114);
                        Gray_Image[x, y]= Gray_Value;
                    }  
                }  
                       
                return Gray_Image;
            }
            public static int[,] Blur_Image(int[,] Gray_Image)
            {
                int width = Gray_Image.GetLength(0);
                int height =Gray_Image.GetLength(1);

                int[,] Blur_Image=new int[width,height];

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
                                sum += GaussianKernel[i+2,j+2]*Gray_Image[x+i,y+j];

                            }    
                        }
                        Blur_Image[x, y] = (int)(sum / 159);
                    }    
                }    

                return Blur_Image;
            }
            public static int[,] Computting_Gradient(int[,] Blur_Image, int high_threshold, int low_threshold)
            {
                //int[,] GradientX = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                //int[,] GradientY = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                int[,] gradientMagnitude= new int [Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                int[,] gradientAngle = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                int[,] gradient = new int[Blur_Image.GetLength(0), Blur_Image.GetLength(1)];
                int[,] SobelX = {{ -1, 0, 1 },
                                    { -2, 0, 2 },
                                    { -1, 0, 1 }};
                int[,] SobelY = {{ -1, -2, -1 },
                                    { 0, 0, 0 },
                                    { 1, 2, 1 }};
                for (int x = 1; x < Blur_Image.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < Blur_Image.GetLength(1) - 1; y++)
                    {
                        int sumX = 0;
                        int sumY = 0;
                        for (int i = -1; i <= 1; i++)
                        {
                            for (int j = -1; j <= 1; j++)
                            {
                                sumX += (int)(SobelX[i + 1, j + 1] * Blur_Image[x + i, y + j]);
                                sumY += (int)(SobelY[i + 1, j + 1] * Blur_Image[x + i, y + j]);
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
                for (int x = 1; x < Blur_Image.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < Blur_Image.GetLength(1) - 1; y++)
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
                            gradient[x, y] = gradientMagnitude[x,y];
                        }    
                        else
                        {
                            gradient[x, y] = 0;
                        }
                        if (gradient[x, y] >= high_threshold)
                        {
                            gradient[x, y] = 255;
                        }
                        else if (gradient[x, y] >= low_threshold && gradient[x, y] < high_threshold)
                        {
                            gradient[x, y] = 50;
                        }
                        else
                        {
                            gradient[x, y] = 0;
                        }
                    }
                }
                //int[,] output=new int[gradient.GetLength(0) , gradient.GetLength(1) ];
                for (int x = 1; x < gradient.GetLength(0) - 1; x++)
                {
                    for (int y = 1; y < gradient.GetLength(1) - 1; y++)
                    {
                        if (gradient[x, y] == 50)
                        {
                            if ((gradient[x + 1, y - 1] == 255) || (gradient[x + 1, y] == 255) || (gradient[x + 1, y + 1] == 255) || (gradient[x, y - 1] == 255) || (gradient[x, y + 1] == 255) || (gradient[x - 1, y - 1] == 255) || (gradient[x - 1, y] == 255) || (gradient[x - 1, y + 1] == 255))
                            {
                                gradient[x, y] = 255;
                            }
                            else
                                gradient[x, y] = 0;
                        }

                    }
                }

                return gradient;
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
            Import_picture = new Bitmap(imagePath);
            int[,] gray = CannyEdgeDetection.RGB2Gray(Import_picture);
            int[,] blur = CannyEdgeDetection.Blur_Image(gray);
        
            int[,] edges= CannyEdgeDetection.Computting_Gradient(blur,80,40);

            // Kích thước hình ảnh
            int width = edges.GetLength(0);
            int height = edges.GetLength(1);

            // Tạo bitmap từ mảng hai chiều
            Bitmap bitmap = new Bitmap(width, height);
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
                    bitmap.SetPixel(x, y, color);
                }
            }

            Processed_Pic.Image = bitmap;
        }
    }
}
