namespace SamhwaInspectionNeo.UI.Controls
{
    partial class Users
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Users));
            this.bind유저정보 = new System.Windows.Forms.BindingSource(this.components);
            this.g유저관리 = new DevExpress.XtraEditors.GroupControl();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.b유저저장 = new DevExpress.XtraEditors.SimpleButton();
            this.GridControl1 = new MvUtils.CustomGrid();
            this.GridView1 = new MvUtils.CustomView();
            this.col성명 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col암호 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.e암호 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.col비고 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col권한 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col허용 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.bind유저정보)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.g유저관리)).BeginInit();
            this.g유저관리.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e암호)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // bind유저정보
            // 
            this.bind유저정보.DataSource = typeof(SamhwaInspectionNeo.Schemas.유저자료);
            // 
            // g유저관리
            // 
            this.g유저관리.Controls.Add(this.panelControl1);
            this.g유저관리.Controls.Add(this.GridControl1);
            this.g유저관리.Dock = System.Windows.Forms.DockStyle.Fill;
            this.g유저관리.Location = new System.Drawing.Point(0, 0);
            this.g유저관리.Name = "g유저관리";
            this.g유저관리.Size = new System.Drawing.Size(650, 370);
            this.g유저관리.TabIndex = 11;
            this.g유저관리.Text = "사용자 관리";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.b유저저장);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(2, 334);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(646, 34);
            this.panelControl1.TabIndex = 5;
            // 
            // b유저저장
            // 
            this.b유저저장.Appearance.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b유저저장.Appearance.Options.UseFont = true;
            this.b유저저장.Dock = System.Windows.Forms.DockStyle.Right;
            this.b유저저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b유저저장.ImageOptions.SvgImage")));
            this.b유저저장.ImageOptions.SvgImageSize = new System.Drawing.Size(24, 24);
            this.b유저저장.Location = new System.Drawing.Point(1, 3);
            this.b유저저장.Name = "b유저저장";
            this.b유저저장.Size = new System.Drawing.Size(642, 28);
            this.b유저저장.TabIndex = 1;
            this.b유저저장.Text = "저  장";
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.bind유저정보;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(2, 27);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.e암호});
            this.GridControl1.Size = new System.Drawing.Size(646, 341);
            this.GridControl1.TabIndex = 0;
            this.GridControl1.UseDirectXPaint = DevExpress.Utils.DefaultBoolean.True;
            this.GridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.GridView1});
            // 
            // GridView1
            // 
            this.GridView1.AllowColumnMenu = true;
            this.GridView1.AllowCustomMenu = true;
            this.GridView1.AllowExport = true;
            this.GridView1.AllowPrint = true;
            this.GridView1.AllowSettingsMenu = false;
            this.GridView1.AllowSummaryMenu = true;
            this.GridView1.ApplyFocusedRow = true;
            this.GridView1.Caption = "";
            this.GridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.col성명,
            this.col암호,
            this.col비고,
            this.col권한,
            this.col허용});
            this.GridView1.FooterPanelHeight = 21;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 21;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 16;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsCustomization.AllowColumnMoving = false;
            this.GridView1.OptionsCustomization.AllowFilter = false;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView1.OptionsCustomization.AllowSort = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.RowHeight = 20;
            // 
            // col성명
            // 
            this.col성명.AppearanceHeader.Options.UseTextOptions = true;
            this.col성명.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col성명.FieldName = "성명";
            this.col성명.Name = "col성명";
            this.col성명.Visible = true;
            this.col성명.VisibleIndex = 0;
            // 
            // col암호
            // 
            this.col암호.AppearanceHeader.Options.UseTextOptions = true;
            this.col암호.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col암호.ColumnEdit = this.e암호;
            this.col암호.FieldName = "암호";
            this.col암호.Name = "col암호";
            this.col암호.Visible = true;
            this.col암호.VisibleIndex = 1;
            // 
            // e암호
            // 
            this.e암호.AutoHeight = false;
            this.e암호.Name = "e암호";
            this.e암호.PasswordChar = '*';
            this.e암호.UseSystemPasswordChar = true;
            // 
            // col비고
            // 
            this.col비고.AppearanceHeader.Options.UseTextOptions = true;
            this.col비고.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col비고.FieldName = "비고";
            this.col비고.Name = "col비고";
            this.col비고.Visible = true;
            this.col비고.VisibleIndex = 2;
            // 
            // col권한
            // 
            this.col권한.AppearanceHeader.Options.UseTextOptions = true;
            this.col권한.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col권한.FieldName = "권한";
            this.col권한.Name = "col권한";
            this.col권한.Visible = true;
            this.col권한.VisibleIndex = 3;
            // 
            // col허용
            // 
            this.col허용.AppearanceHeader.Options.UseTextOptions = true;
            this.col허용.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col허용.FieldName = "허용";
            this.col허용.Name = "col허용";
            this.col허용.Visible = true;
            this.col허용.VisibleIndex = 4;
            // 
            // barManager1
            // 
            this.barManager1.DockingEnabled = false;
            this.barManager1.Form = this;
            // 
            // Users
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.g유저관리);
            this.Name = "Users";
            this.Size = new System.Drawing.Size(650, 370);
            ((System.ComponentModel.ISupportInitialize)(this.bind유저정보)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.g유저관리)).EndInit();
            this.g유저관리.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e암호)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource bind유저정보;
        private DevExpress.XtraEditors.GroupControl g유저관리;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton b유저저장;
        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomView GridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col성명;
        private DevExpress.XtraGrid.Columns.GridColumn col암호;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit e암호;
        private DevExpress.XtraGrid.Columns.GridColumn col비고;
        private DevExpress.XtraGrid.Columns.GridColumn col권한;
        private DevExpress.XtraGrid.Columns.GridColumn col허용;
        private DevExpress.XtraBars.BarManager barManager1;
    }
}
