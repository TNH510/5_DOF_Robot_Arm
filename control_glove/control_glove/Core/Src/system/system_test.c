/**
 * @file       system_test.c
 * @copyright  Copyright (C) 2023 TNH510
 * @version    1.0.0
 * @date       2023-11
 * @author     Hieu Tran Ngoc
 * @brief      Test project components
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "system_test.h"
#include "drv_uart.h"
#include "drv_button.h"
#include "drv_magnetic.h"
#include "drv_acc.h"
#include "MadgwickAHRS.h"
#include "bsp_mpu6050.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
static button_name_t button_state;
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
static float deg2rad(float degrees);
static void quaternion_to_euler(float q1, float q2, float q3, float q4, float *psi, float *theta, float *phi);
/* Public implementations --------------------------------------------------- */
system_test_error_t system_test_init(void)
{
    drv_uart_init(); 
    drv_button_init();
    drv_magnetic_init();
//    if (drv_acc_init() == BS_OK)
//    {
//        printf("IMU init success\r\n");
//    }

    bsp_mpu6050_init();
}

system_test_error_t system_test_general(void)
{
    // drv_uart_printf("Hello, this is Smart Glove");
    return SYSTEM_TEST_OK;
}

system_test_error_t system_test_polling(void)
{
    // drv_button_check_event(&button_state);

    // if (button_state == CLICK_SELECT_BUTTON)
    // {
    //     drv_uart_printf("Clicking...");
    // }
    // else if (button_state == HOLD_SELECT_BUTTON)
    // {
    //     drv_uart_printf("Holding...");
    // }

    // drv_acc_data_t acc_data;
//    drv_magnetic_data_t mag_data;
    // drv_acc_get_data(&acc_data);
//    drv_magnetic_get_data(&mag_data);
    
//    MadgwickAHRSupdate(deg2rad((float)acc_data.gy_x), deg2rad((float)acc_data.gy_y), deg2rad((float)acc_data.gy_z),
//    	    (float)acc_data.acc_x, (float)acc_data.acc_y, (float)acc_data.acc_z, mag_data.XAxis, mag_data.YAxis, mag_data.ZAxis);

    // MadgwickAHRSupdate(deg2rad((float)acc_data.gy_x), deg2rad((float)acc_data.gy_y), deg2rad((float)acc_data.gy_z),
            // (float)acc_data.acc_x, (float)acc_data.acc_y, (float)acc_data.acc_z, 0, 0, 0);

    // printf("%d,%d,%d\r\n",(int)acc_data.acc_x,(int)acc_data.acc_y,(int)acc_data.acc_z);
    // printf("%0.4f,%0.4f,%0.4f,%0.4f\r\n",q0,q1,q2,q3);
    // printf("%d,%d,%d\r\n",(int)acc_data.gy_x,(int)acc_data.gy_y,(int)acc_data.gy_z);
    // float psi, theta, phi;
    // quaternion_to_euler(q0, q1, q2, q3, &psi, &theta, &phi);
    //  printf("%0.4f,%0.4f,%0.4f,%0.4f\r\n",psi,theta,phi,0);
    // printf("%0.4f\r\n",psi);
    // HAL_Delay(10);
    bsp_mpu6050_filter_task();

    return SYSTEM_TEST_OK;
}

/* Private implementations -------------------------------------------------- */
static float deg2rad(float degrees)
{
    return (float)(3.141593 / 180) * degrees;
}

static void quaternion_to_euler(float q1, float q2, float q3, float q4, float *psi, float *theta, float *phi) {
    // Calculate (yaw)
    *psi = atan2(2 * (q2 * q3 - q1 * q4), 2 * (q1 * q1 + q2 * q2) - 1);

    // Calculate (pitch)
    *theta = -asin(2 * (q2 * q4 + q1 * q3));

    // Calculate (roll)
    *phi = atan2(2 * (q3 * q4 - q1 * q2), 2 * (q1 * q1 + q4 * q4) - 1);
}
/* End of file -------------------------------------------------------------- */
