% Hàm ?? chia bánh
function cakes_per_person = distribute_cakes(total_cakes, total_people)
    % Chia ??u bánh cho m?i ng??i
    base_cakes_per_person = floor(total_cakes / total_people);
    % S? bánh còn d? sau khi chia ??u
    remaining_cakes = mod(total_cakes, total_people);

    % T?o m?ng ?? l?u s? mi?ng bánh m?i ng??i nh?n
    cakes_per_person = base_cakes_per_person * ones(1, total_people);

    % Phân ph?i s? bánh còn d? cho nh?ng ng??i ??u tiên
    for i = 1:remaining_cakes
        cakes_per_person(i) = cakes_per_person(i) + 1;
    end
end