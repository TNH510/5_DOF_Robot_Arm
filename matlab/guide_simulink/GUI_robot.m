function varargout = GUI_robot(varargin)
% GUI_ROBOT MATLAB code for GUI_robot.fig
%      GUI_ROBOT, by itself, creates a new GUI_ROBOT or raises the existing
%      singleton*.
%
%      H = GUI_ROBOT returns the handle to a new GUI_ROBOT or the handle to
%      the existing singleton*.
%
%      GUI_ROBOT('CALLBACK',hObject,eventData,handles,...) calls the local
%      function named CALLBACK in GUI_ROBOT.M with the given input arguments.
%
%      GUI_ROBOT('Property','Value',...) creates a new GUI_ROBOT or raises the
%      existing singleton*.  Starting from the left, property value pairs are
%      applied to the GUI before GUI_robot_OpeningFcn gets called.  An
%      unrecognized property name or invalid value makes property application
%      stop.  All inputs are passed to GUI_robot_OpeningFcn via varargin.
%
%      *See GUI Options on GUIDE's Tools menu.  Choose "GUI allows only one
%      instance to run (singleton)".
%
% See also: GUIDE, GUIDATA, GUIHANDLES

% Edit the above text to modify the response to help GUI_robot

% Last Modified by GUIDE v2.5 12-Nov-2023 21:38:43

% Begin initialization code - DO NOT EDIT
gui_Singleton = 1;
gui_State = struct('gui_Name',       mfilename, ...
                   'gui_Singleton',  gui_Singleton, ...
                   'gui_OpeningFcn', @GUI_robot_OpeningFcn, ...
                   'gui_OutputFcn',  @GUI_robot_OutputFcn, ...
                   'gui_LayoutFcn',  [] , ...
                   'gui_Callback',   []);
if nargin && ischar(varargin{1})
    gui_State.gui_Callback = str2func(varargin{1});
end

if nargout
    [varargout{1:nargout}] = gui_mainfcn(gui_State, varargin{:});
else
    gui_mainfcn(gui_State, varargin{:});
end
% End initialization code - DO NOT EDIT


% --- Executes just before GUI_robot is made visible.
function GUI_robot_OpeningFcn(hObject, eventdata, handles, varargin)
% This function has no output args, see OutputFcn.
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
% varargin   command line arguments to GUI_robot (see VARARGIN)

% Choose default command line output for GUI_robot
handles.output = hObject;

% Update handles structure
guidata(hObject, handles);

% UIWAIT makes GUI_robot wait for user response (see UIRESUME)
% uiwait(handles.figure1);


% --- Outputs from this function are returned to the command line.
function varargout = GUI_robot_OutputFcn(hObject, eventdata, handles) 
% varargout  cell array for returning output args (see VARARGOUT);
% hObject    handle to figure
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Get default command line output from handles structure
varargout{1} = handles.output;


% --- Executes on slider movement.
function slider4_Callback(hObject, eventdata, handles)

l1=690;
l2=440;
l3=500;
l5=230;
theta1=get(handles.slider2,'Value');
set(handles.edit4,'String',num2str(theta1));
set(handles.edit4,'Value',theta1);

theta2=get(handles.slider1,'Value');
set(handles.edit5,'String',num2str(theta2));
set(handles.edit5,'Value',theta2);

theta3=get(handles.slider3,'Value');
set(handles.edit6,'String',num2str(theta3));
set(handles.edit6,'Value',theta3);

theta4=get(handles.slider4,'Value');
set(handles.edit7,'String',num2str(theta4));
set(handles.edit7,'Value',theta4);

