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
        private void Timer1_Tick(object sender, EventArgs e)
        {
            Forward_Kinematic();
        }
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

        public static (double, double, double, double, double) convert_position_angle(double x, double y, double z)
        {
            double t1, t2, t3, t4, t5, s2, c2, s3, c3, m, n;
            double roll, pitch;
            roll = 0.0;
            pitch = -Math.PI / 2;
            t1 = Math.Atan2(y, x);
            t5 = roll - t1;
            m = Math.Sqrt(x * x + y * y);
            n = z - Constants.l1 + Constants.l5;
            c3 = (m * m + n * n - Constants.l2 * Constants.l2 - Constants.l3 * Constants.l3) / (2 * Constants.l2 * Constants.l3);
            /* s3 has 2 value --> take the value of -sin */
            s3 = Math.Sqrt(1 - c3 * c3);
            t3 = Math.Atan2(s3, c3);
            if (t3 >= -Math.PI / 6 && t3 <= (4 * Math.PI) / 9)
            {
                /* Do nothing*/
            }
            else
            {
                s3 = -1 * Math.Sqrt(1 - c3 * c3);
                t3 = Math.Atan2(s3, c3);
            }
            /* Angle 3 */
            c2 = m * (Constants.l3 * c3 + Constants.l2) + n * (Constants.l3 * s3);
            s2 = n * (Constants.l3 * c3 + Constants.l2) - m * (Constants.l3 * s3);
            /* Angle 2 */
            t2 = Math.Atan2(s2, c2);
            /* Angle 4 */
            t4 = pitch - t2 - t3;
            t1 = t1 / Math.PI * 180.0;
            t2 = t2 / Math.PI * 180.0;
            t3 = t3 / Math.PI * 180.0;
            t4 = t4 / Math.PI * 180.0;
            t5 = t5 / Math.PI * 180.0;
            return (t1, t2, t3, t4, t5);
        }

        // Convert value read from PLC to int value
        public int Read_Position(int value_positon1, int value_positon2)
        {
            return (value_positon2 << 16 | value_positon1) - 18000000;
        }
        // Convert value read from PLC to int value
        public int[] Write_Theta(int value_angle)
        {
            int[] value_angle_arr = new int[2];
            value_angle_arr[0] = value_angle & 0xFFFF; //byte high for register
            value_angle_arr[1] = (value_angle >> 16) & 0xFFFF; // byte low for register
            return value_angle_arr;
        }
        public void Forward_Kinematic()
        {
            const int NUM_AFTER_COMMA = 5;
            int t1, t2, t3, t4, t5;
            int[] value_positon = new int[16];
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            double t1_dh, t2_dh, t3_dh, t4_dh, x, y, z;
            if (this.Connect_button.Enabled == false)
            {
                /* Read position of 5 angle */
                plc.ReadDeviceBlock(Constants.R_POSITION_1, 2, out value_positon[0]);
                plc.ReadDeviceBlock(Constants.R_POSITION_2, 2, out value_positon[2]);
                plc.ReadDeviceBlock(Constants.R_POSITION_3, 2, out value_positon[4]);
                plc.ReadDeviceBlock(Constants.R_POSITION_4, 2, out value_positon[6]);
                plc.ReadDeviceBlock(Constants.R_POSITION_5, 2, out value_positon[8]);

                // Read and convert driver angle value to real position value (was subtracted by 180)
                t1 = Read_Position(value_positon[0], value_positon[1]);
                t2 = Read_Position(value_positon[2], value_positon[3]);
                t3 = Read_Position(value_positon[4], value_positon[5]);
                t4 = Read_Position(value_positon[6], value_positon[7]);
                t5 = Read_Position(value_positon[8], value_positon[9]);

                // Convert theta read from int to double
                t1_out = double.Parse(Convert.ToString(t1)) / 100000.0;
                t2_out = double.Parse(Convert.ToString(t2)) / 100000.0;
                t3_out = double.Parse(Convert.ToString(t3)) / 100000.0;
                t4_out = double.Parse(Convert.ToString(t4)) / 100000.0;
                t5_out = double.Parse(Convert.ToString(t5)) / 100000.0;

                /* Transfer the angle value into float */
                t1_out = Math.Round(t1_out, NUM_AFTER_COMMA);
                t2_out = Math.Round(t2_out, NUM_AFTER_COMMA);
                t3_out = Math.Round(t3_out, NUM_AFTER_COMMA);
                t4_out = Math.Round(t4_out, NUM_AFTER_COMMA);
                t5_out = Math.Round(t5_out, NUM_AFTER_COMMA);

                // Convert the angle from degree to radian and define actual initial position
                t1_dh = t1_out / 180 * Math.PI;
                t2_dh = (t2_out + 90) / 180 * Math.PI;
                t3_dh = (t3_out - 90) / 180 * Math.PI;
                t4_dh = (t4_out - 90) / 180 * Math.PI;

                // Caculate Foward Kinematic
                x = Math.Cos(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
                y = Math.Sin(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
                z = Constants.l1 + Constants.l2 * Math.Sin(t2_dh) + Constants.l3 * Math.Sin(t2_dh + t3_dh) + Constants.l5 * Math.Sin(t2_dh + t3_dh + t4_dh);

                // Display position value into screen
                X_curpos.Text = Convert.ToString(Math.Round(x, NUM_AFTER_COMMA));
                Y_curpos.Text = Convert.ToString(Math.Round(y, NUM_AFTER_COMMA));
                Z_curpos.Text = Convert.ToString(Math.Round(z, NUM_AFTER_COMMA));
                Pitch_curpos.Text = Convert.ToString(Math.Round(t2_out + t3_out + t4_out, NUM_AFTER_COMMA));
                Roll_curpos.Text = Convert.ToString(Math.Round(t1_out + t5_out, NUM_AFTER_COMMA));

            }
            //await Task.Delay(1);
            //return;
        }
        public int Check_angle(double t1, double t2, double t3, double t4, double t5)
        {
            if ((t1 > Constants.T1_LU) || (t1 < Constants.T1_LD) || double.IsNaN(t1))
            {
                return 1;
            }
            if ((t2 > Constants.T2_LU) || (t2 < Constants.T2_LD) || double.IsNaN(t2))
            {
                return 2;
            }
            if ((t3 > Constants.T3_LU) || (t3 < Constants.T3_LD) || double.IsNaN(t3))
            {
                return 3;
            }
            if ((t4 > Constants.T4_LU) || (t4 < Constants.T4_LD) || double.IsNaN(t4))
            {
                return 4;
            }
            if ((t5 > Constants.T5_LU) || (t5 < Constants.T5_LD) || double.IsNaN(t5))
            {
                return 5;
            }
            return 0;
        }

        public void write_d_mem_32_bit(int device, int data)
        {
            string device_str = "";
            device_str = "D" + Convert.ToString(device);
            int[] temp = new int[2];
            temp[0] = data & 0xFFFF; //byte high for register
            temp[1] = (data >> 16) & 0xFFFF; // byte low for register
            /* Write the angle */
            plc.WriteDeviceBlock(device_str, 2, ref temp[0]);
        }
        public void turn_on_1_pulse_relay(int device)
        {
            string device_str = "M" + Convert.ToString(device);
            int status, ret;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            /* Turn on relay */
            /* Read status */
            ret = PLCReadbit(device_str, out status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            /* Reverse bit: status == 1 */
            ret = PLCWritebit(device_str, (~status) & 0x01);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Write PLC Fail");
                return;
            }
            PLCReadbit(device_str, out readbit);
            PrintLog("Info", device_str, Convert.ToString(readbit));

            /* Reverse it into the initial state */
            ret = PLCWritebit(device_str, (~(~status)) & 0x01);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Write PLC Fail");
                return;
            }
            PLCReadbit(device_str, out readbit);
            PrintLog("Info", device_str, Convert.ToString(readbit));
        }
        #endregion Functions

        #region Buttons

        private void Connect_button_Click(object sender, EventArgs e)
        {
            /* Timer start */
            Timer1.Start();
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
            /* Read the servo mode */
            int servo_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            /* Read status of Brake and AC Servo */
            ret = PLCReadbit(Constants.R_SERVO_ON, out servo_status);
            if (ret != 0)
            {
                PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            if (servo_status == 0) /* Servo is currently off */
            {
                OnServo_button.Text = "SERVO: OFF";
                ChangeColorObject(OnServo_button, Constants.OBJECT_RED);
                PrintLog("SERVO:", servo_status.ToString(), "OFF");
            }
            else
            {
                OnServo_button.Text = "SERVO: ON";
                ChangeColorObject(OnServo_button, Constants.OBJECT_GREEN);
                PrintLog("SERVO:", servo_status.ToString(), "ON");
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
        private void Run_button_Click(object sender, EventArgs e)
        {
            int ret, run_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            int[] value_angle = new int[10];
            /* Run */
            int temp_value_1 = (int)(Convert.ToDouble(t1_tb.Text) + 180) * 100000;
            int temp_value_2 = (int)(Convert.ToDouble(t2_tb.Text) + 180) * 100000;
            int temp_value_3 = (int)(Convert.ToDouble(t3_tb.Text) + 180) * 100000;
            int temp_value_4 = (int)(Convert.ToDouble(t4_tb.Text) + 180) * 100000;
            int temp_value_5 = (int)(Convert.ToDouble(t5_tb.Text) + 180) * 100000;

            value_angle[0] = temp_value_1 & 0xFFFF; //byte high for register
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
            /* Write the angle */
            plc.WriteDeviceBlock("D1010", 10, ref value_angle[0]);

            /* Turn on relay */
            turn_on_1_pulse_relay(528);
            ///* Read run_status */
            //ret = PLCReadbit(Constants.R_RUN, out run_status);
            //if (ret != 0)
            //{
            //    PrintLog("Error", getName, "Read PLC Fail");
            //    return;
            //}
            ///* Reverse bit: OnServo_status == 1 */
            //ret = PLCWritebit(Constants.R_RUN, (~run_status) & 0x01);
            //if (ret != 0)
            //{
            //    PrintLog("Error", getName, "Write PLC Fail");
            //    return;
            //}
            //PLCReadbit("M528", out readbit);
            //PrintLog("Info", "M528", Convert.ToString(readbit));

            ///* Reverse it into the initial state */
            //ret = PLCWritebit(Constants.R_RUN, (~(~run_status)) & 0x01);
            //if (ret != 0)
            //{
            //    PrintLog("Error", getName, "Write PLC Fail");
            //    return;
            //}
            //PLCReadbit("M528", out readbit);
            //PrintLog("Info", "M528", Convert.ToString(readbit));

        }
        private void Start_button_Click(object sender, EventArgs e)
        {
            const int NUM_AFTER_COMMA = 5;
            int t1, t2, t3, t4, t5;
            int[] value_positon = new int[16];
            double t1_out, t2_out, t3_out, t4_out, t5_out;
            double t1_dh, t2_dh, t3_dh, t4_dh, x, y, z;
            if (this.Connect_button.Enabled == false)
            {
                /* Read position of 5 angle */
                plc.ReadDeviceBlock(Constants.R_POSITION_1, 2, out value_positon[0]);
                plc.ReadDeviceBlock(Constants.R_POSITION_2, 2, out value_positon[2]);
                plc.ReadDeviceBlock(Constants.R_POSITION_3, 2, out value_positon[4]);
                plc.ReadDeviceBlock(Constants.R_POSITION_4, 2, out value_positon[6]);
                plc.ReadDeviceBlock(Constants.R_POSITION_5, 2, out value_positon[8]);

                // Read and convert driver angle value to real position value (was subtracted by 180)
                t1 = Read_Position(value_positon[0], value_positon[1]);
                t2 = Read_Position(value_positon[2], value_positon[3]);
                t3 = Read_Position(value_positon[4], value_positon[5]);
                t4 = Read_Position(value_positon[6], value_positon[7]);
                t5 = Read_Position(value_positon[8], value_positon[9]);

                // Convert theta read from int to double
                t1_out = double.Parse(Convert.ToString(t1)) / 100000.0;
                t2_out = double.Parse(Convert.ToString(t2)) / 100000.0;
                t3_out = double.Parse(Convert.ToString(t3)) / 100000.0;
                t4_out = double.Parse(Convert.ToString(t4)) / 100000.0;
                t5_out = double.Parse(Convert.ToString(t5)) / 100000.0;

                /* Transfer the angle value into float */
                t1_out = Math.Round(t1_out, NUM_AFTER_COMMA);
                t2_out = Math.Round(t2_out, NUM_AFTER_COMMA);
                t3_out = Math.Round(t3_out, NUM_AFTER_COMMA);
                t4_out = Math.Round(t4_out, NUM_AFTER_COMMA);
                t5_out = Math.Round(t5_out, NUM_AFTER_COMMA);

                // Convert the angle from degree to radian and define actual initial position
                t1_dh = t1_out / 180 * Math.PI;
                t2_dh = (t2_out + 90) / 180 * Math.PI;
                t3_dh = (t3_out - 90) / 180 * Math.PI;
                t4_dh = (t4_out - 90) / 180 * Math.PI;

                /* Test forward kinematic manually 
                t1_dh = Convert.ToDouble(t1_tb.Text) / 180 * Math.PI;
                t2_dh = (Convert.ToDouble(t2_tb.Text)) / 180 * Math.PI;
                t3_dh = (Convert.ToDouble(t3_tb.Text)) / 180 * Math.PI;
                t4_dh = (Convert.ToDouble(t4_tb.Text)) / 180 * Math.PI;
                */

                // Caculate Foward Kinematic
                x = Math.Cos(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
                y = Math.Sin(t1_dh) * (Constants.l2 * Math.Cos(t2_dh) + Constants.l3 * Math.Cos(t2_dh + t3_dh) + Constants.l5 * Math.Cos(t2_dh + t3_dh + t4_dh));
                z = Constants.l1 + Constants.l2 * Math.Sin(t2_dh) + Constants.l3 * Math.Sin(t2_dh + t3_dh) + Constants.l5 * Math.Sin(t2_dh + t3_dh + t4_dh);

                // Display position value into screen
                X_curpos.Text = Convert.ToString(Math.Round(x, NUM_AFTER_COMMA));
                Y_curpos.Text = Convert.ToString(Math.Round(y, NUM_AFTER_COMMA));
                Z_curpos.Text = Convert.ToString(Math.Round(z, NUM_AFTER_COMMA));
                Pitch_curpos.Text = Convert.ToString(Math.Round(t2_out + t3_out + t4_out, NUM_AFTER_COMMA));
                Roll_curpos.Text = Convert.ToString(Math.Round(t1_out + t5_out, NUM_AFTER_COMMA));
            }
        }

        private void Transmit_button_Click(object sender, EventArgs e)
        {
            double x, y, z, alpha, gamma;
            double velocity = 100.0; /* Assign directly the value of velocity */
            double t1, t2, t3, t4, t5, v1, v2, v3, v4, v5;
            double t1_current, t2_current, t3_current, t4_current, t5_current;
            double delta_theta1, delta_theta2, delta_theta3, delta_theta4, delta_theta5;
            double delta_theta_max = -1.0;
            int t1_out, t2_out, t3_out, t4_out, t5_out, v1_out, v2_out, v3_out, v4_out, v5_out;
            int[] arr = new int[100];
            int count = 0, ret;
            try
            {
                x = double.Parse(X_tb.Text);
                y = double.Parse(Y_tb.Text);
                z = double.Parse(Z_tb.Text);

                //alpha = double.Parse(Position_P.Text);
                //gamma = double.Parse(Position_G.Text);
                //velocity = double.Parse(Position_Time.Text);
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                t1_tb.Text = t1.ToString("0.####");
                t2_tb.Text = t2.ToString("0.####");
                t3_tb.Text = t3.ToString("0.####");
                t4_tb.Text = t4.ToString("0.####");
                t5_tb.Text = t5.ToString("0.####");
            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }
        private void pRu_button_Click(object sender, EventArgs e)
        {
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 4];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];

            /* Assign corrdination for each array */
            curr_pos[0] = Convert.ToDouble(X_curpos.Text);
            curr_pos[1] = Convert.ToDouble(Y_curpos.Text);
            curr_pos[2] = Convert.ToDouble(Z_curpos.Text);

            targ_pos[0] = Convert.ToDouble(X2_tb.Text);
            targ_pos[1] = Convert.ToDouble(Y2_tb.Text);
            targ_pos[2] = Convert.ToDouble(Z2_tb.Text);

            /* Referred vector */
            for (int i = 0; i < 3; i++)
            {
                vect_u[i] = targ_pos[i] - curr_pos[i];
                //PrintLog("vect", "value", Convert.ToString(vect_u[i]));
            }

            /* Linear Equation */
            for (int t = 0; t < 10; t++)
            {
                x = curr_pos[0] + vect_u[0] * t; /* 500 is the actual position of robot following the x axis */
                y = curr_pos[1] + vect_u[1] * t; /* 0 is the actual position of robot following the y axis */
                z = curr_pos[2] + vect_u[2] * t; /* 900 is the actual position of robot following the y axis */
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Assign value */
                angle_array[t, 0] = ((int)t1 + 180) * 100000;
                angle_array[t, 1] = ((int)t2 + 180) * 100000;
                angle_array[t, 2] = ((int)t3 + 180) * 100000;
                angle_array[t, 3] = ((int)t4 + 180) * 100000;
            }
            for (int j = 0; j < 10; j++)
            {

                value_angle[8 * j] = Write_Theta(angle_array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(angle_array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(angle_array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(angle_array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(angle_array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(angle_array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(angle_array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(angle_array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));

            }
            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);

        }
        private void Jog1F_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD1, 0);
            ChangeColorObject(Jog1F, Constants.OBJECT_WHITE);
        }
        private void Jog1F_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD1, 1);
            ChangeColorObject(Jog1F, Constants.OBJECT_GREEN);
        }

        private void Jog1B_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE1, 0);
            ChangeColorObject(Jog1B, Constants.OBJECT_WHITE);
        }

        private void Jog1B_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE1, 1);
            ChangeColorObject(Jog1B, Constants.OBJECT_GREEN);
        }
        private void Jog2F_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD2, 0);
            ChangeColorObject(Jog2F, Constants.OBJECT_WHITE);
        }

        private void Jog2F_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD2, 1);
            ChangeColorObject(Jog2F, Constants.OBJECT_GREEN);
        }
        private void Jog2B_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE2, 0);
            ChangeColorObject(Jog2B, Constants.OBJECT_WHITE);
        }

        private void Jog2B_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE2, 1);
            ChangeColorObject(Jog2B, Constants.OBJECT_GREEN);
        }

        private void Jog3F_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD3, 0);
            ChangeColorObject(Jog3F, Constants.OBJECT_WHITE);
        }

        private void Jog3F_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD3, 1);
            ChangeColorObject(Jog3F, Constants.OBJECT_GREEN);
        }

        private void Jog3B_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE3, 0);
            ChangeColorObject(Jog3B, Constants.OBJECT_WHITE);
        }

        private void Jog3B_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE3, 1);
            ChangeColorObject(Jog3B, Constants.OBJECT_GREEN);
        }

        private void Jog4F_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD4, 0);
            ChangeColorObject(Jog4F, Constants.OBJECT_WHITE);
        }

        private void Jog4F_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD4, 1);
            ChangeColorObject(Jog4F, Constants.OBJECT_GREEN);
        }

        private void Jog4B_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE4, 0);
            ChangeColorObject(Jog4B, Constants.OBJECT_WHITE);
        }

        private void Jog4B_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE4, 1);
            ChangeColorObject(Jog4B, Constants.OBJECT_GREEN);
        }

        private void Jog5F_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD5, 0);
            ChangeColorObject(Jog5F, Constants.OBJECT_WHITE);
        }

        private void Jog5F_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGFORWARD5, 1);
            ChangeColorObject(Jog5F, Constants.OBJECT_GREEN);
        }

        private void Jog5B_MouseUp(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE5, 0);
            ChangeColorObject(Jog5B, Constants.OBJECT_WHITE);
        }

        private void Jog5B_MouseDown(object sender, MouseEventArgs e)
        {
            PLCWritebit(Constants.R_JOGGINGINVERSE5, 1);
            ChangeColorObject(Jog5B, Constants.OBJECT_GREEN);
        }
        private void set_speed_btn_Click(object sender, EventArgs e)
        {
            int velocity;
            try
            {
                velocity = Convert.ToInt32(spd_tb.Text) * 1000;
                write_d_mem_32_bit(1008, velocity);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion


        private void test_button_Click(object sender, EventArgs e)
        {
            // write_d_mem_32_bit(2000, 170);
            turn_on_1_pulse_relay(444);

        }
        private void Speed_button_Click(object sender, EventArgs e)
        {
            int[] arr = new int[20];
            int count = 0, value;
            int[] velocity = new int[5];
            try
            {
                velocity[0] = Convert.ToInt32(setvel_1.Text) * 1000;
                velocity[1] = Convert.ToInt32(setvel_2.Text) * 1000;
                velocity[2] = Convert.ToInt32(setvel_3.Text) * 1000;
                velocity[3] = Convert.ToInt32(setvel_4.Text) * 1000;
                velocity[4] = Convert.ToInt32(setvel_5.Text) * 1000;
                for (int ind = 0; ind < 5; ind++)
                {
                    value = velocity[ind];
                    write_d_mem_32_bit(640 + 2 * ind, value);
                }
                // plc.WriteDeviceBlock(Constants.R_VELOCITYJOGGING1_L, count, ref arr[0]);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Test_run_Click(object sender, EventArgs e)
        {
            int ret, run_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            int[] value_angle = new int[20];

            /* Run */
            int temp_value_1 = (int)(Convert.ToDouble(t1_tb.Text) + 180) * 100000;
            int temp_value_2 = (int)(Convert.ToDouble(t2_tb.Text) + 180) * 100000;
            int temp_value_3 = (int)(Convert.ToDouble(t3_tb.Text) + 180) * 100000;
            int temp_value_4 = (int)(Convert.ToDouble(t4_tb.Text) + 180) * 100000;
            int temp_value_5 = (int)(Convert.ToDouble(t5_tb.Text) + 180) * 100000;

            value_angle[0] = temp_value_1 & 0xFFFF; //byte high for register
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
            /* Write the angle */
            plc.WriteDeviceBlock("D1010", 10, ref value_angle[0]);
        }

        private void Trasmit_1_Click(object sender, EventArgs e)
        {
            double x, y, z, alpha, gamma;
            double x2, y2, z2;
            double t1 = 0, t2 = 0, t3 = 0, t4 = 0, t5 = 0;
            double t12 = 0, t22 = 0, t32 = 0, t42 = 0, t52 = 0;
            double t1_current, t2_current, t3_current, t4_current, t5_current;
            double delta_theta1, delta_theta2, delta_theta3, delta_theta4, delta_theta5;
            double delta_theta_max = -1.0;
            int t1_out, t2_out, t3_out, t4_out, t5_out, v1_out, v2_out, v3_out, v4_out, v5_out;
            int[] arr = new int[100];
            int ret, ret2;
            int run_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            int readbit;
            int[] value_angle = new int[20];
            int[] value_angle_out = new int[20];

            try
            {
                x = double.Parse(X_tb.Text);
                y = double.Parse(Y_tb.Text);
                z = double.Parse(Z_tb.Text);

                x2 = double.Parse(X2_tb.Text);
                y2 = double.Parse(Y2_tb.Text);
                z2 = double.Parse(Z2_tb.Text);

                //alpha = double.Parse(Position_P.Text);
                //gamma = double.Parse(Position_G.Text);
                //velocity = double.Parse(Position_Time.Text);
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                (t12, t22, t32, t42, t52) = convert_position_angle(x2, y2, z2);
                ret = Check_angle(t1, t2, t3, t4, t5);

                ret2 = Check_angle(t12, t22, t32, t42, t52);
                if (ret2 != 0)
                {
                    double theta = 0.0;
                    if (ret2 == 1) theta = t12;
                    else if (ret2 == 2) theta = t22;
                    else if (ret2 == 3) theta = t32;
                    else if (ret2 == 4) theta = t42;
                    else if (ret2 == 5) theta = t52;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret2, theta));
                    return;
                }

                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;

                t22 -= 90.0;
                t32 += 90.0;
                t42 += 90.0;

            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }



            /* Run */
            int temp_value_1 = ((int)t1 + 180) * 100000;
            int temp_value_2 = (int)(t2 + 180) * 100000;
            int temp_value_3 = (int)(t3 + 180) * 100000;
            int temp_value_4 = (int)(t4 + 180) * 100000;
            int temp_value_5 = (int)(t5 + 180) * 100000;

            int temp_value_12 = (int)(t12 + 180) * 100000;
            int temp_value_22 = (int)(t22 + 180) * 100000;
            int temp_value_32 = (int)(t32 + 180) * 100000;
            int temp_value_42 = (int)(t42 + 180) * 100000;
            int temp_value_52 = (int)(t52 + 180) * 100000;

            value_angle[0] = temp_value_1 & 0xFFFF; //byte high for register
            value_angle[1] = (temp_value_1 >> 16) & 0xFFFF; // byte low for register
            //t1 = (t1 << 16) & 0xFFFF;

            value_angle[2] = temp_value_2 & 0xFFFF; //byte high for register
            value_angle[3] = (temp_value_2 >> 16) & 0xFFFF; // byte low for register

            value_angle[4] = temp_value_3 & 0xFFFF; //byte high for register
            value_angle[5] = (temp_value_3 >> 16) & 0xFFFF; // byte low for register

            value_angle[6] = temp_value_4 & 0xFFFF; //byte high for register
            value_angle[7] = (temp_value_4 >> 16) & 0xFFFF; // byte low for register

            //value_angle[8] = temp_value_5 & 0xFFFF; //byte high for register
            //value_angle[9] = (temp_value_5 >> 16) & 0xFFFF; // byte low for register

            value_angle[8] = temp_value_12 & 0xFFFF; //byte high for register
            value_angle[9] = (temp_value_12 >> 16) & 0xFFFF; // byte low for register
            //t1 = (t1 << 16) & 0xFFFF;

            value_angle[10] = temp_value_22 & 0xFFFF; //byte high for register
            value_angle[11] = (temp_value_22 >> 16) & 0xFFFF; // byte low for register

            value_angle[12] = temp_value_32 & 0xFFFF; //byte high for register
            value_angle[13] = (temp_value_32 >> 16) & 0xFFFF; // byte low for register

            value_angle[14] = temp_value_42 & 0xFFFF; //byte high for register
            value_angle[15] = (temp_value_42 >> 16) & 0xFFFF; // byte low for register

            //value_angle[18] = temp_value_52 & 0xFFFF; //byte high for register
            //value_angle[19] = (temp_value_52 >> 16) & 0xFFFF; // byte low for register

            /* Write the angle */
            plc.WriteDeviceBlock("D1010", 16, ref value_angle[0]);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Tsm_moveJ_btn_Click(object sender, EventArgs e)
        {
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            try
            {
                x = double.Parse(MvJx_tb.Text);
                y = double.Parse(MvJy_tb.Text);
                z = double.Parse(MvJz_tb.Text);

                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                t1_tb.Text = t1.ToString("0.####");
                t2_tb.Text = t2.ToString("0.####");
                t3_tb.Text = t3.ToString("0.####");
                t4_tb.Text = t4.ToString("0.####");
                t5_tb.Text = t5.ToString("0.####");

                int run_status;
                int[] value_angle = new int[10];
                /* Run */
                temp_value[0] = (int)(Convert.ToDouble(t1_tb.Text) + 180) * 100000;
                temp_value[1] = (int)(Convert.ToDouble(t2_tb.Text) + 180) * 100000;
                temp_value[2] = (int)(Convert.ToDouble(t3_tb.Text) + 180) * 100000;
                temp_value[3] = (int)(Convert.ToDouble(t4_tb.Text) + 180) * 100000;
                temp_value[4] = (int)(Convert.ToDouble(t5_tb.Text) + 180) * 100000;
                /* Write the angle */
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(1010 + 2 * ind, temp_value[ind]);
                }

            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }

        private void Tsm_moveL_btn_Click(object sender, EventArgs e)
        {
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];

            /* Assign corrdination for each array */
            curr_pos[0] = Convert.ToDouble(X_curpos.Text);
            curr_pos[1] = Convert.ToDouble(Y_curpos.Text);
            curr_pos[2] = Convert.ToDouble(Z_curpos.Text);

            targ_pos[0] = Convert.ToDouble(MvLx_tb.Text);
            targ_pos[1] = Convert.ToDouble(MvLy_tb.Text);
            targ_pos[2] = Convert.ToDouble(MvLz_tb.Text);

            /* Referred vector */
            for (int i = 0; i < 3; i++)
            {
                vect_u[i] = targ_pos[i] - curr_pos[i];
                //PrintLog("vect", "value", Convert.ToString(vect_u[i]));
            }

            /* Linear Equation */
            for (int t = 0; t < 10; t++)
            {
                x = curr_pos[0] + (vect_u[0] / 10) * (t + 1); /* 500 is the actual position of robot following the x axis */
                y = curr_pos[1] + (vect_u[1] / 10) * (t + 1); /* 0 is the actual position of robot following the y axis */
                z = curr_pos[2] + (vect_u[2] / 10) * (t + 1); /* 900 is the actual position of robot following the y axis */
                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Assign value */
                angle_array[t, 0] = (int)(t1 * 100000 + 18000000);
                angle_array[t, 1] = (int)(t2 * 100000 + 18000000);
                angle_array[t, 2] = (int)(t3 * 100000 + 18000000);
                angle_array[t, 3] = (int)(t4 * 100000 + 18000000);
                angle_array[t, 4] = (int)(t5 * 100000 + 18000000);
            }
            for (int j = 0; j < 10; j++)
            {
                value_angle[8 * j] = Write_Theta(angle_array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(angle_array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(angle_array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(angle_array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(angle_array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(angle_array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(angle_array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(angle_array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));

            }
            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);
            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
            //plc.WriteDeviceBlock("D2000", 20, ref value_angle_t5[0]);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            /* Turn on relay */
            turn_on_1_pulse_relay(530);
        }

        private void Tsm_moveC_btn_Click(object sender, EventArgs e)
        {
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];

            /* Assign corrdination for each array */
            curr_pos[0] = Convert.ToDouble(X_curpos.Text);
            curr_pos[1] = Convert.ToDouble(Y_curpos.Text);
            curr_pos[2] = Convert.ToDouble(Z_curpos.Text);

            targ_pos[0] = Convert.ToDouble(MvLx_tb.Text);
            targ_pos[1] = Convert.ToDouble(MvLy_tb.Text);
            targ_pos[2] = Convert.ToDouble(MvLz_tb.Text);

            /* Calculate circle parameters */
            double radius = Math.Sqrt(Math.Pow(vect_u[0], 2) + Math.Pow(vect_u[1], 2) + Math.Pow(vect_u[2], 2)) / 2.0;
            double centerX = (curr_pos[0] + targ_pos[0]) / 2.0;
            double centerY = (curr_pos[1] + targ_pos[1]) / 2.0;
            double verticalDistancePerTheta = (targ_pos[2] - curr_pos[2]) / (2.0 * Math.PI);

            /* Circular motion */
            for (int t = 0; t < 10; t++)
            {
                double theta = t * 2.0 * Math.PI / 10.0;
                x = radius * Math.Cos(theta) + centerX;
                y = radius * Math.Sin(theta) + centerY;
                z = verticalDistancePerTheta * theta + curr_pos[2];

                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Assign value */
                angle_array[t, 0] = (int)(t1 * 100000 + 18000000);
                angle_array[t, 1] = (int)(t2 * 100000 + 18000000);
                angle_array[t, 2] = (int)(t3 * 100000 + 18000000);
                angle_array[t, 3] = (int)(t4 * 100000 + 18000000);
                angle_array[t, 4] = (int)(t5 * 100000 + 18000000);
            }
            for (int j = 0; j < 10; j++)
            {
                value_angle[8 * j] = Write_Theta(angle_array[j, 0])[0];
                value_angle[8 * j + 1] = Write_Theta(angle_array[j, 0])[1];

                value_angle[8 * j + 2] = Write_Theta(angle_array[j, 1])[0];
                value_angle[8 * j + 3] = Write_Theta(angle_array[j, 1])[1];

                value_angle[8 * j + 4] = Write_Theta(angle_array[j, 2])[0];
                value_angle[8 * j + 5] = Write_Theta(angle_array[j, 2])[1];

                value_angle[8 * j + 6] = Write_Theta(angle_array[j, 3])[0];
                value_angle[8 * j + 7] = Write_Theta(angle_array[j, 3])[1];

                PrintLog("vect", "value:", Convert.ToString(value_angle[8 * j]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 1]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 2]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 3]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 4]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 5]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 6]));
                PrintLog("vect", "value", Convert.ToString(value_angle[8 * j + 7]));

            }
            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);
            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
            //plc.WriteDeviceBlock("D2000", 20, ref value_angle_t5[0]);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void Cvt_Trajectory_Click(object sender, EventArgs e)
        {
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            try
            {
                x = double.Parse(MvJx_tb.Text);
                y = double.Parse(MvJy_tb.Text);
                z = double.Parse(MvJz_tb.Text);

                (t1, t2, t3, t4, t5) = convert_position_angle(x, y, z);
                ret = Check_angle(t1, t2, t3, t4, t5);
                if (ret != 0)
                {
                    double theta = 0.0;
                    if (ret == 1) theta = t1;
                    else if (ret == 2) theta = t2;
                    else if (ret == 3) theta = t3;
                    else if (ret == 4) theta = t4;
                    else if (ret == 5) theta = t5;
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, string.Format("P2P: theta{0} = {1} out range", ret, theta));
                    return;
                }
                t2 -= 90.0;
                t3 += 90.0;
                t4 += 90.0;
                /* Calculate the desired speed */



                t1_tb.Text = t1.ToString("0.####");
                t2_tb.Text = t2.ToString("0.####");
                t3_tb.Text = t3.ToString("0.####");
                t4_tb.Text = t4.ToString("0.####");
                t5_tb.Text = t5.ToString("0.####");

                int run_status;
                int[] value_angle = new int[10];
                /* Run */
                temp_value[0] = (int)(Convert.ToDouble(t1_tb.Text) + 180) * 100000;
                temp_value[1] = (int)(Convert.ToDouble(t2_tb.Text) + 180) * 100000;
                temp_value[2] = (int)(Convert.ToDouble(t3_tb.Text) + 180) * 100000;
                temp_value[3] = (int)(Convert.ToDouble(t4_tb.Text) + 180) * 100000;
                temp_value[4] = (int)(Convert.ToDouble(t5_tb.Text) + 180) * 100000;
                /* Write the angle */
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(1010 + 2 * ind, temp_value[ind]);
                }

            }
            catch (Exception er)
            {
                PrintLog("Bug", MethodBase.GetCurrentMethod().Name, string.Format("Error: {0}", er));
            }
        }

        private void spd_tb_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_MATLAB_Click(object sender, EventArgs e)
        {
            MLApp.MLApp matlab = new MLApp.MLApp();

            matlab.Execute("disp('Hello from MATLAB');");

            Console.ReadLine();
        }
    }

}