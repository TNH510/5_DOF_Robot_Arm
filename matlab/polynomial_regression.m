% Load data from CSV (assuming CSV file has only x, y, z columns)
data = csvread('test.csv');  % Load your CSV file
num_samples = size(data, 1);  % Number of samples

% Generate a time vector assuming uniform sampling
t = linspace(0, 1, num_samples);  % Adjust as needed based on your data

x = data(:, 1);  % First column is x coordinate
y = data(:, 2);  % Second column is y coordinate
z = data(:, 3);  % Third column is z coordinate

% Number of points per group
group_size = 30;

% Number of groups
num_groups = floor(num_samples / group_size);

% Number of remaining points
num_remaining_points = mod(num_samples, group_size);

% Degree of the polynomial
degree = 4;  % Example: 4th degree polynomial

% Initialize figure
figure;
hold on;
xlabel('X');
ylabel('Y');
zlabel('Z');
title('3D Trajectory Visualization with Polynomial Regression for Each Group');
grid on;
legend_entries = {};

% Plot original data points
scatter3(x, y, z, 'filled', 'MarkerFaceColor', 'b');
legend_entries{end+1} = 'Original Trajectory';

cakes_per_person = distribute_cakes(total_cakes, total_people);
% Hi?n th? k?t qu?
disp('S? mi?ng bánh m?i ng??i nh?n ???c là:');
disp(cakes_per_person);

% Loop through each group
for i = 1:num_groups
    % Select data for the current group
    start_index = (i-1) * cakes_per_person(i) + 1;
    end_index = i * cakes_per_person(i);
    
    t_group = t(start_index:end_index);
    t_column = t_group'; % Chuy?n v? t thành m?t ma tr?n c?t
    x_group = x(start_index:end_index);
    y_group = y(start_index:end_index);
    z_group = z(start_index:end_index);
    
    % Perform polynomial regression for the current group
    coefficients_x = polyfit(t_column, x_group, degree);
    coefficients_y = polyfit(t_column, y_group, degree);
    coefficients_z = polyfit(t_column, z_group, degree);
    
    % Generate predicted values using polynomial equation
    t_fit = linspace(t_column(1), t_column(end), cakes_per_person(i));
    predicted_x_group = polyval(coefficients_x, t_fit);
    predicted_y_group = polyval(coefficients_y, t_fit);
    predicted_z_group = polyval(coefficients_z, t_fit);
    
    % Plot predicted trajectory for the current group
    plot3(predicted_x_group, predicted_y_group, predicted_z_group, 'LineWidth', 2);
    legend_entries{end+1} = sprintf('Group %d', i);
end

% Display legend
legend(legend_entries, 'Location', 'Best');

% Show plot
hold off;

% G?i hàm v?i s? l??ng bánh và ng??i b?t k?
total_cakes = num_samples; % B?n có th? thay ??i s? l??ng bánh
total_people = num_groups; % B?n có th? thay ??i s? l??ng ng??i

