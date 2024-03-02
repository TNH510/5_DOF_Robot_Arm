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

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
static button_name_t button_state;
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
system_test_error_t system_test_init(void)
{
    drv_uart_init(); 
    drv_button_init();
    drv_magnetic_init();
    drv_acc_init();
}

system_test_error_t system_test_general(void)
{
    drv_uart_printf("Hello, this is Smart Glove");
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

    drv_acc_data_t acc_data;
    drv_acc_get_data(&acc_data);
    // printf("%d,%d,%d\r\n",(int)acc_data.acc_x,(int)acc_data.acc_y,(int)acc_data.acc_z);
    printf("%d,%d,%d\r\n",(int)acc_data.gy_x,(int)acc_data.gy_y,(int)acc_data.gy_z);
    HAL_Delay(50);

    return SYSTEM_TEST_OK;
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