theta5=get(handles.slider5,'Value');
set(handles.edit8,'String',num2str(theta5));
set(handles.edit8,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to slider4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider


% --- Executes during object creation, after setting all properties.
function slider4_CreateFcn(hObject, eventdata, handles)
% hObject    handle to slider4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on slider movement.
function slider1_Callback(hObject, eventdata, handles)
l1=690;
l2=440;
l3=500;
l5=230;
theta1=get(handles.slider2,'Value');
set(handles.edit4,'String',num2str(theta1));
set(handles.edit4,'Value',theta1);

theta2=get(handles.slider1,'Value');
set(handles.edit5,'String',num2str(theta2));
set(handles.edit5,'Value',theta2);

theta3=get(handles.slider3,'Value');
set(handles.edit6,'String',num2str(theta3));
set(handles.edit6,'Value',theta3);

theta4=get(handles.slider4,'Value');
set(handles.edit7,'String',num2str(theta4));
set(handles.edit7,'Value',theta4);

theta5=get(handles.slider5,'Value');
set(handles.edit8,'String',num2str(theta5));
set(handles.edit8,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to slider1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider


% --- Executes during object creation, after setting all properties.
function slider1_CreateFcn(hObject, eventdata, handles)

% hObject    handle to slider1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on slider movement.
function slider2_Callback(hObject, eventdata, handles)
% hObject    handle to slider2 (see GCBO)
l1=690;
l2=440;
l3=500;
l5=230;
theta1=get(handles.slider2,'Value');
set(handles.edit4,'String',num2str(theta1));
set(handles.edit4,'Value',theta1);

theta2=get(handles.slider1,'Value');
set(handles.edit5,'String',num2str(theta2));
set(handles.edit5,'Value',theta2);

theta3=get(handles.slider3,'Value');
set(handles.edit6,'String',num2str(theta3));
set(handles.edit6,'Value',theta3);

theta4=get(handles.slider4,'Value');
set(handles.edit7,'String',num2str(theta4));
set(handles.edit7,'Value',theta4);

theta5=get(handles.slider5,'Value');
set(handles.edit8,'String',num2str(theta5));
set(handles.edit8,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));

% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider


% --- Executes during object creation, after setting all properties.
function slider2_CreateFcn(hObject, eventdata, handles)
% hObject    handle to slider2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on slider movement.
function slider3_Callback(hObject, eventdata, handles)

l1=690;
l2=440;
l3=500;
l5=230;
theta1=get(handles.slider2,'Value');
set(handles.edit4,'String',num2str(theta1));
set(handles.edit4,'Value',theta1);

theta2=get(handles.slider1,'Value');
set(handles.edit5,'String',num2str(theta2));
set(handles.edit5,'Value',theta2);

theta3=get(handles.slider3,'Value');
set(handles.edit6,'String',num2str(theta3));
set(handles.edit6,'Value',theta3);

theta4=get(handles.slider4,'Value');
set(handles.edit7,'String',num2str(theta4));
set(handles.edit7,'Value',theta4);

theta5=get(handles.slider5,'Value');
set(handles.edit8,'String',num2str(theta5));
set(handles.edit8,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));


t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to slider3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider


% --- Executes during object creation, after setting all properties.
function slider3_CreateFcn(hObject, eventdata, handles)
% hObject    handle to slider3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on slider movement.
function slider5_Callback(hObject, eventdata, handles)
l1=690;
l2=440;
l3=500;
l5=230;
theta1=get(handles.slider2,'Value');
set(handles.edit4,'String',num2str(theta1));
set(handles.edit4,'Value',theta1);

theta2=get(handles.slider1,'Value');
set(handles.edit5,'String',num2str(theta2));
set(handles.edit5,'Value',theta2);

theta3=get(handles.slider3,'Value');
set(handles.edit6,'String',num2str(theta3));
set(handles.edit6,'Value',theta3);

theta4=get(handles.slider4,'Value');
set(handles.edit7,'String',num2str(theta4));
set(handles.edit7,'Value',theta4);

theta5=get(handles.slider5,'Value');
set(handles.edit8,'String',num2str(theta5));
set(handles.edit8,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));


t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to slider5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'Value') returns position of slider
%        get(hObject,'Min') and get(hObject,'Max') to determine range of slider


