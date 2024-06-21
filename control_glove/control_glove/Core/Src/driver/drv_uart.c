/**
 * @file       drv_uart.c
 * @copyright  Copyright (C) HieuTranNgoc
 * @license    This project is released under HieuTranNgoc License.
 * @version    1.0.0
 * @date       2023-09-05
 * @author     Hieu Tran
 *
 * @brief      System to manage uart events
 */

/* Includes ----------------------------------------------------------- */
#include "drv_uart.h"

/* Private defines ---------------------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */
extern UART_HandleTypeDef huart1;
extern uint8_t            rx_buffer_user[RX_BUFFER_SIZE];
extern cbuffer_t          cb;
/* Private variables -------------------------------------------------- */

/* Private function prototypes ---------------------------------------- */

/* Function definitions ----------------------------------------------- */

drv_uart_error_t drv_uart_init(void)
{
    bsp_uart_init();
    return DRV_UART_OK;
}

drv_uart_error_t drv_uart_send_data(uint8_t *data, uint16_t len)
{
    bsp_uart_send_data(&huart1, data, len);
}

drv_uart_error_t drv_uart_printf(uint8_t *string)
{
    bsp_uart_printf(&huart1, string);
    return DRV_UART_OK;
}

drv_uart_error_t drv_uart_receive(void)
{
    bsp_uart_receive_to_idle_dma(&huart1, rx_buffer_user, RX_BUFFER_SIZE);
}

uint32_t drv_uart_num_unread_cb_data(void)
{
    return cb_data_count(&cb);
}

drv_uart_error_t drv_uart_read_cb_data(uint8_t *handle_data, uint32_t nbytes)
{
    cb_read(&cb, handle_data, nbytes);

    return DRV_UART_OK;
}

void drv_uart_clear_cb(void)
{
    cb_clear(&cb);
}

/* End of file -------------------------------------------------------- */
