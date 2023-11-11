﻿namespace gui
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            Connect_Panel = new Panel();
            panel1 = new Panel();
            Disconnect_button = new Button();
            Connect_button = new Button();
            PLC_Label = new Label();
            Connect_Label = new Label();
            ErrorLog = new TextBox();
            panel2 = new Panel();
            panel3 = new Panel();
            SetHome_button = new Button();
            OnServo_button = new Button();
            GoHome_button = new Button();
            label1 = new Label();
            ResetError_button = new Button();
            Control_Label = new Label();
            panel4 = new Panel();
            test_button = new Button();
            Run_button = new Button();
            t5_tb = new TextBox();
            label7 = new Label();
            t4_tb = new TextBox();
            label5 = new Label();
            t3_tb = new TextBox();
            label6 = new Label();
            t2_tb = new TextBox();
            label4 = new Label();
            t1_tb = new TextBox();
            label2 = new Label();
            panel10 = new Panel();
            panel11 = new Panel();
            Roll_curpos = new TextBox();
            label21 = new Label();
            Pitch_curpos = new TextBox();
            label22 = new Label();
            Z_curpos = new TextBox();
            label23 = new Label();
            Y_curpos = new TextBox();
            label24 = new Label();
            X_curpos = new TextBox();
            label25 = new Label();
            label30 = new Label();
            panel6 = new Panel();
            panel7 = new Panel();
            Trasmit_1 = new Button();
            Test_run = new Button();
            Speed_button = new Button();
            label14 = new Label();
            Speed_tb = new TextBox();
            label3 = new Label();
            Z2_tb = new TextBox();
            label9 = new Label();
            Y2_tb = new TextBox();
            label10 = new Label();
            X2_tb = new TextBox();
            pRu_button = new Button();
            Transmit_button = new Button();
            label12 = new Label();
            Z_tb = new TextBox();
            label8 = new Label();
            Y_tb = new TextBox();
            label11 = new Label();
            X_tb = new TextBox();
            label13 = new Label();
            Timer1 = new System.Windows.Forms.Timer(components);
            Connect_Panel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // Connect_Panel
            // 
            Connect_Panel.BorderStyle = BorderStyle.FixedSingle;
            Connect_Panel.Controls.Add(panel1);
            Connect_Panel.Controls.Add(Connect_Label);
            Connect_Panel.Location = new Point(11, 12);
            Connect_Panel.Name = "Connect_Panel";
            Connect_Panel.Size = new Size(152, 181);
            Connect_Panel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(Disconnect_button);
            panel1.Controls.Add(Connect_button);
            panel1.Controls.Add(PLC_Label);
            panel1.Location = new Point(9, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(135, 113);
            panel1.TabIndex = 1;
            // 
            // Disconnect_button
            // 
            Disconnect_button.BackColor = Color.Red;
            Disconnect_button.Location = new Point(11, 75);
            Disconnect_button.Name = "Disconnect_button";
            Disconnect_button.Size = new Size(113, 29);
            Disconnect_button.TabIndex = 4;
            Disconnect_button.Text = "Disconnect";
            Disconnect_button.UseVisualStyleBackColor = false;
            Disconnect_button.Click += Disconnect_button_Click;
            // 
            // Connect_button
            // 
            Connect_button.Location = new Point(11, 40);
            Connect_button.Name = "Connect_button";
            Connect_button.Size = new Size(113, 29);
            Connect_button.TabIndex = 3;
            Connect_button.Text = "Connect";
            Connect_button.UseVisualStyleBackColor = true;
            Connect_button.Click += Connect_button_Click;
            // 
            // PLC_Label
            // 
            PLC_Label.AutoSize = true;
            PLC_Label.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            PLC_Label.Location = new Point(3, 9);
            PLC_Label.Name = "PLC_Label";
            PLC_Label.Size = new Size(46, 28);
            PLC_Label.TabIndex = 2;
            PLC_Label.Text = "PLC";
            // 
            // Connect_Label
            // 
            Connect_Label.AutoSize = true;
            Connect_Label.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            Connect_Label.Location = new Point(3, 9);
            Connect_Label.Name = "Connect_Label";
            Connect_Label.Size = new Size(124, 38);
            Connect_Label.TabIndex = 0;
            Connect_Label.Text = "Connect";
            // 
            // ErrorLog
            // 
            ErrorLog.BorderStyle = BorderStyle.FixedSingle;
            ErrorLog.Location = new Point(492, 199);
            ErrorLog.Multiline = true;
            ErrorLog.Name = "ErrorLog";
            ErrorLog.ReadOnly = true;
            ErrorLog.Size = new Size(528, 256);
            ErrorLog.TabIndex = 73;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(Control_Label);
            panel2.Location = new Point(170, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(249, 181);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(SetHome_button);
            panel3.Controls.Add(OnServo_button);
            panel3.Controls.Add(GoHome_button);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(ResetError_button);
            panel3.Location = new Point(3, 49);
            panel3.Name = "panel3";
            panel3.Size = new Size(237, 113);
            panel3.TabIndex = 5;
            // 
            // SetHome_button
            // 
            SetHome_button.Location = new Point(122, 40);
            SetHome_button.Name = "SetHome_button";
            SetHome_button.Size = new Size(113, 29);
            SetHome_button.TabIndex = 11;
            SetHome_button.Text = "Set Home";
            SetHome_button.UseVisualStyleBackColor = true;
            SetHome_button.Click += SetHome_button_Click;
            // 
            // OnServo_button
            // 
            OnServo_button.Location = new Point(3, 40);
            OnServo_button.Name = "OnServo_button";
            OnServo_button.Size = new Size(113, 29);
            OnServo_button.TabIndex = 10;
            OnServo_button.Text = "Servo";
            OnServo_button.UseVisualStyleBackColor = true;
            OnServo_button.Click += OnServo_button_Click;
            // 
            // GoHome_button
            // 
            GoHome_button.Location = new Point(122, 75);
            GoHome_button.Name = "GoHome_button";
            GoHome_button.Size = new Size(113, 29);
            GoHome_button.TabIndex = 8;
            GoHome_button.Text = "Go Home";
            GoHome_button.UseVisualStyleBackColor = true;
            GoHome_button.Click += GoHome_button_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(3, 9);
            label1.Name = "label1";
            label1.Size = new Size(83, 28);
            label1.TabIndex = 2;
            label1.Text = "Default";
            // 
            // ResetError_button
            // 
            ResetError_button.Location = new Point(3, 75);
            ResetError_button.Name = "ResetError_button";
            ResetError_button.Size = new Size(113, 29);
            ResetError_button.TabIndex = 7;
            ResetError_button.Text = "Reset Error";
            ResetError_button.UseVisualStyleBackColor = true;
            ResetError_button.Click += ResetError_button_Click;
            // 
            // Control_Label
            // 
            Control_Label.AutoSize = true;
            Control_Label.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            Control_Label.Location = new Point(3, 9);
            Control_Label.Name = "Control_Label";
            Control_Label.Size = new Size(115, 38);
            Control_Label.TabIndex = 0;
            Control_Label.Text = "Control";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(test_button);
            panel4.Location = new Point(1041, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(314, 269);
            panel4.TabIndex = 2;
            // 
            // test_button
            // 
            test_button.Location = new Point(3, 9);
            test_button.Name = "test_button";
            test_button.Size = new Size(113, 29);
            test_button.TabIndex = 85;
            test_button.Text = "Exe";
            test_button.UseVisualStyleBackColor = true;
            test_button.Click += test_button_Click;
            // 
            // Run_button
            // 
            Run_button.Location = new Point(451, 51);
            Run_button.Name = "Run_button";
            Run_button.Size = new Size(113, 29);
            Run_button.TabIndex = 74;
            Run_button.Text = "Run";
            Run_button.UseVisualStyleBackColor = true;
            Run_button.Click += Run_button_Click;
            // 
            // t5_tb
            // 
            t5_tb.Location = new Point(154, 168);
            t5_tb.Multiline = true;
            t5_tb.Name = "t5_tb";
            t5_tb.Size = new Size(79, 23);
            t5_tb.TabIndex = 82;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(126, 171);
            label7.Name = "label7";
            label7.Size = new Size(22, 19);
            label7.TabIndex = 81;
            label7.Text = "t5";
            // 
            // t4_tb
            // 
            t4_tb.Location = new Point(154, 132);
            t4_tb.Multiline = true;
            t4_tb.Name = "t4_tb";
            t4_tb.Size = new Size(79, 23);
            t4_tb.TabIndex = 80;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(126, 135);
            label5.Name = "label5";
            label5.Size = new Size(22, 19);
            label5.TabIndex = 79;
            label5.Text = "t4";
            // 
            // t3_tb
            // 
            t3_tb.Location = new Point(154, 95);
            t3_tb.Multiline = true;
            t3_tb.Name = "t3_tb";
            t3_tb.Size = new Size(79, 23);
            t3_tb.TabIndex = 78;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(126, 96);
            label6.Name = "label6";
            label6.Size = new Size(22, 19);
            label6.TabIndex = 77;
            label6.Text = "t3";
            // 
            // t2_tb
            // 
            t2_tb.Location = new Point(154, 57);
            t2_tb.Multiline = true;
            t2_tb.Name = "t2_tb";
            t2_tb.Size = new Size(79, 23);
            t2_tb.TabIndex = 76;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(126, 60);
            label4.Name = "label4";
            label4.Size = new Size(22, 19);
            label4.TabIndex = 75;
            label4.Text = "t2";
            // 
            // t1_tb
            // 
            t1_tb.Location = new Point(154, 19);
            t1_tb.Multiline = true;
            t1_tb.Name = "t1_tb";
            t1_tb.Size = new Size(79, 23);
            t1_tb.TabIndex = 74;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(126, 20);
            label2.Name = "label2";
            label2.Size = new Size(22, 19);
            label2.TabIndex = 5;
            label2.Text = "t1";
            // 
            // panel10
            // 
            panel10.BorderStyle = BorderStyle.FixedSingle;
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(label30);
            panel10.Location = new Point(11, 199);
            panel10.Name = "panel10";
            panel10.Size = new Size(475, 256);
            panel10.TabIndex = 82;
            // 
            // panel11
            // 
            panel11.BorderStyle = BorderStyle.FixedSingle;
            panel11.Controls.Add(t5_tb);
            panel11.Controls.Add(Roll_curpos);
            panel11.Controls.Add(label7);
            panel11.Controls.Add(label21);
            panel11.Controls.Add(t4_tb);
            panel11.Controls.Add(Pitch_curpos);
            panel11.Controls.Add(label5);
            panel11.Controls.Add(label22);
            panel11.Controls.Add(t3_tb);
            panel11.Controls.Add(Z_curpos);
            panel11.Controls.Add(label6);
            panel11.Controls.Add(label23);
            panel11.Controls.Add(t2_tb);
            panel11.Controls.Add(Y_curpos);
            panel11.Controls.Add(label4);
            panel11.Controls.Add(label24);
            panel11.Controls.Add(t1_tb);
            panel11.Controls.Add(label2);
            panel11.Controls.Add(X_curpos);
            panel11.Controls.Add(label25);
            panel11.Location = new Point(9, 49);
            panel11.Name = "panel11";
            panel11.Size = new Size(256, 202);
            panel11.TabIndex = 81;
            // 
            // Roll_curpos
            // 
            Roll_curpos.Location = new Point(31, 168);
            Roll_curpos.Multiline = true;
            Roll_curpos.Name = "Roll_curpos";
            Roll_curpos.ReadOnly = true;
            Roll_curpos.Size = new Size(79, 23);
            Roll_curpos.TabIndex = 82;
            Roll_curpos.Text = "0";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label21.Location = new Point(5, 171);
            label21.Name = "label21";
            label21.Size = new Size(18, 19);
            label21.TabIndex = 81;
            label21.Text = "R";
            // 
            // Pitch_curpos
            // 
            Pitch_curpos.Location = new Point(31, 132);
            Pitch_curpos.Multiline = true;
            Pitch_curpos.Name = "Pitch_curpos";
            Pitch_curpos.ReadOnly = true;
            Pitch_curpos.Size = new Size(79, 23);
            Pitch_curpos.TabIndex = 80;
            Pitch_curpos.Text = "0";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label22.Location = new Point(3, 132);
            label22.Name = "label22";
            label22.Size = new Size(20, 19);
            label22.TabIndex = 79;
            label22.Text = "φ";
            // 
            // Z_curpos
            // 
            Z_curpos.Location = new Point(31, 95);
            Z_curpos.Multiline = true;
            Z_curpos.Name = "Z_curpos";
            Z_curpos.ReadOnly = true;
            Z_curpos.Size = new Size(79, 23);
            Z_curpos.TabIndex = 78;
            Z_curpos.Text = "0";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label23.Location = new Point(3, 96);
            label23.Name = "label23";
            label23.Size = new Size(18, 19);
            label23.TabIndex = 77;
            label23.Text = "Z";
            // 
            // Y_curpos
            // 
            Y_curpos.Location = new Point(31, 57);
            Y_curpos.Multiline = true;
            Y_curpos.Name = "Y_curpos";
            Y_curpos.ReadOnly = true;
            Y_curpos.Size = new Size(79, 23);
            Y_curpos.TabIndex = 76;
            Y_curpos.Text = "0";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label24.Location = new Point(3, 60);
            label24.Name = "label24";
            label24.Size = new Size(18, 19);
            label24.TabIndex = 75;
            label24.Text = "Y";
            // 
            // X_curpos
            // 
            X_curpos.Location = new Point(31, 19);
            X_curpos.Multiline = true;
            X_curpos.Name = "X_curpos";
            X_curpos.ReadOnly = true;
            X_curpos.Size = new Size(79, 23);
            X_curpos.TabIndex = 74;
            X_curpos.Text = "0";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label25.Location = new Point(3, 21);
            label25.Name = "label25";
            label25.Size = new Size(18, 19);
            label25.TabIndex = 5;
            label25.Text = "X";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label30.Location = new Point(3, 9);
            label30.Name = "label30";
            label30.Size = new Size(152, 38);
            label30.TabIndex = 0;
            label30.Text = "Parameter";
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(panel7);
            panel6.Controls.Add(label13);
            panel6.Location = new Point(425, 12);
            panel6.Name = "panel6";
            panel6.Size = new Size(595, 181);
            panel6.TabIndex = 75;
            // 
            // panel7
            // 
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel7.Controls.Add(Trasmit_1);
            panel7.Controls.Add(Test_run);
            panel7.Controls.Add(Speed_button);
            panel7.Controls.Add(label14);
            panel7.Controls.Add(Speed_tb);
            panel7.Controls.Add(label3);
            panel7.Controls.Add(Z2_tb);
            panel7.Controls.Add(label9);
            panel7.Controls.Add(Y2_tb);
            panel7.Controls.Add(label10);
            panel7.Controls.Add(X2_tb);
            panel7.Controls.Add(pRu_button);
            panel7.Controls.Add(Transmit_button);
            panel7.Controls.Add(Run_button);
            panel7.Controls.Add(label12);
            panel7.Controls.Add(Z_tb);
            panel7.Controls.Add(label8);
            panel7.Controls.Add(Y_tb);
            panel7.Controls.Add(label11);
            panel7.Controls.Add(X_tb);
            panel7.Location = new Point(11, 49);
            panel7.Name = "panel7";
            panel7.Size = new Size(579, 127);
            panel7.TabIndex = 1;
            // 
            // Trasmit_1
            // 
            Trasmit_1.Location = new Point(334, 18);
            Trasmit_1.Name = "Trasmit_1";
            Trasmit_1.Size = new Size(113, 29);
            Trasmit_1.TabIndex = 95;
            Trasmit_1.Text = "Transmit_pos2";
            Trasmit_1.UseVisualStyleBackColor = true;
            Trasmit_1.Click += Trasmit_1_Click;
            // 
            // Test_run
            // 
            Test_run.Location = new Point(215, 79);
            Test_run.Name = "Test_run";
            Test_run.Size = new Size(113, 29);
            Test_run.TabIndex = 94;
            Test_run.Text = "Run_2_pos";
            Test_run.UseVisualStyleBackColor = true;
            Test_run.Click += Test_run_Click;
            // 
            // Speed_button
            // 
            Speed_button.Location = new Point(215, 47);
            Speed_button.Name = "Speed_button";
            Speed_button.Size = new Size(113, 29);
            Speed_button.TabIndex = 93;
            Speed_button.Text = "Speed";
            Speed_button.UseVisualStyleBackColor = true;
            Speed_button.Click += Speed_button_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(209, 20);
            label14.Name = "label14";
            label14.Size = new Size(51, 19);
            label14.TabIndex = 92;
            label14.Text = "Speed";
            // 
            // Speed_tb
            // 
            Speed_tb.Location = new Point(266, 19);
            Speed_tb.Multiline = true;
            Speed_tb.Name = "Speed_tb";
            Speed_tb.Size = new Size(62, 24);
            Speed_tb.TabIndex = 91;
            Speed_tb.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(115, 20);
            label3.Name = "label3";
            label3.Size = new Size(26, 19);
            label3.TabIndex = 90;
            label3.Text = "X2";
            // 
            // Z2_tb
            // 
            Z2_tb.Location = new Point(141, 79);
            Z2_tb.Multiline = true;
            Z2_tb.Name = "Z2_tb";
            Z2_tb.Size = new Size(62, 24);
            Z2_tb.TabIndex = 89;
            Z2_tb.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(115, 83);
            label9.Name = "label9";
            label9.Size = new Size(26, 19);
            label9.TabIndex = 88;
            label9.Text = "Z2";
            // 
            // Y2_tb
            // 
            Y2_tb.Location = new Point(141, 48);
            Y2_tb.Multiline = true;
            Y2_tb.Name = "Y2_tb";
            Y2_tb.Size = new Size(62, 24);
            Y2_tb.TabIndex = 87;
            Y2_tb.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(115, 51);
            label10.Name = "label10";
            label10.Size = new Size(26, 19);
            label10.TabIndex = 86;
            label10.Text = "Y2";
            // 
            // X2_tb
            // 
            X2_tb.Location = new Point(141, 19);
            X2_tb.Multiline = true;
            X2_tb.Name = "X2_tb";
            X2_tb.Size = new Size(62, 24);
            X2_tb.TabIndex = 85;
            X2_tb.Text = "0";
            // 
            // pRu_button
            // 
            pRu_button.Location = new Point(451, 83);
            pRu_button.Name = "pRu_button";
            pRu_button.Size = new Size(113, 29);
            pRu_button.TabIndex = 84;
            pRu_button.Text = "Path Run";
            pRu_button.UseVisualStyleBackColor = true;
            pRu_button.Click += pRu_button_Click;
            // 
            // Transmit_button
            // 
            Transmit_button.Location = new Point(451, 18);
            Transmit_button.Name = "Transmit_button";
            Transmit_button.Size = new Size(113, 29);
            Transmit_button.TabIndex = 75;
            Transmit_button.Text = "Transmit";
            Transmit_button.UseVisualStyleBackColor = true;
            Transmit_button.Click += Transmit_button_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(5, 20);
            label12.Name = "label12";
            label12.Size = new Size(26, 19);
            label12.TabIndex = 83;
            label12.Text = "X1";
            // 
            // Z_tb
            // 
            Z_tb.Location = new Point(31, 79);
            Z_tb.Multiline = true;
            Z_tb.Name = "Z_tb";
            Z_tb.Size = new Size(62, 24);
            Z_tb.TabIndex = 82;
            Z_tb.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(5, 83);
            label8.Name = "label8";
            label8.Size = new Size(26, 19);
            label8.TabIndex = 81;
            label8.Text = "Z1";
            // 
            // Y_tb
            // 
            Y_tb.Location = new Point(31, 48);
            Y_tb.Multiline = true;
            Y_tb.Name = "Y_tb";
            Y_tb.Size = new Size(62, 24);
            Y_tb.TabIndex = 76;
            Y_tb.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(5, 51);
            label11.Name = "label11";
            label11.Size = new Size(26, 19);
            label11.TabIndex = 75;
            label11.Text = "Y1";
            // 
            // X_tb
            // 
            X_tb.Location = new Point(31, 19);
            X_tb.Multiline = true;
            X_tb.Name = "X_tb";
            X_tb.Size = new Size(62, 24);
            X_tb.TabIndex = 74;
            X_tb.Text = "0";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(3, 9);
            label13.Name = "label13";
            label13.Size = new Size(199, 38);
            label13.TabIndex = 0;
            label13.Text = "Point to Point";
            // 
            // Timer1
            // 
            Timer1.Interval = 250;
            Timer1.Tick += Timer1_Tick;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1367, 565);
            Controls.Add(panel6);
            Controls.Add(panel10);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(ErrorLog);
            Controls.Add(Connect_Panel);
            Name = "Form1";
            Text = "Main";
            Connect_Panel.ResumeLayout(false);
            Connect_Panel.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel4.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel Connect_Panel;
        private Label Connect_Label;
        private Panel panel1;
        private Button Connect_button;
        private Label PLC_Label;
        private Button Disconnect_button;
        private TextBox ErrorLog;
        private Panel panel2;
        private Label Control_Label;
        private Button ResetError_button;
        private Panel panel3;
        private Button GoHome_button;
        private Label label1;
        private Button OnServo_button;
        private Button SetHome_button;
        private Panel panel4;
        private Label label2;
        private TextBox t4_tb;
        private Label label5;
        private TextBox t3_tb;
        private Label label6;
        private TextBox t2_tb;
        private Label label4;
        private TextBox t1_tb;
        private Button Run_button;
        private Panel panel10;
        private Panel panel11;
        private TextBox Roll_curpos;
        private Label label21;
        private TextBox Pitch_curpos;
        private Label label22;
        private TextBox Z_curpos;
        private Label label23;
        private TextBox Y_curpos;
        private Label label24;
        private TextBox X_curpos;
        private Label label25;
        private Label label30;
        private TextBox t5_tb;
        private Label label7;
        private Panel panel6;
        private Panel panel7;
        private TextBox Z_tb;
        private Label label8;
        private TextBox Y_tb;
        private Label label11;
        private TextBox X_tb;
        private Label label13;
        private Label label12;
        private Button Transmit_button;
        private System.Windows.Forms.Timer Timer1;
        private Button pRu_button;
        private Button test_button;
        private Label label3;
        private TextBox Z2_tb;
        private Label label9;
        private TextBox Y2_tb;
        private Label label10;
        private TextBox X2_tb;
        private Label label14;
        private TextBox Speed_tb;
        private Button Speed_button;
        private Button Test_run;
        private Button Trasmit_1;
    }
}