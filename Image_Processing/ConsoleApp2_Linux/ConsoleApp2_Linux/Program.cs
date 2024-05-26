using System;
using System.Drawing;
using System.IO;

class Program
{
    static void Main()
    {
        // Đường dẫn tới bức ảnh màu bitmap
        string imagePath = @"C:\Users\Loc\Desktop\XLA_05 (2).jpg";

        // Đọc bức ảnh màu bitmap
        Bitmap bitmap = new Bitmap(imagePath);

        // Chuyển đổi thành ảnh mức xám
        int[,] grayImage = ConvertToGrayScale(bitmap);

        // Lưu ảnh mức xám thành file dữ liệu
        string outputFilePath = @"C:\Users\Loc\Desktop\outputfile5.csv";
        SaveGrayImageToFile(grayImage, outputFilePath);

        Console.WriteLine("Đã chuyển đổi và lưu ảnh thành công!");
    }

    static int[,] ConvertToGrayScale(Bitmap bitmap)
    {
        int width = bitmap.Width;
        int height = bitmap.Height;

        int[,] grayImage = new int[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                Color pixelColor = bitmap.GetPixel(x, y);

                // Chuyển đổi màu của mỗi pixel thành giá trị mức xám
                int grayValue = (int)(pixelColor.R * 0.3 + pixelColor.G * 0.59 + pixelColor.B * 0.11);

                grayImage[x, y] = grayValue;
            }
        }

        return grayImage;
    }

    static void SaveGrayImageToFile(int[,] grayImage, string filePath)
    {
        int height = grayImage.GetLength(0);
        int width = grayImage.GetLength(1);

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            // Ghi kích thước ảnh vào file (định dạng: height,width)
            writer.WriteLine($"{height},{width}");

            // Ghi giá trị mức xám của từng pixel vào file
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    writer.Write(grayImage[y, x]);

                    // Ngăn cách giá trị bằng dấu phẩy
                    if (x < width - 1)
                        writer.Write(",");
                }
                writer.WriteLine();
            }
        }
    }
}