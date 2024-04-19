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
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
static float square(float num);

/* Public implementations --------------------------------------------------- */
void glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *yaw)
{
  *yaw   = -atan2(2.0f * (q1 * q2 + q0 * q3), q0 * q0 + q1 * q1 - q2 * q2 - q3 * q3) * 57.29577951;   
  *pitch = asin(2.0f * (q1 * q3 - q0 * q2)) * 57.29577951;
  *roll  = atan2(2.0f * (q0 * q1 + q2 * q3), q0 * q0 - q1 * q1 - q2 * q2 + q3 * q3) * 57.29577951;
}

void glv_pos_convert(float q0, float q1, float q2, float q3, float elbow_angle, 
                              float *x_pos, float *y_pos, float *z_pos)
{
	const float l1 = 200.0;
	const float l2 = 200.0;
	float ce = cos(elbow_angle);
	float se = sin(elbow_angle);
	float n = square(q0) + square(q1) + square(q2) + square(q3);

	*x_pos = l2*((ce*(square(q0) + square(q1) - square(q2) - square(q3)))/(n) - (se*(2*q0*q3 - 2*q1*q2))/(n)) + (l1*(square(q0) + square(q1) - square(q2) - square(q3)))/(n);
	*y_pos = l2*((se*(square(q0) - square(q1) + square(q2) - square(q3)))/(n) + (ce*(2*q0*q3 + 2*q1*q2))/(n)) + (l1*(2*q0*q3 + 2*q1*q2))/(n);
	*z_pos = - l2*((ce*(2*q0*q2 - 2*q1*q3))/(n) - (se*(2*q0*q1 + 2*q2*q3))/(n)) - (l1*(2*q0*q2 - 2*q1*q3))/(n);
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

/* Private implementations -------------------------------------------------- */
static float square(float num)
{
	return (float)(num * num);
}
/* End of file -------------------------------------------------------------- */
