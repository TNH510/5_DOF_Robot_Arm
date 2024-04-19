import serial
import matplotlib.pyplot as plt

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

data1 = []
data2 = []
data3 = []
data4 = []
timestamps = []

plt.ion()  # Bật chế độ interactive cho đồ thị

fig, ax = plt.subplots()
line1, = ax.plot(timestamps, data1, label='Data 1')
line2, = ax.plot(timestamps, data2, label='Data 2')
line3, = ax.plot(timestamps, data3, label='Data 3')
# line4, = ax.plot(timestamps, data4, label='Data 4')

ax.set_xlabel('Time')
ax.set_ylabel('Data')
ax.set_title('UART Data Real-time Graph')
ax.legend()

while True:
    if uart.in_waiting > 10:
        dataBytes = uart.read(uart.in_waiting)

        # q0
        byte_1 = dataBytes[0]
        byte_2 = dataBytes[1]

        # q1
        byte_3 = dataBytes[2]
        byte_4 = dataBytes[3]

        # q2
        byte_5 = dataBytes[4]
        byte_6 = dataBytes[5]

        # q3
        byte_7 = dataBytes[6]
        byte_8 = dataBytes[7]

        # elbow_angle
        byte_9 = dataBytes[8]
        byte_10 = dataBytes[9]

        # Convert to actual value
        q0 = (byte_1 << 8) | byte_2
        if q0 <= 9999:
            q0 = q0 / 10000
        else:
            q0 = (q0 - 10000) * (-1) / 10000

        q1 = (byte_3 << 8) | byte_4
        if q1 <= 9999:
            q1 = q1 / 10000
        else:
            q1 = (q1 - 10000) * (-1) / 10000

        q2 = (byte_5 << 8) | byte_6
        if q2 <= 9999:
            q2 = q2 / 10000
        else:
            q2 = (q2 - 10000) * (-1) / 10000

        q3 = (byte_7 << 8) | byte_8
        if q3 <= 9999:
            q3 = q3 / 10000
        else:
            q3 = (q3 - 10000) * (-1) / 10000

        elbow = (byte_9 << 8) | byte_10
        elbow = elbow / 10  # rad

        try:
            num1 = float(q1)
            num2 = float(q2)
            num3 = float(q3)
            # num4 = float(numbers[3])

            data1.append(num1)
            data2.append(num2)
            data3.append(num3)
            # data4.append(num4)

            timestamps.append(len(data1))

            line1.set_data(timestamps, data1)  # Cập nhật dữ liệu cho đường 1
            line2.set_data(timestamps, data2)  # Cập nhật dữ liệu cho đường 2
            line3.set_data(timestamps, data3)  # Cập nhật dữ liệu cho đường 3
            # line4.set_data(timestamps, data4)  # Cập nhật dữ liệu cho đường 4

            # Tự động điều chỉnh giới hạn trục x để đồ thị cuộn
            ax.relim()
            ax.autoscale_view()

            plt.pause(0.01)  # Tạm dừng một chút để đồ thị được cập nhật
        except ValueError:
            pass