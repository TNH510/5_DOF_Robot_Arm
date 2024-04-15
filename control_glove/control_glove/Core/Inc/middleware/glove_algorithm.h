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
base_status_t glv_convert_euler_angle(float q0, float q1, float q2, float q3, 
                                      float *pitch, float *roll, float *yaw);

base_status_t glv_pos_convert(float q0, float q1, float q2, float q3, 
                              float *x_pos, float *y_pos, float *z_pos);
/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __GLOVE_ALGORITHM_H */

/* End of file -------------------------------------------------------------- */
