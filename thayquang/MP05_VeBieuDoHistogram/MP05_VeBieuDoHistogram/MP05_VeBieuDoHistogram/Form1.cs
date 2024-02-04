//code
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.Util;
using ZedGraph;
using System.Drawing.Imaging;



namespace MP03_ChuyenAnhMauRGBsangGrayscale
{
    public partial class Form1 : Form
    {

        // Create a socket object
        Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        string response_client;
        public Form1()
        {
            InitializeComponent();

            //Goi cac ham de ve bieu do histogram
            //=================================================
            }

        //Khai bao ham tinh toan muc xam theo phuong phap Average
        public Image<Bgr, byte> HinhxamAverage(Image<Bgr, byte> hinhgoc)
        {
            Image<Bgr, byte> Hinhmucxam = new Image<Bgr, byte>(hinhgoc.Width, hinhgoc.Height);

            for (int x = 0; x < hinhgoc.Width; x++)
            {
                for (int y = 0; y < hinhgoc.Height; y++)
                {
                    //Lay diem anh
                    Bgr pixelValue = hinhgoc[y, x];
                    byte R = (byte)pixelValue.Red;
                    byte G = (byte)pixelValue.Green;
                    byte B = (byte)pixelValue.Blue;

                    //Tinh gia tri muc xam
                    byte gray = (byte)((R + G + B) / 3);

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[y, x, 2] = gray;
                    Hinhmucxam.Data[y, x, 1] = gray;
                    Hinhmucxam.Data[y, x, 0] = gray;

                }
            }
            return Hinhmucxam;
        }

        //Tinh histogram cua anh xam
        public double[] TinhHistogram(Image <Bgr, byte> Hinhmucxam)
        {
            //Moi pixel muc xam co gia tri tu 0 den 255, do vay ta khai bao mot mang
            //co 256 phan tu dung de chua so dem cua cac pixels co cung muc xam trong anh
            //Dung kieu double vi tong so dem rat lon va phu thuoc vao kich thuoc cua anh
            double[] histogram = new double[256];

            for (int x = 0; x < Hinhmucxam.Width; x++)
                for (int y = 0; y < Hinhmucxam.Height; y++)
                {
                    Bgr pixelValue = Hinhmucxam[y, x];
                    byte gray = (byte)pixelValue.Red;
                    //Lay red vi trong hinh xam red = green = blue

                    //Gia tri gray cung la phan tu thu gray trong mang histogram
                    //da khai bao. Se tang so dem cua phan tu gray len 1
                    histogram[gray]++;
          
                }
            return histogram;//So luong diem anh co cung gia tri muc xam


        }
        //Chuyen du lieu tu dang ma tran sang kieu du lieu cua zedgragp
        PointPairList ChuyendoiHistogram(double[] histogram) 
        {
            //PointPairList la kieu du lieu cua Zedgraph de ve bieu do
            PointPairList points = new PointPairList();

            for (int i = 0; i < histogram.Length; i++)
            {   
                //i tuong ung truc nam ngang tu 0 - 255
                //Histogram tuong ung truc dung, la so pixel cung 
                points.Add(i, histogram[i]);

                
            }
            return points;

        }

