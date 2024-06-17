function model = polyfitn(X, y, n)
    % X: Ma tr?n k�ch th??c [m x 2] v?i m l� s? ?i?m d? li?u
    % y: Vector k�ch th??c [m x 1] ch?a c�c gi� tr? m?c ti�u
    % n: B?c c?a ?a th?c
    % model: C?u tr�c ch?a c�c h? s? c?a ?a th?c
    
    % S? ?i?m d? li?u
    m = size(X, 1);
    
    % T�nh s? l??ng h? s? c?a ?a th?c b?c n trong kh�ng gian 2 chi?u
    numCoeffs = (n + 1) * (n + 2) / 2;
    
    % X�y d?ng ma tr?n thi?t k?
    A = zeros(m, numCoeffs);
    idx = 1;
    for i = 0:n
        for j = 0:i
            A(:, idx) = (X(:, 1).^(i-j)) .* (X(:, 2).^j);
            idx = idx + 1;
        end
    end
    
    % Gi?i h? ph??ng tr�nh tuy?n t�nh ?? t�m c�c h? s?
    coeffs = A \ y;
    
    % Tr? v? c�c h? s? trong m?t c?u tr�c
    model.coeffs = coeffs;
    model.n = n;
end
