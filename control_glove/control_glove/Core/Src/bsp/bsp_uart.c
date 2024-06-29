/**
 * @file       bsp_uart.c
 * @copyright  Copyright (C) HieuTranNgoc
 * @version    1.0.1
 * @date       2023-08-14
 * @author     Hieu Tran
 *
 * @brief      bsp uart
 *
 * @note
 */

/* Includes ----------------------------------------------------------- */
#include "bsp_uart.h"
/* Private defines ---------------------------------------------------- */

/* Private enumerate/structure ---------------------------------------- */

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */
extern UART_HandleTypeDef huart2;
cbuffer_t                 cb;

/* Private variables -------------------------------------------------- */
/* Use for cbuffer */
uint8_t rx_buffer[RX_BUFFER_SIZE];

/* User for handle data receive in dma mode */
uint8_t         rx_buffer_user[RX_BUFFER_SIZE];
static uint8_t  rx_buffer_one[RX_BUFFER_SIZE];
static uint8_t  rx_buffer_two[RX_BUFFER_SIZE];
static uint8_t *p_buffer_for_reception;
static uint8_t *p_buffer_for_user;

/* Num of received chars */
__IO uint32_t num_received_chars;

typedef void (*bootloader_handle_error_t)(void);
bootloader_handle_error_t bsp_uart_bootloader_error;
/* Private function prototypes ---------------------------------------- */

/* Function definitions ----------------------------------------------- */
void bsp_uart_init(void)
{
    cb_init(&cb, rx_buffer, RX_BUFFER_SIZE);
}

void bsp_uart_printf(UART_HandleTypeDef *huart, uint8_t *string)
{
    HAL_UART_Transmit(huart, string, strlen((char *) string), TIME_OUT_TRANSMIT_UART);
}

void bsp_uart_printf_len(UART_HandleTypeDef *huart, uint8_t *string, uint16_t len)
{
    HAL_UART_Transmit(huart, string, len, TIME_OUT_TRANSMIT_UART);
}

void bsp_uart_send_data(UART_HandleTypeDef *huart, uint8_t *data, uint16_t len)
{
    HAL_UART_Transmit(huart, data, len, 10);
}

void bsp_uart_receive_to_idle_dma(UART_HandleTypeDef *huart, uint8_t *data, uint16_t size)
{
    p_buffer_for_reception = rx_buffer_one;
    p_buffer_for_user      = rx_buffer_two;

    if (HAL_OK != HAL_UARTEx_ReceiveToIdle_DMA(huart, data, size))
    {
        Error_Handler();
    }
}

void bsp_uart_dma_unregister_callback(DMA_HandleTypeDef *hdma, HAL_DMA_CallbackIDTypeDef CallbackID)
{
    HAL_DMA_UnRegisterCallback(hdma, CallbackID);
}

void bsp_uart_deinit_peripheral(void)
{
    HAL_NVIC_DisableIRQ(DMA2_Stream2_IRQn);
    HAL_UART_DeInit(&huart2);
}

void bsp_uart_handle_data(UART_HandleTypeDef *huart, uint16_t size)
{
    static uint16_t old_pos = 0;
    uint8_t        *p_temp;
    uint16_t        i;

    /*Check if number of received data in reception buffer has changed*/
    if (size != old_pos)
    {
        /*Check if of index in reception buffer has be increased */
        if (size > old_pos)
        {
            /*Current position is higher than previous one*/
            num_received_chars = size - old_pos;
            /*Coppy received data in "User" buffer for evacution*/
            for (i = 0; i < num_received_chars; i++)
            {
                p_buffer_for_user[i] = rx_buffer_user[old_pos + i];
            }
        }
        else
        {
            /*Current position is lower than previous one: end of buffer*/
            /*First coppy data from current potion till end of buffer*/
            num_received_chars = RX_BUFFER_SIZE - old_pos;
            /*Coppy received data in "User" buffer for evacuation*/
            for (i = 0; i < num_received_chars; i++)
            {
                p_buffer_for_user[i] = rx_buffer_user[i + old_pos];
            }

            /*Check and continue with beginning of buffer*/
            if (size > 0)
            {
                for (i = 0; i < size; i++)
                {
                    p_buffer_for_user[i] = rx_buffer_user[i];
                }
                num_received_chars = num_received_chars + size;
            }
        }
        /* Write data to cbuffer */
        cb_write(&cb, p_buffer_for_user, num_received_chars);

        /*Swap buffers for next bytes to be processed*/
        /*p_buffer_for_user <--> p_buffer_for_reception*/
        p_temp                 = p_buffer_for_user;
        p_buffer_for_user      = p_buffer_for_reception;
        p_buffer_for_reception = p_temp;
    }

    /*Update old_pos*/
    old_pos = size;
}

void HAL_UARTEx_RxEventCallback(UART_HandleTypeDef *huart, uint16_t size)
{
    bsp_uart_handle_data(&huart, size);
}

/* End of file -------------------------------------------------------- */
