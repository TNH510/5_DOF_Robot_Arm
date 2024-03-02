/**
 * @file       drv_acc.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-02
 * @author     Hieu Tran
 * @brief      Driver for ACC module
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "drv_acc.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
extern I2C_HandleTypeDef hi2c1;

/* Private variables -------------------------------------------------------- */
static SD_MPU6050 g_acc;

/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t drv_acc_init(void)
{
	SD_MPU6050_Result result;
	result = SD_MPU6050_Init(&hi2c1,&g_acc,SD_MPU6050_Device_0,SD_MPU6050_Accelerometer_2G,SD_MPU6050_DataRate_250Hz);
    if(result == SD_MPU6050_Result_Ok)
    {
        return BS_OK;
    }
    else
    {
        return BS_ERROR;
    }
}

base_status_t drv_acc_get_data(drv_acc_data_t *acc_data)
{
    SD_MPU6050_ReadTemperature(&hi2c1,&g_acc);
    SD_MPU6050_ReadGyroscope(&hi2c1,&g_acc);
    SD_MPU6050_ReadAccelerometer(&hi2c1,&g_acc);

    acc_data->acc_x = g_acc.Accelerometer_X;
    acc_data->acc_y = g_acc.Accelerometer_Y;
    acc_data->acc_z = g_acc.Accelerometer_Z;
    acc_data->gy_x  = g_acc.Gyroscope_X;
    acc_data->gy_y  = g_acc.Gyroscope_Y;
    acc_data->gy_z  = g_acc.Gyroscope_Z;
    acc_data->temp  = g_acc.Temperature;
    
	return BS_OK;
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
