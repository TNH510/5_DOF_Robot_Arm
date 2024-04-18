/**
 * @file       bsp_button.h
 * @copyright  Copyright (C) 2019 Fiot Co., Ltd. All rights reserved.
 * @license    This project is released under the Fiot License.
 * @version    1.0.2
 * @date       2023-08-25
 * @author     Hieu Tran
 *
 * @brief      BSP for button
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef __BSP_BUTTON_H
#define __BSP_BUTTON_H

/* Includes ----------------------------------------------------------- */
#include "stm32f4xx_hal.h"

#include <stdbool.h>
#include <stdint.h>
#include "bsp_common.h"
/* Public defines ----------------------------------------------------- */
#define USER_BUTTON_Pin GPIO_PIN_14
#define USER_BUTTON_GPIO_Port GPIOC
#define USER_BUTTON_EXTI_IRQn EXTI15_10_IRQn
#define SW3_Pin GPIO_PIN_3
#define SW3_GPIO_Port GPIOA
#define SW3_EXTI_IRQn EXTI3_IRQn

/* Number of button */
#define BUTTON_MAX         (1)
/* Public enumerate/structure ----------------------------------------- */
/**
 * @brief Enum for button events
 *
 */
typedef enum
{
    CLICK_EVENT = 0xA0u,
    HOLD_EVENT,
    INVALID_EVENT
} button_event_t;

/**
 * @brief Enum for button state
 *
 */
typedef enum
{
    IDLE = 0x10u,
    WAIT_BUTTON_UP,
    WAIT_PRESS_TIMEOUT,
    WAIT_CLICK_TIMEOUT,
    WAIT_HOLD_TIMEOUT
} button_state_t;

/**
 * @brief Enum for id button
 *
 */
typedef enum
{
    BUTTON_1 = 0x01u,
    BUTTON_2,
    BUTTON_3,
    BUTTON_4,
    BUTTON_5,
    BUTTON_6
} button_id_t;

/**
 * @brief Struct for button variables
 *
 */
typedef struct
{
    button_state_t state;
    GPIO_TypeDef  *gpio_port;
    uint16_t       gpio_pin;
    uint32_t       timeout;
} button_t;
/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */
/**
 * @brief           Initialize bsp button
 *
 * @return true     Init success
 * @return false    Init failure
 */
bool bsp_button_init(button_t *button);

/**
 * @brief                   Check state of button
 *
 * @param button_id         ID of button want to check
 * @param interrupt_trigger Pointer to interrupt button variable
 * @param button            Button object
 * @return button_event_t   Event of button current
 */
button_event_t bsp_button_check_state_one_button(button_id_t button_id, bool *interrupt_trigger, button_t *button);

#endif  // __BSP_BUTTON_H

/* End of file -------------------------------------------------------- */
