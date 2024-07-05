/**
 * @file       glove_algorithm.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran 
 * @brief      Algorithm for control glove
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "glove_algorithm.h"
#include <math.h>

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
typedef struct 
{
	uint8_t start_frame;
	uint8_t cmd;
	uint8_t x_pos[3];
	uint8_t y_pos[3];
	uint8_t z_pos[3];
	uint8_t x_vel[2];
	uint8_t y_vel[2];
	uint8_t z_vel[2];
	uint8_t crc;
} glv_protocol_t;

typedef struct
{
    uint8_t value  		: 7;
    uint8_t sign_bit    : 1;
} glv_pos_byte_high_t;

/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
static float sy = 0.0f, cy = 1.0f; // Yaw init is 0

/* Private prototypes ------------------------------------------------------- */
static float square(float num);
static bool encode_pos(float value, uint8_t *result);
static bool encode_vel(float value, uint8_t *result);
static uint8_t crc_8_atm(uint8_t *data, uint16_t length);

/* Public implementations --------------------------------------------------- */
void glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *q2aw)
{
  *q2aw   = -atan2(2.0f * (q1 * q2 + q0 * q3), q0 * q0 + q1 * q1 - q2 * q2 - q3 * q3) * 57.29577951;   
  *pitch = asin(2.0f * (q1 * q3 - q0 * q2)) * 57.29577951;
  *roll  = atan2(2.0f * (q0 * q1 + q2 * q3), q0 * q0 - q1 * q1 - q2 * q2 + q3 * q3) * 57.29577951;
}

void glv_set_init_yaw(float yaw_value_rad)
{
	sy = sin(-yaw_value_rad);
	cy = cos(-yaw_value_rad);
}

void glv_pos_convert(float q0, float q1, float q2, float q3, float elbow_angle, 
                              float *x, float *y, float *z)
{
	const float l1 = 15.0;
	const float l2 = 17.0;
	float ce = cos(elbow_angle);
	float se = sin(elbow_angle);

	*x = l2*(ce*((cy*(square(q0) + square(q1) - square(q2) - square(q3))) - (sy*(2*q0*q3 + 2*q1*q2))) - se*((sy*(square(q0) - square(q1) + square(q2) - square(q3))) + (cy*(2*q0*q3 - 2*q1*q2)))) + l1*((cy*(square(q0) + square(q1) - square(q2) - square(q3))) - (sy*(2*q0*q3 + 2*q1*q2)));
	*y = l2*(ce*((sy*(square(q0) + square(q1) - square(q2) - square(q3))) + (cy*(2*q0*q3 + 2*q1*q2))) + se*((cy*(square(q0) - square(q1) + square(q2) - square(q3))) - (sy*(2*q0*q3 - 2*q1*q2)))) + l1*((sy*(square(q0) + square(q1) - square(q2) - square(q3))) + (cy*(2*q0*q3 + 2*q1*q2)));
	*z = - l2*((ce*(2*q0*q2 - 2*q1*q3)) - (se*(2*q0*q1 + 2*q2*q3))) - (l1*(2*q0*q2 - 2*q1*q3));
}

void glv_pos_shoulder_convert(float q0, float q1, float q2, float q3, 
                              float *x, float *y, float *z)
{
	const float l1 = 12.0;

	*x = (l1*(square(q0) + square(q1) - square(q2) - square(q3)));
    *y = (l1*(2*q0*q3 + 2*q1*q2));
    *z = -(l1*(2*q0*q2 - 2*q1*q3));
}

void glv_robot_pos_convert(float x_pos, float y_pos, float z_pos, 
                           float *robot_x_pos, float *robot_y_pos, float *robot_z_pos)
{
	// Offset FIRST POINT to zero (31.8, 0, 0)
	x_pos = x_pos - 31.8f;

	// Set axis direction
	x_pos = x_pos * (-1);
	y_pos = y_pos * (-1);
	z_pos = z_pos * (-1);

	// Scale to robot axis (x5)
	x_pos = x_pos * 5;
	y_pos = y_pos * 5;
	z_pos = z_pos * 5;

	// Set final point to ROBOT FIRST POINT
	x_pos = x_pos + 500;

	// Return value
	*robot_x_pos = x_pos;
	*robot_y_pos = y_pos;
	*robot_z_pos = z_pos;
}

