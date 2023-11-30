t1 = 0;
t2 = 0;
t3 = 0;
t4 = 0;
t5 = 0;
% while true
%     t1 = t1 + 1;
%     t2 = t2 + 1;
%     t3 = t3 + 1;
%     t4 = t4 + 1;
%     t5 = t5 + 1;
%     set_param('Complete/Slider Gain','Gain',num2str(t1));
%     set_param('Complete/Slider Gain1','Gain',num2str(t2));
%     set_param('Complete/Slider Gain2','Gain',num2str(t3));
%     set_param('Complete/Slider Gain3','Gain',num2str(t4));
%     set_param('Complete/Slider Gain4','Gain',num2str(t5));
%     pause(0.1);
% end

s = serial('COM17'); 
set(s, 'BaudRate', 9600); 
fopen(s); 

figure;
plotData = []; 
while true
    if s.BytesAvailable > 0 
        dataBytes = fread(s, s.BytesAvailable); 

        % numBytes = length(dataBytes);
        % numIntegers = numBytes / 2;
        % dataIntegers = zeros(numIntegers, 1, 'int16');

        % for i = 1:numIntegers
        %     startIndex = 2 * (i - 1) + 1;
        %     dataIntegers(i) = typecast(uint8(dataBytes(startIndex:startIndex+1)), 'int16');
        % end

        set_param('Complete/Slider Gain','Gain',num2str(dataIntegers));
        pause(0.1);
    end
end

fclose(s);
delete(s);