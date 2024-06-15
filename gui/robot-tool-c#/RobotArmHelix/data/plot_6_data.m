
csvFilePath = 'H:/OneDrive - hcmute.edu.vn/Desktop/5_DOF_Robot_Arm/gui/robot-tool-c#/RobotArmHelix/data/test.csv';


data = csvread(csvFilePath);


col1 = data(:, 1);
col2 = data(:, 2);
col3 = data(:, 3);
col4 = data(:, 4);
col5 = data(:, 5);
col6 = data(:, 6);


figure;
plot(col1, 'r');
hold on;
plot(col2, 'g');
plot(col3, 'b');
plot(col4, 'm');
plot(col5, 'c');
plot(col6, 'k');
legend('Column 1', 'Column 2', 'Column 3', 'Column 4', 'Column 5', 'Column 6');
xlabel('Index');
ylabel('Value');
title('CSV Data Plot');
grid on;