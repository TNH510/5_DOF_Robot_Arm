import numpy as np
import matplotlib.pyplot as plt
from mpl_toolkits.mplot3d import Axes3D

# Function to transpose a matrix
def transpose(matrix):
    return [[matrix[j][i] for j in range(len(matrix))] for i in range(len(matrix[0]))]

# Function to multiply two matrices
def matrix_multiply(A, B):
    result = [[sum(A[i][k] * B[k][j] for k in range(len(B))) for j in range(len(B[0]))] for i in range(len(A))]
    return result

# Function to invert a 2x2 matrix
def invert_2x2_matrix(matrix):
    a, b, c, d = matrix[0][0], matrix[0][1], matrix[1][0], matrix[1][1]
    determinant = a * d - b * c
    if determinant == 0:
        raise ValueError("Matrix is singular and cannot be inverted.")
    inv_det = 1 / determinant
    inverted_matrix = [[d * inv_det, -b * inv_det], [-c * inv_det, a * inv_det]]
    return inverted_matrix

# Custom Linear Regression function without using np.linalg.inv or np.dot
def custom_linear_regression(X, Y):
    # Add a column of ones to include the intercept in the model
    X_b = [[1, x[0]] for x in X]
    
    # Compute the transpose of X_b
    X_b_T = transpose(X_b)
    
    # Compute the coefficients using the Normal Equation (theta_best = (X_b_T * X_b)^-1 * X_b_T * Y)
    X_b_T_X_b = matrix_multiply(X_b_T, X_b)
    
    # Invert the 2x2 matrix (X_b_T * X_b)
    X_b_T_X_b_inv = invert_2x2_matrix(X_b_T_X_b)
    
    # Compute X_b_T * Y
    X_b_T_Y = matrix_multiply(X_b_T, [[y] for y in Y])
    
    # Compute the final coefficients
    theta_best = matrix_multiply(X_b_T_X_b_inv, X_b_T_Y)
    
    return [theta[0] for theta in theta_best]

# Step 1: Generate raw data
np.random.seed(42)
t = np.linspace(0, 10, 100)
X = t.reshape(-1, 1)
Y = 3 * t + 5  # Y = 3*t + 5
Z = 2 * t + 10  # Z = 2*t + 10

# Step 2: Modify data (add noise)
Y_modified = Y + np.random.randn(100) * 2
Z_modified = Z + np.random.randn(100) * 2

# Combine the modified data for regression
modified_data = np.vstack((Y_modified, Z_modified)).T

# Step 3: Perform custom linear regression on modified data
# Fit Y
theta_Y = custom_linear_regression(X, Y_modified)
Y_pred = [theta_Y[0] + theta_Y[1] * x for x in t]

# Fit Z
theta_Z = custom_linear_regression(X, Z_modified)
Z_pred = [theta_Z[0] + theta_Z[1] * x for x in t]

# Step 4: Plot raw data line and regression line
fig = plt.figure()
ax = fig.add_subplot(111, projection='3d')

# Plot raw data line
ax.plot(t, Y, Z, color='blue', label='Raw Data Line')

# Plot modified data points
ax.scatter(t, Y_modified, Z_modified, color='red', label='Modified Data Points')

# Plot regression line
ax.plot(t, Y_pred, Z_pred, color='green', linestyle='--', label='Regression Line')

# Labels and legend
ax.set_xlabel('t')
ax.set_ylabel('Y')
ax.set_zlabel('Z')
ax.legend()
ax.set_title('Raw Data Line and Custom Linear Regression on Modified Data in 3D')

plt.show()
