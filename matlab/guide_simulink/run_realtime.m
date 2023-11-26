s = serial('COM25'); 
set(s, 'BaudRate', 115200); 
fopen(s); 

% tm = timer('ExecutionMode', 'FixedRate', 'Period', 0.1, 'TimerFcn', {@update, handles});
% start(tm);

while true
    pause(0.1);
    if s.BytesAvailable > 0 
        dataBytes = fread(s, s.BytesAvailable); 
        % dataBytes = [01, 01];

        byte_1 = dataBytes(1);
        byte_2 = dataBytes(2);
        
        byte_3 = dataBytes(3);
        byte_4 = dataBytes(4);
        
        byte_5 = dataBytes(5);
        byte_6 = dataBytes(6);
        
        byte_7 = dataBytes(7);
        byte_8 = dataBytes(8);
        
        byte_9 = dataBytes(9);
        byte_10 = dataBytes(10);

        t1 = bitor(bitshift(byte_1, 8), byte_2) / 100
        t2 = bitor(bitshift(byte_3, 8), byte_4) / 100
        t3 = bitor(bitshift(byte_5, 8), byte_6) / 100
        t4 = bitor(bitshift(byte_7, 8), byte_8) / 100
        t5 = bitor(bitshift(byte_9, 8), byte_10) / 100
        
        set_param('Complete/Slider Gain','Gain',num2str(t1));
        set_param('Complete/Slider Gain1','Gain',num2str(t2));
        set_param('Complete/Slider Gain2','Gain',num2str(t3));
        set_param('Complete/Slider Gain3','Gain',num2str(t4));
        set_param('Complete/Slider Gain4','Gain',num2str(t5));

        % dec2hex(t1)
        % dec2hex(t2)
        % dec2hex(t3)
        % dec2hex(t4)
        % dec2hex(t5)
    end
end

% function MoveL(hObject, event, handles)

% end

fclose(s);
delete(s);