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
    + Góc khớp 1: 
    + Góc khớp 2: 
    + Góc khớp 3:
    + Góc khớp 4:
    + Góc khớp 5:
    + Độ dài khâu 0:
    + Độ dài khâu 1:
    + Độ dài khâu 2:
    + Độ dài khâu 3:
    + Độ dài khâu 4:
    + Độ dài khâu 5:
    + Miền hoạt động trong không gian:
  + Khối lượng các khâu:
    + Khâu 0:
    + Khâu 1:
    + Khâu 2:
    + Khâu 3:
    + Khâu 4:
    + Khâu 5:
  + Moment quán tính các khâu:
    + Khâu 0:
    + Khâu 1:
    + Khâu 2:
    + Khâu 3:
    + Khâu 4:
    + Khâu 5:
  + Moment MAX của động cơ:
  + Các bộ truyền tại các khớp:
    + Bộ truyền 1: 
      - Loại bộ truyền: Bánh răng
      - Tỉ số truyền: 1:1?
      - Thông số kích thước: ???

    + Bộ truyền 2: 
      - Loại bộ truyền: Bánh răng
      - Tỉ số truyền: 1:180 ?
      - Thông số kích thước: (số răng, số răng)

    + Bộ truyền 3: Đai (1:1) và bánh răng (?)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:180 ?
      - Thông số kích thước: (số răng, số răng)

    + Bộ truyền 4: Đai (1:1) + Bánh răng(1:80) + Xích (1:1)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:80
      - Thông số kích thước: (số răng, số răng)

    + Bộ truyền 5: Đai(1:1) + Bánh răng(1:80)
      - Loại bộ truyền: 
      - Tỉ số truyền: 1:80 ?
      - Thông số kích thước: (số răng, số răng)
