/**
 * @file       bsp_timer.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran Ngoc
 * @brief      Board supoprt package for Timer
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __BSP_TIMER_H
#define __BSP_TIMER_H

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
base_status_t bsp_timer_init(void);
base_status_t bsp_timer_start(void);
base_status_t bsp_timer_tick_start(void);
base_status_t bsp_timer_tick_stop(float *result_freq);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __BSP_TIMER_H */

/* End of file -------------------------------------------------------------- */
