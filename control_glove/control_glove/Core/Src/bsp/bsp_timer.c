/**
 * @file       bsp_timer.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran Ngoc
 * @brief      Board support package for Timer
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "bsp_timer.h"
#include "stm32f4xx_hal.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
static uint32_t tick_start = 0;
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t bsp_timer_init(void)
{
    return BS_OK;
}

base_status_t bsp_timer_start(void)
{
    return BS_OK;
}
base_status_t bsp_timer_tick_start(void)
{
    tick_start = HAL_GetTick();
}
base_status_t bsp_timer_tick_stop(float *result_freq)
{
    *result_freq = 1000.0f / (HAL_GetTick() - tick_start);
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
