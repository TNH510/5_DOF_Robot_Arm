/**
 * @file       bsp_i2c.c
 * @copyright  Copyright (C) HieuTranNgoc
 * @license    This project is released under HieuTranNgoc License.
 * @version    1.0.0
 * @date       2023-08-19
 * @author     Hieu Tran Ngoc
 *
 * @brief      handle i2c
 *
 * @note
 */

/* Includes ----------------------------------------------------------- */
#include "bsp_i2c.h"

/* Private defines ---------------------------------------------------- */
#define BSP_I2C_TIMEOUT     100
#define BSP_I2C_TIMEOUT_DMA 50000
#define BSP_I2C1            1

/* Private enumerate/structure ---------------------------------------- */
#if BSP_I2C_DMA == 1
typedef void (*i2c_mem_dma_tx_cplt_callback_t)(I2C_HandleTypeDef *hi2c);
typedef void (*i2c_mem_dma_rx_cplt_callback_t)(I2C_HandleTypeDef *hi2c);
#endif

#if BSP_I2C1 == 1
extern I2C_HandleTypeDef hi2c1;
#endif
#if BSP_I2C2 == 1
extern I2C_HandleTypeDef hi2c2;
#endif
#if BSP_I2C3 == 1
extern I2C_HandleTypeDef hi2c3;
#endif

/* Private macros ----------------------------------------------------- */

/* Public variables --------------------------------------------------- */

/* Private variables -------------------------------------------------- */
#if BSP_I2C_DMA == 1

#if BSP_I2C1 == 1
i2c_mem_dma_tx_cplt_callback_t i2c1_mem_dma_tx_cplt_callback = NULL;
i2c_mem_dma_rx_cplt_callback_t i2c1_mem_dma_rx_cplt_callback = NULL;
#endif
#if BSP_I2C2 == 1
i2c_mem_dma_tx_cplt_callback_t i2c2_mem_dma_tx_cplt_callback = NULL;
i2c_mem_dma_rx_cplt_callback_t i2c2_mem_dma_rx_cplt_callback = NULL;
#endif
#if BSP_I2C3 == 1
i2c_mem_dma_tx_cplt_callback_t i2c3_mem_dma_tx_cplt_callback = NULL;
i2c_mem_dma_rx_cplt_callback_t i2c3_mem_dma_rx_cplt_callback = NULL;
#endif

#endif
/* Private function prototypes ---------------------------------------- */

/* Function definitions ----------------------------------------------- */

#if BSP_I2C1 == 1
void bsp_i2c1_deinit(void)
{
    HAL_I2C_DeInit(&hi2c1);
}

void bsp_i2c1_init(void)
{
    hi2c1.Instance             = I2C1;
    hi2c1.Init.ClockSpeed      = 100000;
    hi2c1.Init.DutyCycle       = I2C_DUTYCYCLE_2;
    hi2c1.Init.OwnAddress1     = 0;
    hi2c1.Init.AddressingMode  = I2C_ADDRESSINGMODE_7BIT;
    hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
    hi2c1.Init.OwnAddress2     = 0;
    hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
    hi2c1.Init.NoStretchMode   = I2C_NOSTRETCH_DISABLE;
    if (HAL_I2C_Init(&hi2c1) != HAL_OK)
    {
        Error_Handler();
    }
}

bool bsp_i2c1_is_device_ready(uint8_t address_device)
{
    return (HAL_I2C_IsDeviceReady(&hi2c1, address_device, 5, BSP_I2C_TIMEOUT) == HAL_ERROR) ? false : true;
}

bool bsp_i2c1_is_busy()
{
    return (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_BUSY) ? false : true;
}

bool bsp_i2c1_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write(&hi2c1, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data,
                          BSP_I2C_TIMEOUT)
        == HAL_ERROR)
        return false;
    return true;
}

bool bsp_i2c1_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read(&hi2c1, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data, BSP_I2C_TIMEOUT)
        != HAL_OK)
        return false;
    return true;
}

#if BSP_I2C_DMA == 1
bool bsp_i2c1_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write_DMA(&hi2c1, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data)
        == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}

bool bsp_i2c1_read_mem_dma(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read_DMA(&hi2c1, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data) == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c1) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}
#endif
#endif

#if BSP_I2C2 == 1
bool bsp_i2c2_is_device_ready(uint8_t address_device)
{
    return (HAL_I2C_IsDeviceReady(&hi2c2, address_device, 5, BSP_I2C_TIMEOUT) == HAL_ERROR) ? false : true;
}

bool bsp_i2c2_is_busy(void)
{
    return (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_BUSY) ? false : true;
}

bool bsp_i2c2_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write(&hi2c2, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data,
                          BSP_I2C_TIMEOUT)
        == HAL_ERROR)
        return false;
    return true;
}

bool bsp_i2c2_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read(&hi2c2, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data, BSP_I2C_TIMEOUT)
        != HAL_OK)
        return false;
    return true;
}

