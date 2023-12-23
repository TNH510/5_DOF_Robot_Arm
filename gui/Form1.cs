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
using System.Security.Cryptography.X509Certificates;
using System.IO.Ports;
using System.Data;
using System.Linq;

namespace GUI
{
    public partial class Form1 : Form
    {


        public ActUtlType plc = new();
        int run_test = 0;
        string dataOUT;
        int move = 0; /* move = 1 -> MoveJ, move = 2 -> MoveL */ 
        UInt16 t1_mat, t2_mat,t3_mat,t4_mat,t5_mat;
        byte[] frame_prepare_to_send = new byte[10];
        byte[] frame_off_matlab = new byte[10];
        byte[] frame_stop_record = new byte[10];
        byte[] frame_start_record = new byte[10];


        string currentDirectory;
        string TargetDirectory;
        MLApp.MLApp matlab = new MLApp.MLApp();
        public Form1()
        {
            InitializeComponent();
            bt_off_matlab.Enabled = false;
            bt_stop_record.Enabled = false;
            bt_start_record.Enabled = false;
            bt_start_timer.Enabled = true;
            bt_stop_timer.Enabled = false;

            GUI.Form1.CheckForIllegalCrossThreadCalls = false;
            string[] ports = SerialPort.GetPortNames(); //Gan gia tri cac port co trong may
            cBoxPort.Items.AddRange(ports); //Hien thi len cBoxPort

            // Get the directory of the currently executing assembly
            currentDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // String to remove
            string substringToRemove = "gui\\bin\\Debug\\";

            // Remove the specified substring from the end of the path
            if (currentDirectory.EndsWith(substringToRemove, StringComparison.OrdinalIgnoreCase))
            {
                currentDirectory = currentDirectory.Substring(0, currentDirectory.Length - substringToRemove.Length);

                // Optionally, trim trailing directory separator
                currentDirectory = currentDirectory.TrimEnd('\\');

                TargetDirectory = currentDirectory + "\\matlab\\guide_simulink";

                Console.WriteLine("Modified Path: " + TargetDirectory);
            }
            else
            {
                Console.WriteLine("The specified substring is not at the end of the path.");
            }
        }

        private void Connect_btn_Click(object sender, EventArgs e)
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
        #region Functions


