data = csvread('data_robot_tinh_toan.csv', 0, 1); % 'file.csv' là tên tệp CSV, 0 là số dòng bắt đầu (0 nếu không có dòng tiêu đ�?), 1 là số cột (cột thứ 2)
columnData = data(:, 2) % Lấy dữ liệu từ cột đầu tiên và lưu vào mảng columnData