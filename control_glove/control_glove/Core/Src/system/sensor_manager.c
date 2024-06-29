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
#include "bsp_i2c.h"

#include "glove_algorithm.h"
// #include "MadgwickAHRS.h"
#include "MahonyAHRS.h"
#include "drv_led.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
typedef enum
{
    MODE_ONLY_SEND,
    MODE_RECORD
} cmd_mode_t;

static cmd_mode_t g_mode = MODE_ONLY_SEND;
static drv_imu_t g_imu_data = {.axg = 0, .ayg = 0, .azg = 0, .gxrs = 0, .gzrs = 0};
static drv_magnetic_data_t g_magnetic_data = {.XAxis = 0, .YAxis = 0, .ZAxis = 0};

/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
static float g_freq = 100;

// ADC
static uint16_t adc_value[10] = {0}; 
float adc_avr_value;
static float adc_low_pass = 0.0f;
static uint8_t adc_sample_count = 0;
float elbow_angle_deg = 0.0;
float elbow_angle_rad = 0.0;

float robot_x_pos, robot_y_pos, robot_z_pos;
float x_pos, y_pos, z_pos;
float pitch, roll, yaw;

static float x_pos_pre, y_pos_pre, z_pos_pre;
static float x_vel, y_vel, z_vel;

static float x_vel_pre = 0, y_vel_pre = 0, z_vel_pre = 0;
static float x_vel_pass, y_vel_pass, z_vel_pass;

static uint8_t data_index_send = 0;
static uint8_t encode_frame[19];

static uint8_t g_sensor_test_mode = 0;
static bool g_is_yaw_angle_calib = false;
static float g_yaw_angle_calib_result = 0.0f;

/* Private prototypes ------------------------------------------------------- */
static void sensor_manager_run_handle_data(void);
static void sensor_manager_test_handle_data(void);
static void sensor_manager_run_button_change(button_name_t event);
static void sensor_manager_test_button_change(button_name_t event);
static float sensor_manager_caculate_current_yaw(void);

#ifdef I2C_RECOVERY
static void sensor_manager_i2c_recovery(void);
#endif

/* Public implementations --------------------------------------------------- */
base_status_t sensor_manager_init(void)
{
    // Init imu and marg
    drv_imu_init();
    drv_magnetic_init();

    // Init button
    drv_button_init();

    // Start get tick
    bsp_timer_init();
    bsp_timer_tick_start();

    // Start ADC
    bsp_adc_start();

    return BS_OK;
}

base_status_t sensor_manager_update_data(void)
{
    sensor_manager_test_handle_data();
    return BS_OK;
}

base_status_t sensor_manager_calib(button_name_t event)
{
    if (event == CLICK_LEFT_BUTTON || event == CLICK_SELECT_BUTTON)
    {
        g_yaw_angle_calib_result = sensor_manager_caculate_current_yaw();
        printf("Yaw Calib: %0.2f\r\n", g_yaw_angle_calib_result * 180.0 / M_PI);
        glv_set_init_yaw(g_yaw_angle_calib_result);

        g_is_yaw_angle_calib = true;
    }

    return BS_OK;
}

bool sensor_manager_is_yaw_angle_calib()
{
    return g_is_yaw_angle_calib;
}

base_status_t sensor_manager_test(button_name_t event)
{
    sensor_manager_test_button_change(event);
    sensor_manager_test_handle_data();

    static uint32_t tick_test = 0;
    if (HAL_GetTick() - tick_test > 200)
    {
        tick_test = HAL_GetTick();
        switch (g_sensor_test_mode)
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
            printf("%0.2f,%0.2f,%0.2f\r\n", x_pos, y_pos, z_pos);
            break;
        case 5:
            printf("%0.2f,%0.2f,%0.2f\r\n", adc_low_pass, (float)adc_value[adc_sample_count], elbow_angle_deg);
            break;
        default:
            break;
        }
    }

    return BS_OK;
}


base_status_t sensor_manager_run(button_name_t event)
{
    sensor_manager_run_button_change(event);
    sensor_manager_run_handle_data();

    static uint32_t tick_run = 0;
    if (HAL_GetTick() - tick_run > 10)
    {
        tick_run = HAL_GetTick();

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

            x_vel_pre = x_vel_pass;
            y_vel_pre = y_vel_pass;
            z_vel_pre = z_vel_pass;

            if (g_mode == MODE_RECORD)
            {
                glv_encode_uart_command(x_pos, y_pos, z_pos, x_vel_pass, y_vel_pass, z_vel_pass, GLV_CMD_POS_TRANSMIT_AND_START_RECORD, encode_frame);
            }
            else if (g_mode == MODE_ONLY_SEND)
            {
                glv_encode_uart_command(x_pos, y_pos, z_pos, x_vel_pass, y_vel_pass, z_vel_pass, GLV_CMD_ONLY_POS_TRANSMIT, encode_frame);
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
    }

    return BS_OK;
}

/* Private implementations -------------------------------------------------- */
static void sensor_manager_run_handle_data(void)
{
    // Get imu data
    if (drv_imu_get_data(&g_imu_data) != BS_OK) 
    {
        drv_led_turn_on_red_led();
        drv_led_turn_off_green_led();
    } 

    // Get marg data
    drv_magnetic_get_data(&g_magnetic_data);

    // Get RV1 data
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
        adc_low_pass = low_pass_filter(adc_avr_value, pre_output, 0.3f);
        pre_output = adc_low_pass;
    }

    // Caculate elbow angle
    elbow_angle_deg = (adc_avr_value - 3093) / (-14.2f); 
    elbow_angle_rad =  elbow_angle_deg * 0.0174533f;

    // Get sample freq 
    bsp_timer_tick_stop(&g_freq);
    bsp_timer_tick_start(); // Reset tick

    // Caculate quaternion
    MahonyAHRSupdate(g_freq, 
                        -g_imu_data.gxrs, -g_imu_data.gyrs, g_imu_data.gzrs, 
                        -g_imu_data.axg, -g_imu_data.ayg, g_imu_data.azg, 
                        g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);

    // Caculate kinematic
    glv_pos_convert(q0, q1, q2, q3, elbow_angle_rad, &x_pos, &y_pos, &z_pos);
}

