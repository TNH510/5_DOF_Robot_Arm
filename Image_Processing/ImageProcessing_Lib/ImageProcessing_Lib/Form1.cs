using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;



namespace ImageProcessing_Lib
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }
        private Bitmap ConvertMatToBitmap(Mat image)
        {
            // Tạo một Bitmap với kích thước và định dạng phù hợp
            Bitmap bitmap = new Bitmap(image.Width, image.Height, PixelFormat.Format24bppRgb);

            // Chuyển dữ liệu từ Mat sang mảng byte[]
            byte[] imageData = new byte[image.Width * image.Height * 3]; // *3 là do dữ liệu ảnh màu có 3 kênh
            Marshal.Copy(image.DataPointer, imageData, 0, imageData.Length);

            // Sao chép dữ liệu từ mảng byte[] vào Bitmap
            BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.WriteOnly, bitmap.PixelFormat);
            Marshal.Copy(imageData, 0, bitmapData.Scan0, imageData.Length);
            bitmap.UnlockBits(bitmapData);

            return bitmap;
        }
        private void ProcessImage(string imagePath)
        {
            Mat image = CvInvoke.Imread(imagePath, ImreadModes.Color);

            if (image != null)
            {
                Mat grayImage = new Mat();
                CvInvoke.CvtColor(image, grayImage, ColorConversion.Bgr2Gray);

                Mat cannyEdges = new Mat();
                CvInvoke.Canny(grayImage, cannyEdges, 100, 200);

                // Chuyển đổi từ Mat sang Bitmap trước khi gán cho PictureBox
                Bitmap bitmap = ConvertMatToBitmap(cannyEdges);
                pictureBox1.Image = bitmap;
            }
            else
            {
                MessageBox.Show("Không thể mở ảnh", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void imageBox_Click(object sender, EventArgs e)
        {

        }

        private void OpenImageButton_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string selectedImagePath = openFileDialog.FileName;
                ProcessImage(selectedImagePath);
            }
        }
    }
}
