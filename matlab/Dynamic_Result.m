%% CACULATE DYNAMIC FOR ROBOT 5 DOF

% Variables
syms px py pz l1 l2 l3 l4 l5
syms lc1 lc2 lc3 lc4 lc5
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

%% MA TRAN MOMENT QUAN TINH (I)
syms I1 I2 I3 I4 I5
syms Ixx1 Iyy1 Iyy2 Iyy3 Iyy4 Iyy5 Izz5

% Tinh moment quan tinh...

I1 = [Ixx1 0 0; 0 Iyy1 0; 0 0 Ixx1]
I2 = [0 0 0; 0 Iyy2 0; 0 0 Iyy2]
I3 = [0 0 0; 0 Iyy3 0; 0 0 Iyy3]
I4 = [Iyy4 0 0; 0 Iyy4 0; 0 0 0]
I5 = [Iyy5 0 0; 0 Iyy5 0; 0 0 Izz5]

%% MA TRAN BIEU DIEN VI TRI TRONG TAM KHAU (pci)
syms pc1 pc2 pc3 pc4 pc5

pc1 = [0 ; 0 ; lc1]
pc2 = [lc2*cos(t1)*cos(t2) ; lc2*sin(t1)*cos(t2) ; (l1 + lc2*sin(t2))]
pc3 = [(cos(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (sin(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (l1 + lc3*sin(t2 + t3) + l2*sin(t2))]
pc4 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*cos(t2 + t3 +t4))) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*cos(t2 + t3 +t4))) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc4*sin(t2 + t3 +t4))]
pc5 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*cos(t2 + t3 +t4))) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*cos(t2 + t3 +t4))) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc5*sin(t2 + t3 +t4))]

%% MA TRAN JACOBI KHAU (Jw)
syms Jw1 Jw2 Jw3 Jw4 Jw5
Jw1 = [0 0 0 0 0; 0 0 0 0 0; 1 0 0 0 0]
Jw2 = [0 sin(t1) 0 0 0; 0 -cos(t1) 0 0 0; 1 0 0 0 0]
Jw3 = [0 sin(t1) sin(t1) 0 0; 0 -cos(t1) -cos(t1) 0 0; 1 0 0 0 0]
Jw4 = [0 sin(t1) sin(t1) sin(t1) 0; 0 -cos(t1) -cos(t1) -cos(t1) 0; 1 0 0 0 0]
Jw5 = [0 sin(t1) sin(t1) sin(t1) cos(t2 + t3 + t4)*cos(t1); 0 -cos(t1) -cos(t1) -cos(t1) cos(t2 + t3 + t4)*sin(t1); 1 0 0 0 sin(t2 + t3 + t4)]

%% MA TRAN JACOBI VAN TOC DAI (Jv)
syms Jv1 Jv2 Jv3 Jv4 Jv5
Jv1 = [diff(pc1(1),t1) 0 0 0 0 ; diff(pc1(2),t1) 0 0 0 0; diff(pc1(3),t1) 0 0 0 0]
Jv2 = [diff(pc2(1),t1) diff(pc2(1),t2) 0 0 0 ; diff(pc2(2),t1) diff(pc2(2),t2) 0 0 0; diff(pc2(3),t1) diff(pc2(3),t2) 0 0 0]
Jv3 = [diff(pc3(1),t1) diff(pc3(1),t2) diff(pc3(1),t3) 0 0 ; diff(pc3(2),t1) diff(pc3(2),t2) diff(pc3(2),t3) 0 0; diff(pc3(3),t1) diff(pc3(3),t2) diff(pc3(3),t3) 0 0]
Jv4 = [diff(pc4(1),t1) diff(pc4(1),t2) diff(pc4(1),t3) diff(pc4(1),t4) 0 ; diff(pc4(2),t1) diff(pc4(2),t2) diff(pc4(2),t3) diff(pc4(2),t4) 0; diff(pc4(3),t1) diff(pc4(3),t2) diff(pc4(3),t3) diff(pc4(3),t4) 0]
Jv5 = [diff(pc5(1),t1) diff(pc5(1),t2) diff(pc5(1),t3) diff(pc5(1),t4) diff(pc5(1),t5) ; diff(pc5(2),t1) diff(pc5(2),t2) diff(pc5(2),t3) diff(pc5(2),t4) diff(pc5(2),t5); diff(pc5(3),t1) diff(pc5(3),t2) diff(pc5(3),t3) diff(pc5(3),t4) diff(pc5(3),t5)]

