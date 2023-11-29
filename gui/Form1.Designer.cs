namespace GUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.Disconnect_button = new System.Windows.Forms.Button();
            this.Connect_button = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.GoHome_button = new System.Windows.Forms.Button();
            this.SetHome_button = new System.Windows.Forms.Button();
            this.ResetError_button = new System.Windows.Forms.Button();
            this.OnServo_button = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.jog_speed_tb = new System.Windows.Forms.TextBox();
            this.joint_tb = new System.Windows.Forms.TextBox();
            this.Jog_set_speed = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.Backward_btn = new System.Windows.Forms.Button();
            this.Forward_btn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.ErrorLog = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.t5_tb = new System.Windows.Forms.TextBox();
            this.Roll_curpos = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.t4_tb = new System.Windows.Forms.TextBox();
            this.Pitch_curpos = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.t3_tb = new System.Windows.Forms.TextBox();
            this.Z_curpos = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.t2_tb = new System.Windows.Forms.TextBox();
            this.Y_curpos = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.t1_tb = new System.Windows.Forms.TextBox();
            this.X_curpos = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.Timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel5 = new System.Windows.Forms.Panel();
            this.MvCy2_tb = new System.Windows.Forms.TextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.MvCx2_tb = new System.Windows.Forms.TextBox();
            this.label30 = new System.Windows.Forms.Label();
            this.MvCy1_tb = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.MvCx1_tb = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.Tsm_moveC_btn = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.spd_tb = new System.Windows.Forms.TextBox();
            this.set_const_speed_btn = new System.Windows.Forms.Button();
            this.run_btn = new System.Windows.Forms.Button();
            this.Tsm_moveL_btn = new System.Windows.Forms.Button();
            this.Tsm_moveJ_btn = new System.Windows.Forms.Button();
            this.Mvz_tb = new System.Windows.Forms.TextBox();
            this.label21 = new System.Windows.Forms.Label();
            this.Mvy_tb = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.Mvx_tb = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.bt_off_matlab = new System.Windows.Forms.Button();
            this.bt_on_matlab = new System.Windows.Forms.Button();
            this.cBoxParityBits = new System.Windows.Forms.ComboBox();
            this.label24 = new System.Windows.Forms.Label();
            this.cBoxStopBits = new System.Windows.Forms.ComboBox();
            this.label22 = new System.Windows.Forms.Label();
            this.cBoxDataBits = new System.Windows.Forms.ComboBox();
            this.cBoxBaudRate = new System.Windows.Forms.ComboBox();
            this.cBoxPort = new System.Windows.Forms.ComboBox();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label18 = new System.Windows.Forms.Label();
            this.bClose = new System.Windows.Forms.Button();
            this.bOpen = new System.Windows.Forms.Button();
            this.label19 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.Disconnect_button);
            this.panel1.Controls.Add(this.Connect_button);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(123, 124);
            this.panel1.TabIndex = 0;
            // 
            // Disconnect_button
            // 
            this.Disconnect_button.BackColor = System.Drawing.Color.Red;
            this.Disconnect_button.Location = new System.Drawing.Point(3, 84);
            this.Disconnect_button.Name = "Disconnect_button";
            this.Disconnect_button.Size = new System.Drawing.Size(115, 26);
            this.Disconnect_button.TabIndex = 2;
            this.Disconnect_button.Text = "Disconnect";
            this.Disconnect_button.UseVisualStyleBackColor = false;
            this.Disconnect_button.Click += new System.EventHandler(this.Disconnect_button_Click);
            // 
            // Connect_button
            // 
            this.Connect_button.Location = new System.Drawing.Point(3, 52);
            this.Connect_button.Name = "Connect_button";
            this.Connect_button.Size = new System.Drawing.Size(115, 26);
            this.Connect_button.TabIndex = 1;
            this.Connect_button.Text = "Connect";
            this.Connect_button.UseVisualStyleBackColor = true;
            this.Connect_button.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(-1, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 38);
            this.label1.TabIndex = 1;
            this.label1.Text = "Connect";
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.GoHome_button);
            this.panel2.Controls.Add(this.SetHome_button);
            this.panel2.Controls.Add(this.ResetError_button);
            this.panel2.Controls.Add(this.OnServo_button);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Location = new System.Drawing.Point(142, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(248, 123);
            this.panel2.TabIndex = 3;
            // 
            // GoHome_button
            // 
            this.GoHome_button.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.GoHome_button.Location = new System.Drawing.Point(124, 84);
            this.GoHome_button.Name = "GoHome_button";
            this.GoHome_button.Size = new System.Drawing.Size(115, 26);
            this.GoHome_button.TabIndex = 4;
            this.GoHome_button.Text = "Go home";
            this.GoHome_button.UseVisualStyleBackColor = false;
            this.GoHome_button.Click += new System.EventHandler(this.GoHome_button_Click);
            // 
            // SetHome_button
            // 
            this.SetHome_button.Location = new System.Drawing.Point(124, 52);
            this.SetHome_button.Name = "SetHome_button";
            this.SetHome_button.Size = new System.Drawing.Size(115, 26);
            this.SetHome_button.TabIndex = 3;
            this.SetHome_button.Text = "Set home";
            this.SetHome_button.UseVisualStyleBackColor = true;
            this.SetHome_button.Click += new System.EventHandler(this.SetHome_button_Click);
            // 
            // ResetError_button
            // 
            this.ResetError_button.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ResetError_button.Location = new System.Drawing.Point(3, 84);
            this.ResetError_button.Name = "ResetError_button";
            this.ResetError_button.Size = new System.Drawing.Size(115, 26);
            this.ResetError_button.TabIndex = 2;
            this.ResetError_button.Text = "Reset error";
            this.ResetError_button.UseVisualStyleBackColor = false;
            this.ResetError_button.Click += new System.EventHandler(this.ResetError_button_Click);
            // 
            // OnServo_button
            // 
            this.OnServo_button.Location = new System.Drawing.Point(3, 52);
            this.OnServo_button.Name = "OnServo_button";
            this.OnServo_button.Size = new System.Drawing.Size(115, 26);
            this.OnServo_button.TabIndex = 1;
            this.OnServo_button.Text = "Servo";
            this.OnServo_button.UseVisualStyleBackColor = true;
            this.OnServo_button.Click += new System.EventHandler(this.OnServo_button_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label2.Location = new System.Drawing.Point(-1, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 38);
            this.label2.TabIndex = 1;
            this.label2.Text = "Control";
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.jog_speed_tb);
            this.panel3.Controls.Add(this.joint_tb);
            this.panel3.Controls.Add(this.Jog_set_speed);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.Backward_btn);
            this.panel3.Controls.Add(this.Forward_btn);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(12, 142);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(378, 89);
            this.panel3.TabIndex = 5;
            // 
            // jog_speed_tb
            // 
            this.jog_speed_tb.Location = new System.Drawing.Point(170, 11);
            this.jog_speed_tb.Name = "jog_speed_tb";
            this.jog_speed_tb.Size = new System.Drawing.Size(65, 22);
            this.jog_speed_tb.TabIndex = 6;
            this.jog_speed_tb.Text = "300";
            // 
            // joint_tb
            // 
            this.joint_tb.Location = new System.Drawing.Point(60, 52);
            this.joint_tb.Name = "joint_tb";
            this.joint_tb.Size = new System.Drawing.Size(59, 22);
            this.joint_tb.TabIndex = 8;
            this.joint_tb.Text = "1";
            // 
            // Jog_set_speed
            // 
            this.Jog_set_speed.Location = new System.Drawing.Point(254, 9);
            this.Jog_set_speed.Name = "Jog_set_speed";
            this.Jog_set_speed.Size = new System.Drawing.Size(113, 26);
            this.Jog_set_speed.TabIndex = 5;
            this.Jog_set_speed.Text = "Set speed";
            this.Jog_set_speed.UseVisualStyleBackColor = true;
            this.Jog_set_speed.Click += new System.EventHandler(this.Jog_set_speed_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label5.Location = new System.Drawing.Point(13, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 19);
            this.label5.TabIndex = 7;
            this.label5.Text = "Joint";
            // 
            // Backward_btn
            // 
            this.Backward_btn.Location = new System.Drawing.Point(254, 49);
            this.Backward_btn.Name = "Backward_btn";
            this.Backward_btn.Size = new System.Drawing.Size(115, 26);
            this.Backward_btn.TabIndex = 3;
            this.Backward_btn.Text = "Backward";
            this.Backward_btn.UseVisualStyleBackColor = true;
            this.Backward_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Backward_btn_MouseDown);
            this.Backward_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Backward_btn_MouseUp);
            // 
            // Forward_btn
            // 
            this.Forward_btn.Location = new System.Drawing.Point(133, 49);
            this.Forward_btn.Name = "Forward_btn";
            this.Forward_btn.Size = new System.Drawing.Size(115, 26);
            this.Forward_btn.TabIndex = 1;
            this.Forward_btn.Text = "Forward";
            this.Forward_btn.UseVisualStyleBackColor = true;
            this.Forward_btn.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Forward_btn_MouseDown);
            this.Forward_btn.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Forward_btn_MouseUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(-1, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(122, 38);
            this.label3.TabIndex = 1;
            this.label3.Text = "Jogging";
            // 
            // ErrorLog
            // 
            this.ErrorLog.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ErrorLog.Location = new System.Drawing.Point(303, 238);
            this.ErrorLog.Multiline = true;
            this.ErrorLog.Name = "ErrorLog";
            this.ErrorLog.Size = new System.Drawing.Size(347, 166);
            this.ErrorLog.TabIndex = 6;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.t5_tb);
            this.panel4.Controls.Add(this.Roll_curpos);
            this.panel4.Controls.Add(this.label15);
            this.panel4.Controls.Add(this.label16);
            this.panel4.Controls.Add(this.t4_tb);
            this.panel4.Controls.Add(this.Pitch_curpos);
            this.panel4.Controls.Add(this.label13);
            this.panel4.Controls.Add(this.label14);
            this.panel4.Controls.Add(this.t3_tb);
            this.panel4.Controls.Add(this.Z_curpos);
            this.panel4.Controls.Add(this.label11);
            this.panel4.Controls.Add(this.label12);
            this.panel4.Controls.Add(this.t2_tb);
            this.panel4.Controls.Add(this.Y_curpos);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.label10);
            this.panel4.Controls.Add(this.t1_tb);
            this.panel4.Controls.Add(this.X_curpos);
            this.panel4.Controls.Add(this.label6);
            this.panel4.Controls.Add(this.label7);
            this.panel4.Controls.Add(this.label8);
            this.panel4.Location = new System.Drawing.Point(396, 13);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(254, 218);
            this.panel4.TabIndex = 9;
            // 
            // t5_tb
            // 
            this.t5_tb.Location = new System.Drawing.Point(165, 182);
            this.t5_tb.Name = "t5_tb";
            this.t5_tb.Size = new System.Drawing.Size(71, 22);
            this.t5_tb.TabIndex = 25;
            this.t5_tb.Text = "0";
            // 
            // Roll_curpos
            // 
            this.Roll_curpos.Location = new System.Drawing.Point(60, 181);
            this.Roll_curpos.Name = "Roll_curpos";
            this.Roll_curpos.Size = new System.Drawing.Size(71, 22);
            this.Roll_curpos.TabIndex = 24;
            this.Roll_curpos.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label15.Location = new System.Drawing.Point(13, 182);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(35, 19);
            this.label15.TabIndex = 23;
            this.label15.Text = "Roll";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label16.Location = new System.Drawing.Point(137, 185);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(22, 19);
            this.label16.TabIndex = 22;
            this.label16.Text = "t5";
            // 
            // t4_tb
            // 
            this.t4_tb.Location = new System.Drawing.Point(165, 154);
            this.t4_tb.Name = "t4_tb";
            this.t4_tb.Size = new System.Drawing.Size(71, 22);
            this.t4_tb.TabIndex = 21;
            this.t4_tb.Text = "0";
            // 
            // Pitch_curpos
            // 
            this.Pitch_curpos.Location = new System.Drawing.Point(60, 153);
            this.Pitch_curpos.Name = "Pitch_curpos";
            this.Pitch_curpos.Size = new System.Drawing.Size(71, 22);
            this.Pitch_curpos.TabIndex = 20;
            this.Pitch_curpos.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label13.Location = new System.Drawing.Point(13, 154);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(42, 19);
            this.label13.TabIndex = 19;
            this.label13.Text = "Pitch";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label14.Location = new System.Drawing.Point(137, 157);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(22, 19);
            this.label14.TabIndex = 18;
            this.label14.Text = "t4";
            // 
            // t3_tb
            // 
            this.t3_tb.Location = new System.Drawing.Point(165, 126);
            this.t3_tb.Name = "t3_tb";
            this.t3_tb.Size = new System.Drawing.Size(71, 22);
            this.t3_tb.TabIndex = 17;
            this.t3_tb.Text = "0";
            // 
            // Z_curpos
            // 
            this.Z_curpos.Location = new System.Drawing.Point(60, 125);
            this.Z_curpos.Name = "Z_curpos";
            this.Z_curpos.Size = new System.Drawing.Size(71, 22);
            this.Z_curpos.TabIndex = 16;
            this.Z_curpos.Text = "0";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label11.Location = new System.Drawing.Point(13, 126);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(18, 19);
            this.label11.TabIndex = 15;
            this.label11.Text = "Z";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label12.Location = new System.Drawing.Point(137, 129);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(22, 19);
            this.label12.TabIndex = 14;
            this.label12.Text = "t3";
            // 
            // t2_tb
            // 
            this.t2_tb.Location = new System.Drawing.Point(165, 98);
            this.t2_tb.Name = "t2_tb";
            this.t2_tb.Size = new System.Drawing.Size(71, 22);
            this.t2_tb.TabIndex = 13;
            this.t2_tb.Text = "0";
            // 
            // Y_curpos
            // 
            this.Y_curpos.Location = new System.Drawing.Point(60, 97);
            this.Y_curpos.Name = "Y_curpos";
            this.Y_curpos.Size = new System.Drawing.Size(71, 22);
            this.Y_curpos.TabIndex = 12;
            this.Y_curpos.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label9.Location = new System.Drawing.Point(13, 98);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(18, 19);
            this.label9.TabIndex = 11;
            this.label9.Text = "Y";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label10.Location = new System.Drawing.Point(137, 101);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(22, 19);
            this.label10.TabIndex = 10;
            this.label10.Text = "t2";
            // 
            // t1_tb
            // 
            this.t1_tb.Location = new System.Drawing.Point(165, 70);
            this.t1_tb.Name = "t1_tb";
            this.t1_tb.Size = new System.Drawing.Size(71, 22);
            this.t1_tb.TabIndex = 9;
            this.t1_tb.Text = "0";
            // 
            // X_curpos
            // 
            this.X_curpos.Location = new System.Drawing.Point(60, 69);
            this.X_curpos.Name = "X_curpos";
            this.X_curpos.Size = new System.Drawing.Size(71, 22);
            this.X_curpos.TabIndex = 8;
            this.X_curpos.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label6.Location = new System.Drawing.Point(13, 70);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(18, 19);
            this.label6.TabIndex = 7;
            this.label6.Text = "X";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label7.Location = new System.Drawing.Point(137, 73);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(22, 19);
            this.label7.TabIndex = 5;
            this.label7.Text = "t1";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label8.Location = new System.Drawing.Point(-1, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(113, 38);
            this.label8.TabIndex = 1;
            this.label8.Text = "Display";
            // 
            // Timer1
            // 
            this.Timer1.Interval = 200;
            this.Timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.MvCy2_tb);
            this.panel5.Controls.Add(this.label29);
            this.panel5.Controls.Add(this.MvCx2_tb);
            this.panel5.Controls.Add(this.label30);
            this.panel5.Controls.Add(this.MvCy1_tb);
            this.panel5.Controls.Add(this.label26);
            this.panel5.Controls.Add(this.MvCx1_tb);
            this.panel5.Controls.Add(this.label28);
            this.panel5.Controls.Add(this.Tsm_moveC_btn);
            this.panel5.Controls.Add(this.label4);
            this.panel5.Controls.Add(this.spd_tb);
            this.panel5.Controls.Add(this.set_const_speed_btn);
            this.panel5.Controls.Add(this.run_btn);
            this.panel5.Controls.Add(this.Tsm_moveL_btn);
            this.panel5.Controls.Add(this.Tsm_moveJ_btn);
            this.panel5.Controls.Add(this.Mvz_tb);
            this.panel5.Controls.Add(this.label21);
            this.panel5.Controls.Add(this.Mvy_tb);
            this.panel5.Controls.Add(this.label23);
            this.panel5.Controls.Add(this.Mvx_tb);
            this.panel5.Controls.Add(this.label25);
            this.panel5.Controls.Add(this.label27);
            this.panel5.Location = new System.Drawing.Point(12, 237);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(285, 272);
            this.panel5.TabIndex = 26;
            // 
            // MvCy2_tb
            // 
            this.MvCy2_tb.Location = new System.Drawing.Point(136, 155);
            this.MvCy2_tb.Name = "MvCy2_tb";
            this.MvCy2_tb.Size = new System.Drawing.Size(59, 22);
            this.MvCy2_tb.TabIndex = 28;
            this.MvCy2_tb.Text = "0";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label29.Location = new System.Drawing.Point(107, 156);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(26, 19);
            this.label29.TabIndex = 27;
            this.label29.Text = "Y2";
            // 
            // MvCx2_tb
            // 
            this.MvCx2_tb.Location = new System.Drawing.Point(136, 127);
            this.MvCx2_tb.Name = "MvCx2_tb";
            this.MvCx2_tb.Size = new System.Drawing.Size(59, 22);
            this.MvCx2_tb.TabIndex = 26;
            this.MvCx2_tb.Text = "0";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label30.Location = new System.Drawing.Point(107, 128);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(26, 19);
            this.label30.TabIndex = 25;
            this.label30.Text = "X2";
            // 
            // MvCy1_tb
            // 
            this.MvCy1_tb.Location = new System.Drawing.Point(42, 155);
            this.MvCy1_tb.Name = "MvCy1_tb";
            this.MvCy1_tb.Size = new System.Drawing.Size(59, 22);
            this.MvCy1_tb.TabIndex = 24;
            this.MvCy1_tb.Text = "0";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label26.Location = new System.Drawing.Point(13, 156);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(26, 19);
            this.label26.TabIndex = 23;
            this.label26.Text = "Y1";
            // 
            // MvCx1_tb
            // 
            this.MvCx1_tb.Location = new System.Drawing.Point(42, 127);
            this.MvCx1_tb.Name = "MvCx1_tb";
            this.MvCx1_tb.Size = new System.Drawing.Size(59, 22);
            this.MvCx1_tb.TabIndex = 22;
            this.MvCx1_tb.Text = "0";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label28.Location = new System.Drawing.Point(13, 128);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(26, 19);
            this.label28.TabIndex = 21;
            this.label28.Text = "X1";
            // 
            // Tsm_moveC_btn
            // 
            this.Tsm_moveC_btn.Location = new System.Drawing.Point(201, 128);
            this.Tsm_moveC_btn.Name = "Tsm_moveC_btn";
            this.Tsm_moveC_btn.Size = new System.Drawing.Size(74, 49);
            this.Tsm_moveC_btn.TabIndex = 20;
            this.Tsm_moveC_btn.Text = "Transmit MoveC";
            this.Tsm_moveC_btn.UseVisualStyleBackColor = true;
            this.Tsm_moveC_btn.Click += new System.EventHandler(this.Tsm_moveC_btn_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label4.Location = new System.Drawing.Point(13, 187);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 19);
            this.label4.TabIndex = 19;
            this.label4.Text = "ω";
            // 
            // spd_tb
            // 
            this.spd_tb.Location = new System.Drawing.Point(42, 185);
            this.spd_tb.Name = "spd_tb";
            this.spd_tb.Size = new System.Drawing.Size(59, 22);
            this.spd_tb.TabIndex = 9;
            this.spd_tb.Text = "0";
            // 
            // set_const_speed_btn
            // 
            this.set_const_speed_btn.Location = new System.Drawing.Point(129, 183);
            this.set_const_speed_btn.Name = "set_const_speed_btn";
            this.set_const_speed_btn.Size = new System.Drawing.Size(146, 26);
            this.set_const_speed_btn.TabIndex = 9;
            this.set_const_speed_btn.Text = "Set const speed";
            this.set_const_speed_btn.UseVisualStyleBackColor = true;
            this.set_const_speed_btn.Click += new System.EventHandler(this.set_const_speed_btn_Click);
            // 
            // run_btn
            // 
            this.run_btn.Location = new System.Drawing.Point(127, 11);
            this.run_btn.Name = "run_btn";
            this.run_btn.Size = new System.Drawing.Size(117, 26);
            this.run_btn.TabIndex = 18;
            this.run_btn.Text = "Run";
            this.run_btn.UseVisualStyleBackColor = true;
            this.run_btn.Click += new System.EventHandler(this.run_btn_Click);
            // 
            // Tsm_moveL_btn
            // 
            this.Tsm_moveL_btn.Location = new System.Drawing.Point(127, 85);
            this.Tsm_moveL_btn.Name = "Tsm_moveL_btn";
            this.Tsm_moveL_btn.Size = new System.Drawing.Size(148, 26);
            this.Tsm_moveL_btn.TabIndex = 17;
            this.Tsm_moveL_btn.Text = "Transmit MoveL";
            this.Tsm_moveL_btn.UseVisualStyleBackColor = true;
            this.Tsm_moveL_btn.Click += new System.EventHandler(this.Tsm_moveL_btn_Click);
            // 
            // Tsm_moveJ_btn
            // 
            this.Tsm_moveJ_btn.Location = new System.Drawing.Point(127, 53);
            this.Tsm_moveJ_btn.Name = "Tsm_moveJ_btn";
            this.Tsm_moveJ_btn.Size = new System.Drawing.Size(148, 26);
            this.Tsm_moveJ_btn.TabIndex = 8;
            this.Tsm_moveJ_btn.Text = "Transmit MoveJ";
            this.Tsm_moveJ_btn.UseVisualStyleBackColor = true;
            this.Tsm_moveJ_btn.Click += new System.EventHandler(this.Tsm_moveJ_btn_Click);
            // 
            // Mvz_tb
            // 
            this.Mvz_tb.Location = new System.Drawing.Point(42, 97);
            this.Mvz_tb.Name = "Mvz_tb";
            this.Mvz_tb.Size = new System.Drawing.Size(59, 22);
            this.Mvz_tb.TabIndex = 16;
            this.Mvz_tb.Text = "0";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label21.Location = new System.Drawing.Point(13, 98);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(18, 19);
            this.label21.TabIndex = 15;
            this.label21.Text = "Z";
            // 
            // Mvy_tb
            // 
            this.Mvy_tb.Location = new System.Drawing.Point(42, 69);
            this.Mvy_tb.Name = "Mvy_tb";
            this.Mvy_tb.Size = new System.Drawing.Size(59, 22);
            this.Mvy_tb.TabIndex = 12;
            this.Mvy_tb.Text = "0";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label23.Location = new System.Drawing.Point(13, 70);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(18, 19);
            this.label23.TabIndex = 11;
            this.label23.Text = "Y";
            // 
            // Mvx_tb
            // 
            this.Mvx_tb.Location = new System.Drawing.Point(42, 41);
            this.Mvx_tb.Name = "Mvx_tb";
            this.Mvx_tb.Size = new System.Drawing.Size(59, 22);
            this.Mvx_tb.TabIndex = 8;
            this.Mvx_tb.Text = "0";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label25.Location = new System.Drawing.Point(13, 42);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(18, 19);
            this.label25.TabIndex = 7;
            this.label25.Text = "X";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label27.Location = new System.Drawing.Point(-1, 0);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(76, 38);
            this.label27.TabIndex = 1;
            this.label27.Text = "Path";
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.bt_off_matlab);
            this.panel6.Controls.Add(this.bt_on_matlab);
            this.panel6.Controls.Add(this.cBoxParityBits);
            this.panel6.Controls.Add(this.label24);
            this.panel6.Controls.Add(this.cBoxStopBits);
            this.panel6.Controls.Add(this.label22);
            this.panel6.Controls.Add(this.cBoxDataBits);
            this.panel6.Controls.Add(this.cBoxBaudRate);
            this.panel6.Controls.Add(this.cBoxPort);
            this.panel6.Controls.Add(this.progressBar1);
            this.panel6.Controls.Add(this.label18);
            this.panel6.Controls.Add(this.bClose);
            this.panel6.Controls.Add(this.bOpen);
            this.panel6.Controls.Add(this.label19);
            this.panel6.Controls.Add(this.label17);
            this.panel6.Controls.Add(this.label20);
            this.panel6.Location = new System.Drawing.Point(656, 14);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(489, 287);
            this.panel6.TabIndex = 3;
            // 
            // bt_off_matlab
            // 
            this.bt_off_matlab.Location = new System.Drawing.Point(289, 117);
            this.bt_off_matlab.Name = "bt_off_matlab";
            this.bt_off_matlab.Size = new System.Drawing.Size(158, 41);
            this.bt_off_matlab.TabIndex = 38;
            this.bt_off_matlab.Text = "Turn OFF MATLAB";
            this.bt_off_matlab.UseVisualStyleBackColor = true;
            this.bt_off_matlab.Click += new System.EventHandler(this.bt_off_matlab_Click);
            // 
            // bt_on_matlab
            // 
            this.bt_on_matlab.Location = new System.Drawing.Point(289, 50);
            this.bt_on_matlab.Name = "bt_on_matlab";
            this.bt_on_matlab.Size = new System.Drawing.Size(158, 41);
            this.bt_on_matlab.TabIndex = 20;
            this.bt_on_matlab.Text = "Turn ON MATLAB";
            this.bt_on_matlab.UseVisualStyleBackColor = true;
            this.bt_on_matlab.Click += new System.EventHandler(this.bt_on_matlab_Click);
            // 
            // cBoxParityBits
            // 
            this.cBoxParityBits.FormattingEnabled = true;
            this.cBoxParityBits.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even"});
            this.cBoxParityBits.Location = new System.Drawing.Point(102, 181);
            this.cBoxParityBits.Name = "cBoxParityBits";
            this.cBoxParityBits.Size = new System.Drawing.Size(99, 24);
            this.cBoxParityBits.TabIndex = 37;
            this.cBoxParityBits.Text = "None";
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label24.Location = new System.Drawing.Point(5, 184);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(90, 19);
            this.label24.TabIndex = 36;
            this.label24.Text = "PARITY BITS";
            // 
            // cBoxStopBits
            // 
            this.cBoxStopBits.FormattingEnabled = true;
            this.cBoxStopBits.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cBoxStopBits.Location = new System.Drawing.Point(102, 152);
            this.cBoxStopBits.Name = "cBoxStopBits";
            this.cBoxStopBits.Size = new System.Drawing.Size(99, 24);
            this.cBoxStopBits.TabIndex = 35;
            this.cBoxStopBits.Text = "1";
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label22.Location = new System.Drawing.Point(5, 155);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(77, 19);
            this.label22.TabIndex = 34;
            this.label22.Text = "STOP BITS";
            // 
            // cBoxDataBits
            // 
            this.cBoxDataBits.FormattingEnabled = true;
            this.cBoxDataBits.Items.AddRange(new object[] {
            "7",
            "8",
            "9"});
            this.cBoxDataBits.Location = new System.Drawing.Point(102, 123);
            this.cBoxDataBits.Name = "cBoxDataBits";
            this.cBoxDataBits.Size = new System.Drawing.Size(99, 24);
            this.cBoxDataBits.TabIndex = 33;
            this.cBoxDataBits.Text = "8";
            // 
            // cBoxBaudRate
            // 
            this.cBoxBaudRate.FormattingEnabled = true;
            this.cBoxBaudRate.Items.AddRange(new object[] {
            "115200",
            "9600",
            "4800"});
            this.cBoxBaudRate.Location = new System.Drawing.Point(102, 95);
            this.cBoxBaudRate.Name = "cBoxBaudRate";
            this.cBoxBaudRate.Size = new System.Drawing.Size(99, 24);
            this.cBoxBaudRate.TabIndex = 32;
            this.cBoxBaudRate.Text = "9600";
            // 
            // cBoxPort
            // 
            this.cBoxPort.FormattingEnabled = true;
            this.cBoxPort.Location = new System.Drawing.Point(102, 67);
            this.cBoxPort.Name = "cBoxPort";
            this.cBoxPort.Size = new System.Drawing.Size(99, 24);
            this.cBoxPort.TabIndex = 31;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 252);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(236, 23);
            this.progressBar1.TabIndex = 3;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label18.Location = new System.Drawing.Point(5, 126);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(78, 19);
            this.label18.TabIndex = 30;
            this.label18.Text = "DATA BITS";
            // 
            // bClose
            // 
            this.bClose.BackColor = System.Drawing.Color.Red;
            this.bClose.Location = new System.Drawing.Point(137, 220);
            this.bClose.Name = "bClose";
            this.bClose.Size = new System.Drawing.Size(115, 26);
            this.bClose.TabIndex = 2;
            this.bClose.Text = "CLOSE";
            this.bClose.UseVisualStyleBackColor = false;
            this.bClose.Click += new System.EventHandler(this.bClose_Click);
            // 
            // bOpen
            // 
            this.bOpen.BackColor = System.Drawing.Color.Lime;
            this.bOpen.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.bOpen.Location = new System.Drawing.Point(16, 220);
            this.bOpen.Name = "bOpen";
            this.bOpen.Size = new System.Drawing.Size(115, 26);
            this.bOpen.TabIndex = 1;
            this.bOpen.Text = "OPEN";
            this.bOpen.UseVisualStyleBackColor = false;
            this.bOpen.Click += new System.EventHandler(this.bOpen_Click);
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label19.Location = new System.Drawing.Point(5, 98);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(85, 19);
            this.label19.TabIndex = 28;
            this.label19.Text = "BAUD RATE";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold);
            this.label17.Location = new System.Drawing.Point(-1, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(247, 38);
            this.label17.TabIndex = 1;
            this.label17.Text = "Real-time Display";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.label20.Location = new System.Drawing.Point(5, 70);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(83, 19);
            this.label20.TabIndex = 26;
            this.label20.Text = "COM PORT";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1158, 568);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.ErrorLog);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button Connect_button;
        private System.Windows.Forms.Button Disconnect_button;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button GoHome_button;
        private System.Windows.Forms.Button SetHome_button;
        private System.Windows.Forms.Button ResetError_button;
        private System.Windows.Forms.Button OnServo_button;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox jog_speed_tb;
        private System.Windows.Forms.Button Backward_btn;
        private System.Windows.Forms.Button Forward_btn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox joint_tb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button Jog_set_speed;
        private System.Windows.Forms.TextBox ErrorLog;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.TextBox t1_tb;
        private System.Windows.Forms.TextBox X_curpos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox t5_tb;
        private System.Windows.Forms.TextBox Roll_curpos;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox t4_tb;
        private System.Windows.Forms.TextBox Pitch_curpos;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox t3_tb;
        private System.Windows.Forms.TextBox Z_curpos;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox t2_tb;
        private System.Windows.Forms.TextBox Y_curpos;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Timer Timer1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox Mvz_tb;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox Mvy_tb;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox Mvx_tb;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button Tsm_moveJ_btn;
        private System.Windows.Forms.Button Tsm_moveL_btn;
        private System.Windows.Forms.Button run_btn;
        private System.Windows.Forms.TextBox spd_tb;
        private System.Windows.Forms.Button set_const_speed_btn;
        private System.Windows.Forms.Label label4;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button bClose;
        private System.Windows.Forms.Button bOpen;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.ComboBox cBoxBaudRate;
        private System.Windows.Forms.ComboBox cBoxPort;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.ComboBox cBoxParityBits;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.ComboBox cBoxStopBits;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox cBoxDataBits;
        private System.Windows.Forms.Button bt_on_matlab;
        private System.Windows.Forms.Button bt_off_matlab;
        private System.Windows.Forms.Button Tsm_moveC_btn;
        private System.Windows.Forms.TextBox MvCy2_tb;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.TextBox MvCx2_tb;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.TextBox MvCy1_tb;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox MvCx1_tb;
        private System.Windows.Forms.Label label28;
    }
}

