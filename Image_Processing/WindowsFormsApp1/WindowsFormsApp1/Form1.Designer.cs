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
            this.Original_picture = new System.Windows.Forms.PictureBox();
            this.Processed_picture = new System.Windows.Forms.PictureBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.Original_picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Processed_picture)).BeginInit();
            this.SuspendLayout();
            // 
            // Original_picture
            // 
            this.Original_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Original_picture.Location = new System.Drawing.Point(12, 12);
            this.Original_picture.Name = "Original_picture";
            this.Original_picture.Size = new System.Drawing.Size(506, 342);
            this.Original_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Original_picture.TabIndex = 0;
            this.Original_picture.TabStop = false;
            // 
            // Processed_picture
            // 
            this.Processed_picture.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Processed_picture.Location = new System.Drawing.Point(524, 12);
            this.Processed_picture.Name = "Processed_picture";
            this.Processed_picture.Size = new System.Drawing.Size(506, 342);
            this.Processed_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Processed_picture.TabIndex = 1;
            this.Processed_picture.TabStop = false;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(1037, 13);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(129, 22);
            this.textBox1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1037, 41);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 51);
            this.button1.TabIndex = 3;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1193, 396);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.Processed_picture);
            this.Controls.Add(this.Original_picture);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Original_picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Processed_picture)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Original_picture;
        private System.Windows.Forms.PictureBox Processed_picture;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

