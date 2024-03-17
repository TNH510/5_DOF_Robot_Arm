using SharpDX.DirectWrite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RobotArmHelix
{
    /// <summary>
    /// Interaction logic for Menu_control.xaml
    /// </summary>
    public partial class Menu_control : Window
    {
        private MainWindow mainWindow; // Reference to MainWindow
        public Menu_control(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow; // Store the reference to MainWindow
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Change_color_mainwindow(object sender, RoutedEventArgs e)
        {
            /* Disable slider */
            mainWindow.joint1.IsEnabled = false;
            mainWindow.joint2.IsEnabled = false;
            mainWindow.joint3.IsEnabled = false;
            mainWindow.joint4.IsEnabled = false;
            mainWindow.joint5.IsEnabled = false;
            /* Disable test position button */
            mainWindow.testpos_bttn = false;
            /* Change the color of the button when clicked */
            mainWindow.ChangeColorObjectBackground(mainWindow.TestPos_bttn, Constants.OBJECT_MODIFIED);
            mainWindow.ChangeColorObjectForeground(mainWindow.TestPos_bttn, Constants.OBJECT_MODIFIED1);
            mainWindow.ChangeColorObjectBorderBrush(mainWindow.TestPos_bttn, Constants.OBJECT_MODIFIED);

            /* Declare the variable(s) */
            int ret;
            /* A logical station number set in Communication Setup Utility - Datasheet - Page 61 */
            mainWindow.plc.ActLogicalStationNumber = 1;
            /* Open the connection between PLC and C# - Datasheet - Page 381 */
            ret = mainWindow.plc.Open();
            /* Return value
               Normal termination : 0 is returned.
               Abnormal termination: Any value other than 0 is returned
            */
            if (ret == 0 && mainWindow.cn_bttn == true)
            {
                //Connect_button.IsEnabled = false;
                //Disconnect_button.IsEnabled = true;
                mainWindow.cn_bttn = false;
                mainWindow.ds_bttn = true;
                /* Change the color of the button when clicked */
                mainWindow.ChangeColorObjectBackground(mainWindow.Connect_button, Constants.OBJECT_MODIFIED);
                mainWindow.ChangeColorObjectBackground(mainWindow.Disconnect_button, Constants.OBJECT_MODIFIED1);
                mainWindow.ChangeColorObjectForeground(mainWindow.Connect_button, Constants.OBJECT_MODIFIED1);
                mainWindow.ChangeColorObjectForeground(mainWindow.Disconnect_button, Constants.OBJECT_MODIFIED);
                mainWindow.ChangeColorObjectBorderBrush(mainWindow.Disconnect_button, Constants.OBJECT_MODIFIED);
                /* 
                    Print the log command
                    MethosBase.GetCurrentMethod returns the action user did.
                */
                mainWindow.PrintLog("Info", MethodBase.GetCurrentMethod().Name, "Connect PLC successfully");
            }
            else if (mainWindow.ds_bttn == true)
            {
                mainWindow.PrintLog("Infor", MethodBase.GetCurrentMethod().Name, "PLC was connected");
            }
            else
            {
                mainWindow.PrintLog("Error", MethodBase.GetCurrentMethod().Name, "Connect PLC unsuccessfully");
            }
            /* Read the servo mode */
            int servo_status;
            string getName = MethodBase.GetCurrentMethod().Name;
            /* Read status of Brake and AC Servo */
            ret = mainWindow.PLCReadbit(Constants.R_SERVO_ON, out servo_status);
            if (ret != 0)
            {
                mainWindow.PrintLog("Error", getName, "Read PLC Fail");
                return;
            }
            if (servo_status == 0) /* Servo is currently off */
            {
                mainWindow.Servo_button.Content = "Servo: off";
                mainWindow.ChangeColorObjectBackground(mainWindow.Servo_button, Constants.OBJECT_MODIFIED1);
                mainWindow.ChangeColorObjectForeground(mainWindow.Servo_button, Constants.OBJECT_MODIFIED);
                mainWindow.PrintLog("SERVO:", servo_status.ToString(), "OFF");
            }
            else
            {
                mainWindow.Servo_button.Content = "Servo: on";
                mainWindow.ChangeColorObjectBackground(mainWindow.Servo_button, Constants.OBJECT_MODIFIED);
                mainWindow.ChangeColorObjectForeground(mainWindow.Servo_button, Constants.OBJECT_MODIFIED1);
                mainWindow.PrintLog("SERVO:", servo_status.ToString(), "ON");
            }
            /* Start timer1 and timer2 */
            // timer1.Start();
            mainWindow.Thread1Start();
            mainWindow.Thread2Start();
            //timer1.Start();
        }
    }
}
