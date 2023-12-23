s = serial('COM17');
set(s, 'BaudRate', 962500); 
fopen(s); 
t = 0.0;
t1_pre = -1;
t2_pre = -1; 
t3_pre = -1;
t4_pre = -1;
l1=690;
l2=440;
l3=500;
l5=230;

while true
    pause(0.01);
    dt = 0.01;
    t = t + 0.01;
    if s.BytesAvailable > 0 
        dataBytes = fread(s, s.BytesAvailable); 

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

        px=cosd(t1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
        py=sind(t1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
        pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
        
        %Plot for drawing
        subplot(3,2,1);
        plot(t,t1,'.r'); xlabel('time'); ylabel('angle 1'); title('Graph of theta1');
        xlim([t-2, t]); 
        hold on
        grid on
        %Plot for drawing
        subplot(3,2,2);
        plot(t,t2,'.g'); xlabel('time'); ylabel('angle 2'); title('Graph of theta2');
        xlim([t-2, t]); 
        hold on
        grid on
        %Plot for drawing
        subplot(3,2,3);
        plot(t,t3,'.b'); xlabel('time'); ylabel('angle 3'); title('Graph of theta3');
        xlim([t-2, t]); 
        hold on
        grid on
        
        %Plot for drawing
        subplot(3,2,4);
        plot(t,t4,'.r'); xlabel('time'); ylabel('angle 4'); title('Graph of theta4');
        xlim([t-2, t]); 
        hold on
        grid on

        subplot(3,2,5);
        plot(px,py,'.k'); xlabel('x'); ylabel('y'); title('Graph of Oxy')
        hold on
        grid on

        subplot(3,2,6);
        plot3(px,py, pz,'.m'); xlabel('x'); ylabel('y'); zlabel('z'); title('Graph of Oxyz')
        hold on
        grid on
    end
end

% function MoveL(hObject, event, handles)

% end

fclose(s);
delete(s);