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

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
void glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *yaw);

void glv_pos_convert(float q0, float q1, float q2, float q3, float elbow_angle, 
                              float *x_pos, float *y_pos, float *z_pos);

float low_pass_filter(float input, float pre_output, float alpha);
/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __GLOVE_ALGORITHM_H */

/* End of file -------------------------------------------------------------- */