clc % clear CW
syms th1 th2 th3 th4 th5 roll pitch m n px py pz c3 s3 c2 s2 A
syms l1 l2 l3 l4 l5 l6 l7 l8

l1 = 690.0;
l2 = 440.0;
l3 = 500.0;
l4 = 0.0;
l5 = 230.0;

px = 584.6666;

py = 0;

pz = -143.5314;

roll = 0;

pitch = -pi/2;



th1 = atan2(py,px);

th5 = roll - th1;

m = sqrt(px^2 + py^2);
n = pz - l1 + l5;

c3 = (m^2 + n^2 - l3^2 - l2^2)/(2*l3*l2);
s3 = sqrt(1 - c3^2);

th3 = atan2(s3, c3);

c2 = m*(l3*c3 + l2) + n*(l3*s3);
s2 = n*(l3*c3 + l2) - m*(l3*s3);
% 
th2 = atan2(s2, c2);
% 
th4 = pitch - th3 - th2;


% Show the result
th1 = th1 * 180 / pi
th2 = th2 * 180 / pi
th3 = th3 * 180 / pi
th4 = th4 * 180 / pi
th5 = th5 * 180 / pi


