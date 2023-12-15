sym x
L1= 95.5;
R1=36.25;
% T?o m?ng giá tr?
theta_values = linspace(0, pi, 100);

% Kh?i t?o m?ng l?u giá tr? 
x_max_solutions = zeros(size(theta_values));

% Gi?i ph??ng trình giá tr? theta
for i = 1:length(theta_values)
    % H? s? trong ph??ng trình
    a = 1;
    b = -2 * cos(theta_values(i))* R1;
    c = -(L1^2-R1^2);

    % Gi?i ph??ng trình b?c 2
    roots_result = roots([a, b, c]);

    % Ll?y nghi?m th?c 
    real_roots = real(roots_result);
    real_roots = real_roots(imag(roots_result) == 0);

    % N?u có 2 nghi?p th?c
    if ~isempty(real_roots)
        % Ll?y nghi?m l?n nh?t 
        max_real_root = max(real_roots);

        % L?u nghi?m
        x_max_solutions(i) = max_real_root;
    end
end
% V? ?? th?
plot(theta_values,(131.75-x_max_solutions));
title('?? th? nghi?m th?c l?n nh?t c?a x theo theta');
xlabel('Theta');
ylabel('Nghi?m th?c l?n nh?t x');
grid on;
% Kh?i t?o m?ng l?u giá tr?
F_alpha_values = zeros(size(theta_values));

for i = 5:length(theta_values)-4
    F=20/cos(pi/2-theta_values(i));
    % L?u giá tr? vào m?ng
    F_alpha_values(i) = F;
end

figure;
plot(theta_values, F_alpha_values);
title('L?c c?n thi?t ?? k?p theo theta');
xlabel('Theta');
ylabel('F');
grid on;
