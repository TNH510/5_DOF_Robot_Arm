using ActUtlTypeLib; /* The utility setting type control which is used to create a user
                        program using Communication Setup Utility. */
using System.Reflection;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Diagnostics;
using System.Collections;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using static System.Net.Mime.MediaTypeNames;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.ObjectModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;
using static System.Net.WebRequestMethods;
using System.Data.SqlTypes;
using System.Security.AccessControl;
using System.Diagnostics.Metrics;
using System.Security.Cryptography.X509Certificates;

namespace gui
{
    public partial class Form1 : Form
    {
        /*  Running a C++ COM DLL from a Win Forms C# project on a 64-bit OS -> Change the Win Forms C# project to be x86, re-ran, and it worked. To do this:
            - Right-click the .NET project, and choose Properties
            - Select the Build tab
            - Set Platform target to x86
            - Recompile, and re-run
        */
        public ActUtlType plc = new();
        int run_test = 0;
        public Form1()
        {
            InitializeComponent();
            gui.Form1.CheckForIllegalCrossThreadCalls = false;

        }
        #region Functions
        public void ChangeColorObject(object objectin, Color color_object)
        {
            var button = objectin as Button;
            if (button != null)
            {
                button.BackColor = color_object;
                return;
            }

            var textbox = objectin as TextBox;
            if (textbox != null)
            {
                textbox.BackColor = color_object;
                return;
            }
        }
        public void PrintLog(string level, string namefunction, string msg)
        {
            DateTime time = DateTime.Now;
            ErrorLog.AppendText(time.ToString("h:mm:ss") + " - " + level + " - " + namefunction + ": " + msg);
            ErrorLog.AppendText(Environment.NewLine);
        }
        public int PLCReadbit(string adr, out int receive)
        {
            return plc.GetDevice(adr, out receive);
        }
        public int PLCWritebit(string adr, int value)
        {
            return plc.SetDevice(adr, value);
        }
        private void Press_button(string name, string adr)
        {
            int ret;
            ret = PLCWritebit(adr, 1);
            if (ret != 0)
            {
                PrintLog("Error", name, string.Format("Set {0} = 1 Failed", adr));
                return;
            }
            Thread.Sleep(1);
            ret = PLCWritebit(adr, 0);
            if (ret != 0)
            {
                PrintLog("Error", name, string.Format("Set {0} = 0 Failed", adr));
                return;
            }
            PrintLog("Info", name, string.Format("Raise {0} Successfully", adr));
        }

