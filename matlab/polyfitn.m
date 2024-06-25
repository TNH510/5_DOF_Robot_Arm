function p = myPolyfit(x, y, n)
    % Hàm myPolyfit tìm các h? s? c?a ?a th?c b?c n sao cho kh?p v?i d? li?u x và y
    % x: vector ch?a các giá tr? x
    % y: vector ch?a các giá tr? y t??ng ?ng
    % n: b?c c?a ?a th?c c?n tìm
    
    % Ki?m tra s? l??ng ?i?m d? li?u
    if length(x) ~= length(y)
        error('x và y ph?i có cùng s? l??ng ph?n t?');
    end
    
    % S? l??ng ?i?m d? li?u
    m = length(x);
    
    % Xây d?ng ma tr?n Vandermonde
    A = zeros(m, n+1);
    for i = 0:n
        A(:, i+1) = x.^i;
    end
    
    % Gi?i h? ph??ng trình tuy?n tính A*p = y
    p = A\y;
    
    % ??o ng??c vector h? s? ?? phù h?p v?i cách tr? v? c?a hàm polyfit c?a MATLAB
    p = flipud(p);
end