% --- Executes during object creation, after setting all properties.
function slider5_CreateFcn(hObject, eventdata, handles)
% hObject    handle to slider5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: slider controls usually have a light gray background.
if isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor',[.9 .9 .9]);
end


% --- Executes on button press in pushbutton1.
function pushbutton1_Callback(hObject, eventdata, handles)
open_system('Complete');
set_param('Complete','BlockReduction','off');
set_param('Complete','StopTime','inf');
set_param('Complete','SimulationMode','normal');
set_param('Complete','StartFcn','1');
set_param('Complete','SimulationCommand','start');
% hObject    handle to pushbutton1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


% --- Executes on button press in pushbutton2.
function pushbutton2_Callback(hObject, eventdata, handles)
close;
% hObject    handle to pushbutton2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


% --- Executes on button press in pushbutton3.
function pushbutton3_Callback(hObject, eventdata, handles)
syms theta1 theta2 theta3 theta4 theta5 c3 s3
l1=690;
l2=440;
l3=500;
l5=230;
theta234=-90;

px=str2double(get(handles.edit1,'String'));
py=str2double(get(handles.edit2,'String'));
pz=str2double(get(handles.edit3,'String'));
theta1=atan2d(py,px);

m= sqrt(px^2+py^2)-l5*cosd(theta234);
%m1=l3*cosd(theta2+theta3)+l2*cosd(theta2);
n=pz-l1-l5*sind(theta234);
%n1=l3*sind(theta2+theta3)+l2*sind(theta2);
c3=double((m^2+n^2-l3^2-l2^2)/(2*l3*l2));
s3=double(sqrt(1-c3^2));
theta3=atan2d(s3,c3);
if (theta3<80 && theta3 >-30)
    theta3=atan2d(s3,c3);
else
    s3=double(-sqrt(1-c3^2));
    theta3=atan2d(s3,c3);
end
A= m*(l3*cosd(theta3)+l2)+n*(l3*sind(theta3));
B= n*(l3*cosd(theta3)+l2)-m*(l3*sind(theta3));
theta2=atan2d(B,A);
theta4=-90-theta2-theta3;

guidata(hObject,handles);
set(handles.edit4,'String',num2str(theta1));
set(handles.edit5,'String',num2str(theta2-90));
set(handles.edit6,'String',num2str(theta3+90));
set(handles.edit7,'String',num2str(theta4+90));

set(handles.slider2,'Value',round(theta1,2));
set(handles.slider1,'Value',round(theta2-90,2));
set(handles.slider3,'Value',round(theta3+90,2));
set(handles.slider4,'Value',round(theta4+90,2));

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2));
set_param('Complete/Slider Gain2','Gain',num2str(theta3));
set_param('Complete/Slider Gain3','Gain',num2str(theta4));


% hObject    handle to pushbutton3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)


% --- Executes on button press in pushbutton4.
function pushbutton4_Callback(hObject, eventdata, handles)
set(handles.edit4,'String','0');
set(handles.slider2,'Value',0);

set(handles.edit5,'String','0');
set(handles.slider1,'Value',0);

set(handles.edit6,'String','0');
set(handles.slider3,'Value',0);

set(handles.edit7,'String','0');
set(handles.slider4,'Value',0);

set(handles.edit8,'String','0');
set(handles.slider5,'Value',0);

set(handles.edit1,'String','500');
set(handles.edit2,'String','0');
set(handles.edit3,'String','900');

theta1=get(handles.edit4,'String');
theta2=get(handles.edit5,'String');
theta3=get(handles.edit6,'String');
theta4=get(handles.edit7,'String');
theta5=get(handles.edit8,'String');
set_param('Complete/Slider Gain','Gain',num2str(0));
set_param('Complete/Slider Gain1','Gain',num2str(90));
set_param('Complete/Slider Gain2','Gain',num2str(-90));
set_param('Complete/Slider Gain3','Gain',num2str(-90));
set_param('Complete/Slider Gain4','Gain',num2str(0));
% hObject    handle to pushbutton4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)



