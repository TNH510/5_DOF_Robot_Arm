/**
 * @file       sensor_manager.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-04
 * @author     Hieu Tran Ngoc
 * @brief      Sensor manager module
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "sensor_manager.h"

#include "drv_imu.h"
#include "drv_magnetic.h"
#include "drv_button.h"

#include "bsp_timer.h"

#include "glove_algorithm.h"
// #include "MadgwickAHRS.h"
#include "MahonyAHRS.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
static drv_imu_t g_imu_data = {.axg = 0, .ayg = 0, .azg = 0, .gxrs = 0, .gzrs = 0};
static drv_magnetic_data_t g_magnetic_data = {.XAxis = 0, .YAxis = 0, .ZAxis = 0};

/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
static float g_freq = 100;
button_name_t g_button_state;

/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t sensor_manager_init(void)
{
    // Init imu and marg
    drv_imu_init();
    drv_magnetic_init();

    // Init button
    drv_button_init();

    // Start get tick
    bsp_timer_tick_start();

    return BS_OK;
}

base_status_t sensor_manager_task(void)
{
    // Check event button
    static count = 0;
    drv_button_check_event(&g_button_state);
    if (g_button_state == CLICK_SELECT_BUTTON)
    {
        count++;
        if (count == 4)
        {
            count = 0;
        }
    }

    // Get imu data
    drv_imu_get_data(&g_imu_data);  

    // Get marg data
    drv_magnetic_get_data(&g_magnetic_data);

    // Get sample freq 
    bsp_timer_tick_stop(&g_freq);
    bsp_timer_tick_start(); // Reset tick

    // Caculate quaternion
    MahonyAHRSupdate(g_freq, 
                        g_imu_data.gxrs, g_imu_data.gyrs, g_imu_data.gzrs, 
                        g_imu_data.axg, g_imu_data.ayg, g_imu_data.azg, 
                        -g_magnetic_data.XAxis, -g_magnetic_data.YAxis, -g_magnetic_data.ZAxis);

    // Caculate Euler to test
    float pitch, roll, yaw;
    glv_convert_euler_angle(q0, q1, q2, q3, &pitch, &roll, &yaw);

    // Printf
    // printf("%0.2f,%0.2f,%0.2f\r\n", roll, pitch, yaw);

    static uint32_t tick = 0;
    if (HAL_GetTick() - tick > 100)
    {
        tick = HAL_GetTick();
        switch (count)
        {
        case 0:
            printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.gxrs, -g_imu_data.gyrs, -g_imu_data.gzrs);
            break;
        case 1:
            printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.axg, -g_imu_data.ayg, -g_imu_data.azg);
            break;
        case 2:
            printf("%0.2f,%0.2f,%0.2f\r\n", g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);
            break;
        case 3:
            printf("%0.2f,%0.2f,%0.2f\r\n", roll, pitch, yaw);
            break;
        default:
            break;
        }
    }
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
