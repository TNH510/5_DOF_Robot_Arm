s = serial('COM17');
fopen(s);
fprintf(s, 'Hello');
data = fread(s, 1, 'uint8');
fclose(s);
delete(s);
clear s;

disp(data);

