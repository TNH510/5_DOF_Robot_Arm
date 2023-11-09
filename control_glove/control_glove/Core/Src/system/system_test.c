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

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
system_test_error_t system_test_init(void)
{
    drv_uart_init(); 
}

system_test_error_t system_test_general(void)
{
    drv_uart_printf("Hello, this is Smart Glove");
    return SYSTEM_TEST_OK;
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
