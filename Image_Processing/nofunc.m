I = imread('XL123.jpg');
I = rgb2gray(I);
sigma = 10; % ?? l?n c?a b? l?c Gaussian
H = fspecial('gaussian', [5 5], sigma);
blurredImage = imfilter(I, H, 'replicate');
% Sobel operators
SobelX = [-1 0 1; -2 0 2; -1 0 1];
SobelY = [-1 -2 -1; 0 0 0; 1 2 1];

% Tính toán gradient theo h??ng x và y
gradientX = imfilter(blurredImage, SobelX, 'replicate');
gradientY = imfilter(blurredImage, SobelY, 'replicate');

gradientX = double(gradientX);
gradientY = double(gradientY);
gradientMagnitude = sqrt(gradientX.^2 + gradientY.^2);
gradientAngle = atan2(gradientY, gradientX);
lowThreshold = 0.2; % Ng??ng d??i
highThreshold = 0.4; % Ng??ng trên

% Xác ??nh các ?i?m ?nh "m?nh" và "y?u"
strongEdges = gradientMagnitude > highThreshold;
weakEdges = (gradientMagnitude >= lowThreshold) & (gradientMagnitude <= highThreshold);

% T?o ???ng vi?n k?t qu?
edges = strongEdges;

% Liên k?t các ?i?m y?u v?i các ?i?m m?nh lân c?n
[rows, cols] = size(edges);
for i = 2 : rows - 1
    for j = 2 : cols - 1
        if weakEdges(i, j)
            if any(strongEdges(i-1:i+1, j-1:j+1))
                edges(i, j) = 1;
            end
        end
    end
end
imshow(edges);