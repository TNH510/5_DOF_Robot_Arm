namespace MP03_ChuyenAnhMauRGBsangGrayscale
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
            this.imageBox_Hinhgoc = new Emgu.CV.UI.ImageBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.zedGraphHistogram = new ZedGraph.ZedGraphControl();
            this.Connect_btn = new System.Windows.Forms.Button();
            this.Image_btn = new System.Windows.Forms.Button();
            this.Disconnect_btn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Hinhgoc)).BeginInit();
            this.SuspendLayout();
            // 
            // imageBox_Hinhgoc
            // 
            this.imageBox_Hinhgoc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox_Hinhgoc.Location = new System.Drawing.Point(12, 152);
            this.imageBox_Hinhgoc.Name = "imageBox_Hinhgoc";
            this.imageBox_Hinhgoc.Size = new System.Drawing.Size(640, 480);
            this.imageBox_Hinhgoc.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.imageBox_Hinhgoc.TabIndex = 2;
            this.imageBox_Hinhgoc.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label1.Location = new System.Drawing.Point(9, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 25);
            this.label1.TabIndex = 6;
            this.label1.Text = "Camera";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.label2.Location = new System.Drawing.Point(671, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(171, 25);
            this.label2.TabIndex = 7;
            this.label2.Text = "Biểu đồ Histogram";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // zedGraphHistogram
            // 
            this.zedGraphHistogram.Location = new System.Drawing.Point(676, 152);
            this.zedGraphHistogram.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.zedGraphHistogram.Name = "zedGraphHistogram";
            this.zedGraphHistogram.ScrollGrace = 0D;
            this.zedGraphHistogram.ScrollMaxX = 0D;
            this.zedGraphHistogram.ScrollMaxY = 0D;
            this.zedGraphHistogram.ScrollMaxY2 = 0D;
            this.zedGraphHistogram.ScrollMinX = 0D;
            this.zedGraphHistogram.ScrollMinY = 0D;
            this.zedGraphHistogram.ScrollMinY2 = 0D;
            this.zedGraphHistogram.Size = new System.Drawing.Size(640, 480);
            this.zedGraphHistogram.TabIndex = 10;
            this.zedGraphHistogram.Load += new System.EventHandler(this.zedGraph_Load);
            // 
            // Connect_btn
            // 
            this.Connect_btn.Location = new System.Drawing.Point(12, 46);
            this.Connect_btn.Name = "Connect_btn";
            this.Connect_btn.Size = new System.Drawing.Size(174, 46);
            this.Connect_btn.TabIndex = 11;
            this.Connect_btn.Text = "Connect";
            this.Connect_btn.UseVisualStyleBackColor = true;
            this.Connect_btn.Click += new System.EventHandler(this.Connect_btn_Click);
            // 
            // Image_btn
            // 
            this.Image_btn.Location = new System.Drawing.Point(423, 46);
            this.Image_btn.Name = "Image_btn";
            this.Image_btn.Size = new System.Drawing.Size(174, 46);
            this.Image_btn.TabIndex = 13;
            this.Image_btn.Text = "Get Image";
            this.Image_btn.UseVisualStyleBackColor = true;
            this.Image_btn.Click += new System.EventHandler(this.Image_btn_Click);
            // 
            // Disconnect_btn
            // 
            this.Disconnect_btn.Location = new System.Drawing.Point(220, 46);
            this.Disconnect_btn.Name = "Disconnect_btn";
            this.Disconnect_btn.Size = new System.Drawing.Size(174, 46);
            this.Disconnect_btn.TabIndex = 14;
            this.Disconnect_btn.Text = "Disconnect";
            this.Disconnect_btn.UseVisualStyleBackColor = true;
            this.Disconnect_btn.Click += new System.EventHandler(this.Disconnect_btn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 662);
            this.Controls.Add(this.Disconnect_btn);
            this.Controls.Add(this.Image_btn);
            this.Controls.Add(this.Connect_btn);
            this.Controls.Add(this.zedGraphHistogram);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.imageBox_Hinhgoc);
            this.Name = "Form1";
            this.Text = "Chuyển RGB sang Greyscale";
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageBox_Hinhgoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Emgu.CV.UI.ImageBox imageBox_Hinhgoc;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private ZedGraph.ZedGraphControl zedGraphHistogram;
        private System.Windows.Forms.Button Connect_btn;
        private System.Windows.Forms.Button Image_btn;
        private System.Windows.Forms.Button Disconnect_btn;
    }
}

