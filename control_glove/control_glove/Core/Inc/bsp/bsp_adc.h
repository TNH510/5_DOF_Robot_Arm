/**
 * @file       bsp_adc.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-03
 * @author     Hieu Tran
 * @brief      ADC
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __BSP_ADC_H
#define __BSP_ADC_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include "main.h"
#include <stdint.h>
#include "bsp_common.h"

/* Public defines ----------------------------------------------------------- */ 
/* Public enumerate/structure ----------------------------------------------- */
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
base_status_t bsp_adc_start(void);
base_status_t bsp_adc_get_data(uint16_t* adc_value);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __BSP_ADC_H */

/* End of file -------------------------------------------------------------- */
