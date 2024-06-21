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
sys_mode_t     g_mode         = SYS_MODE_TEST;

/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
void system_manager_init(void)
{
    sensor_manager_init();
}
void system_manager_task(void)
{
    drv_button_check_event(&g_button_state);

    switch (g_mode)
    {
    case SYS_MODE_TEST:
        sensor_manager_test(g_button_state);
        break;
    case SYS_MODE_CALIB:
        sensor_manager_calib(g_button_state);
        break;
    case SYS_MODE_RUN:
        sensor_manager_run(g_button_state);
        break;

    default: 
        break;
    }
}

/* Private implementations -------------------------------------------------- */

/* End of file -------------------------------------------------------------- */
