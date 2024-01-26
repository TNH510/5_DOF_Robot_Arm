from PIL import Image
import numpy as np

# Read the byte string from the file
with open("C:/Users/daveb/Desktop/5_DOF_Robot_Arm/gui/Camera/raw_data/response_bmp.txt", "r") as file:
    byte_string = file.read()

# Split the byte string into individual byte values
byte_values = byte_string.split()

# Convert each byte value from string to integer
byte_data = [int(byte) for byte in byte_values]

# Define the number of bytes to delete from the beginning
bytes_to_delete = 1080  # Adjust this number according to your requirement

# Delete the specified number of bytes from the beginning
byte_data_modified = byte_data[bytes_to_delete:]

# Pack the modified byte values into a bytes object
byte_array_modified = bytes(byte_data_modified)

# Determine the dimensions of the original image
width = 640  # Adjust according to your image width
height = 480  # Adjust according to your image height

# Calculate the new dimensions of the image after removing bytes
new_width = width  # Since bytes removed from the beginning don't affect width
new_height = height - (bytes_to_delete // width)  # Adjust height accordingly

# Convert bytes data into an image using PIL
image_modified = Image.frombytes("L", (new_width, new_height), byte_array_modified)

# Display the modified image
image_modified.show()
