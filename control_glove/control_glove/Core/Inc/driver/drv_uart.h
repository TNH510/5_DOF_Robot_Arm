/**
 * @file       drv_uart.h
 * @copyright  Copyright (C) 2019 Fiot Co., Ltd. All rights reserved.
 * @license    This project is released under the Fiot License.
 * @version    1.0.1
 * @date       2023-09-06
 * @author     Hieu Tran
 *
 * @brief      System to manage uart events
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef __DRIVER_UART_H
#define __DRIVER_UART_H

/* Includes ----------------------------------------------------------- */
#include "bsp_uart.h"
#include "stdint.h"
/* Public defines ----------------------------------------------------- */

/* Public enumerate/structure ----------------------------------------- */

typedef enum
{
    DRV_UART_OK,
    DRV_UART_ERROR,
} drv_uart_error_t;

/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */
/**
 * @brief Init driver uartg
 * 
 * @return drv_uart_error_t Status of function
 */
drv_uart_error_t drv_uart_init(void);

/**
 * @brief Print string via COM
 * 
 * @param string             String want to print
 * @return drv_uart_error_t  Status of function
 */
drv_uart_error_t drv_uart_printf(uint8_t *string);

/**
 * @brief Driver start receive data in DMA mode
 * 
 * @return drv_uart_error_t  Status of function
 */
drv_uart_error_t drv_uart_receive(void);

/**
 * @brief Use for check unread data in cbuffer
 * 
 * @return uint32_t Number of unread data
 */
uint32_t drv_uart_num_unread_cb_data(void);

/**
 * @brief Read data from cbuffer in driver
 * 
 * @param handle_data         Pointer to array contain data
 * @param nbytes              Number of byte want to read
 * @return drv_uart_error_t   Function status
 */
drv_uart_error_t drv_uart_read_cb_data(uint8_t *handle_data, uint32_t nbytes);

void drv_uart_clear_cb(void);
#endif  // __DRIVER_UART_H

/* End of file -------------------------------------------------------- */
