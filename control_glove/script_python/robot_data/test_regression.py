import numpy as np
import matplotlib.pyplot as plt
from scipy import interpolate
import pandas as pd


import numpy as np

def distribute_cakes(total_cakes, total_people):
    # Chia đều bánh cho mỗi người
    base_cakes_per_person = total_cakes // total_people
    # Số bánh còn dư sau khi chia đều
    remaining_cakes = total_cakes % total_people

    # Tạo mảng để lưu số miếng bánh mỗi người nhận
    cakes_per_person = [base_cakes_per_person] * total_people

    # Phân phối số bánh còn dư cho những người đầu tiên
    for i in range(remaining_cakes):
        cakes_per_person[i] += 1

    return cakes_per_person

# Load data from CSV (assuming CSV file has only x, y, z columns)
data = pd.read_csv('C:/Users/daveb/Desktop/5_DOF_Robot_Arm/matlab/test.csv')  # Load your CSV file
num_samples = data.shape[0] + 1  # Number of samples

# Generate a time vector assuming uniform sampling
t = np.linspace(0, 1, num_samples)  # Adjust as needed based on your data

x = data.iloc[:, 0].to_numpy()  # First column is x coordinate
y = data.iloc[:, 1].to_numpy()  # Second column is y coordinate
z = data.iloc[:, 2].to_numpy()  # Third column is z coordinate

# Number of points per group
group_size = 30

# Number of groups
num_groups = num_samples // group_size

# Number of remaining points
num_remaining_points = num_samples % group_size

# Degree of the polynomial
degree = 4  # Example: 4th degree polynomial

# Initialize figure
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')
ax.set_xlabel('X')
ax.set_ylabel('Y')
ax.set_zlabel('Z')
ax.set_title('3D Trajectory Visualization with Polynomial Regression for Each Group')
ax.grid(True)

# Plot original data points
ax.scatter(x, y, z, c='b', marker='o', label='Original Trajectory')

# List to store predicted points for each group
predicted_points = []

# Loop through each group
for i in range(1, num_groups + 1):
    # Select data for the current group
    start_index = (i - 1) * group_size
    if i == num_groups:
        end_index = i * group_size + num_remaining_points  # Include remaining points in the last group
    else:
        end_index = i * group_size
    
    t_group = t[start_index:end_index]
    x_group = x[start_index:end_index]
    y_group = y[start_index:end_index]
    z_group = z[start_index:end_index]
    
    # Perform polynomial regression for the current group
    coefficients_x = np.polyfit(t_group, x_group, degree)
    coefficients_y = np.polyfit(t_group, y_group, degree)
    coefficients_z = np.polyfit(t_group, z_group, degree)
    
    # Generate predicted values using polynomial equation
    t_fit = np.linspace(t_group[0], t_group[-1], group_size)
    predicted_x_group = np.polyval(coefficients_x, t_fit)
    predicted_y_group = np.polyval(coefficients_y, t_fit)
    predicted_z_group = np.polyval(coefficients_z, t_fit)
    
    # Store predicted points for current group
    predicted_points.append((predicted_x_group, predicted_y_group, predicted_z_group))
    
    # Plot polynomial regression for the current group
    ax.plot(predicted_x_group, predicted_y_group, predicted_z_group, label=f'Group {i+1}')
    
# Display predicted points for each group
for i, (px, py, pz) in enumerate(predicted_points):
    print(len(px))
    print(f'Group {i+1} predicted points:')
    for j in range(len(px)):
        print(f'({px[j]}, {py[j]}, {pz[j]})')
    print()

# Gọi hàm với số lượng bánh và người bất kỳ
total_cakes = num_samples  # Bạn có thể thay đổi số lượng bánh
total_people = num_groups  # Bạn có thể thay đổi số lượng người

cakes_per_person = distribute_cakes(total_cakes, total_people)

# Hiển thị kết quả
print('Số miếng bánh mỗi người nhận được là:')
print(cakes_per_person)

# Show legend
ax.legend()

# Show plot
plt.show()



