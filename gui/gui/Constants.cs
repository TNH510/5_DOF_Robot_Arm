using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gui
{
    static class Constants
    {
        /* Robot Arm */
        public const double l1 = 690.0;
        public const double l2 = 440.0;
        public const double l3 = 500.0;
        public const double l4 = 0.0;
        public const double l5 = 230.0;

        public const double T1_LU = 110.0;
        public const double T1_LD = -95.0;
        public const double T2_LU = 110.0;
        public const double T2_LD = 30.0;
        public const double T3_LU = -30.0;
        public const double T3_LD = -120.0;
        public const double T4_LU = 5.0;
        public const double T4_LD = -105.0;
        public const double T5_LU = 95.0;
        public const double T5_LD = -110.0;
        //PLC
        public const string R_PLCREADY = "M512"; /*Address of reg Servo ON*/
        public const string R_BRAKE = "M513"; /*Address of reg BRAKE*/
        public const string R_GOHOME = "M514"; /*Address of reg Servo ON*/
        public const string R_ERR_RESET = "M515"; /*Address of reg BRAKE*/
        public const string R_SERVO_ON = "M516"; /*Address of reg Servo ON*/
        public const string R_SETHOME = "M1999"; /*Address of reg BRAKE*/
        public const string R_STATUS = "M8000"; /*Address of value 17bit contain SERVO, BRAKE, STATUS OF SERVO*/

        #region Angle
        public const string R_POSITION_1 = "D0"; /* Address of angle 1 */
        public const string R_POSITION_2 = "D20"; /* Address of angle 2 */
        public const string R_POSITION_3 = "D40"; /* Address of angle 3 */
        public const string R_POSITION_4 = "D60"; /* Address of angle 4 */
        public const string R_POSITION_5 = "D80"; /* Address of angle 5 */
        #endregion Angle

        public const string R_P2P_DATA = "D1000"; /*Address of value 17bit contain SERVO, BRAKE, STATUS OF SERVO*/
        public const string R_P2P_TRIGGER = "M517";
        public const string R_STOP_SERVO = "M4096";
        public const string R_MODBUS_ERROR1 = "M8018";
        public const string R_MODBUS_ERROR2 = "M8019";
        public const string R_SERVO_ERROR1 = "D6";
        public const string R_SERVO_ERROR2 = "D26";
        public const string R_SERVO_ERROR3 = "D46";
        public const string R_SERVO_ERROR4 = "D66";
        public const string R_SERVO_ERROR5 = "D86";
        public const string R_RUN = "M528";
        //Object
        public static readonly Color OBJECT_WHITE = Color.FromArgb(255, 255, 255);
        public static readonly Color OBJECT_GREEN = Color.FromArgb(0, 255, 0);
        public static readonly Color OBJECT_RED = Color.FromArgb(255, 0, 0);
        public static readonly Color OBJECT_YELLOW = Color.FromArgb(255, 255, 0);
    }
}
