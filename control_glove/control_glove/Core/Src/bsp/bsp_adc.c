/**
 * @file       bsp_adc.c
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-03
 * @author     Hieu Tran
 * @brief      ADC
 * @note       None
 */
/* Public includes ---------------------------------------------------------- */
#include "bsp_adc.h"

/* Private includes --------------------------------------------------------- */
/* Private defines ---------------------------------------------------------- */
/* Private enumerate/structure ---------------------------------------------- */
/* Private macros ----------------------------------------------------------- */
/* Public variables --------------------------------------------------------- */
extern ADC_HandleTypeDef hadc1;

/* Private variables -------------------------------------------------------- */
/* Private prototypes ------------------------------------------------------- */
/* Public implementations --------------------------------------------------- */
base_status_t bsp_adc_start(void)
{
    return BS_OK;
}

base_status_t bsp_adc_get_data(uint16_t* adc_value)
{
    HAL_ADC_Start(&hadc1);
    HAL_ADC_PollForConversion(&hadc1, 50);
    *adc_value = HAL_ADC_GetValue(&hadc1);
    return BS_OK;
}

/* Private implementations -------------------------------------------------- */


/* End of file -------------------------------------------------------------- */
