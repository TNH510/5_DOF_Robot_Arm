﻿namespace WindowsFormsApp2
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
            this.picture1 = new System.Windows.Forms.PictureBox();
            this.picture2 = new System.Windows.Forms.PictureBox();
            this.open = new System.Windows.Forms.Button();
            this.process = new System.Windows.Forms.Button();
            this.high = new System.Windows.Forms.TextBox();
            this.low = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.picture3 = new System.Windows.Forms.PictureBox();
            this.text1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.picture4 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).BeginInit();
            this.SuspendLayout();
            // 
            // picture1
            // 
            this.picture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture1.Location = new System.Drawing.Point(22, 22);
            this.picture1.Name = "picture1";
            this.picture1.Size = new System.Drawing.Size(288, 299);
            this.picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture1.TabIndex = 0;
            this.picture1.TabStop = false;
            // 
            // picture2
            // 
            this.picture2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture2.Location = new System.Drawing.Point(326, 22);
            this.picture2.Name = "picture2";
            this.picture2.Size = new System.Drawing.Size(265, 299);
            this.picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture2.TabIndex = 0;
            this.picture2.TabStop = false;
            // 
            // open
            // 
            this.open.Location = new System.Drawing.Point(651, 263);
            this.open.Name = "open";
            this.open.Size = new System.Drawing.Size(163, 71);
            this.open.TabIndex = 1;
            this.open.Text = "OPEN";
            this.open.UseVisualStyleBackColor = true;
            this.open.Click += new System.EventHandler(this.open_Click_1);
            // 
            // process
            // 
            this.process.Location = new System.Drawing.Point(651, 354);
            this.process.Name = "process";
            this.process.Size = new System.Drawing.Size(163, 71);
            this.process.TabIndex = 1;
            this.process.Text = "PROCESS";
            this.process.UseVisualStyleBackColor = true;
            this.process.Click += new System.EventHandler(this.process_Click_1);
            // 
            // high
            // 
            this.high.Location = new System.Drawing.Point(648, 69);
            this.high.Name = "high";
            this.high.Size = new System.Drawing.Size(163, 22);
            this.high.TabIndex = 2;
            this.high.TextChanged += new System.EventHandler(this.high_TextChanged);
            // 
            // low
            // 
            this.low.Location = new System.Drawing.Point(648, 126);
            this.low.Name = "low";
            this.low.Size = new System.Drawing.Size(163, 22);
            this.low.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(648, 50);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "NGƯỠNG TRÊN";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(648, 107);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(103, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "NGƯỠNG DƯỚI";
            // 
            // picture3
            // 
            this.picture3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture3.Location = new System.Drawing.Point(326, 327);
            this.picture3.Name = "picture3";
            this.picture3.Size = new System.Drawing.Size(265, 270);
            this.picture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture3.TabIndex = 0;
            this.picture3.TabStop = false;
            // 
            // text1
            // 
            this.text1.Location = new System.Drawing.Point(648, 198);
            this.text1.Name = "text1";
            this.text1.Size = new System.Drawing.Size(163, 22);
            this.text1.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(648, 179);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "NGƯỠNG ";
            // 
            // picture4
            // 
            this.picture4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture4.Location = new System.Drawing.Point(22, 327);
            this.picture4.Name = "picture4";
            this.picture4.Size = new System.Drawing.Size(288, 270);
            this.picture4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture4.TabIndex = 0;
            this.picture4.TabStop = false;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(862, 621);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.text1);
            this.Controls.Add(this.low);
            this.Controls.Add(this.high);
            this.Controls.Add(this.process);
            this.Controls.Add(this.open);
            this.Controls.Add(this.picture3);
            this.Controls.Add(this.picture2);
            this.Controls.Add(this.picture4);
            this.Controls.Add(this.picture1);
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Original_Pic;
        private System.Windows.Forms.PictureBox Processed_Pic;
        private System.Windows.Forms.TextBox HIGH_Threshold;
        private System.Windows.Forms.Button Open_Image;
        private System.Windows.Forms.Button Active_Image;
        private System.Windows.Forms.TextBox LOW_Threshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picture1;
        private System.Windows.Forms.PictureBox picture2;
        private System.Windows.Forms.Button open;
        private System.Windows.Forms.Button process;
        private System.Windows.Forms.TextBox high;
        private System.Windows.Forms.TextBox low;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox picture3;
        private System.Windows.Forms.TextBox text1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.PictureBox picture4;
    }
}
