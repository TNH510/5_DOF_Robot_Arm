function model = polyfitn(X, y, n)
    % X: Ma tr?n kích th??c [m x 2] v?i m là s? ?i?m d? li?u
    % y: Vector kích th??c [m x 1] ch?a các giá tr? m?c tiêu
    % n: B?c c?a ?a th?c
    % model: C?u trúc ch?a các h? s? c?a ?a th?c
    
    % S? ?i?m d? li?u
    m = size(X, 1);
    
    % Tính s? l??ng h? s? c?a ?a th?c b?c n trong không gian 2 chi?u
    numCoeffs = (n + 1) * (n + 2) / 2;
    
    % Xây d?ng ma tr?n thi?t k?
    A = zeros(m, numCoeffs);
    idx = 1;
    for i = 0:n
        for j = 0:i
            A(:, idx) = (X(:, 1).^(i-j)) .* (X(:, 2).^j);
            idx = idx + 1;
        end
    end
    
    % Gi?i h? ph??ng trình tuy?n tính ?? tìm các h? s?
    coeffs = A \ y;
    
    % Tr? v? các h? s? trong m?t c?u trúc
    model.coeffs = coeffs;
    model.n = n;
end
