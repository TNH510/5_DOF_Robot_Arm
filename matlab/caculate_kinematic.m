A1 = [cos(t1) 0 sin(t1) 0; sin(t1) 0 -cos(t1) 0; 0 1 0 l1; 0 0 0 1]
A2 = [cos(t2) sin(t2) 0 l3*cos(t2); sin(t2) -cos(t2) 0 l3*sin(t2); 0 0 1 -l42; 0 0 0 1]
A3 = [cos(t3) sin(t3) 0 l5*cos(t3); sin(t3) -cos(t3) 0 l5*sin(t3); 0 0 1 -l6; 0 0 0 1]
A4 = [cos(t4) 0 sin(t4) 0; sin(t4) 0 -cos(t4) 0; 0 1 0 0; 0 0 0 1]
A5 = [cos(t5) -sin(t5) 0 0; sin(t5) cos(t5) 0 0; 0 0 1 l78; 0 0 0 1]

P = [0;0;0;1]
P0 = A1*A2*A3*A4*A5*P
P1 = simplify(P0)