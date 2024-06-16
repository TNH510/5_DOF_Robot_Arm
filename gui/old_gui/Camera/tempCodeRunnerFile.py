# Convert each byte value from string to integer
byte_data = [int(byte) for byte in byte_values]

# Pack the integer byte values into a bytes object
byte_array = bytes(byte_data)

# Determine the dimensions of the image (e.g., if it's a BMP file)
width = 640  # Adjust according to your image width
height = 480  # Adjust according to your image height

# Convert bytes data into an image using PIL
image = Image.frombytes("L", (width, height), byte_array)

# Display the image
image.show()