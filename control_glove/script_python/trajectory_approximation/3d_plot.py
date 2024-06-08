import numpy as np
import matplotlib.pyplot as plt
from scipy.optimize import curve_fit

# Generate sample data
np.random.seed(0)
x = np.linspace(0, 10, 100)
y_linear = 2 * x + np.random.normal(0, 1, 100)  # Linear relationship with noise
y_circular = 3 * np.sin(x) + np.random.normal(0, 1, 100)  # Circular relationship with noise

# Linear regression model
coefficients_linear = np.polyfit(x, y_linear, 1)
linear_fit = np.poly1d(coefficients_linear)

# Circular regression model
def circular_func(x, a, b, c):
    return a * np.sin(b * x + c)

initial_guess = [np.mean(y_circular), 1, 0]  # Initial guess for parameters
params_circular, _ = curve_fit(circular_func, x, y_circular, p0=initial_guess)

# Generate modified data using the fitted models
x_modified = np.linspace(0, 10, 100)
y_modified_linear = linear_fit(x_modified)
y_modified_circular = circular_func(x_modified, *params_circular)

# Plot original and modified data
plt.figure(figsize=(12, 5))

plt.subplot(1, 2, 1)
plt.scatter(x, y_linear, label='Original Data')
plt.plot(x_modified, y_modified_linear, color='red', label='Linear Fit')
plt.xlabel('X')
plt.ylabel('Y')
plt.title('Linear Regression')
plt.legend()

plt.subplot(1, 2, 2)
plt.scatter(x, y_circular, label='Original Data')
plt.plot(x_modified, y_modified_circular, color='green', label='Circular Fit')
plt.xlabel('X')
plt.ylabel('Y')
plt.title('Circular Regression')
plt.legend()

plt.tight_layout()
plt.show()
