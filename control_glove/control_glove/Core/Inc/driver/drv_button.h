/**
 * @file       drv_button.h
 * @copyright  Copyright (C) 2019 Fiot Co., Ltd. All rights reserved.
 * @license    This project is released under the Fiot License.
 * @version    1.1.2
 * @date       2023-08-30
 * @author     Hieu Tran
 *
 * @brief      Driver for button
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef __DRIVER_BUTTON_H
#define __DRIVER_BUTTON_H

/* Includes ----------------------------------------------------------- */
#include "bsp_button.h"
/* Public defines ----------------------------------------------------- */
/**
 * @brief Enum to all button events
 */
typedef enum
{
    CLICK_RIGHT_BUTTON = 1,
    CLICK_UP_BUTTON,
    CLICK_DOWN_BUTTON,
    CLICK_LEFT_BUTTON,
    CLICK_SELECT_BUTTON,
    CLICK_CANCEL_BUTTON,

    HOLD_RIGHT_BUTTON,
    HOLD_UP_BUTTON,
    HOLD_DOWN_BUTTON,
    HOLD_LEFT_BUTTON,
    HOLD_SELECT_BUTTON,
    HOLD_CANCEL_BUTTON,

    NO_EVENT
} button_name_t;

/**
 * @brief All variables use for check button events
 */
typedef struct
{
    button_event_t button_event_button_1;
    button_event_t button_event_button_2;
    button_event_t button_event_button_3;

    volatile bool button_1_interrupt;
    bool         *button_1_interrupt_p;
    volatile bool button_2_interrupt;
    bool         *button_2_interrupt_p;
    volatile bool button_3_interrupt;
    bool         *button_3_interrupt_p;
} drv_button_t;
/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */
/**
 * @brief Initialize driver button
 *
 * @return true   Init success
 * @return false  Init fail
 */
bool drv_button_init(void);

/**
 * @brief Use this function in while() to check polling button state
 *
 * @param button_state Pointer to variable receive button state
 */
void drv_button_check_event(button_name_t *button_state);

#endif  // __DRIVER_BUTTON_H

/* End of file -------------------------------------------------------- */
