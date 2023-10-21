clc % clear CW
syms px py pz l1 l2 l3 l4 l5 l6 l7 l8
syms th1 th2 th3 th4 th5
% Condition: th2 - th3 + th4 = 0 --> set as this rule
th1 = 0;
th2 = 0;
th3 = 0;
th4 = -pi/2;

l1 = 240;
l4 = 200;
l2 = 150;
l3 = 450;
l5 = 500;
l6 = 50;
l7 = 100; l8 = 0;

px = cos(th1)*(l3*cos(th2 + th3) + l2*cos(th2) + l5*cos(th2 + th3 + th4))
py = sin(th1)*(l3*cos(th2 + th3) + l2*cos(th2) + l5*cos(th2 + th3 + th4))
pz = l1 + l3*sin(th2 + th3) + l2*sin(th2) + l5*sin(th2 + th3 + th4)

% px = l1*cos(th1) + l6*sin(th1) + sin(th1)*(l2 - l4) + l3*cos(th1)*cos(th2) + sin(th2 - th3 + th4)*cos(th1)*(l7 + l8) + l5*cos(th1)*cos(th2)*cos(th3) + l5*cos(th1)*sin(th2)*sin(th3)
% py = l1*sin(th1) - l6*cos(th1) - cos(th1)*(l2 - l4) + sin(th2 - th3 + th4)*sin(th1)*(l7 + l8) + l3*cos(th2)*sin(th1) + l5*cos(th2)*cos(th3)*sin(th1) + l5*sin(th1)*sin(th2)*sin(th3)
% pz = l3*sin(th2) - l8*cos(th2 - th3 + th4) - l7*cos(th2 - th3 + th4) + l5*sin(th2 - th3)
