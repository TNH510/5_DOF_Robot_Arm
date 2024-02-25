import serial
import matplotlib.pyplot as plt

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

data1 = []
data2 = []
data3 = []
timestamps = []

plt.ion()  # Bật chế độ interactive cho đồ thị

fig, ax = plt.subplots()
line1, = ax.plot(timestamps, data1, label='Data 1')
line2, = ax.plot(timestamps, data2, label='Data 2')
line3, = ax.plot(timestamps, data3, label='Data 3')

ax.set_xlabel('Time')
ax.set_ylabel('Data')
ax.set_title('UART Data Real-time Graph')
ax.legend()

while True:
    if uart.in_waiting > 0:
        received_data = uart.readline().decode().strip()
        
        # Tách các số bằng dấu phẩy và khoảng trắng, và xử lý từng số
        numbers = received_data.split(',')
        if len(numbers) == 3:
            try:
                num1 = int(numbers[0])
                num2 = int(numbers[1])
                num3 = int(numbers[2])
                
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
                
                plt.pause(0.001)  # Tạm dừng một chút để đồ thị được cập nhật
            except ValueError:
                pass