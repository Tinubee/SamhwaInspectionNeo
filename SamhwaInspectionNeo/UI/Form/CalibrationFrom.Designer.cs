﻿namespace SamhwaInspectionNeo.UI.Form
{
    partial class CalibrationFrom
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
            this.calibration1 = new SamhwaInspectionNeo.UI.Control.Calibration();
            this.SuspendLayout();
            // 
            // calibration1
            // 
            this.calibration1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calibration1.Location = new System.Drawing.Point(0, 0);
            this.calibration1.Name = "calibration1";
            this.calibration1.Size = new System.Drawing.Size(1099, 641);
            this.calibration1.TabIndex = 0;
            // 
            // CalibrationFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 641);
            this.Controls.Add(this.calibration1);
            this.Name = "CalibrationFrom";
            this.Text = "Calibration";
            this.ResumeLayout(false);

        }

        #endregion

        private Control.Calibration calibration1;
    }
}