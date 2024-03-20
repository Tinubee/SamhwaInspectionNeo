namespace SamhwaInspectionNeo.UI.Control
{
    partial class Calibration
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Calibration));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.b조회 = new DevExpress.XtraEditors.SimpleButton();
            this.e지그선택 = new DevExpress.XtraEditors.LookUpEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.e플로우선택 = new DevExpress.XtraEditors.LookUpEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.Bind검사결과 = new System.Windows.Forms.BindingSource(this.components);
            this.GridControl1 = new MvUtils.CustomGrid();
            this.GridView1 = new MvUtils.CustomView();
            this.col검사일시 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사항목 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사그룹 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col검사장치 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col결과분류 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정단위 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col기준값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최소값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col최대값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col보정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col교정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col측정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.col결과값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcmm측정값 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ｅ교정계산 = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.e마진값 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.e지그선택.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e플로우선택.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사결과)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ｅ교정계산)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.e마진값)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AutoScroll = false;
            this.layoutControl1.Controls.Add(this.b조회);
            this.layoutControl1.Controls.Add(this.e지그선택);
            this.layoutControl1.Controls.Add(this.e플로우선택);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1228, 0, 650, 400);
            this.layoutControl1.Root = this.Root;
            this.layoutControl1.Size = new System.Drawing.Size(1181, 43);
            this.layoutControl1.TabIndex = 16;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // b조회
            // 
            this.b조회.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.b조회.Appearance.Options.UseFont = true;
            this.b조회.Cursor = System.Windows.Forms.Cursors.Hand;
            this.b조회.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("b조회.ImageOptions.SvgImage")));
            this.b조회.ImageOptions.SvgImageSize = new System.Drawing.Size(16, 16);
            this.b조회.Location = new System.Drawing.Point(980, 9);
            this.b조회.Name = "b조회";
            this.b조회.Size = new System.Drawing.Size(192, 24);
            this.b조회.StyleController = this.layoutControl1;
            this.b조회.TabIndex = 5;
            this.b조회.Text = "조  회";
            // 
            // e지그선택
            // 
            this.e지그선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.e지그선택.Location = new System.Drawing.Point(207, 7);
            this.e지그선택.MenuManager = this.barManager1;
            this.e지그선택.Name = "e지그선택";
            this.e지그선택.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e지그선택.Properties.Appearance.Options.UseFont = true;
            this.e지그선택.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e지그선택.Properties.NullText = "[ 지그위치 선택 ]";
            this.e지그선택.Size = new System.Drawing.Size(203, 26);
            this.e지그선택.StyleController = this.layoutControl1;
            this.e지그선택.TabIndex = 26;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControl1);
            this.barManager1.DockControls.Add(this.barDockControl2);
            this.barManager1.DockControls.Add(this.barDockControl3);
            this.barManager1.DockControls.Add(this.barDockControl4);
            this.barManager1.Form = this;
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            this.barDockControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControl1.Location = new System.Drawing.Point(0, 0);
            this.barDockControl1.Manager = this.barManager1;
            this.barDockControl1.Size = new System.Drawing.Size(1181, 0);
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            this.barDockControl2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControl2.Location = new System.Drawing.Point(0, 723);
            this.barDockControl2.Manager = this.barManager1;
            this.barDockControl2.Size = new System.Drawing.Size(1181, 0);
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            this.barDockControl3.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControl3.Location = new System.Drawing.Point(0, 0);
            this.barDockControl3.Manager = this.barManager1;
            this.barDockControl3.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            this.barDockControl4.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControl4.Location = new System.Drawing.Point(1181, 0);
            this.barDockControl4.Manager = this.barManager1;
            this.barDockControl4.Size = new System.Drawing.Size(0, 723);
            // 
            // e플로우선택
            // 
            this.e플로우선택.Cursor = System.Windows.Forms.Cursors.Hand;
            this.e플로우선택.Location = new System.Drawing.Point(7, 7);
            this.e플로우선택.MenuManager = this.barManager1;
            this.e플로우선택.Name = "e플로우선택";
            this.e플로우선택.Properties.Appearance.Font = new System.Drawing.Font("맑은 고딕", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.e플로우선택.Properties.Appearance.Options.UseFont = true;
            this.e플로우선택.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.e플로우선택.Properties.NullText = "[ 플로우 선택 ]";
            this.e플로우선택.Size = new System.Drawing.Size(196, 26);
            this.e플로우선택.StyleController = this.layoutControl1;
            this.e플로우선택.TabIndex = 25;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.Root.Name = "Root";
            this.Root.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.Root.Size = new System.Drawing.Size(1181, 43);
            this.Root.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.b조회;
            this.layoutControlItem3.Location = new System.Drawing.Point(971, 0);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(200, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(120, 32);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.layoutControlItem3.Size = new System.Drawing.Size(200, 33);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.e지그선택;
            this.layoutControlItem1.Location = new System.Drawing.Point(200, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(207, 33);
            this.layoutControlItem1.Text = "지그 위치 선택";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.e플로우선택;
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(200, 33);
            this.layoutControlItem2.Text = "플로우 선택";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.Location = new System.Drawing.Point(407, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(564, 33);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = null;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1181, 0);
            this.barDockControlRight.Manager = null;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 723);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 723);
            this.barDockControlBottom.Manager = null;
            this.barDockControlBottom.Size = new System.Drawing.Size(1181, 0);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = null;
            this.barDockControlTop.Size = new System.Drawing.Size(1181, 0);
            // 
            // Bind검사결과
            // 
            this.Bind검사결과.DataSource = typeof(SamhwaInspectionNeo.Schemas.검사자료);
            // 
            // GridControl1
            // 
            this.GridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GridControl1.Location = new System.Drawing.Point(0, 43);
            this.GridControl1.MainView = this.GridView1;
            this.GridControl1.Name = "GridControl1";
            this.GridControl1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ｅ교정계산,
            this.e마진값,
            this.repositoryItemToggleSwitch1});
            this.GridControl1.Size = new System.Drawing.Size(1181, 680);
            this.GridControl1.TabIndex = 25;
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
            this.col검사일시,
            this.col검사항목,
            this.col검사그룹,
            this.col검사장치,
            this.col결과분류,
            this.col측정단위,
            this.col기준값,
            this.col최소값,
            this.col최대값,
            this.col보정값,
            this.col교정값,
            this.col측정값,
            this.col결과값,
            this.colcmm측정값});
            this.GridView1.DetailHeight = 375;
            this.GridView1.FooterPanelHeight = 22;
            this.GridView1.GridControl = this.GridControl1;
            this.GridView1.GroupRowHeight = 22;
            this.GridView1.IndicatorWidth = 44;
            this.GridView1.MinColumnRowHeight = 24;
            this.GridView1.MinRowHeight = 16;
            this.GridView1.Name = "GridView1";
            this.GridView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.GridView1.OptionsClipboard.CopyColumnHeaders = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsCustomization.AllowColumnMoving = false;
            this.GridView1.OptionsCustomization.AllowGroup = false;
            this.GridView1.OptionsCustomization.AllowQuickHideColumns = false;
            this.GridView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.GridView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.GridView1.OptionsPrint.AutoWidth = false;
            this.GridView1.OptionsPrint.UsePrintStyles = false;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.GridView1.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
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
            this.col검사일시.OptionsColumn.TabStop = false;
            // 
            // col검사항목
            // 
            this.col검사항목.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사항목.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사항목.FieldName = "검사항목";
            this.col검사항목.Name = "col검사항목";
            this.col검사항목.OptionsColumn.TabStop = false;
            this.col검사항목.Visible = true;
            this.col검사항목.VisibleIndex = 0;
            // 
            // col검사그룹
            // 
            this.col검사그룹.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사그룹.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사그룹.FieldName = "검사그룹";
            this.col검사그룹.Name = "col검사그룹";
            this.col검사그룹.OptionsColumn.TabStop = false;
            // 
            // col검사장치
            // 
            this.col검사장치.AppearanceHeader.Options.UseTextOptions = true;
            this.col검사장치.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col검사장치.FieldName = "검사장치";
            this.col검사장치.Name = "col검사장치";
            this.col검사장치.OptionsColumn.TabStop = false;
            this.col검사장치.Visible = true;
            this.col검사장치.VisibleIndex = 1;
            // 
            // col결과분류
            // 
            this.col결과분류.AppearanceHeader.Options.UseTextOptions = true;
            this.col결과분류.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col결과분류.FieldName = "결과분류";
            this.col결과분류.Name = "col결과분류";
            this.col결과분류.OptionsColumn.TabStop = false;
            // 
            // col측정단위
            // 
            this.col측정단위.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정단위.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정단위.FieldName = "측정단위";
            this.col측정단위.Name = "col측정단위";
            this.col측정단위.OptionsColumn.TabStop = false;
            this.col측정단위.Visible = true;
            this.col측정단위.VisibleIndex = 2;
            // 
            // col기준값
            // 
            this.col기준값.AppearanceHeader.Options.UseTextOptions = true;
            this.col기준값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col기준값.FieldName = "기준값";
            this.col기준값.Name = "col기준값";
            this.col기준값.OptionsColumn.TabStop = false;
            // 
            // col최소값
            // 
            this.col최소값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최소값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최소값.FieldName = "최소값";
            this.col최소값.Name = "col최소값";
            this.col최소값.OptionsColumn.TabStop = false;
            // 
            // col최대값
            // 
            this.col최대값.AppearanceHeader.Options.UseTextOptions = true;
            this.col최대값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col최대값.FieldName = "최대값";
            this.col최대값.Name = "col최대값";
            this.col최대값.OptionsColumn.TabStop = false;
            // 
            // col보정값
            // 
            this.col보정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col보정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col보정값.FieldName = "보정값";
            this.col보정값.Name = "col보정값";
            this.col보정값.OptionsColumn.TabStop = false;
            // 
            // col교정값
            // 
            this.col교정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col교정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col교정값.FieldName = "교정값";
            this.col교정값.Name = "col교정값";
            this.col교정값.OptionsColumn.TabStop = false;
            this.col교정값.Visible = true;
            this.col교정값.VisibleIndex = 4;
            // 
            // col측정값
            // 
            this.col측정값.AppearanceHeader.Options.UseTextOptions = true;
            this.col측정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col측정값.FieldName = "측정값";
            this.col측정값.Name = "col측정값";
            this.col측정값.OptionsColumn.TabStop = false;
            this.col측정값.Visible = true;
            this.col측정값.VisibleIndex = 3;
            // 
            // col결과값
            // 
            this.col결과값.AppearanceHeader.Options.UseTextOptions = true;
            this.col결과값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.col결과값.FieldName = "결과값";
            this.col결과값.Name = "col결과값";
            this.col결과값.OptionsColumn.TabStop = false;
            // 
            // colcmm측정값
            // 
            this.colcmm측정값.AppearanceHeader.Options.UseTextOptions = true;
            this.colcmm측정값.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colcmm측정값.FieldName = "CMM측정값";
            this.colcmm측정값.Name = "colcmm측정값";
            this.colcmm측정값.Visible = true;
            this.colcmm측정값.VisibleIndex = 5;
            // 
            // ｅ교정계산
            // 
            this.ｅ교정계산.AutoHeight = false;
            this.ｅ교정계산.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.ｅ교정계산.Name = "ｅ교정계산";
            // 
            // e마진값
            // 
            this.e마진값.AutoHeight = false;
            this.e마진값.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo, "", -1, true, false, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.e마진값.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.e마진값.Name = "e마진값";
            // 
            // repositoryItemToggleSwitch1
            // 
            this.repositoryItemToggleSwitch1.AutoHeight = false;
            this.repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
            this.repositoryItemToggleSwitch1.OffText = "Off";
            this.repositoryItemToggleSwitch1.OnText = "On";
            // 
            // Calibration
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GridControl1);
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.Name = "Calibration";
            this.Size = new System.Drawing.Size(1181, 723);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.e지그선택.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e플로우선택.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Bind검사결과)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ｅ교정계산)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.e마진값)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton b조회;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private System.Windows.Forms.BindingSource Bind검사결과;
        private DevExpress.XtraEditors.LookUpEdit e지그선택;
        private DevExpress.XtraEditors.LookUpEdit e플로우선택;
        private MvUtils.CustomGrid GridControl1;
        private MvUtils.CustomView GridView1;
        private DevExpress.XtraGrid.Columns.GridColumn col검사일시;
        private DevExpress.XtraGrid.Columns.GridColumn col검사항목;
        private DevExpress.XtraGrid.Columns.GridColumn col검사그룹;
        private DevExpress.XtraGrid.Columns.GridColumn col검사장치;
        private DevExpress.XtraGrid.Columns.GridColumn col결과분류;
        private DevExpress.XtraGrid.Columns.GridColumn col측정단위;
        private DevExpress.XtraGrid.Columns.GridColumn col기준값;
        private DevExpress.XtraGrid.Columns.GridColumn col최소값;
        private DevExpress.XtraGrid.Columns.GridColumn col최대값;
        private DevExpress.XtraGrid.Columns.GridColumn col보정값;
        private DevExpress.XtraGrid.Columns.GridColumn col교정값;
        private DevExpress.XtraGrid.Columns.GridColumn col측정값;
        private DevExpress.XtraGrid.Columns.GridColumn col결과값;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit ｅ교정계산;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit e마진값;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
        private DevExpress.XtraGrid.Columns.GridColumn colcmm측정값;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}
