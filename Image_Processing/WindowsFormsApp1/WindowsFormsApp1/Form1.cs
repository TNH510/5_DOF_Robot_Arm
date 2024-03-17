using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.CvEnum;
using Emgu.CV;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;
using OpenCvSharp;
using System.IO;
using Emgu.CV.CvEnum;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap Import_picture;
        Bitmap Output;
        private Image<Bgr, byte> inputImage;

        string link_picture = @"C:\Users\Loc\Desktop\XL123.jpg";
        public Form1()
        {
            Import_picture = new Bitmap(link_picture);
            InitializeComponent();
            Original_picture.Image = ConvertToGrayscale(Import_picture);
        }

        public Bitmap ColorImageBorderline(Bitmap Import_picture)
        {
            //tạo biến ngưỡng để xét với giá trị 
            int nguong = Convert.ToInt16(text_Threshold.Text);
            //tạo biến chứa hình mức xám
            //Bitmap hinhxam = ChuyenhinhRGBsanghinhxamLightness();
            //tạo ma trận sobel theo phương x
            int[,] ngang = { { -1, -2, -1  },
                             {  0,  0,  0  },
                             {  1,  2,  1  } };
            //tạo ma trận sobel theo phương y
            int[,] doc = { { -1, 0, 1 },
                             { -2, 0, 2 },
                             { -1, 0, 1 } };
            //tạo ma trận sobel theo phương x
            Bitmap imgboderline = new Bitmap(Import_picture.Width, Import_picture.Height);
            // dùng vòng for để đọc điểm ảnh ở dạng 2 chiều, bỏ viền ngoài của ảnh vì là mặt nạ 3x3
            for (int x = 1; x < Import_picture.Width - 1; x++)
                for (int y = 1; y < Import_picture.Height - 1; y++)
                {
                    //các biến cộng dồn giá trị điểm ảnh
                    int Gx = 0, Gy = 0;
                    int gray = 0;

                    //quét các điểm trong mặt nạ
                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            Color color = Import_picture.GetPixel(i, j);
                            //lấy giá trị xám
                            gray = color.R;
                            // nhân ma trận điểm ảnh với hệ số C
                            Gx += gray * ngang[i - x + 1, j - y + 1];
                            Gy += gray * doc[i - x + 1, j - y + 1];

                        }
                    int M = Math.Abs(Gx) + Math.Abs(Gy);
                    if (M <= nguong)
                        gray = 0;
                    else
                        gray = 255;
                    //set các điểm ảnh vào biến
                    imgboderline.SetPixel(x, y, Color.FromArgb(gray, gray, gray));


                }
            return imgboderline;

        }
        public Bitmap ColorImageSmoothing(Bitmap hinhgoc, int point, int level)
        {
            //tạo biến để chứa hình được smooth
            Bitmap pic_smoothed = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            //tạo vòng lặp để quét hình 
            // dùng 2 vòng FOR để quét hết ảnh, độ dài và độ rộng phải trừ đi viền của mặt nạ
            for (int x = point; x < hinhgoc.Width - point; x++)
                for (int y = point; y < hinhgoc.Height - point; y++)
                {
                    //biến cộng dồn cho các giá trị điểm màu trên mặt nạn của từng kênh R-G-B
                    int Rs = 0, Gs = 0, Bs = 0;
                    //quét mặt nạ
                    for (int i = x - point; i <= x + point; i++)
                        for (int j = y - point; j <= y + point; j++)
                        {
                            // đọc giá trị pixel tại điểm  ảnh có vị trí (i,j)
                            Color pixel = hinhgoc.GetPixel(i, j);
                            //lấy giá trị màu cảu các kênh
                            byte R = pixel.R;// giá trị kênh red
                            byte G = pixel.G;// giá trị kênh green
                            byte B = pixel.B;// giá trị kênh blue
                            byte A = pixel.A;// giá trị kênh blue
                            //cộng dồn giá trị màu của các kênh
                            Rs += R;
                            Gs += G;
                            Bs += B;


                        }
                    //K là số lượng điểm ảnh của mặt nạ
                    int K = level * level;
                    //đưa ra giá trị màu trung bình của kênh trong mặt nạ
                    Rs = (int)(Rs / K);
                    Gs = (int)(Gs / K);
                    Bs = (int)(Bs / K);
                    //gán giá trị màu vào biến bitmap đã tạo
                    pic_smoothed.SetPixel(x, y, Color.FromArgb(Rs, Gs, Bs));
                }
            //trả về giá trị của hàm
            return pic_smoothed;
        }

        // Hàm chuyển đổi ảnh màu sang ảnh xám
        static Bitmap ConvertToGrayscale(Bitmap inputImage)
        {
            Bitmap grayImage = new Bitmap(inputImage.Width, inputImage.Height);

            for (int x = 0; x < inputImage.Width; x++)
            {
                for (int y = 0; y < inputImage.Height; y++)
                {
                    Color pixel = inputImage.GetPixel(x, y);
                    int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                    grayImage.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }

            return grayImage;
        }

        // Hàm áp dụng bộ lọc Gaussian Blur
        static double[,] GaussianBlur(Bitmap image)
        {
            // Triển khai bộ lọc Gaussian Blur
            // Trả về ảnh đã được làm mịn dưới dạng một mảng hai chiều
            int[,] kernel = {
        {1, 2, 1},
        {2, 4, 2},
        {1, 2, 1}
    };

            int kernelSize = 3;
            int kernelWeight = 16;

            int width = image.Width;
            int height = image.Height;

            double[,] blurredImage = new double[width, height];

            // Áp dụng bộ lọc Gaussian Blur
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    double sum = 0;
                    for (int i = 0; i < kernelSize; i++)
                    {
                        for (int j = 0; j < kernelSize; j++)
                        {
                            Color pixel = image.GetPixel(x - 1 + i, y - 1 + j);
                            int grayValue = (int)(pixel.R * 0.3 + pixel.G * 0.59 + pixel.B * 0.11);
                            sum += kernel[i, j] * grayValue;
                        }
                    }
                    blurredImage[x, y] = sum / kernelWeight;
                }
            }

            return blurredImage;
        }

        // Hàm tính toán gradient của ảnh
        static double[,] ComputeGradientMagnitudes(double[,] smoothedImage)
        {
            // Triển khai tính toán gradient
            // Trả về gradient magnitudes dưới dạng một mảng hai chiều
            int width = smoothedImage.GetLength(0);
            int height = smoothedImage.GetLength(1);

            double[,] gradientMagnitudes = new double[width, height];

            // Tính toán gradient của ảnh
            for (int x = 1; x < width - 1; x++)
            {
                for (int y = 1; y < height - 1; y++)
                {
                    double dx = smoothedImage[x + 1, y] - smoothedImage[x - 1, y];
                    double dy = smoothedImage[x, y + 1] - smoothedImage[x, y - 1];
                    gradientMagnitudes[x, y] = Math.Sqrt(dx * dx + dy * dy);
                }
            }

            return gradientMagnitudes;
        }

        // Hàm áp dụng ngưỡng Thresholding
        static double[,] ApplyThreshold(double[,] gradientMagnitudes, double threshold)
        {
            // Triển khai ngưỡng hóa
            // Trả về ảnh nhị phân dưới dạng một mảng hai chiều
            int width = gradientMagnitudes.GetLength(0);
            int height = gradientMagnitudes.GetLength(1);

            double[,] thresholdedImage = new double[width, height];

            // Áp dụng ngưỡng Thresholding
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    thresholdedImage[x, y] = gradientMagnitudes[x, y] > threshold ? 255 : 0;
                }
            }

            return thresholdedImage;
        }

        // Hàm chuyển đổi ảnh nhị phân trở lại ảnh màu
        static Bitmap ConvertToColor(double[,] thresholdedImage)
        {
            // Chuyển đổi ảnh nhị phân trở lại ảnh màu
            // Trả về một Bitmap
            int width = thresholdedImage.GetLength(0);
            int height = thresholdedImage.GetLength(1);

            Bitmap outputImage = new Bitmap(width, height);

            // Chuyển đổi ảnh nhị phân trở lại ảnh màu
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int pixelValue = (int)thresholdedImage[x, y];
                    Color color = Color.FromArgb(pixelValue, pixelValue, pixelValue);
                    outputImage.SetPixel(x, y, color);
                }
            }

            return outputImage;
        }

        // Hàm áp dụng thuật toán Canny Edge Detection
        static Bitmap ApplyCannyEdgeDetection(Bitmap inputImage)
        {

           // int nguong = Convert.ToInt16(text_Threshold.Text);
            // Bước 1: Chuyển đổi ảnh màu sang ảnh xám
            Bitmap grayImage = ConvertToGrayscale(inputImage);

            // Bước 2: Làm mịn ảnh (Gaussian Blur)
            double[,] smoothedImage = GaussianBlur(grayImage);

            // Bước 3: Tính toán gradient của ảnh
            double[,] gradientMagnitudes = ComputeGradientMagnitudes(smoothedImage);

            // Bước 4: Áp dụng ngưỡng Thresholding
            
            double[,] thresholdedImage = ApplyThreshold(gradientMagnitudes,50);

            // Bước 5: Chuyển đổi ảnh nhị phân trở lại ảnh màu
            Bitmap outputImage = ConvertToColor(thresholdedImage);

            return outputImage;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Processed_picture.Image= ApplyCannyEdgeDetection(Import_picture);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
