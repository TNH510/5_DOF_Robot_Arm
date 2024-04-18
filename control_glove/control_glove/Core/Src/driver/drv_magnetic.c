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
static float A[3][3] = {{ 87.423326f, -2.273900f, -0.483181f}, 
                        {-2.273900f, 91.119974f, -1.580122f}, 
                        {-0.483181f, -1.580122f, 105.946092f}};

static float b[3] = {38.294125f, -167.486285f, -9.709824f};
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

    float mag_temp[3] = {0.0f};
    float mag_calib[3] = {0.0f};
    
    mag_temp[0] = mag.XAxis;
    mag_temp[1] = mag.YAxis;
    mag_temp[2] = mag.ZAxis;

    for (int i = 0; i < 3; i++)
    {
        mag_temp[i] = mag_temp[i] - b[i];
    }

    for (int j = 0; j < 3; j++)
    {
        float temp = 0.0f;
        for (int k = 0; k < 3; k++)
        {
            temp += A[j][k] * mag_temp[k];
        }

        mag_calib[j] = temp; 
    }

    magnetic_data->XAxis = mag_calib[0];
    magnetic_data->YAxis = mag_calib[1];
    magnetic_data->ZAxis = mag_calib[2];

    // magnetic_data->XAxis = mag.XAxis;
    // magnetic_data->YAxis = mag.YAxis;
    // magnetic_data->ZAxis = mag.ZAxis;

    return BS_OK;
}


/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
