% ??c ?nh
I = imread('BOX4.jpg');

% Chuy?n ??i ?nh sang ?nh ?en tr?ng
BW = rgb2gray(I);

% Ph�t hi?n bi�n c?nh
edges = edge(BW, 'Canny');

% T�m c�c ???ng bi�n ?�ng vi?n
filledEdges = imfill(edges, 'holes');

% T�m v� v? vi?n bao quanh v?t th?
boundaries = bwboundaries(filledEdges);
figure;
imshow(I);
hold on;
for k = 1:length(boundaries)
    boundary = boundaries{k};
    plot(boundary(:, 2), boundary(:, 1), 'r', 'LineWidth', 2);
end
hold off;