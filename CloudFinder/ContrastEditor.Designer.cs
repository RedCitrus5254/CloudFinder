namespace CloudFinder
{
    partial class ContrastEditor
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
            this.originalImagePictureBox = new System.Windows.Forms.PictureBox();
            this.posterizeImagePictureBox = new System.Windows.Forms.PictureBox();
            this.pathToImageTextBox = new System.Windows.Forms.TextBox();
            this.selectImageButton = new System.Windows.Forms.Button();
            this.contrastTrackBar = new System.Windows.Forms.TrackBar();
            this.pixelValueLabel = new System.Windows.Forms.Label();
            this.cloudPercentLabel = new System.Windows.Forms.Label();
            this.gammaPictureBox = new System.Windows.Forms.PictureBox();
            this.gammaTrackBar = new System.Windows.Forms.TrackBar();
            this.GammaLabel = new System.Windows.Forms.Label();
            this.gammaValueLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.findCloudinessCheckBox = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.posterizeImagePictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // originalImagePictureBox
            // 
            this.originalImagePictureBox.Location = new System.Drawing.Point(12, 12);
            this.originalImagePictureBox.Name = "originalImagePictureBox";
            this.originalImagePictureBox.Size = new System.Drawing.Size(472, 504);
            this.originalImagePictureBox.TabIndex = 0;
            this.originalImagePictureBox.TabStop = false;
            // 
            // posterizeImagePictureBox
            // 
            this.posterizeImagePictureBox.Location = new System.Drawing.Point(968, 12);
            this.posterizeImagePictureBox.Name = "posterizeImagePictureBox";
            this.posterizeImagePictureBox.Size = new System.Drawing.Size(472, 504);
            this.posterizeImagePictureBox.TabIndex = 1;
            this.posterizeImagePictureBox.TabStop = false;
            // 
            // pathToImageTextBox
            // 
            this.pathToImageTextBox.Location = new System.Drawing.Point(70, 545);
            this.pathToImageTextBox.Name = "pathToImageTextBox";
            this.pathToImageTextBox.Size = new System.Drawing.Size(326, 20);
            this.pathToImageTextBox.TabIndex = 2;
            // 
            // selectImageButton
            // 
            this.selectImageButton.Location = new System.Drawing.Point(402, 543);
            this.selectImageButton.Name = "selectImageButton";
            this.selectImageButton.Size = new System.Drawing.Size(32, 23);
            this.selectImageButton.TabIndex = 3;
            this.selectImageButton.Text = ". . .";
            this.selectImageButton.UseVisualStyleBackColor = true;
            this.selectImageButton.Click += new System.EventHandler(this.SelectImageButton_Click);
            // 
            // contrastTrackBar
            // 
            this.contrastTrackBar.Location = new System.Drawing.Point(1043, 539);
            this.contrastTrackBar.Maximum = 100;
            this.contrastTrackBar.Name = "contrastTrackBar";
            this.contrastTrackBar.Size = new System.Drawing.Size(323, 45);
            this.contrastTrackBar.TabIndex = 4;
            this.contrastTrackBar.Value = 50;
            this.contrastTrackBar.Scroll += new System.EventHandler(this.ContrastTrackBar_Scroll);
            // 
            // pixelValueLabel
            // 
            this.pixelValueLabel.AutoSize = true;
            this.pixelValueLabel.Location = new System.Drawing.Point(1388, 553);
            this.pixelValueLabel.Name = "pixelValueLabel";
            this.pixelValueLabel.Size = new System.Drawing.Size(25, 13);
            this.pixelValueLabel.TabIndex = 5;
            this.pixelValueLabel.Text = "127";
            // 
            // cloudPercentLabel
            // 
            this.cloudPercentLabel.AutoSize = true;
            this.cloudPercentLabel.Location = new System.Drawing.Point(1155, 590);
            this.cloudPercentLabel.Name = "cloudPercentLabel";
            this.cloudPercentLabel.Size = new System.Drawing.Size(117, 13);
            this.cloudPercentLabel.TabIndex = 6;
            this.cloudPercentLabel.Text = "Процент облачности: ";
            // 
            // gammaPictureBox
            // 
            this.gammaPictureBox.Location = new System.Drawing.Point(490, 12);
            this.gammaPictureBox.Name = "gammaPictureBox";
            this.gammaPictureBox.Size = new System.Drawing.Size(472, 504);
            this.gammaPictureBox.TabIndex = 7;
            this.gammaPictureBox.TabStop = false;
            // 
            // gammaTrackBar
            // 
            this.gammaTrackBar.Location = new System.Drawing.Point(533, 539);
            this.gammaTrackBar.Maximum = 100;
            this.gammaTrackBar.Minimum = 10;
            this.gammaTrackBar.Name = "gammaTrackBar";
            this.gammaTrackBar.Size = new System.Drawing.Size(301, 45);
            this.gammaTrackBar.TabIndex = 8;
            this.gammaTrackBar.Value = 10;
            this.gammaTrackBar.Scroll += new System.EventHandler(this.GammaTrackBar_Scroll);
            // 
            // GammaLabel
            // 
            this.GammaLabel.AutoSize = true;
            this.GammaLabel.Location = new System.Drawing.Point(840, 535);
            this.GammaLabel.Name = "GammaLabel";
            this.GammaLabel.Size = new System.Drawing.Size(43, 13);
            this.GammaLabel.TabIndex = 9;
            this.GammaLabel.Text = "Gamma";
            // 
            // gammaValueLabel
            // 
            this.gammaValueLabel.AutoSize = true;
            this.gammaValueLabel.Location = new System.Drawing.Point(853, 553);
            this.gammaValueLabel.Name = "gammaValueLabel";
            this.gammaValueLabel.Size = new System.Drawing.Size(13, 13);
            this.gammaValueLabel.TabIndex = 10;
            this.gammaValueLabel.Text = "1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1358, 535);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Порог яркости";
            // 
            // findCloudinessCheckBox
            // 
            this.findCloudinessCheckBox.AutoSize = true;
            this.findCloudinessCheckBox.Checked = true;
            this.findCloudinessCheckBox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.findCloudinessCheckBox.Location = new System.Drawing.Point(606, 589);
            this.findCloudinessCheckBox.Name = "findCloudinessCheckBox";
            this.findCloudinessCheckBox.Size = new System.Drawing.Size(156, 17);
            this.findCloudinessCheckBox.TabIndex = 12;
            this.findCloudinessCheckBox.Text = "Искать облачность сразу";
            this.findCloudinessCheckBox.UseVisualStyleBackColor = true;
            this.findCloudinessCheckBox.CheckedChanged += new System.EventHandler(this.FindCloudinessCheckBox_CheckedChanged);
            // 
            // ContrastEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1451, 618);
            this.Controls.Add(this.findCloudinessCheckBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.gammaValueLabel);
            this.Controls.Add(this.GammaLabel);
            this.Controls.Add(this.gammaTrackBar);
            this.Controls.Add(this.gammaPictureBox);
            this.Controls.Add(this.cloudPercentLabel);
            this.Controls.Add(this.pixelValueLabel);
            this.Controls.Add(this.contrastTrackBar);
            this.Controls.Add(this.selectImageButton);
            this.Controls.Add(this.pathToImageTextBox);
            this.Controls.Add(this.posterizeImagePictureBox);
            this.Controls.Add(this.originalImagePictureBox);
            this.Name = "ContrastEditor";
            this.Text = "ContrastEditor";
            ((System.ComponentModel.ISupportInitialize)(this.originalImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.posterizeImagePictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.contrastTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gammaTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox originalImagePictureBox;
        private System.Windows.Forms.PictureBox posterizeImagePictureBox;
        private System.Windows.Forms.TextBox pathToImageTextBox;
        private System.Windows.Forms.Button selectImageButton;
        private System.Windows.Forms.TrackBar contrastTrackBar;
        private System.Windows.Forms.Label pixelValueLabel;
        private System.Windows.Forms.Label cloudPercentLabel;
        private System.Windows.Forms.PictureBox gammaPictureBox;
        private System.Windows.Forms.TrackBar gammaTrackBar;
        private System.Windows.Forms.Label GammaLabel;
        private System.Windows.Forms.Label gammaValueLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox findCloudinessCheckBox;
    }
}