import serial

def merge_bytes(byte1, byte2, byte3):
    merged_value = (byte1 << 16) | (byte2 << 8) | byte3
    print(hex(merged_value))
    if merged_value > 0x800000:
        merged_value = merged_value - 0x800000  # Xóa bit số 32 (bit dấu)
        print(hex(merged_value))
        merged_value = merged_value * (-1)
        print(int(merged_value))
    return merged_value

uart = serial.Serial('COM20', 115200)  # Thay đổi cổng UART và baudrate tương ứng

while True:
    if uart.in_waiting == 19:  # Đảm bảo có đủ 12 byte trong buffer
        data = uart.read(19)  # Đọc 12 byte từ UART

        # x_pos = merge_bytes(data[2], data[3], data[4])
        y_pos = merge_bytes(data[2], data[3], data[4])
        # z_pos = merge_bytes(data[8], data[9], data[10])
        # print(hex(data[5]), hex(data[6]), hex(data[7]))
        print(hex(data[2]), hex(data[3]), hex(data[4]))
        # print(float(x_pos / 10000), float(y_pos / 10000), float(z_pos / 10000))