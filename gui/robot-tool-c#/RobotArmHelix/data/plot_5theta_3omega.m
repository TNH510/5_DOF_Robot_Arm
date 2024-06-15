csvFilePath = 'H:/OneDrive - hcmute.edu.vn/Desktop/5_DOF_Robot_Arm/gui/robot-tool-c#/RobotArmHelix/data/5theta_3vel.csv';

data = csvread(csvFilePath);

theta1 = data(:, 1);
theta2 = data(:, 2);
theta3 = data(:, 3);
theta4 = data(:, 4);
theta5 = data(:, 5);
omega1 = data(:, 6);
omega2 = data(:, 7);
omega3 = data(:, 8);

figure;
plot(theta1, 'r');
hold on;
plot(theta2, 'g');
plot(theta3, 'b');
plot(theta4, 'm');
plot(theta5, 'c');
plot(omega1, 'k');
plot(omega2, 'y');
plot(omega3, 'Color', [0.5 0.5 0.5]);
legend('Theta1', 'Theta2', 'Theta3', 'Theta4', 'Theta5', 'Omega1', 'Omega2', 'Omega3');
xlabel('Index');
ylabel('Value');
title('CSV Data Plot');
grid on;