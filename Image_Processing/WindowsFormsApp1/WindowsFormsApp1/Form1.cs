using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV.Structure;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        Bitmap Import_picture;
        Bitmap Gray_picture; 
        Bitmap Sharp_picture;
        string link_picture = @"C:\Users\Loc\Desktop\XLA.jpg";
        public Form1()
        {
            Import_picture = new Bitmap(link_picture);
            InitializeComponent();
        }
        public Bitmap RBGtoGray(Bitmap Import_picture)
        {
            Gray_picture = new Bitmap(Import_picture.Width, Import_picture.Height);
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
        public Bitmap Sharppicture(Bitmap Gray_picture)
        {
            int[,] mash = { { 0, -1, 0 }, { -1, -4, -1 }, { 0, -1, 0 } };
            Sharp_picture = new Bitmap(Gray_picture.Width, Gray_picture.Height);
            for (int x = 1; x < Gray_picture.Width - 1; x++)
                for (int y = 1; y < Gray_picture.Height-1; y++)
                {
                    //các biến cộng dồn giá trị điểm ảnh
                    int Rs = 0;
                    int SharpR = 0;
                    //quét các điểm trong mặt nạ
                    //các biến cộng dồn giá trị điểm ảnh
                    for (int i = x - 1; i <= x + 1; i++)
                        for (int j = y - 1; j <= y + 1; j++)
                        {
                            Color color = Gray_picture.GetPixel(i, j);
                            int R = color.R;
                            // nhân ma trận điểm ảnh với hệ số C
                            Rs += R * mash[i - x + 1, j - y + 1];

                        }
                    Color color1 = Gray_picture.GetPixel(x, y);
                    int R1 = color1.R; 

                    SharpR = R1 + Rs; 

                    if (SharpR < 0)
                        SharpR = 0;
                    else if (SharpR > 255)
                        SharpR = 255;
                    Sharp_picture.SetPixel(x, y, Color.FromArgb(SharpR, SharpR, SharpR));
                }
            return Sharp_picture;
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
            private void button1_Click(object sender, EventArgs e)
        {
            Original_picture.Image = RBGtoGray(Import_picture);
            Processed_picture.Image = ColorImageSharpening(RBGtoGray(Import_picture));
        }
    }
}
