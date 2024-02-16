namespace SamhwaInspectionNeo.UI.Controls
{
    partial class DeviceSettings
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceSettings));
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.e카메라 = new SamhwaInspectionNeo.UI.Controls.CamSettings();
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.p제어 = new DevExpress.XtraTab.XtraTabPage();
            this.e입출신호 = new SamhwaInspectionNeo.UI.Control.IOControl();
            this.p설정 = new DevExpress.XtraTab.XtraTabPage();
            this.e기본설정 = new SamhwaInspectionNeo.UI.Controls.Config();
            this.p유저정보 = new DevExpress.XtraTab.XtraTabPage();
            this.e유저관리 = new SamhwaInspectionNeo.UI.Controls.Users();
            this.b트리거보드리셋 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.p제어.SuspendLayout();
            this.p설정.SuspendLayout();
            this.p유저정보.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.e카메라);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.xtraTabControl1);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1920, 1050);
            this.splitContainerControl1.SplitterPosition = 1288;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // e카메라
            // 
            this.e카메라.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e카메라.Location = new System.Drawing.Point(0, 0);
            this.e카메라.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.e카메라.Name = "e카메라";
            this.e카메라.Size = new System.Drawing.Size(1288, 1050);
            this.e카메라.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.p제어;
            this.xtraTabControl1.Size = new System.Drawing.Size(622, 1050);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.p제어,
            this.p설정,
            this.p유저정보});
            // 
            // p제어
            // 
            this.p제어.Controls.Add(this.b트리거보드리셋);
            this.p제어.Controls.Add(this.e입출신호);
            this.p제어.Name = "p제어";
            this.p제어.Size = new System.Drawing.Size(620, 1019);
            this.p제어.Text = "Others";
            // 
            // e입출신호
            // 
            this.e입출신호.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.e입출신호.Location = new System.Drawing.Point(0, 230);
            this.e입출신호.Name = "e입출신호";
            this.e입출신호.Size = new System.Drawing.Size(620, 789);
            this.e입출신호.TabIndex = 6;
            // 
            // p설정
            // 
            this.p설정.Controls.Add(this.e기본설정);
            this.p설정.Name = "p설정";
            this.p설정.Size = new System.Drawing.Size(620, 1019);
            this.p설정.Text = "Config";
            // 
            // e기본설정
            // 
            this.e기본설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e기본설정.Location = new System.Drawing.Point(0, 0);
            this.e기본설정.Name = "e기본설정";
            this.e기본설정.Size = new System.Drawing.Size(620, 1019);
            this.e기본설정.TabIndex = 0;
            // 
            // p유저정보
            // 
            this.p유저정보.Controls.Add(this.e유저관리);
            this.p유저정보.Name = "p유저정보";
            this.p유저정보.Size = new System.Drawing.Size(620, 1019);
            this.p유저정보.Text = "User";
            // 
            // e유저관리
            // 
            this.e유저관리.Dock = System.Windows.Forms.DockStyle.Top;
            this.e유저관리.Location = new System.Drawing.Point(0, 0);
            this.e유저관리.Name = "e유저관리";
            this.e유저관리.Size = new System.Drawing.Size(620, 396);
            this.e유저관리.TabIndex = 0;
            // 
            // b트리거보드리셋
            // 
            this.b트리거보드리셋.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold);
            this.b트리거보드리셋.Appearance.Options.UseFont = true;
            this.b트리거보드리셋.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b캠트리거리셋.ImageOptions.SvgImage")));
            this.b트리거보드리셋.Location = new System.Drawing.Point(3, 3);
            this.b트리거보드리셋.Name = "b트리거보드리셋";
            this.b트리거보드리셋.Size = new System.Drawing.Size(305, 40);
            this.b트리거보드리셋.TabIndex = 7;
            this.b트리거보드리셋.Text = "카메라 트리거 리셋";
            // 
            // DeviceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "DeviceSettings";
            this.Size = new System.Drawing.Size(1920, 1050);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.p제어.ResumeLayout(false);
            this.p설정.ResumeLayout(false);
            this.p유저정보.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage p제어;
        private DevExpress.XtraTab.XtraTabPage p설정;
        private Controls.CamSettings e카메라;
        private Config e기본설정;
        private DevExpress.XtraTab.XtraTabPage p유저정보;
        private Users e유저관리;
        private Control.IOControl e입출신호;
        private DevExpress.XtraEditors.SimpleButton b트리거보드리셋;
    }
}
