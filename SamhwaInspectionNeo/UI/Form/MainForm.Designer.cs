namespace SamhwaInspectionNeo
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tabFormControl1 = new DevExpress.XtraBars.TabFormControl();
            this.타이틀 = new DevExpress.XtraBars.BarStaticItem();
            this.e프로젝트 = new DevExpress.XtraBars.BarStaticItem();
            this.skinPaletteDropDownButtonItem1 = new DevExpress.XtraBars.SkinPaletteDropDownButtonItem();
            this.p검사하기 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer1 = new DevExpress.XtraBars.TabFormContentContainer();
            this.e결과뷰어 = new SamhwaInspectionNeo.UI.Control.CamViewers();
            this.e상태뷰어 = new SamhwaInspectionNeo.UI.Controls.State();
            this.p환경설정 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer2 = new DevExpress.XtraBars.TabFormContentContainer();
            this.t환경설정 = new DevExpress.XtraTab.XtraTabControl();
            this.t검사설정 = new DevExpress.XtraTab.XtraTabPage();
            this.e검사설정 = new SamhwaInspectionNeo.UI.Control.SetInspection();
            this.t마스터설정 = new DevExpress.XtraTab.XtraTabPage();
            this.t변수설정 = new DevExpress.XtraTab.XtraTabPage();
            this.e변수설정 = new SamhwaInspectionNeo.UI.Control.SetVariables();
            this.t장치설정 = new DevExpress.XtraTab.XtraTabPage();
            this.e장치설정 = new SamhwaInspectionNeo.UI.Controls.DeviceSettings();
            this.p검사내역 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer4 = new DevExpress.XtraBars.TabFormContentContainer();
            this.e검사내역 = new SamhwaInspectionNeo.UI.Controls.Results();
            this.p로그내역 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer3 = new DevExpress.XtraBars.TabFormContentContainer();
            this.e로그내역 = new SamhwaInspectionNeo.UI.Controls.LogViewer();
            this.p마스터검사내역 = new DevExpress.XtraBars.TabFormPage();
            this.tabFormContentContainer5 = new DevExpress.XtraBars.TabFormContentContainer();
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).BeginInit();
            this.tabFormContentContainer1.SuspendLayout();
            this.tabFormContentContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.t환경설정)).BeginInit();
            this.t환경설정.SuspendLayout();
            this.t검사설정.SuspendLayout();
            this.t변수설정.SuspendLayout();
            this.t장치설정.SuspendLayout();
            this.tabFormContentContainer4.SuspendLayout();
            this.tabFormContentContainer3.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabFormControl1
            // 
            this.tabFormControl1.AllowMoveTabs = false;
            this.tabFormControl1.AllowMoveTabsToOuterForm = false;
            this.tabFormControl1.Appearance.TabFormControl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tabFormControl1.Appearance.TabFormControl.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.tabFormControl1.Appearance.TabFormControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabFormControl1.Appearance.TabFormControl.Options.UseBackColor = true;
            this.tabFormControl1.Appearance.TabFormControl.Options.UseBorderColor = true;
            this.tabFormControl1.Appearance.TabFormControl.Options.UseForeColor = true;
            this.tabFormControl1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.tabFormControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.타이틀,
            this.e프로젝트,
            this.skinPaletteDropDownButtonItem1});
            this.tabFormControl1.Location = new System.Drawing.Point(0, 0);
            this.tabFormControl1.Margin = new System.Windows.Forms.Padding(0);
            this.tabFormControl1.Name = "tabFormControl1";
            this.tabFormControl1.Pages.Add(this.p검사하기);
            this.tabFormControl1.Pages.Add(this.p환경설정);
            this.tabFormControl1.Pages.Add(this.p검사내역);
            this.tabFormControl1.Pages.Add(this.p로그내역);
            this.tabFormControl1.Pages.Add(this.p마스터검사내역);
            this.tabFormControl1.SelectedPage = this.p마스터검사내역;
            this.tabFormControl1.ShowAddPageButton = false;
            this.tabFormControl1.ShowTabCloseButtons = false;
            this.tabFormControl1.ShowTabsInTitleBar = DevExpress.XtraBars.ShowTabsInTitleBar.True;
            this.tabFormControl1.Size = new System.Drawing.Size(1920, 30);
            this.tabFormControl1.TabForm = this;
            this.tabFormControl1.TabIndex = 0;
            this.tabFormControl1.TabLeftItemLinks.Add(this.타이틀);
            this.tabFormControl1.TabRightItemLinks.Add(this.e프로젝트);
            this.tabFormControl1.TabRightItemLinks.Add(this.skinPaletteDropDownButtonItem1);
            this.tabFormControl1.TabStop = false;
            // 
            // 타이틀
            // 
            this.타이틀.Caption = "Samhwa Busbar Inspection";
            this.타이틀.Id = 0;
            this.타이틀.ImageOptions.SvgImage = global::SamhwaInspectionNeo.Properties.Resources.vision;
            this.타이틀.Name = "타이틀";
            this.타이틀.PaintStyle = DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph;
            // 
            // e프로젝트
            // 
            this.e프로젝트.Caption = "IVM : 23-1228-002";
            this.e프로젝트.Id = 1;
            this.e프로젝트.Name = "e프로젝트";
            // 
            // skinPaletteDropDownButtonItem1
            // 
            this.skinPaletteDropDownButtonItem1.Id = 1;
            this.skinPaletteDropDownButtonItem1.Name = "skinPaletteDropDownButtonItem1";
            // 
            // p검사하기
            // 
            this.p검사하기.ContentContainer = this.tabFormContentContainer1;
            this.p검사하기.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("p검사하기.ImageOptions.SvgImage")));
            this.p검사하기.Name = "p검사하기";
            this.p검사하기.Text = "검사화면";
            // 
            // tabFormContentContainer1
            // 
            this.tabFormContentContainer1.Controls.Add(this.e결과뷰어);
            this.tabFormContentContainer1.Controls.Add(this.e상태뷰어);
            this.tabFormContentContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer1.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer1.Name = "tabFormContentContainer1";
            this.tabFormContentContainer1.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer1.TabIndex = 1;
            // 
            // e결과뷰어
            // 
            this.e결과뷰어.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e결과뷰어.Location = new System.Drawing.Point(0, 104);
            this.e결과뷰어.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.e결과뷰어.Name = "e결과뷰어";
            this.e결과뷰어.Size = new System.Drawing.Size(1920, 906);
            this.e결과뷰어.TabIndex = 1;
            // 
            // e상태뷰어
            // 
            this.e상태뷰어.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.e상태뷰어.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.e상태뷰어.Appearance.Options.UseBackColor = true;
            this.e상태뷰어.Appearance.Options.UseForeColor = true;
            this.e상태뷰어.Dock = System.Windows.Forms.DockStyle.Top;
            this.e상태뷰어.Location = new System.Drawing.Point(0, 0);
            this.e상태뷰어.Margin = new System.Windows.Forms.Padding(4);
            this.e상태뷰어.Name = "e상태뷰어";
            this.e상태뷰어.Size = new System.Drawing.Size(1920, 104);
            this.e상태뷰어.TabIndex = 0;
            // 
            // p환경설정
            // 
            this.p환경설정.ContentContainer = this.tabFormContentContainer2;
            this.p환경설정.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("p환경설정.ImageOptions.SvgImage")));
            this.p환경설정.Name = "p환경설정";
            this.p환경설정.Text = "환경설정";
            // 
            // tabFormContentContainer2
            // 
            this.tabFormContentContainer2.Controls.Add(this.t환경설정);
            this.tabFormContentContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer2.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer2.Name = "tabFormContentContainer2";
            this.tabFormContentContainer2.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer2.TabIndex = 2;
            // 
            // t환경설정
            // 
            this.t환경설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.t환경설정.Location = new System.Drawing.Point(0, 0);
            this.t환경설정.Name = "t환경설정";
            this.t환경설정.SelectedTabPage = this.t검사설정;
            this.t환경설정.Size = new System.Drawing.Size(1920, 1010);
            this.t환경설정.TabIndex = 2;
            this.t환경설정.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.t검사설정,
            this.t마스터설정,
            this.t변수설정,
            this.t장치설정});
            // 
            // t검사설정
            // 
            this.t검사설정.Controls.Add(this.e검사설정);
            this.t검사설정.Name = "t검사설정";
            this.t검사설정.Size = new System.Drawing.Size(1918, 980);
            this.t검사설정.Text = "검사설정";
            // 
            // e검사설정
            // 
            this.e검사설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e검사설정.Location = new System.Drawing.Point(0, 0);
            this.e검사설정.Name = "e검사설정";
            this.e검사설정.Size = new System.Drawing.Size(1918, 980);
            this.e검사설정.TabIndex = 0;
            // 
            // t마스터설정
            // 
            this.t마스터설정.Name = "t마스터설정";
            this.t마스터설정.Size = new System.Drawing.Size(1918, 980);
            this.t마스터설정.Text = "마스터설정";
            // 
            // t변수설정
            // 
            this.t변수설정.Controls.Add(this.e변수설정);
            this.t변수설정.Name = "t변수설정";
            this.t변수설정.Size = new System.Drawing.Size(1918, 980);
            this.t변수설정.Text = "변수설정";
            // 
            // e변수설정
            // 
            this.e변수설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e변수설정.Location = new System.Drawing.Point(0, 0);
            this.e변수설정.Name = "e변수설정";
            this.e변수설정.Size = new System.Drawing.Size(1918, 980);
            this.e변수설정.TabIndex = 0;
            // 
            // t장치설정
            // 
            this.t장치설정.Controls.Add(this.e장치설정);
            this.t장치설정.Name = "t장치설정";
            this.t장치설정.Size = new System.Drawing.Size(1918, 980);
            this.t장치설정.Text = "장치설정";
            // 
            // e장치설정
            // 
            this.e장치설정.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e장치설정.Location = new System.Drawing.Point(0, 0);
            this.e장치설정.Name = "e장치설정";
            this.e장치설정.Size = new System.Drawing.Size(1918, 980);
            this.e장치설정.TabIndex = 1;
            // 
            // p검사내역
            // 
            this.p검사내역.ContentContainer = this.tabFormContentContainer4;
            this.p검사내역.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("p검사내역.ImageOptions.SvgImage")));
            this.p검사내역.Name = "p검사내역";
            this.p검사내역.Text = "검사내역";
            // 
            // tabFormContentContainer4
            // 
            this.tabFormContentContainer4.Controls.Add(this.e검사내역);
            this.tabFormContentContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer4.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer4.Name = "tabFormContentContainer4";
            this.tabFormContentContainer4.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer4.TabIndex = 3;
            // 
            // e검사내역
            // 
            this.e검사내역.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e검사내역.Location = new System.Drawing.Point(0, 0);
            this.e검사내역.Name = "e검사내역";
            this.e검사내역.Size = new System.Drawing.Size(1920, 1010);
            this.e검사내역.TabIndex = 0;
            // 
            // p로그내역
            // 
            this.p로그내역.ContentContainer = this.tabFormContentContainer3;
            this.p로그내역.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("p로그내역.ImageOptions.SvgImage")));
            this.p로그내역.Name = "p로그내역";
            this.p로그내역.Text = "로그내역";
            // 
            // tabFormContentContainer3
            // 
            this.tabFormContentContainer3.Controls.Add(this.e로그내역);
            this.tabFormContentContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer3.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer3.Name = "tabFormContentContainer3";
            this.tabFormContentContainer3.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer3.TabIndex = 2;
            // 
            // e로그내역
            // 
            this.e로그내역.Dock = System.Windows.Forms.DockStyle.Fill;
            this.e로그내역.Location = new System.Drawing.Point(0, 0);
            this.e로그내역.Margin = new System.Windows.Forms.Padding(4);
            this.e로그내역.Name = "e로그내역";
            this.e로그내역.Size = new System.Drawing.Size(1920, 1010);
            this.e로그내역.TabIndex = 0;
            // 
            // p마스터검사내역
            // 
            this.p마스터검사내역.ContentContainer = this.tabFormContentContainer5;
            this.p마스터검사내역.ImageOptions.SvgImage = global::SamhwaInspectionNeo.Properties.Resources.insertcombobox;
            this.p마스터검사내역.Name = "p마스터검사내역";
            this.p마스터검사내역.Text = "마스터검사내역";
            // 
            // tabFormContentContainer5
            // 
            this.tabFormContentContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabFormContentContainer5.Location = new System.Drawing.Point(0, 30);
            this.tabFormContentContainer5.Name = "tabFormContentContainer5";
            this.tabFormContentContainer5.Size = new System.Drawing.Size(1920, 1010);
            this.tabFormContentContainer5.TabIndex = 4;
            // 
            // MainForm
            // 
            this.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(35)))), ((int)(((byte)(35)))));
            this.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.Appearance.Options.UseBackColor = true;
            this.Appearance.Options.UseFont = true;
            this.Appearance.Options.UseForeColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1040);
            this.Controls.Add(this.tabFormContentContainer5);
            this.Controls.Add(this.tabFormControl1);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TabFormControl = this.tabFormControl1;
            this.Text = "Busbar 검사기";
            ((System.ComponentModel.ISupportInitialize)(this.tabFormControl1)).EndInit();
            this.tabFormContentContainer1.ResumeLayout(false);
            this.tabFormContentContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.t환경설정)).EndInit();
            this.t환경설정.ResumeLayout(false);
            this.t검사설정.ResumeLayout(false);
            this.t변수설정.ResumeLayout(false);
            this.t장치설정.ResumeLayout(false);
            this.tabFormContentContainer4.ResumeLayout(false);
            this.tabFormContentContainer3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.TabFormControl tabFormControl1;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer1;
        private DevExpress.XtraBars.TabFormPage p검사하기;
        private DevExpress.XtraBars.BarStaticItem 타이틀;
        private DevExpress.XtraBars.TabFormPage p환경설정;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer2;
        private DevExpress.XtraBars.BarStaticItem e프로젝트;
        private DevExpress.XtraBars.SkinPaletteDropDownButtonItem skinPaletteDropDownButtonItem1;
        private UI.Controls.State e상태뷰어;
        private DevExpress.XtraBars.TabFormPage p로그내역;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer3;
        private UI.Controls.LogViewer e로그내역;
        private DevExpress.XtraTab.XtraTabControl t환경설정;
        private DevExpress.XtraTab.XtraTabPage t검사설정;
        private DevExpress.XtraTab.XtraTabPage t변수설정;
        private DevExpress.XtraTab.XtraTabPage t장치설정;
        private UI.Controls.DeviceSettings e장치설정;
        private DevExpress.XtraBars.TabFormPage p검사내역;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer4;
        private UI.Controls.Results e검사내역;
        private UI.Control.SetInspection e검사설정;
        public UI.Control.SetVariables e변수설정;
        public UI.Control.CamViewers e결과뷰어;
        private DevExpress.XtraTab.XtraTabPage t마스터설정;
        private DevExpress.XtraBars.TabFormPage p마스터검사내역;
        private DevExpress.XtraBars.TabFormContentContainer tabFormContentContainer5;
    }
}

