% t1_values = -35:5:90; 
t1_values = 0;
t2_values = 45:1:110;  
t3_values = -120:1:-80;
t4_values = -105:1:5;

l1 = 690.0;
l2 = 440.0;
l3 = 500.0;
l4 = 0.0;
l5 = 230.0;

x = [];
y = [];
z = [];

for t1 = t1_values * pi / 180 
    for t2 = t2_values * pi / 180 
        for t3 = t3_values * pi / 180 
                t4 = (- pi / 2) - t3 - t2;
                
                if (t4 >= (-7 * pi / 12)) && (t4 <= (pi / 36))
                    x_value = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
                    y_value = sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
                    z_value = l1 + l3*sin(t2 + t3) + l2*sin(t2) + l5*sin(t2 + t3 + t4);

                    if (z_value >= 500 && z_value <= 1000)
                        x = [x, x_value];
                        y = [y, y_value];
                        z = [z, z_value];
                    end
                end

        end
    end
end

scatter3(x,y,z);

xlabel('X');
ylabel('Y');
zlabel('Z');

grid on;

axis equal;