namespace Testing_Detect_Shape
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
            this.components = new System.ComponentModel.Container();
            this.Original_Image = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Binary_Image = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.OPEN = new System.Windows.Forms.Button();
            this.THRESHOLD = new System.Windows.Forms.Button();
            this.PREDICT = new System.Windows.Forms.Button();
            this.zGHistogram = new ZedGraph.ZedGraphControl();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.text_threshold = new System.Windows.Forms.TextBox();
            this.HISTOGRAM = new System.Windows.Forms.Button();
            this.THRESHOLD_MAN = new System.Windows.Forms.Button();
            this.textBox = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.Original_Image)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Binary_Image)).BeginInit();
            this.SuspendLayout();
            // 
            // Original_Image
            // 
            this.Original_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Original_Image.Location = new System.Drawing.Point(12, 29);
            this.Original_Image.Name = "Original_Image";
            this.Original_Image.Size = new System.Drawing.Size(301, 252);
            this.Original_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Original_Image.TabIndex = 0;
            this.Original_Image.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Original Image";
            // 
            // Binary_Image
            // 
            this.Binary_Image.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Binary_Image.Location = new System.Drawing.Point(319, 29);
            this.Binary_Image.Name = "Binary_Image";
            this.Binary_Image.Size = new System.Drawing.Size(519, 498);
            this.Binary_Image.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Binary_Image.TabIndex = 0;
            this.Binary_Image.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Binary Image";
            // 
            // OPEN
            // 
            this.OPEN.Location = new System.Drawing.Point(865, 29);
            this.OPEN.Name = "OPEN";
            this.OPEN.Size = new System.Drawing.Size(119, 46);
            this.OPEN.TabIndex = 2;
            this.OPEN.Text = "OPEN";
            this.OPEN.UseVisualStyleBackColor = true;
            this.OPEN.Click += new System.EventHandler(this.OPEN_Click);
            // 
            // THRESHOLD
            // 
            this.THRESHOLD.Location = new System.Drawing.Point(865, 135);
            this.THRESHOLD.Name = "THRESHOLD";
            this.THRESHOLD.Size = new System.Drawing.Size(119, 46);
            this.THRESHOLD.TabIndex = 2;
            this.THRESHOLD.Text = "THRESHOLD_AUTO";
            this.THRESHOLD.UseVisualStyleBackColor = true;
            this.THRESHOLD.Click += new System.EventHandler(this.THRESHOLD_Click);
            // 
            // PREDICT
            // 
            this.PREDICT.Location = new System.Drawing.Point(865, 289);
            this.PREDICT.Name = "PREDICT";
            this.PREDICT.Size = new System.Drawing.Size(119, 46);
            this.PREDICT.TabIndex = 2;
            this.PREDICT.Text = "PREDICT";
            this.PREDICT.UseVisualStyleBackColor = true;
            this.PREDICT.Click += new System.EventHandler(this.PREDICT_Click);
            // 
            // zGHistogram
            // 
            this.zGHistogram.Location = new System.Drawing.Point(12, 311);
            this.zGHistogram.Margin = new System.Windows.Forms.Padding(4);
            this.zGHistogram.Name = "zGHistogram";
            this.zGHistogram.ScrollGrace = 0D;
            this.zGHistogram.ScrollMaxX = 0D;
            this.zGHistogram.ScrollMaxY = 0D;
            this.zGHistogram.ScrollMaxY2 = 0D;
            this.zGHistogram.ScrollMinX = 0D;
            this.zGHistogram.ScrollMinY = 0D;
            this.zGHistogram.ScrollMinY2 = 0D;
            this.zGHistogram.Size = new System.Drawing.Size(132, 112);
            this.zGHistogram.TabIndex = 5;
            this.zGHistogram.UseExtendedPrintDialog = true;
            // 
            // text_threshold
            // 
            this.text_threshold.Location = new System.Drawing.Point(865, 187);
            this.text_threshold.Name = "text_threshold";
            this.text_threshold.Size = new System.Drawing.Size(119, 20);
            this.text_threshold.TabIndex = 6;
            // 
            // HISTOGRAM
            // 
            this.HISTOGRAM.Location = new System.Drawing.Point(865, 82);
            this.HISTOGRAM.Name = "HISTOGRAM";
            this.HISTOGRAM.Size = new System.Drawing.Size(119, 47);
            this.HISTOGRAM.TabIndex = 7;
            this.HISTOGRAM.Text = "HISTOGRAM";
            this.HISTOGRAM.UseVisualStyleBackColor = true;
            this.HISTOGRAM.Click += new System.EventHandler(this.HISTOGRAM_Click);
            // 
            // THRESHOLD_MAN
            // 
            this.THRESHOLD_MAN.Location = new System.Drawing.Point(865, 213);
            this.THRESHOLD_MAN.Name = "THRESHOLD_MAN";
            this.THRESHOLD_MAN.Size = new System.Drawing.Size(119, 37);
            this.THRESHOLD_MAN.TabIndex = 8;
            this.THRESHOLD_MAN.Text = "THRESHOLD_MAN";
            this.THRESHOLD_MAN.UseVisualStyleBackColor = true;
            this.THRESHOLD_MAN.Click += new System.EventHandler(this.THRESHOLD_MAN_Click);
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(865, 257);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(119, 20);
            this.textBox.TabIndex = 9;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(865, 351);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 10;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(865, 377);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 10;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(865, 403);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(100, 20);
            this.textBox3.TabIndex = 10;
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(865, 429);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(100, 20);
            this.textBox4.TabIndex = 10;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(865, 455);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(100, 20);
            this.textBox5.TabIndex = 10;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(865, 481);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(100, 20);
            this.textBox6.TabIndex = 10;
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(865, 507);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(100, 20);
            this.textBox7.TabIndex = 10;
            // 
            // textBox8
            // 
            this.textBox8.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.Location = new System.Drawing.Point(12, 563);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(252, 38);
            this.textBox8.TabIndex = 11;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 617);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox6);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox4);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.THRESHOLD_MAN);
            this.Controls.Add(this.HISTOGRAM);
            this.Controls.Add(this.text_threshold);
            this.Controls.Add(this.zGHistogram);
            this.Controls.Add(this.PREDICT);
            this.Controls.Add(this.THRESHOLD);
            this.Controls.Add(this.OPEN);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Binary_Image);
            this.Controls.Add(this.Original_Image);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.Original_Image)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Binary_Image)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox Original_Image;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox Binary_Image;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button OPEN;
        private System.Windows.Forms.Button THRESHOLD;
        private System.Windows.Forms.Button PREDICT;
        private ZedGraph.ZedGraphControl zGHistogram;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.TextBox text_threshold;
        private System.Windows.Forms.Button HISTOGRAM;
        private System.Windows.Forms.Button THRESHOLD_MAN;
        private System.Windows.Forms.TextBox textBox;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
    }
}

