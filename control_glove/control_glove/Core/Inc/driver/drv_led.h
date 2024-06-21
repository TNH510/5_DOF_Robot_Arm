/**
 * @file       drv_led.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-06
 * @author     Hieu Tran
 * @brief      Driver for LED control
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __DRV_LED_H
#define __DRV_LED_H

#ifdef __cplusplus
extern "C"
{
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>
#include "main.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
void drv_led_turn_on_red_led(void);
void drv_led_turn_on_green_led(void);
void drv_led_turn_on_blue_led(void);
void drv_led_turn_off_red_led(void);
void drv_led_turn_off_green_led(void);
void drv_led_turn_off_blue_led(void);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __DRV_LED_H */

/* End of file -------------------------------------------------------------- */