void glv_robot_invert_kinematic(float x_pos, float y_pos, float z_pos,
                                float *theta1, float *theta2, float *theta3, 
                                float *theta4, float *theta5)
{
	float t1, t2, t3, t4, t5, s2, c2, s3, c3, m, n;
	const float l1 = 690.0f, l2 = 440.0f, l3 = 500.0f, l4 = 0.0f, l5 = 230.0f; 
	float roll, pitch;
	roll = 0.0;
	pitch = - M_PI / 2;
	t1 = atan2(y_pos, x_pos);
	t5 = roll - t1;
	m = sqrt(x_pos * x_pos + y_pos * y_pos);
	n = z_pos - l1 + l5;
	c3 = (m * m + n * n - l2 * l2 - l3 * l3) / (2 * l2 * l3);
	/* s3 has 2 value --> take the value of -sin */
	s3 = sqrt(1 - c3 * c3);
	t3 = atan2(s3, c3);
	if (t3 >= -M_PI / 6 && t3 <= (4 * M_PI) / 9)
	{
		/* Do nothing*/
	}
	else
	{
		s3 = -1 * sqrt(1 - c3 * c3);
		t3 = atan2(s3, c3);
	}
	/* Angle 3 */
	c2 = m * (l3 * c3 + l2) + n * (l3 * s3);
	s2 = n * (l3 * c3 + l2) - m * (l3 * s3);
	/* Angle 2 */
	t2 = atan2(s2, c2);
	/* Angle 4 */
	t4 = pitch - t2 - t3;
	*theta1 = t1 / M_PI * 180.0;
	*theta2 = t2 / M_PI * 180.0;
	*theta3 = t3 / M_PI * 180.0;
	*theta4 = t4 / M_PI * 180.0;
	*theta5 = t5 / M_PI * 180.0;
}								

float low_pass_filter(float input, float pre_output, float alpha) 
{
    return alpha * input + (1 - alpha) * pre_output;
}

void glv_encrypt_sensor_data(float q0, float q1, float q2, float q3, 
                             float elbow_angle, uint8_t *data)
{
	// Format quaternion data: [X--> 0+, 1-, 2->5: reserved][XXXX--> 0000, 9999] (q0->q3)
	float q[4];
	q[0] = q0; q[1] = q1; q[2] = q2; q[3] = q3;

	for (int i = 0; i < 4; i++)
	{
		// Check value equal 1.0 ?
		if (q[i] == 1.0f)
		{
			q[i] = q[i] - 0.0001f;
		}
		else if (q[i] == -1.0f)
		{
			q[i] = q[i] + 0.0001f;
		}

		if (q[i] >= 0)
		{
			q[i] = q[i] * 10000.0f;
		}
		else
		{
			q[i] = q[i] * (-1.0f) * 10000.0f + 10000.0f;
		}
	}

	uint16_t q_temp[4] = {0};
	for (int t = 0; t < 4; t++)
	{
		q_temp[t] = (uint16_t)q[t];
	}

	// Format elbow angle data: [XXXXX, 00000-> 52360]
	uint16_t elbow_temp = 0;
	elbow_temp = (uint16_t)(elbow_angle * 10.0f);

	// Change all data to byte array: [q0-h][q0-l][q1-h][q1-l][...][elbow-h][elbow-l] --> 10 bytes

	for (int j = 0; j < 4; j++)
	{
		data[2*j] = (uint8_t)((q_temp[j] >> 8) & 0xFF);
		data[2*j + 1] = (uint8_t)(q_temp[j] & 0xFF);
	}

	data[8] = (uint8_t)((elbow_temp >> 8) & 0xFF);
	data[9] = (uint8_t)(elbow_temp & 0xFF);
}

