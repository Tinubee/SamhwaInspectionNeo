namespace SamhwaInspectionNeo.UI.Form
{
    partial class TrendReportViewer
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
            this.cSlot1상부 = new SamhwaInspectionNeo.UI.Control.Chart();
            this.SuspendLayout();
            // 
            // cSlot1상부
            // 
            this.cSlot1상부.Location = new System.Drawing.Point(12, 12);
            this.cSlot1상부.Name = "cSlot1상부";
            this.cSlot1상부.Size = new System.Drawing.Size(1319, 187);
            this.cSlot1상부.TabIndex = 0;
            // 
            // TrendReportViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1355, 831);
            this.Controls.Add(this.cSlot1상부);
            this.Name = "TrendReportViewer";
            this.Text = "TrendReportViewer";
            this.ResumeLayout(false);

        }

        #endregion

        private Control.Chart cSlot1상부;
    }
}