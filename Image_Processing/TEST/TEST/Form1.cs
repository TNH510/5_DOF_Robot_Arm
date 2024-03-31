using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Imaging;
using AForge.Imaging.Filters;

namespace TEST
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;
        private Bitmap linkedImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                pictureBoxOriginal.Image = originalImage;
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap grayImage = ToGrayscale(originalImage);
                Bitmap blurImage = ApplyGaussianBlur(grayImage, 2);
                Bitmap edgeImage = ApplyCannyEdgeDetection(blurImage);
                linkedImage = LinkEdges(edgeImage);
                pictureBoxLinked.Image = linkedImage;
            }
            else
            {
                MessageBox.Show("Please open an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private Bitmap ToGrayscale(Bitmap image)
        {
            Bitmap grayImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);
                    int grayValue = (int)(pixel.R * 0.299 + pixel.G * 0.587 + pixel.B * 0.114);
                    grayImage.SetPixel(x, y, Color.FromArgb(grayValue, grayValue, grayValue));
                }
            }

            return grayImage;
        }

        private Bitmap ApplyGaussianBlur(Bitmap image, double sigma)
        {
            AForge.Imaging.Filters.GaussianBlur filter = new AForge.Imaging.Filters.GaussianBlur(sigma);
            return filter.Apply(image);
        }

        private Bitmap ApplyCannyEdgeDetection(Bitmap image)
        {
            Bitmap convertedBitmap = image.Clone(new Rectangle(0, 0, image.Width, image.Height), System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            AForge.Imaging.Filters.CannyEdgeDetector filter = new AForge.Imaging.Filters.CannyEdgeDetector();
            
            return filter.Apply(convertedBitmap);
        }

        private Bitmap LinkEdges(Bitmap image)
        {
            Bitmap linkedImage = new Bitmap(image.Width, image.Height);

            for (int y = 0; y < image.Height; y++)
            {
                for (int x = 0; x < image.Width; x++)
                {
                    Color pixel = image.GetPixel(x, y);

                    if (pixel.R == 255)
                    {
                        List<Point> edgePoints = new List<Point>();
                        FollowEdge(image, x, y, edgePoints);

                        foreach (Point point in edgePoints)
                        {
                            linkedImage.SetPixel(point.X, point.Y, Color.White);
                        }
                    }
                }
            }

            return linkedImage;
        }

        private void FollowEdge(Bitmap image, int x, int y, List<Point> edgePoints)
        {
            if (x < 0 || x >= image.Width || y < 0 || y >= image.Height)
            {
                return;
            }

            Color pixel = image.GetPixel(x, y);

            if (pixel.R == 255)
            {
                image.SetPixel(x, y, Color.Black);
                edgePoints.Add(new Point(x, y));

                FollowEdge(image, x - 1, y, edgePoints);
                FollowEdge(image, x + 1, y, edgePoints);
                FollowEdge(image, x, y - 1, edgePoints);
                FollowEdge(image, x, y + 1, edgePoints);
            }
        }

        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                originalImage = new Bitmap(openFileDialog.FileName);
                pictureBoxOriginal.Image = originalImage;
            }
        }

        private void btnProcess_Click_1(object sender, EventArgs e)
        {
            if (originalImage != null)
            {
                Bitmap grayImage = ToGrayscale(originalImage);
                Bitmap blurImage = ApplyGaussianBlur(grayImage, 2);
                Bitmap edgeImage = ApplyCannyEdgeDetection(blurImage);
                linkedImage = LinkEdges(edgeImage);
                pictureBoxLinked.Image = linkedImage;
            }
            else
            {
                MessageBox.Show("Please open an image first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
