namespace COM_PORT
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cBoxRTS = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.bClose = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.cBoxDTR = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.cBoxDataBits = new System.Windows.Forms.ComboBox();
            this.cBoxStopBits = new System.Windows.Forms.ComboBox();
            this.cBoxParityBits = new System.Windows.Forms.ComboBox();
            this.cBoxPort = new System.Windows.Forms.ComboBox();
            this.bSend = new System.Windows.Forms.Button();
            this.tBoxDataOut = new System.Windows.Forms.TextBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.Data_10_funtion = new System.Windows.Forms.TextBox();
            this.Number_Byte = new System.Windows.Forms.TextBox();
            this.Start_Address = new System.Windows.Forms.TextBox();
            this.Command = new System.Windows.Forms.TextBox();
            this.bt_Send_Slave = new System.Windows.Forms.Button();
            this.bt_Caculate_CRC = new System.Windows.Forms.Button();
            this.tBox_Data_CRC = new System.Windows.Forms.TextBox();
            this.Address = new System.Windows.Forms.TextBox();
            this.lbLengthText = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.bClear = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tBox_Receive_from_Slave = new System.Windows.Forms.TextBox();
            this.lbLenghIN = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.checkBoxOldUpdate = new System.Windows.Forms.CheckBox();
            this.checkBoxUpdate = new System.Windows.Forms.CheckBox();
            this.bClearIN = new System.Windows.Forms.Button();
            this.tBoxDataIn = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cBoxRTS);
            this.groupBox1.Controls.Add(this.groupBox2);
            this.groupBox1.Controls.Add(this.cBoxDTR);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cBoxBaudRate);
            this.groupBox1.Controls.Add(this.cBoxDataBits);
            this.groupBox1.Controls.Add(this.cBoxStopBits);
            this.groupBox1.Controls.Add(this.cBoxParityBits);
            this.groupBox1.Controls.Add(this.cBoxPort);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.groupBox1.Location = new System.Drawing.Point(24, 27);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(359, 452);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Com Port Control";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // cBoxRTS
            // 
            this.cBoxRTS.AutoSize = true;
            this.cBoxRTS.Location = new System.Drawing.Point(201, 290);
            this.cBoxRTS.Name = "cBoxRTS";
            this.cBoxRTS.Size = new System.Drawing.Size(116, 21);
            this.cBoxRTS.TabIndex = 5;
            this.cBoxRTS.Text = "RTS ENABLE";
            this.cBoxRTS.UseVisualStyleBackColor = true;
            this.cBoxRTS.CheckedChanged += new System.EventHandler(this.cBoxRTS_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.BackColor = System.Drawing.SystemColors.ControlLight;
            this.groupBox2.Controls.Add(this.progressBar1);
            this.groupBox2.Controls.Add(this.bClose);
            this.groupBox2.Controls.Add(this.bOpen);
            this.groupBox2.Location = new System.Drawing.Point(24, 333);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(298, 100);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(21, 71);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(258, 23);
            this.progressBar1.TabIndex = 2;
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.Red;
            this.bClose.Location = new System.Drawing.Point(153, 32);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(126, 33);
            this.bClose.TabIndex = 1;
            this.bClose.Text = "CLOSE";
            this.bClose.UseVisualStyleBackColor = false;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bOpen
            // 
            this.bOpen.BackColor = System.Drawing.Color.LimeGreen;
            this.bOpen.Location = new System.Drawing.Point(21, 32);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(126, 33);
            this.bOpen.TabIndex = 0;
            this.bOpen.Text = "OPEN";
            this.bOpen.UseVisualStyleBackColor = false;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // cBoxDTR
            // 
            this.cBoxDTR.AutoSize = true;
            this.cBoxDTR.Location = new System.Drawing.Point(49, 290);
            this.cBoxDTR.Name = "cBoxDTR";
            this.cBoxDTR.Size = new System.Drawing.Size(117, 21);
            this.cBoxDTR.TabIndex = 10;
            this.cBoxDTR.Text = "DTR ENABLE";
            this.cBoxDTR.UseVisualStyleBackColor = true;
            this.cBoxDTR.CheckedChanged += new System.EventHandler(this.cBoxDTR_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(91, 238);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(91, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "PARITY BITS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 189);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(80, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "STOP BITS";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(90, 141);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "DATA BITS";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "BAUD RATE";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(90, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "COM PORT";
            // 
            // cBoxBaudRate
            // 
            this.cBoxBaudRate.FormattingEnabled = true;
            this.cBoxBaudRate.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "11400",
            "19200"});
            this.cBoxBaudRate.Location = new System.Drawing.Point(201, 86);
            this.cBoxBaudRate.Name = "cBoxBaudRate";
            this.cBoxBaudRate.Size = new System.Drawing.Size(121, 24);
            this.cBoxBaudRate.TabIndex = 4;
            this.cBoxBaudRate.Text = "9600";
            // 
            // cBoxDataBits
            // 
            this.cBoxDataBits.FormattingEnabled = true;
            this.cBoxDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cBoxDataBits.Location = new System.Drawing.Point(201, 134);
            this.cBoxDataBits.Name = "cBoxDataBits";
            this.cBoxDataBits.Size = new System.Drawing.Size(121, 24);
            this.cBoxDataBits.TabIndex = 3;
            this.cBoxDataBits.Text = "8";
            // 
            // cBoxStopBits
            // 
            this.cBoxStopBits.FormattingEnabled = true;
            this.cBoxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cBoxStopBits.Location = new System.Drawing.Point(201, 182);
            this.cBoxStopBits.Name = "cBoxStopBits";
            this.cBoxStopBits.Size = new System.Drawing.Size(121, 24);
            this.cBoxStopBits.TabIndex = 2;
            this.cBoxStopBits.Text = "1";
            // 
            // cBoxParityBits
            // 
            this.cBoxParityBits.FormattingEnabled = true;
            this.cBoxParityBits.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cBoxParityBits.Location = new System.Drawing.Point(201, 231);
            this.cBoxParityBits.Name = "cBoxParityBits";
            this.cBoxParityBits.Size = new System.Drawing.Size(121, 24);
            this.cBoxParityBits.TabIndex = 1;
            this.cBoxParityBits.Text = "None";
            // 
            // cBoxPort
            // 
            this.cBoxPort.FormattingEnabled = true;
            this.cBoxPort.Location = new System.Drawing.Point(201, 38);
            this.cBoxPort.Name = "cBoxPort";
            this.cBoxPort.Size = new System.Drawing.Size(121, 24);
            this.cBoxPort.TabIndex = 0;
            // 
            // bSend
            // 
            this.bSend.BackColor = System.Drawing.SystemColors.Info;
            this.bSend.ForeColor = System.Drawing.Color.Maroon;
            this.bSend.Location = new System.Drawing.Point(19, 388);
            this.bSend.Name = "bSend";
            this.bSend.Size = new System.Drawing.Size(129, 37);
            this.bSend.TabIndex = 3;
            this.bSend.Text = "Send Data";
            this.bSend.UseVisualStyleBackColor = false;
            this.bSend.Click += new System.EventHandler(this.bSend_Click);
            // 
            // tBoxDataOut
            // 
            this.tBoxDataOut.Location = new System.Drawing.Point(19, 27);
            this.tBoxDataOut.Multiline = true;
            this.tBoxDataOut.Name = "tBoxDataOut";
            this.tBoxDataOut.Size = new System.Drawing.Size(385, 95);
            this.tBoxDataOut.TabIndex = 4;
            this.tBoxDataOut.TextChanged += new System.EventHandler(this.tBoxDataOut_TextChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.Data_10_funtion);
            this.groupBox3.Controls.Add(this.Number_Byte);
            this.groupBox3.Controls.Add(this.Start_Address);
            this.groupBox3.Controls.Add(this.Command);
            this.groupBox3.Controls.Add(this.bt_Send_Slave);
            this.groupBox3.Controls.Add(this.bt_Caculate_CRC);
            this.groupBox3.Controls.Add(this.tBox_Data_CRC);
            this.groupBox3.Controls.Add(this.Address);
            this.groupBox3.Controls.Add(this.lbLengthText);
            this.groupBox3.Controls.Add(this.label6);
            this.groupBox3.Controls.Add(this.checkBox1);
            this.groupBox3.Controls.Add(this.checkBox2);
            this.groupBox3.Controls.Add(this.bClear);
            this.groupBox3.Controls.Add(this.tBoxDataOut);
            this.groupBox3.Controls.Add(this.bSend);
            this.groupBox3.Location = new System.Drawing.Point(389, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(429, 452);
            this.groupBox3.TabIndex = 11;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Transmitter Control";
            // 
            // Data_10_funtion
            // 
            this.Data_10_funtion.Location = new System.Drawing.Point(20, 191);
            this.Data_10_funtion.Multiline = true;
            this.Data_10_funtion.Name = "Data_10_funtion";
            this.Data_10_funtion.Size = new System.Drawing.Size(385, 26);
            this.Data_10_funtion.TabIndex = 24;
            // 
            // Number_Byte
            // 
            this.Number_Byte.Location = new System.Drawing.Point(300, 160);
            this.Number_Byte.Multiline = true;
            this.Number_Byte.Name = "Number_Byte";
            this.Number_Byte.Size = new System.Drawing.Size(77, 26);
            this.Number_Byte.TabIndex = 23;
            // 
            // Start_Address
            // 
            this.Start_Address.Location = new System.Drawing.Point(218, 160);
            this.Start_Address.Multiline = true;
            this.Start_Address.Name = "Start_Address";
            this.Start_Address.Size = new System.Drawing.Size(77, 26);
            this.Start_Address.TabIndex = 22;
            // 
            // Command
            // 
            this.Command.Location = new System.Drawing.Point(136, 160);
            this.Command.Multiline = true;
            this.Command.Name = "Command";
            this.Command.Size = new System.Drawing.Size(77, 26);
            this.Command.TabIndex = 21;
            // 
            // bt_Send_Slave
            // 
            this.bt_Send_Slave.BackColor = System.Drawing.Color.Lime;
            this.bt_Send_Slave.ForeColor = System.Drawing.Color.Maroon;
            this.bt_Send_Slave.Location = new System.Drawing.Point(146, 333);
            this.bt_Send_Slave.Name = "bt_Send_Slave";
            this.bt_Send_Slave.Size = new System.Drawing.Size(129, 37);
            this.bt_Send_Slave.TabIndex = 20;
            this.bt_Send_Slave.Text = "Send to Slave";
            this.bt_Send_Slave.UseVisualStyleBackColor = false;
            this.bt_Send_Slave.Click += new System.EventHandler(this.bt_Send_Slave_Click);
            // 
            // bt_Caculate_CRC
            // 
            this.bt_Caculate_CRC.BackColor = System.Drawing.Color.PeachPuff;
            this.bt_Caculate_CRC.ForeColor = System.Drawing.Color.Maroon;
            this.bt_Caculate_CRC.Location = new System.Drawing.Point(146, 223);
            this.bt_Caculate_CRC.Name = "bt_Caculate_CRC";
            this.bt_Caculate_CRC.Size = new System.Drawing.Size(129, 37);
            this.bt_Caculate_CRC.TabIndex = 19;
            this.bt_Caculate_CRC.Text = "Caculate_CRC";
            this.bt_Caculate_CRC.UseVisualStyleBackColor = false;
            this.bt_Caculate_CRC.Click += new System.EventHandler(this.bt_Caculate_CRC_Click);
            // 
            // tBox_Data_CRC
            // 
            this.tBox_Data_CRC.Location = new System.Drawing.Point(20, 266);
            this.tBox_Data_CRC.Multiline = true;
            this.tBox_Data_CRC.Name = "tBox_Data_CRC";
            this.tBox_Data_CRC.Size = new System.Drawing.Size(385, 61);
            this.tBox_Data_CRC.TabIndex = 18;
            // 
            // Address
            // 
            this.Address.Location = new System.Drawing.Point(54, 160);
            this.Address.Multiline = true;
            this.Address.Name = "Address";
            this.Address.Size = new System.Drawing.Size(77, 26);
            this.Address.TabIndex = 17;
            // 
            // lbLengthText
            // 
            this.lbLengthText.AutoSize = true;
            this.lbLengthText.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLengthText.Location = new System.Drawing.Point(396, 86);
            this.lbLengthText.Name = "lbLengthText";
            this.lbLengthText.Size = new System.Drawing.Size(0, 17);
            this.lbLengthText.TabIndex = 16;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Length :";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(315, 415);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(63, 21);
            this.checkBox1.TabIndex = 14;
            this.checkBox1.Text = "Write";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(315, 388);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(90, 21);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "WriteLine";
            this.checkBox2.UseVisualStyleBackColor = true;
            this.checkBox2.CheckedChanged += new System.EventHandler(this.checkBox2_CheckedChanged);
            // 
            // bClear
            // 
            this.bClear.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bClear.ForeColor = System.Drawing.Color.Maroon;
            this.bClear.Location = new System.Drawing.Point(167, 388);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(129, 37);
            this.bClear.TabIndex = 5;
            this.bClear.Text = "Clear Data";
            this.bClear.UseVisualStyleBackColor = false;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tBox_Receive_from_Slave);
            this.groupBox4.Controls.Add(this.lbLenghIN);
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.checkBoxOldUpdate);
            this.groupBox4.Controls.Add(this.checkBoxUpdate);
            this.groupBox4.Controls.Add(this.bClearIN);
            this.groupBox4.Controls.Add(this.tBoxDataIn);
            this.groupBox4.Location = new System.Drawing.Point(824, 27);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(429, 452);
            this.groupBox4.TabIndex = 17;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Receiver Control";
            // 
            // tBox_Receive_from_Slave
            // 
            this.tBox_Receive_from_Slave.Location = new System.Drawing.Point(19, 160);
            this.tBox_Receive_from_Slave.Multiline = true;
            this.tBox_Receive_from_Slave.Name = "tBox_Receive_from_Slave";
            this.tBox_Receive_from_Slave.Size = new System.Drawing.Size(385, 82);
            this.tBox_Receive_from_Slave.TabIndex = 18;
            // 
            // lbLenghIN
            // 
            this.lbLenghIN.AutoSize = true;
            this.lbLenghIN.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbLenghIN.Location = new System.Drawing.Point(384, 86);
            this.lbLenghIN.Name = "lbLenghIN";
            this.lbLenghIN.Size = new System.Drawing.Size(0, 17);
            this.lbLenghIN.TabIndex = 16;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(311, 86);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 17);
            this.label8.TabIndex = 15;
            this.label8.Text = "Length :";
            // 
            // checkBoxOldUpdate
            // 
            this.checkBoxOldUpdate.AutoSize = true;
            this.checkBoxOldUpdate.Location = new System.Drawing.Point(201, 388);
            this.checkBoxOldUpdate.Name = "checkBoxOldUpdate";
            this.checkBoxOldUpdate.Size = new System.Drawing.Size(152, 21);
            this.checkBoxOldUpdate.TabIndex = 14;
            this.checkBoxOldUpdate.Text = "Add To Old Update";
            this.checkBoxOldUpdate.UseVisualStyleBackColor = true;
            this.checkBoxOldUpdate.CheckedChanged += new System.EventHandler(this.checkBoxOldUpdate_CheckedChanged);
            // 
            // checkBoxUpdate
            // 
            this.checkBoxUpdate.AutoSize = true;
            this.checkBoxUpdate.Location = new System.Drawing.Point(201, 361);
            this.checkBoxUpdate.Name = "checkBoxUpdate";
            this.checkBoxUpdate.Size = new System.Drawing.Size(123, 21);
            this.checkBoxUpdate.TabIndex = 13;
            this.checkBoxUpdate.Text = "Always Update";
            this.checkBoxUpdate.UseVisualStyleBackColor = true;
            this.checkBoxUpdate.CheckedChanged += new System.EventHandler(this.checkBoxUpdate_CheckedChanged);
            // 
            // bClearIN
            // 
            this.bClearIN.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.bClearIN.ForeColor = System.Drawing.Color.Maroon;
            this.bClearIN.Location = new System.Drawing.Point(19, 355);
            this.bClearIN.Name = "bClearIN";
            this.bClearIN.Size = new System.Drawing.Size(161, 72);
            this.bClearIN.TabIndex = 5;
            this.bClearIN.Text = "Clear Data";
            this.bClearIN.UseVisualStyleBackColor = false;
            this.bClearIN.Click += new System.EventHandler(this.bClearIN_Click);
            // 
            // tBoxDataIn
            // 
            this.tBoxDataIn.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.tBoxDataIn.Location = new System.Drawing.Point(19, 27);
            this.tBoxDataIn.Multiline = true;
            this.tBoxDataIn.Name = "tBoxDataIn";
            this.tBoxDataIn.ReadOnly = true;
            this.tBoxDataIn.Size = new System.Drawing.Size(385, 95);
            this.tBoxDataIn.TabIndex = 4;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 545);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "COM PORT C#";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cBoxPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cBoxBaudRate;
        private System.Windows.Forms.ComboBox cBoxDataBits;
        private System.Windows.Forms.ComboBox cBoxStopBits;
        private System.Windows.Forms.ComboBox cBoxParityBits;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Button bSend;
        private System.Windows.Forms.TextBox tBoxDataOut;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.CheckBox cBoxDTR;
        private System.Windows.Forms.CheckBox cBoxRTS;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Label lbLengthText;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label lbLenghIN;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button bClearIN;
        private System.Windows.Forms.TextBox tBoxDataIn;
        private System.Windows.Forms.CheckBox checkBoxOldUpdate;
        private System.Windows.Forms.CheckBox checkBoxUpdate;
        private System.Windows.Forms.Button bt_Send_Slave;
        private System.Windows.Forms.Button bt_Caculate_CRC;
        private System.Windows.Forms.TextBox tBox_Data_CRC;
        private System.Windows.Forms.TextBox Address;
        private System.Windows.Forms.TextBox tBox_Receive_from_Slave;
        private System.Windows.Forms.TextBox Number_Byte;
        private System.Windows.Forms.TextBox Start_Address;
        private System.Windows.Forms.TextBox Command;
        private System.Windows.Forms.TextBox Data_10_funtion;
    }
}

