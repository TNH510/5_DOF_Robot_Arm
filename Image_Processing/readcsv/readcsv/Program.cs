string filePath = @"C:\Users\Loc\Desktop\outputfile.csv";
string[] lines = File.ReadAllLines(filePath);

int rowCount = lines.Length;
int columnCount = lines[1].Split(',').Length;

int[,] array2D = new int[rowCount, columnCount];

for (int i = 1; i < rowCount; i++)
{
    string[] fields = lines[i].Split(',');

    for (int j = 1; j < columnCount; j++)
    {
        if (int.TryParse(fields[j], out int value))
        {
            array2D[i, j] = value;
        }
        else
        {
            // Xử lý lỗi định dạng không hợp lệ nếu cần thiết
        }
    }
}

// Sử dụng mảng 2 chiều `array2D` đã gán giá trị từ file CSV
// ...