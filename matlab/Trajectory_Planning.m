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
num_points = 100;  % Number of points for linspace
% Initialize qdx, vdx, and adx
% Initialize qdx to an empty matrix
qdx_expand = [];
vdx_expand = [];
adx_expand = [];

qdy_expand = [];
vdy_expand = [];
ady_expand = [];

qdz_expand = [];
vdz_expand = [];
adz_expand = [];
for traj_loop = 1:3
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
    
    % Generate time vector
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

    % Update old values for the next iteration
    tf_old = tf;
    
    % Assuming qdx_expand is a 1x500 matrix and qdx is a 1x300 matrix
    num_columns_needed = max(size(qdx_expand, 2), size(qdx, 2));
    % Pad qdx_expand with zeros if needed
    qdx_expand = [qdx_expand, zeros(size(qdx_expand, 1), num_columns_needed - size(qdx_expand, 2))];
    % Pad qdx with zeros if needed
    qdx = [qdx, zeros(size(qdx, 1), num_columns_needed - size(qdx, 2))];
    % Assuming qdx_expand is a 1x500 matrix and qdx is a 1x300 matrix
    num_columns_needed2 = max(size(vdx_expand, 2), size(vdx, 2));
    % Pad qdx_expand with zeros if needed
    vdx_expand = [vdx_expand, zeros(size(vdx_expand, 1), num_columns_needed - size(vdx_expand, 2))];
    % Pad qdx with zeros if needed
    vdx = [vdx, zeros(size(vdx, 1), num_columns_needed - size(vdx, 2))];
    % Assuming qdx_expand is a 1x500 matrix and qdx is a 1x300 matrix
    num_columns_needed3 = max(size(adx_expand, 2), size(adx, 2));
    % Pad dx_expand with zeros if needed
    adx_expand = [adx_expand, zeros(size(adx_expand, 1), num_columns_needed - size(adx_expand, 2))];
    % Pad adx with zeros if needed
    adx = [adx, zeros(size(adx, 1), num_columns_needed - size(adx, 2))];
    % Expand the matrix A to store more data
    qdx_expand = [qdx_expand; qdx];
    vdx_expand = [vdx_expand; vdx];
    adx_expand = [adx_expand; adx];

    % Assuming qdx_expand is a 1x500 matrix and qdx is a 1x300 matrix
    num_columns_needed4 = max(size(qdy_expand, 2), size(qdy, 2));
    % Pad qdx_expand with zeros if needed
    qdy_expand = [qdy_expand, zeros(size(qdy_expand, 1), num_columns_needed - size(qdy_expand, 2))];
    % Pad qdx with zeros if needed
    qdy = [qdy, zeros(size(qdy, 1), num_columns_needed - size(qdy, 2))];
    % Assuming qdy_expand is a 1x500 matrix and qdy is a 1x300 matrix
    num_columns_needed5 = max(size(vdy_expand, 2), size(vdy, 2));
    % Pad qdy_expand with zeros if needed
    vdy_expand = [vdy_expand, zeros(size(vdy_expand, 1), num_columns_needed - size(vdy_expand, 2))];
    % Pad qdy with zeros if needed
    vdy = [vdy, zeros(size(vdy, 1), num_columns_needed - size(vdy, 2))];
    % Assuming qdy_expand is a 1x500 matrix and qdy is a 1x300 matrix
    num_columns_needed6 = max(size(ady_expand, 2), size(ady, 2));
    % Pad dx_expand with zeros if needed
    ady_expand = [ady_expand, zeros(size(ady_expand, 1), num_columns_needed - size(ady_expand, 2))];
    % Pad ady with zeros if needed
    ady = [ady, zeros(size(ady, 1), num_columns_needed - size(ady, 2))];    
    % Expand the matrix A to store more data
    qdy_expand = [qdy_expand; qdy];
    vdy_expand = [vdy_expand; vdy];
    ady_expand = [ady_expand; ady];
   
    
    % Assuming qdx_expand is a 1x500 matrix and qdx is a 1x300 matrix
    num_columns_needed7 = max(size(qdz_expand, 2), size(qdz, 2));
    % Pad qdx_expand with zeros if needed
    qdz_expand = [qdz_expand, zeros(size(qdz_expand, 1), num_columns_needed - size(qdz_expand, 2))];
    % Pad qdx with zeros if needed
    qdz = [qdz, zeros(size(qdz, 1), num_columns_needed - size(qdz, 2))];
    % Assuming qdy_expand is a 1x500 matrix and qdy is a 1x300 matrix
    num_columns_needed8 = max(size(vdz_expand, 2), size(vdz, 2));
    % Pad qdy_expand with zeros if needed
    vdz_expand = [vdz_expand, zeros(size(vdz_expand, 1), num_columns_needed - size(vdz_expand, 2))];
    % Pad qdy with zeros if needed
    vdz = [vdz, zeros(size(vdz, 1), num_columns_needed - size(vdz, 2))];
    % Assuming qdy_expand is a 1x500 matrix and qdy is a 1x300 matrix
    num_columns_needed9 = max(size(adz_expand, 2), size(adz, 2));
    % Pad dx_expand with zeros if needed
    adz_expand = [adz_expand, zeros(size(adz_expand, 1), num_columns_needed - size(adz_expand, 2))];
    % Pad ady with zeros if needed
    adz = [adz, zeros(size(adz, 1), num_columns_needed - size(adz, 2))];    
    % Expand the matrix A to store more data
    qdz_expand = [qdz_expand; qdz];
    vdz_expand = [vdz_expand; vdz];
    adz_expand = [adz_expand; adz];
    
