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
            Exe_button = new Button();
            panel5 = new Panel();
            t4_tb = new TextBox();
            label5 = new Label();
            t3_tb = new TextBox();
            label6 = new Label();
            t2_tb = new TextBox();
            label4 = new Label();
            t1_tb = new TextBox();
            label2 = new Label();
            label3 = new Label();
            Connect_Panel.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panel5.SuspendLayout();
            SuspendLayout();
            // 
            // Connect_Panel
            // 
            Connect_Panel.Controls.Add(panel1);
            Connect_Panel.Controls.Add(Connect_Label);
            Connect_Panel.Location = new Point(12, 12);
            Connect_Panel.Name = "Connect_Panel";
            Connect_Panel.Size = new Size(370, 172);
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
            ErrorLog.Location = new Point(388, 190);
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
            panel2.Location = new Point(12, 190);
            panel2.Name = "panel2";
            panel2.Size = new Size(370, 172);
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
            panel3.Size = new Size(359, 113);
            panel3.TabIndex = 5;
            // 
            // SetHome_button
            // 
            SetHome_button.Location = new Point(241, 40);
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
            OnServo_button.Text = "SERVO";
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
            ResetError_button.Location = new Point(122, 40);
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
            panel4.Controls.Add(Exe_button);
            panel4.Controls.Add(panel5);
            panel4.Controls.Add(label3);
            panel4.Location = new Point(388, 12);
            panel4.Name = "panel4";
            panel4.Size = new Size(335, 172);
            panel4.TabIndex = 2;
            // 
            // Exe_button
            // 
            Exe_button.Location = new Point(216, 66);
            Exe_button.Name = "Exe_button";
            Exe_button.Size = new Size(113, 29);
            Exe_button.TabIndex = 12;
            Exe_button.Text = "EXECUTE";
            Exe_button.UseVisualStyleBackColor = true;
            Exe_button.Click += Exe_button_Click;
            // 
            // panel5
            // 
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
            panel5.Size = new Size(201, 113);
            panel5.TabIndex = 1;
            // 
            // t4_tb
            // 
            t4_tb.Location = new Point(128, 75);
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
            label5.Location = new Point(100, 75);
            label5.Name = "label5";
            label5.Size = new Size(22, 19);
            label5.TabIndex = 79;
            label5.Text = "t4";
            // 
            // t3_tb
            // 
            t3_tb.Location = new Point(31, 75);
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
            label6.Location = new Point(3, 75);
            label6.Name = "label6";
            label6.Size = new Size(22, 19);
            label6.TabIndex = 77;
            label6.Text = "t3";
            // 
            // t2_tb
            // 
            t2_tb.Location = new Point(128, 18);
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
            label4.Location = new Point(100, 18);
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
            label2.Location = new Point(3, 18);
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
            label3.Size = new Size(68, 38);
            label3.TabIndex = 0;
            label3.Text = "Test";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1190, 433);
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
    }
}