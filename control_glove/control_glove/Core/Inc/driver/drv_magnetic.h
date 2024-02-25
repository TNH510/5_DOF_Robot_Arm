/**
 * @file       drv_magnetic.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       2024-02
 * @author     Hieu Tran
 * @brief      Driver for magnetic module
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __DRV_MAGNETIC_H
#define __DRV_MAGNETIC_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>
#include "bsp_hmc5883l.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef struct 
{
    float XAxis;
    float YAxis;
    float ZAxis;    
} drv_magnetic_data_t;

/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
base_status_t drv_magnetic_init(void);
base_status_t drv_magnetic_get_data(drv_magnetic_data_t *magnetic_data);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __DRV_MAGNETIC_H */

/* End of file -------------------------------------------------------------- */
