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
float robot_x_pos, robot_y_pos, robot_z_pos;
float x_pos, y_pos, z_pos;

static float x_pos_pre, y_pos_pre, z_pos_pre;
static float x_vel, y_vel, z_vel;

static float x_vel_pre = 0, y_vel_pre = 0, z_vel_pre = 0;
static float x_vel_pass, y_vel_pass, z_vel_pass;

glv_cmd_t g_cmd = GLV_CMD_ONLY_POS_TRANSMIT;
bool is_cmd_send = false;
static uint8_t data_index_send = 0;
static uint8_t encode_frame[19];

/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t sensor_manager_test(void)
{
    drv_button_check_event(&g_button_state);
    if (g_button_state == CLICK_SELECT_BUTTON)
    {
        // printf("CLICK_SELECT_BUTTON\r\n");
    }
    else if (g_button_state == HOLD_SELECT_BUTTON)
    {
        // printf("HOLD_SELECT_BUTTON\r\n");
    }
    else if (g_button_state == CLICK_LEFT_BUTTON)
    {
        // bsp_gpio_set_pin(LED_RED_GPIO_Port, LED_RED_Pin);
        g_cmd = GLV_CMD_POS_TRANSMIT_AND_START_RECORD;
        is_cmd_send = true;

    }
    else if (g_button_state == HOLD_LEFT_BUTTON)
    {
        // printf("HOLD_LEFT_BUTTON\r\n");
        // bsp_gpio_reset_pin(LED_RED_GPIO_Port, LED_RED_Pin);
        g_cmd = GLV_CMD_POS_TRANSMIT_AND_STOP_RECORD;
    }
    else if (g_button_state == CLICK_RIGHT_BUTTON)
    {
        // printf("CLICK_RIGHT_BUTTON\r\n");
        // bsp_gpio_set_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
        g_cmd = GLV_CMD_POS_TRANSMIT_AND_STOP_RECORD;
        is_cmd_send = true;
    }
    else if (g_button_state == HOLD_RIGHT_BUTTON)
    {
        // printf("HOLD_RIGHT_BUTTON\r\n");
        // bsp_gpio_reset_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
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
    sensor_manager_test();
    // drv_button_check_event(&g_button_state);
    // if (g_button_state == CLICK_SELECT_BUTTON)
    // {
    //     count++;
    //     if (count == 6)
    //     {
    //         count = 0;
    //     }
    // }
    // else if (g_button_state == HOLD_SELECT_BUTTON)
    // {
    //     // Test reset I2C
    //     bsp_i2c1_deinit();

    //     // Wait 1000ms
    //     HAL_Delay(1000);

    //     // Reinit I2C 
    //     bsp_i2c1_init();

    //     // Init sensor
    //     drv_imu_init();
    //     drv_magnetic_init();
    // }

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
    // float pitch, roll, yaw;
    // glv_convert_euler_angle(q0, q1, q2, q3, &pitch, &roll, &yaw);

    // Caculate kinematic
    glv_pos_convert(q0, q1, q2, q3, elbow_angle, &x_pos, &y_pos, &z_pos);

    // glv_robot_pos_convert(x_pos, y_pos, z_pos, &robot_x_pos, &robot_y_pos, &robot_z_pos);
    // glv_pos_shoulder_convert(q0, q1, q2, q3, &x_pos, &y_pos, &z_pos);

    static uint32_t tick = 0;
    if (HAL_GetTick() - tick > 10)
    {
        tick = HAL_GetTick();
        // printf("%0.2f,%0.2f,%0.2f\r\n", x_vel_pass, y_vel_pass, z_vel_pass);

        if (data_index_send == 0)
        {
            // Caculate velocity
            x_vel = (x_pos - x_pos_pre) / 0.2f;
            y_vel = (y_pos - y_pos_pre) / 0.2f;
            z_vel = (z_pos - z_pos_pre) / 0.2f;

            // Filter for velocity
            x_vel_pass = low_pass_filter(x_vel, x_vel_pre, 0.3f);
            y_vel_pass = low_pass_filter(y_vel, y_vel_pre, 0.3f);
            z_vel_pass = low_pass_filter(z_vel, z_vel_pre, 0.3f);

            if (x_vel_pass > 100)
            {
                x_vel_pass = 100;
            }
            else if (x_vel_pass < -100)
            {
                x_vel_pass = -100;
            }

            if (y_vel_pass > 100)
            {
                y_vel_pass = 100;
            }
            else if (y_vel_pass < -100)
            {
                y_vel_pass = -100;
            }

            if (z_vel_pass > 100)
            {
                z_vel_pass = 100;
            }
            else if (z_vel_pass < -100)
            {
                z_vel_pass = -100;
            }

            x_pos_pre = x_pos;
            y_pos_pre = y_pos;
            z_pos_pre = z_pos;

            x_vel_pre = x_vel;
            y_vel_pre = y_vel;
            z_vel_pre = z_vel;

            if ((x_vel_pass > 5 || x_vel_pass < -5) && (y_vel_pass > 5 || y_vel_pass < -5))
            {
                bsp_gpio_reset_pin(USER_LED_GPIO_Port, USER_LED_Pin);
            }
            else
            {
                bsp_gpio_set_pin(USER_LED_GPIO_Port, USER_LED_Pin);
            }

            if (g_cmd != GLV_CMD_ONLY_POS_TRANSMIT)
            {
                glv_encode_uart_command(x_pos, y_pos, z_pos, x_vel_pass, y_vel_pass, z_vel_pass, g_cmd, encode_frame);

                if (g_cmd == GLV_CMD_POS_TRANSMIT_AND_START_RECORD)
                {
                    bsp_gpio_set_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
                }
                else if (g_cmd == GLV_CMD_POS_TRANSMIT_AND_STOP_RECORD)
                {
                    bsp_gpio_set_pin(LED_GREEN_GPIO_Port, LED_RED_Pin);
                }

                g_cmd = GLV_CMD_ONLY_POS_TRANSMIT;
            }
            else
            {
                glv_encode_uart_command(x_pos, y_pos, z_pos, x_vel_pass, y_vel_pass, z_vel_pass, g_cmd, encode_frame);
                bsp_gpio_reset_pin(LED_GREEN_GPIO_Port, LED_GREEN_Pin);
                bsp_gpio_reset_pin(LED_GREEN_GPIO_Port, LED_RED_Pin);
            }
            
            HAL_Delay(5);
        }

        if (data_index_send <= 18)
        {            
            drv_uart_send_data(encode_frame + data_index_send, 1);
        }

        data_index_send++;

        if (data_index_send > (18 + 2))
        {
            data_index_send = 0;
        }
        
        // uint8_t send_data[10] = {0};
        // glv_encrypt_sensor_data(q0, q1, q2, q3, elbow_angle, send_data);

        // drv_uart_send_data(send_data, 10);

        // if (is_cmd_send == true)
        // {
        //     if (glv_encode_uart_command(x_pos, y_pos, z_pos, g_cmd, encode_frame))
        //     {
        //         drv_uart_send_data(encode_frame, sizeof(encode_frame));
        //     }

        //     g_cmd = GLV_CMD_ONLY_POS_TRANSMIT;
        //     is_cmd_send = false;
        // }
        // else
        // {
        //     if (glv_encode_uart_command(x_pos, y_pos, z_pos, GLV_CMD_ONLY_POS_TRANSMIT, encode_frame))
        //     {
        //         drv_uart_send_data(encode_frame, sizeof(encode_frame));
        //     }
        // }



        // switch (count)
        // {
        // case 0:
        //     printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.gxrs, -g_imu_data.gyrs, g_imu_data.gzrs);
        //     break;
        // case 1:
        //     printf("%0.2f,%0.2f,%0.2f\r\n", -g_imu_data.axg, -g_imu_data.ayg, g_imu_data.azg);
        //     break;
        // case 2:
        //     printf("%0.2f,%0.2f,%0.2f\r\n", g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);
        //     break;
        // case 3:
        //     printf("%0.2f,%0.2f,%0.2f\r\n", pitch, roll, yaw);
        //     break;
        // case 4:
        //     // printf("%0.2f,%0.2f,%0.2f\r\n", robot_x_pos, robot_y_pos, robot_z_pos);
        //     if (glv_encode_uart_command(robot_x_pos, robot_y_pos, robot_z_pos, GLV_CMD_ONLY_POS_TRANSMIT, encode_frame))
        //     {
        //         drv_uart_send_data(encode_frame, sizeof(encode_frame));
        //     }
        //     break;
        // case 5:
        //     // printf("%0.2f,%0.2f,%0.2f\r\n", adc_low_pass, (float)adc_value[adc_sample_count], elbow_angle*57.296f);
        //     printf("%0.2f,%0.2f,%0.2f\r\n", x_pos, y_pos, z_pos);
        //     break;
        // default:
        //     break;
        // }
    }
}

/* Private implementations -------------------------------------------------- */
/* End of file -------------------------------------------------------------- */
