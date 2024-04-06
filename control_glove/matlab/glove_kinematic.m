syms w x y z n l1 l2 ce se
% syms l1 l2 

% w = 0.5
% x = 0.5
% y = 0.5
% z = 0.5
% ce = 1
% se = 0

% Compute quaternion norm
n = w^2 + x^2 + y^2 + z^2;

% Compute rotation matrix elements
a11 = (w^2 + x^2 - y^2 - z^2) / n;
a12 = 2 * (x*y - w*z) / n;
a13 = 2 * (x*z + w*y) / n;

a21 = 2 * (x*y + w*z) / n;
a22 = (w^2 - x^2 + y^2 - z^2) / n;
a23 = 2 * (y*z - w*x) / n;

a31 = 2 * (x*z - w*y) / n;
a32 = 2 * (y*z + w*x) / n;
a33 = (w^2 - x^2 - y^2 + z^2) / n;

% Create the rotation matrix
rotm = [a11 a12 a13 0; a21 a22 a23 0; a31 a32 a33 0; 0 0 0 1];
T1 = [1 0 0 l1; 0 1 0 0; 0 0 1 0; 0 0 0 1]
R2 = [ce -se 0 0; se ce 0 0; 0 0 1 0; 0 0 0 1]
T2 = [1 0 0 l2; 0 1 0 0; 0 0 1 0; 0 0 0 1]
P  = [0; 0; 0; 1]

result = rotm * T1 * R2 * T2 * P