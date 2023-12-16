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
R1 = [cos(t1) 0 sin(t1); sin(t1) 0 -cos(t1); 0 1 0]
R2 = [cos(t1)*cos(t2) -cos(t1)*sin(t2) sin(t1); sin(t1)*cos(t2) -sin(t1)*sin(t2) -cos(t1); sin(t2) cos(t2) 0]
R3 = [cos(t1)*cos(t2 + t3) -cos(t1)*sin(t2 + t3) sin(t1); sin(t1)*cos(t2 + t3) -sin(t1)*sin(t2 + t3) -cos(t1); sin(t2 + t3) cos(t2 + t3) 0]
R4 = [-cos(t1)*(-1) sin(t1) cos(t1)*0; -sin(t1)*(-1) -cos(t1) sin(t1)*0; 0 0 (-1)]
R5 = [(sin(t1)*sin(t5) - (-1)*cos(t1)*cos(t5)) (sin(t1)*cos(t5) + (-1)*cos(t1)*sin(t5)) (0*cos(t1)); (-cos(t1)*sin(t5) - (-1)*sin(t1)*cos(t5) ) (-cos(t1)*cos(t5) + (-1)*sin(t1)*sin(t5)) (0*sin(t1)); 0*cos(t5) -0*sin(t5) (-1)]

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

pc1 = [0 ; 0 ; lc1]
pc2 = [lc2*cos(t1)*cos(t2) ; lc2*sin(t1)*cos(t2) ; (l1 + lc2*sin(t2))]
pc3 = [(cos(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (sin(t1)*(lc3*cos(t2 + t3) + l2*cos(t2))) ; (l1 + lc3*sin(t2 + t3) + l2*sin(t2))]
pc4 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*0)) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*0)) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc4*sin((-pi / 2)))]
pc5 = [(cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*0)) ; (sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*0)) ; (l1 + l3*sin(t2 + t3) + l2*sin(t2) + lc5*sin((-pi / 2)))]

%% MA TRAN JACOBI KHAU (Jw)
Jw1 = [0 0 0 0 0; 0 0 0 0 0; 1 0 0 0 0];
Jw2 = [0 sin(t1) 0 0 0; 0 -cos(t1) 0 0 0; 1 0 0 0 0];
Jw3 = [0 sin(t1) sin(t1) 0 0; 0 -cos(t1) -cos(t1) 0 0; 1 0 0 0 0];
Jw4 = [0 sin(t1) sin(t1) sin(t1) 0; 0 -cos(t1) -cos(t1) -cos(t1) 0; 1 0 0 0 0];
Jw5 = [0 sin(t1) sin(t1) sin(t1) 0*cos(t1); 0 -cos(t1) -cos(t1) -cos(t1) 0*sin(t1); 1 0 0 0 (-1)];

%% MA TRAN JACOBI VAN TOC DAI (Jv)
Jv1 = [diff(pc1(1),t1) 0 0 0 0 ; diff(pc1(2),t1) 0 0 0 0; diff(pc1(3),t1) 0 0 0 0]
Jv2 = [diff(pc2(1),t1) diff(pc2(1),t2) 0 0 0 ; diff(pc2(2),t1) diff(pc2(2),t2) 0 0 0; diff(pc2(3),t1) diff(pc2(3),t2) 0 0 0]
Jv3 = [diff(pc3(1),t1) diff(pc3(1),t2) diff(pc3(1),t3) 0 0 ; diff(pc3(2),t1) diff(pc3(2),t2) diff(pc3(2),t3) 0 0; diff(pc3(3),t1) diff(pc3(3),t2) diff(pc3(3),t3) 0 0]
Jv4 = [diff(pc4(1),t1) diff(pc4(1),t2) diff(pc4(1),t3) diff(pc4(1),t4) 0 ; diff(pc4(2),t1) diff(pc4(2),t2) diff(pc4(2),t3) diff(pc4(2),t4) 0; diff(pc4(3),t1) diff(pc4(3),t2) diff(pc4(3),t3) diff(pc4(3),t4) 0]
Jv5 = [diff(pc5(1),t1) diff(pc5(1),t2) diff(pc5(1),t3) diff(pc5(1),t4) diff(pc5(1),t5) ; diff(pc5(2),t1) diff(pc5(2),t2) diff(pc5(2),t3) diff(pc5(2),t4) diff(pc5(2),t5); diff(pc5(3),t1) diff(pc5(3),t2) diff(pc5(3),t3) diff(pc5(3),t4) diff(pc5(3),t5)]

%% MA TRAN QUAN TINH
% m1 = 20.0
% m2 = 21.0
% m3 = 20.0
% m4 = 2.0
% m5 = 2.0

syms m1 m2 m3 m4 m5

m = [m1 ; m2 ; m3 ; m4 ; m5]

