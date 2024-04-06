import serial
import matplotlib.pyplot as plt

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

data1 = []
data2 = []
data3 = []
data4 = []
timestamps = []

plt.ion()  # Bật chế độ interactive cho đồ thị
fig = plt.figure()
ax = fig.add_subplot()

scatter = ax.scatter(data1, data2, data3)

ax.set_xlabel('Data 0')
ax.set_ylabel('Data 1')
ax.set_title('UART Data Real-time 3D Graph')

while True:
    if uart.in_waiting > 0:
        received_data = uart.readline().decode().strip()

        # Tách các số bằng dấu phẩy và khoảng trắng, và xử lý từng số
        numbers = received_data.split(',')
        if len(numbers) == 3:
            try:
                num1 = float(numbers[0])
                num2 = float(numbers[1])
                # num3 = float(numbers[2])

                data1.append(num1)
                data2.append(num2)
                # data3.append(num3)

                # timestamps.append(len(data1))

                # Cập nhật dữ liệu cho scatter plot
                # ax.scatter(num1, num2, color='r')
                ax.plot(data1, data2, color='r')

                # Tự động điều chỉnh giới hạn trục x để đồ thị cuộn
                # ax.relim()
                ax.autoscale_view()

                plt.pause(0.01)  # Tạm dừng một chút để đồ thị được cập nhật
            except ValueError:
                pass