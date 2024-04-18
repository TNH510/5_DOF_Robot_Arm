from vpython import *
from time import *
# from visual import *
import numpy as np
import math
import serial


ad=serial.Serial('COM20', 115200)
sleep(1)

canvas(title='IMU Data 3D Visualization', caption='A caption')

scene.range=5
toRad=2*np.pi/360
toDeg=1/toRad
scene.forward=vector(-1,-1,-1)

scene.width=800
scene.height=800

xarrow=arrow(lenght=2, shaftwidth=.1, color=color.red,axis=vector(1,0,0))
yarrow=arrow(lenght=2, shaftwidth=.1, color=color.green,axis=vector(0,1,0))
zarrow=arrow(lenght=4, shaftwidth=.1, color=color.blue,axis=vector(0,0,1))

frontArrow=arrow(length=4,shaftwidth=.1,color=color.purple,axis=vector(1,0,0))
upArrow=arrow(length=1,shaftwidth=.1,color=color.magenta,axis=vector(0,1,0))
sideArrow=arrow(length=2,shaftwidth=.1,color=color.orange,axis=vector(0,0,1))

bBoard=box(texture={'file':'board.jpg','place':['right'] },length=.2,width=2,height=6,opacity=.8)
bBoard.rotate(angle=np.pi/2,axis=vector(0,0,1))

while (True):
    while (ad.inWaiting()==0):
        pass
    roll,pitch,yaw= .0,.0,.0     
    try:
            dataPacket=ad.readline()
            dataPacket=str(dataPacket,'utf-8')
            splitPacket=dataPacket.split(",")
            roll=float(splitPacket[1])*toRad
            pitch=float(splitPacket[0])*toRad
            yaw=float(splitPacket[2])*toRad+np.pi
            yaw = -yaw
    except:
        pass
    log = 'Roll={},Pitch={},Yaw={}'.format(roll, pitch,yaw)
    print(log)
    scene.caption = log
     
    rate(50)

    body_x,body_y,body_z = cos(yaw)*cos(pitch),sin(pitch),sin(yaw)*cos(pitch)
    k=vector(body_x,body_y,body_z)

    y=vector(0,1,0)
    s=cross(k,y)
    v=cross(s,k)
    vrot=v*cos(roll)+cross(k,v)*sin(roll)

    frontArrow.axis=k
    sideArrow.axis=cross(k,vrot)
    upArrow.axis=v
    bBoard.axis=vrot
    bBoard.up=k
    sideArrow.length=2
    frontArrow.length=4
    upArrow.length=1
    bBoard.width = 2
    bBoard.height = 6
    bBoard.length = .2