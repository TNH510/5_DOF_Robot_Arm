# Tổng quan về đồ án

## Tên đề tài: `Điều khiển tay máy ứng dụng trong xếp sản phẩm`

## Các mục tiêu, giới hạn của đề tài
+ Mục tiêu chung: Nghiên cứu, thiết kế chương trình điều khiển cánh tay robot 5 bậc tự do để sắp xếp, phân loại các thùng sản phẩm từ vị trí này sang vị trí khác áp dụng xử lí ảnh. Chế tạo găng tay điều khiển nhằm giúp người dùng có thể điều khiển robot dễ dàng chỉ bằng cử chỉ của tay. Chế tạo đầu tay gắp đơn giản và tối ưu cho việc gắp các thùng sản phẩm.

## Thiết kế hệ thống
+ Tổng quan hệ thống
  
![Tổng quan hệ thống](general_system.svg)

+ Găng tay điều khiển
  
![Găng tay điều khiển](gang_tay.svg)

## Phân chia nhiệm vụ chung

+ Hiểu: Găng tay + quản lí chung
+ Thạch: Tool C# + xử lí ảnh
+ Lộc: Đầu tay gắp + PLC (robot, băng tải)
  
## Các nhiệm vụ cần làm 

+ Thiết kế bộ tay gắp bằng giác hút cho robot [Loc]
+ Lập trình điều khiển cho PLC [Loc]
+ Lập trình giao diện tạo quỹ đạo cho robot và tự động xác định các vùng bị giới hạn hoạt động [Thach]
+ Import hệ thống băng tải vào giao diện điều khiển [Thach]
+ Clean và review lại giao diện điều khiển và tối ưu hóa lại [Thach]
+ Thiết kế giải thuật điều khiển robot với quỹ đạo thay đổi liên tục [Hieu]
+ Thiết kế mạch, phần cứng, lấy data, giải thuật lọc nhiễu, xử lí góc, send log về máy tính từ găng tay [Hieu]