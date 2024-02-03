namespace SamhwaInspectionNeo.UI.Form
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
            this.p보정값설정창 = new SamhwaInspectionNeo.UI.Control.Calibration();
            this.SuspendLayout();
            // 
            // p보정값설정창
            // 
            this.p보정값설정창.Dock = System.Windows.Forms.DockStyle.Fill;
            this.p보정값설정창.Location = new System.Drawing.Point(0, 0);
            this.p보정값설정창.Name = "p보정값설정창";
            this.p보정값설정창.Size = new System.Drawing.Size(1099, 641);
            this.p보정값설정창.TabIndex = 0;
            // 
            // CalibrationFrom
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1099, 641);
            this.Controls.Add(this.p보정값설정창);
            this.Name = "CalibrationFrom";
            this.Text = "Calibration";
            this.ResumeLayout(false);

        }

        #endregion

        private Control.Calibration p보정값설정창;
    }
}