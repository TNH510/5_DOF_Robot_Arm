namespace Image_proccessing
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
            this.picture3 = new System.Windows.Forms.PictureBox();
            this.picture4 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Mid_Point_X = new System.Windows.Forms.TextBox();
            this.Mid_Point_Y = new System.Windows.Forms.TextBox();
            this.angle1 = new System.Windows.Forms.TextBox();
            this.angle2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dim1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dim2 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).BeginInit();
            this.SuspendLayout();
            // 
            // picture1
            // 
            this.picture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture1.Location = new System.Drawing.Point(16, 15);
            this.picture1.Margin = new System.Windows.Forms.Padding(4);
            this.picture1.Name = "picture1";
            this.picture1.Size = new System.Drawing.Size(459, 361);
            this.picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture1.TabIndex = 0;
            this.picture1.TabStop = false;
            // 
            // picture2
            // 
            this.picture2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture2.Location = new System.Drawing.Point(484, 15);
            this.picture2.Margin = new System.Windows.Forms.Padding(4);
            this.picture2.Name = "picture2";
            this.picture2.Size = new System.Drawing.Size(459, 361);
            this.picture2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture2.TabIndex = 0;
            this.picture2.TabStop = false;
            // 
            // picture3
            // 
            this.picture3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture3.Location = new System.Drawing.Point(16, 384);
            this.picture3.Margin = new System.Windows.Forms.Padding(4);
            this.picture3.Name = "picture3";
            this.picture3.Size = new System.Drawing.Size(459, 361);
            this.picture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture3.TabIndex = 0;
            this.picture3.TabStop = false;
            // 
            // picture4
            // 
            this.picture4.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.picture4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture4.Location = new System.Drawing.Point(484, 384);
            this.picture4.Margin = new System.Windows.Forms.Padding(4);
            this.picture4.Name = "picture4";
            this.picture4.Size = new System.Drawing.Size(459, 361);
            this.picture4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture4.TabIndex = 0;
            this.picture4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(952, 15);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(204, 63);
            this.button1.TabIndex = 1;
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(952, 101);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(204, 68);
            this.button2.TabIndex = 2;
            this.button2.Text = "DETECT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Mid_Point_X
            // 
            this.Mid_Point_X.Location = new System.Drawing.Point(952, 197);
            this.Mid_Point_X.Margin = new System.Windows.Forms.Padding(4);
            this.Mid_Point_X.Name = "Mid_Point_X";
            this.Mid_Point_X.Size = new System.Drawing.Size(132, 22);
            this.Mid_Point_X.TabIndex = 3;
            this.Mid_Point_X.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // Mid_Point_Y
            // 
            this.Mid_Point_Y.Location = new System.Drawing.Point(952, 247);
            this.Mid_Point_Y.Margin = new System.Windows.Forms.Padding(4);
            this.Mid_Point_Y.Name = "Mid_Point_Y";
            this.Mid_Point_Y.Size = new System.Drawing.Size(132, 22);
            this.Mid_Point_Y.TabIndex = 3;
            this.Mid_Point_Y.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // angle1
            // 
            this.angle1.Location = new System.Drawing.Point(952, 302);
            this.angle1.Margin = new System.Windows.Forms.Padding(4);
            this.angle1.Name = "angle1";
            this.angle1.Size = new System.Drawing.Size(132, 22);
            this.angle1.TabIndex = 3;
            this.angle1.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // angle2
            // 
            this.angle2.Location = new System.Drawing.Point(952, 415);
            this.angle2.Margin = new System.Windows.Forms.Padding(4);
            this.angle2.Name = "angle2";
            this.angle2.Size = new System.Drawing.Size(132, 22);
            this.angle2.TabIndex = 3;
            this.angle2.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(948, 177);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mid_Point_X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(948, 225);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mid_Point_Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(948, 283);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Angle 1";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(948, 396);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(49, 16);
            this.label5.TabIndex = 4;
            this.label5.Text = "Angle2";
            this.label5.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(952, 507);
            this.textBox8.Margin = new System.Windows.Forms.Padding(4);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(239, 22);
            this.textBox8.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(948, 488);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 4;
            this.label6.Text = "NHẬN DẠNG";
            this.label6.Click += new System.EventHandler(this.label3_Click);
            // 
            // dim1
            // 
            this.dim1.Location = new System.Drawing.Point(951, 354);
            this.dim1.Margin = new System.Windows.Forms.Padding(4);
            this.dim1.Name = "dim1";
            this.dim1.Size = new System.Drawing.Size(132, 22);
            this.dim1.TabIndex = 3;
            this.dim1.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(949, 334);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 16);
            this.label4.TabIndex = 4;
            this.label4.Text = "Dim 1";
            this.label4.Click += new System.EventHandler(this.label3_Click);
            // 
            // dim2
            // 
            this.dim2.Location = new System.Drawing.Point(952, 462);
            this.dim2.Margin = new System.Windows.Forms.Padding(4);
            this.dim2.Name = "dim2";
            this.dim2.Size = new System.Drawing.Size(132, 22);
            this.dim2.TabIndex = 3;
            this.dim2.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(950, 442);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 4;
            this.label7.Text = "Dim 2";
            this.label7.Click += new System.EventHandler(this.label3_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1195, 774);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.angle2);
            this.Controls.Add(this.dim2);
            this.Controls.Add(this.dim1);
            this.Controls.Add(this.angle1);
            this.Controls.Add(this.Mid_Point_Y);
            this.Controls.Add(this.Mid_Point_X);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.picture4);
            this.Controls.Add(this.picture3);
            this.Controls.Add(this.picture2);
            this.Controls.Add(this.picture1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picture1;
        private System.Windows.Forms.PictureBox picture2;
        private System.Windows.Forms.PictureBox picture3;
        private System.Windows.Forms.PictureBox picture4;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox Mid_Point_X;
        private System.Windows.Forms.TextBox Mid_Point_Y;
        private System.Windows.Forms.TextBox angle1;
        private System.Windows.Forms.TextBox angle2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox dim1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox dim2;
        private System.Windows.Forms.Label label7;
    }
}

