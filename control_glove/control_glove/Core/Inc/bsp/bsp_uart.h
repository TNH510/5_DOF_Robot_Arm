/**
 * @file       bsp_uart.h
 * @copyright  Copyright (C) HieuTranNgoc
 * @license    This project is released under the QuyLe License.
 * @version    1.0.1
 * @date       2023-08-14
 * @author     Quy Le
 * @author     Hieu Tran
 *
 * @brief      bsp uart
 *
 * @note
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef BSP_USART_H
#define BSP_USART_H

/* Includes ----------------------------------------------------------- */
#include "bsp_common.h"
#include "cbuffer.h"
#include "main.h"

#include <string.h>
/* Public defines ---------------------------------------------------- */
#define TIME_OUT_TRANSMIT_UART 1000
#define TRUE                   1
#define FALSE                  0
#define RX_BUFFER_SIZE         2000
/* Public enumerate/structure ----------------------------------------- */

/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */

/**
 * @brief  print string to uart
 *
 * @param[in]     huart 		uart transfer data
 * @param[out]    data       	data transfer through uart
 */
void bsp_uart_printf(UART_HandleTypeDef *huart, uint8_t *data);

/**
 * @brief  read string from uart
 *
 * @param[in]     huart 		uart transfer data
 * @param[out]    data       	data read through uart
 */
void bsp_uart_read(UART_HandleTypeDef *huart, uint8_t *data);

/**
 * @brief  config receive data uart dma wait to idle
 *
 * @param[in]    huart 		    uart receive data
 * @param[in]    data       	data receive dma uart
 * @param[in]    size           size buffer data read from dma
 */
void bsp_uart_receive_to_idle_dma(UART_HandleTypeDef *huart, uint8_t *data, uint16_t size);

/**
 * @brief  unregister callback dma
 *
 * @param[in]    hdma 		    dma of uart
 * @param[in]    CallbackID     callback unregister
 */
void bsp_uart_dma_unregister_callback(DMA_HandleTypeDef *hdma, HAL_DMA_CallbackIDTypeDef CallbackID);

/**
 * @brief  uart init
 */
void bsp_uart_init(void);

/**
 * @brief  uart deinit peripheral
 */
void bsp_uart_deinit_peripheral(void);

/**
 * @brief Print length of string
 *
 * @param huart   UART want to transmit
 * @param string  String want to transmit
 * @param len     Length string want to transmit
 */
void bsp_uart_printf_len(UART_HandleTypeDef *huart, uint8_t *string, uint16_t len);

void bsp_uart_send_data(UART_HandleTypeDef *huart, uint8_t *data, uint16_t len);

#endif

/* End of file -------------------------------------------------------- */
