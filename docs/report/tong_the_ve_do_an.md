# Tổng quan về đồ án

## Tên đề tài: `Nghiên cứu phát triển tay máy 5 bậc tự do ứng dụng trong bốc xếp`

## Các mục tiêu, giới hạn của đề tài
+ Mục tiêu chung: Nghiên cứu, thiết kế, điều khiển cánh tay robot 5 bậc tự do để sắp xếp, phân loại các thùng hình hộp chữ nhật đựng sản phẩm theo kích thước khác nhau.

+ Mục tiêu cụ thể trong khuôn khổ đồ án Cơ điện tử:
  + Về mặt lí thuyết:
    + Tính toán động học vị trí:
      + Động học thuận (Xác định được vị trí đầu tay gắp cuối, không gian hoạt động)
      + Động học nghịch (Điều khiển được vị trí tay gắp)
    + Tính toán động học vận tốc
      + Jacobi thuận (Xác định được vận tốc của đầu tay gắp)
      + Jacobi nghịch (Điều khiển được vận tốc tay gắp)
    + Tính toán động lực học
      + Động lực học thuận (xác định moment của đầu tay gắp) (xác định được khối lượng vật lớn nhất có thể chịu được)
      + Động lực học nghịch (Điều khiển được lực của đầu tay gắp) (xác định được moment lớn nhất mà động cơ phải chịu)
    + Tính toán quỹ đạo chuyển động cho đầu tay gắp
      + Dùng phương pháp quỹ đạo bậc 3 (Vị trí, vận tốc, gia tốc) tính quỹ đạo cho: Đường thẳng, đường tròn
    + Tính toán cho đầu tay gắp
      + Tính toán góc quay cho cơ cấu tay quay, con trượt
      + Tính toán lực kẹp phù hợp thỏa mãn ràng buộc đầu ra
      + Tính toán chọn động cơ phù hợp
      + Tính toán xung PWM để đạt được lực kẹp mong muốn
  + Về mặt mô phỏng:
    + Mô phỏng MATLAB:
      + Mô phỏng vị trí
      + Mô phỏng vận tốc (nếu được)
      + Vẽ đồ thị vị trí 
      + Vẽ đồ thị vận tốc
      + Vẽ đồ thị moment
    + Mô phỏng SOLID (Nếu có thể)
      + Mô phỏng độ biến dạng của hộp khi gắp?
  + Về mặt thực tế:
    + Giao diện C#
      + Điều khiển về HOME cho các khớp
      + Điều khiển jogging cho từng khớp
      + Điều khiển được vị trí đầu tay gắp
      + Điều khiển theo quỹ đạo (đường thẳng, đường tròn) (chỉ vị trí) cho đầu tay gắp
      + Thể hiện được vị trí robot thực tế trên giao diện.
  + Về mặt bản vẽ
    + Bản vẽ cơ khí
      + Bản vẽ chi tiết 2D cho 5 khung cơ khí của 5 khâu của tay gắp (Cùng với các chi tiết không tiêu chuẩn đi kèm)
      + Bản vẽ 2D cho các chi tiết trong đầu tay gắp
      + Bản vẽ lắp cho toàn bộ robot
      + Bản vẽ điện cho PLC
+ Các ràng buộc giới hạn về đầu ra:
  + Vật gắp:
    + Hình dạng: Hình hộp chữ nhật
    + Vật liệu: Hộp bìa giấy
    + Kích thước: Có ít nhất chiều rộng hoặc chiều dài không quá 140 mm, 2 chiều còn lại không quá 300 mm
    + Khối lượng: Không quá ... Kg
    + Độ chính xác vị trí gắp: +/- 5 mm
    + Năng suất gắp tối đa:
    + Lực gắp tối đa mà không làm biến dạng hộp:
  + Băng tải:
    + Khoảng cách 3 trục của robot so với băng tải:
    + Kích thước:
  
+ Các ràng buộc giới hạn về đầu vào:
  + Không gian hoạt động:
    + Góc khớp 1: 85.0 -> 295.0 (degrees)
    + Góc khớp 2: 120.0 -> 200.0 (degrees)
    + Góc khớp 3: 150.0 -> 260.0 (degrees)
    + Góc khớp 4: 165.0 -> 275.0 (degrees)
    + Góc khớp 5: 0.0 -> 359.9 (degrees)
    + Khoảng cách l1: 690.0 (mm)
    + Khoảng cách l2: 440.0 (mm)
    + Khoảng cách l3: 500.0 (mm)
    + Khoảng cách l4: 0.0 (mm)
    + Khoảng cách l5: ... (mm)
    + Miền hoạt động trong không gian:
  + Khối lượng các khâu:
    + Khối lượng riêng vật liệu: ... Kg/m^3
    + Khâu 1: 20.0 ~ 27.5 (Kg)
    + Khâu 2: 21.0 ~ 24.0 (Kg)
    + Khâu 3: 19.0 ~ 25.1 (Kg)
    + Khâu 4: 2.0 ~ 3.0 (Kg)
    + Khâu 5: ~ 0.5 (Kg)
  + Moment quán tính các khâu:
    + Khâu 1: Ixx ~ 461147 Nmm^2, Iyy ~ 299920 Nmm^2
    + Khâu 2: Ixx ~ 0 Nmm^2, Iyy ~ 803322 Nmm^2
    + Khâu 3: Ixx ~ 0 Nmm^2, Iyy ~ 1604504 Nmm^2
    + Khâu 4: Ixx ~ 0 Nmm^2, Iyy ~ 6328 Nmm^2
    + Khâu 5: Ixx ~ ... Nmm^2, Iyy ~ ... Nmm^2
  + Công suất 5 động cơ của 5 khớp:
    - Động cơ 1: 750W
    - Động cơ 2: 750W
    - Động cơ 3: 750W
    - Động cơ 4: 200W
    - Động cơ 5: 200W
  + Các bộ truyền tại các khớp:
    + Bộ truyền 1: 
      - Loại bộ truyền: Bánh răng
      - Tỉ số truyền: 1:165.306

    + Bộ truyền 2: 
      - Loại bộ truyền: Bánh răng
      - Tỉ số truyền: 1:180

    + Bộ truyền 3: Đai (1:1) và bánh răng (?)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:180

    + Bộ truyền 4: Đai (1:1) + Bánh răng(1:80) + Xích (1:1)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:80

    + Bộ truyền 5: Đai(1:1) + Bánh răng(1:80)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:73.3
