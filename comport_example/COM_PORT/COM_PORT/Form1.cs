using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;

namespace COM_PORT
{
    public partial class Form1 : Form
    {
        string dataOUT;
        string sendwith;
        char led1 = '1', led2 = '1', led3 = '1', led4 = '1', led5 = '1', led6 = '1', led7 = '1', led8 = '1';
        string led_data;
        byte[] frame_03 = new byte[8];
        byte[] frame_data = new byte[100];
        int value = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public static byte[] StringToHex(string input)
        {
            // Xóa khoảng trắng và các ký tự không hợp lệ khác khỏi chuỗi đầu vào
            input = new string(input.Where(c => "0123456789abcdefABCDEF".Contains(c)).ToArray());

            // Kiểm tra độ dài của chuỗi đầu vào và đảm bảo nó là số chẵn
            if (input.Length % 2 != 0)
                throw new ArgumentException("Chuỗi đầu vào không hợp lệ");

            // Chuyển đổi chuỗi Hexa thành mảng byte
            return Enumerable.Range(0, input.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(input.Substring(x, 2), 16))
                             .ToArray();
        }

        private void DisplayCRC_03(byte slaveAddress, byte funtionCode, ushort startAddress, ushort numberOfPoints)
        {
            //Clear data
            tBox_Data_CRC.Text = "";

            //Build Message (FC03)
            frame_03[0] = slaveAddress;
            frame_03[1] = funtionCode;
            frame_03[2] = (byte)(startAddress >> 8);
            frame_03[3] = (byte)(startAddress);
            frame_03[4] = (byte)(numberOfPoints >> 8);
            frame_03[5] = (byte)numberOfPoints;

            byte[] checkSum = CRC16(frame_03);
            frame_03[6] = checkSum[0];
            frame_03[7] = checkSum[1];

            //print
            foreach (var item in frame_03)
            {
                tBox_Data_CRC.Text += string.Format("{0:X2}", item) + ' ';
            }
        }

        private void DisplayCRC_10(byte slaveAddress, byte funtionCode, ushort startAddress, ushort numberOfPoints, byte[] frame_data_10)
        {
            //Clear data
            tBox_Data_CRC.Text = "";

            //Build Message (FC03)
            frame_data[0] = slaveAddress;
            frame_data[1] = funtionCode;
            frame_data[2] = (byte)(startAddress >> 8);
            frame_data[3] = (byte)(startAddress);
            frame_data[4] = (byte)(numberOfPoints >> 8);
            frame_data[5] = (byte)numberOfPoints;
            frame_data[6] = (byte)(numberOfPoints*2);

            //Bo data funtion 10 vao frame data chuan 100 gia tri
            for (int i = 0; i < frame_data_10.Length; i++)
            {
                frame_data[7 + i] = frame_data_10[i];
            }

            //Rut gon frame de tinh CRC
            byte[] frame_temp = new byte[frame_data_10.Length + 7 + 2];

            for (int j = 0; j < frame_temp.Length; j++)
            {
                frame_temp[j] = frame_data[j];
            }
            
            //Tinh CRC
            byte[] checkSum = CRC16(frame_temp);
            frame_temp[frame_data_10.Length + 7 + 2 - 1] = checkSum[1];
            frame_temp[frame_data_10.Length + 7 + 2 - 1 - 1] = checkSum[0];

            //print
            foreach (var item in frame_temp)
            {
                tBox_Data_CRC.Text += string.Format("{0:X2}", item) + ' ';
            }
        }

        private static byte[] CRC16(byte[] data)
        {
            byte[] checkSum = new byte[2];
            ushort reg_crc = 0xFFFF;
            for (int i = 0; i < data.Length - 2; i++)
            {
                reg_crc ^= data[i];
                for (int j = 0; j < 8; j++)
                {
                    if ((reg_crc & 0x01) == 1)
                    {
                        reg_crc = (ushort)((reg_crc >> 1) ^ 0xA001);
                    }
                    else
                    {
                        reg_crc = (ushort)(reg_crc >> 1);
                    }
                }
            }
            checkSum[1] = (byte)((reg_crc >> 8) & 0xFF);
            checkSum[0] = (byte)(reg_crc & 0xFF);
            return checkSum;
        }

        public static byte ShiftBits(byte num, byte shiftAmount)
        {
            if (shiftAmount >= 0)
            {
                return (byte)(num << shiftAmount);
            }
            else
            {
                return (byte)(num >> Math.Abs(shiftAmount));
            }
        }

