function p = myPolyfit(x, y, n)
    % H�m myPolyfit t�m c�c h? s? c?a ?a th?c b?c n sao cho kh?p v?i d? li?u x v� y
    % x: vector ch?a c�c gi� tr? x
    % y: vector ch?a c�c gi� tr? y t??ng ?ng
    % n: b?c c?a ?a th?c c?n t�m
    
    % Ki?m tra s? l??ng ?i?m d? li?u
    if length(x) ~= length(y)
        error('x v� y ph?i c� c�ng s? l??ng ph?n t?');
    end
    
    % S? l??ng ?i?m d? li?u
    m = length(x);
    
    % X�y d?ng ma tr?n Vandermonde
    A = zeros(m, n+1);
    for i = 0:n
        A(:, i+1) = x.^i;
    end
    
    % Gi?i h? ph??ng tr�nh tuy?n t�nh A*p = y
    p = A\y;
    
    % ??o ng??c vector h? s? ?? ph� h?p v?i c�ch tr? v? c?a h�m polyfit c?a MATLAB
    p = flipud(p);
end
