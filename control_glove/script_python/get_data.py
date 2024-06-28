import serial
import os
import csv
import matplotlib.pyplot as plt

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

data1 = []
data2 = []
data3 = []
timestamps = []

plt.ion()  # Bật chế độ interactive cho đồ thị

fig, ax = plt.subplots()
line1, = ax.plot(timestamps, data1, label='X')
line2, = ax.plot(timestamps, data2, label='Y')
line3, = ax.plot(timestamps, data3, label='Z')

ax.set_xlabel('Time')
ax.set_ylabel('Data')
ax.set_title('UART Data Real-time Graph')
ax.legend()

# Kiểm tra nếu tập tin data.csv đã tồn tại, thì xóa nó
if os.path.exists('data.csv'):
    os.remove('data.csv')

# Mở file CSV để ghi dữ liệu
with open('data.csv', 'w', newline='') as csvfile:
    writer = csv.writer(csvfile)

    while True:
        if uart.in_waiting > 0:
            received_data = uart.readline().decode().strip()

            # Tách các số bằng dấu phẩy và khoảng trắng, và xử lý từng số
            numbers = received_data.split(',')
            if len(numbers) == 3:
                try:
                    num1 = float(numbers[0])
                    num2 = float(numbers[1])
                    num3 = float(numbers[2])

                    data1.append(num1)
                    data2.append(num2)
                    data3.append(num3)

                    timestamps.append(len(data1))

                    line1.set_data(timestamps, data1)  # Cập nhật dữ liệu cho đường 1
                    line2.set_data(timestamps, data2)  # Cập nhật dữ liệu cho đường 2
                    line3.set_data(timestamps, data3)  # Cập nhật dữ liệu cho đường 3

                    # Tự động điều chỉnh giới hạn trục x để đồ thị cuộn
                    ax.relim()
                    ax.autoscale_view()

                    # Ghi dữ liệu vào file CSV
                    writer.writerow([num1, num2, num3])

                    plt.pause(0.07)  # Tạm dừng một chút để đồ thị được cập nhật

                    # Kiểm tra nếu đạt 200 mẫu dữ liệu, thoát khỏi vòng lặp
                    if len(data1) == 500:
                        break
                except ValueError:
                    pass