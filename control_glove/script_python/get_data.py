import serial
import matplotlib.pyplot as plt

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

data = []
timestamps = []

plt.ion()  # Bật chế độ interactive cho đồ thị

fig, ax = plt.subplots()
line, = ax.plot(timestamps, data)

ax.set_xlabel('Time')
ax.set_ylabel('Data')
ax.set_title('UART Data Real-time Graph')

while True:
    if uart.in_waiting > 0:
        received_data = uart.readline().decode().strip()
        
        # Tách các số bằng "\r\n" và xử lý từng số
        numbers = received_data.split("\r\n")
        for number in numbers:
            try:
                num = float(number)
                data.append(num)
                timestamps.append(len(data))
                
                line.set_data(timestamps, data)  # Cập nhật dữ liệu đồ thị
                
                # Tự động điều chỉnh giới hạn trục x để đồ thị cuộn
                ax.relim()
                ax.autoscale_view()
                
                plt.pause(0.001)  # Tạm dừng một chút để đồ thị được cập nhật
            except ValueError:
                pass