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

% Last Modified by GUIDE v2.5 04-Nov-2023 15:17:40

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