import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from matplotlib.animation import FuncAnimation

# Khởi tạo các biến
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')
ax.set_xlim([-1, 1])
ax.set_ylim([-1, 1])
ax.set_zlim([-1, 1])

# Tạo ma trận chứa các điểm của khối lập phương không đổi
cubePoints = np.array([[0.5, -0.5, -0.5, 0.5, 0.5],
                       [0.5, 0.5, -0.5, -0.5, 0.5],
                       [0.5, 0.5, 0.5, 0.5, 0.5]])

# Hàm xử lý sự kiện nhấn phím
def on_key_press(event):
    global quaternion
    if event.key == 'up':
        # Quay quaternion theo trục x
        angle = np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [1, 0, 0])
    elif event.key == 'down':
        # Quay quaternion theo trục x ngược chiều kim đồng hồ
        angle = -np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [1, 0, 0])
    elif event.key == 'left':
        # Quay quaternion theo trục y
        angle = np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [0, 1, 0])
    elif event.key == 'right':
        # Quay quaternion theo trục y ngược chiều kim đồng hồ
        angle = -np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [0, 1, 0])
    elif event.key == 'pageup':
        # Quay quaternion theo trục z
        angle = np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [0, 0, 1])
    elif event.key == 'pagedown':
        # Quay quaternion theo trục z ngược chiều kim đồng hồ
        angle = -np.pi / 12
        quaternion = quaternion_rotation(quaternion, angle, [0, 0, 1])

# Hàm tính toán ma trận quay từ quaternion
def quaternion_to_rotation_matrix(q):
    q0, q1, q2, q3 = q
    rotationMatrix = np.array([[1-2*q2**2-2*q3**2, 2*q1*q2-2*q0*q3, 2*q1*q3+2*q0*q2],
                               [2*q1*q2+2*q0*q3, 1-2*q1**2-2*q2**2, 2*q2*q3-2*q0*q1],
                               [2*q1*q3-2*q0*q2, 2*q2*q3+2*q0*q1, 1-2*q1**2-2*q3**2]])
    return rotationMatrix

# Hàm tính toán quaternion quay theo trục và góc
def quaternion_rotation(q, angle, axis):
    q = np.array(q)
    axis = np.array(axis) / np.linalg.norm(axis)
    q_axis = np.concatenate(([0], axis))
    q_new = np.cos(angle / 2) * q - np.sin(angle / 2) * q_axis
    return q_new / np.linalg.norm(q_new)

# Khởi tạo quaternion ban đầu
quaternion = np.array([0.707, 0.0, 0.707, 0.0])

# Gắn kết sự kiện nhấn phím
fig.canvas.mpl_connect('key_press_event', on_key_press)

# Hàm cập nhật frame
def update_frame(frame):
    ax.clear()
    ax.set_xlim([-1, 1])
    ax.set_ylim([-1, 1])
    ax.set_zlim([-1, 1])

    # Tính ma trận quay từ quaternion
    rotationMatrix = quaternion_to_rotation_matrix(quaternion)

    # Tính toán các điểm mới của khối lập phương theo ma trận quay
    rotatedPoints = np.dot(rotationMatrix, cubePoints)

    # Vẽ khối lập phương
    ax.plot(rotatedPoints[0], rotatedPoints[1], rotatedPoints[2], 'b')
    ax.set_xlabel('X')
    ax.set_ylabel('Y')
    ax.set_zlabel('Z')

# Tạo animation
ani = FuncAnimation(fig, update_frame, frames=np.arange(0, 100), interval=50, repeat=True)

# Hiển thị đồ thị
plt.show()