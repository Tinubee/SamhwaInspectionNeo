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
            this.xtraTabPage1 = new DevExpress.XtraTab.XtraTabPage();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.e강제배출 = new DevExpress.XtraEditors.ToggleSwitch();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.b캠트리거리셋 = new DevExpress.XtraEditors.SimpleButton();
            this.xtraTabPage2 = new DevExpress.XtraTab.XtraTabPage();
            this.e기본설정 = new SamhwaInspectionNeo.UI.Controls.Config();
            this.xtraTabPage3 = new DevExpress.XtraTab.XtraTabPage();
            this.e유저관리 = new SamhwaInspectionNeo.UI.Controls.Users();
            this.e입출신호 = new SamhwaInspectionNeo.UI.Control.IOControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.xtraTabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e강제배출.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.xtraTabPage2.SuspendLayout();
            this.xtraTabPage3.SuspendLayout();
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
            this.splitContainerControl1.Size = new System.Drawing.Size(1920, 980);
            this.splitContainerControl1.SplitterPosition = 1288;
            this.splitContainerControl1.TabIndex = 1;
            // 
            // e카메라
            // 
            this.e카메라.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e카메라.Location = new System.Drawing.Point(0, 0);
            this.e카메라.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.e카메라.Name = "e카메라";
            this.e카메라.Size = new System.Drawing.Size(1288, 980);
            this.e카메라.TabIndex = 0;
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.xtraTabPage1;
            this.xtraTabControl1.Size = new System.Drawing.Size(622, 980);
            this.xtraTabControl1.TabIndex = 2;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.xtraTabPage1,
            this.xtraTabPage2,
            this.xtraTabPage3});
            // 
            // xtraTabPage1
            // 
            this.xtraTabPage1.Controls.Add(this.e입출신호);
            this.xtraTabPage1.Controls.Add(this.groupControl1);
            this.xtraTabPage1.Controls.Add(this.b캠트리거리셋);
            this.xtraTabPage1.Name = "xtraTabPage1";
            this.xtraTabPage1.Size = new System.Drawing.Size(620, 950);
            this.xtraTabPage1.Text = "Others";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.layoutControl1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(620, 79);
            this.groupControl1.TabIndex = 5;
            this.groupControl1.Text = "Forced Ejection";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.e강제배출);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(2, 27);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(616, 50);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // e강제배출
            // 
            this.e강제배출.EnterMoveNextControl = true;
            this.e강제배출.Location = new System.Drawing.Point(66, 14);
            this.e강제배출.Name = "e강제배출";
            this.e강제배출.Properties.OffText = "Off";
            this.e강제배출.Properties.OnText = "On";
            this.e강제배출.Size = new System.Drawing.Size(536, 21);
            this.e강제배출.StyleController = this.layoutControl1;
            this.e강제배출.TabIndex = 4;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(616, 50);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.e강제배출;
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem1.Size = new System.Drawing.Size(596, 30);
            this.layoutControlItem1.Text = "강제배출";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(40, 14);
            // 
            // b캠트리거리셋
            // 
            this.b캠트리거리셋.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b캠트리거리셋.Appearance.Options.UseFont = true;
            this.b캠트리거리셋.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b캠트리거리셋.ImageOptions.SvgImage")));
            this.b캠트리거리셋.Location = new System.Drawing.Point(3, 83);
            this.b캠트리거리셋.Name = "b캠트리거리셋";
            this.b캠트리거리셋.Size = new System.Drawing.Size(302, 46);
            this.b캠트리거리셋.TabIndex = 4;
            this.b캠트리거리셋.Text = "카메라 트리거 리셋";
            // 
            // xtraTabPage2
            // 
            this.xtraTabPage2.Controls.Add(this.e기본설정);
            this.xtraTabPage2.Name = "xtraTabPage2";
            this.xtraTabPage2.Size = new System.Drawing.Size(620, 950);
            this.xtraTabPage2.Text = "Config";
            // 
            // e기본설정
            // 
            this.e기본설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e기본설정.Location = new System.Drawing.Point(0, 0);
            this.e기본설정.Name = "e기본설정";
            this.e기본설정.Size = new System.Drawing.Size(620, 950);
            this.e기본설정.TabIndex = 0;
            // 
            // xtraTabPage3
            // 
            this.xtraTabPage3.Controls.Add(this.e유저관리);
            this.xtraTabPage3.Name = "xtraTabPage3";
            this.xtraTabPage3.Size = new System.Drawing.Size(620, 950);
            this.xtraTabPage3.Text = "User";
            // 
            // e유저관리
            // 
            this.e유저관리.Dock = System.Windows.Forms.DockStyle.Top;
            this.e유저관리.Location = new System.Drawing.Point(0, 0);
            this.e유저관리.Name = "e유저관리";
            this.e유저관리.Size = new System.Drawing.Size(620, 370);
            this.e유저관리.TabIndex = 0;
            // 
            // e입출신호
            // 
            this.e입출신호.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.e입출신호.Location = new System.Drawing.Point(0, 195);
            this.e입출신호.Name = "e입출신호";
            this.e입출신호.Size = new System.Drawing.Size(620, 755);
            this.e입출신호.TabIndex = 6;
            // 
            // DeviceSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "DeviceSettings";
            this.Size = new System.Drawing.Size(1920, 980);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.xtraTabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e강제배출.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.xtraTabPage2.ResumeLayout(false);
            this.xtraTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage1;
        private DevExpress.XtraEditors.SimpleButton b캠트리거리셋;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage2;
        private Controls.CamSettings e카메라;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.ToggleSwitch e강제배출;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private Config e기본설정;
        private DevExpress.XtraTab.XtraTabPage xtraTabPage3;
        private Users e유저관리;
        private Control.IOControl e입출신호;
    }
}
