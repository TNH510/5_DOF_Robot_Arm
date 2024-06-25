/**
 * @file       glove_algorithm.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran 
 * @brief      Algorithm for control glove
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __GLOVE_ALGORITHM_H
#define __GLOVE_ALGORITHM_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>
#include "bsp_common.h"
#include "math.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef enum
{
    GLV_CMD_ONLY_POS_TRANSMIT,
    GLV_CMD_POS_TRANSMIT_AND_START_RECORD,
    GLV_CMD_POS_TRANSMIT_AND_STOP_RECORD,
    GLV_CMD_DELETE_CURRENT_RECORD,
    GLV_CMD_POS_TRANSMIT_AND_5_AXIS_POSITIVE,
    GLV_CMD_POS_TRANSMIT_AND_5_AXIS_NEGATIVE,
} glv_cmd_t;

/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
void glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *yaw);

void glv_set_init_yaw(float yaw_value_rad);

void glv_pos_convert(float q0, float q1, float q2, float q3, float elbow_angle, 
                              float *x_pos, float *y_pos, float *z_pos);

void glv_pos_shoulder_convert(float q0, float q1, float q2, float q3, 
                              float *q1_pos, float *q2_pos, float *q3_pos);

void glv_robot_pos_convert(float x_pos, float y_pos, float z_pos, 
                           float *robot_x_pos, float *robot_y_pos, float *robot_z_pos);

void glv_robot_invert_kinematic(float x_pos, float y_pos, float z_pos,
                                float *theta1, float *theta2, float *theta3, 
                                float *theta4, float *theta5);

void glv_encrypt_sensor_data(float q0, float q1, float q2, float q3, 
                             float elbow_angle, uint8_t *data);
                             
float low_pass_filter(float input, float pre_output, float alpha);

bool glv_encode_uart_command(float x_pos, float y_pos, float z_pos, 
                             float x_vel, float y_vel, float z_vel,
                                glv_cmd_t cmd, uint8_t *encode_frame);
/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __GLOVE_ALGORITHM_H */

/* End of file -------------------------------------------------------------- */
