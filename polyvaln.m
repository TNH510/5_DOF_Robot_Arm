function y_fit = polyvaln(model, X)
    % model: C?u tr�c ch?a c�c h? s? c?a ?a th?c
    % X: Ma tr?n k�ch th??c [m x 2] v?i m l� s? ?i?m d? li?u m?i
    % y_fit: Vector k�ch th??c [m x 1] ch?a c�c gi� tr? d? ?o�n
    
    % S? ?i?m d? li?u m?i
    m = size(X, 1);
    
    % T�nh s? l??ng h? s? c?a ?a th?c b?c n trong kh�ng gian 2 chi?u
    numCoeffs = (model.n + 1) * (model.n + 2) / 2;
    
    % X�y d?ng ma tr?n thi?t k? cho c�c ?i?m m?i
    A = zeros(m, numCoeffs);
    idx = 1;
    for i = 0:model.n
        for j = 0:i
            A(:, idx) = (X(:, 1).^(i-j)) .* (X(:, 2).^j);
            idx = idx + 1;
        end
    end
    
    % T�nh to�n gi� tr? d? ?o�n
    y_fit = A * model.coeffs;
end