D1 = m1*(Jv1')*Jv1 + (Jw1')*R1*I1*(R1')*Jw1; 
D2 = m2*(Jv2')*Jv2 + (Jw2')*R2*I2*(R2')*Jw2;
D3 = m3*(Jv3')*Jv3 + (Jw3')*R3*I3*(R3')*Jw3;
D4 = m4*(Jv4')*Jv4 + (Jw4')*R4*I4*(R4')*Jw4;
D5 = m5*(Jv5')*Jv5 + (Jw5')*R5*I5*(R5')*Jw5;

D1 = simplify(D1)
D2 = simplify(D2)
D3 = simplify(D3)
D4 = simplify(D4)
D5 = simplify(D5)

D = D1 + D2 + D3 + D4 + D5;

%% VECTOR HUONG TAM
V1 = 0
V2 = 0
V3 = 0
V4 = 0
V5 = 0
syms t1_d t2_d t3_d t4_d t5_d

q_d = [t1_d ; t2_d ; t3_d ; t4_d ; t5_d]
q   = [t1 ; t2 ; t3 ; t4 ; t5]

for i = 1:1:5
    for j = 1:1:5
        V1 = V1 + (diff(D(1,j),q(i)) - 1/2 * diff(D(i,j),q(1))) * q_d(i) * q_d(j);
    end
end

for i = 1:1:5
    for j = 1:1:5
        V2 = V2 + (diff(D(2,j),q(i)) - 1/2 * diff(D(i,j),q(2))) * q_d(i) * q_d(j);
    end
end

for i = 1:1:5
    for j = 1:1:5
        V3 = V3 + (diff(D(3,j),q(i)) - 1/2 * diff(D(i,j),q(3))) * q_d(i) * q_d(j);
    end
end

for i = 1:1:5
    for j = 1:1:5
        V4 = V4 + (diff(D(4,j),q(i)) - 1/2 * diff(D(i,j),q(4))) * q_d(i) * q_d(j);
    end
end

for i = 1:1:5
    for j = 1:1:5
        V5 = V5 + (diff(D(5,j),q(i)) - 1/2 * diff(D(i,j),q(5))) * q_d(i) * q_d(j);
    end
end

V1
V2
V3
V4
V5

V = [V1 ; V2 ; V3 ; V4 ; V5];

%% VECTOR TRONG LUC

gT =  [0 , 0, -9.8]

G1 = - (m(1) * gT * Jv1(:, 1) + m(2) * gT * Jv2(:, 1) + m(3) * gT * Jv3(:, 1) + m(4) * gT * Jv4(:, 1) + m(5) * gT * Jv5(:, 1))
G2 = - (m(1) * gT * Jv1(:, 2) + m(2) * gT * Jv2(:, 2) + m(3) * gT * Jv3(:, 2) + m(4) * gT * Jv4(:, 2) + m(5) * gT * Jv5(:, 2))
G3 = - (m(1) * gT * Jv1(:, 3) + m(2) * gT * Jv2(:, 3) + m(3) * gT * Jv3(:, 3) + m(4) * gT * Jv4(:, 3) + m(5) * gT * Jv5(:, 3))
G4 = - (m(1) * gT * Jv1(:, 4) + m(2) * gT * Jv2(:, 4) + m(3) * gT * Jv3(:, 4) + m(4) * gT * Jv4(:, 4) + m(5) * gT * Jv5(:, 4))
G5 = - (m(1) * gT * Jv1(:, 5) + m(2) * gT * Jv2(:, 5) + m(3) * gT * Jv3(:, 5) + m(4) * gT * Jv4(:, 5) + m(5) * gT * Jv5(:, 5))

G = [simplify(G1) ; simplify(G2) ; simplify(G3) ; simplify(G4) ; simplify(G5)]

%% KET QUA TINH DONG LUC HOC
syms t1_dd t2_dd t3_dd t4_dd t5_dd
q_dd = [t1_dd ; t2_dd ; t3_dd ; t4_dd ; t5_dd];

%D(q)*q_dd + V(q, q_d) + G(q) = tau
%(5x5)(5x1) + (5x1) + (5x1)

tau = G

m1 = 27.5 %kg
m2 = 24.0
m3 = 25.0
m4 = 3.0
m5 = 1.0 + 15.0

t1 = 0 %rad
t2 = 0
t3 = 0
t4 = - pi / 2
t5 = 0

l1 = 0.690 %m
l2 = 0.440
l3 = 0.500
l4 = 0.0
l5 = 0.230

lc1 = 0.660 %m
lc2 = 0.255
lc3 = 0.143
lc4 = 0.6
lc5 = 0.143

tau_2 = (49*m4*(l3*cos(t2 + t3) + l2*cos(t2) + lc4*0))/5 + (49*m5*(l3*cos(t2 + t3) + l2*cos(t2) + lc5*0))/5 + (49*m3*(lc3*cos(t2 + t3) + l2*cos(t2)))/5 + (49*lc2*m2*cos(t2))/5
tau_3 = (49*m4*(l3*cos(t2 + t3) + lc4*0))/5 + (49*m5*(l3*cos(t2 + t3) + lc5*0))/5 + (49*lc3*m3*cos(t2 + t3))/5
tau_4 = (49*0*(lc4*m4 + lc5*m5))/5

