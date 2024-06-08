import pandas as pd
import matplotlib.pyplot as plt

# Load the CSV file
file_path = 'C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv'
df = pd.read_csv(file_path, header=None)
# Set the column headers to X, Y, Z
df.columns = ['X', 'Y', 'Z']

# Apply transformations
df['X'] = df['X'] * 20
df['Y'] = df['Y'] * 20
df['Z'] = df['Z'] * 15 + 700

# Moving Average Function
def moving_average(data, window_size):
    return data.rolling(window=window_size).mean()

# Apply Moving Average
window_size = 3
smoothed_df = df.apply(moving_average, window_size=window_size)

# Plot the data
plt.figure(figsize=(10, 6))
for column in df.columns:
    # plt.plot(df[column], label=f'Original {column}')
    plt.plot(smoothed_df[column], label=f'Smoothed {column}')
plt.xlabel('Time')
plt.ylabel('Values')
plt.title('Values over Time with Moving Average')
plt.legend()
plt.grid(True)
plt.show()



# df = pd.read_csv('C:/Users/daveb/Desktop/5_DOF_Robot_Arm/control_glove/script_python/robot_data/test.csv', header=None)