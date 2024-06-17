# Những nội dung chính
## Tổng quan về hệ thống điều khiển
Lí do chọn PLC trong việc điều khiển robot
- Tầm quan trọng của PLC trong công nghiệp
- Khả năng của PLC trong điều khiển robot.
1. **Sơ lược về Hệ thống điều khiển (Q02HCPU - Q02HCPU - Q172HCPU - MR-J[]-[]B)**
   1. **Mô hình chung cho hệ thống điều khiển**
      - Bao nhiêu bộ phận
      - Chức năng của từng bộ phận
      - Tiềm năng của hệ thống lắp đặt
      - Hạn chế của hệ thống 
   2. **Giới thiệu từng module điều khiển**
      1. **Q02HCPU**
         - Vai trò \ Chức năng
      2. **Q172HCPU**
         - Vai trò \ Chức năng
      3. **MR-J[]-[]B**
         - Vai trò \ Chức năng   
   3. **Cách lắp đặt hệ thống điều khiển**
      1. **Q02HCPU và máy tính thông qua GX Work2**
         1. **Q02HCPU:**
            - Những thành phần kết nối
            - Lưu ý trong việc lắp đặt kết nối với máy tính
         2. **Máy tính (GX Work2)**
            -  Cách kiểm tra kết nối với PLC
            -  Cách kết nối với PLC thông qua phần mềm
            -  Thiết đặt thông số trong GX Work2
         3. **Q02HCPU và QY42P:**
            - Những thành phần phần cứng kết nối
            - Lưu ý trong việc kết nối
      2. **Q172HCPU và những thành phần liên quan:**
         1. **Q172HCPU và Q02HCPU:**
            - Những thành phần phần cứng kết nối
            - Các thông số cài đặt
            - Lưu ý trong việc kết nối
         2. **Q172HCPU và Servo Motor:**
            - Những thành phần phần cứng kết nối của servo với PLC
            - Kết nối giữa servo với servo
            - Lưu ý trong việc kết nối
         3. **Q172HCPU và máy tính thông qua MT Developer:**
            -  Cách kiểm tra kết nối với PLC
            -  Cách kết nối với PLC thông qua phần mềm
            -  Thiết đặt thông số trong GX Work2
2. **Kiểm tra chức năng bằng phần mềm MT Developer và GX Work2**
   1. **MT Developer**
      - Giới thiệu chức năng giám sát và điều khiển trên phần mềm
      - Thực hiện chạy jog thử nghiệm cho từng servo motor
      - Kết luận thực nghiệm
   2. **GX Work2**
      - Giới thiệu chức năng giám sát và điều khiển trên phần mềm
      - Thực hiện chạy jog thử nghiệm cho từng servo motor
      - Kết luận thực nghiệm

## Viết chương trình điều khiển cho robot
1. **Chương trình trên GX Work2**
   1. Giới thiệu cấu trúc chương trình
   2. Cấu trúc vùng nhớ
   3. Các hàm cơ bản
   4. Chương trình liên kết giữa GX Work2 và MT Developer2
2. **Chương trình trên MT Developer2**
   1. Giới thiệu cấu trúc chương trình
   2. Cấu trúc vùng nhớ
   3. Các hàm cơ bản
3. **Giới thiệu tổng quan về C# và Q02HCPU**
   1. Cách cấu hình thông qua phần mềm Communication Setup Utility
   2. Thư viện hỗ trợ
   3. Chức năng cơ bản
   4. Các hàm cơ bản

## Giới thiệu về giao diện WPF trong C#
1. **Sơ lược về WPF**
2. **Ứng dụng của WPF**
3. **Làm việc với giao diện điều khiển**
   - Điều khiển trên giao diện
   - Cách cài đặt thư viện.
4. **Làm việc với code**
   - Giới thiệu về Timer.
   - Giới thiệu về Luồng (Thread).
   - Giới thiệu sơ lược về cấu trúc chương trình.
     - Cấu trúc quản lí code.
     - Các kiểu khai báo dữ liệu.