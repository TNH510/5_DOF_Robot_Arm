using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Testing_Detect_Shape
{
    public partial class Form1 : Form
    {
        Bitmap hinhgoc;
        string imagePath;
        public Form1()
        {
            InitializeComponent();
        }
        public Bitmap RGB2Gray(Bitmap hinhgoc)
        {
            Bitmap HinhMucXam = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //lấy điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    // Tính giá trị mức xám cho điểm (x,y)
                    byte gray = R;
                    
                    // gán giá trị mức xám vừa tính
                    HinhMucXam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }
            return HinhMucXam;
        }
        public double Moment_Calculate(int[,]Binary_image, int i, int j)
        {
            double Moment=0;
            for(int x=0; x< Binary_image.GetLength(0); x++)
            {
                for(int y=0; y< Binary_image.GetLength(1);y++)
                {
                    Moment= Moment+Math.Pow(x,i)*Math.Pow(y,j)*Binary_image[x,y];
                }    
            }    
            return Moment;
        }

        public double CentralMoment1(int[,] Binary_image, int i, int j)
        {
            double U_ij=0;
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
            double U_ij= CentralMoment1(Binary_image, i, j);
            double U_00 = CentralMoment1(Binary_image, 0, 0);
            double N_ij=U_ij/Math.Pow(U_00, (i+j+2)/2);
            return N_ij;
        }
        public double[] HuMoment(int[,] Binary_image)
        {
            double[] H = new double[7];
            double N_20 = CentralMoment2(Binary_image, 2, 0);
            double N_02 = CentralMoment2(Binary_image, 0, 2);
            double N_11 = CentralMoment2(Binary_image, 1, 1);
            double N_30 = CentralMoment2(Binary_image, 3, 0);
            double N_12 = CentralMoment2(Binary_image, 1, 2);
            double N_03 = CentralMoment2(Binary_image, 0, 3);
            double N_21 = CentralMoment2(Binary_image, 2, 1);
            double U_03 = CentralMoment1(Binary_image, 0, 3);
            H[0] = N_20 + N_02;//
            H[1] = Math.Pow((N_20 - N_02),2) + 4 * Math.Pow(N_11,2) ;//
            H[2] = Math.Pow((N_30 - 3 * N_12), 2) + Math.Pow((3*N_21-N_03),2);//
            H[3] = Math.Pow((N_30 + N_12), 2) + Math.Pow((N_21 + N_03), 2);//
            H[4] = (N_30-3*N_12)*(N_30+N_12) * (Math.Pow((N_30 + N_12),2) - 3 * Math.Pow((N_21+N_03),2)) + ( 3 * N_21 - N_03 ) * ( 3 * Math.Pow((N_30+N_12),2) - Math.Pow(( N_21 +N_03),2));
            H[5] = (N_20 - N_02) * (Math.Pow((N_30 + N_12),2) - Math.Pow((N_21 + N_03),2) + 4 * N_11 * (N_30 + N_12) * (N_21 + N_03));
            H[6] = (3 * N_21 - N_03) * (N_30 + N_12) * (Math.Pow((N_30 + N_12),2) - 3 * Math.Pow((N_21 + N_03) ,2)) + (N_30 - 3 * N_12) * (N_21 + N_03) * (3 * Math.Pow((N_30 + N_12),2) - Math.Pow((N_21 + N_03),2));

            for (int i = 0; i < 7; i++)
            {
                H[i] = -1 * Math.Sign(H[i]) * Math.Log10(Math.Abs(H[i]));
            }

            return H;
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
           // gradient = gradientMagnitude;
            return Result;
        }
        public int[] TinhHistogram(int[,] Gray)
        {
            // khai báo mảng có 256 phần tử
            int[] histogram = new int[256];
            for (int x = 0; x < Gray.GetLength(0); x++)
                for (int y = 0; y < Gray.GetLength(1); y++)
                {                  
                    int gray = Gray[x,y];//trong hình  mức xám giá trị kênh R cũng giống  G hoặc B
                    // giá trị gray cũng chính là  phần tử thứ gray  trong mảng histogram dã khai báo 
                    //  Sẽ Tăng số đểm của phần tử thứ gray lên 1
                    histogram[gray]++;

                }
            return histogram;
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
                    int grayscaleValue = (pixelColor.R + pixelColor.G + pixelColor.B) / 3;
                    imageArray[i, j] = grayscaleValue;
                }
            }

            return imageArray;
        }
        public static int CalculateThreshold(int[,] Gray)
        {
            // Tính histogram
            int[] histogram = new int[256];
            int totalPixels = 0;

            for (int x = 0; x < Gray.GetLength(0); x++)
            {
                for (int y = 0; y < Gray.GetLength(1); y++)
                {                    
                    int grayLevel = Gray[x, y];
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

            gp.Title.Text = @"Biểu đồ  Histogram";//tên biểu đồ
            //gp.Rect = new Rectangle(0, 0, 1000, 900);// khung chứa biểu đồ
            // thiết lập trục ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của điểm ảnh";
            gp.XAxis.Scale.Min = 0;
            gp.XAxis.Scale.Max = 255;
            gp.XAxis.Scale.MajorStep = 5;
            gp.XAxis.Scale.MinorStep = 1;
            //Thiết lập trục đứng
            gp.YAxis.Title.Text = @"Giá trị mức xám của điểm ảnh";
            gp.YAxis.Scale.Min = 0;
            gp.YAxis.Scale.Max = 25000;// cho giá trị lớn hơn giá trị điểm ảnh có nhiều nhất
            gp.YAxis.Scale.MajorStep = 5;
            gp.YAxis.Scale.MinorStep = 1;
            // dùng biểu đồ dạng Bar để  biểu diễn Histogram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);
            return gp;
        }

        private void OPEN_Click(object sender, EventArgs e)
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
                Original_Image.Image = image;
            }
        }

        private void THRESHOLD_Click(object sender, EventArgs e)
        {
            int[,] Temp = Canny_Detect(BitmapToInt(RGB2Gray(hinhgoc)),50,200);
            int Threshold = CalculateThreshold(Temp);

            for (int i = 0; i < Temp.GetLength(0); i++)
            {
                for (int j = 0; j < Temp.GetLength(1); j++)
                {
                    if (Temp[i, j] < Threshold)
                    {
                        Temp[i, j] = 0;
                    }
                    else
                    {
                        Temp[i, j] = 255;
                    }
                }
            }
            Bitmap binary_image = IntToBitmap(Temp);
            Binary_Image.Image = binary_image;
            text_threshold.Text = Threshold.ToString();
        }

        private void PREDICT_Click(object sender, EventArgs e)
        {
            int[,] Temp = Canny_Detect(BitmapToInt(RGB2Gray(hinhgoc)), 50, 200);
            //int[,] Temp = BitmapToInt(RGB2Gray(hinhgoc));
            //int Threshold = Convert.ToInt16(textBox.Text);
            //for (int i = 0; i < Temp.GetLength(0); i++)
            //{
            //    for (int j = 0; j < Temp.GetLength(1); j++)
            //    {
            //        if (Temp[i,j] < Threshold)
            //        {
            //            Temp[i,j] = 0;
            //        }
            //        else
            //        {
            //            Temp[i, j] = 255;
            //        }
            //    }
            //}
            Binary_Image.Image = IntToBitmap(Temp);
            double[] H = HuMoment(Temp);
            textBox1.Text = H[0].ToString();
            textBox2.Text = H[1].ToString();
            textBox3.Text = H[2].ToString();
            textBox4.Text = H[3].ToString();
            textBox5.Text = H[4].ToString();
            textBox6.Text = H[5].ToString();
            textBox7.Text = H[6].ToString();

            // Dữ liệu hình chữ nhật,tròn, vuông, tam giác
            double[,] H1= new double[4,7] 
            { 
                { 3.12312876539246, 6.85381516209359, 9.88575150311941, 10.7838647822104, 15.6771151736907, -14.5406929254356, -21.1913516631291  } ,
                { 3.20469343954819, 10.346963490014, 10.2131978348198, 13.2563928897252, -18.6600719397016, 18.5286332390215, 24.991593246933 } ,
                { 3.18467661863633, 17.3395943883656, 9.90880841836875, 10.8155095284822, -21.1776685117439, -19.485306722665, 34.4426325862341},
                { 3.10917495216815, 7.45148166979752 , 3.07362704873088, 5.00704032046893, 6.31108372681061, -9.73140421171348, -9.36694031488709} ,                                        
            };
            // tính  giá trị chênh lệch, chênh lệch thấp => tương thích cao.
            double[] D = new double[4] { 0,0,0,0};
            for (int i=0; i<4;i++)
            {
                for(int j=0; j<2; j++)
                {
                    D[i] = D[i] + Math.Sqrt( (H1[i,j] - H[j]) * (H1[i, j] - H[j]) );
                    //D[i] = D[i] + Math.Abs((H1[i, j] - H[j]));
                }    
            }
            // tìm giá trị nhỏ nhất
            double minValue = D[0]; // Giả sử giá trị đầu tiên là giá trị nhỏ nhất
            int minIndex = 0; // Giả sử vị trí đầu tiên là vị trí của giá trị nhỏ nhất

            for (int i = 1; i < D.Length; i++)
            {
                if (D[i] < minValue)
                {
                    minValue = D[i];
                    minIndex = i;
                }
            }
            // cho kết quả
            switch (minIndex) 
            { 
                case 0:
                    textBox8.Text = "HÌNH CHỮ NHẬT";
                    break;
                case 1:
                    textBox8.Text = "HÌNH TRÒN";
                    break;
                case 2:
                    textBox8.Text = "HÌNH VUÔNG";
                    break;
                case 3:
                    textBox8.Text = "HÌNH TAM GIÁC";
                    break;
            }

        }

        private void HISTOGRAM_Click(object sender, EventArgs e)
        {
            hinhgoc = new Bitmap(imagePath);
            //hiển thị hình gốc
            Original_Image.Image = hinhgoc;
            //hiển thị hình xám
            Bitmap HinhMucXam = RGB2Gray(hinhgoc);
            Binary_Image.Image = HinhMucXam;

            // gọi hàm đã viết để chạy biểu đồ
            int[] histogram = TinhHistogram(BitmapToInt(hinhgoc));
            PointPairList points = ChuyenDoiHistogram(histogram);
            // vẽ và hiển thị biểu đồ
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }

        private void THRESHOLD_MAN_Click(object sender, EventArgs e)
        {
            int[,] Temp = BitmapToInt(RGB2Gray(hinhgoc));
            int Threshold = Convert.ToInt16(textBox.Text);
            for (int i = 0; i < Temp.GetLength(0); i++)
            {
                for (int j = 0; j < Temp.GetLength(1); j++)
                {
                    if (Temp[i, j] < Threshold)
                    {
                        Temp[i, j] = 0;
                    }
                    else
                    {
                        Temp[i, j] = 1;
                    }
                }
            }
            Bitmap binary_image = IntToBitmap(Temp);
            Binary_Image.Image = binary_image;
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
