s = serial('COM3');
set(s, 'BaudRate', 962500); 
fopen(s); 
t = 0.0;
t1_pre = -1;
t2_pre = -1; 
t3_pre = -1;
t4_pre = -1;

while true
    pause(0.01);
    dt = 0.01;
    t = t + 0.01;
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

        t1 = bitor(bitshift(byte_1, 8), byte_2) / 100 - 180
        t2 = bitor(bitshift(byte_3, 8), byte_4) / 100 - 180 + 90
        t3 = bitor(bitshift(byte_5, 8), byte_6) / 100 - 180 - 90
        t4 = bitor(bitshift(byte_7, 8), byte_8) / 100 - 180 - 90
        t5 = bitor(bitshift(byte_9, 8), byte_10) / 100 - 180

        if t1 == 65535
            break;
        end
        
        set_param('Complete/Slider Gain','Gain',num2str(t1));
        set_param('Complete/Slider Gain1','Gain',num2str(t2));
        set_param('Complete/Slider Gain2','Gain',num2str(t3));
        set_param('Complete/Slider Gain3','Gain',num2str(t4));
        set_param('Complete/Slider Gain4','Gain',num2str(t5));
        
        w1 = (t1 - t1_pre) / dt;
        w2 = (t2 - t2_pre) / dt;
        w3 = (t3 - t3_pre) / dt;
        w4 = (t4 - t4_pre) / dt;
        
        t1_pre = t1;
        t2_pre = t2;
        t3_pre = t3;
        t4_pre = t4;
        
        
        %Plot for drawing
        subplot(4,2,1);
        plot(t,t1,'.'); xlabel('time'); ylabel('angle 1'); title('Graph of theta1')
        hold on
        grid on
        %Plot for drawing
        subplot(4,2,2);
        plot(t,t2,'.'); xlabel('time'); ylabel('angle 2'); title('Graph of theta2')
        hold on
        grid on
        %Plot for drawing
        subplot(4,2,3);
        plot(t,t3,'.'); xlabel('time'); ylabel('angle 3'); title('Graph of theta3')
        hold on
        grid on
        %Plot for drawing
        subplot(4,2,4);
        plot(t,t4,'.'); xlabel('time'); ylabel('angle 4'); title('Graph of theta4')
        hold on
        grid on
        
        if (t1_pre ~= -1)
            %Plot for drawing
            subplot(4,2,5);
            plot(t,w1,'.'); xlabel('time'); ylabel('omega 1'); title('Graph of omega1')
            hold on
            grid on
            %Plot for drawing
            subplot(4,2,6);
            plot(t,w2,'.'); xlabel('time'); ylabel('omega 2'); title('Graph of omega2')
            hold on
            grid on
            %Plot for drawing
            subplot(4,2,7);
            plot(t,w3,'.'); xlabel('time'); ylabel('omega 3'); title('Graph of omega3')
            hold on
            grid on
            %Plot for drawing
            subplot(4,2,8);
            plot(t,w4,'.'); xlabel('time'); ylabel('omega 4'); title('Graph of omega4')
            hold on
            grid on
        end
    end
end

% function MoveL(hObject, event, handles)

% end

fclose(s);
delete(s);