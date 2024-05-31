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
#include "drv_uart.h"

#include "bsp_timer.h"
#include "bsp_adc.h"

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
base_status_t sensor_manager_test(void)
{
    drv_button_check_event(&g_button_state);
    if (g_button_state == CLICK_SELECT_BUTTON)
    {
        printf("CLICK_SELECT_BUTTON\r\n");
    }
    else if (g_button_state == HOLD_SELECT_BUTTON)
    {
        printf("HOLD_SELECT_BUTTON\r\n");
    }
    else if (g_button_state == CLICK_LEFT_BUTTON)
    {
        printf("CLICK_LEFT_BUTTON\r\n");
    }
    else if (g_button_state == HOLD_LEFT_BUTTON)
    {
        printf("HOLD_LEFT_BUTTON\r\n");
    }
    else if (g_button_state == CLICK_RIGHT_BUTTON)
    {
        printf("CLICK_RIGHT_BUTTON\r\n");
    }
    else if (g_button_state == HOLD_RIGHT_BUTTON)
    {
        printf("HOLD_RIGHT_BUTTON\r\n");
    }

    return BS_OK;
}

base_status_t sensor_manager_init(void)
{
    // Init imu and marg
    drv_imu_init();
    drv_magnetic_init();

    // Init button
    drv_button_init();

    // Start get tick
    bsp_timer_tick_start();

    // Start ADC
    bsp_adc_start();

    return BS_OK;
}

base_status_t sensor_manager_task(void)
{
    // Check event button
    static count = 4;
    drv_button_check_event(&g_button_state);
    if (g_button_state == CLICK_SELECT_BUTTON)
    {
        count++;
        if (count == 6)
        {
            count = 0;
        }
    }
    else if (g_button_state == HOLD_SELECT_BUTTON)
    {
        // Test reset I2C
        bsp_i2c1_deinit();

        // Wait 1000ms
        HAL_Delay(1000);

        // Reinit I2C 
        bsp_i2c1_init();

        // Init sensor
        drv_imu_init();
        drv_magnetic_init();
    }

    // Get imu data
    drv_imu_get_data(&g_imu_data);  

    // Get marg data
    drv_magnetic_get_data(&g_magnetic_data);

    // Get RV1 data
    static uint16_t adc_value[10] = {0}; 
    float adc_avr_value;
    static float adc_low_pass = 0.0f;
    static uint8_t adc_sample_count = 0;

    bsp_adc_get_data(&adc_value[adc_sample_count]);
    adc_sample_count++;

    if (adc_sample_count == 10)
    {
        uint16_t sum = 0;
        adc_sample_count = 0;
        // Caculate average 20 sample for RV1
        for (int i = 0; i < 10; i++)
        {
            sum += adc_value[i];
        }

        adc_avr_value = sum / 10.0f;

        // Low pass filter for RV1
        static float pre_output = 0;
        adc_low_pass = low_pass_filter(adc_avr_value, pre_output, 0.03f);
        pre_output = adc_avr_value;
    }

    // Caculate elbow angle
    float elbow_angle = adc_avr_value * 0.0013189f - 1.1519173f; 

    // Get sample freq 
    bsp_timer_tick_stop(&g_freq);
    bsp_timer_tick_start(); // Reset tick

    // Caculate quaternion
    MahonyAHRSupdate(g_freq, 
                        -g_imu_data.gxrs, -g_imu_data.gyrs, g_imu_data.gzrs, 
                        -g_imu_data.axg, -g_imu_data.ayg, g_imu_data.azg, 
                        g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);

    // Caculate Euler to test
    float pitch, roll, yaw;
    glv_convert_euler_angle(q0, q1, q2, q3, &pitch, &roll, &yaw);

    // Caculate kinematic
    float robot_x_pos, robot_y_pos, robot_z_pos;
    float x_pos, y_pos, z_pos;
    glv_pos_convert(q0, q1, q2, q3, elbow_angle, &x_pos, &y_pos, &z_pos);
    glv_robot_pos_convert(x_pos, y_pos, z_pos, &robot_x_pos, &robot_y_pos, &robot_z_pos);
    // glv_pos_shoulder_convert(q0, q1, q2, q3, &x_pos, &y_pos, &z_pos);

    static uint32_t tick = 0;
    if (HAL_GetTick() - tick > 100)
    {
        tick = HAL_GetTick();

        // uint8_t send_data[10] = {0};
        // glv_encrypt_sensor_data(q0, q1, q2, q3, elbow_angle, send_data);

        // drv_uart_send_data(send_data, 10);

        switch (count)
        {
        case 0:
            printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.gxrs, -g_imu_data.gyrs, g_imu_data.gzrs);
            break;
        case 1:
            printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.axg, -g_imu_data.ayg, g_imu_data.azg);
            break;
        case 2:
            printf("%0.2f,%0.2f,%0.2f\r\n", g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);
            break;
        case 3:
            printf("%0.2f,%0.2f,%0.2f\r\n", pitch, roll, yaw);
            break;
        case 4:
            printf("%0.2f,%0.2f,%0.2f\r\n", robot_x_pos, robot_y_pos, robot_z_pos);
            break;
        case 5:
            // printf("%0.2f,%0.2f,%0.2f\r\n", adc_low_pass, (float)adc_value[adc_sample_count], elbow_angle*57.296f);
            printf("%0.2f,%0.2f,%0.2f\r\n", x_pos, y_pos, z_pos);
            break;
        default:
            break;
        }
    }
}

/* Private implementations -------------------------------------------------- */
/* End of file -------------------------------------------------------------- */