        //Thiet lap mot bieu do trong Zedgraph
        public GraphPane BieudoHistogram(PointPairList histogram)
        {
            //GraphPane la doi tuong trong Zedgraph
            GraphPane gp = new GraphPane();

            gp.Title.Text = @"Biểu đồ Histogram";
            gp.Rect = new Rectangle(0, 0, 640, 480);//Khung chua bieu do

            //Thiet lap truc ngang
            gp.XAxis.Title.Text = @"Giá trị mức xám của các điểm ảnh";
            gp.XAxis.Scale.Min = 0; // nho nhat la 0
            gp.XAxis.Scale.Max = 255; // lon nhat la 255
            gp.XAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.XAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Tuong tu thiet lap cho truc dung
            gp.YAxis.Title.Text = @"Số điểm ảnh có cùng mức xám";
            gp.YAxis.Scale.Min = 0; // nho nhat la 0
            gp.YAxis.Scale.Max = 15000; // So nay phai lon hon kich thuoc anh
            gp.YAxis.Scale.MajorStep = 5;// Moi buoc chinh la 5
            gp.YAxis.Scale.MinorStep = 1; //Moi buoc trong mot buoc chinh la 1

            //Dung bieu do dang bar de bieu dien histgram
            gp.AddBar("Histogram", histogram, Color.OrangeRed);
            return gp;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void zedGraph_Load(object sender, EventArgs e)
        {

        }

        private void Connect_btn_Click(object sender, EventArgs e)
        {
            Connect_btn.Enabled = false;
            Disconnect_btn.Enabled = true;
            // Connect to the server
            string host = "192.168.0.49";
            int port = Convert.ToInt16("2011");

            try
            {
                clientSocket.Connect(host, port);
                Console.WriteLine("Connected");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to connect");
            }
        }

        private void Image_btn_Click(object sender, EventArgs e)
        {
            // Perform your desired action here
            string filepathtosave = @"C:\Users\daveb\Desktop\raw_data\Image\";
            // Perform your desired action here
            string CaptureImageMessage = "1003t\r\n";
            byte[] CaptureImageBytes = Encoding.ASCII.GetBytes(CaptureImageMessage);
            clientSocket.Send(CaptureImageBytes);
            // Receive the response from the server
            var buffer = new byte[308295];
            int bytesRead = clientSocket.Receive(buffer);

            System.Threading.Thread.Sleep(500); // Simulating a 2-second delay

            string RequestImageMessage = "1003I?\r\n";
            // Send the command to the server
            byte[] RequestImageBytes = Encoding.ASCII.GetBytes(RequestImageMessage);
            clientSocket.Send(RequestImageBytes);

            string sentencetosend = "1003I?\r\n";

            // Receive the response from the server
            buffer = new byte[308295];
            bytesRead = clientSocket.Receive(buffer);

            if (RequestImageMessage == sentencetosend)
            {
                response_client = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                //PrintLog("", "", response_client);
                //Console.WriteLine(response_client);
                while (bytesRead < 308291)
                {
                    bytesRead += clientSocket.Receive(buffer, bytesRead, 308291 - bytesRead, SocketFlags.None);
                }
            }

            response_client = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            //PrintLog("Data received", "", response_client);

            // Sentences to remove
            string[] sentencesToRemove = { "1003000308278", "1003*", "1003?", "1003000307200" };
            //Console.WriteLine("hello");
            // Loop through each sentence and replace it with an empty string
            foreach (string sentence in sentencesToRemove)
            {
                response_client = response_client.Replace(sentence, "");
            }

            // Convert the modified response string back to bytes
            byte[] modifiedBuffer = Encoding.ASCII.GetBytes(response_client);
            // Convert each byte to its string representation
            string[] byteStrings = new string[modifiedBuffer.Length];
            for (int i = 0; i < modifiedBuffer.Length; i++)
            {
                byteStrings[i] = modifiedBuffer[i].ToString();
            }
            // Join the string representations of bytes
            string bmpString = string.Join(" ", byteStrings);
            // Split the byte string into individual byte values
            string[] byteValues = bmpString.Split();

            // Convert each byte value from string to integer
            List<byte> byteData = new List<byte>();


            foreach (string byteString in byteValues)
            {
                byteData.Add(Convert.ToByte(byteString));
            }
            
            // Define the number of bytes to delete from the beginning

            // Bitmap Header(14 bytes) + Bitmap Information (40 bytes) + Color Palette (4 * 256) = 1078 bytes to delete

            // The kinds of image format (RAW or bitmap) is based on the configuration on E2D200.exe
            int bytesToDelete = 1078; // Adjust this number according to your requirement

            // Delete the specified number of bytes from the beginning
            byteData.RemoveRange(0, bytesToDelete);

            // Convert the list of bytes back to byte array
            byte[] byteArrayModified = byteData.ToArray();

            // Determine the dimensions of the original image
            int width = 640;  // Adjust according to your image width
            int height = 480;  // Adjust according to your image height

            // Calculate the new dimensions of the image after removing bytes
            int newWidth = width;  // Since bytes removed from the beginning don't affect width
            int newHeight = height;  // Adjust height accordingly

            // convert from 1-D array to 2-D array
            byte[,] byteArray2D = new byte[newHeight, newWidth];
            byteArray2D = ConvertTo2DArray(byteArrayModified, newHeight, newWidth);

            Image<Bgr, byte> Hinhmucxam = new Image<Bgr, byte>(newWidth, newHeight);


            for (int x = 0; x < newWidth; x++)
            {
                for (int y = 0; y < newHeight; y++)
                {

                    //Gan gia tri muc xam vua tinh duoc vao hinh muc xam
                    Hinhmucxam.Data[y, x, 2] = byteArray2D[y, x];
                    Hinhmucxam.Data[y, x, 1] = byteArray2D[y, x];
                    Hinhmucxam.Data[y, x, 0] = byteArray2D[y, x];
                }
            }
            //Hien thi hinh goc trong picBox_Hinhgoc da tao

            imageBox_Hinhgoc.Image = Hinhmucxam;

            //Tinh histogram
            double[] histogram = TinhHistogram(Hinhmucxam);

            //Chuyen doi kieu du lieu
            PointPairList points = ChuyendoiHistogram(histogram);

            //Ve bieu do histogram va cho hien thi
            zedGraphHistogram.GraphPane = BieudoHistogram(points);
            zedGraphHistogram.Width = 640;
            zedGraphHistogram.Width = 480;
            zedGraphHistogram.Refresh();


        }

        public static byte[,] ConvertTo2DArray(byte[] array1D, int rows, int columns)
        {
            byte[,] array2D = new byte[rows, columns];
            int index = 0;

            // Iterate over the elements of the 1D array and assign them to the 2D array
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = 0; j < columns; j++)
                {
                    // Check if there are enough elements in the 1D array
                    if (index < array1D.Length)
                    {
                        array2D[i, j] = array1D[index];
                        index++;
                    }
                    else
                    {
                        // If there are not enough elements, you may handle this case according to your requirements
                        // For example, you can fill remaining elements with default values or throw an exception
                        // Here, I'm filling remaining elements with 0
                        array2D[i, j] = 0;
                    }
                }
            }
            return array2D;
        }

        private void Disconnect_btn_Click(object sender, EventArgs e)
        {
            Connect_btn.Enabled = true;
            Disconnect_btn.Enabled = false;
            try
            {
                clientSocket.Close();
                Console.WriteLine("Disconnected");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to disconnect");
            }
        }
    }
}