        public static (double, double, double, double, double) convert_position_angle(double x, double y, double z, double alpha, double gamma)
        {
            double t1, t2, t3, t4, t5, s2, c2, s3, c3, m, n;
            alpha = 0.0;
            gamma = -Math.PI / 2;
            t1 = Math.Atan2(y, x);
            t5 = alpha - t1;
            m = Math.Sqrt(x * x + y * y) - Constants.l4 * Math.Cos(gamma);
            n = z - Constants.l1 - Constants.l4 * Math.Sin(gamma);
            c3 = (m * m + n * n - Constants.l2 * Constants.l2 - Constants.l3 * Constants.l3) / (2 * Constants.l2 * Constants.l3);
            s3 = -1 * Math.Sqrt(1 - c3 * c3);
            t3 = Math.Atan2(s3, c3);
            c2 = m * (Constants.l3 * c3 + Constants.l2) + n * (Constants.l3 * s3);
            s2 = n * (Constants.l3 * c3 + Constants.l2) - m * (Constants.l3 * s3);
            t2 = Math.Atan2(s2, c2);
            t4 = gamma - t2 - t3;
            t1 = t1 / Math.PI * 180.0;
            t2 = t2 / Math.PI * 180.0;
            t3 = t3 / Math.PI * 180.0;
            t4 = t4 / Math.PI * 180.0;
            t5 = t5 / Math.PI * 180.0;
            return (t1, t2, t3, t4, t5);

        }
        public int Read_Position(int value_positon1, int value_positon2)
        {
            return (value_positon2 << 16 | value_positon1) - 18000000;
        }
        public async Task Forward_Kinematic()
        {
            int diginumber_display = 5;
            int t1, t2, t3, t4, t5;
            int[] value_positon = new int[16];
            int[] adc_raw = new int[4];
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            double t1_dh, t2_dh, t3_dh, t4_dh, x, y, z;
            if (this.Connect_button.Enabled == false)
            {
                plc.ReadDeviceBlock(Constants.R_POSITION, 12, out value_positon[0]);
                plc.ReadDeviceBlock("D8021", 4, out adc_raw[0]);

                t1 = Read_Position(value_positon[0], value_positon[1]);
                t2 = Read_Position(value_positon[2], value_positon[3]);
                t3 = Read_Position(value_positon[4], value_positon[5]);
                t4 = Read_Position(value_positon[6], value_positon[7]);
                t5 = Read_Position(value_positon[8], value_positon[9]);
                // MCode.Text = Convert.ToString(value_positon[10]);

                t1_out = double.Parse(Convert.ToString(t1)) / 100000;
                t2_out = double.Parse(Convert.ToString(t2)) / 100000;
                t3_out = double.Parse(Convert.ToString(t3)) / 100000;
                t4_out = double.Parse(Convert.ToString(t4)) / 100000;
                t5_out = double.Parse(Convert.ToString(t5)) / 100000;
                /* Transfer the angle value into float */
                t1_out = Math.Round(t1_out, diginumber_display);
                t2_out = Math.Round(t2_out, diginumber_display);
                t3_out = Math.Round(t3_out, diginumber_display);
                t4_out = Math.Round(t4_out, diginumber_display);
                t5_out = Math.Round(t5_out, diginumber_display);
                /* Convert the angle from degree to radian */
                t1_dh = t1_out / 180 * Math.PI;
                t2_dh = (t2_out + 90) / 180 * Math.PI;
                t3_dh = (t3_out - 90) / 180 * Math.PI;
                t4_dh = (t4_out - 90) / 180 * Math.PI;
                x = Math.Cos(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l4 * Math.Cos(t2_dh + t3_dh + t4_dh));
                y = Math.Sin(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l4 * Math.Cos(t2_dh + t3_dh + t4_dh));
                z = Constants.l1 + Constants.l2 * Math.Sin(t2_dh) + Constants.l3 * Math.Sin(t2_dh + t3_dh) + Constants.l4 * Math.Sin(t2_dh + t3_dh + t4_dh);

                X_tb.Text = Convert.ToString(Math.Round(x, diginumber_display));
                Y_tb.Text = Convert.ToString(Math.Round(y, diginumber_display));
                Z_tb.Text = Convert.ToString(Math.Round(z, diginumber_display));
                Pitch_tb.Text = Convert.ToString(Math.Round(t2_out + t3_out + t4_out, diginumber_display));
                Roll_tb.Text = Convert.ToString(Math.Round(t1_out + t5_out, diginumber_display));

                //ADC2.Text = Convert.ToString(0.01336 * (float)(adc_raw[0]) - 116.22360);
                //ADC3.Text = Convert.ToString(0.00894 * (float)(adc_raw[1]) - 53.82361);
                //ADC4.Text = Convert.ToString(0.02212 * (float)(adc_raw[2]) - 84.06421);
                //ADC5.Text = Convert.ToString(0.03095 * (float)(adc_raw[3]) - 133.03176);
            }
            await Task.Delay(1);
            return;
        }
        #endregion Functions

        #region Buttons
        private void Connect_button_Click(object sender, EventArgs e)
        {
            /* Declare the variable(s) */
            int ret;
            /* A logical station number set in Communication Setup Utility - Datasheet - Page 61 */
            plc.ActLogicalStationNumber = 1;
            /* Open the connection between PLC and C# - Datasheet - Page 381 */
            ret = plc.Open();
            /* Return value
               Normal termination : 0 is returned.
               Abnormal termination: Any value other than 0 is returned
            */
            if (ret == 0)
            {
                /* Change the color of the button when clicked */
                ChangeColorObject(Connect_button, Constants.OBJECT_GREEN);
                ChangeColorObject(Disconnect_button, Constants.OBJECT_WHITE);
                Connect_button.Enabled = false;
                Disconnect_button.Enabled = true;
                /* 
                    Print the log command
                    MethosBase.GetCurrentMethod returns the action user did.
                */
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Connect PLC successfully");
            }
            else
            {
                PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Connect PLC unsuccessfully");
            }

        }

