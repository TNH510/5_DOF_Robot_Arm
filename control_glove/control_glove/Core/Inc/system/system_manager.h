/**
 * @file       system_manager.h
 * @copyright  Copyright (C) 2023 TNH510
 * @version    1.0.0
 * @date       2023-11
 * @author     Hieu Tran Ngoc
 * @brief      System manager for Control Glove project
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __SYSTEM_MANAGER_H
#define __SYSTEM_MANAGER_H

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
base_status_t system_manager_init(void);
base_status_t system_manager_task(void);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __SYSTEM_MANAGER_H */

/* End of file -------------------------------------------------------------- */
