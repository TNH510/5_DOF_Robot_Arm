% ??c và hi?n th? hình ?nh
image = imread('XL123.jpg');
imshow(image);

% Chuy?n ??i hình ?nh sang ?nh xám
grayImage = rgb2gray(image);

% Làm m? ?nh ?? gi?m nhi?u
blurImage = imgaussfilt(grayImage, 5);

% Phát hi?n ???ng biên b?ng ph??ng pháp Canny
edgeImage = edge(blurImage, 'Canny',[0.1,0.4]);
% Tìm và v? ???ng vi?n bao quanh v?t th?
boundaries = bwboundaries(edgeImage);
hold on;
for k = 1:length(boundaries)
    boundary = boundaries{k};
    plot(boundary(:, 2), boundary(:, 1), 'r', 'LineWidth', 2);
end
hold off;