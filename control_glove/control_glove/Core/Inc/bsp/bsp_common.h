/**
 * @file       bsp_common.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-02
 * @author     Hieu Tran
 * @brief      BSP for common packet
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __BSP_COMMON_H
#define __BSP_COMMON_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef enum
{
    BS_OK,
    BS_ERROR
} base_status_t;

/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */


/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __BSP_COMMON_H */

/* End of file -------------------------------------------------------------- */
