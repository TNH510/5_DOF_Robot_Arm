import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from sklearn.preprocessing import PolynomialFeatures
from sklearn.linear_model import LinearRegression

# Load 3D Movement Data from CSV
def load_3d_movement_from_csv(file_path):
    data = pd.read_csv(file_path, header=None)
    x = data[0].values
    y = data[1].values
    z = data[2].values
    t = np.linspace(0, 10, len(x))  # Create a time variable based on the length of the data
    return t, x, y, z

# Polynomial Regression
def polynomial_regression(X, Y, degree):
    polynomial_features = PolynomialFeatures(degree=degree)
    X_poly = polynomial_features.fit_transform(X)
    model = LinearRegression()
    model.fit(X_poly, Y)
    return model

# Visualize the Data and Curves in 3D
def plot_trajectory_3d(t, x, y, z, models, degrees):
    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')
    
    # Scatter plot of the original data points
    ax.scatter(x, y, z, color='blue', label='Original Trajectory')
    ax.set_xlabel('X')
    ax.set_ylabel('Y')
    ax.set_zlabel('Z')
    
    # Plot curves
    t_plot = np.linspace(min(t), max(t), 100).reshape(-1, 1)
    for model, degree in models:
        t_plot_poly = PolynomialFeatures(degree=degree).fit_transform(t_plot)
        predicted_x = model['x'].predict(t_plot_poly)
        predicted_y = model['y'].predict(t_plot_poly)
        predicted_z = model['z'].predict(t_plot_poly)
        ax.plot(predicted_x, predicted_y, predicted_z, label=f'Polynomial Curve (Degree {degree})')
    
    plt.legend()
    plt.show()

# Path to the CSV file
csv_file_path = 'C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv'

# Load the 3D movement data from CSV
t, x, y, z = load_3d_movement_from_csv(csv_file_path)

# Prepare the data for regression
T = t.reshape(-1, 1)

# Choose the degrees of the polynomials
degrees = [2, 3, 4]

# Perform polynomial regression for each degree and store models
models = []
for degree in degrees:
    model_x = polynomial_regression(T, x, degree)
    model_y = polynomial_regression(T, y, degree)
    model_z = polynomial_regression(T, z, degree)
    
    models.append(({
        'x': model_x,
        'y': model_y,
        'z': model_z
    }, degree))

# Plot data and curves in 3D
plot_trajectory_3d(t, x, y, z, models, degrees)
