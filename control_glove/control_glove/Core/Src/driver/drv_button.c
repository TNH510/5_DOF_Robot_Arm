/**
 * @file       drv_button.c
 * @copyright  Copyright (C) 2019 Fiot Co., Ltd. All rights reserved.
 * @license    This project is released under the Fiot License.
 * @version    1.1.2
 * @date       2023-08-30
 * @author     Hieu Tran
 *
 * @brief      Driver for button
 */

/* Includes ----------------------------------------------------------- */
#include "drv_button.h"

/* Private defines ---------------------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */

/* Private variables -------------------------------------------------- */
static button_t     button[BUTTON_MAX];
static bool         button_initialized[BUTTON_MAX];
static drv_button_t drv_button;
/* Private function prototypes ---------------------------------------- */

/* Function definitions ----------------------------------------------- */
bool drv_button_init(void)
{
    bsp_button_init(button);

    for (uint8_t i = 0; i < BUTTON_MAX; i++)
    {
        button_initialized[i] = false;
    }

    drv_button.button_event_button_1 = INVALID_EVENT;
    drv_button.button_1_interrupt = false;
    drv_button.button_1_interrupt_p = &drv_button.button_1_interrupt;

    return true;
}

void drv_button_check_event(button_name_t *button_state)
{
    /* State machine for every button */
    drv_button.button_event_button_1 =
      bsp_button_check_state_one_button(BUTTON_1, drv_button.button_1_interrupt_p, &button[BUTTON_1 - 1]);

    /* Export state to global variable */
    if (drv_button.button_event_button_1 == CLICK_EVENT)  // Right
    {
        *button_state = CLICK_SELECT_BUTTON;
    }

    else if (drv_button.button_event_button_1 == HOLD_EVENT)
    {
        *button_state = HOLD_SELECT_BUTTON;
    }

    if (drv_button.button_event_button_1 == INVALID_EVENT)
    {
        *button_state = NO_EVENT;
    }
}

void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
    if (GPIO_Pin == USER_BUTTON_Pin)
        drv_button.button_1_interrupt = true;
}

/* End of file -------------------------------------------------------- */
