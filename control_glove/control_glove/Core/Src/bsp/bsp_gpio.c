/**
 * @file       bsp_gpio.c
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

/* Includes ----------------------------------------------------------- */
#include "bsp_gpio.h"

/* Private defines ---------------------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */

/* Private variables -------------------------------------------------- */

/* Private function prototypes ---------------------------------------- */

/* Function definitions ----------------------------------------------- */

void bsp_gpio_set_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin)
{
    HAL_GPIO_WritePin(bsp_gpio_port, bsp_gpio_pin, GPIO_PIN_SET);
}

void bsp_gpio_reset_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin)
{
    HAL_GPIO_WritePin(bsp_gpio_port, bsp_gpio_pin, GPIO_PIN_RESET);
}

bool bsp_gpio_read_pin(GPIO_TypeDef *bsp_gpio_port, uint16_t bsp_gpio_pin)
{
    return HAL_GPIO_ReadPin(bsp_gpio_port, bsp_gpio_pin) == GPIO_PIN_SET ? true : false;
}

/* End of file -------------------------------------------------------- */
