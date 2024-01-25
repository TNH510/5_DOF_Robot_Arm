from PIL import Image
import numpy as np

# Read the byte string from the file
with open("C:/Users/daveb/Desktop/raw_data/response_bmp.txt", "r") as file:
    byte_string = file.read()

# Split the byte string into individual byte values
byte_values = byte_string.split()
print(byte_values[22])

# Convert each byte value from string to integer
byte_data = [int(byte) for byte in byte_values]

# Pack the integer byte values into a bytes object
byte_array = bytes(byte_data)

# Determine the dimensions of the image (e.g., if it's a BMP file)
width = 64  # Adjust according to your image width
height = 64  # Adjust according to your image height

# Convert bytes data into an image using PIL
image = Image.frombytes("L", (width, height), byte_array)

# Display the image
image.show()
