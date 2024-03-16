using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Features2D;
using Emgu.CV.Structure;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap Import_picture;
        Bitmap Output;

        string link_picture = @"C:\Users\Loc\Desktop\XL123.jpg";
        public Form1()
        {
            Import_picture = new Bitmap(link_picture);
            
            InitializeComponent();
            Original_picture.Image = RBGtoGray(Import_picture);
        }
        public Bitmap RBGtoGray(Bitmap Import_picture)
        {
            Bitmap Gray_picture = new Bitmap(Import_picture.Width, Import_picture.Height);
            for (int x = 0; x < Import_picture.Width; x++)
                for (int y = 0; y < Import_picture.Height; y++)
                { 
                    Color pixel = Import_picture.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    Gray_picture.SetPixel(x, y, Color.FromArgb(gray, gray, gray));
                }    
            return Gray_picture;
        }
        public Bitmap ColorImageSharpening(Bitmap hinhgoc)
        {
            int[,] c = { { 0, -1, 0 }, { -1, 4, -1 }, { 0, -1, 0 } };
            Bitmap imgsharpe = new Bitmap(hinhgoc.Width, hinhgoc.Height);

            // dùng vòng for để đọc điểm ảnh ở dạng 2 chiều, bỏ viền ngoài của ảnh vì là mặt nạ 3x3
            for (int x = 1; x < hinhgoc.Width - 1; x++)
                for (int y = 1; y < hinhgoc.Height - 1; y++)
                {
                    //các biến cộng dồn giá trị điểm ảnh
                    int Rs = 0, Gs = 0, Bs = 0;
                    int SharpR = 0, SharpG = 0, SharpB = 0;
                    //quét các điểm trong mặt nạ
                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            Color color = hinhgoc.GetPixel(i, j);
                            int R = color.R; int G = color.G; int B = color.B;
                            // nhân ma trận điểm ảnh với hệ số C
                            Rs += R * c[i - x + 1, j - y + 1];
                            Gs += G * c[i - x + 1, j - y + 1];
                            Bs += B * c[i - x + 1, j - y + 1];

                        }
                    //tính điểm sắc nét
                    Color color1 = hinhgoc.GetPixel(x, y);
                    int R1 = color1.R; int G1 = color1.G; int B1 = color1.B;

                    SharpR = R1 + Rs; SharpG = G1 + Gs; SharpB = B1 + Bs;

                    if (SharpR < 0)
                        SharpR = 0;
                    else if (SharpR > 255)
                        SharpR = 255;

                    if (SharpG < 0)
                        SharpG = 0;
                    else if (SharpG > 255)
                        SharpG = 255;

                    if (SharpB < 0)
                        SharpB = 0;
                    else if (SharpB > 255)
                        SharpB = 255;
                    //set các điểm ảnh vào biến
                    imgsharpe.SetPixel(x, y, Color.FromArgb(SharpR, SharpG, SharpB));


                }
            return imgsharpe;
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
        private void button1_Click(object sender, EventArgs e)
        {
            Output = RBGtoGray(Import_picture);
            Processed_picture.Image = ColorImageBorderline(ColorImageSmoothing(RBGtoGray(Import_picture),4,9)); 
            Output.Save(@"C:\Users\Loc\Desktop\XL.jpg");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
