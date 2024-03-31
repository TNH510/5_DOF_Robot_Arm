% ??c v� hi?n th? h�nh ?nh
image = imread('XL123.jpg');
imshow(image);

% Chuy?n ??i h�nh ?nh sang ?nh x�m
grayImage = rgb2gray(image);

% L�m m? ?nh ?? gi?m nhi?u
blurImage = imgaussfilt(grayImage, 5);

% Ph�t hi?n ???ng bi�n b?ng ph??ng ph�p Canny
edgeImage = edge(blurImage, 'Canny',[0.1,0.4]);
% T�m v� v? ???ng vi?n bao quanh v?t th?
boundaries = bwboundaries(edgeImage);
hold on;
for k = 1:length(boundaries)
    boundary = boundaries{k};
    plot(boundary(:, 2), boundary(:, 1), 'r', 'LineWidth', 2);
end
hold off;