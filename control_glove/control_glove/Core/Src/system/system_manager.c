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
#include "drv_led.h"
#include "sensor_manager.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
typedef enum
{
    SYS_MODE_TEST,
    SYS_MODE_CALIB,
    SYS_MODE_RUN,
} sys_mode_t;

/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
button_name_t g_button_state = NO_EVENT;
button_name_t g_button_state_pre = NO_EVENT;
sys_mode_t     g_mode         = SYS_MODE_TEST;

/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
void system_manager_init(void)
{
    // Do not delete this delay because IMU will be init failed
    HAL_Delay(1000);
    sensor_manager_init();
    printf("SYS_MODE_TEST\r\n");
}
void system_manager_task(void)
{
    drv_button_check_event(&g_button_state);

    switch (g_mode)
    {
    case SYS_MODE_TEST:
        sensor_manager_test(g_button_state);

        static uint32_t tick_blue_led = 0;
        if (HAL_GetTick() - tick_blue_led > 200)
        {
            if (HAL_GetTick() - tick_blue_led > 1000)
            {
                drv_led_turn_on_blue_led();
                tick_blue_led = HAL_GetTick();
            }
            else
            {
                drv_led_turn_off_blue_led();
            }
        }

        if (g_button_state_pre != HOLD_SELECT_BUTTON && g_button_state == HOLD_SELECT_BUTTON)
        {
            g_mode = SYS_MODE_CALIB;
            printf("SYS_MODE_CALIB\r\n");
            drv_led_turn_off_red_led();
            drv_led_turn_off_green_led();
            drv_led_turn_off_blue_led();
        }
        break;
    case SYS_MODE_CALIB:
        // Set Mode Calib LED state
        if (sensor_manager_is_yaw_angle_calib() == true)
        {
            drv_led_turn_on_red_led();
            drv_led_turn_on_green_led();
        }
        else
        {
            static uint32_t tick_red_led = 0;
            if (HAL_GetTick() - tick_red_led > 500)
            {
                if (HAL_GetTick() - tick_red_led > 1000)
                {
                    drv_led_turn_on_red_led();
                    tick_red_led = HAL_GetTick();
                }
                else
                {
                    drv_led_turn_off_red_led();
                }
            }
        }

        sensor_manager_calib(g_button_state);
        if (g_button_state_pre != HOLD_SELECT_BUTTON && g_button_state == HOLD_SELECT_BUTTON)
        {
            g_mode = SYS_MODE_RUN;
            printf("SYS_MODE_RUN\r\n");
            drv_led_turn_off_red_led();
            drv_led_turn_on_green_led(); // On green LED
            drv_led_turn_off_blue_led();
        }
        break;
    case SYS_MODE_RUN:
        sensor_manager_run(g_button_state);
        if (g_button_state_pre != HOLD_SELECT_BUTTON && g_button_state == HOLD_SELECT_BUTTON)
        {
            g_mode = SYS_MODE_TEST;
            printf("SYS_MODE_TEST\r\n");
            drv_led_turn_off_red_led();
            drv_led_turn_off_green_led();
            drv_led_turn_off_blue_led();
        }
        break;

    default: 
        break;
    }

    g_button_state_pre = g_button_state;
}

/* Private implementations -------------------------------------------------- */

/* End of file -------------------------------------------------------------- */
