namespace gui
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
            Connect_Label = new Label();
            ErrorLog = new TextBox();
            panel2 = new Panel();
            panel3 = new Panel();
            SetHome_button = new Button();
            OnServo_button = new Button();
            GoHome_button = new Button();
            ResetError_button = new Button();
            Control_Label = new Label();
            panel4 = new Panel();
            Cvt_Trajectory = new Button();
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
            label14 = new Label();
            Speed_tb = new TextBox();
            label3 = new Label();
            Z2_tb = new TextBox();
            label9 = new Label();
            Y2_tb = new TextBox();
            label10 = new Label();
            Transmit_button = new Button();
            X2_tb = new TextBox();
            pRu_button = new Button();
            label12 = new Label();
            Z_tb = new TextBox();
            label8 = new Label();
            Y_tb = new TextBox();
            label11 = new Label();
            X_tb = new TextBox();
            label13 = new Label();
            Speed_button = new Button();
            Timer1 = new System.Windows.Forms.Timer(components);
            textBox1 = new TextBox();
            label15 = new Label();
            Jog1B = new Button();
            Jog2B = new Button();
            Jog2F = new Button();
            Jog3B = new Button();
            Jog3F = new Button();
            Jog4B = new Button();
            Jog4F = new Button();
            Jog5B = new Button();
            Jog5F = new Button();
            label17 = new Label();
            label18 = new Label();
            label19 = new Label();
            label20 = new Label();
            setvel_5 = new TextBox();
            setvel_4 = new TextBox();
            setvel_3 = new TextBox();
            setvel_2 = new TextBox();
            setvel_1 = new TextBox();
            label16 = new Label();
            Jog1F = new Button();
            panel5 = new Panel();
            set_speed_btn = new Button();
            panel8 = new Panel();
            Tsm_moveC_btn = new Button();
            MvCz_tb = new TextBox();
            label36 = new Label();
            label37 = new Label();
            label38 = new Label();
            MvCy_tb = new TextBox();
            label39 = new Label();
            MvCx_tb = new TextBox();
            Tsm_moveL_btn = new Button();
            MvLz_tb = new TextBox();
            label27 = new Label();
            label28 = new Label();
            label29 = new Label();
            MvLy_tb = new TextBox();
            label35 = new Label();
            MvLx_tb = new TextBox();
            Tsm_moveJ_btn = new Button();
            button1 = new Button();
            MvJz_tb = new TextBox();
            label26 = new Label();
            label31 = new Label();
            label32 = new Label();
            MvJy_tb = new TextBox();
            label33 = new Label();
            MvJx_tb = new TextBox();
            label1 = new Label();
            spd_tb = new TextBox();
            label34 = new Label();
            Connect_Panel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            panel5.SuspendLayout();
            panel8.SuspendLayout();
            SuspendLayout();
            // 
            // Connect_Panel
            // 
            Connect_Panel.BorderStyle = BorderStyle.FixedSingle;
            Connect_Panel.Controls.Add(panel1);
            Connect_Panel.Controls.Add(Connect_Label);
            Connect_Panel.Location = new Point(10, 9);
            Connect_Panel.Margin = new Padding(3, 2, 3, 2);
            Connect_Panel.Name = "Connect_Panel";
            Connect_Panel.Size = new Size(133, 136);
            Connect_Panel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(Disconnect_button);
            panel1.Controls.Add(Connect_button);
            panel1.Location = new Point(8, 37);
            panel1.Margin = new Padding(3, 2, 3, 2);
            panel1.Name = "panel1";
            panel1.Size = new Size(118, 85);
            panel1.TabIndex = 1;
            // 
            // Disconnect_button
            // 
            Disconnect_button.BackColor = Color.Red;
            Disconnect_button.Location = new Point(10, 56);
            Disconnect_button.Margin = new Padding(3, 2, 3, 2);
            Disconnect_button.Name = "Disconnect_button";
            Disconnect_button.Size = new Size(99, 22);
            Disconnect_button.TabIndex = 4;
            Disconnect_button.Text = "Disconnect";
            Disconnect_button.UseVisualStyleBackColor = false;
            Disconnect_button.Click += Disconnect_button_Click;
            // 
            // Connect_button
            // 
            Connect_button.Location = new Point(10, 30);
            Connect_button.Margin = new Padding(3, 2, 3, 2);
            Connect_button.Name = "Connect_button";
            Connect_button.Size = new Size(99, 22);
            Connect_button.TabIndex = 3;
            Connect_button.Text = "Connect";
            Connect_button.UseVisualStyleBackColor = true;
            Connect_button.Click += Connect_button_Click;
            // 
            // Connect_Label
            // 
            Connect_Label.AutoSize = true;
            Connect_Label.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            Connect_Label.Location = new Point(3, 7);
            Connect_Label.Name = "Connect_Label";
            Connect_Label.Size = new Size(98, 30);
            Connect_Label.TabIndex = 0;
            Connect_Label.Text = "Connect";
            // 
            // ErrorLog
            // 
            ErrorLog.BorderStyle = BorderStyle.FixedSingle;
            ErrorLog.Location = new Point(616, 9);
            ErrorLog.Margin = new Padding(3, 2, 3, 2);
            ErrorLog.Multiline = true;
            ErrorLog.Name = "ErrorLog";
            ErrorLog.ReadOnly = true;
            ErrorLog.Size = new Size(320, 205);
            ErrorLog.TabIndex = 73;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(Control_Label);
            panel2.Location = new Point(149, 9);
            panel2.Margin = new Padding(3, 2, 3, 2);
            panel2.Name = "panel2";
            panel2.Size = new Size(218, 136);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.BorderStyle = BorderStyle.FixedSingle;
            panel3.Controls.Add(SetHome_button);
            panel3.Controls.Add(OnServo_button);
            panel3.Controls.Add(GoHome_button);
            panel3.Controls.Add(ResetError_button);
            panel3.Location = new Point(3, 37);
            panel3.Margin = new Padding(3, 2, 3, 2);
            panel3.Name = "panel3";
            panel3.Size = new Size(208, 85);
            panel3.TabIndex = 5;
            panel3.Paint += panel3_Paint;
            // 
            // SetHome_button
            // 
            SetHome_button.Location = new Point(107, 30);
            SetHome_button.Margin = new Padding(3, 2, 3, 2);
            SetHome_button.Name = "SetHome_button";
            SetHome_button.Size = new Size(99, 22);
            SetHome_button.TabIndex = 11;
            SetHome_button.Text = "Set Home";
            SetHome_button.UseVisualStyleBackColor = true;
            SetHome_button.Click += SetHome_button_Click;
            // 
            // OnServo_button
            // 
            OnServo_button.Location = new Point(3, 30);
            OnServo_button.Margin = new Padding(3, 2, 3, 2);
            OnServo_button.Name = "OnServo_button";
            OnServo_button.Size = new Size(99, 22);
            OnServo_button.TabIndex = 10;
            OnServo_button.Text = "Servo";
            OnServo_button.UseVisualStyleBackColor = true;
            OnServo_button.Click += OnServo_button_Click;
            // 
            // GoHome_button
            // 
            GoHome_button.Location = new Point(107, 56);
            GoHome_button.Margin = new Padding(3, 2, 3, 2);
            GoHome_button.Name = "GoHome_button";
            GoHome_button.Size = new Size(99, 22);
            GoHome_button.TabIndex = 8;
            GoHome_button.Text = "Go Home";
            GoHome_button.UseVisualStyleBackColor = true;
            GoHome_button.Click += GoHome_button_Click;
            // 
            // ResetError_button
            // 
            ResetError_button.Location = new Point(3, 56);
            ResetError_button.Margin = new Padding(3, 2, 3, 2);
            ResetError_button.Name = "ResetError_button";
            ResetError_button.Size = new Size(99, 22);
            ResetError_button.TabIndex = 7;
            ResetError_button.Text = "Reset Error";
            ResetError_button.UseVisualStyleBackColor = true;
            ResetError_button.Click += ResetError_button_Click;
            // 
            // Control_Label
            // 
            Control_Label.AutoSize = true;
            Control_Label.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            Control_Label.Location = new Point(3, 7);
            Control_Label.Name = "Control_Label";
            Control_Label.Size = new Size(90, 30);
            Control_Label.TabIndex = 0;
            Control_Label.Text = "Control";
            // 
            // panel4
            // 
            panel4.BorderStyle = BorderStyle.FixedSingle;
            panel4.Controls.Add(Cvt_Trajectory);
            panel4.Controls.Add(test_button);
            panel4.Location = new Point(10, 364);
            panel4.Margin = new Padding(3, 2, 3, 2);
            panel4.Name = "panel4";
            panel4.Size = new Size(288, 92);
            panel4.TabIndex = 2;
            // 
            // Cvt_Trajectory
            // 
            Cvt_Trajectory.Location = new Point(3, 33);
            Cvt_Trajectory.Margin = new Padding(3, 2, 3, 2);
            Cvt_Trajectory.Name = "Cvt_Trajectory";
            Cvt_Trajectory.Size = new Size(153, 22);
            Cvt_Trajectory.TabIndex = 86;
            Cvt_Trajectory.Text = "Trajectory_Convert";
            Cvt_Trajectory.UseVisualStyleBackColor = true;
            Cvt_Trajectory.Click += Cvt_Trajectory_Click;
            // 
            // test_button
            // 
            test_button.Location = new Point(3, 7);
            test_button.Margin = new Padding(3, 2, 3, 2);
            test_button.Name = "test_button";
            test_button.Size = new Size(153, 22);
            test_button.TabIndex = 85;
            test_button.Text = "Trajectory_Planning";
            test_button.UseVisualStyleBackColor = true;
            test_button.Click += test_button_Click;
            // 
            // Run_button
            // 
            Run_button.Location = new Point(395, 38);
            Run_button.Margin = new Padding(3, 2, 3, 2);
            Run_button.Name = "Run_button";
            Run_button.Size = new Size(99, 22);
            Run_button.TabIndex = 74;
            Run_button.Text = "Run";
            Run_button.UseVisualStyleBackColor = true;
            Run_button.Click += Run_button_Click;
            // 
            // t5_tb
            // 
            t5_tb.Location = new Point(135, 126);
            t5_tb.Margin = new Padding(3, 2, 3, 2);
            t5_tb.Multiline = true;
            t5_tb.Name = "t5_tb";
            t5_tb.Size = new Size(70, 18);
            t5_tb.TabIndex = 82;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(110, 128);
            label7.Name = "label7";
            label7.Size = new Size(17, 13);
            label7.TabIndex = 81;
            label7.Text = "t5";
            // 
            // t4_tb
            // 
            t4_tb.Location = new Point(135, 99);
            t4_tb.Margin = new Padding(3, 2, 3, 2);
            t4_tb.Multiline = true;
            t4_tb.Name = "t4_tb";
            t4_tb.Size = new Size(70, 18);
            t4_tb.TabIndex = 80;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(110, 101);
            label5.Name = "label5";
            label5.Size = new Size(17, 13);
            label5.TabIndex = 79;
            label5.Text = "t4";
            // 
            // t3_tb
            // 
            t3_tb.Location = new Point(135, 71);
            t3_tb.Margin = new Padding(3, 2, 3, 2);
            t3_tb.Multiline = true;
            t3_tb.Name = "t3_tb";
            t3_tb.Size = new Size(70, 18);
            t3_tb.TabIndex = 78;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(110, 72);
            label6.Name = "label6";
            label6.Size = new Size(17, 13);
            label6.TabIndex = 77;
            label6.Text = "t3";
            // 
            // t2_tb
            // 
            t2_tb.Location = new Point(135, 43);
            t2_tb.Margin = new Padding(3, 2, 3, 2);
            t2_tb.Multiline = true;
            t2_tb.Name = "t2_tb";
            t2_tb.Size = new Size(70, 18);
            t2_tb.TabIndex = 76;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(110, 45);
            label4.Name = "label4";
            label4.Size = new Size(17, 13);
            label4.TabIndex = 75;
            label4.Text = "t2";
            // 
            // t1_tb
            // 
            t1_tb.Location = new Point(135, 14);
            t1_tb.Margin = new Padding(3, 2, 3, 2);
            t1_tb.Multiline = true;
            t1_tb.Name = "t1_tb";
            t1_tb.Size = new Size(70, 18);
            t1_tb.TabIndex = 74;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(110, 15);
            label2.Name = "label2";
            label2.Size = new Size(17, 13);
            label2.TabIndex = 5;
            label2.Text = "t1";
            // 
            // panel10
            // 
            panel10.BorderStyle = BorderStyle.FixedSingle;
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(label30);
            panel10.Location = new Point(372, 9);
            panel10.Margin = new Padding(3, 2, 3, 2);
            panel10.Name = "panel10";
            panel10.Size = new Size(237, 205);
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
            panel11.Location = new Point(8, 37);
            panel11.Margin = new Padding(3, 2, 3, 2);
            panel11.Name = "panel11";
            panel11.Size = new Size(224, 152);
            panel11.TabIndex = 81;
            // 
            // Roll_curpos
            // 
            Roll_curpos.Location = new Point(27, 126);
            Roll_curpos.Margin = new Padding(3, 2, 3, 2);
            Roll_curpos.Multiline = true;
            Roll_curpos.Name = "Roll_curpos";
            Roll_curpos.ReadOnly = true;
            Roll_curpos.Size = new Size(70, 18);
            Roll_curpos.TabIndex = 82;
            Roll_curpos.Text = "0";
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label21.Location = new Point(4, 128);
            label21.Name = "label21";
            label21.Size = new Size(14, 13);
            label21.TabIndex = 81;
            label21.Text = "R";
            // 
            // Pitch_curpos
            // 
            Pitch_curpos.Location = new Point(27, 99);
            Pitch_curpos.Margin = new Padding(3, 2, 3, 2);
            Pitch_curpos.Multiline = true;
            Pitch_curpos.Name = "Pitch_curpos";
            Pitch_curpos.ReadOnly = true;
            Pitch_curpos.Size = new Size(70, 18);
            Pitch_curpos.TabIndex = 80;
            Pitch_curpos.Text = "0";
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label22.Location = new Point(3, 99);
            label22.Name = "label22";
            label22.Size = new Size(15, 13);
            label22.TabIndex = 79;
            label22.Text = "φ";
            // 
            // Z_curpos
            // 
            Z_curpos.Location = new Point(27, 71);
            Z_curpos.Margin = new Padding(3, 2, 3, 2);
            Z_curpos.Multiline = true;
            Z_curpos.Name = "Z_curpos";
            Z_curpos.ReadOnly = true;
            Z_curpos.Size = new Size(70, 18);
            Z_curpos.TabIndex = 78;
            Z_curpos.Text = "0";
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label23.Location = new Point(3, 72);
            label23.Name = "label23";
            label23.Size = new Size(14, 13);
            label23.TabIndex = 77;
            label23.Text = "Z";
            // 
            // Y_curpos
            // 
            Y_curpos.Location = new Point(27, 43);
            Y_curpos.Margin = new Padding(3, 2, 3, 2);
            Y_curpos.Multiline = true;
            Y_curpos.Name = "Y_curpos";
            Y_curpos.ReadOnly = true;
            Y_curpos.Size = new Size(70, 18);
            Y_curpos.TabIndex = 76;
            Y_curpos.Text = "0";
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label24.Location = new Point(3, 45);
            label24.Name = "label24";
            label24.Size = new Size(14, 13);
            label24.TabIndex = 75;
            label24.Text = "Y";
            // 
            // X_curpos
            // 
            X_curpos.Location = new Point(27, 14);
            X_curpos.Margin = new Padding(3, 2, 3, 2);
            X_curpos.Multiline = true;
            X_curpos.Name = "X_curpos";
            X_curpos.ReadOnly = true;
            X_curpos.Size = new Size(70, 18);
            X_curpos.TabIndex = 74;
            X_curpos.Text = "0";
            // 
            // label25
            // 
            label25.AutoSize = true;
            label25.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label25.Location = new Point(3, 16);
            label25.Name = "label25";
            label25.Size = new Size(14, 13);
            label25.TabIndex = 5;
            label25.Text = "X";
            // 
            // label30
            // 
            label30.AutoSize = true;
            label30.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label30.Location = new Point(3, 7);
            label30.Name = "label30";
            label30.Size = new Size(89, 30);
            label30.TabIndex = 0;
            label30.Text = "Display";
            // 
            // panel6
            // 
            panel6.BorderStyle = BorderStyle.FixedSingle;
            panel6.Controls.Add(panel7);
            panel6.Controls.Add(label13);
            panel6.Location = new Point(372, 381);
            panel6.Margin = new Padding(3, 2, 3, 2);
            panel6.Name = "panel6";
            panel6.Size = new Size(553, 149);
            panel6.TabIndex = 75;
            // 
            // panel7
            // 
            panel7.BorderStyle = BorderStyle.FixedSingle;
            panel7.Controls.Add(Trasmit_1);
            panel7.Controls.Add(Test_run);
            panel7.Controls.Add(label14);
            panel7.Controls.Add(Speed_tb);
            panel7.Controls.Add(label3);
            panel7.Controls.Add(Z2_tb);
            panel7.Controls.Add(label9);
            panel7.Controls.Add(Y2_tb);
            panel7.Controls.Add(label10);
            panel7.Controls.Add(Transmit_button);
            panel7.Controls.Add(X2_tb);
            panel7.Controls.Add(pRu_button);
            panel7.Controls.Add(Run_button);
            panel7.Controls.Add(label12);
            panel7.Controls.Add(Z_tb);
            panel7.Controls.Add(label8);
            panel7.Controls.Add(Y_tb);
            panel7.Controls.Add(label11);
            panel7.Controls.Add(X_tb);
            panel7.Location = new Point(10, 37);
            panel7.Margin = new Padding(3, 2, 3, 2);
            panel7.Name = "panel7";
            panel7.Size = new Size(507, 102);
            panel7.TabIndex = 1;
            // 
            // Trasmit_1
            // 
            Trasmit_1.Location = new Point(292, 14);
            Trasmit_1.Margin = new Padding(3, 2, 3, 2);
            Trasmit_1.Name = "Trasmit_1";
            Trasmit_1.Size = new Size(99, 22);
            Trasmit_1.TabIndex = 95;
            Trasmit_1.Text = "Transmit_pos2";
            Trasmit_1.UseVisualStyleBackColor = true;
            Trasmit_1.Click += Trasmit_1_Click;
            // 
            // Test_run
            // 
            Test_run.Location = new Point(188, 59);
            Test_run.Margin = new Padding(3, 2, 3, 2);
            Test_run.Name = "Test_run";
            Test_run.Size = new Size(99, 22);
            Test_run.TabIndex = 94;
            Test_run.Text = "Run_2_pos";
            Test_run.UseVisualStyleBackColor = true;
            Test_run.Click += Test_run_Click;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label14.Location = new Point(183, 15);
            label14.Name = "label14";
            label14.Size = new Size(39, 13);
            label14.TabIndex = 92;
            label14.Text = "Speed";
            // 
            // Speed_tb
            // 
            Speed_tb.Location = new Point(233, 14);
            Speed_tb.Margin = new Padding(3, 2, 3, 2);
            Speed_tb.Multiline = true;
            Speed_tb.Name = "Speed_tb";
            Speed_tb.Size = new Size(55, 19);
            Speed_tb.TabIndex = 91;
            Speed_tb.Text = "0";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(101, 15);
            label3.Name = "label3";
            label3.Size = new Size(20, 13);
            label3.TabIndex = 90;
            label3.Text = "X2";
            // 
            // Z2_tb
            // 
            Z2_tb.Location = new Point(123, 59);
            Z2_tb.Margin = new Padding(3, 2, 3, 2);
            Z2_tb.Multiline = true;
            Z2_tb.Name = "Z2_tb";
            Z2_tb.Size = new Size(55, 19);
            Z2_tb.TabIndex = 89;
            Z2_tb.Text = "0";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(101, 62);
            label9.Name = "label9";
            label9.Size = new Size(20, 13);
            label9.TabIndex = 88;
            label9.Text = "Z2";
            // 
            // Y2_tb
            // 
            Y2_tb.Location = new Point(123, 36);
            Y2_tb.Margin = new Padding(3, 2, 3, 2);
            Y2_tb.Multiline = true;
            Y2_tb.Name = "Y2_tb";
            Y2_tb.Size = new Size(55, 19);
            Y2_tb.TabIndex = 87;
            Y2_tb.Text = "0";
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(101, 38);
            label10.Name = "label10";
            label10.Size = new Size(20, 13);
            label10.TabIndex = 86;
            label10.Text = "Y2";
            // 
            // Transmit_button
            // 
            Transmit_button.Location = new Point(395, 10);
            Transmit_button.Margin = new Padding(3, 2, 3, 2);
            Transmit_button.Name = "Transmit_button";
            Transmit_button.Size = new Size(107, 22);
            Transmit_button.TabIndex = 75;
            Transmit_button.Text = "Transmit MoveJ";
            Transmit_button.UseVisualStyleBackColor = true;
            Transmit_button.Click += Transmit_button_Click;
            // 
            // X2_tb
            // 
            X2_tb.Location = new Point(123, 14);
            X2_tb.Margin = new Padding(3, 2, 3, 2);
            X2_tb.Multiline = true;
            X2_tb.Name = "X2_tb";
            X2_tb.Size = new Size(55, 19);
            X2_tb.TabIndex = 85;
            X2_tb.Text = "0";
            // 
            // pRu_button
            // 
            pRu_button.Location = new Point(395, 62);
            pRu_button.Margin = new Padding(3, 2, 3, 2);
            pRu_button.Name = "pRu_button";
            pRu_button.Size = new Size(99, 22);
            pRu_button.TabIndex = 84;
            pRu_button.Text = "Path Run";
            pRu_button.UseVisualStyleBackColor = true;
            pRu_button.Click += pRu_button_Click;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(4, 15);
            label12.Name = "label12";
            label12.Size = new Size(20, 13);
            label12.TabIndex = 83;
            label12.Text = "X1";
            // 
            // Z_tb
            // 
            Z_tb.Location = new Point(27, 59);
            Z_tb.Margin = new Padding(3, 2, 3, 2);
            Z_tb.Multiline = true;
            Z_tb.Name = "Z_tb";
            Z_tb.Size = new Size(55, 19);
            Z_tb.TabIndex = 82;
            Z_tb.Text = "0";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(4, 62);
            label8.Name = "label8";
            label8.Size = new Size(20, 13);
            label8.TabIndex = 81;
            label8.Text = "Z1";
            // 
            // Y_tb
            // 
            Y_tb.Location = new Point(27, 36);
            Y_tb.Margin = new Padding(3, 2, 3, 2);
            Y_tb.Multiline = true;
            Y_tb.Name = "Y_tb";
            Y_tb.Size = new Size(55, 19);
            Y_tb.TabIndex = 76;
            Y_tb.Text = "0";
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(4, 38);
            label11.Name = "label11";
            label11.Size = new Size(20, 13);
            label11.TabIndex = 75;
            label11.Text = "Y1";
            // 
            // X_tb
            // 
            X_tb.Location = new Point(27, 14);
            X_tb.Margin = new Padding(3, 2, 3, 2);
            X_tb.Multiline = true;
            X_tb.Name = "X_tb";
            X_tb.Size = new Size(55, 19);
            X_tb.TabIndex = 74;
            X_tb.Text = "0";
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(3, 7);
            label13.Name = "label13";
            label13.Size = new Size(60, 30);
            label13.TabIndex = 0;
            label13.Text = "Path";
            // 
            // Speed_button
            // 
            Speed_button.Location = new Point(146, 157);
            Speed_button.Margin = new Padding(3, 2, 3, 2);
            Speed_button.Name = "Speed_button";
            Speed_button.Size = new Size(203, 22);
            Speed_button.TabIndex = 93;
            Speed_button.Text = "Transmit Speed";
            Speed_button.UseVisualStyleBackColor = true;
            Speed_button.Click += Speed_button_Click;
            // 
            // Timer1
            // 
            Timer1.Interval = 250;
            Timer1.Tick += Timer1_Tick;
            // 
            // textBox1
            // 
            textBox1.BorderStyle = BorderStyle.FixedSingle;
            textBox1.Location = new Point(10, 149);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new Size(357, 192);
            textBox1.TabIndex = 84;
            textBox1.TextChanged += textBox1_TextChanged;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label15.Location = new Point(15, 157);
            label15.Name = "label15";
            label15.Size = new Size(97, 30);
            label15.TabIndex = 85;
            label15.Text = "Jogging";
            // 
            // Jog1B
            // 
            Jog1B.Location = new Point(250, 192);
            Jog1B.Margin = new Padding(3, 2, 3, 2);
            Jog1B.Name = "Jog1B";
            Jog1B.Size = new Size(99, 22);
            Jog1B.TabIndex = 13;
            Jog1B.Text = "Backward";
            Jog1B.UseVisualStyleBackColor = true;
            Jog1B.MouseDown += Jog1B_MouseDown;
            Jog1B.MouseUp += Jog1B_MouseUp;
            // 
            // Jog2B
            // 
            Jog2B.Location = new Point(250, 222);
            Jog2B.Margin = new Padding(3, 2, 3, 2);
            Jog2B.Name = "Jog2B";
            Jog2B.Size = new Size(99, 22);
            Jog2B.TabIndex = 87;
            Jog2B.Text = "Backward";
            Jog2B.UseVisualStyleBackColor = true;
            Jog2B.MouseDown += Jog2B_MouseDown;
            Jog2B.MouseUp += Jog2B_MouseUp;
            // 
            // Jog2F
            // 
            Jog2F.Location = new Point(146, 222);
            Jog2F.Margin = new Padding(3, 2, 3, 2);
            Jog2F.Name = "Jog2F";
            Jog2F.Size = new Size(99, 22);
            Jog2F.TabIndex = 86;
            Jog2F.Text = "Forward";
            Jog2F.UseVisualStyleBackColor = true;
            Jog2F.MouseDown += Jog2F_MouseDown;
            Jog2F.MouseUp += Jog2F_MouseUp;
            // 
            // Jog3B
            // 
            Jog3B.Location = new Point(250, 249);
            Jog3B.Margin = new Padding(3, 2, 3, 2);
            Jog3B.Name = "Jog3B";
            Jog3B.Size = new Size(99, 22);
            Jog3B.TabIndex = 89;
            Jog3B.Text = "Backward";
            Jog3B.UseVisualStyleBackColor = true;
            Jog3B.MouseDown += Jog3B_MouseDown;
            Jog3B.MouseUp += Jog3B_MouseUp;
            // 
            // Jog3F
            // 
            Jog3F.Location = new Point(146, 249);
            Jog3F.Margin = new Padding(3, 2, 3, 2);
            Jog3F.Name = "Jog3F";
            Jog3F.Size = new Size(99, 22);
            Jog3F.TabIndex = 88;
            Jog3F.Text = "Forward";
            Jog3F.UseVisualStyleBackColor = true;
            Jog3F.MouseDown += Jog3F_MouseDown;
            Jog3F.MouseUp += Jog3F_MouseUp;
            // 
            // Jog4B
            // 
            Jog4B.Location = new Point(250, 276);
            Jog4B.Margin = new Padding(3, 2, 3, 2);
            Jog4B.Name = "Jog4B";
            Jog4B.Size = new Size(99, 22);
            Jog4B.TabIndex = 91;
            Jog4B.Text = "Backward";
            Jog4B.UseVisualStyleBackColor = true;
            Jog4B.MouseDown += Jog4B_MouseDown;
            Jog4B.MouseUp += Jog4B_MouseUp;
            // 
            // Jog4F
            // 
            Jog4F.Location = new Point(146, 276);
            Jog4F.Margin = new Padding(3, 2, 3, 2);
            Jog4F.Name = "Jog4F";
            Jog4F.Size = new Size(99, 22);
            Jog4F.TabIndex = 90;
            Jog4F.Text = "Forward";
            Jog4F.UseVisualStyleBackColor = true;
            Jog4F.MouseDown += Jog4F_MouseDown;
            Jog4F.MouseUp += Jog4F_MouseUp;
            // 
            // Jog5B
            // 
            Jog5B.Location = new Point(250, 302);
            Jog5B.Margin = new Padding(3, 2, 3, 2);
            Jog5B.Name = "Jog5B";
            Jog5B.Size = new Size(99, 22);
            Jog5B.TabIndex = 93;
            Jog5B.Text = "Backward";
            Jog5B.UseVisualStyleBackColor = true;
            Jog5B.MouseDown += Jog5B_MouseDown;
            Jog5B.MouseUp += Jog5B_MouseUp;
            // 
            // Jog5F
            // 
            Jog5F.Location = new Point(146, 302);
            Jog5F.Margin = new Padding(3, 2, 3, 2);
            Jog5F.Name = "Jog5F";
            Jog5F.Size = new Size(99, 22);
            Jog5F.TabIndex = 92;
            Jog5F.Text = "Forward";
            Jog5F.UseVisualStyleBackColor = true;
            Jog5F.MouseDown += Jog5F_MouseDown;
            Jog5F.MouseUp += Jog5F_MouseUp;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label17.Location = new Point(29, 223);
            label17.Name = "label17";
            label17.Size = new Size(19, 13);
            label17.TabIndex = 94;
            label17.Text = "v2";
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label18.Location = new Point(29, 249);
            label18.Name = "label18";
            label18.Size = new Size(19, 13);
            label18.TabIndex = 95;
            label18.Text = "v3";
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label19.Location = new Point(29, 275);
            label19.Name = "label19";
            label19.Size = new Size(19, 13);
            label19.TabIndex = 96;
            label19.Text = "v4";
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label20.Location = new Point(29, 302);
            label20.Name = "label20";
            label20.Size = new Size(19, 13);
            label20.TabIndex = 97;
            label20.Text = "v5";
            // 
            // setvel_5
            // 
            setvel_5.Location = new Point(72, 305);
            setvel_5.Margin = new Padding(3, 2, 3, 2);
            setvel_5.Multiline = true;
            setvel_5.Name = "setvel_5";
            setvel_5.Size = new Size(70, 18);
            setvel_5.TabIndex = 102;
            setvel_5.Text = "300";
            // 
            // setvel_4
            // 
            setvel_4.Location = new Point(72, 278);
            setvel_4.Margin = new Padding(3, 2, 3, 2);
            setvel_4.Multiline = true;
            setvel_4.Name = "setvel_4";
            setvel_4.Size = new Size(70, 18);
            setvel_4.TabIndex = 101;
            setvel_4.Text = "300";
            // 
            // setvel_3
            // 
            setvel_3.Location = new Point(72, 250);
            setvel_3.Margin = new Padding(3, 2, 3, 2);
            setvel_3.Multiline = true;
            setvel_3.Name = "setvel_3";
            setvel_3.Size = new Size(70, 18);
            setvel_3.TabIndex = 100;
            setvel_3.Text = "300";
            // 
            // setvel_2
            // 
            setvel_2.Location = new Point(72, 223);
            setvel_2.Margin = new Padding(3, 2, 3, 2);
            setvel_2.Multiline = true;
            setvel_2.Name = "setvel_2";
            setvel_2.Size = new Size(70, 18);
            setvel_2.TabIndex = 99;
            setvel_2.Text = "300";
            // 
            // setvel_1
            // 
            setvel_1.Location = new Point(72, 194);
            setvel_1.Margin = new Padding(3, 2, 3, 2);
            setvel_1.Multiline = true;
            setvel_1.Name = "setvel_1";
            setvel_1.Size = new Size(70, 18);
            setvel_1.TabIndex = 98;
            setvel_1.Text = "300";
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label16.Location = new Point(29, 196);
            label16.Name = "label16";
            label16.Size = new Size(19, 13);
            label16.TabIndex = 103;
            label16.Text = "v1";
            // 
            // Jog1F
            // 
            Jog1F.Location = new Point(146, 192);
            Jog1F.Margin = new Padding(3, 2, 3, 2);
            Jog1F.Name = "Jog1F";
            Jog1F.Size = new Size(99, 22);
            Jog1F.TabIndex = 104;
            Jog1F.Text = "Forward";
            Jog1F.UseVisualStyleBackColor = true;
            Jog1F.MouseDown += Jog1F_MouseDown;
            Jog1F.MouseUp += Jog1F_MouseUp;
            // 
            // panel5
            // 
            panel5.BorderStyle = BorderStyle.FixedSingle;
            panel5.Controls.Add(set_speed_btn);
            panel5.Controls.Add(panel8);
            panel5.Controls.Add(label1);
            panel5.Controls.Add(spd_tb);
            panel5.Controls.Add(label34);
            panel5.Location = new Point(372, 218);
            panel5.Margin = new Padding(3, 2, 3, 2);
            panel5.Name = "panel5";
            panel5.Size = new Size(565, 140);
            panel5.TabIndex = 76;
            // 
            // set_speed_btn
            // 
            set_speed_btn.Location = new Point(438, 6);
            set_speed_btn.Margin = new Padding(3, 2, 3, 2);
            set_speed_btn.Name = "set_speed_btn";
            set_speed_btn.Size = new Size(99, 22);
            set_speed_btn.TabIndex = 98;
            set_speed_btn.Text = "Set Speed";
            set_speed_btn.UseVisualStyleBackColor = true;
            set_speed_btn.Click += set_speed_btn_Click;
            // 
            // panel8
            // 
            panel8.BorderStyle = BorderStyle.FixedSingle;
            panel8.Controls.Add(Tsm_moveC_btn);
            panel8.Controls.Add(MvCz_tb);
            panel8.Controls.Add(label36);
            panel8.Controls.Add(label37);
            panel8.Controls.Add(label38);
            panel8.Controls.Add(MvCy_tb);
            panel8.Controls.Add(label39);
            panel8.Controls.Add(MvCx_tb);
            panel8.Controls.Add(Tsm_moveL_btn);
            panel8.Controls.Add(MvLz_tb);
            panel8.Controls.Add(label27);
            panel8.Controls.Add(label28);
            panel8.Controls.Add(label29);
            panel8.Controls.Add(MvLy_tb);
            panel8.Controls.Add(label35);
            panel8.Controls.Add(MvLx_tb);
            panel8.Controls.Add(Tsm_moveJ_btn);
            panel8.Controls.Add(button1);
            panel8.Controls.Add(MvJz_tb);
            panel8.Controls.Add(label26);
            panel8.Controls.Add(label31);
            panel8.Controls.Add(label32);
            panel8.Controls.Add(MvJy_tb);
            panel8.Controls.Add(label33);
            panel8.Controls.Add(MvJx_tb);
            panel8.Location = new Point(10, 37);
            panel8.Margin = new Padding(3, 2, 3, 2);
            panel8.Name = "panel8";
            panel8.Size = new Size(543, 94);
            panel8.TabIndex = 1;
            // 
            // Tsm_moveC_btn
            // 
            Tsm_moveC_btn.Location = new Point(318, 66);
            Tsm_moveC_btn.Margin = new Padding(3, 2, 3, 2);
            Tsm_moveC_btn.Name = "Tsm_moveC_btn";
            Tsm_moveC_btn.Size = new Size(107, 22);
            Tsm_moveC_btn.TabIndex = 113;
            Tsm_moveC_btn.Text = "Transmit MoveC";
            Tsm_moveC_btn.UseVisualStyleBackColor = true;
            Tsm_moveC_btn.Click += Tsm_moveC_btn_Click;
            // 
            // MvCz_tb
            // 
            MvCz_tb.Location = new Point(249, 68);
            MvCz_tb.Margin = new Padding(3, 2, 3, 2);
            MvCz_tb.Multiline = true;
            MvCz_tb.Name = "MvCz_tb";
            MvCz_tb.Size = new Size(55, 19);
            MvCz_tb.TabIndex = 112;
            MvCz_tb.Text = "0";
            // 
            // label36
            // 
            label36.AutoSize = true;
            label36.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label36.Location = new Point(11, 71);
            label36.Name = "label36";
            label36.Size = new Size(44, 13);
            label36.TabIndex = 111;
            label36.Text = "MoveC";
            // 
            // label37
            // 
            label37.AutoSize = true;
            label37.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label37.Location = new Point(67, 72);
            label37.Name = "label37";
            label37.Size = new Size(14, 13);
            label37.TabIndex = 110;
            label37.Text = "X";
            // 
            // label38
            // 
            label38.AutoSize = true;
            label38.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label38.Location = new Point(228, 71);
            label38.Name = "label38";
            label38.Size = new Size(14, 13);
            label38.TabIndex = 109;
            label38.Text = "Z";
            // 
            // MvCy_tb
            // 
            MvCy_tb.Location = new Point(169, 68);
            MvCy_tb.Margin = new Padding(3, 2, 3, 2);
            MvCy_tb.Multiline = true;
            MvCy_tb.Name = "MvCy_tb";
            MvCy_tb.Size = new Size(55, 19);
            MvCy_tb.TabIndex = 108;
            MvCy_tb.Text = "0";
            // 
            // label39
            // 
            label39.AutoSize = true;
            label39.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label39.Location = new Point(148, 71);
            label39.Name = "label39";
            label39.Size = new Size(14, 13);
            label39.TabIndex = 107;
            label39.Text = "Y";
            // 
            // MvCx_tb
            // 
            MvCx_tb.Location = new Point(88, 68);
            MvCx_tb.Margin = new Padding(3, 2, 3, 2);
            MvCx_tb.Multiline = true;
            MvCx_tb.Name = "MvCx_tb";
            MvCx_tb.Size = new Size(55, 19);
            MvCx_tb.TabIndex = 106;
            MvCx_tb.Text = "0";
            // 
            // Tsm_moveL_btn
            // 
            Tsm_moveL_btn.Location = new Point(318, 40);
            Tsm_moveL_btn.Margin = new Padding(3, 2, 3, 2);
            Tsm_moveL_btn.Name = "Tsm_moveL_btn";
            Tsm_moveL_btn.Size = new Size(107, 22);
            Tsm_moveL_btn.TabIndex = 105;
            Tsm_moveL_btn.Text = "Transmit MoveL";
            Tsm_moveL_btn.UseVisualStyleBackColor = true;
            Tsm_moveL_btn.Click += Tsm_moveL_btn_Click;
            // 
            // MvLz_tb
            // 
            MvLz_tb.Location = new Point(249, 42);
            MvLz_tb.Margin = new Padding(3, 2, 3, 2);
            MvLz_tb.Multiline = true;
            MvLz_tb.Name = "MvLz_tb";
            MvLz_tb.Size = new Size(55, 19);
            MvLz_tb.TabIndex = 104;
            MvLz_tb.Text = "0";
            // 
            // label27
            // 
            label27.AutoSize = true;
            label27.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label27.Location = new Point(11, 45);
            label27.Name = "label27";
            label27.Size = new Size(43, 13);
            label27.TabIndex = 103;
            label27.Text = "MoveL";
            // 
            // label28
            // 
            label28.AutoSize = true;
            label28.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label28.Location = new Point(67, 46);
            label28.Name = "label28";
            label28.Size = new Size(14, 13);
            label28.TabIndex = 102;
            label28.Text = "X";
            // 
            // label29
            // 
            label29.AutoSize = true;
            label29.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label29.Location = new Point(228, 45);
            label29.Name = "label29";
            label29.Size = new Size(14, 13);
            label29.TabIndex = 101;
            label29.Text = "Z";
            // 
            // MvLy_tb
            // 
            MvLy_tb.Location = new Point(169, 42);
            MvLy_tb.Margin = new Padding(3, 2, 3, 2);
            MvLy_tb.Multiline = true;
            MvLy_tb.Name = "MvLy_tb";
            MvLy_tb.Size = new Size(55, 19);
            MvLy_tb.TabIndex = 100;
            MvLy_tb.Text = "0";
            // 
            // label35
            // 
            label35.AutoSize = true;
            label35.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label35.Location = new Point(148, 45);
            label35.Name = "label35";
            label35.Size = new Size(14, 13);
            label35.TabIndex = 99;
            label35.Text = "Y";
            // 
            // MvLx_tb
            // 
            MvLx_tb.Location = new Point(88, 42);
            MvLx_tb.Margin = new Padding(3, 2, 3, 2);
            MvLx_tb.Multiline = true;
            MvLx_tb.Name = "MvLx_tb";
            MvLx_tb.Size = new Size(55, 19);
            MvLx_tb.TabIndex = 98;
            MvLx_tb.Text = "0";
            // 
            // Tsm_moveJ_btn
            // 
            Tsm_moveJ_btn.Location = new Point(318, 14);
            Tsm_moveJ_btn.Margin = new Padding(3, 2, 3, 2);
            Tsm_moveJ_btn.Name = "Tsm_moveJ_btn";
            Tsm_moveJ_btn.Size = new Size(107, 22);
            Tsm_moveJ_btn.TabIndex = 97;
            Tsm_moveJ_btn.Text = "Transmit MoveJ";
            Tsm_moveJ_btn.UseVisualStyleBackColor = true;
            Tsm_moveJ_btn.Click += Tsm_moveJ_btn_Click;
            // 
            // button1
            // 
            button1.Location = new Point(430, 11);
            button1.Margin = new Padding(3, 2, 3, 2);
            button1.Name = "button1";
            button1.Size = new Size(99, 74);
            button1.TabIndex = 96;
            button1.Text = "Run";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // MvJz_tb
            // 
            MvJz_tb.Location = new Point(249, 15);
            MvJz_tb.Margin = new Padding(3, 2, 3, 2);
            MvJz_tb.Multiline = true;
            MvJz_tb.Name = "MvJz_tb";
            MvJz_tb.Size = new Size(55, 19);
            MvJz_tb.TabIndex = 85;
            MvJz_tb.Text = "0";
            // 
            // label26
            // 
            label26.AutoSize = true;
            label26.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label26.Location = new Point(11, 18);
            label26.Name = "label26";
            label26.Size = new Size(42, 13);
            label26.TabIndex = 84;
            label26.Text = "MoveJ";
            // 
            // label31
            // 
            label31.AutoSize = true;
            label31.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label31.Location = new Point(67, 19);
            label31.Name = "label31";
            label31.Size = new Size(14, 13);
            label31.TabIndex = 83;
            label31.Text = "X";
            // 
            // label32
            // 
            label32.AutoSize = true;
            label32.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label32.Location = new Point(228, 18);
            label32.Name = "label32";
            label32.Size = new Size(14, 13);
            label32.TabIndex = 81;
            label32.Text = "Z";
            // 
            // MvJy_tb
            // 
            MvJy_tb.Location = new Point(169, 15);
            MvJy_tb.Margin = new Padding(3, 2, 3, 2);
            MvJy_tb.Multiline = true;
            MvJy_tb.Name = "MvJy_tb";
            MvJy_tb.Size = new Size(55, 19);
            MvJy_tb.TabIndex = 76;
            MvJy_tb.Text = "0";
            // 
            // label33
            // 
            label33.AutoSize = true;
            label33.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label33.Location = new Point(148, 18);
            label33.Name = "label33";
            label33.Size = new Size(14, 13);
            label33.TabIndex = 75;
            label33.Text = "Y";
            // 
            // MvJx_tb
            // 
            MvJx_tb.Location = new Point(88, 15);
            MvJx_tb.Margin = new Padding(3, 2, 3, 2);
            MvJx_tb.Multiline = true;
            MvJx_tb.Name = "MvJx_tb";
            MvJx_tb.Size = new Size(55, 19);
            MvJx_tb.TabIndex = 74;
            MvJx_tb.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(329, 8);
            label1.Name = "label1";
            label1.Size = new Size(39, 13);
            label1.TabIndex = 97;
            label1.Text = "Speed";
            // 
            // spd_tb
            // 
            spd_tb.Location = new Point(379, 7);
            spd_tb.Margin = new Padding(3, 2, 3, 2);
            spd_tb.Multiline = true;
            spd_tb.Name = "spd_tb";
            spd_tb.Size = new Size(55, 19);
            spd_tb.TabIndex = 96;
            spd_tb.Text = "0";
            spd_tb.TextChanged += spd_tb_TextChanged;
            // 
            // label34
            // 
            label34.AutoSize = true;
            label34.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label34.Location = new Point(3, 7);
            label34.Name = "label34";
            label34.Size = new Size(60, 30);
            label34.TabIndex = 0;
            label34.Text = "Path";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1196, 548);
            Controls.Add(panel5);
            Controls.Add(Jog1F);
            Controls.Add(label16);
            Controls.Add(setvel_5);
            Controls.Add(setvel_4);
            Controls.Add(Speed_button);
            Controls.Add(setvel_3);
            Controls.Add(setvel_2);
            Controls.Add(setvel_1);
            Controls.Add(label20);
            Controls.Add(label19);
            Controls.Add(label18);
            Controls.Add(label17);
            Controls.Add(Jog5B);
            Controls.Add(Jog5F);
            Controls.Add(Jog4B);
            Controls.Add(Jog4F);
            Controls.Add(Jog3B);
            Controls.Add(Jog3F);
            Controls.Add(Jog2B);
            Controls.Add(Jog2F);
            Controls.Add(Jog1B);
            Controls.Add(label15);
            Controls.Add(textBox1);
            Controls.Add(panel6);
            Controls.Add(panel10);
            Controls.Add(panel4);
            Controls.Add(panel2);
            Controls.Add(ErrorLog);
            Controls.Add(Connect_Panel);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Form1";
            Text = "Main";
            Load += Form1_Load;
            Connect_Panel.ResumeLayout(false);
            Connect_Panel.PerformLayout();
            panel1.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panel10.ResumeLayout(false);
            panel10.PerformLayout();
            panel11.ResumeLayout(false);
            panel11.PerformLayout();
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel7.ResumeLayout(false);
            panel7.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
            panel8.ResumeLayout(false);
            panel8.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Panel Connect_Panel;
        private Label Connect_Label;
        private Panel panel1;
        private Button Connect_button;
        private Button Disconnect_button;
        private TextBox ErrorLog;
        private Panel panel2;
        private Label Control_Label;
        private Button ResetError_button;
        private Panel panel3;
        private Button GoHome_button;
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
        private TextBox textBox1;
        private Label label15;
        private Button Jog1B;
        private Button Jog2B;
        private Button Jog2F;
        private Button Jog3B;
        private Button Jog3F;
        private Button Jog4B;
        private Button Jog4F;
        private Button Jog5B;
        private Button Jog5F;
        private Label label17;
        private Label label18;
        private Label label19;
        private Label label20;
        private TextBox setvel_5;
        private TextBox setvel_4;
        private TextBox setvel_3;
        private TextBox setvel_2;
        private TextBox setvel_1;
        private Label label16;
        private Button Jog1F;
        private Panel panel5;
        private Panel panel8;
        private Label label31;
        private Label label32;
        private TextBox MvJy_tb;
        private Label label33;
        private TextBox MvJx_tb;
        private Label label34;
        private TextBox MvJz_tb;
        private Label label26;
        private Button Tsm_moveJ_btn;
        private Button button1;
        private Button set_speed_btn;
        private Label label1;
        private TextBox spd_tb;
        private Button Tsm_moveL_btn;
        private TextBox MvLz_tb;
        private Label label27;
        private Label label28;
        private Label label29;
        private TextBox MvLy_tb;
        private Label label35;
        private TextBox MvLx_tb;
        private Button Tsm_moveC_btn;
        private TextBox MvCz_tb;
        private Label label36;
        private Label label37;
        private Label label38;
        private TextBox MvCy_tb;
        private Label label39;
        private TextBox MvCx_tb;
        private Button Cvt_Trajectory;
    }
}