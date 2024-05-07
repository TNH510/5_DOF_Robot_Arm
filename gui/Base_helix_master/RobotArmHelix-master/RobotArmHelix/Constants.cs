using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Drawing;

namespace RobotArmHelix
{
    static class Constants
    {
        /* Robot Arm */
        public const double l1 = 690.0;
        public const double l2 = 440.0;
        public const double l3 = 500.0;
        public const double l4 = 0.0;
        public const double l5 = 230.0;

        public const double T1_LU = 90.0;
        public const double T1_LD = -35.0;

        public const double T2_LU = 110.0;
        public const double T2_LD = 45.0;
        public const double T3_LU = -80.0;
        public const double T3_LD = -120.0;
        public const double T4_LU = 5.0;
        public const double T4_LD = -105.0;
        public const double T5_LU = 179;
        public const double T5_LD = -180.0;

        public const double X_LU = 90.0;
        public const double X_LD = 45.0;
        public const double Y_LU = -80.0;
        public const double Y_LD = -120.0;
        public const double Z_LU = 5.0;
        public const double Z_LD = -105.0;
        //PLC
        public const string R_PLCREADY = "M512"; /*Address of reg Servo ON*/
        public const string R_BRAKE = "M513"; /*Address of reg BRAKE*/
        public const string R_GOHOME = "M514"; /*Address of reg Servo ON*/
        public const string R_ERR_RESET = "M515"; /*Address of reg BRAKE*/
        public const string R_SERVO_ON = "M516"; /*Address of reg Servo ON*/
        public const string R_SETHOME = "M1999"; /*Address of reg BRAKE*/
        public const string R_STATUS = "M8000"; /*Address of value 17bit contain SERVO, BRAKE, STATUS OF SERVO*/
        public const string R_PATH = "M529"; /*Address of value 17bit contain SERVO, BRAKE, STATUS OF SERVO*/

        public const string MOVEL_PATH1 = "700"; /*Address of value 16 for PLC move path1 completed */


        public const string R_JOGGINGFORWARD1 = "M550";
        public const string R_JOGGINGINVERSE1 = "M551";
        public const string R_JOGGINGFORWARD2 = "M552";
        public const string R_JOGGINGINVERSE2 = "M553";
        public const string R_JOGGINGFORWARD3 = "M554";
        public const string R_JOGGINGINVERSE3 = "M555";
        public const string R_JOGGINGFORWARD4 = "M556";
        public const string R_JOGGINGINVERSE4 = "M557";
        public const string R_JOGGINGFORWARD5 = "M558";
        public const string R_JOGGINGINVERSE5 = "M559";
        public const string R_VELOCITYJOGGING1_L = "D640";
        public const string R_VELOCITYJOGGING1_H = "D641";
        public const string R_VELOCITYJOGGING2_L = "D642";
        public const string R_VELOCITYJOGGING2_H = "D643";
        public const string R_VELOCITYJOGGING3_L = "D644";
        public const string R_VELOCITYJOGGING3_H = "D645";
        public const string R_VELOCITYJOGGING4_L = "D646";
        public const string R_VELOCITYJOGGING4_H = "D647";
        public const string R_VELOCITYJOGGING5_L = "D648";
        public const string R_VELOCITYJOGGING5_H = "D649";

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
        public static readonly System.Windows.Media.Color OBJECT_WHITE = System.Windows.Media.Color.FromRgb(255, 255, 255);
        public static readonly System.Windows.Media.Color OBJECT_GREEN = System.Windows.Media.Color.FromRgb(0, 255, 0);
        public static readonly System.Windows.Media.Color OBJECT_RED = System.Windows.Media.Color.FromRgb(255, 0, 0);
        public static readonly System.Windows.Media.Color OBJECT_YELLOW = System.Windows.Media.Color.FromRgb(255, 255, 0);
        public static readonly System.Windows.Media.Color OBJECT_MODIFIED = System.Windows.Media.Color.FromRgb(0, 126, 249);
        public static readonly System.Windows.Media.Color OBJECT_MODIFIED1 = System.Windows.Media.Color.FromRgb(24, 30, 54);




    }
}
