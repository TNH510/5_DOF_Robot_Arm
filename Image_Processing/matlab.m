I=imread('XL123.jpg');
L=I(:,:,1);
phuongphap = 'Canny';
nguong = [0.2 0.4];
huong = 'horizontal';
BW = edge(L, phuongphap, nguong, huong);
figure;
subplot(1, 2, 1);
imshow(I);
title('?nh g?c');
subplot(1, 2, 2);
imshow(BW);
title('Biên c?nh');
[H,theta,rho] = hough(BW);
P = houghpeaks(H, 6);
lines = houghlines(BW, theta, rho, P);
figure;
imshow(I);
hold on;
for k = 1:length(lines)
    xy = [lines(k).point1; lines(k).point2];
    plot(xy(:,1), xy(:,2), 'LineWidth', 2, 'Color', 'r');
    
    % Tính toán góc nghiêng c?a ???ng th?ng
    angle = atan2d(lines(k).point2(2) - lines(k).point1(2), lines(k).point2(1) - lines(k).point1(1));
    fprintf('Góc nghiêng c?a ???ng th?ng %d: %.2f ??\n', k, angle);
end
hold off;