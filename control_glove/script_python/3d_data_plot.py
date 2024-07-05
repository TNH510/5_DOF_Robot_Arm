import csv
from mpl_toolkits.mplot3d import Axes3D
import matplotlib.pyplot as plt

# Đường dẫn đến file CSV
csv_file = 'H:/OneDrive - hcmute.edu.vn/Desktop/5_DOF_Robot_Arm/lap_lai_quy_dao_lien_tuc.csv'

# Đọc dữ liệu từ file CSV
data1 = []
data2 = []
data3 = []

with open(csv_file, 'r') as csvfile:
    reader = csv.reader(csvfile)
    for row in reader:
        if len(row) == 3:
            try:
                # num1 = float(row[0]) * 21
                # num2 = float(row[1]) * 21
                # num3 = float(row[2]) * 15 + 650
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

ax.set_xlabel('x (cm)')
ax.set_ylabel('y (cm)')
ax.set_zlabel('z (cm)')

plt.title('Các điểm di chuyển của thiết bị trong không gian')

plt.show()