#if BSP_I2C_DMA == 1
bool bsp_i2c2_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write_DMA(&hi2c2, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data)
        == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}

bool bsp_i2c2_read_mem_dma(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read_DMA(&hi2c2, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data) == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c2) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}
#endif
#endif

#if BSP_I2C3 == 1
bool bsp_i2c3_is_device_ready(uint8_t address_device)
{
    return (HAL_I2C_IsDeviceReady(&hi2c3, address_device, 5, BSP_I2C_TIMEOUT) == HAL_ERROR) ? false : true;
}

bool bsp_i2c3_is_busy(void)
{
    return (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_BUSY) ? false : true;
}

bool bsp_i2c3_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write(&hi2c3, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data,
                          BSP_I2C_TIMEOUT)
        == HAL_ERROR)
        return false;
    return true;
}

bool bsp_i2c3_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read(&hi2c3, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data, BSP_I2C_TIMEOUT)
        != HAL_OK)
        return false;
    return true;
}

#if BSP_I2C_DMA == 1
bool bsp_i2c3_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Write_DMA(&hi2c3, address_slave, reg_write, I2C_MEMADD_SIZE_8BIT, data_write, size_data)
        == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}

bool bsp_i2c3_read_mem_dma(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data)
{
    uint16_t timeout_dma = BSP_I2C_TIMEOUT_DMA;
    /* Get status bus I2C */
    if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_BUSY)
        return false;
    /* Transmit data */
    if (HAL_I2C_Mem_Read_DMA(&hi2c3, address_slave, reg_read, I2C_MEMADD_SIZE_8BIT, data_read, size_data) == HAL_ERROR)
        return false;
    /* Wait for the end of the transfer */
    while (timeout_dma > 0)
    {
        if (HAL_I2C_GetState(&hi2c3) == HAL_I2C_STATE_READY)
            break;
        timeout_dma--;
    }
    return timeout_dma > 0 ? true : false;
}
#endif
#endif

#if BSP_I2C_DMA == 1

#if BSP_I2C1 == 1
void i2c1_mem_dma_set_tx_cplt_callback(void *cb)
{
    i2c1_mem_dma_tx_cplt_callback = cb;
}
void i2c1_mem_dma_set_rx_cplt_callback(void *cb)
{
    i2c1_mem_dma_rx_cplt_callback = cb;
}
#endif
#if BSP_I2C2 == 1
void i2c2_mem_dma_set_tx_cplt_callback(void *cb)
{
    i2c2_mem_dma_tx_cplt_callback = cb;
}
void i2c2_mem_dma_set_rx_cplt_callback(void *cb)
{
    i2c2_mem_dma_rx_cplt_callback = cb;
}
#endif
#if BSP_I2C3 == 1
void i2c3_mem_dma_set_tx_cplt_callback(void *cb)
{
    i2c3_mem_dma_tx_cplt_callback = cb;
}
void i2c3_mem_dma_set_rx_cplt_callback(void *cb)
{
    i2c3_mem_dma_rx_cplt_callback = cb;
}
#endif

void HAL_I2C_MemTxCpltCallback(I2C_HandleTypeDef *hi2c)
{
#if BSP_I2C1 == 1
    if (hi2c->Instance == I2C1)
    {
        if (i2c1_mem_dma_tx_cplt_callback != NULL)
        {
            i2c1_mem_dma_tx_cplt_callback(hi2c);
        }
    }
#endif
#if BSP_I2C2 == 1
    if (hi2c->Instance == I2C2)
    {
        if (i2c2_mem_dma_tx_cplt_callback != NULL)
        {
            i2c2_mem_dma_tx_cplt_callback(hi2c);
        }
    }
#endif
#if BSP_I2C3 == 1
    if (hi2c->Instance == I2C3)
    {
        if (i2c3_mem_dma_tx_cplt_callback != NULL)
        {
            i2c3_mem_dma_tx_cplt_callback(hi2c);
        }
    }
#endif
}

void HAL_I2C_MemRxCpltCallback(I2C_HandleTypeDef *hi2c)
{
#if BSP_I2C1 == 1
    if (hi2c->Instance == I2C1)
    {
        if (i2c1_mem_dma_rx_cplt_callback != NULL)
        {
            i2c1_mem_dma_rx_cplt_callback(hi2c);
        }
    }
#endif
#if BSP_I2C2 == 1
    if (hi2c->Instance == I2C2)
    {
        if (i2c2_mem_dma_rx_cplt_callback != NULL)
        {
            i2c2_mem_dma_rx_cplt_callback(hi2c);
        }
    }
#endif
#if BSP_I2C3 == 1
    if (hi2c->Instance == I2C3)
    {
        if (i2c3_mem_dma_rx_cplt_callback != NULL)
        {
            i2c3_mem_dma_rx_cplt_callback(hi2c);
        }
    }
#endif
}

#endif

/* End of file -------------------------------------------------------- */
