/**
 * @file       bsp_qmc5883l.h
 * @copyright  Copyright (C) 2024 TNH510
 * @version    1.0.0
 * @date       03-2024
 * @author     Hieu Tran Ngoc
 * @brief      Board support package for MARG sensor 
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __BSP_QMC5883L_H
#define __BSP_QMC5883L_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>
#include "bsp_common.h"

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
base_status_t bsp_qmc5883l_init(void);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __BSP_QMC5883L  _H */

/* End of file -------------------------------------------------------------- */
