clc % clear CW
syms th1 th2 th3 th4 th5 nx ny x y z c3 s3
syms l1 l2 l3 l4 l5 l6 l7 l8

l1 = 240;
l4 = 200;
l2 = 150;
l3 = 450;
l5 = 500;
l6 = 50;
l7 = 50; l8 = 50;

x = 5.8171e-14

y = -950

z = 140

th1 = atan2(y,x);

nx = x * cos(th1) + y * sin(th1);
ny = z - l1 + l7 + l8;

c3 = (nx^2 + ny^2 - l3^2 - l5^2)/(2*l3*l5);
s3 = sqrt(1 - c3^2);

th3 = atan2(s3, c3);

c2 = (nx * (l3 + l5 * c3) - l5 * s3 * ny)/((l3 + l5 * c3)^2 + (l5 * s3)^2);
s2 = sqrt(1 - c2^2);
% 
th2 = atan2(s2, c2);
% 
th4 = th3 - th2;

% Show the result
th1 = th1 * 180 / pi
th2 = th2 * 180 / pi
th3 = th3 * 180 / pi
th4 = th4 * 180 / pi

