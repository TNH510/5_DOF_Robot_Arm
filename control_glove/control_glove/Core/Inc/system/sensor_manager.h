/**
 * @file       sensor_manager.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran Ngoc
 * @brief      Sensor manager module
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __SENSOR_MANAGER_H
#define __SENSOR_MANAGER_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>
#include "bsp_common.h"
#include "drv_button.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
base_status_t sensor_manager_init(void);
base_status_t sensor_manager_run(button_name_t event);
base_status_t sensor_manager_calib(button_name_t event);
base_status_t sensor_manager_update_data(void);
base_status_t sensor_manager_test(button_name_t event);
bool          sensor_manager_is_yaw_angle_calib();

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __SENSOR_MANAGER_H */

/* End of file -------------------------------------------------------------- */