        private void Timer1_Tick(object sender, EventArgs e)
        {
            Forward_Kinematic();
            send_data();
        }
        private void send_data()
        {
            if (serialPort1.IsOpen)
            {
                frame_prepare_to_send[0] = (byte) ((t1_mat >> 8) & 0xFF);
                frame_prepare_to_send[1] = (byte) (t1_mat & 0xFF);

                frame_prepare_to_send[2] = (byte) ((t2_mat >> 8) & 0xFF);
                frame_prepare_to_send[3] = (byte) (t2_mat & 0xFF);

                frame_prepare_to_send[4] = (byte) ((t3_mat >> 8) & 0xFF);
                frame_prepare_to_send[5] = (byte)(t3_mat & 0xFF);

                frame_prepare_to_send[6] = (byte)((t4_mat >> 8) & 0xFF);
                frame_prepare_to_send[7] = (byte)(t4_mat & 0xFF);

                frame_prepare_to_send[8] = (byte)((t5_mat >> 8) & 0xFF);
                frame_prepare_to_send[9] = (byte)(t5_mat & 0xFF);

                serialPort1.Write(frame_prepare_to_send, 0, frame_prepare_to_send.Length);
            }
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
        public uint Read_Position(uint value_positon1, uint value_positon2)
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
            uint t1 = 0 , t2 = 0, t3 = 0, t4 = 0, t5 = 0;
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
                t1 = Read_Position((uint)value_positon[0], (uint)value_positon[1]);
                t2 = Read_Position((uint)value_positon[2], (uint)value_positon[3]);
                t3 = Read_Position((uint)value_positon[4], (uint)value_positon[5]);
                t4 = Read_Position((uint)value_positon[6], (uint)value_positon[7]);
                t5 = Read_Position((uint)value_positon[8], (uint)value_positon[9]);

                t1_mat = Convert.ToUInt16((t1 + 18000000) / 1000);
                t2_mat = Convert.ToUInt16((t2 + 18000000) / 1000);
                t3_mat = Convert.ToUInt16((t3 + 18000000) / 1000);
                t4_mat = Convert.ToUInt16((t4 + 18000000) / 1000);
                t5_mat = Convert.ToUInt16((t5 + 18000000) / 1000);

                // Convert theta read from int to double
                t1_out = double.Parse(Convert.ToString((int) t1)) / 100000.0;
                t2_out = double.Parse(Convert.ToString((int) t2)) / 100000.0;
                t3_out = double.Parse(Convert.ToString((int) t3)) / 100000.0;
                t4_out = double.Parse(Convert.ToString((int) t4)) / 100000.0;
                t5_out = double.Parse(Convert.ToString((int) t5)) / 100000.0;

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

        private void Jog_set_speed_Click(object sender, EventArgs e)
        {
            int velocity = 0;
            try
            {
                velocity = Convert.ToInt32(jog_speed_tb.Text) * 1000;
                for (int ind = 0; ind < 5; ind++)
                {
                    write_d_mem_32_bit(640 + 2 * ind, velocity);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Forward_btn_MouseDown(object sender, MouseEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);
            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGFORWARD1, 1);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_GREEN);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGFORWARD2, 1);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_GREEN);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGFORWARD3, 1);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_GREEN);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGFORWARD4, 1);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_GREEN);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGFORWARD5, 1);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_GREEN);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }

        }

        private void Forward_btn_MouseUp(object sender, MouseEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            PLCWritebit(Constants.R_JOGGINGFORWARD1, 0);
            ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGFORWARD1, 0);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGFORWARD2, 0);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGFORWARD3, 0);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGFORWARD4, 0);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGFORWARD5, 0);
                    ChangeColorObject(Forward_btn, Constants.OBJECT_WHITE);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Backward_btn_MouseDown(object sender, MouseEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGINVERSE1, 1);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_GREEN);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGINVERSE2, 1);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_GREEN);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGINVERSE3, 1);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_GREEN);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGINVERSE4, 1);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_GREEN);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGINVERSE5, 1);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_GREEN);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Backward_btn_MouseUp(object sender, MouseEventArgs e)
        {
            int joint = Convert.ToInt16(joint_tb.Text);

            switch (joint)
            {
                case 1:
                    PLCWritebit(Constants.R_JOGGINGINVERSE1, 0);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_WHITE);
                    break;

                case 2:
                    PLCWritebit(Constants.R_JOGGINGINVERSE2, 0);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_WHITE);
                    break;

                case 3:
                    PLCWritebit(Constants.R_JOGGINGINVERSE3, 0);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_WHITE);
                    break;

                case 4:
                    PLCWritebit(Constants.R_JOGGINGINVERSE4, 0);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_WHITE);
                    break;

                case 5:
                    PLCWritebit(Constants.R_JOGGINGINVERSE5, 0);
                    ChangeColorObject(Backward_btn, Constants.OBJECT_WHITE);
                    break;

                default:
                    PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Invalid input number");
                    break;
            }
        }

        private void Tsm_moveJ_btn_Click(object sender, EventArgs e)
        {
            move = 1; /* moveJ */
            double x, y, z;
            double t1, t2, t3, t4, t5;
            int ret;
            int[] temp_value = new int[5];
            try
            {
                x = double.Parse(Mvx_tb.Text);
                y = double.Parse(Mvy_tb.Text);
                z = double.Parse(Mvz_tb.Text);

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

                int[] value_angle = new int[10];
                /* Run */
                temp_value[0] = (int)(Convert.ToDouble(t1_tb.Text) * 100000 + 18000000);
                temp_value[1] = (int)(Convert.ToDouble(t2_tb.Text) * 100000 + 18000000);
                temp_value[2] = (int)(Convert.ToDouble(t3_tb.Text) * 100000 + 18000000);
                temp_value[3] = (int)(Convert.ToDouble(t4_tb.Text) * 100000 + 18000000);
                temp_value[4] = (int)(Convert.ToDouble(t5_tb.Text) * 100000 + 18000000);
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

        private void run_btn_Click(object sender, EventArgs e)
        {
            if (move == 1)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(528);
            }
            else if (move == 2)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(530);
            }
            else if (move == 3)
            {
                /* Turn on relay */
                turn_on_1_pulse_relay(530);
            }

            move = 0;
        }

        private void bt_on_matlab_Click(object sender, EventArgs e)
        {
            // Execute MATLAB Robot Simulink
            // matlab.Execute(@"cd 'C:\Users\daveb\Desktop\5_DOF_Robot_Arm\matlab\guide_simulink'");
            matlab.Execute($"cd '{TargetDirectory}'");
            matlab.Execute(@"open_system('Complete.slx');");
            matlab.Execute(@"sim('Complete');");
        }

        private void bt_off_matlab_Click(object sender, EventArgs e)
        {
            frame_off_matlab[0] = (byte)(0xFF);
            frame_off_matlab[1] = (byte)(0xFF);

            frame_off_matlab[2] = (byte)(0x00);
            frame_off_matlab[3] = (byte)(0x00);

            frame_off_matlab[4] = (byte)(0xFF);
            frame_off_matlab[5] = (byte)(0xFF);

            frame_off_matlab[6] = (byte)(0xFF);
            frame_off_matlab[7] = (byte)(0xFF);

            frame_off_matlab[8] = (byte)(0xFF);
            frame_off_matlab[9] = (byte)(0xFF);

            for (int i = 0; i < 5; i++)
            {
                serialPort1.Write(frame_off_matlab, 0, frame_off_matlab.Length);
            }
        }

        private void Tsm_moveC_btn_Click(object sender, EventArgs e)
        {
            move = 3;
            double x1, y1, x2, y2, x_cur, y_cur, z_cur;
            double[] vect_u = new double[3];
            double[] curr_pos = new double[3];
            double[] targ_pos = new double[3];
            double t1, t2, t3, t4, t5;
            int[,] angle_array = new int[10, 5];
            double x, y, z;
            int ret;
            int[] value_angle = new int[80];
            int[] value_angle_t5 = new int[20];
            x_cur = Convert.ToDouble(X_curpos.Text);
            y_cur = Convert.ToDouble(Y_curpos.Text);
            z_cur = Convert.ToDouble(Z_curpos.Text);

            x1 = Convert.ToDouble(MvCx1_tb.Text);
            y1 = Convert.ToDouble(MvCy1_tb.Text);

            x2 = Convert.ToDouble(MvCx2_tb.Text);
            y2 = Convert.ToDouble(MvCy2_tb.Text);
            // Tạo các điểm (x, y)
            Point point1 = new Point(x_cur, y_cur);
            Point point2 = new Point(x1, y1);
            Point point3 = new Point(x2, y2);

            // Tính toán quỹ đạo đường tròn
            Circle circle = CalculateCircle(point1, point2, point3);

            // In kết quả
            Console.WriteLine($"Tâm đường tròn: ({circle.Center.X}, {circle.Center.Y})");
            Console.WriteLine($"Bán kính đường tròn: {circle.Radius}");

            double R = circle.Radius;
            double a = circle.Center.X;
            double b = circle.Center.Y;
            /* Linear Equation */
            for (int t = 0; t < 9; t++)
            {
                x = R * Math.Sin(2*Math.PI * t / 9) + a;
                y = R * Math.Cos(2*Math.PI * t / 9) + b;
                z = z_cur;
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
            for (int j = 0; j < 9; j++)
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
            value_angle[8 * 9] = Write_Theta(angle_array[0, 0])[0];
            value_angle[8 * 9 + 1] = Write_Theta(angle_array[0, 0])[1];

            value_angle[8 * 9 + 2] = Write_Theta(angle_array[0, 1])[0];
            value_angle[8 * 9 + 3] = Write_Theta(angle_array[0, 1])[1];

            value_angle[8 * 9 + 4] = Write_Theta(angle_array[0, 2])[0];
            value_angle[8 * 9 + 5] = Write_Theta(angle_array[0, 2])[1];

            value_angle[8 * 9 + 6] = Write_Theta(angle_array[0, 3])[0];
            value_angle[8 * 9 + 7] = Write_Theta(angle_array[0, 3])[1];

            plc.WriteDeviceBlock("D1010", 80, ref value_angle[0]);
            for (int j = 0; j < 10; j++)
            {
                value_angle_t5[2 * j] = Write_Theta(angle_array[j, 4])[0];
                value_angle_t5[2 * j + 1] = Write_Theta(angle_array[j, 4])[1];
            }
        }

        private void Tsm_moveL_btn_Click(object sender, EventArgs e)
        {
            move = 2;
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

            targ_pos[0] = Convert.ToDouble(Mvx_tb.Text);
            targ_pos[1] = Convert.ToDouble(Mvy_tb.Text);
            targ_pos[2] = Convert.ToDouble(Mvz_tb.Text);

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
        }

        private void set_const_speed_btn_Click(object sender, EventArgs e)
        {
            int velocity;
            try
            {
                velocity = Convert.ToInt32(spd_tb.Text) * 1000;
                write_d_mem_32_bit(1008, velocity);
                PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Set Velocity successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bOpen_Click(object sender, EventArgs e)
        {
            try
            {   //Set up parameter for COM port
                serialPort1.PortName = cBoxPort.Text;
                serialPort1.BaudRate = Convert.ToInt32(cBoxBaudRate.Text);
                serialPort1.DataBits = Convert.ToInt32(cBoxDataBits.Text);
                serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), cBoxStopBits.Text);
                serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), cBoxParityBits.Text);
                serialPort1.Open();
                progressBar1.Value = 100;

                if (bt_start_timer.Enabled == true)
                {
                    bt_off_matlab.Enabled = true;
                    bt_stop_record.Enabled = true;
                    bt_start_record.Enabled = true;
                }

                bOpen.Enabled = false;
                bClose.Enabled = true;
            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error :))", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bOpen.Enabled = true;
                bClose.Enabled = false;

            }
        }

        private void bt_start_record_Click(object sender, EventArgs e)
        {
            bt_start_record.Enabled = false;
            bt_stop_record.Enabled = true;

            PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Start Record data");

            frame_start_record[0] = (byte) (0xFF);
            frame_start_record[1] = (byte) (0xFF);

            frame_start_record[2] = (byte) (0x01);
            frame_start_record[3] = (byte) (0x01);

            frame_start_record[4] = (byte) (0xFF);
            frame_start_record[5] = (byte) (0xFF);

            frame_start_record[6] = (byte) (0xFF);
            frame_start_record[7] = (byte) (0xFF);

            frame_start_record[8] = (byte) (0xFF);
            frame_start_record[9] = (byte) (0xFF);

            for (int i = 0; i < 10; i++)
            {
                serialPort1.Write(frame_start_record, 0, frame_start_record.Length);
            }
        }

        private void bt_stop_record_Click(object sender, EventArgs e)
        {
            bt_stop_record.Enabled = false;
            bt_start_record.Enabled = true;

            PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Stop Record data");

            frame_stop_record[0] = (byte)(0xFF);
            frame_stop_record[1] = (byte)(0xFF);

            frame_stop_record[2] = (byte)(0x02);
            frame_stop_record[3] = (byte)(0x02);

            frame_stop_record[4] = (byte)(0xFF);
            frame_stop_record[5] = (byte)(0xFF);

            frame_stop_record[6] = (byte)(0xFF);
            frame_stop_record[7] = (byte)(0xFF);

            frame_stop_record[8] = (byte)(0xFF);
            frame_stop_record[9] = (byte)(0xFF);

            for (int i = 0; i < 10; i++)
            {
                serialPort1.Write(frame_stop_record, 0, frame_stop_record.Length);
            }
        }

        private void bt_start_timer_Click(object sender, EventArgs e)
        {
            /* Timer start */
            Timer1.Start();

            bt_start_record.Enabled = false;
            bt_stop_record.Enabled = false;
            bt_off_matlab.Enabled = false;
            bt_start_timer.Enabled = false;
            bt_stop_timer.Enabled = true;
        }

        private void bt_stop_timer_Click(object sender, EventArgs e)
        {
            /* Timer stop */
            Timer1.Stop();

            if (bOpen.Enabled == false)
            {
                bt_start_record.Enabled = true;
                bt_stop_record.Enabled = true;
                bt_off_matlab.Enabled = true;
            }

            bt_start_timer.Enabled = true;
            bt_stop_timer.Enabled = false;
        }

        private void bClose_Click(object sender, EventArgs e)
        {
            bt_off_matlab.Enabled = false;
            bt_stop_record.Enabled = false;
            bt_start_record.Enabled = false;

            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                bOpen.Enabled = true;
                bClose.Enabled = false;
                progressBar1.Value = 0;

            }
        }
        class Point
        {
            public double X { get; set; }
            public double Y { get; set; }

            public Point(double x, double y)
            {
                X = x;
                Y = y;
            }
        }

        class Circle
        {
            public Point Center { get; set; }
            public double Radius { get; set; }

            public Circle(Point center, double radius)
            {
                Center = center;
                Radius = radius;
            }
        }
        static Circle CalculateCircle(Point point1, Point point2, Point point3)
        {
            double x1 = point1.X;
            double y1 = point1.Y;
            double x2 = point2.X;
            double y2 = point2.Y;
            double x3 = point3.X;
            double y3 = point3.Y;

            double x12 = x1 - x2;
            double x13 = x1 - x3;

            double y12 = y1 - y2;
            double y13 = y1 - y3;

            double y31 = y3 - y1;
            double y21 = y2 - y1;

            double x31 = x3 - x1;
            double x21 = x2 - x1;

            double sx13 = Math.Pow(x1, 2) - Math.Pow(x3, 2);
            double sy13 = Math.Pow(y1, 2) - Math.Pow(y3, 2);

            double sx21 = Math.Pow(x2, 2) - Math.Pow(x1, 2);
            double sy21 = Math.Pow(y2, 2) - Math.Pow(y1, 2);

            double f = ((sx13) * (x12) + (sy13) * (x12) + (sx21) * (x13) + (sy21) * (x13)) / (2 * ((y31) * (x12) - (y21) * (x13)));
            double g = ((sx13) * (y12) + (sy13) * (y12) + (sx21) * (y13) + (sy21) * (y13)) / (2 * ((x31) * (y12) - (x21) * (y13)));

            double c = -Math.Pow(x1, 2) - Math.Pow(y1, 2) - 2 * g * x1 - 2 * f * y1;

            double h = -g;
            double k = -f;
            double sqr_of_r = h * h + k * k - c;

            double r = Math.Sqrt(sqr_of_r);

            return new Circle(new Point(h, k), r);
        }
    }
}