static void sensor_manager_test_handle_data(void)
{
    // Get imu data
    if (drv_imu_get_data(&g_imu_data) != BS_OK) 
    {
        drv_led_turn_on_red_led();
        drv_led_turn_off_green_led();
    } 

    // Get marg data
    drv_magnetic_get_data(&g_magnetic_data);

    // Get RV1 data
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
        adc_low_pass = low_pass_filter(adc_avr_value, pre_output, 0.3f);
        pre_output = adc_low_pass;
    }

    // Caculate elbow angle
    elbow_angle_deg = (adc_avr_value - 3093) / (-14.2f);  
    elbow_angle_rad =  elbow_angle_deg * 0.0174533f;

    // Get sample freq 
    bsp_timer_tick_stop(&g_freq);
    bsp_timer_tick_start(); // Reset tick

    // Caculate quaternion
    MahonyAHRSupdate(g_freq, 
                        -g_imu_data.gxrs, -g_imu_data.gyrs, g_imu_data.gzrs, 
                        -g_imu_data.axg, -g_imu_data.ayg, g_imu_data.azg, 
                        g_magnetic_data.XAxis, g_magnetic_data.YAxis, g_magnetic_data.ZAxis);

    // Caculate Euler to test
    glv_convert_euler_angle(q0, q1, q2, q3, &pitch, &roll, &yaw);

    // Caculate kinematic
    glv_pos_convert(q0, q1, q2, q3, elbow_angle_rad, &x_pos, &y_pos, &z_pos);
}

static void sensor_manager_run_button_change(button_name_t event)
{
    if (event == CLICK_SELECT_BUTTON)
    {
        if (g_mode == MODE_ONLY_SEND)
        {
            g_mode = MODE_RECORD;
            drv_led_turn_on_blue_led();
        }
        else if (g_mode == MODE_RECORD)
        {
            g_mode = MODE_ONLY_SEND;
            drv_led_turn_off_blue_led();
        }
    }
    else if (event == HOLD_SELECT_BUTTON)
    {
        /**/
    }
    else if (event == CLICK_LEFT_BUTTON)
    {
        g_mode = MODE_RECORD;
    }
    else if (event == HOLD_LEFT_BUTTON)
    {
        /**/
    }
    else if (event == CLICK_RIGHT_BUTTON)
    {
        g_mode = MODE_ONLY_SEND;
    }
    else if (event == HOLD_RIGHT_BUTTON)
    {
        /**/
    }
}

static void sensor_manager_test_button_change(button_name_t event)
{
    if (event == CLICK_SELECT_BUTTON)
    {
        g_sensor_test_mode++;
        if (g_sensor_test_mode == 6)
        {
            g_sensor_test_mode = 0;
        }
    }
}

static float sensor_manager_caculate_current_yaw(void)
{
    // First Quadrant
    if (x_pos >= 0.0f && y_pos >= 0.0f)
    {
        g_yaw_angle_calib_result = asin(y_pos/sqrt(x_pos * x_pos + y_pos * y_pos));
    }
    // II
    else if (x_pos < 0.0f && y_pos >= 0.0f)
    {
        g_yaw_angle_calib_result = M_PI - asin(y_pos/sqrt(x_pos * x_pos + y_pos * y_pos));
    }
    // III
    else if (x_pos < 0.0f && y_pos < 0.0f)
    {
        g_yaw_angle_calib_result = M_PI + asin(-y_pos/sqrt(x_pos * x_pos + y_pos * y_pos));
    }
    // IV
    else if (x_pos >= 0.0f && y_pos < 0.0f)
    {
        g_yaw_angle_calib_result = 2 * M_PI - asin(-y_pos/sqrt(x_pos * x_pos + y_pos * y_pos));
    }

    return g_yaw_angle_calib_result; 
}

#ifdef I2C_RECOVERY
static void sensor_manager_i2c_recovery(void)
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
#endif

/* End of file -------------------------------------------------------------- */
