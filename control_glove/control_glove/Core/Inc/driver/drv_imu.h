/**
 * @file       drv_imu.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran Ngoc
 * @brief      Driver for IMU
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __DRV_IMU_H
#define __DRV_IMU_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include "bsp_common.h"
#include <stdint.h>

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef struct
{
    float axg;
    float ayg;
    float azg;
    float gxrs;
    float gyrs;
    float gzrs;  
} drv_imu_t;

/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
base_status_t drv_imu_init(void);
base_status_t drv_imu_get_data(drv_imu_t *imu_data);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __DRV_IMU_H */

/* End of file -------------------------------------------------------------- */
