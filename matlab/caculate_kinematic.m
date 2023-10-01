syms c1 s1 c2 s2 c3 s3 c4 s4 c5 s5 c6 s6 l1 l2 l3 l4 l5 l6 l7 l8 t1 t2 t3 t4 t5 t6 t7 l42 l78

A1 = [cos(t1) 0 sin(t1) 0; sin(t1) 0 -cos(t1) 0; 0 1 0 l1; 0 0 0 1]
A2 = [cos(t2) sin(t2) 0 l3*cos(t2); sin(t2) -cos(t2) 0 l3*sin(t2); 0 0 1 -l42; 0 0 0 1]
A3 = [cos(t3) sin(t3) 0 l5*cos(t3); sin(t3) -cos(t3) 0 l5*sin(t3); 0 0 1 -l6; 0 0 0 1]
A4 = [cos(t4) 0 sin(t4) 0; sin(t4) 0 -cos(t4) 0; 0 1 0 0; 0 0 0 1]
A5 = [cos(t5) -sin(t5) 0 0; sin(t5) cos(t5) 0 0; 0 0 1 l78; 0 0 0 1]

P = [0;0;0;1]
P0 = A1*A2*A3*A4*A5*P

%P0 = T(0,0,l1).R(z,deta1).T(0,l4,l2).R(z,deta2).T(0,l5,l3-l6-l7).R(z,deta3).P6
%P = T1*Rz1*T2*Rz2*T3*Rz3*P6

%P0 =
 
% l3*cos(t1)*cos(t2) - l6*sin(t1) - l42*sin(t1) - l78*(cos(t4)*(cos(t1)*cos(t2)*sin(t3) - cos(t1)*cos(t3)*sin(t2)) - sin(t4)*(cos(t1)*sin(t2)*sin(t3) + cos(t1)*cos(t2)*cos(t3))) + l5*cos(t1)*cos(t2)*cos(t3) + l5*cos(t1)*sin(t2)*sin(t3)
% l6*cos(t1) - l78*(cos(t4)*(cos(t2)*sin(t1)*sin(t3) - cos(t3)*sin(t1)*sin(t2)) - sin(t4)*(sin(t1)*sin(t2)*sin(t3) + cos(t2)*cos(t3)*sin(t1))) + l42*cos(t1) + l3*cos(t2)*sin(t1) + l5*cos(t2)*cos(t3)*sin(t1) + l5*sin(t1)*sin(t2)*sin(t3)
%                                                                               l1 + l3*sin(t2) - l78*(cos(t4)*(cos(t2)*cos(t3) + sin(t2)*sin(t3)) + sin(t4)*(cos(t2)*sin(t3) - cos(t3)*sin(t2))) - l5*cos(t2)*sin(t3) + l5*cos(t3)*sin(t2)
%                                                                                                                                                                                                                                         1
 