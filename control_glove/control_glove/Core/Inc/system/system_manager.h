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
#include "drv_button.h"
#include "system_test.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef enum
{
    SYSTEM_MANAGER_OK,
    SYSTEM_MANAGER_ERROR,
} system_manager_error_t;
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
system_manager_error_t system_manager_test(void);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __SYSTEM_MANAGER_H */

/* End of file -------------------------------------------------------------- */
