/**
 * @file       drv_magnetic.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-02
 * @author     Hieu Tran
 * @brief      Driver for magetic module
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "drv_magnetic.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t drv_magnetic_init(void)
{
    HMC5883L_setRange(HMC5883L_RANGE_1_3GA);
	HMC5883L_setMeasurementMode(HMC5883L_CONTINOUS);
	HMC5883L_setDataRate(HMC5883L_DATARATE_75HZ);
	HMC5883L_setSamples(HMC5883L_SAMPLES_1);
	HMC5883L_setOffset(0, 0);

    return BS_OK;
}

base_status_t drv_magnetic_get_data(drv_magnetic_data_t *magnetic_data)
{
    Vector mag = HMC5883L_readNormalize();
    magnetic_data->XAxis = mag.XAxis;
    magnetic_data->YAxis = mag.YAxis;
    magnetic_data->ZAxis = mag.ZAxis;

    return BS_OK;
}


/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
