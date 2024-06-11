import numpy as np
import pandas as pd
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D
from sklearn.preprocessing import PolynomialFeatures
from sklearn.linear_model import LinearRegression
import os

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

# Path to the CSV file
csv_file_path = 'C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv'

# Load the 3D movement data from CSV
t, x, y, z = load_3d_movement_from_csv(csv_file_path)

# Prepare the data for regression
T = t.reshape(-1, 1)

# Choose a high degree for the polynomial
high_degree = 10  # Adjust as needed to increase or decrease the fitting accuracy

# Perform polynomial regression for the high degree and store the model
model_x = polynomial_regression(T, x, high_degree)
model_y = polynomial_regression(T, y, high_degree)
model_z = polynomial_regression(T, z, high_degree)

# Predict the values at the original sample points
T_poly = PolynomialFeatures(degree=high_degree).fit_transform(T)
predicted_x = model_x.predict(T_poly)
predicted_y = model_y.predict(T_poly)
predicted_z = model_z.predict(T_poly)

# Store the results in a new CSV file
predicted_data = np.vstack((predicted_x, predicted_y, predicted_z)).T
predicted_df = pd.DataFrame(predicted_data, columns=['Predicted X', 'Predicted Y', 'Predicted Z'])

# Save the file in the same directory as the input CSV file
output_path = os.path.join(os.path.dirname(csv_file_path), 'polynomial_regression.csv')
predicted_df.to_csv(output_path, index=False)

# Visualize the Data and Curves in 3D
def plot_trajectory_3d(t, x, y, z, predicted_x, predicted_y, predicted_z):
    fig = plt.figure()
    ax = fig.add_subplot(111, projection='3d')
    
    # Scatter plot of the original data points
    ax.scatter(x, y, z, color='blue', label='Original Trajectory')
    ax.set_xlabel('X')
    ax.set_ylabel('Y')
    ax.set_zlabel('Z')
    
    # Plot the predicted trajectory
    ax.plot(predicted_x, predicted_y, predicted_z, color='red', label='Polynomial Regression')
    
    plt.legend()
    plt.show()

# Plot the original and predicted data
plot_trajectory_3d(t, x, y, z, predicted_x, predicted_y, predicted_z)
