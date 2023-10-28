t1_values = -pi/2:0.3:pi/2;  % T?o vector c�c gi� tr? t1 t? 0 ??n pi/2 v?i b??c 0.1
t2_values = -pi/2:0.3:pi/2;  % T?o vector c�c gi� tr? t2 t? 0 ??n pi/2 v?i b??c 0.1
t3_values = -pi/3:0.3:2*pi/3;
t4_values = 0:0.3:2*pi;

l1 = 300;
l4 = 200;
l2 = 150;
l3 = 450;
l5 = 500;
l6 = 50;
l7 = 100;
l8 = 100;

% Kh?i t?o m?ng tr?ng ?? l?u t?a ?? x, y, z
x = [];
y = [];
z = [];

% V�ng l?p ?? t�nh to�n v� l?u t?a ?? x, y, z cho t?ng gi� tr? t1, t2
for t1 = t1_values
    for t2 = t2_values
        for t3 = t3_values
            for t4 = t4_values
            x_value = l6*sin(t1) + sin(t1)*(l2 - l4) + l3*cos(t1)*cos(t2) + sin(t2 - t3 + t4)*cos(t1)*(l7 + l8) + l5*cos(t1)*cos(t2)*cos(t3) + l5*cos(t1)*sin(t2)*sin(t3)
            y_value = sin(t2 - t3 + t4)*sin(t1)*(l7 + l8) - cos(t1)*(l2 - l4) - l6*cos(t1) + l3*cos(t2)*sin(t1) + l5*cos(t2)*cos(t3)*sin(t1) + l5*sin(t1)*sin(t2)*sin(t3)
            z_value = l1 - l7*cos(t2 - t3 + t4) - l8*cos(t2 - t3 + t4) + l3*sin(t2) + l5*sin(t2 - t3)
 
        % Th�m t?a ?? v�o m?ng
        x = [x, x_value];
        y = [y, y_value];
        z = [z, z_value];
            end
        end
    end
end

% V? c�c ?i?m ri�ng l?
scatter3(x, y, z);

% ??t t�n cho c�c tr?c
xlabel('X');
ylabel('Y');
zlabel('Z');

% Hi?n th? l??i
grid on;

% T? ??ng ?i?u ch?nh t? l? tr?c
axis equal;