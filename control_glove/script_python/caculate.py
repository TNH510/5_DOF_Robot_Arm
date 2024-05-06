import math

# Nhập giá trị quaternion
q0 = 0.8446232
q1 = 0.1913417
q2 = 0.4619398
q3 = 0.1913417

# Tính toán pitch, roll và yaw
yaw = math.atan2(2.0 * (q1 * q2 + q0 * q3), q0 * q0 + q1 * q1 - q2 * q2 - q3 * q3) * 57.29577951
pitch = -math.asin(2.0 * (q1 * q3 - q0 * q2)) * 57.29577951
roll = math.atan2(2.0 * (q0 * q1 + q2 * q3), q0 * q0 - q1 * q1 - q2 * q2 + q3 * q3) * 57.29577951

# In kết quả
print("Yaw:", yaw)
print("Pitch:", pitch)
print("Roll:", roll)