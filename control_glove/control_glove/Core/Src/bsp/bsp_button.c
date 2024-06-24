/**
 * @file       bsp_button.c
 * @copyright  Copyright (C) HieuTranNgoc
 * @license    This project is released under HieuTranNgoc License.
 * @version    1.0.2
 * @date       2023-08-25
 * @author     Hieu Tran
 *
 * @brief      BSP for button
 */

/* Includes ----------------------------------------------------------- */
#include "bsp_button.h"

/* Private defines ---------------------------------------------------- */
#define HAVE_BUTTON

#define BSP_BUTTON_CHECK_NUM_BUTTON_INIT(x) \
    if (x > BUTTON_MAX || x < 1)            \
    return false

#define BSP_BUTTON_CHECK_BUTTON_INIT(x) \
    if (x != true)                      \
    return false

/* Private enumerate/structure ---------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */
typedef GPIO_PinState (*button_value_t)(GPIO_TypeDef *GPIOx, uint16_t GPIO_Pin);

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */

/* Private variables -------------------------------------------------- */

/* Private function prototypes ---------------------------------------- */
/**
 * @brief Initialize button
 *
 * @param button      Object button
 * @param button_id   ID of button want to init
 * @param button_port Port button want to init
 * @param button_pin  Pin button want to init
 * @return true       Button init success
 * @return false      Button init failure
 */
static bool
bsp_button_init_one_button(button_t *button, button_id_t button_id, GPIO_TypeDef *button_port, uint16_t button_pin);
/* Function definitions ----------------------------------------------- */
static bool
bsp_button_init_one_button(button_t *button, button_id_t button_id, GPIO_TypeDef *button_port, uint16_t button_pin)
{
    BSP_BUTTON_CHECK_NUM_BUTTON_INIT(button_id);

    button->gpio_port = button_port;
    button->gpio_pin  = button_pin;
    button->state     = IDLE;

    return true;
}

bool bsp_button_init(button_t *button)
{
#ifdef HAVE_BUTTON

    /* Init 3 buttons */
    if ((bsp_button_init_one_button(&button[BUTTON_1 - 1u], BUTTON_1, SW1_GPIO_Port, SW1_Pin) == true)
        && (bsp_button_init_one_button(&button[BUTTON_2 - 1u], BUTTON_2, SW2_GPIO_Port, SW2_Pin) == true)
        && (bsp_button_init_one_button(&button[BUTTON_3 - 1u], BUTTON_3, SW3_GPIO_Port, SW3_Pin) == true))
        return true;
    return false;
#endif
}

button_event_t bsp_button_check_state_one_button(button_id_t button_id, bool *interrupt_trigger, button_t *button)
{
    button_event_t event = INVALID_EVENT;

    switch (button->state)
    {
    case IDLE:
        if (*interrupt_trigger == true && (HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_RESET))
        {
            button->state   = WAIT_PRESS_TIMEOUT;
            button->timeout = HAL_GetTick() + 20;    // Set max press time is 20ms to detect press event
        }
        break;

    case WAIT_PRESS_TIMEOUT:
        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_RESET)
            && HAL_GetTick() > button->timeout)
        {
            button->state   = WAIT_CLICK_TIMEOUT;
            button->timeout = HAL_GetTick() + 180;    // Set max press time is 200ms to detect click event
        }
        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_SET) && HAL_GetTick() <= button->timeout)
        {
            button->state = IDLE;
        }
        break;

    case WAIT_CLICK_TIMEOUT:
        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_SET) && HAL_GetTick() <= button->timeout)
        {
            /* Click handle */
            button->state = IDLE;
            /* Return state current */
            event = CLICK_EVENT;
        }

        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_RESET)
            && HAL_GetTick() > button->timeout)
        {
            button->state   = WAIT_HOLD_TIMEOUT;
            button->timeout = HAL_GetTick() + 500;    // Set max press time is 500ms to detect hold event
        }
        break;

    case WAIT_HOLD_TIMEOUT:
        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_RESET)
            && HAL_GetTick() > button->timeout)
        {
            /* Holding time out handle */
            event = HOLD_EVENT;
        }

        if ((HAL_GPIO_ReadPin(button->gpio_port, button->gpio_pin) == GPIO_PIN_SET))
        {
            button->state = IDLE;
        }
        break;

    default: button->state = IDLE; break;
    }

    /* Reset interrupt variable */
    *interrupt_trigger = false;
    return event;
}

/* End of file -------------------------------------------------------- */
