/**
 * @file       bsp_gpio.h
 * @copyright  Copyright (C) 2023 QuyLe Co., Ltd. All rights reserved.
 * @license    This project is released under the QuyLe License.
 * @version    v1.0.0
 * @date       2023-08-20
 * @author     quyle-itr-intern
 *
 * @brief      handle gpio
 *
 * @note
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef __BSP_GPIO_H
#define __BSP_GPIO_H

/* Includes ----------------------------------------------------------- */
#include "main.h"
#include "stdbool.h"
#include "bsp_common.h"
/* Public defines ----------------------------------------------------- */

/* Public enumerate/structure ----------------------------------------- */

/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */

/**
 * @brief bsp_gpio_set_pin
 *
 * This function set pin gpio
 *
 * @param bsp_gpio_port     gpio port
 * @param bsp_gpio_pin      gpio pin
 */
void bsp_gpio_set_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin);

/**
 * @brief bsp_gpio_reset_pin
 *
 * This function reset pin gpio
 *
 * @param bsp_gpio_port     gpio port
 * @param bsp_gpio_pin      gpio pin
 */
void bsp_gpio_reset_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin);

/**
 * @brief bsp_gpio_read_pin
 * 
 * @param bsp_gpio_port  gpio_port
 * @param bsp_gpio_pin   gpio_pin
 * @return true 
 * @return false 
 */
bool bsp_gpio_read_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin);

#endif  // __BSP_GPIO_H

/* End of file -------------------------------------------------------- */
