syms th al d a th1 th2 th3 th4 th5 l1 l2 l3 l4 l5 l6 l7 l8
Tii1 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1]
%T01
a = 0; al = sym(pi/2); d = l1; th = th1;
T01 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1];
%T12
a = l3; al = sym(pi); d = -(l4-l2); th = th2;
T12 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1];
%T23
a = l5; al = sym(pi); d = -l6; th = th3;
T23 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1];
%T34
a = 0; al = sym(pi/2); d = 0; th = th4;
T34 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1];
%T45
a = 0; al = sym(0); d = l7+l8; th = th5;
T45 = [cos(th) -sin(th)*cos(al) sin(th)*sin(al) a*cos(th); sin(th) cos(th)*cos(al) -cos(th)*sin(al) a*sin(th); 0 sin(al) cos(al) d; 0 0 0 1];
%T05
T05 = T01*T12*T23*T34*T45;
%px py pz
px = simplify(T05(1,4));
py = simplify(T05(2,4));
pz = simplify(T05(3,4));
%FK matrix
syms r11 r12 r13 r21 r22 r23 r31 r32 r33 x y z
FK = [r11 r12 r13 x; r21 r22 r23 y; r31 r32 r33 z; 0 0 0 1];
%inv T01
T10 = inv(T01);
%T10FK
T10FK = T10 * FK;
%T12T23T34T45
T12T23T34T45 = T12*T23*T34*T45;
%n1 n2 n3 m1 m2 m3
n1 = simplify(T10FK(1,4));
n2 = simplify(T10FK(2,4));
n3 = simplify(T10FK(3,4));
m1 = simplify(T12T23T34T45(1,4));
m2 = simplify(T12T23T34T45(2,4));
m3 = simplify(T12T23T34T45(3,4));




