clc

%% CACULATE DYNAMIC FOR ROBOT 5 DOF

% Variables
% l1 = 690
% l2 = 440
% l3 = 500
% l4 = 0
% l5 = 230

% lc1 = 660
% lc2 = 255
% lc3 = 143
% lc4 = 6
% lc5 = 143

syms l1 l2 l3 l4 l5 lc1 lc2 lc3 lc4 lc5

syms t1 t2 t3 t4 t5

%% Forward Kinematic Result
% x_value = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% y_value = sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% z_value = l1 + l3*sin(t2 + t3) + l2*sin(t2) + l5*sin(t2 + t3 + t4);

%% MA TRAN CHI HUONG (Ri)
R1 = [cos(t1) 0 sin(t1); sin(t1) 0 -cos(t1); 0 1 0];
R2 = [cos(t1)*cos(t2) -cos(t1)*sin(t2) sin(t1); sin(t1)*cos(t2) -sin(t1)*sin(t2) -cos(t1); sin(t2) cos(t2) 0];
R3 = [cos(t1)*cos(t2 + t3) -cos(t1)*sin(t2 + t3) sin(t1); sin(t1)*cos(t2 + t3) -sin(t1)*sin(t2 + t3) -cos(t1); sin(t2 + t3) cos(t2 + t3) 0];
R4 = [-cos(t1)*(-1) sin(t1) cos(t1)*0; -sin(t1)*(-1) -cos(t1) sin(t1)*0; 0 0 (-1)];
R5 = [(sin(t1)*sin(t5) - (-1)*cos(t1)*cos(t5)) (sin(t1)*cos(t5) + (-1)*cos(t1)*sin(t5)) (0*cos(t1)); (-cos(t1)*sin(t5) - (-1)*sin(t1)*cos(t5) ) (-cos(t1)*cos(t5) + (-1)*sin(t1)*sin(t5)) (0*sin(t1)); 0*cos(t5) -0*sin(t5) (-1)];

%% MA TRAN MOMENT QUAN TINH (I)
% Ixx1 = 461147
% Iyy1 = 299920
% Iyy2 = 803322
% Iyy3 = 1604504
% Iyy4 = 6328
% Iyy5 = 24861 
% Izz5 = 14388 

syms Ixx1 Iyy1 Iyy2 Iyy3 Iyy4 Iyy5 Izz5

% Tinh moment quan tinh...

I1 = [Ixx1 0 0; 0 Iyy1 0; 0 0 Ixx1];
I2 = [0 0 0; 0 Iyy2 0; 0 0 Iyy2];
I3 = [0 0 0; 0 Iyy3 0; 0 0 Iyy3];
I4 = [Iyy4 0 0; 0 Iyy4 0; 0 0 0];
I5 = [Iyy5 0 0; 0 Iyy5 0; 0 0 Izz5];

%% MA TRAN BIEU DIEN VI TRI TRONG TAM KHAU (pci)

pc1 = [0 ; 0 ; lc1];
pc2 = [lc2*cos(t1)*cos(t2) ; lc2*sin(t1)*cos(t2) ; (l1 + lc2*sin(t2))];
pc3 = [(cos(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (sin(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (l1 + lc3*sin(t2 + t3) + l2*sin(t2))];
pc4 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*0)) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*0)) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc4*sin((-pi / 2)))];
pc5 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*0)) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*0)) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc5*sin((-pi / 2)))];

%% MA TRAN JACOBI KHAU (Jw)
Jw1 = [0 0 0 0 0; 0 0 0 0 0; 1 0 0 0 0];
Jw2 = [0 sin(t1) 0 0 0; 0 -cos(t1) 0 0 0; 1 0 0 0 0];
Jw3 = [0 sin(t1) sin(t1) 0 0; 0 -cos(t1) -cos(t1) 0 0; 1 0 0 0 0];
Jw4 = [0 sin(t1) sin(t1) sin(t1) 0; 0 -cos(t1) -cos(t1) -cos(t1) 0; 1 0 0 0 0];
Jw5 = [0 sin(t1) sin(t1) sin(t1) 0*cos(t1); 0 -cos(t1) -cos(t1) -cos(t1) 0*sin(t1); 1 0 0 0 (-1)];

%% MA TRAN JACOBI VAN TOC DAI (Jv)
Jv1 = [diff(pc1(1),t1) 0 0 0 0 ; diff(pc1(2),t1) 0 0 0 0; diff(pc1(3),t1) 0 0 0 0];
Jv2 = [diff(pc2(1),t1) diff(pc2(1),t2) 0 0 0 ; diff(pc2(2),t1) diff(pc2(2),t2) 0 0 0; diff(pc2(3),t1) diff(pc2(3),t2) 0 0 0];
Jv3 = [diff(pc3(1),t1) diff(pc3(1),t2) diff(pc3(1),t3) 0 0 ; diff(pc3(2),t1) diff(pc3(2),t2) diff(pc3(2),t3) 0 0; diff(pc3(3),t1) diff(pc3(3),t2) diff(pc3(3),t3) 0 0];
Jv4 = [diff(pc4(1),t1) diff(pc4(1),t2) diff(pc4(1),t3) diff(pc4(1),t4) 0 ; diff(pc4(2),t1) diff(pc4(2),t2) diff(pc4(2),t3) diff(pc4(2),t4) 0; diff(pc4(3),t1) diff(pc4(3),t2) diff(pc4(3),t3) diff(pc4(3),t4) 0];
Jv5 = [diff(pc5(1),t1) diff(pc5(1),t2) diff(pc5(1),t3) diff(pc5(1),t4) diff(pc5(1),t5) ; diff(pc5(2),t1) diff(pc5(2),t2) diff(pc5(2),t3) diff(pc5(2),t4) diff(pc5(2),t5); diff(pc5(3),t1) diff(pc5(3),t2) diff(pc5(3),t3) diff(pc5(3),t4) diff(pc5(3),t5)]

J_plus = simplify(Jv5' * inv((Jv5 * Jv5')))
