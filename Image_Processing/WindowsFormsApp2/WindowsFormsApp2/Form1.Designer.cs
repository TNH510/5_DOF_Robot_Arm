namespace WindowsFormsApp2
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
            this.HIGH_Threshold = new System.Windows.Forms.TextBox();
            this.Open_Image = new System.Windows.Forms.Button();
            this.Active_Image = new System.Windows.Forms.Button();
            this.LOW_Threshold = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Original_Pic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Processed_Pic)).BeginInit();
            this.SuspendLayout();
            // 
            // Original_Pic
            // 
            this.Original_Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Original_Pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Original_Pic.Location = new System.Drawing.Point(12, 32);
            this.Original_Pic.Name = "Original_Pic";
            this.Original_Pic.Size = new System.Drawing.Size(629, 642);
            this.Original_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Original_Pic.TabIndex = 0;
            this.Original_Pic.TabStop = false;
            // 
            // Processed_Pic
            // 
            this.Processed_Pic.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Processed_Pic.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Processed_Pic.Location = new System.Drawing.Point(647, 32);
            this.Processed_Pic.Name = "Processed_Pic";
            this.Processed_Pic.Size = new System.Drawing.Size(629, 642);
            this.Processed_Pic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Processed_Pic.TabIndex = 1;
            this.Processed_Pic.TabStop = false;
            // 
            // HIGH_Threshold
            // 
            this.HIGH_Threshold.Location = new System.Drawing.Point(1300, 51);
            this.HIGH_Threshold.Name = "HIGH_Threshold";
            this.HIGH_Threshold.Size = new System.Drawing.Size(150, 22);
            this.HIGH_Threshold.TabIndex = 2;
            // 
            // Open_Image
            // 
            this.Open_Image.Location = new System.Drawing.Point(1300, 155);
            this.Open_Image.Name = "Open_Image";
            this.Open_Image.Size = new System.Drawing.Size(150, 62);
            this.Open_Image.TabIndex = 3;
            this.Open_Image.Text = "OPEN";
            this.Open_Image.UseVisualStyleBackColor = true;
            this.Open_Image.Click += new System.EventHandler(this.Open_Image_Click);
            // 
            // Active_Image
            // 
            this.Active_Image.Location = new System.Drawing.Point(1300, 223);
            this.Active_Image.Name = "Active_Image";
            this.Active_Image.Size = new System.Drawing.Size(150, 62);
            this.Active_Image.TabIndex = 3;
            this.Active_Image.Text = "ACTIVE";
            this.Active_Image.UseVisualStyleBackColor = true;
            this.Active_Image.Click += new System.EventHandler(this.Active_Image_Click);
            // 
            // LOW_Threshold
            // 
            this.LOW_Threshold.Location = new System.Drawing.Point(1300, 112);
            this.LOW_Threshold.Name = "LOW_Threshold";
            this.LOW_Threshold.Size = new System.Drawing.Size(150, 22);
            this.LOW_Threshold.TabIndex = 2;
            this.LOW_Threshold.TextChanged += new System.EventHandler(this.LOW_Threshold_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1297, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Ngưỡng trên";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1297, 93);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Ngưỡng dưới";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1462, 776);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Active_Image);
            this.Controls.Add(this.Open_Image);
            this.Controls.Add(this.LOW_Threshold);
            this.Controls.Add(this.HIGH_Threshold);
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
        private System.Windows.Forms.TextBox HIGH_Threshold;
        private System.Windows.Forms.Button Open_Image;
        private System.Windows.Forms.Button Active_Image;
        private System.Windows.Forms.TextBox LOW_Threshold;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}

