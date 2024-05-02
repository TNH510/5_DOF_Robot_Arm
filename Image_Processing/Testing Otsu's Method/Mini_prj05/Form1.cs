using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ZedGraph;

namespace Mini_prj05
{
    public partial class Form1 : Form
    {
        Bitmap hinhgoc;
        static string imagePath;
        public Form1()
        {
            InitializeComponent();
      

        }
        public Bitmap ChuyenhinhRBGsanghinhxamLuminance(Bitmap hinhgoc)
        {
            Bitmap HinhMucXam = new Bitmap(hinhgoc.Width, hinhgoc.Height);
            for (int x = 0; x < hinhgoc.Width; x++)
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //lấy điểm ảnh
                    Color pixel = hinhgoc.GetPixel(x, y);
                    byte R = pixel.R;
                    byte G = pixel.G;
                    byte B = pixel.B;

                    // Tính giá trị mức xám cho điểm (x,y)
                    byte gray = (byte)(0.2126 * R + 0.7152 * G + 0.0722 * B);
                    // gán giá trị mức xám vừa tính
                    HinhMucXam.SetPixel(x, y, Color.FromArgb(gray, gray, gray));

                }
            return HinhMucXam;
        }
        public double[] TinhHistogram(Bitmap HinhMucXam)
        {

            // khai báo mảng có 256 phần tử
            double[] histogram = new double[256];
            for (int x = 0; x < HinhMucXam.Width; x++) 
                for (int y = 0; y<HinhMucXam.Height;y++)
                {

                    Color color = HinhMucXam.GetPixel(x, y);
                    byte gray = color.R;//trong hình  mức xám giá trị kênh R cũng giống  G hoặc B
                    // giá trị gray cũng chính là  phần tử thứ gray  trong mảng histogram dã khai báo 
                    //  Sẽ Tăng số đểm của phần tử thứ gray lên 1
                    histogram[gray]++;

                }
            return histogram;

        }

        PointPairList ChuyenDoiHistogram(double[] histogram)
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
                System.Drawing.Image image = System.Drawing.Image.FromFile(imagePath);
                pic_goc.Image = image;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            hinhgoc = new Bitmap(imagePath);
            //hiển thị hình gốc
            pic_goc.Image = hinhgoc;
            //hiển thị hình xám
            Bitmap HinhMucXam = ChuyenhinhRBGsanghinhxamLuminance(hinhgoc);
            pic_xam.Image = HinhMucXam;

            // gọi hàm đã viết để chạy biểu đồ
            double[] histogram = TinhHistogram(HinhMucXam);
            PointPairList points = ChuyenDoiHistogram(histogram);
            // vẽ và hiển thị biểu đồ
            zGHistogram.GraphPane = BieuDoHistogram(points);
            zGHistogram.Refresh();
        }
    }

}
