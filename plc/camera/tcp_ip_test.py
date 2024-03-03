import socket

def connect_to_device(ip, port):
    try:
        # Tạo một socket TCP/IP
        sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
        
        # Kết nối tới địa chỉ IP và cổng
        sock.connect((ip, port))
        
        # In ra thông tin kết nối thành công
        print(f"Kết nối thành công tới {ip}:{port}")
        
        # Gửi lệnh tới thiết bị
        command = "1002D?\r\n"
        sock.sendall(command.encode())
        
        # Nhận phản hồi từ thiết bị
        response = sock.recv(1024).decode()
        
        # In ra phản hồi
        print("Phản hồi từ thiết bị:")
        print(response)
        
        # Đóng kết nối
        sock.close()
        print("Đã đóng kết nối")
        
    except socket.error as e:
        print(f"Lỗi kết nối: {str(e)}")

# Gọi hàm connect_to_device với địa chỉ IP và cổng tương ứng
connect_to_device("192.168.0.49", 50010)