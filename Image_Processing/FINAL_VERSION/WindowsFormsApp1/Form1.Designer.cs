namespace WindowsFormsApp1
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
            this.Original_Pic = new System.Windows.Forms.PictureBox();
            this.Processed_Pic = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OPEN = new System.Windows.Forms.Button();
            this.PHOTO = new System.Windows.Forms.Button();
            this.ACTIVE = new System.Windows.Forms.Button();
            this.Center_X = new System.Windows.Forms.TextBox();
            this.Center_Y = new System.Windows.Forms.TextBox();
            this.Detect_shape = new System.Windows.Forms.TextBox();
            this.Angle1 = new System.Windows.Forms.TextBox();
            this.Angle2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.Width = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Height = new System.Windows.Forms.TextBox();
            this.Radius_circle = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Original_Pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Processed_Pic)).BeginInit();
            this.SuspendLayout();
            // 
            // Original_Pic
            // 
            this.Original_Pic.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Original_Pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Original_Pic.Location = new System.Drawing.Point(12, 43);
            this.Original_Pic.Name = "Original_Pic";
            this.Original_Pic.Size = new System.Drawing.Size(320, 240);
            this.Original_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Original_Pic.TabIndex = 0;
            this.Original_Pic.TabStop = false;
            // 
            // Processed_Pic
            // 
            this.Processed_Pic.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.Processed_Pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Processed_Pic.Location = new System.Drawing.Point(338, 43);
            this.Processed_Pic.Name = "Processed_Pic";
            this.Processed_Pic.Size = new System.Drawing.Size(320, 240);
            this.Processed_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Processed_Pic.TabIndex = 1;
            this.Processed_Pic.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "ẢNH CHỤP";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(358, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "ẢNH XỬ LÍ";
            // 
            // OPEN
            // 
            this.OPEN.Location = new System.Drawing.Point(719, 42);
            this.OPEN.Name = "OPEN";
            this.OPEN.Size = new System.Drawing.Size(119, 66);
            this.OPEN.TabIndex = 4;
            this.OPEN.Text = "OPEN";
            this.OPEN.UseVisualStyleBackColor = true;
            this.OPEN.Click += new System.EventHandler(this.OPEN_Click);
            // 
            // PHOTO
            // 
            this.PHOTO.Location = new System.Drawing.Point(844, 42);
            this.PHOTO.Name = "PHOTO";
            this.PHOTO.Size = new System.Drawing.Size(119, 66);
            this.PHOTO.TabIndex = 4;
            this.PHOTO.Text = "TAKE A PHOTO";
            this.PHOTO.UseVisualStyleBackColor = true;
            this.PHOTO.Click += new System.EventHandler(this.PHOTO_Click);
            // 
            // ACTIVE
            // 
            this.ACTIVE.Location = new System.Drawing.Point(719, 114);
            this.ACTIVE.Name = "ACTIVE";
            this.ACTIVE.Size = new System.Drawing.Size(119, 66);
            this.ACTIVE.TabIndex = 4;
            this.ACTIVE.Text = "ACTIVE";
            this.ACTIVE.UseVisualStyleBackColor = true;
            this.ACTIVE.Click += new System.EventHandler(this.ACTIVE_Click);
            // 
            // Center_X
            // 
            this.Center_X.Location = new System.Drawing.Point(719, 220);
            this.Center_X.Name = "Center_X";
            this.Center_X.Size = new System.Drawing.Size(100, 20);
            this.Center_X.TabIndex = 5;
            // 
            // Center_Y
            // 
            this.Center_Y.Location = new System.Drawing.Point(825, 220);
            this.Center_Y.Name = "Center_Y";
            this.Center_Y.Size = new System.Drawing.Size(100, 20);
            this.Center_Y.TabIndex = 5;
            // 
            // Detect_shape
            // 
            this.Detect_shape.Location = new System.Drawing.Point(719, 265);
            this.Detect_shape.Name = "Detect_shape";
            this.Detect_shape.Size = new System.Drawing.Size(100, 20);
            this.Detect_shape.TabIndex = 5;
            // 
            // Angle1
            // 
            this.Angle1.Location = new System.Drawing.Point(719, 307);
            this.Angle1.Name = "Angle1";
            this.Angle1.Size = new System.Drawing.Size(100, 20);
            this.Angle1.TabIndex = 5;
            // 
            // Angle2
            // 
            this.Angle2.Location = new System.Drawing.Point(825, 307);
            this.Angle2.Name = "Angle2";
            this.Angle2.Size = new System.Drawing.Size(100, 20);
            this.Angle2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(716, 204);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "VỊ TRÍ TÂM";
            this.label3.Click += new System.EventHandler(this.label1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(716, 249);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "NHẬN DẠNG";
            this.label4.Click += new System.EventHandler(this.label1_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(716, 291);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(75, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "GÓC NGHIÊN";
            this.label5.Click += new System.EventHandler(this.label1_Click);
            // 
            // Width
            // 
            this.Width.Location = new System.Drawing.Point(719, 352);
            this.Width.Name = "Width";
            this.Width.Size = new System.Drawing.Size(100, 20);
            this.Width.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(716, 335);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(106, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "KÍCH THƯỚC CẠNH";
            this.label6.Click += new System.EventHandler(this.label1_Click);
            // 
            // Height
            // 
            this.Height.Location = new System.Drawing.Point(825, 352);
            this.Height.Name = "Height";
            this.Height.Size = new System.Drawing.Size(100, 20);
            this.Height.TabIndex = 6;
            // 
            // Radius_circle
            // 
            this.Radius_circle.Location = new System.Drawing.Point(719, 397);
            this.Radius_circle.Name = "Radius_circle";
            this.Radius_circle.Size = new System.Drawing.Size(100, 20);
            this.Radius_circle.TabIndex = 6;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(716, 381);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 13);
            this.label7.TabIndex = 2;
            this.label7.Text = "BÁN KÍNH";
            this.label7.Click += new System.EventHandler(this.label1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(989, 446);
            this.Controls.Add(this.Height);
            this.Controls.Add(this.Radius_circle);
            this.Controls.Add(this.Width);
            this.Controls.Add(this.Angle2);
            this.Controls.Add(this.Angle1);
            this.Controls.Add(this.Detect_shape);
            this.Controls.Add(this.Center_Y);
            this.Controls.Add(this.Center_X);
            this.Controls.Add(this.ACTIVE);
            this.Controls.Add(this.PHOTO);
            this.Controls.Add(this.OPEN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Processed_Pic);
            this.Controls.Add(this.Original_Pic);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Original_Pic)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Processed_Pic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Original_Pic;
        private System.Windows.Forms.PictureBox Processed_Pic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OPEN;
        private System.Windows.Forms.Button PHOTO;
        private System.Windows.Forms.Button ACTIVE;
        private System.Windows.Forms.TextBox Center_X;
        private System.Windows.Forms.TextBox Center_Y;
        private System.Windows.Forms.TextBox Detect_shape;
        private System.Windows.Forms.TextBox Angle1;
        private System.Windows.Forms.TextBox Angle2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox Width;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Height;
        private System.Windows.Forms.TextBox Radius_circle;
        private System.Windows.Forms.Label label7;
    }
}

