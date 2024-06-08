import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# Load the CSV file
file_path = 'C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv'
df = pd.read_csv(file_path, header=None)

# Set the column headers to X, Y, Z
df.columns = ['X', 'Y', 'Z']

# Apply transformations
df['X'] = df['X'] * 20
df['Y'] = df['Y'] * 20
df['Z'] = df['Z'] * 15 + 700

# Plot the transformed data in 3D
fig = plt.figure(figsize=(10, 6))
ax = fig.add_subplot(111, projection='3d')

ax.plot(df['X'], df['Y'], df['Z'], label='Trajectory')

ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Z')
ax.set_title('3D Trajectory of Transformed Values')
ax.legend()
ax.grid(True)

plt.show()
