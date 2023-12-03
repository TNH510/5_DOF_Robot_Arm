%% CACULATE DYNAMIC FOR ROBOT 5 DOF

% Variables
syms px py pz l1 l2 l3 l4 l5
syms t1 t2 t3 t4 t5

%% Forward Kinematic Result
% x_value = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% y_value = sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% z_value = l1 + l3*sin(t2 + t3) + l2*sin(t2) + l5*sin(t2 + t3 + t4);

%% MA TRAN CHI HUONG (Ri)
syms R1 R2 R3 R4 R5
R1 = [cos(t1) 0 sin(t1); sin(t1) 0 -cos(t1); 0 1 0]
R2 = [cos(t1)*cos(t2) -cos(t1)*sin(t2) sin(t1); sin(t1)*cos(t2) -sin(t1)*sin(t2) -cos(t1); sin(t2) cos(t2) 0]
R3 = [cos(t1)*cos(t2 + t3) -cos(t1)*sin(t2 + t3) sin(t1); sin(t1)*cos(t2 + t3) -sin(t1)*sin(t2 + t3) -cos(t1); sin(t2 + t3) cos(t2 + t3) 0]
R4 = [-cos(t1)*sin(t2 + t3 + t4) sin(t1) cos(t1)*cos(t2 + t3 + t4); -sin(t1)*sin(t2 + t3 + t4) -cos(t1) sin(t1)*cos(t2 + t3 + t4); cos(t2 + t3 + t4) 0 sin(t2 + t3 + t4)]
R5 = [(sin(t1)*sin(t5) - sin(t2 + t3 + t4)*cos(t1)*cos(t5)) (sin(t1)*cos(t5) + sin(t2 + t3 + t4)*cos(t1)*sin(t5)) (cos(t2 + t3 + t4)*cos(t1)); (-cos(t1)*sin(t5) - sin(t2 + t3 + t4)*sin(t1)*cos(t5) ) (-cos(t1)*cos(t5) + sin(t2 + t3 + t4)*sin(t1)*sin(t5)) (cos(t2 + t3 + t4)*sin(t1)); cos(t2 + t3 + t4)*cos(t5) -cos(t2 + t3 + t4)*sin(t5) sin(t2 + t3 + t4)]
