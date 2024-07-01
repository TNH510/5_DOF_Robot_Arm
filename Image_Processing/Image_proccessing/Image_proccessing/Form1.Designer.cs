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
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picture1
            // 
            this.picture1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture1.Location = new System.Drawing.Point(37, 28);
            this.picture1.Name = "picture1";
            this.picture1.Size = new System.Drawing.Size(358, 325);
            this.picture1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture1.TabIndex = 0;
            this.picture1.TabStop = false;
            // 
            // picture3
            // 
            this.picture3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture3.Location = new System.Drawing.Point(37, 388);
            this.picture3.Name = "picture3";
            this.picture3.Size = new System.Drawing.Size(358, 325);
            this.picture3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture3.TabIndex = 0;
            this.picture3.TabStop = false;
            // 
            // picture4
            // 
            this.picture4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.picture4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picture4.Location = new System.Drawing.Point(418, 28);
            this.picture4.Name = "picture4";
            this.picture4.Size = new System.Drawing.Size(358, 325);
            this.picture4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picture4.TabIndex = 0;
            this.picture4.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(14, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(153, 51);
            this.button1.TabIndex = 1;
            this.button1.Text = "OPEN";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(183, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(153, 51);
            this.button2.TabIndex = 2;
            this.button2.Text = "DETECT";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Mid_Point_X
            // 
            this.Mid_Point_X.Location = new System.Drawing.Point(14, 105);
            this.Mid_Point_X.Name = "Mid_Point_X";
            this.Mid_Point_X.Size = new System.Drawing.Size(100, 20);
            this.Mid_Point_X.TabIndex = 3;
            this.Mid_Point_X.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // Mid_Point_Y
            // 
            this.Mid_Point_Y.Location = new System.Drawing.Point(126, 105);
            this.Mid_Point_Y.Name = "Mid_Point_Y";
            this.Mid_Point_Y.Size = new System.Drawing.Size(100, 20);
            this.Mid_Point_Y.TabIndex = 3;
            this.Mid_Point_Y.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // angle1
            // 
            this.angle1.Location = new System.Drawing.Point(126, 156);
            this.angle1.Name = "angle1";
            this.angle1.Size = new System.Drawing.Size(58, 20);
            this.angle1.TabIndex = 3;
            this.angle1.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // angle2
            // 
            this.angle2.Location = new System.Drawing.Point(126, 201);
            this.angle2.Name = "angle2";
            this.angle2.Size = new System.Drawing.Size(58, 20);
            this.angle2.TabIndex = 3;
            this.angle2.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 89);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Mid_Point_X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(123, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Mid_Point_Y";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(122, 140);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Angle 1";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 186);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "Angle2";
            this.label5.Click += new System.EventHandler(this.label3_Click);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(14, 250);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(148, 20);
            this.textBox8.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 234);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 13);
            this.label6.TabIndex = 4;
            this.label6.Text = "DETECT SHAPE";
            this.label6.Click += new System.EventHandler(this.label3_Click);
            // 
            // dim1
            // 
            this.dim1.Location = new System.Drawing.Point(14, 156);
            this.dim1.Name = "dim1";
            this.dim1.Size = new System.Drawing.Size(100, 20);
            this.dim1.TabIndex = 3;
            this.dim1.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 140);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Length/ Radius";
            this.label4.Click += new System.EventHandler(this.label3_Click);
            // 
            // dim2
            // 
            this.dim2.Location = new System.Drawing.Point(14, 201);
            this.dim2.Name = "dim2";
            this.dim2.Size = new System.Drawing.Size(100, 20);
            this.dim2.TabIndex = 3;
            this.dim2.TextChanged += new System.EventHandler(this.Mid_Point_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(11, 184);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 4;
            this.label7.Text = "Width";
            this.label7.Click += new System.EventHandler(this.label3_Click);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.textBox8);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Controls.Add(this.label7);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.angle2);
            this.panel1.Controls.Add(this.dim2);
            this.panel1.Controls.Add(this.dim1);
            this.panel1.Controls.Add(this.angle1);
            this.panel1.Controls.Add(this.Mid_Point_Y);
            this.panel1.Controls.Add(this.Mid_Point_X);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Location = new System.Drawing.Point(419, 388);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(357, 326);
            this.panel1.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(808, 748);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.picture4);
            this.Controls.Add(this.picture3);
            this.Controls.Add(this.picture1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picture1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picture4)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picture1;
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
        private System.Windows.Forms.Panel panel1;
    }
}

