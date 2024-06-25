import serial
import time

# Cấu hình cổng COM và tốc độ baud
com_port = 'COM2'  # Thay thế bằng cổng COM của bạn
baud_rate = 115200
i = 0
b = -1
# Khởi tạo kết nối serial
ser = serial.Serial(com_port, baud_rate)

def float_to_3bytes(value):
    # Chuyển đổi số thực thành 3 byte
    byte_array = bytearray(3)
    # Chia nhỏ số thực thành các byte
    byte_array[0] = int(value) & 0xFF
    byte_array[1] = (int(value) >> 8) & 0xFF
    byte_array[2] = (int(value) >> 16) & 0xFF
    return byte_array

def send_data(a):
    angleX = float_to_3bytes(500.0 + a)
    angleY = float_to_3bytes(0.0 + a)
    angleZ = float_to_3bytes(900.0 + a)
    
    # Dữ liệu cần gửi
    byte_array = bytearray(19)
    byte_array[0] = 0xAA
    byte_array[1] = 0x00
    
    byte_array[2] = angleX[0]
    byte_array[3] = angleX[1]
    byte_array[4] = angleX[2]
    
    byte_array[5] = angleY[0]
    byte_array[6] = angleY[1]
    byte_array[7] = angleY[2]
    
    byte_array[8] = angleZ[0]
    byte_array[9] = angleZ[1]
    byte_array[10] = angleZ[2]
    byte_array[11] = 0x00
    byte_array[12] = 0x00
    byte_array[13] = 0x00
    byte_array[14] = 0x00
    byte_array[15] = 0x00
    byte_array[16] = 0x00
    byte_array[17] = 0x00
    byte_array[18] = 0x00
    ser.write(byte_array)

try:
    while True:
        # i = i + 1
        # a = 10 + b * i
        # if(a >= 0):
        #     b = -1
        # else:
        #     b = 1
        send_data(0)
        time.sleep(0.2)  # Đợi 200ms
except KeyboardInterrupt:
    print("Stopping...")
finally:
    ser.close()
