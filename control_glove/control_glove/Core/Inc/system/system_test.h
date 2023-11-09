/**
 * @file       sytem_test.h
 * @copyright  Copyright (C) 2023 TNH510
 * @version    1.0.0
 * @date       2023-11
 * @author     Hieu Tran Ngoc
 * @brief      Test project components
 * @note       None
 */
/* Define to prevent recursive inclusion ------------------------------------ */
#ifndef __SYSTEM_TEST_H
#define __SYSTEM_TEST_H

#ifdef __cplusplus
extern "C" {
#endif

/* Includes ----------------------------------------------------------------- */
#include <stdint.h>

/* Public defines ----------------------------------------------------------- */
/* Public enumerate/structure ----------------------------------------------- */
typedef enum
{
    SYSTEM_TEST_OK,
    SYSTEM_TEST_ERROR,
} system_test_error_t;
/* Public macros ------------------------------------------------------------ */
/* Public variables --------------------------------------------------------- */
/* Public APIs -------------------------------------------------------------- */
system_test_error_t system_test_init(void);
system_test_error_t system_test_general(void);

/* -------------------------------------------------------------------------- */
#ifdef __cplusplus
} /* extern "C" { */
#endif

#endif /* __SYSTEM_TEST_H */

/* End of file -------------------------------------------------------------- */
