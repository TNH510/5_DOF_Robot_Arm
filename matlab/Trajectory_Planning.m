syms th alpha d l th1 th2 th3 th4 th5 l1 l2 l3 l4 l5 l6 l7 l8
syms px py pz
l1 = 690.0;
l2 = 440.0;
l3 = 500.0;
l4 = 0.0;
l5 = 230.0;
Tii1 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R01
l = 0; alpha = sym(pi/2); d = l1; th = th1;
R01 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R12
l = l2; alpha = sym(0); d = 0; th = th2;
R12 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R23
l = l3; alpha = sym(0); d = 0; th = th3;
R23 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R34
l = 0; alpha = sym(pi/2); d = 0; th = th4 + sym(pi/2);
R34 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R45
l = 0; alpha = sym(0); d = l5; th = th5;
R45 = [cos(th) -sin(th)*cos(alpha) sin(th)*sin(alpha); sin(th) cos(th)*cos(alpha) -cos(th)*sin(alpha); 0 sin(alpha) cos(alpha)];
%R02
R02 = R01*R12;
%R03
R03 = R01*R12*R23;
%R04
R04 = R01*R12*R23*R34;
%R05
R05 = simplify(R01*R12*R23*R34*R45);

syms a0x a0y a0z
syms a1x a1y a1z
syms a2x a2y a2z
syms a3x a3y a3z
syms t
syms q0x q0y q0z q1x q1y q1z
syms v0x v0y v0z
syms vfx vfy vfz
syms t0 tf
syms dx dy dz
% Constraints
xf = 0;
yf = 700;
zf = 600;
% define the trajectory
% x = aox + a1x*t + a2x*t^2 + a3x*t^3
% y = aoy + a1y*t + a2y*t^2 + a3y*t^3
% z = aoz + a1z*t + a2z*t^2 + a3z*t^3
% desired velocity
% xdot = a1x + 2*a2x*t + 3*a3x*t^2
% ydot = a1y + 2*a2y*t + 3*a3y*t^2
% zdot = a1y + 2*a2y*t + 3*a3y*t^2
% desired acceleration
% x2dot = 2*a2x + 6*a3x*t
% y2dot = 2*a2y + 6*a3y*t
% z2dot = 2*a2z + 6*a3z*t

clear
dx = input('initial data for x = [q0x, v0x, q1x, v1x]')
dy = input('initial data for y = [q0y, v0y, q1y, v1y]')
dz = input('initial data for z = [q0z, v0z, q1z, v1z]')
dt = input('initial data for t = [t0, tf]')
q0x = dx(1); v0x = dx(2); q1x = dx(3); v1x = dx(4);
q0y = dy(1); v0y = dx(2); q1y = dx(3); v1y = dy(4);
q0z = dz(1); v0z = dx(2); q1z = dx(3); v1z = dz(4);
t0 = dt(1); tf = dt(2);
t = linspace(t0,tf,100*(tf-t0));
c = ones(size(t));
M = [1 t0 t0^2 t0^3;
     0 1 2*t0 3*t0^2;
     1 tf tf^2 tf^3;
     0 1 2*tf 3*tf^2];
bx = [q0x; v0x; q1x; v1x];
by = [q0y; v0y; q1y; v1y];
bz = [q0z; v0z; q1z; v1z];
ax = inv(M)*bx;
ay = inv(M)*by;
az = inv(M)*bz;

qdx = ax(1).*c + ax(2).*t + ax(3).*t.^2 + ax(4).*t.^3
vdx = ax(2).*c + 2*ax(3).*t + 3*ax(4).*t.^2
adx = 2*ax(3).*c + 6*ax(4).*t

% Plot all three functions
figure;

subplot(3, 1, 1);
plot(t, qdx);
xlabel('Time (t)');
ylabel('qd');
title('Position (qd)');

subplot(3, 1, 2);
plot(t, vdx);
xlabel('Time (t)');
ylabel('vd');
title('Velocity (vd)');

subplot(3, 1, 3);
plot(t, adx);
xlabel('Time (t)');
ylabel('ad');
title('Acceleration (ad)');

suptitle('Plot of qd, vd, and ad');