end
% qdx_reshaped = reshape(qdx_expand, 1, []);

% % Plot all three functions for x
figure;
for n = 0:2
    % Assuming qdx_expand is a 3x500 matrix
    [~, num_columns] = size(qdx_expand);
    % Assuming qdx_expand is a 3x500 matrix
    t = linspace(t0 + n*tf, tf + n*tf, num_columns);
    subplot(3, 1, 1);
    plot(t, qdx_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('qdx');
    title('Position (qdx)');
    hold on
    grid on
    
    subplot(3, 1, 2);
    plot(t, vdx_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('vdx');
    title('Velocity (vdx)');
    hold on
    grid on
    
    subplot(3, 1, 3);
    plot(t, adx_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('adx');
    title('Acceleration (adx)');
    hold on
    grid on
end
suptitle('Plot of qdx, vdx, and adx');
% Plot all three functions for y
figure;
for n = 0:2
    % Assuming qdy_expand is a 3x500 matrix
    [~, num_columns] = size(qdy_expand);
    % Assuming qdy_expand is a 3x500 matrix
    t = linspace(t0 + n*tf, tf + n*tf, num_columns);
    subplot(3, 1, 1);
    plot(t, qdy_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('qdy');
    title('Position (qdy)');
    hold on
    grid on
    
    subplot(3, 1, 2);
    plot(t, vdy_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('vdy');
    title('Velocity (vdy)');
    hold on
    grid on
    
    subplot(3, 1, 3);
    plot(t, ady_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('ady');
    title('Acceleration (ady)');
    hold on
    grid on
end
suptitle('Plot of qdy, vdy, and ady');

% Plot all three functions for y
figure;
for n = 0:2
    % Assuming qdy_expand is a 3x500 matrix
    [~, num_columns] = size(qdz_expand);
    % Assuming qdy_expand is a 3x500 matrix
    t = linspace(t0 + n*tf, tf + n*tf, num_columns);
    subplot(3, 1, 1);
    plot(t, qdz_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('qdz');
    title('Position (qdz)');
    hold on
    grid on
    
    subplot(3, 1, 2);
    plot(t, vdz_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('vdz');
    title('Velocity (vdz)');
    hold on
    grid on
    
    subplot(3, 1, 3);
    plot(t, adz_expand((n+1), :));
    xlabel('Time (t)');
    ylabel('adz');
    title('Acceleration (adz)');
    hold on
    grid on
end
suptitle('Plot of qdz, vdz, and adz');

% % Plot all three functions for z
% figure;
% 
% subplot(3, 1, 1);
% plot(t, qdz);
% xlabel('Time (t)');
% ylabel('qdz');
% title('Position (qdz)');
% 
% subplot(3, 1, 2);
% plot(t, vdz);
% xlabel('Time (t)');
% ylabel('vdz');
% title('Velocity (vdz)');
% 
% subplot(3, 1, 3);
% plot(t, adz);
% xlabel('Time (t)');
% ylabel('adz');
% title('Acceleration (adz)');
% 
% suptitle('Plot of qdz, vdz, and adz');




