t1 = 0;
t2 = pi / 2;
t3 = -pi / 2;
t4 = -pi / 2;
t5 = 0;

l1 = 690;
l2 = 440;
l3 = 500;
l4 = 0;
l5 = 230;

omega = [-0.1; 0.3; 0.5; 0.1; 0.1];

Jv5 = [ -sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2)), -cos(t1)*(l3*sin(t2 + t3) + l2*sin(t2)), - l3*sin(t2 + t3)*cos(t1), 0, 0;
        cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2)), -sin(t1)*(l3*sin(t2 + t3) + l2*sin(t2)), - l3*sin(t2 + t3)*sin(t1), 0, 0;
        0, l3*cos(t2 + t3) + l2*cos(t2), l3*cos(t2 + t3), 0, 0];

v = [100; 100; 100]    
% v = Jv5 * omega

J_plus = [ -sin(t1)/(l3*cos(t2 + t3) + l2*cos(t2)), cos(t1)/(l3*cos(t2 + t3) + l2*cos(t2)), 0;
            (cos(t2 + t3)*cos(t1))/(l2*sin(t3)), (cos(t2 + t3)*sin(t1))/(l2*sin(t3)), sin(t2 + t3)/(l2*sin(t3));
            -(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2)))/(l2*l3*sin(t3)), -(sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2)))/(l2*l3*sin(t3)), -(l3*sin(t2 + t3) + l2*sin(t2))/(l2*l3*sin(t3));
            0, 0, 0;
            0, 0, 0];

omega = J_plus * v
 
  