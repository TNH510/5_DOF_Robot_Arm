%% CACULATE DYNAMIC FOR ROBOT 5 DOF

% Variables
syms px py pz l1 l2 l3 l4 l5
syms t1 t2 t3 t4 t5
syms c1 c2 c3 c4 c5 s1 s2 s3 s4 s5

%% Forward Kinematic Result
% x_value = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% y_value = sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2) + l5*cos(t2 + t3 + t4));
% z_value = l1 + l3*sin(t2 + t3) + l2*sin(t2) + l5*sin(t2 + t3 + t4);

%% MA TRAN CHI HUONG (Ri)
syms R1 R2 R3 R4 R5
R1 = [c1 0 s1; s1 0 -c1; 0 1 0]
R2 = [c1*c2 -c1*s2 s1; s1*c2 -s1*s2 -c1; s2 c2 0]
R3 = [c1*c23 -c1*s23 s1; s1*c23 -s1*s23 -c1; s23 c23 0]
R4 = [-c1*s234 s1 c1*c234; -s1*s234 -c1 s1*c234; c234 0 s234]
R5 = [(s1*s5 - s234*c1*c5) (s1*c5 + s234*c1*s5) (c234*c1); (-c1*s5 - s234*s1*c5 ) (-c1*c5 + s234*s1*s5) (c234*s1); c234*c5 -c234*s5 s234]
