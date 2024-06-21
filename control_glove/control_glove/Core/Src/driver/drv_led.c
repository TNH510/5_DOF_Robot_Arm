/**
 * @file       drv_led.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-06
 * @author     Hieu Tran Ngoc
 * @brief      Driver for LED control
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "drv_led.h"
#include "bsp_gpio.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
void drv_led_turn_on_red_led(void)
{
    bsp_gpio_set_pin(LED_RED_GPIO_Port, LED_RED_Pin);
}

void drv_led_turn_on_green_led(void)
{
    bsp_gpio_set_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
}

void drv_led_turn_on_blue_led(void)
{
    bsp_gpio_reset_pin(USER_LED_GPIO_Port, USER_LED_Pin);
}

void drv_led_turn_off_red_led(void)
{
    bsp_gpio_reset_pin(LED_RED_GPIO_Port, LED_RED_Pin);
}
void drv_led_turn_off_green_led(void)
{
    bsp_gpio_reset_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
}

void drv_led_turn_off_blue_led(void)
{
    bsp_gpio_set_pin(USER_LED_GPIO_Port, USER_LED_Pin);
}

/* Private implementations -------------------------------------------------- */

/* End of file -------------------------------------------------------------- */