function edit1_Callback(hObject, eventdata, handles)
% hObject    handle to edit1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit1 as text
%        str2double(get(hObject,'String')) returns contents of edit1 as a double


% --- Executes during object creation, after setting all properties.
function edit1_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit1 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit2_Callback(hObject, eventdata, handles)
% hObject    handle to edit2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit2 as text
%        str2double(get(hObject,'String')) returns contents of edit2 as a double


% --- Executes during object creation, after setting all properties.
function edit2_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit2 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit3_Callback(hObject, eventdata, handles)
% hObject    handle to edit3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit3 as text
%        str2double(get(hObject,'String')) returns contents of edit3 as a double


% --- Executes during object creation, after setting all properties.
function edit3_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit3 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit4_Callback(hObject, eventdata, handles)
l1=690;
l2=440;
l3=500;
l5=230;
theta1=str2double(get(handles.edit4,'String'));
set(handles.slider2,'Value',theta1);

theta2=str2double(get(handles.edit5,'String'));
set(handles.slider1,'Value',theta2);

theta3=str2double(get(handles.edit6,'String'));
set(handles.slider3,'Value',theta3);

theta4=str2double(get(handles.edit7,'String'));
set(handles.slider4,'Value',theta4);

theta5=str2double(get(handles.edit8,'String'));
set(handles.slider5,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));


t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to edit4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit4 as text
%        str2double(get(hObject,'String')) returns contents of edit4 as a double


% --- Executes during object creation, after setting all properties.
function edit4_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit4 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit5_Callback(hObject, eventdata, handles)
l1=690;
l2=440;
l3=500;
l5=230;

theta1=str2double(get(handles.edit4,'String'));
set(handles.slider2,'Value',theta1);

theta2=str2double(get(handles.edit5,'String'));
set(handles.slider1,'Value',theta2);

theta3=str2double(get(handles.edit6,'String'));
set(handles.slider3,'Value',theta3);

theta4=str2double(get(handles.edit7,'String'));
set(handles.slider4,'Value',theta4);

