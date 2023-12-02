%t1_values = pi*17/36:1:pi*59/36; 

t1 = 0
t2_values = pi*2/3:0.1:pi*10/9;  
t3_values = pi*5/6:0.1:pi*55/36;
t4_values = 0:0.1:2*pi;

l1 = 690.0;
l2 = 440.0;
l3 = 500.0;
l4 = 0.0;
l5 = 100.0;

x = [];
y = [];
z = [];

%for t1 = t1_values - pi
    for t2 = t2_values - pi
        for t3 = t3_values - pi
            for t4 = t4_values - pi

                x_value = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
                y_value = sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
                z_value = l1 + l3*sin(t2 + t3) + l2*sin(t2) + l5*sin(t2 + t3 + t4);

                x = [x, x_value];
                y = [y, y_value];
                z = [z, z_value];

            end
        end
    end
%end

scatter(x,z,'filled');

xlabel('X');
ylabel('Z');
zlabel('Z');

grid on;

axis equal;