        private void prepare_data()
        {
            string data_led;
            data_led = String.Concat(led8, led7, led6, led5, led4, led3, led2, led1);
            led_data = String.Concat('~', led8, led7, led6, led5, led4, led3, led2, led1);
        }
        private void send_data()
        {
            if (serialPort1.IsOpen)
            {
                prepare_data();
                dataOUT = led_data;
                serialPort1.Write(dataOUT);
            }

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void bClose_Click(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
                bOpen.Enabled = true;
                bClose.Enabled = false;
                progressBar1.Value = 0;

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] ports = SerialPort.GetPortNames(); //Gan gia tri cac port co trong may
            cBoxPort.Items.AddRange(ports); //Hien thi len cBoxPort

            cBoxDTR.Checked = false;
            serialPort1.DtrEnable = false;

            cBoxRTS.Checked = false;
            serialPort1.RtsEnable = false;

            checkBox2.Checked = false;
            checkBox1.Checked = false;

            sendwith = "Write";

            checkBoxOldUpdate.Checked = true;
            checkBoxUpdate.Checked = false;
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
                bOpen.Enabled = false;
                bClose.Enabled = true;


            }

            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Lỗi rồi bạn :))", MessageBoxButtons.OK, MessageBoxIcon.Error);
                bOpen.Enabled = true;
                bClose.Enabled = false;

            }

        }

        private void bSend_Click(object sender, EventArgs e)
        {          
            if (serialPort1.IsOpen)
            {
                dataOUT = tBoxDataOut.Text;
                if (sendwith == "Write")
                {
                    serialPort1.Write(dataOUT);
                }
                else if (sendwith == "WriteLine")
                {
                    serialPort1.WriteLine(dataOUT);
                }

            }

        }


        private void cBoxDTR_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxDTR.Checked)
            {
                serialPort1.DtrEnable = true;
            }
            else { serialPort1.DtrEnable = false; }

        }

        private void cBoxRTS_CheckedChanged(object sender, EventArgs e)
        {
            if (cBoxRTS.Checked)
            {
                serialPort1.RtsEnable = true;
            }
            else { serialPort1.RtsEnable = false; }
        }

        private void bClear_Click(object sender, EventArgs e)
        {
            if (tBoxDataOut.Text != "")
            {
                tBoxDataOut.Text = "";
            }

            if (tBox_Data_CRC.Text != "")
            {
                tBox_Data_CRC.Text = "";
            }
        }

        private void tBoxDataOut_TextChanged(object sender, EventArgs e)
        {
            int dataLength = tBoxDataOut.TextLength;
            lbLengthText.Text = string.Format("{0:00}", dataLength);

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked)
            {
                sendwith = "WriteLine";
                checkBox1.Checked = false;
                checkBox2.Checked = true;
            }
        }

        private void bt_Caculate_CRC_Click(object sender, EventArgs e)
        {
            // Lấy chuỗi từ TextBox
            string Add = Address.Text;
            string Com = Command.Text;
            string Sta = Start_Address.Text;
            string Num = Number_Byte.Text;

            //Lay gia tri tu data funtion 10
            byte[] Data_funtion_10 = StringToHex(Data_10_funtion.Text);

            //Lay gia tri 
            byte slaveAddress = Convert.ToByte(Add, 16);
            byte funtionCode = Convert.ToByte(Com, 16);
            ushort startAddress = Convert.ToUInt16(Sta, 16);
            ushort numberOfPoints = Convert.ToUInt16(Num, 16);

            //Xem xet cau lenh la 03 hay 10
            if (funtionCode == 0x03)
            {
                //Hien thi
                DisplayCRC_03(slaveAddress, funtionCode, startAddress, numberOfPoints);
            }

            if (funtionCode == 0x10)
            {
                //Hien thi
                DisplayCRC_10(slaveAddress, funtionCode, startAddress, numberOfPoints, Data_funtion_10);

            }


        }


        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                sendwith = "Write";
                checkBox2.Checked = false;
                checkBox1.Checked = true;
            }
        }

        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {

            // Đọc dữ liệu nhận được từ cổng COM
            byte[] buffer = new byte[serialPort1.BytesToRead];
            serialPort1.Read(buffer, 0, buffer.Length);

            // Chuyển đổi dữ liệu thành chuỗi số hexa và in vào TextBox
            string hexString = BitConverter.ToString(buffer).Replace("-", " ");
            Invoke(new Action(() => tBox_Receive_from_Slave.AppendText(hexString)));

            //Luu gia tri doc duoc
            for (int i = 3; i < buffer.Length - 2; i++)
            {
                value = (value << 8) | buffer[i];
            }

            Console.WriteLine(string.Format("{0:00}", value));
        }


        private void checkBoxUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxUpdate.Checked)
            {
                checkBoxUpdate.Checked = true;
                checkBoxOldUpdate.Checked = false;
            }
            
        }

        private void checkBoxOldUpdate_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxOldUpdate.Checked)
            {
                checkBoxUpdate.Checked = false;
                checkBoxOldUpdate.Checked = true;
            }
        }

        private void bClearIN_Click(object sender, EventArgs e)
        {
            if (tBoxDataIn.Text != "")
            {
                tBoxDataIn.Text = "";
            }

            if (tBox_Receive_from_Slave.Text != "")
            {
                tBox_Receive_from_Slave.Text = "";
            }
        }

        private void bt_Send_Slave_Click(object sender, EventArgs e)
        {
            //Clear
            value = 0;
            tBox_Receive_from_Slave.Text = "";


            if (serialPort1.IsOpen)
            {
                //Send start byte
                //string start = String.Concat(':');
                //serialPort1.Write(start);

                //Send data from text box
                byte[] frame_prepare_to_send = StringToHex(tBox_Data_CRC.Text);
                serialPort1.Write(frame_prepare_to_send, 0, frame_prepare_to_send.Length);
            }
        }
    }
}
