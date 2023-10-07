%t1=0:0.1:pi;
%t2=0:0.1:pi;
%t3=0:0.1:pi;
%t4=0:0.1:pi;
l1=300;
l42=50;
l3=450;
l5=500;
l6=50;
l78=100;
x=[];
y=[];
z=[];
for t1=0:0.3:pi
    for t2=0:0.3:pi
        for t3=0:0.3:pi
            for t4=0:0.3:pi
                x_value = l3*cos(t1)*cos(t2) - l6*sin(t1) - l42*sin(t1) - l78*(cos(t4)*(cos(t1)*cos(t2)*sin(t3) - cos(t1)*cos(t3)*sin(t2)) - sin(t4)*(cos(t1)*sin(t2)*sin(t3) + cos(t1)*cos(t2)*cos(t3))) + l5*cos(t1)*cos(t2)*cos(t3) + l5*cos(t1)*sin(t2)*sin(t3)
                y_value = l6*cos(t1) - l78*(cos(t4)*(cos(t2)*sin(t1)*sin(t3) - cos(t3)*sin(t1)*sin(t2)) - sin(t4)*(sin(t1)*sin(t2)*sin(t3) + cos(t2)*cos(t3)*sin(t1))) + l42*cos(t1) + l3*cos(t2)*sin(t1) + l5*cos(t2)*cos(t3)*sin(t1) + l5*sin(t1)*sin(t2)*sin(t3)
                z_value = l1 - l78*cos(t2 - t3 + t4) + l3*sin(t2) + l5*sin(t2 - t3)
                x =[x,x_value];
                y =[y,y_value];
                z =[z,z_value];
            end
        end
    end
end
figure;
plot3(x, y, z);
xlabel('X');
ylabel('Y');
zlabel('Z');
title('Mô phỏng một điểm trong không gian');
grid on;
