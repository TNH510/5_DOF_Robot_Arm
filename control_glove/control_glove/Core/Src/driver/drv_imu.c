/**
 * @file       drv_imu.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran
 * @brief      Driver for IMU
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "drv_imu.h"
#include "bsp_mpu6050.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t drv_imu_init(void)
{
    return bsp_mpu6050_init();
}

base_status_t drv_imu_get_data(drv_imu_t *imu_data)
{
    return bsp_mpu6050_get_data(&imu_data->gxrs, &imu_data->gyrs, &imu_data->gzrs, &imu_data->axg, &imu_data->ayg, &imu_data->azg);
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
