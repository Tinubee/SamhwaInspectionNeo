namespace SamhwaInspectionNeo.UI.Control
{
    partial class SetMasterInspection
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetMasterInspection));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.b보정값설정 = new DevExpress.XtraEditors.SimpleButton();
            this.b수동검사 = new DevExpress.XtraEditors.SimpleButton();
            this.b도구설정 = new DevExpress.XtraEditors.SimpleButton();
            this.e모델선택 = new DevExpress.XtraEditors.LookUpEdit();
            this.b설정저장 = new DevExpress.XtraEditors.SimpleButton();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bind검사설정 = new System.Windows.Forms.BindingSource(this.components);
            this.bind모델자료 = new System.Windows.Forms.BindingSource(this.components);
            this.GridControl1 = new MvUtils.CustomGrid();
            this.GridView1 = new MvUtils.CustomView();
            this.col검사일시 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사항목 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사그룹 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사장치 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col플로우 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col지그 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col결과분류 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정단위 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col보정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col교정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마스터값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col마스터공차 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col결과값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정결과 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind검사설정)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind모델자료)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.b보정값설정);
            this.panelControl1.Controls.Add(this.b수동검사);
            this.panelControl1.Controls.Add(this.b도구설정);
            this.panelControl1.Controls.Add(this.e모델선택);
            this.panelControl1.Controls.Add(this.b설정저장);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Padding = new System.Windows.Forms.Padding(3);
            this.panelControl1.Size = new System.Drawing.Size(1634, 56);
            this.panelControl1.TabIndex = 6;
            // 
            // b보정값설정
            // 
            this.b보정값설정.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b보정값설정.Appearance.Options.UseFont = true;
            this.b보정값설정.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b보정값설정.Dock = System.Windows.Forms.DockStyle.Right;
            this.b보정값설정.ImageOptions.SvgImage = global::SamhwaInspectionNeo.Properties.Resources.properties;
            this.b보정값설정.Location = new System.Drawing.Point(1269, 5);
            this.b보정값설정.Name = "b보정값설정";
            this.b보정값설정.Size = new System.Drawing.Size(180, 46);
            this.b보정값설정.TabIndex = 14;
            this.b보정값설정.Text = "보정값설정";
            // 
            // b수동검사
            // 
            this.b수동검사.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b수동검사.Appearance.Options.UseFont = true;
            this.b수동검사.Dock = System.Windows.Forms.DockStyle.Left;
            this.b수동검사.ImageOptions.SvgImage = global::SamhwaInspectionNeo.Properties.Resources.bo_security_permission_action;
            this.b수동검사.Location = new System.Drawing.Point(551, 5);
            this.b수동검사.Name = "b수동검사";
            this.b수동검사.Size = new System.Drawing.Size(195, 46);
            this.b수동검사.TabIndex = 12;
            this.b수동검사.Text = "수동검사";
            // 
            // b도구설정
            // 
            this.b도구설정.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b도구설정.Appearance.Options.UseFont = true;
            this.b도구설정.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b도구설정.Dock = System.Windows.Forms.DockStyle.Left;
            this.b도구설정.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b도구설정.ImageOptions.SvgImage")));
            this.b도구설정.Location = new System.Drawing.Point(379, 5);
            this.b도구설정.Name = "b도구설정";
            this.b도구설정.Size = new System.Drawing.Size(172, 46);
            this.b도구설정.TabIndex = 10;
            this.b도구설정.Text = "VM 설정";
            // 
            // e모델선택
            // 
            this.e모델선택.Dock = System.Windows.Forms.DockStyle.Left;
            this.e모델선택.Location = new System.Drawing.Point(5, 5);
            this.e모델선택.Name = "e모델선택";
            this.e모델선택.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e모델선택.Properties.Appearance.Options.UseFont = true;
            this.e모델선택.Properties.Appearance.Options.UseTextOptions = true;
            this.e모델선택.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.e모델선택.Properties.AppearanceDropDown.Font = new System.Drawing.Font("맑은 고딕", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e모델선택.Properties.AppearanceDropDown.Options.UseFont = true;
            this.e모델선택.Properties.AutoHeight = false;
            this.e모델선택.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e모델선택.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("모델구분", "구분", 150, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("모델설명", "설명", 240, DevExpress.Utils.FormatType.None, "", true, DevExpress.Utils.HorzAlignment.Near, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.e모델선택.Properties.DisplayMember = "모델구분";
            this.e모델선택.Properties.NullText = "[모델선택]";
            this.e모델선택.Properties.ValueMember = "모델구분";
            this.e모델선택.Size = new System.Drawing.Size(374, 46);
            this.e모델선택.TabIndex = 9;
            // 
            // b설정저장
            // 
            this.b설정저장.Appearance.Font = new System.Drawing.Font("맑은 고딕", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b설정저장.Appearance.Options.UseFont = true;
            this.b설정저장.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b설정저장.Dock = System.Windows.Forms.DockStyle.Right;
            this.b설정저장.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b설정저장.ImageOptions.SvgImage")));
            this.b설정저장.Location = new System.Drawing.Point(1449, 5);
            this.b설정저장.Name = "b설정저장";
            this.b설정저장.Size = new System.Drawing.Size(180, 46);
            this.b설정저장.TabIndex = 0;
            this.b설정저장.Text = "설정저장";
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1634, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 924);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1634, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 924);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1634, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 924);
            // 
            // bind검사설정
            // 
            this.bind검사설정.DataSource = typeof(SamhwaInspectionNeo.Schemas.검사설정자료);
            // 
            // bind모델자료
            // 
            this.bind모델자료.DataSource = typeof(SamhwaInspectionNeo.Schemas.모델자료);
            // 
            // GridControl1
            // 
            this.GridControl1.DataSource = this.bind검사설정;
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 56);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.MenuManager = this.barManager1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.Size = new System.Drawing.Size(1634, 868);
            this.GridControl1.TabIndex = 12;
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
            this.col검사일시,
            this.col검사항목,
            this.col검사그룹,
            this.col검사장치,
            this.col플로우,
            this.col지그,
            this.col결과분류,
            this.col측정단위,
            this.col기준값,
            this.col최소값,
            this.col최대값,
            this.col보정값,
            this.col교정값,
            this.col측정값,
            this.col마스터값,
            this.col마스터공차,
            this.col결과값,
            this.col측정결과});
            this.GridView1.DetailHeight = 375;
            this.GridView1.FooterPanelHeight = 22;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 22;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 18;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.Editable = false;
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.ShowGroupPanel = false;
            this.GridView1.OptionsView.ShowIndicator = false;
            this.GridView1.RowHeight = 21;
            // 
            // col검사일시
            // 
            this.col검사일시.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사일시.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사일시.FieldName = "검사일시";
            this.col검사일시.Name = "col검사일시";
            this.col검사일시.Visible = true;
            this.col검사일시.VisibleIndex = 0;
            // 
            // col검사항목
            // 
            this.col검사항목.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사항목.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사항목.FieldName = "검사항목";
            this.col검사항목.Name = "col검사항목";
            this.col검사항목.Visible = true;
            this.col검사항목.VisibleIndex = 1;
            // 
            // col검사그룹
            // 
            this.col검사그룹.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사그룹.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사그룹.FieldName = "검사그룹";
            this.col검사그룹.Name = "col검사그룹";
            this.col검사그룹.Visible = true;
            this.col검사그룹.VisibleIndex = 2;
            // 
            // col검사장치
            // 
            this.col검사장치.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사장치.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사장치.FieldName = "검사장치";
            this.col검사장치.Name = "col검사장치";
            this.col검사장치.Visible = true;
            this.col검사장치.VisibleIndex = 3;
            // 
            // col플로우
            // 
            this.col플로우.AppearanceHeader.Options.UseTextOptions = true;
            this.col플로우.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col플로우.FieldName = "플로우";
            this.col플로우.Name = "col플로우";
            // 
            // col지그
            // 
            this.col지그.AppearanceHeader.Options.UseTextOptions = true;
            this.col지그.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col지그.FieldName = "지그";
            this.col지그.Name = "col지그";
            // 
            // col결과분류
            // 
            this.col결과분류.AppearanceHeader.Options.UseTextOptions = true;
            this.col결과분류.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col결과분류.FieldName = "결과분류";
            this.col결과분류.Name = "col결과분류";
            this.col결과분류.Visible = true;
            this.col결과분류.VisibleIndex = 4;
            // 
            // col측정단위
            // 
            this.col측정단위.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정단위.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정단위.FieldName = "측정단위";
            this.col측정단위.Name = "col측정단위";
            this.col측정단위.Visible = true;
            this.col측정단위.VisibleIndex = 5;
            // 
            // col기준값
            // 
            this.col기준값.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준값.FieldName = "기준값";
            this.col기준값.Name = "col기준값";
            // 
            // col최소값
            // 
            this.col최소값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소값.FieldName = "최소값";
            this.col최소값.Name = "col최소값";
            // 
            // col최대값
            // 
            this.col최대값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대값.FieldName = "최대값";
            this.col최대값.Name = "col최대값";
            // 
            // col보정값
            // 
            this.col보정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col보정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col보정값.FieldName = "보정값";
            this.col보정값.Name = "col보정값";
            // 
            // col교정값
            // 
            this.col교정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col교정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col교정값.FieldName = "교정값";
            this.col교정값.Name = "col교정값";
            // 
            // col측정값
            // 
            this.col측정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정값.FieldName = "측정값";
            this.col측정값.Name = "col측정값";
            // 
            // col마스터값
            // 
            this.col마스터값.AppearanceHeader.Options.UseTextOptions = true;
            this.col마스터값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col마스터값.FieldName = "마스터값";
            this.col마스터값.Name = "col마스터값";
            this.col마스터값.Visible = true;
            this.col마스터값.VisibleIndex = 6;
            // 
            // col마스터공차
            // 
            this.col마스터공차.AppearanceHeader.Options.UseTextOptions = true;
            this.col마스터공차.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col마스터공차.FieldName = "마스터공차";
            this.col마스터공차.Name = "col마스터공차";
            this.col마스터공차.Visible = true;
            this.col마스터공차.VisibleIndex = 7;
            // 
            // col결과값
            // 
            this.col결과값.AppearanceHeader.Options.UseTextOptions = true;
            this.col결과값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col결과값.FieldName = "결과값";
            this.col결과값.Name = "col결과값";
            this.col결과값.Visible = true;
            this.col결과값.VisibleIndex = 8;
            // 
            // col측정결과
            // 
            this.col측정결과.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정결과.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정결과.FieldName = "측정결과";
            this.col측정결과.Name = "col측정결과";
            // 
            // SetMasterInspection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "SetMasterInspection";
            this.Size = new System.Drawing.Size(1634, 924);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e모델선택.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind검사설정)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind모델자료)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.SimpleButton b도구설정;
        private DevExpress.XtraEditors.LookUpEdit e모델선택;
        private DevExpress.XtraEditors.SimpleButton b설정저장;
        private System.Windows.Forms.BindingSource bind검사설정;
        private System.Windows.Forms.BindingSource bind모델자료;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton b수동검사;
        private DevExpress.XtraEditors.SimpleButton b보정값설정;
        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomView GridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col검사일시;
        private DevExpress.XtraGrid.Columns.GridColumn col검사항목;
        private DevExpress.XtraGrid.Columns.GridColumn col검사그룹;
        private DevExpress.XtraGrid.Columns.GridColumn col검사장치;
        private DevExpress.XtraGrid.Columns.GridColumn col플로우;
        private DevExpress.XtraGrid.Columns.GridColumn col지그;
        private DevExpress.XtraGrid.Columns.GridColumn col결과분류;
        private DevExpress.XtraGrid.Columns.GridColumn col측정단위;
        private DevExpress.XtraGrid.Columns.GridColumn col기준값;
        private DevExpress.XtraGrid.Columns.GridColumn col최소값;
        private DevExpress.XtraGrid.Columns.GridColumn col최대값;
        private DevExpress.XtraGrid.Columns.GridColumn col보정값;
        private DevExpress.XtraGrid.Columns.GridColumn col교정값;
        private DevExpress.XtraGrid.Columns.GridColumn col측정값;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터값;
        private DevExpress.XtraGrid.Columns.GridColumn col마스터공차;
        private DevExpress.XtraGrid.Columns.GridColumn col결과값;
        private DevExpress.XtraGrid.Columns.GridColumn col측정결과;
    }
}
