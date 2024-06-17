function y_fit = polyvaln(model, X)
    % model: C?u trúc ch?a các h? s? c?a ?a th?c
    % X: Ma tr?n kích th??c [m x 2] v?i m là s? ?i?m d? li?u m?i
    % y_fit: Vector kích th??c [m x 1] ch?a các giá tr? d? ?oán
    
    % S? ?i?m d? li?u m?i
    m = size(X, 1);
    
    % Tính s? l??ng h? s? c?a ?a th?c b?c n trong không gian 2 chi?u
    numCoeffs = (model.n + 1) * (model.n + 2) / 2;
    
    % Xây d?ng ma tr?n thi?t k? cho các ?i?m m?i
    A = zeros(m, numCoeffs);
    idx = 1;
    for i = 0:model.n
        for j = 0:i
            A(:, idx) = (X(:, 1).^(i-j)) .* (X(:, 2).^j);
            idx = idx + 1;
        end
    end
    
    % Tính toán giá tr? d? ?oán
    y_fit = A * model.coeffs;
end
