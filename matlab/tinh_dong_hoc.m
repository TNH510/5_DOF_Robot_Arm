syms th a1 d a th1 th2 th3 th4 th5 l1 l2 l3 l4 l5 l6 l7 l8

%Tii1 = [cos(th) -sin(th)*cos(a1) sin(th)*sin(a1) a*cos(th); sin(th) cos(th)*cos(a1) -cos(th)*sin(a1) a*sin(th); 0 sin(a1) cos(a1) d; 0 0 0 1]

%Tii1 =
%[ cos(th), -cos(a1)*sin(th),  sin(a1)*sin(th), a*cos(th)]
%[ sin(th),  cos(a1)*cos(th), -sin(a1)*cos(th), a*sin(th)]
%[       0,          sin(a1),          cos(a1),         d]
%[       0,                0,                0,         1]


%T01 =
 
[ cos(th1), 0,  sin(th1),  0]
[ sin(th1), 0, -cos(th1),  0]
[        0, 1,         0, l1]
[        0, 0,         0,  1]

%T12 =
 
[ cos(th2),  sin(th2),  0, l3*cos(th2)]
[ sin(th2), -cos(th2),  0, l3*sin(th2)]
[        0,         0, -1,     l2 - l4]
[        0,         0,  0,           1]

%T23 =
 
[ cos(th3),  sin(th3),  0, l5*cos(th3)]
[ sin(th3), -cos(th3),  0, l5*sin(th3)]
[        0,         0, -1,         -l6]
[        0,         0,  0,           1]

%T34 =
 
[ cos(th4), 0,  sin(th4), 0]
[ sin(th4), 0, -cos(th4), 0]
[        0, 1,         0, 0]
[        0, 0,         0, 1]


%T45 =
 
[ cos(th5), -sin(th5), 0,       0]
[ sin(th5),  cos(th5), 0,       0]
[        0,         0, 1, l7 + l8]
[        0,         0, 0,       1]


px = l6*sin(th1) + sin(th1)*(l2 - l4) + l3*cos(th1)*cos(th2) + sin(th2 - th3 + th4)*cos(th1)*(l7 + l8) + l5*cos(th1)*cos(th2)*cos(th3) + l5*cos(th1)*sin(th2)*sin(th3)
 
py = sin(th2 - th3 + th4)*sin(th1)*(l7 + l8) - cos(th1)*(l2 - l4) - l6*cos(th1) + l3*cos(th2)*sin(th1) + l5*cos(th2)*cos(th3)*sin(th1) + l5*sin(th1)*sin(th2)*sin(th3)
 
pz = l1 - l7*cos(th2 - th3 + th4) - l8*cos(th2 - th3 + th4) + l3*sin(th2) + l5*sin(th2 - th3)
 

%FK =
 
%[ r11, r12, r13, x]
%[ r21, r22, r23, y]
%[ r31, r32, r33, z]
%[   0,   0,   0, 1]

%T10 =
 
[ cos(th1)/(cos(th1)^2 + sin(th1)^2),  sin(th1)/(cos(th1)^2 + sin(th1)^2), 0,   0]
[                                  0,                                   0, 1, -l1]
[ sin(th1)/(cos(th1)^2 + sin(th1)^2), -cos(th1)/(cos(th1)^2 + sin(th1)^2), 0,   0]
[                                  0,                                   0, 0,   1]

%T10FK =
 
[ (r11*cos(th1))/(cos(th1)^2 + sin(th1)^2) + (r21*sin(th1))/(cos(th1)^2 + sin(th1)^2), (r12*cos(th1))/(cos(th1)^2 + sin(th1)^2) + (r22*sin(th1))/(cos(th1)^2 + sin(th1)^2), (r13*cos(th1))/(cos(th1)^2 + sin(th1)^2) + (r23*sin(th1))/(cos(th1)^2 + sin(th1)^2), (x*cos(th1))/(cos(th1)^2 + sin(th1)^2) + (y*sin(th1))/(cos(th1)^2 + sin(th1)^2)]
[                                                                                 r31,                                                                                 r32,                                                                                 r33,                                                                          z - l1]
[ (r11*sin(th1))/(cos(th1)^2 + sin(th1)^2) - (r21*cos(th1))/(cos(th1)^2 + sin(th1)^2), (r12*sin(th1))/(cos(th1)^2 + sin(th1)^2) - (r22*cos(th1))/(cos(th1)^2 + sin(th1)^2), (r13*sin(th1))/(cos(th1)^2 + sin(th1)^2) - (r23*cos(th1))/(cos(th1)^2 + sin(th1)^2), (x*sin(th1))/(cos(th1)^2 + sin(th1)^2) - (y*cos(th1))/(cos(th1)^2 + sin(th1)^2)]
[                                                                                   0,                                                                                   0,                                                                                   0,                                                                               1]
 

%T12T23T34T45 =
 
[  cos(th5)*(cos(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)) + sin(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2))), -sin(th5)*(cos(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)) + sin(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2))),   sin(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)) - cos(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)), l3*cos(th2) - (cos(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)) - sin(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)))*(l7 + l8) + l5*cos(th2)*cos(th3) + l5*sin(th2)*sin(th3)]
[ -cos(th5)*(cos(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)) - sin(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3))),  sin(th5)*(cos(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)) - sin(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3))), - cos(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)) - sin(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)), l3*sin(th2) - (cos(th4)*(cos(th2)*cos(th3) + sin(th2)*sin(th3)) + sin(th4)*(cos(th2)*sin(th3) - cos(th3)*sin(th2)))*(l7 + l8) - l5*cos(th2)*sin(th3) + l5*cos(th3)*sin(th2)]
[                                                                                                        sin(th5),                                                                                                        cos(th5),                                                                                                     0,                                                                                                                                                                l2 - l4 + l6]
[                                                                                                               0,                                                                                                               0,                                                                                                     0,                                                                                                                                                                           1]
 

%n1 = x*cos(th1) + y*sin(th1)
%n2 = z - l1
%n3 = x*sin(th1) - y*cos(th1)


%m1 = l7*sin(th2 - th3 + th4) + l8*sin(th2 - th3 + th4) + l3*cos(th2) + l5*cos(th2 - th3)
%m2 = l3*sin(th2) - l8*cos(th2 - th3 + th4) - l7*cos(th2 - th3 + th4) + l5*sin(th2 - th3)
%m3 = l2 - l4 + l6

--> s2 = (ny*(l3 + c3*l5 + l5*nx*s3))/((l3 + c3*l5)^2 + l5^2*s3^2)
--> c2 = (nx*(l3 + c3*l5) - l5*ny*s3)/(((l5^2*s3^2)/(l3 + c3*l5)^2 + 1)*(l3 + c3*l5)^2)

--> tan(th2) = (ny*((l5^2*s3^2)/(l3 + c3*l5)^2 + 1)*(l3 + c3*l5)^2*(l3 + c3*l5 + l5*nx*s3))/(((l3 + c3*l5)^2 + l5^2*s3^2)*(nx*(l3 + c3*l5) - l5*ny*s3))

------ Chon th2 - th3 + th4 = 0 
th4 = th3 - th2