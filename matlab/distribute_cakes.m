% H�m ?? chia b�nh
function cakes_per_person = distribute_cakes(total_cakes, total_people)
    % Chia ??u b�nh cho m?i ng??i
    base_cakes_per_person = floor(total_cakes / total_people);
    % S? b�nh c�n d? sau khi chia ??u
    remaining_cakes = mod(total_cakes, total_people);

    % T?o m?ng ?? l?u s? mi?ng b�nh m?i ng??i nh?n
    cakes_per_person = base_cakes_per_person * ones(1, total_people);

    % Ph�n ph?i s? b�nh c�n d? cho nh?ng ng??i ??u ti�n
    for i = 1:remaining_cakes
        cakes_per_person(i) = cakes_per_person(i) + 1;
    end
end