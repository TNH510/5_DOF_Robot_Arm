/**
 * @file       bsp_i2c.h
 * @copyright  Copyright (C) 2019 Fiot Co., Ltd. All rights reserved.
 * @license    This project is released under the QuyLe License.
 * @version    1.0.0
 * @date       2023-08-19
 * @author     quyle-itr-intern
 *
 * @brief      handle i2c
 *
 * @note
 */

/* Define to prevent recursive inclusion ------------------------------ */
#ifndef __BSP_I2C_H
#define __BSP_I2C_H

/* Includes ----------------------------------------------------------- */
#include "main.h"

#include <stdbool.h>
#include <stdint.h>
#include <stdio.h>
#include "bsp_common.h"
/* Public defines ----------------------------------------------------- */
#define BSP_I2C1    1
#define BSP_I2C2    1
#define BSP_I2C_DMA 1
/* Public enumerate/structure ----------------------------------------- */

/* Public macros ------------------------------------------------------ */

/* Public variables --------------------------------------------------- */

/* Public function prototypes ----------------------------------------- */

#ifdef BSP_I2C1
void bsp_i2c1_deinit(void);
void bsp_i2c1_init(void);

/**
 * @brief Check I2C busy or not
 *
 * @return true  I2C is not busy
 * @return false I2C is busy
 */
bool bsp_i2c1_is_busy(void);

/**
 * @brief Check device is ready or not
 * 
 * @param address_device    Address of device
 * @return true             Device is ready
 * @return false            Device is not ready
 */
bool bsp_i2c1_is_device_ready(uint8_t address_device);

/**
 * @brief Write memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c1_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Write memory in dma mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c1_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Read memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_read      Register of device
 * @param data_read     Data want to write
 * @param size_data     Size data want to write
 * @return true         Read success 
 * @return false        Read fail
 */
bool bsp_i2c1_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data);
#endif

#ifdef BSP_I2C2
/**
 * @brief Check I2C busy or not
 *
 * @return true  I2C is not busy
 * @return false I2C is busy
 */
bool bsp_i2c2_is_busy(void);

/**
 * @brief Check device is ready or not
 * 
 * @param address_device    Address of device
 * @return true             Device is ready
 * @return false            Device is not ready
 */
bool bsp_i2c2_is_device_ready(uint8_t address_device);

/**
 * @brief Write memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c2_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Write memory in dma mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c2_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Read memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_read      Register of device
 * @param data_read     Data want to write
 * @param size_data     Size data want to write
 * @return true         Read success 
 * @return false        Read fail
 */
bool bsp_i2c2_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data);
#endif

#ifdef BSP_I2C3

/**
 * @brief Check I2C busy or not
 *
 * @return true  I2C is not busy
 * @return false I2C is busy
 */
bool bsp_i2c3_is_busy(void);

/**
 * @brief Check device is ready or not
 * 
 * @param address_device    Address of device
 * @return true             Device is ready
 * @return false            Device is not ready
 */
bool bsp_i2c3_is_device_ready(uint8_t address_device);

/**
 * @brief Write memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c3_write_mem(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Write memory in dma mode
 * 
 * @param address_slave Address of device
 * @param reg_write     Register of device
 * @param data_write    Data want to write
 * @param size_data     Size data want to write
 * @return true         Write success 
 * @return false        Write fail
 */
bool bsp_i2c3_write_mem_dma(uint8_t address_slave, uint8_t reg_write, uint8_t *data_write, uint16_t size_data);

/**
 * @brief Read memory in polling mode
 * 
 * @param address_slave Address of device
 * @param reg_read      Register of device
 * @param data_read     Data want to write
 * @param size_data     Size data want to write
 * @return true         Read success 
 * @return false        Read fail
 */
bool bsp_i2c3_read_mem(uint8_t address_slave, uint8_t reg_read, uint8_t *data_read, uint16_t size_data);
#endif

/**
 * @brief Set callback function 
 * 
 * @param cb Function callback
 */
void i2c1_mem_dma_set_cplt_callback(void *cb);

#endif  // __BSP_I2C_H

/* End of file -------------------------------------------------------- */
