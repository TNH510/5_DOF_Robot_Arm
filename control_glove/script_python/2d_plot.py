import csv
import matplotlib.pyplot as plt

# Đường dẫn đến file CSV
csv_file = 'data.csv'

# Đọc dữ liệu từ file CSV
data1 = []
data2 = []
data3 = []

with open(csv_file, 'r') as csvfile:
    reader = csv.reader(csvfile)
    for row in reader:
        if len(row) == 3:
            try:
                num1 = float(row[0])
                num2 = float(row[1])
                num3 = float(row[2])
                data1.append(num1)
                data2.append(num2)
                data3.append(num3)
            except ValueError:
                pass

# Vẽ đồ thị 2D
plt.plot(data1, label='Line 1')
plt.plot(data2, label='Line 2')
plt.plot(data3, label='Line 3')

plt.xlabel('Time')
plt.ylabel('Data')
plt.title('Data Graph')
plt.legend()

plt.show()