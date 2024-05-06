/**
 * @file       system_manager.c
 * @copyright  Copyright (C) 2023 TNH510
 * @version    1.0.0
 * @date       2023-11
 * @author     Hieu Tran Ngoc
 * @brief      System manager for Control Glove project
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "system_manager.h"
#include "sensor_manager.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t system_manager_init(void)
{
    // bsp_gpio_set_pin(LED_RED_GPIO_Port, LED_RED_Pin);
    // bsp_gpio_set_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
    HAL_Delay(1000);
    return sensor_manager_init();
}
base_status_t system_manager_task(void)
{
    return sensor_manager_task();
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
