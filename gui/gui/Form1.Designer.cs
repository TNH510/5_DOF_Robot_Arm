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
            Run_button = new Button();
            Exe_button = new Button();
            panel5 = new Panel();
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
            label3 = new Label();
            panel10 = new Panel();
            panel11 = new Panel();
            textBox10 = new TextBox();
            label21 = new Label();
            textBox11 = new TextBox();
            label22 = new Label();
            textBox12 = new TextBox();
            label23 = new Label();
            textBox13 = new TextBox();
            label24 = new Label();
            textBox14 = new TextBox();
            label25 = new Label();
            label30 = new Label();
            panel6 = new Panel();
            button1 = new Button();
            button2 = new Button();
            panel7 = new Panel();
            textBox1 = new TextBox();
            label8 = new Label();
            textBox2 = new TextBox();
            label9 = new Label();
            textBox3 = new TextBox();
            label10 = new Label();
            textBox4 = new TextBox();
            label11 = new Label();
            textBox5 = new TextBox();
            label13 = new Label();
            label12 = new Label();
            Connect_Panel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            panel10.SuspendLayout();
            panel11.SuspendLayout();
            panel6.SuspendLayout();
            panel7.SuspendLayout();
            SuspendLayout();
            // 
            // Connect_Panel
            // 
            Connect_Panel.Controls.Add(panel1);
            Connect_Panel.Controls.Add(Connect_Label);
            Connect_Panel.Location = new Point(12, 12);
            Connect_Panel.Name = "Connect_Panel";
            Connect_Panel.Size = new Size(152, 172);
            Connect_Panel.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.Controls.Add(Disconnect_button);
            panel1.Controls.Add(Connect_button);
            panel1.Controls.Add(PLC_Label);
            panel1.Location = new Point(9, 49);
            panel1.Name = "panel1";
            panel1.Size = new Size(134, 113);
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
            ErrorLog.Location = new Point(425, 12);
            ErrorLog.Margin = new Padding(3, 2, 3, 2);
            ErrorLog.Multiline = true;
            ErrorLog.Name = "ErrorLog";
            ErrorLog.Size = new Size(370, 173);
            ErrorLog.TabIndex = 73;
            // 
            // panel2
            // 
            panel2.Controls.Add(panel3);
            panel2.Controls.Add(Control_Label);
            panel2.Location = new Point(170, 12);
            panel2.Name = "panel2";
            panel2.Size = new Size(249, 172);
            panel2.TabIndex = 2;
            // 
            // panel3
            // 
            panel3.Controls.Add(SetHome_button);
            panel3.Controls.Add(OnServo_button);
            panel3.Controls.Add(GoHome_button);
            panel3.Controls.Add(label1);
            panel3.Controls.Add(ResetError_button);
            panel3.Location = new Point(3, 49);
            panel3.Name = "panel3";
            panel3.Size = new Size(238, 113);
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
            panel4.Controls.Add(Run_button);
            panel4.Controls.Add(Exe_button);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(label3);
            panel4.Location = new Point(233, 190);
            panel4.Name = "panel4";
            panel4.Size = new Size(314, 170);
            panel4.TabIndex = 2;
            // 
            // Run_button
            // 
            Run_button.Location = new Point(41, 135);
            Run_button.Name = "Run_button";
            Run_button.Size = new Size(113, 29);
            Run_button.TabIndex = 74;
            Run_button.Text = "RUN";
            Run_button.UseVisualStyleBackColor = true;
            Run_button.Click += Run_button_Click;
            // 
            // Exe_button
            // 
            Exe_button.Location = new Point(160, 135);
            Exe_button.Name = "Exe_button";
            Exe_button.Size = new Size(113, 29);
            Exe_button.TabIndex = 12;
            Exe_button.Text = "EXECUTE";
            Exe_button.UseVisualStyleBackColor = true;
            Exe_button.Click += Exe_button_Click;
            // 
            // panel5
            // 
            panel5.Controls.Add(t5_tb);
            panel5.Controls.Add(label7);
            panel5.Controls.Add(t4_tb);
            panel5.Controls.Add(label5);
            panel5.Controls.Add(t3_tb);
            panel5.Controls.Add(label6);
            panel5.Controls.Add(t2_tb);
            panel5.Controls.Add(label4);
            panel5.Controls.Add(t1_tb);
            panel5.Controls.Add(label2);
            panel5.Location = new Point(9, 49);
            panel5.Name = "panel5";
            panel5.Size = new Size(293, 80);
            panel5.TabIndex = 1;
            // 
            // t5_tb
            // 
            t5_tb.Location = new Point(223, 20);
            t5_tb.Margin = new Padding(3, 2, 3, 2);
            t5_tb.Multiline = true;
            t5_tb.Name = "t5_tb";
            t5_tb.Size = new Size(62, 24);
            t5_tb.TabIndex = 82;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(195, 23);
            label7.Name = "label7";
            label7.Size = new Size(22, 19);
            label7.TabIndex = 81;
            label7.Text = "t5";
            // 
            // t4_tb
            // 
            t4_tb.Location = new Point(127, 50);
            t4_tb.Margin = new Padding(3, 2, 3, 2);
            t4_tb.Multiline = true;
            t4_tb.Name = "t4_tb";
            t4_tb.Size = new Size(62, 24);
            t4_tb.TabIndex = 80;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(99, 53);
            label5.Name = "label5";
            label5.Size = new Size(22, 19);
            label5.TabIndex = 79;
            label5.Text = "t4";
            // 
            // t3_tb
            // 
            t3_tb.Location = new Point(31, 50);
            t3_tb.Margin = new Padding(3, 2, 3, 2);
            t3_tb.Multiline = true;
            t3_tb.Name = "t3_tb";
            t3_tb.Size = new Size(62, 24);
            t3_tb.TabIndex = 78;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(3, 52);
            label6.Name = "label6";
            label6.Size = new Size(22, 19);
            label6.TabIndex = 77;
            label6.Text = "t3";
            // 
            // t2_tb
            // 
            t2_tb.Location = new Point(127, 18);
            t2_tb.Margin = new Padding(3, 2, 3, 2);
            t2_tb.Multiline = true;
            t2_tb.Name = "t2_tb";
            t2_tb.Size = new Size(62, 24);
            t2_tb.TabIndex = 76;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label4.Location = new Point(99, 21);
            label4.Name = "label4";
            label4.Size = new Size(22, 19);
            label4.TabIndex = 75;
            label4.Text = "t2";
            // 
            // t1_tb
            // 
            t1_tb.Location = new Point(31, 18);
            t1_tb.Margin = new Padding(3, 2, 3, 2);
            t1_tb.Multiline = true;
            t1_tb.Name = "t1_tb";
            t1_tb.Size = new Size(62, 24);
            t1_tb.TabIndex = 74;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(3, 20);
            label2.Name = "label2";
            label2.Size = new Size(22, 19);
            label2.TabIndex = 5;
            label2.Text = "t1";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(3, 9);
            label3.Name = "label3";
            label3.Size = new Size(265, 38);
            label3.TabIndex = 0;
            label3.Text = "Forward Kinematic";
            // 
            // panel10
            // 
            panel10.Controls.Add(panel11);
            panel10.Controls.Add(label30);
            panel10.Location = new Point(12, 190);
            panel10.Name = "panel10";
            panel10.Size = new Size(215, 170);
            panel10.TabIndex = 82;
            // 
            // panel11
            // 
            panel11.Controls.Add(textBox10);
            panel11.Controls.Add(label21);
            panel11.Controls.Add(textBox11);
            panel11.Controls.Add(label22);
            panel11.Controls.Add(textBox12);
            panel11.Controls.Add(label23);
            panel11.Controls.Add(textBox13);
            panel11.Controls.Add(label24);
            panel11.Controls.Add(textBox14);
            panel11.Controls.Add(label25);
            panel11.Location = new Point(9, 49);
            panel11.Name = "panel11";
            panel11.Size = new Size(197, 113);
            panel11.TabIndex = 81;
            // 
            // textBox10
            // 
            textBox10.Location = new Point(127, 47);
            textBox10.Margin = new Padding(3, 2, 3, 2);
            textBox10.Multiline = true;
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(62, 24);
            textBox10.TabIndex = 82;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label21.Location = new Point(101, 49);
            label21.Name = "label21";
            label21.Size = new Size(18, 19);
            label21.TabIndex = 81;
            label21.Text = "R";
            // 
            // textBox11
            // 
            textBox11.Location = new Point(127, 18);
            textBox11.Margin = new Padding(3, 2, 3, 2);
            textBox11.Multiline = true;
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(62, 24);
            textBox11.TabIndex = 80;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label22.Location = new Point(99, 18);
            label22.Name = "label22";
            label22.Size = new Size(20, 19);
            label22.TabIndex = 79;
            label22.Text = "φ";
            // 
            // textBox12
            // 
            textBox12.Location = new Point(31, 75);
            textBox12.Margin = new Padding(3, 2, 3, 2);
            textBox12.Multiline = true;
            textBox12.Name = "textBox12";
            textBox12.Size = new Size(62, 24);
            textBox12.TabIndex = 78;
            // 
            // label23
            // 
            label23.AutoSize = true;
            label23.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label23.Location = new Point(3, 77);
            label23.Name = "label23";
            label23.Size = new Size(18, 19);
            label23.TabIndex = 77;
            label23.Text = "Z";
            // 
            // textBox13
            // 
            textBox13.Location = new Point(31, 46);
            textBox13.Margin = new Padding(3, 2, 3, 2);
            textBox13.Multiline = true;
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(62, 24);
            textBox13.TabIndex = 76;
            // 
            // label24
            // 
            label24.AutoSize = true;
            label24.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label24.Location = new Point(3, 49);
            label24.Name = "label24";
            label24.Size = new Size(18, 19);
            label24.TabIndex = 75;
            label24.Text = "Y";
            // 
            // textBox14
            // 
            textBox14.Location = new Point(31, 18);
            textBox14.Margin = new Padding(3, 2, 3, 2);
            textBox14.Multiline = true;
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(62, 24);
            textBox14.TabIndex = 74;
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
            panel6.Controls.Add(button1);
            panel6.Controls.Add(button2);
            panel6.Controls.Add(panel7);
            panel6.Controls.Add(label13);
            panel6.Location = new Point(553, 190);
            panel6.Name = "panel6";
            panel6.Size = new Size(314, 170);
            panel6.TabIndex = 75;
            // 
            // button1
            // 
            button1.Location = new Point(41, 135);
            button1.Name = "button1";
            button1.Size = new Size(113, 29);
            button1.TabIndex = 74;
            button1.Text = "RUN";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(160, 135);
            button2.Name = "button2";
            button2.Size = new Size(113, 29);
            button2.TabIndex = 12;
            button2.Text = "EXECUTE";
            button2.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            panel7.Controls.Add(label12);
            panel7.Controls.Add(textBox1);
            panel7.Controls.Add(label8);
            panel7.Controls.Add(textBox2);
            panel7.Controls.Add(label9);
            panel7.Controls.Add(textBox3);
            panel7.Controls.Add(label10);
            panel7.Controls.Add(textBox4);
            panel7.Controls.Add(label11);
            panel7.Controls.Add(textBox5);
            panel7.Location = new Point(9, 49);
            panel7.Name = "panel7";
            panel7.Size = new Size(293, 80);
            panel7.TabIndex = 1;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(223, 20);
            textBox1.Margin = new Padding(3, 2, 3, 2);
            textBox1.Multiline = true;
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(62, 24);
            textBox1.TabIndex = 82;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label8.Location = new Point(195, 23);
            label8.Name = "label8";
            label8.Size = new Size(18, 19);
            label8.TabIndex = 81;
            label8.Text = "Z";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(127, 50);
            textBox2.Margin = new Padding(3, 2, 3, 2);
            textBox2.Multiline = true;
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(62, 24);
            textBox2.TabIndex = 80;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label9.Location = new Point(99, 53);
            label9.Name = "label9";
            label9.Size = new Size(22, 19);
            label9.TabIndex = 79;
            label9.Text = "t4";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(31, 50);
            textBox3.Margin = new Padding(3, 2, 3, 2);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(62, 24);
            textBox3.TabIndex = 78;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label10.Location = new Point(3, 52);
            label10.Name = "label10";
            label10.Size = new Size(22, 19);
            label10.TabIndex = 77;
            label10.Text = "t3";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(127, 18);
            textBox4.Margin = new Padding(3, 2, 3, 2);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(62, 24);
            textBox4.TabIndex = 76;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label11.Location = new Point(99, 21);
            label11.Name = "label11";
            label11.Size = new Size(18, 19);
            label11.TabIndex = 75;
            label11.Text = "Y";
            // 
            // textBox5
            // 
            textBox5.Location = new Point(31, 18);
            textBox5.Margin = new Padding(3, 2, 3, 2);
            textBox5.Multiline = true;
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(62, 24);
            textBox5.TabIndex = 74;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Font = new Font("Segoe UI", 16.2F, FontStyle.Bold, GraphicsUnit.Point);
            label13.Location = new Point(3, 9);
            label13.Name = "label13";
            label13.Size = new Size(265, 38);
            label13.TabIndex = 0;
            label13.Text = "Forward Kinematic";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point);
            label12.Location = new Point(3, 20);
            label12.Name = "label12";
            label12.Size = new Size(18, 19);
            label12.TabIndex = 83;
            label12.Text = "X";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1090, 566);
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
            panel4.PerformLayout();
            panel5.ResumeLayout(false);
            panel5.PerformLayout();
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
        private Panel panel5;
        private Label label3;
        private Label label2;
        private TextBox t4_tb;
        private Label label5;
        private TextBox t3_tb;
        private Label label6;
        private TextBox t2_tb;
        private Label label4;
        private TextBox t1_tb;
        private Button Exe_button;
        private Button Run_button;
        private Panel panel10;
        private Panel panel11;
        private TextBox textBox10;
        private Label label21;
        private TextBox textBox11;
        private Label label22;
        private TextBox textBox12;
        private Label label23;
        private TextBox textBox13;
        private Label label24;
        private TextBox textBox14;
        private Label label25;
        private Label label30;
        private TextBox t5_tb;
        private Label label7;
        private Panel panel6;
        private Button button1;
        private Button button2;
        private Panel panel7;
        private TextBox textBox1;
        private Label label8;
        private TextBox textBox2;
        private Label label9;
        private TextBox textBox3;
        private Label label10;
        private TextBox textBox4;
        private Label label11;
        private TextBox textBox5;
        private Label label13;
        private Label label12;
    }
}