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
syms t0 tf tf_old
syms dx dy dz
% Constraints
xf = 0;
yf = 700;
zf = 600;

clc
syms traj_loop

tf_old = 0;
t0_old = 0;
tf_final = 0;
% Initialize qdx, vdx, and adx
sub_qdx = [];
sub_vdx = [];
sub_adx = [];

sub_qdy = [];
sub_vdy = [];
sub_ady = [];

sub_qdz = [];
sub_vdz = [];
sub_adz = [];
point = input('Enter the desired point(s):');
for traj_loop = 1:point
    % Input initial data for x, y, and z
    dx = input('initial data for x = [q0x, v0x, q1x, v1x]');
    dy = input('initial data for y = [q0y, v0y, q1y, v1y]');
    dz = input('initial data for z = [q0z, v0z, q1z, v1z]');
    dt = input('initial data for t = [t0, tf]');
    
    % Extracting values from input vectors
    q0x = dx(1); v0x = dx(2); q1x = dx(3); v1x = dx(4);
    q0y = dy(1); v0y = dy(2); q1y = dy(3); v1y = dy(4);
    q0z = dz(1); v0z = dz(2); q1z = dz(3); v1z = dz(4);
    t0 = dt(1); tf = dt(2);
   
    t = linspace(t0, tf, 100*(tf - t0));
    c = ones(size(t));

    % Compute coefficients for x, y, and z
    M = [1 t0 t0^2 t0^3; 0 1 2*t0 3*t0^2; 1 tf tf^2 tf^3; 0 1 2*tf 3*tf^2];
    bx = [q0x; v0x; q1x; v1x];
    by = [q0y; v0y; q1y; v1y];
    bz = [q0z; v0z; q1z; v1z];
    
    ax = M \ bx;
    ay = M \ by;
    az = M \ bz;

    % Calculate positions, velocities, and accelerations for each dimension
    qdx = ax(1).*c + ax(2).*t + ax(3).*t.^2 + ax(4).*t.^3;
    vdx = ax(2).*c + 2*ax(3).*t + 3*ax(4).*t.^2;
    adx = 2*ax(3).*c + 6*ax(4).*t;

    qdy = ay(1).*c + ay(2).*t + ay(3).*t.^2 + ay(4).*t.^3;
    vdy = ay(2).*c + 2*ay(3).*t + 3*ay(4).*t.^2;
    ady = 2*ay(3).*c + 6*ay(4).*t;

    qdz = az(1).*c + az(2).*t + az(3).*t.^2 + az(4).*t.^3;
    vdz = az(2).*c + 2*az(3).*t + 3*az(4).*t.^2;
    adz = 2*az(3).*c + 6*az(4).*t;
    
    sub_qdx = [sub_qdx, qdx];
    sub_vdx = [sub_vdx, vdx];
    sub_adx = [sub_adx, adx];
    
    sub_qdy = [sub_qdy, qdy];
    sub_vdy = [sub_vdy, vdy];
    sub_ady = [sub_ady, ady];
    
    sub_qdz = [sub_qdz, qdz];
    sub_vdz = [sub_vdz, vdz];
    sub_adz = [sub_adz, adz];
    
    tf_final = tf_final + tf;
end

figure;
timestamp = linspace(t0, tf_final, 100*(tf_final - t0));
subplot(3, 1, 1);
plot(timestamp, sub_qdy);
xlabel('Time (t)');
ylabel('qdy');
title('Position (qdy)');
grid on

subplot(3, 1, 2);
plot(timestamp, sub_vdy);
xlabel('Time (t)');
ylabel('vdy');
title('Velocity (vdy)');
grid on

subplot(3, 1, 3);
plot(timestamp, sub_ady);
xlabel('Time (t)');
ylabel('ady');
title('Acceleration (ady)');
grid on

figure;
timestamp = linspace(t0, tf_final, 100*(tf_final - t0));
subplot(3, 1, 1);
plot(timestamp, sub_qdx);
xlabel('Time (t)');
ylabel('qdx');
title('Position (qdx)');
grid on

subplot(3, 1, 2);
plot(timestamp, sub_vdx);
xlabel('Time (t)');
ylabel('vdx');
title('Velocity (vdx)');
grid on

subplot(3, 1, 3);
plot(timestamp, sub_adx);
xlabel('Time (t)');
ylabel('adx');
title('Acceleration (adx)');
grid on

figure;
timestamp = linspace(t0, tf_final, 100*(tf_final - t0));
subplot(3, 1, 1);
plot(timestamp, sub_qdz);
xlabel('Time (t)');
ylabel('qdz');
title('Position (qdz)');
grid on

subplot(3, 1, 2);
plot(timestamp, sub_vdz);
xlabel('Time (t)');
ylabel('vdz');
title('Velocity (vdz)');
grid on

subplot(3, 1, 3);
plot(timestamp, sub_adz);
xlabel('Time (t)');
ylabel('adz');
title('Acceleration (adz)');
grid on




