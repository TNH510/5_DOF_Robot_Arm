% COM settings
s = serial('COM20');
set(s, 'BaudRate', 115200); 
fopen(s); 

while true
    pause(0.1);

    % Check data in UART
    if s.BytesAvailable >= 10
        dataBytes = fread(s, s.BytesAvailable);

        % q0
        byte_1 = dataBytes(1);
        byte_2 = dataBytes(2);
        
        % q1
        byte_3 = dataBytes(3);
        byte_4 = dataBytes(4);
        
        % q2
        byte_5 = dataBytes(5);
        byte_6 = dataBytes(6);
        
        % q3
        byte_7 = dataBytes(7);
        byte_8 = dataBytes(8);
        
        % elbow_angle
        byte_9 = dataBytes(9);
        byte_10 = dataBytes(10);

        % Convert to actually value
        q0 = bitor(bitshift(byte_1, 8), byte_2);
        if (q0 <= 9999)
            q0 = q0 / 10000;
        else
            q0 = (q0 - 10000) * (-1) / 10000;
        end

        q1 = bitor(bitshift(byte_3, 8), byte_4);
        if (q1 <= 9999)
            q1 = q1 / 10000;
        else
            q1 = (q1 - 10000) * (-1) / 10000;
        end

        q2 = bitor(bitshift(byte_5, 8), byte_6);
        if (q2 <= 9999)
            q2 = q2 / 10000;
        else
            q2 = (q2 - 10000) * (-1) / 10000;
        end

        q3 = bitor(bitshift(byte_7, 8), byte_8);
        if (q3 <= 9999)
            q3 = q3 / 10000;
        else
            q3 = (q3 - 10000) * (-1) / 10000;
        end

        elbow = bitor(bitshift(byte_9, 8), byte_10) / 10; % rad

        q0
        q1
        q2
        q3
        elbow
    end
end
fclose(s);
delete(s);