        private void Disconnect_button_Click(object sender, EventArgs e)
        {
            /* Close the connection between PLC and C# - Datasheet - Page 383 */
            plc.Close();
            /* Change the color of the button when clicked */
            ChangeColorObject(Connect_button, Constants.OBJECT_WHITE);
            ChangeColorObject(Disconnect_button, Constants.OBJECT_RED);
            Disconnect_button.Enabled = false;
            Connect_button.Enabled = true;
            /* 
                Print the log command
                MethosBase.GetCurrentMethod returns the action user did.
            */
            PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Disonnect PLC successfully");
        }
        private void OnServo_button_Click(object sender, EventArgs e)
        {
            int ret, servo_status;
            string getName = MethodBase.GetCurrentMethod().Name;

            /* Read status of Brake and AC Servo */
            ret = PLCReadbit(Constants.R_SERVO_ON, out servo_status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            if (servo_status == 0) // Servo is currently OFF
            {
                /* Reverse bit: OnServo_status == 1 */
                OnServo_button.Text = "SERVO: ON";
                ChangeColorObject(OnServo_button, Constants.OBJECT_GREEN);
                PrintLog("SERVO:", servo_status.ToString(), "ON");
                ret = PLCWritebit(Constants.R_SERVO_ON, (~servo_status) & 0x01);
                if (ret != 0)
                {
                    PrintLog("Error", getName, "Write PLC Fail");
                    return;
                }
            }
            else
            {
                /* Reverse bit: OnServo_status == 1 */
                OnServo_button.Text = "SERVO: OFF";
                ChangeColorObject(OnServo_button, Constants.OBJECT_RED);
                PrintLog("SERVO:", servo_status.ToString(), "OFF");
                ret = PLCWritebit(Constants.R_SERVO_ON, (~servo_status) & 0x01);
                if (ret != 0)
                {
                    PrintLog("Error", getName, "Write PLC Fail");
                    return;
                }
            }
        }
        private void ResetError_button_Click(object sender, EventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_ERR_RESET);
        }
        private void GoHome_button_Click(object sender, EventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_GOHOME);
        }
        private void SetHome_button_Click(object sender, EventArgs e)
        {
            Press_button(MethodBase.GetCurrentMethod().Name, Constants.R_SETHOME);
        }
        private void Exe_button_Click(object sender, EventArgs e)
        {
            int[] value_angle = new int[10];
            int[] value_angle_out = new int[10];
            int temp_value_1 = Convert.ToInt16(t1_tb.Text) * 100000;
            int temp_value_2 = Convert.ToInt16(t2_tb.Text) * 100000;
            int temp_value_3 = Convert.ToInt16(t3_tb.Text) * 100000;
            int temp_value_4 = Convert.ToInt16(t4_tb.Text) * 100000;
            int temp_value_5 = Convert.ToInt16(t5_tb.Text) * 100000;

            value_angle[0] =  temp_value_1 & 0xFFFF; //byte high for register
            value_angle[1] = (temp_value_1 >> 16) & 0xFFFF; // byte low for register
            //t1 = (t1 << 16) & 0xFFFF;

            value_angle[2] = temp_value_2 & 0xFFFF; //byte high for register
            value_angle[3] = (temp_value_2 >> 16) & 0xFFFF; // byte low for register

            value_angle[4] = temp_value_3 & 0xFFFF; //byte high for register
            value_angle[5] = (temp_value_3 >> 16) & 0xFFFF; // byte low for register

            value_angle[6] = temp_value_4 & 0xFFFF; //byte high for register
            value_angle[7] = (temp_value_4 >> 16) & 0xFFFF; // byte low for register

            value_angle[8] = temp_value_5 & 0xFFFF; //byte high for register
            value_angle[9] = (temp_value_5 >> 16) & 0xFFFF; // byte low for register

            plc.WriteDeviceBlock("D1010", 2, ref value_angle[0]);
            plc.WriteDeviceBlock("D1012", 2, ref value_angle[2]);
            plc.WriteDeviceBlock("D1014", 2, ref value_angle[4]);
            plc.WriteDeviceBlock("D1016", 2, ref value_angle[6]);
            plc.WriteDeviceBlock("D1018", 2, ref value_angle[8]);

            plc.ReadDeviceBlock("D1010", 2, out value_angle_out[0]);
            plc.ReadDeviceBlock("D1012", 2, out value_angle_out[2]);
            plc.ReadDeviceBlock("D1014", 2, out value_angle_out[4]);
            plc.ReadDeviceBlock("D1016", 2, out value_angle_out[6]);
            plc.ReadDeviceBlock("D1018", 2, out value_angle_out[8]);

            //PrintLog("Info", "t1", Convert.ToString(t1_out));
            //PrintLog("Info", "t2", Convert.ToString(t2_out));
            //PrintLog("Info", "t3", Convert.ToString(t3_out));
            //PrintLog("Info", "t4", Convert.ToString(t4_out));
            //PrintLog("Info", "t4", Convert.ToString(t5_out));
        }
        private void Run_button_Click(object sender, EventArgs e)
        {

            int ret, run_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            /* Read run_status */
            ret = PLCReadbit(Constants.R_RUN, out run_status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            /* Reverse bit: OnServo_status == 1 */
            ret = PLCWritebit(Constants.R_RUN, (~run_status) & 0x01);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Write PLC Fail");
                return;
            }
            PLCReadbit("M528", out readbit);
            PrintLog("Info", "M528", Convert.ToString(readbit));
        }
        #endregion

    }

}