xA = 30; yA = 30; zA = 30;
xB = 70; yB = 70; zB = 30;
l1 = 40; l2 = 50; l3 = 40;
for t = 0:0.01:1
    x = xA + (xB - xA)*t;
    y = yA + (yB - yA)*t;
    z = zA + (zB - zA)*t;
    
    t1 = atan2(-x,y);
    A = -x/sin(t1);
    B = z - l1;
    c3 = (A^2 + B^2 - l2^2 - l3^2)/(2*l2*l3);
    s3 = sqrt(abs(1 - c3^2));
    t3 = atan2(s3,c3); %angle 3
    
    c2 = A*(l3*c3 + l2) + B*(l3*s3);
    s2 = B*(l3*c3 + l2) - A*(l3*s3);
    t2 = atan2(s2,c2); %angle 2
    Px = -sin(t1)*(l3*cos(t2 + t3) + l2*cos(t2));
    Py = cos(t1)*(l3*cos(t2 + t3) + l2*cos(t2));
    Pz = l1 + l3*sin(t2 + t3) + l2*sin(t2);

    subplot(2,2,1);
    plot3(Px,Py,Pz,'.'); xlabel('Px'); ylabel('Py'); zlabel('Pz');
    hold on
    grid on
    subplot(2,2,2);
    plot(t,t1*(180/pi),'.'); xlabel('t'); ylabel('t1');
    hold on
    grid on
    subplot(2,2,3);
    plot(t,t2*(180/pi),'.'); xlabel('t'); ylabel('t2');
    hold on
    grid on
    subplot(2,2,4);
    plot(t,t3*(180/pi),'.'); xlabel('t'); ylabel('t3');
    hold on
    grid on
end
    
    