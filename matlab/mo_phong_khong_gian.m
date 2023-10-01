t1_values = -pi/2:0.3:pi/2;  % T?o vector các giá tr? t1 t? 0 ??n pi/2 v?i b??c 0.1
t2_values = -pi/2:0.3:pi/2;  % T?o vector các giá tr? t2 t? 0 ??n pi/2 v?i b??c 0.1
t3_values = -pi/3:0.3:2*pi/3;
t4_values = 0:0.3:2*pi;

l1 = 300;
l42 = 50;
l3 = 450;
l5 = 500;
l6 = 50;
l78 = 100;

% Kh?i t?o m?ng tr?ng ?? l?u t?a ?? x, y, z
x = [];
y = [];
z = [];

% Vòng l?p ?? tính toán và l?u t?a ?? x, y, z cho t?ng giá tr? t1, t2
for t1 = t1_values
    for t2 = t2_values
        for t3 = t3_values
            for t4 = t4_values
        x_value = l3*cos(t1)*cos(t2) - l6*sin(t1) - l42*sin(t1) - l78*(cos(t4)*(cos(t1)*cos(t2)*sin(t3) - cos(t1)*cos(t3)*sin(t2)) - sin(t4)*(cos(t1)*sin(t2)*sin(t3) + cos(t1)*cos(t2)*cos(t3))) + l5*cos(t1)*cos(t2)*cos(t3) + l5*cos(t1)*sin(t2)*sin(t3);
        y_value = l6*cos(t1) - l78*(cos(t4)*(cos(t2)*sin(t1)*sin(t3) - cos(t3)*sin(t1)*sin(t2)) - sin(t4)*(sin(t1)*sin(t2)*sin(t3) + cos(t2)*cos(t3)*sin(t1))) + l42*cos(t1) + l3*cos(t2)*sin(t1) + l5*cos(t2)*cos(t3)*sin(t1) + l5*sin(t1)*sin(t2)*sin(t3);
        z_value = l1 + l3*sin(t2) - l78*(cos(t4)*(cos(t2)*cos(t3) + sin(t2)*sin(t3)) + sin(t4)*(cos(t2)*sin(t3) - cos(t3)*sin(t2))) - l5*cos(t2)*sin(t3) + l5*cos(t3)*sin(t2);
        
        % Thêm t?a ?? vào m?ng
        x = [x, x_value];
        y = [y, y_value];
        z = [z, z_value];
            end
        end
    end
end

% V? các ?i?m riêng l?
scatter3(x, y, z);

% ??t tên cho các tr?c
xlabel('X');
ylabel('Y');
zlabel('Z');

% Hi?n th? l??i
grid on;

% T? ??ng ?i?u ch?nh t? l? tr?c
axis equal;