theta5=str2double(get(handles.edit8,'String'));
set(handles.slider5,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to edit5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit5 as text
%        str2double(get(hObject,'String')) returns contents of edit5 as a double


% --- Executes during object creation, after setting all properties.
function edit5_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit5 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit6_Callback(hObject, eventdata, handles)

l1=690;
l2=440;
l3=500;
l5=230;
theta1=str2double(get(handles.edit4,'String'));
set(handles.slider2,'Value',theta1);

theta2=str2double(get(handles.edit5,'String'));
set(handles.slider1,'Value',theta2);

theta3=str2double(get(handles.edit6,'String'));
set(handles.slider3,'Value',theta3);

theta4=str2double(get(handles.edit7,'String'));
set(handles.slider4,'Value',theta4);

theta5=str2double(get(handles.edit8,'String'));
set(handles.slider5,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to edit6 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit6 as text
%        str2double(get(hObject,'String')) returns contents of edit6 as a double


% --- Executes during object creation, after setting all properties.
function edit6_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit6 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit7_Callback(hObject, eventdata, handles)

l1=690;
l2=440;
l3=500;
l5=230;
theta1=str2double(get(handles.edit4,'String'));
set(handles.slider2,'Value',theta1);

theta2=str2double(get(handles.edit5,'String'));
set(handles.slider1,'Value',theta2);

theta3=str2double(get(handles.edit6,'String'));
set(handles.slider3,'Value',theta3);

theta4=str2double(get(handles.edit7,'String'));
set(handles.slider4,'Value',theta4);

theta5=str2double(get(handles.edit8,'String'));
set(handles.slider5,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));



t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to edit7 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit7 as text
%        str2double(get(hObject,'String')) returns contents of edit7 as a double


% --- Executes during object creation, after setting all properties.
function edit7_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit7 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function edit8_Callback(hObject, eventdata, handles)
l1=690;
l2=440;
l3=500;
l5=230;
theta1=str2double(get(handles.edit4,'String'));
set(handles.slider2,'Value',theta1);

theta2=str2double(get(handles.edit5,'String'));
set(handles.slider1,'Value',theta2);

theta3=str2double(get(handles.edit6,'String'));
set(handles.slider3,'Value',theta3);

theta4=str2double(get(handles.edit7,'String'));
set(handles.slider4,'Value',theta4);

theta5=str2double(get(handles.edit8,'String'));
set(handles.slider5,'Value',theta5);

set_param('Complete/Slider Gain','Gain',num2str(theta1));
set_param('Complete/Slider Gain1','Gain',num2str(theta2+90));
set_param('Complete/Slider Gain2','Gain',num2str(theta3-90));
set_param('Complete/Slider Gain3','Gain',num2str(theta4-90));
set_param('Complete/Slider Gain4','Gain',num2str(theta5));


t2=theta2+90;
t3=theta3-90;
t4=theta4-90;
px=cosd(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
py=sind(theta1)*(l3*cosd(t2+t3)+l2*cosd(t2)+l5*cosd(-90));
pz=l1+l3*sind(t2+t3)+l2*sind(t2)+l5*sind(-90);
set(handles.edit1,'String',num2str(px));

set(handles.edit2,'String',num2str(py));

set(handles.edit3,'String',num2str(pz));
% hObject    handle to edit8 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of edit8 as text
%        str2double(get(hObject,'String')) returns contents of edit8 as a double


% --- Executes during object creation, after setting all properties.
function edit8_CreateFcn(hObject, eventdata, handles)
% hObject    handle to edit8 (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


% --- Executes on button press in com_draw.
function com_draw_Callback(hObject, eventdata, handles)

s = serial('COM37'); 
set(s, 'BaudRate', 9600); 
fopen(s); 

figure;
plotData = []; 
while true
    if s.BytesAvailable > 0 
        data = fread(s, s.BytesAvailable);
        data_ok = typecast(data, 'double');
        set_param('Complete/Slider Gain','Gain',num2str(data_ok));
        break;
    end
end

% ?�ng k?t n?i COM
fclose(s); % ?�ng k?t n?i COM
delete(s); % X�a ??i t??ng COM

function ret = Check_angle(t1, t2, t3, t4, t5)
    if ((t1 > 115.0) || (t1 < -95.0) || isnan(t1))
        ret = 1;
    elseif ((t2 > 110.0) || (t2 < 30.0) || isnan(t2))
        ret = 2;
    elseif ((t3 > -10.0) || (t3 < -120.0) || isnan(t3))
        ret = 3;
    elseif ((t4 > 5.0) || (t4 < -105.0) || isnan(t4))
        ret = 4;
    elseif ((t5 > 179.999) || (t5 < -180.0) || isnan(t5))
        ret = 5;
    else
        ret = 0;
    end


function angles = convert_position_angle(x, y, z)
    l1 = 690.0;
    l2 = 440.0;
    l3 = 500.0;
    l4 = 0.0;
    l5 = 230.0;
    t1 = atan2(y, x);
    roll = 0.0;
    pitch = -pi / 2;
    t5 = roll - t1;
    m = sqrt(x^2 + y^2);
    n = z - l1 + l5;
    c3 = double((m^2 + n^2 - l2^2 - l3^2) / (2 * l2 * l3));
    s3 = double(sqrt(1 - c3^2));
    t3 = atan2(s3, c3);
    if t3 >= -pi / 6 && t3 <= (4 * pi) / 9
        % Do nothing
    else
        s3 = -sqrt(1 - c3^2);
        t3 = atan2(s3, c3);
    end
    c2 = m * (l3 * c3 + l2) + n * (l3 * s3);
    s2 = n * (l3 * c3 + l2) - m * (l3 * s3);
    t2 = atan2(s2, c2);
   
    % Angle 4
    t4 = pitch - t2 - t3;
    
    t1 = t1 / pi * 180.0;
    t2 = t2 / pi * 180.0;
    t3 = t3 / pi * 180.0;
    t4 = t4 / pi * 180.0;
    t5 = t5 / pi * 180.0;
    
    angles = [t1, t2, t3, t4, t5];  

function MoveL(hObject, event, handles)
    global t;
    global tar_pos; 
    global cur_pos;
    global vect_u;
    global tm;
    if t <= 10
        x = cur_pos(1,1) + (vect_u(1,1) / 10) * t;
        y = cur_pos(1,2) + (vect_u(1,2) / 10) * t;
        z = cur_pos(1,3) + (vect_u(1,3) / 10) * t;
        value_angles = convert_position_angle(x, y, z)

        t1 = value_angles(1,1);
        t2 = value_angles(1,2);
        t3 = value_angles(1,3);
        t4 = value_angles(1,4);
        t5 = value_angles(1,5);

        ret = Check_angle(t1, t2, t3, t4, t5);
        if ret ~= 0
            theta = 0.0;
            if ret == 1
                theta = t1;
            elseif ret == 2
                theta = t2;
            elseif ret == 3
                theta = t3;
            elseif ret == 4
                theta = t4;
            elseif ret == 5
                theta = t5;
            end
            disp(['Error: P2P: theta', num2str(ret), ' = ', num2str(theta), ' out of range']);
            return;
        end
        
        set(handles.edit1,'String',num2str(x));
        set(handles.edit2,'String',num2str(y));
        set(handles.edit3,'String',num2str(z));
        % guidata(hObject,handles);
        set(handles.edit4,'String',num2str(t1));
        set(handles.edit5,'String',num2str(t2 - 90.0));
        set(handles.edit6,'String',num2str(t3 + 90.0));
        set(handles.edit7,'String',num2str(t4 + 90.0));
        set(handles.edit8,'String',num2str(t5));

        set(handles.slider2,'Value',round(t1,2));
        set(handles.slider1,'Value',round(t2 - 90.0,2));
        set(handles.slider3,'Value',round(t3 + 90.0,2));
        set(handles.slider4,'Value',round(t4 + 90.0,2));
        set(handles.slider5,'Value',round(t5,2));

        set_param('Complete/Slider Gain','Gain',num2str(t1));
        set_param('Complete/Slider Gain1','Gain',num2str(t2));
        set_param('Complete/Slider Gain2','Gain',num2str(t3));
        set_param('Complete/Slider Gain3','Gain',num2str(t4));
        set_param('Complete/Slider Gain4','Gain',num2str(t5));
        t = t + 1;
    else
        % Stop the timer
        stop(tm);
        % Delete the timer object
        delete(tm);
    end

% --- Executes on button press in MoveL_btn.
function MoveL_btn_Callback(hObject, eventdata, handles)
% hObject    handle to MoveL_btn (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
    global t;
    t = 1;
    global tar_pos; 
    global cur_pos;
    global vect_u;
    global tm;
    tar_pos = zeros(1, 3);  % Initializing vect_u array with zeros
    cur_pos = zeros(1, 3);  % Initializing vect_u array with zeros
    % Referred vector
    vect_u = zeros(1, 3);  % Initializing vect_u array with zeros

    tar_pos(1,1) = str2double(get(handles.MvLx_tb,'String'));
    tar_pos(1,2) = str2double(get(handles.MvLy_tb,'String'));
    tar_pos(1,3) = str2double(get(handles.MvLz_tb,'String'));

    cur_pos(1,1) = str2double(get(handles.edit1,'String'));
    cur_pos(1,2) = str2double(get(handles.edit2,'String'));
    cur_pos(1,3) = str2double(get(handles.edit3,'String'));

    % value_angles = convert_position_angle(x, y, z)

    for i = 1:3
        vect_u(1,i) = tar_pos(1,i) - cur_pos(1,i);
        % Uncomment the following line if you want to print the values
        % disp(['vect_u(', num2str(i), ') = ', num2str(vect_u(i))]);
    end

    vect_u
    tm = timer('ExecutionMode', 'FixedRate', 'Period', 0.1, 'TimerFcn', {@MoveL, handles});
    start(tm);



function MvLx_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvLx_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvLx_tb as text
%        str2double(get(hObject,'String')) returns contents of MvLx_tb as a double


% --- Executes during object creation, after setting all properties.
function MvLx_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvLx_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvLy_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvLy_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvLy_tb as text
%        str2double(get(hObject,'String')) returns contents of MvLy_tb as a double


% --- Executes during object creation, after setting all properties.
function MvLy_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvLy_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvLz_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvLz_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvLz_tb as text
%        str2double(get(hObject,'String')) returns contents of MvLz_tb as a double


% --- Executes during object creation, after setting all properties.
function MvLz_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvLz_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end


function [center, radius] = calculateCircleProperties(x1, y1, z1, x2, y2, z2, x3, y3, z3)
    % T�nh trung ?i?m c?a c�c ?i?m tr�n ???ng tr�n
    midPointX = (x1 + x2 + x3) / 3;
    midPointY = (y1 + y2 + y3) / 3;
    midPointZ = (z1 + z2 + z3) / 3;

    % Radius
    radius = sqrt((x1 - midPointX)^2 + (y1 - midPointY)^2 + (z1 - midPointZ)^2);

    % Center
    center = [midPointX, midPointY, midPointZ];

function MoveC(hObject, event, handles)
    global radiusC;
    global tC_t;
    global tC_s;
    global centerC;
    global tmC;
    if tC_t <= 10
        step = tC_t * 2 * pi / 10;
        if tC_s <= 10
            step_s = tC_s * 2 * pi/ 10;
            % Parametric equations of a 3D circle
            x = radiusC + centerC(1,1) * cos(step) * cos(step_s);
            y = radiusC + centerC(1,2) * cos(step) * sin(step_s);
            z = radiusC + centerC(1,3) * sin(step);
            value_angles = convert_position_angle(x, y, z)

            t1 = value_angles(1,1);
            t2 = value_angles(1,2);
            t3 = value_angles(1,3);
            t4 = value_angles(1,4);
            t5 = value_angles(1,5);

            ret = Check_angle(t1, t2, t3, t4, t5);
            if ret ~= 0
                theta = 0.0;
                if ret == 1
                    theta = t1;
                elseif ret == 2
                    theta = t2;
                elseif ret == 3
                    theta = t3;
                elseif ret == 4
                    theta = t4;
                elseif ret == 5
                    theta = t5;
                end
                disp(['Error: P2P: theta', num2str(ret), ' = ', num2str(theta), ' out of range']);
                return;
            end

            set(handles.edit1,'String',num2str(x));
            set(handles.edit2,'String',num2str(y));
            set(handles.edit3,'String',num2str(z));
            % guidata(hObject,handles);
            set(handles.edit4,'String',num2str(t1));
            set(handles.edit5,'String',num2str(t2 - 90.0));
            set(handles.edit6,'String',num2str(t3 + 90.0));
            set(handles.edit7,'String',num2str(t4 + 90.0));
            set(handles.edit8,'String',num2str(t5));

            set(handles.slider2,'Value',round(t1,2));
            set(handles.slider1,'Value',round(t2 - 90.0,2));
            set(handles.slider3,'Value',round(t3 + 90.0,2));
            set(handles.slider4,'Value',round(t4 + 90.0,2));
            set(handles.slider5,'Value',round(t5,2));

            set_param('Complete/Slider Gain','Gain',num2str(t1));
            set_param('Complete/Slider Gain1','Gain',num2str(t2));
            set_param('Complete/Slider Gain2','Gain',num2str(t3));
            set_param('Complete/Slider Gain3','Gain',num2str(t4));
            set_param('Complete/Slider Gain4','Gain',num2str(t5));
            tC_s = tC_s + 1;
        end
        tC_t = tC_t + 1;        
    else
        % Stop the timer
        stop(tmC);
        % Delete the timer object
        delete(tmC);
    end
    
% --- Executes on button press in MoveC_btn.
function MoveC_btn_Callback(hObject, eventdata, handles)
% hObject    handle to MoveC_btn (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)
    global tC_t;
    global tC_s;
    tC_t = 1;
    tC_s = 1;
    global tar_posC1; 
    global tar_posC2;
    global cur_posC;
    global tmC;
    global centerC;
    global radiusC;
    tar_posC1 = zeros(1, 3);  % Initializing vect_u array with zeros
    tar_posC2 = zeros(1, 3);  % Initializing vect_u array with zeros
    cur_posC = zeros(1, 3);  % Initializing vect_u array with zeros

    tar_posC1(1,1) = str2double(get(handles.MvCx1_tb,'String'));
    tar_posC1(1,2) = str2double(get(handles.MvCy1_tb,'String'));
    tar_posC1(1,3) = str2double(get(handles.MvCz1_tb,'String'));
    x1 = tar_posC1(1,1);
    y1 = tar_posC1(1,2);
    z1 = tar_posC1(1,3);
    
    tar_posC2(1,1) = str2double(get(handles.MvCx2_tb,'String'));
    tar_posC2(1,2) = str2double(get(handles.MvCy2_tb,'String'));
    tar_posC2(1,3) = str2double(get(handles.MvCz2_tb,'String'));
    x2 = tar_posC2(1,1);
    y2 = tar_posC2(1,2);
    z2 = tar_posC2(1,3);
    
    cur_posC(1,1) = str2double(get(handles.edit1,'String'));
    cur_posC(1,2) = str2double(get(handles.edit2,'String'));
    cur_posC(1,3) = str2double(get(handles.edit3,'String'));
    x3 = cur_posC(1,1);
    y3 = cur_posC(1,2);
    z3 = cur_posC(1,3);    
    
    
    [centerC, radiusC] = calculateCircleProperties(x1,y1,z1,x2,y2,z2,x3,y3,z3);
    
    
    % Start timer
    tmC = timer('ExecutionMode', 'FixedRate', 'Period', 0.1, 'TimerFcn', {@MoveC, handles});
    start(tmC);
    


function MvCx1_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCx1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCx1_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCx1_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCx1_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCx1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvCy1_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCy1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCy1_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCy1_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCy1_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCy1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvCz1_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCz1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCz1_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCz1_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCz1_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCz1_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvCx2_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCx2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCx2_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCx2_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCx2_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCx2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvCy2_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCy2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCy2_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCy2_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCy2_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCy2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end



function MvCz2_tb_Callback(hObject, eventdata, handles)
% hObject    handle to MvCz2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    structure with handles and user data (see GUIDATA)

% Hints: get(hObject,'String') returns contents of MvCz2_tb as text
%        str2double(get(hObject,'String')) returns contents of MvCz2_tb as a double


% --- Executes during object creation, after setting all properties.
function MvCz2_tb_CreateFcn(hObject, eventdata, handles)
% hObject    handle to MvCz2_tb (see GCBO)
% eventdata  reserved - to be defined in a future version of MATLAB
% handles    empty - handles not created until after all CreateFcns called

% Hint: edit controls usually have a white background on Windows.
%       See ISPC and COMPUTER.
if ispc && isequal(get(hObject,'BackgroundColor'), get(0,'defaultUicontrolBackgroundColor'))
    set(hObject,'BackgroundColor','white');
end
