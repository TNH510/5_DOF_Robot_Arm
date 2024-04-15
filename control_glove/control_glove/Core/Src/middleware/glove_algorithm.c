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
#define M_PI 3.1415926

/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
static float square(float num);

/* Public implementations --------------------------------------------------- */
base_status_t glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *yaw)
{
    float sinr_cosp = 2 * (q0 * q1 + q2 * q3);
    float cosr_cosp = 1 - 2 * (q1 * q1 + q2 * q2);
    *roll = atan2f(sinr_cosp, cosr_cosp) * (180.0f / M_PI);
    
    float sinp = 2 * (q0 * q2 - q3 * q1);
    if (fabsf(sinp) >= 1)
        *pitch = copysignf(90.0f, sinp);
    else
        *pitch = asinf(sinp) * (180.0f / M_PI);
    
    float siny_cosp = 2 * (q0 * q3 + q1 * q2);
    float cosy_cosp = 1 - 2 * (q2 * q2 + q3 * q3);
    *yaw = atan2f(siny_cosp, cosy_cosp) * (180.0f / M_PI);

    return BS_OK;
}

base_status_t glv_pos_convert(float q0, float q1, float q2, float q3, 
                              float *x_pos, float *y_pos, float *z_pos)
{
	const float l1 = 200.0;
	const float l2 = 200.0;
	const float ce = 0.0;
	const float se = 1.0;
	float n = square(q0) + square(q1) + square(q2) + square(q3);

	*x_pos = l2*((ce*(square(q0) + square(q1) - square(q2) - square(q3)))/(n) - (se*(2*q0*q3 - 2*q1*q2))/(n)) + (l1*(square(q0) + square(q1) - square(q2) - square(q3)))/(n);
	*y_pos = l2*((se*(square(q0) - square(q1) + square(q2) - square(q3)))/(n) + (ce*(2*q0*q3 + 2*q1*q2))/(n)) + (l1*(2*q0*q3 + 2*q1*q2))/(n);
	*z_pos = - l2*((ce*(2*q0*q2 - 2*q1*q3))/(n) - (se*(2*q0*q1 + 2*q2*q3))/(n)) - (l1*(2*q0*q2 - 2*q1*q3))/(n);
}


/* Private implementations -------------------------------------------------- */
static float square(float num)
{
	return (float)(num * num);
}

/* End of file -------------------------------------------------------------- */
