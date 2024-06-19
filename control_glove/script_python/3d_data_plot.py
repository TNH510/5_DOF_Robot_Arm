import csv
from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt

# Đường dẫn đến file CSV
csv_file = 'C:/Users/daveb/Desktop/5_DOF_Robot_Arm/matlab/test.csv'

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

# Vẽ đồ thị 3D
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

ax.scatter(data1, data2, data3)

ax.set_xlabel('num1')
ax.set_ylabel('num2')
ax.set_zlabel('num3')

plt.title('Data Visualization')

plt.show()