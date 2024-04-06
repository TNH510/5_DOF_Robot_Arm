% Define the quaternion
quat = [0, 0, 0.7071068, 0.7071068];  % [qr, qi, qj, qk]

 % Extract quaternion components
w = quat(1);
x = quat(2);
y = quat(3);
z = quat(4);

% Compute quaternion norm
n = w^2 + x^2 + y^2 + z^2;

% Check if the quaternion is invalid
if n == 0
    error('Invalid quaternion');
end

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
rotm = [a11 a12 a13; a21 a22 a23; a31 a32 a33];

% Display the rotation matrix
disp(rotm);