bool glv_encode_uart_command(float x_pos, float y_pos, float z_pos, 
                             float x_vel, float y_vel, float z_vel,
                                glv_cmd_t cmd, uint8_t *encode_frame)
{
	glv_protocol_t frame;

	// Set start frame and cmd
	frame.start_frame = 0xAA;
	frame.cmd = (uint8_t) cmd;

	bool status = encode_pos(x_pos, &frame.x_pos[0]);
	status &= encode_pos(y_pos, &frame.y_pos[0]);
	status &= encode_pos(z_pos, &frame.z_pos[0]);

	status &= encode_vel(x_vel, &frame.x_vel[0]);
	status &= encode_vel(y_vel, &frame.y_vel[0]);
	status &= encode_vel(z_vel, &frame.z_vel[0]);

	if (status == true)
	{
		// Return result
		encode_frame[0] = frame.start_frame;
		encode_frame[1] = frame.cmd;
		encode_frame[2] = frame.x_pos[0];
		encode_frame[3] = frame.x_pos[1];
		encode_frame[4] = frame.x_pos[2];
		encode_frame[5] = frame.y_pos[0];
		encode_frame[6] = frame.y_pos[1];
		encode_frame[7] = frame.y_pos[2];
		encode_frame[8] = frame.z_pos[0];
		encode_frame[9] = frame.z_pos[1];
		encode_frame[10] = frame.z_pos[2];

		encode_frame[12] = frame.x_vel[0];
		encode_frame[13] = frame.x_vel[1];
		encode_frame[14] = frame.y_vel[0];
		encode_frame[15] = frame.y_vel[1];
		encode_frame[16] = frame.z_vel[0];
		encode_frame[17] = frame.z_vel[1];

		encode_frame[18] = 0xFF;

		/* Caculate CRC */
		frame.crc = crc_8_atm(encode_frame, 11);
		encode_frame[11] = frame.crc;

		return true;
	}
	else
	{
		return false;
	}

}

/* Private implementations -------------------------------------------------- */
static float square(float num)
{
	return (float)(num * num);
}

static bool encode_vel(float value, uint8_t *result)
{
	// Check value
	if (value <= 100.0 && value >= -100.0)
	{
		result[0] = (uint8_t)((((int32_t)(value * 100.0)) & 0xFF00) >> 8);
		result[1] = (uint8_t)(((int32_t)(value * 100.0)) & 0x00FF);
		return true;
	}
	else
	{
		return false;
	}
}

static bool encode_pos(float value, uint8_t *result)
{
	// Check value
	if (value < 100.0f && value > -99.9999f)
	{
		glv_pos_byte_high_t pos_h;
		uint32_t mul_1000_value = 0;

		// Check value is positive or negative
		if (value >= 0)
		{
			pos_h.sign_bit = 0;
			mul_1000_value = (uint32_t)(value * 10000.0f);
		}
		else
		{
			pos_h.sign_bit = 1;
			mul_1000_value = (uint32_t)(value * (-10000.0f));
		}		

		// Pos value handle
		pos_h.value = ((uint8_t)((mul_1000_value & 0xFF0000) >> 16)) & 0b01111111;
		result[0] = pos_h.value | (pos_h.sign_bit << 7);
		result[1] = (uint8_t)((mul_1000_value & 0x00FF00) >> 8);
		result[2] = (uint8_t)(mul_1000_value & 0x0000FF);

		return true;
	}
	else
	{
		return false;
	}
}

static uint8_t crc_8_atm(uint8_t *data, uint16_t length)
{
  uint8_t crc = 0;
  for (uint16_t i = 0; i < length; i++)
  {
    crc ^= data[i];

    for (uint8_t bit = 0; bit < 8; bit++)
    {
      if (crc & 0x80)
      {
        crc = (crc << 1) ^ 0x07;
      }
      else
      {
        crc <<= 1;
      }
    }
  }

  return crc;
}
/* End of file -------------------------------------------------------------- */
