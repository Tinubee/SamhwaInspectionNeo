namespace SamhwaInspectionNeo.UI.Control
{
    partial class CamViewers
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
            this.xtraTabControl1 = new DevExpress.XtraTab.XtraTabControl();
            this.p치수검사 = new DevExpress.XtraTab.XtraTabPage();
            this.tablePanel1 = new DevExpress.Utils.Layout.TablePanel();
            this.Flow6Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.Flow5Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.Flow4Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.Flow3Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.Flow2Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.Flow1Viewer = new VMControls.Winform.Release.VmRenderControl();
            this.p트레이검사 = new DevExpress.XtraTab.XtraTabPage();
            this.trayViewer = new VMControls.Winform.Release.VmRenderControl();
            this.p상부표면검사 = new DevExpress.XtraTab.XtraTabPage();
            this.tablePanel2 = new DevExpress.Utils.Layout.TablePanel();
            this.UpSurfaceViewer6 = new VMControls.Winform.Release.VmRenderControl();
            this.UpSurfaceViewer5 = new VMControls.Winform.Release.VmRenderControl();
            this.UpSurfaceViewer4 = new VMControls.Winform.Release.VmRenderControl();
            this.UpSurfaceViewer3 = new VMControls.Winform.Release.VmRenderControl();
            this.UpSurfaceViewer2 = new VMControls.Winform.Release.VmRenderControl();
            this.UpSurfaceViewer1 = new VMControls.Winform.Release.VmRenderControl();
            this.p하부표면검사 = new DevExpress.XtraTab.XtraTabPage();
            this.tablePanel3 = new DevExpress.Utils.Layout.TablePanel();
            this.BottomSurfaceViewer6 = new VMControls.Winform.Release.VmRenderControl();
            this.BottomSurfaceViewer5 = new VMControls.Winform.Release.VmRenderControl();
            this.BottomSurfaceViewer4 = new VMControls.Winform.Release.VmRenderControl();
            this.BottomSurfaceViewer3 = new VMControls.Winform.Release.VmRenderControl();
            this.BottomSurfaceViewer2 = new VMControls.Winform.Release.VmRenderControl();
            this.BottomSurfaceViewer1 = new VMControls.Winform.Release.VmRenderControl();
            this.tablePanel4 = new DevExpress.Utils.Layout.TablePanel();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).BeginInit();
            this.xtraTabControl1.SuspendLayout();
            this.p치수검사.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).BeginInit();
            this.tablePanel1.SuspendLayout();
            this.p트레이검사.SuspendLayout();
            this.p상부표면검사.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).BeginInit();
            this.tablePanel2.SuspendLayout();
            this.p하부표면검사.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).BeginInit();
            this.tablePanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel4)).BeginInit();
            this.tablePanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // xtraTabControl1
            // 
            this.xtraTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControl1.Location = new System.Drawing.Point(0, 0);
            this.xtraTabControl1.Name = "xtraTabControl1";
            this.xtraTabControl1.SelectedTabPage = this.p치수검사;
            this.xtraTabControl1.Size = new System.Drawing.Size(1587, 963);
            this.xtraTabControl1.TabIndex = 0;
            this.xtraTabControl1.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.p치수검사,
            this.p트레이검사,
            this.p상부표면검사,
            this.p하부표면검사});
            // 
            // p치수검사
            // 
            this.p치수검사.Controls.Add(this.tablePanel1);
            this.p치수검사.Name = "p치수검사";
            this.p치수검사.Size = new System.Drawing.Size(1585, 933);
            this.p치수검사.Text = "치수검사";
            // 
            // tablePanel1
            // 
            this.tablePanel1.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel1.Controls.Add(this.Flow6Viewer);
            this.tablePanel1.Controls.Add(this.Flow5Viewer);
            this.tablePanel1.Controls.Add(this.Flow4Viewer);
            this.tablePanel1.Controls.Add(this.Flow3Viewer);
            this.tablePanel1.Controls.Add(this.Flow2Viewer);
            this.tablePanel1.Controls.Add(this.Flow1Viewer);
            this.tablePanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel1.Location = new System.Drawing.Point(0, 0);
            this.tablePanel1.Name = "tablePanel1";
            this.tablePanel1.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F)});
            this.tablePanel1.Size = new System.Drawing.Size(1585, 933);
            this.tablePanel1.TabIndex = 0;
            this.tablePanel1.UseSkinIndents = true;
            // 
            // Flow6Viewer
            // 
            this.Flow6Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow6Viewer, 2);
            this.Flow6Viewer.CoordinateInfoVisible = true;
            this.Flow6Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow6Viewer.ImageSource = null;
            this.Flow6Viewer.IsShowCustomROIMenu = false;
            this.Flow6Viewer.Location = new System.Drawing.Point(1074, 471);
            this.Flow6Viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Flow6Viewer.ModuleSource = null;
            this.Flow6Viewer.Name = "Flow6Viewer";
            this.tablePanel1.SetRow(this.Flow6Viewer, 1);
            this.Flow6Viewer.Size = new System.Drawing.Size(496, 446);
            this.Flow6Viewer.TabIndex = 5;
            // 
            // Flow5Viewer
            // 
            this.Flow5Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow5Viewer, 1);
            this.Flow5Viewer.CoordinateInfoVisible = true;
            this.Flow5Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow5Viewer.ImageSource = null;
            this.Flow5Viewer.IsShowCustomROIMenu = false;
            this.Flow5Viewer.Location = new System.Drawing.Point(519, 471);
            this.Flow5Viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Flow5Viewer.ModuleSource = null;
            this.Flow5Viewer.Name = "Flow5Viewer";
            this.tablePanel1.SetRow(this.Flow5Viewer, 1);
            this.Flow5Viewer.Size = new System.Drawing.Size(547, 446);
            this.Flow5Viewer.TabIndex = 4;
            // 
            // Flow4Viewer
            // 
            this.Flow4Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow4Viewer, 0);
            this.Flow4Viewer.CoordinateInfoVisible = true;
            this.Flow4Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow4Viewer.ImageSource = null;
            this.Flow4Viewer.IsShowCustomROIMenu = false;
            this.Flow4Viewer.Location = new System.Drawing.Point(15, 471);
            this.Flow4Viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Flow4Viewer.ModuleSource = null;
            this.Flow4Viewer.Name = "Flow4Viewer";
            this.tablePanel1.SetRow(this.Flow4Viewer, 1);
            this.Flow4Viewer.Size = new System.Drawing.Size(496, 446);
            this.Flow4Viewer.TabIndex = 3;
            // 
            // Flow3Viewer
            // 
            this.Flow3Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow3Viewer, 2);
            this.Flow3Viewer.CoordinateInfoVisible = true;
            this.Flow3Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow3Viewer.ImageSource = null;
            this.Flow3Viewer.IsShowCustomROIMenu = false;
            this.Flow3Viewer.Location = new System.Drawing.Point(1074, 15);
            this.Flow3Viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Flow3Viewer.ModuleSource = null;
            this.Flow3Viewer.Name = "Flow3Viewer";
            this.tablePanel1.SetRow(this.Flow3Viewer, 0);
            this.Flow3Viewer.Size = new System.Drawing.Size(496, 446);
            this.Flow3Viewer.TabIndex = 2;
            // 
            // Flow2Viewer
            // 
            this.Flow2Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow2Viewer, 1);
            this.Flow2Viewer.CoordinateInfoVisible = true;
            this.Flow2Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow2Viewer.ImageSource = null;
            this.Flow2Viewer.IsShowCustomROIMenu = false;
            this.Flow2Viewer.Location = new System.Drawing.Point(519, 15);
            this.Flow2Viewer.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Flow2Viewer.ModuleSource = null;
            this.Flow2Viewer.Name = "Flow2Viewer";
            this.tablePanel1.SetRow(this.Flow2Viewer, 0);
            this.Flow2Viewer.Size = new System.Drawing.Size(547, 446);
            this.Flow2Viewer.TabIndex = 1;
            // 
            // Flow1Viewer
            // 
            this.Flow1Viewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel1.SetColumn(this.Flow1Viewer, 0);
            this.Flow1Viewer.CoordinateInfoVisible = true;
            this.Flow1Viewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Flow1Viewer.ImageSource = null;
            this.Flow1Viewer.IsShowCustomROIMenu = false;
            this.Flow1Viewer.Location = new System.Drawing.Point(15, 14);
            this.Flow1Viewer.Margin = new System.Windows.Forms.Padding(4);
            this.Flow1Viewer.ModuleSource = null;
            this.Flow1Viewer.Name = "Flow1Viewer";
            this.tablePanel1.SetRow(this.Flow1Viewer, 0);
            this.Flow1Viewer.Size = new System.Drawing.Size(496, 448);
            this.Flow1Viewer.TabIndex = 0;
            // 
            // p트레이검사
            // 
            this.p트레이검사.Controls.Add(this.tablePanel4);
            this.p트레이검사.Name = "p트레이검사";
            this.p트레이검사.Size = new System.Drawing.Size(1585, 933);
            this.p트레이검사.Text = "트레이검사";
            // 
            // trayViewer
            // 
            this.trayViewer.BackColor = System.Drawing.Color.Black;
            this.tablePanel4.SetColumn(this.trayViewer, 0);
            this.trayViewer.CoordinateInfoVisible = true;
            this.trayViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.trayViewer.ImageSource = null;
            this.trayViewer.IsShowCustomROIMenu = false;
            this.trayViewer.Location = new System.Drawing.Point(15, 14);
            this.trayViewer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.trayViewer.ModuleSource = null;
            this.trayViewer.Name = "trayViewer";
            this.tablePanel4.SetRow(this.trayViewer, 0);
            this.trayViewer.Size = new System.Drawing.Size(1555, 904);
            this.trayViewer.TabIndex = 0;
            // 
            // p상부표면검사
            // 
            this.p상부표면검사.Controls.Add(this.tablePanel2);
            this.p상부표면검사.Name = "p상부표면검사";
            this.p상부표면검사.Size = new System.Drawing.Size(1585, 933);
            this.p상부표면검사.Text = "상부표면검사";
            // 
            // tablePanel2
            // 
            this.tablePanel2.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer6);
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer5);
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer4);
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer3);
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer2);
            this.tablePanel2.Controls.Add(this.UpSurfaceViewer1);
            this.tablePanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel2.Location = new System.Drawing.Point(0, 0);
            this.tablePanel2.Name = "tablePanel2";
            this.tablePanel2.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F)});
            this.tablePanel2.Size = new System.Drawing.Size(1585, 933);
            this.tablePanel2.TabIndex = 1;
            this.tablePanel2.UseSkinIndents = true;
            // 
            // UpSurfaceViewer6
            // 
            this.UpSurfaceViewer6.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer6, 2);
            this.UpSurfaceViewer6.CoordinateInfoVisible = true;
            this.UpSurfaceViewer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer6.ImageSource = null;
            this.UpSurfaceViewer6.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer6.Location = new System.Drawing.Point(1074, 471);
            this.UpSurfaceViewer6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpSurfaceViewer6.ModuleSource = null;
            this.UpSurfaceViewer6.Name = "UpSurfaceViewer6";
            this.tablePanel2.SetRow(this.UpSurfaceViewer6, 1);
            this.UpSurfaceViewer6.Size = new System.Drawing.Size(496, 446);
            this.UpSurfaceViewer6.TabIndex = 5;
            // 
            // UpSurfaceViewer5
            // 
            this.UpSurfaceViewer5.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer5, 1);
            this.UpSurfaceViewer5.CoordinateInfoVisible = true;
            this.UpSurfaceViewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer5.ImageSource = null;
            this.UpSurfaceViewer5.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer5.Location = new System.Drawing.Point(519, 471);
            this.UpSurfaceViewer5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpSurfaceViewer5.ModuleSource = null;
            this.UpSurfaceViewer5.Name = "UpSurfaceViewer5";
            this.tablePanel2.SetRow(this.UpSurfaceViewer5, 1);
            this.UpSurfaceViewer5.Size = new System.Drawing.Size(547, 446);
            this.UpSurfaceViewer5.TabIndex = 4;
            // 
            // UpSurfaceViewer4
            // 
            this.UpSurfaceViewer4.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer4, 0);
            this.UpSurfaceViewer4.CoordinateInfoVisible = true;
            this.UpSurfaceViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer4.ImageSource = null;
            this.UpSurfaceViewer4.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer4.Location = new System.Drawing.Point(15, 471);
            this.UpSurfaceViewer4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpSurfaceViewer4.ModuleSource = null;
            this.UpSurfaceViewer4.Name = "UpSurfaceViewer4";
            this.tablePanel2.SetRow(this.UpSurfaceViewer4, 1);
            this.UpSurfaceViewer4.Size = new System.Drawing.Size(496, 446);
            this.UpSurfaceViewer4.TabIndex = 3;
            // 
            // UpSurfaceViewer3
            // 
            this.UpSurfaceViewer3.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer3, 2);
            this.UpSurfaceViewer3.CoordinateInfoVisible = true;
            this.UpSurfaceViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer3.ImageSource = null;
            this.UpSurfaceViewer3.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer3.Location = new System.Drawing.Point(1074, 15);
            this.UpSurfaceViewer3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpSurfaceViewer3.ModuleSource = null;
            this.UpSurfaceViewer3.Name = "UpSurfaceViewer3";
            this.tablePanel2.SetRow(this.UpSurfaceViewer3, 0);
            this.UpSurfaceViewer3.Size = new System.Drawing.Size(496, 446);
            this.UpSurfaceViewer3.TabIndex = 2;
            // 
            // UpSurfaceViewer2
            // 
            this.UpSurfaceViewer2.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer2, 1);
            this.UpSurfaceViewer2.CoordinateInfoVisible = true;
            this.UpSurfaceViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer2.ImageSource = null;
            this.UpSurfaceViewer2.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer2.Location = new System.Drawing.Point(519, 15);
            this.UpSurfaceViewer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpSurfaceViewer2.ModuleSource = null;
            this.UpSurfaceViewer2.Name = "UpSurfaceViewer2";
            this.tablePanel2.SetRow(this.UpSurfaceViewer2, 0);
            this.UpSurfaceViewer2.Size = new System.Drawing.Size(547, 446);
            this.UpSurfaceViewer2.TabIndex = 1;
            // 
            // UpSurfaceViewer1
            // 
            this.UpSurfaceViewer1.BackColor = System.Drawing.Color.Black;
            this.tablePanel2.SetColumn(this.UpSurfaceViewer1, 0);
            this.UpSurfaceViewer1.CoordinateInfoVisible = true;
            this.UpSurfaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UpSurfaceViewer1.ImageSource = null;
            this.UpSurfaceViewer1.IsShowCustomROIMenu = false;
            this.UpSurfaceViewer1.Location = new System.Drawing.Point(15, 14);
            this.UpSurfaceViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.UpSurfaceViewer1.ModuleSource = null;
            this.UpSurfaceViewer1.Name = "UpSurfaceViewer1";
            this.tablePanel2.SetRow(this.UpSurfaceViewer1, 0);
            this.UpSurfaceViewer1.Size = new System.Drawing.Size(496, 448);
            this.UpSurfaceViewer1.TabIndex = 0;
            // 
            // p하부표면검사
            // 
            this.p하부표면검사.Controls.Add(this.tablePanel3);
            this.p하부표면검사.Name = "p하부표면검사";
            this.p하부표면검사.Size = new System.Drawing.Size(1585, 933);
            this.p하부표면검사.Text = "하부표면검사";
            // 
            // tablePanel3
            // 
            this.tablePanel3.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F),
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 50F)});
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer6);
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer5);
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer4);
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer3);
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer2);
            this.tablePanel3.Controls.Add(this.BottomSurfaceViewer1);
            this.tablePanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel3.Location = new System.Drawing.Point(0, 0);
            this.tablePanel3.Name = "tablePanel3";
            this.tablePanel3.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F),
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 26F)});
            this.tablePanel3.Size = new System.Drawing.Size(1585, 933);
            this.tablePanel3.TabIndex = 1;
            this.tablePanel3.UseSkinIndents = true;
            // 
            // BottomSurfaceViewer6
            // 
            this.BottomSurfaceViewer6.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer6, 2);
            this.BottomSurfaceViewer6.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer6.ImageSource = null;
            this.BottomSurfaceViewer6.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer6.Location = new System.Drawing.Point(1074, 471);
            this.BottomSurfaceViewer6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BottomSurfaceViewer6.ModuleSource = null;
            this.BottomSurfaceViewer6.Name = "BottomSurfaceViewer6";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer6, 1);
            this.BottomSurfaceViewer6.Size = new System.Drawing.Size(496, 446);
            this.BottomSurfaceViewer6.TabIndex = 5;
            // 
            // BottomSurfaceViewer5
            // 
            this.BottomSurfaceViewer5.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer5, 1);
            this.BottomSurfaceViewer5.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer5.ImageSource = null;
            this.BottomSurfaceViewer5.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer5.Location = new System.Drawing.Point(519, 471);
            this.BottomSurfaceViewer5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BottomSurfaceViewer5.ModuleSource = null;
            this.BottomSurfaceViewer5.Name = "BottomSurfaceViewer5";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer5, 1);
            this.BottomSurfaceViewer5.Size = new System.Drawing.Size(547, 446);
            this.BottomSurfaceViewer5.TabIndex = 4;
            // 
            // BottomSurfaceViewer4
            // 
            this.BottomSurfaceViewer4.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer4, 0);
            this.BottomSurfaceViewer4.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer4.ImageSource = null;
            this.BottomSurfaceViewer4.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer4.Location = new System.Drawing.Point(15, 471);
            this.BottomSurfaceViewer4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BottomSurfaceViewer4.ModuleSource = null;
            this.BottomSurfaceViewer4.Name = "BottomSurfaceViewer4";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer4, 1);
            this.BottomSurfaceViewer4.Size = new System.Drawing.Size(496, 446);
            this.BottomSurfaceViewer4.TabIndex = 3;
            // 
            // BottomSurfaceViewer3
            // 
            this.BottomSurfaceViewer3.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer3, 2);
            this.BottomSurfaceViewer3.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer3.ImageSource = null;
            this.BottomSurfaceViewer3.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer3.Location = new System.Drawing.Point(1074, 15);
            this.BottomSurfaceViewer3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BottomSurfaceViewer3.ModuleSource = null;
            this.BottomSurfaceViewer3.Name = "BottomSurfaceViewer3";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer3, 0);
            this.BottomSurfaceViewer3.Size = new System.Drawing.Size(496, 446);
            this.BottomSurfaceViewer3.TabIndex = 2;
            // 
            // BottomSurfaceViewer2
            // 
            this.BottomSurfaceViewer2.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer2, 1);
            this.BottomSurfaceViewer2.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer2.ImageSource = null;
            this.BottomSurfaceViewer2.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer2.Location = new System.Drawing.Point(519, 15);
            this.BottomSurfaceViewer2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BottomSurfaceViewer2.ModuleSource = null;
            this.BottomSurfaceViewer2.Name = "BottomSurfaceViewer2";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer2, 0);
            this.BottomSurfaceViewer2.Size = new System.Drawing.Size(547, 446);
            this.BottomSurfaceViewer2.TabIndex = 1;
            // 
            // BottomSurfaceViewer1
            // 
            this.BottomSurfaceViewer1.BackColor = System.Drawing.Color.Black;
            this.tablePanel3.SetColumn(this.BottomSurfaceViewer1, 0);
            this.BottomSurfaceViewer1.CoordinateInfoVisible = true;
            this.BottomSurfaceViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BottomSurfaceViewer1.ImageSource = null;
            this.BottomSurfaceViewer1.IsShowCustomROIMenu = false;
            this.BottomSurfaceViewer1.Location = new System.Drawing.Point(15, 14);
            this.BottomSurfaceViewer1.Margin = new System.Windows.Forms.Padding(4);
            this.BottomSurfaceViewer1.ModuleSource = null;
            this.BottomSurfaceViewer1.Name = "BottomSurfaceViewer1";
            this.tablePanel3.SetRow(this.BottomSurfaceViewer1, 0);
            this.BottomSurfaceViewer1.Size = new System.Drawing.Size(496, 448);
            this.BottomSurfaceViewer1.TabIndex = 0;
            // 
            // tablePanel4
            // 
            this.tablePanel4.Columns.AddRange(new DevExpress.Utils.Layout.TablePanelColumn[] {
            new DevExpress.Utils.Layout.TablePanelColumn(DevExpress.Utils.Layout.TablePanelEntityStyle.Relative, 55F)});
            this.tablePanel4.Controls.Add(this.trayViewer);
            this.tablePanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tablePanel4.Location = new System.Drawing.Point(0, 0);
            this.tablePanel4.Name = "tablePanel4";
            this.tablePanel4.Rows.AddRange(new DevExpress.Utils.Layout.TablePanelRow[] {
            new DevExpress.Utils.Layout.TablePanelRow(DevExpress.Utils.Layout.TablePanelEntityStyle.Absolute, 26F)});
            this.tablePanel4.Size = new System.Drawing.Size(1585, 933);
            this.tablePanel4.TabIndex = 1;
            this.tablePanel4.UseSkinIndents = true;
            // 
            // CamViewers
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xtraTabControl1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "CamViewers";
            this.Size = new System.Drawing.Size(1587, 963);
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControl1)).EndInit();
            this.xtraTabControl1.ResumeLayout(false);
            this.p치수검사.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel1)).EndInit();
            this.tablePanel1.ResumeLayout(false);
            this.p트레이검사.ResumeLayout(false);
            this.p상부표면검사.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel2)).EndInit();
            this.tablePanel2.ResumeLayout(false);
            this.p하부표면검사.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel3)).EndInit();
            this.tablePanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tablePanel4)).EndInit();
            this.tablePanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl xtraTabControl1;
        private DevExpress.XtraTab.XtraTabPage p치수검사;
        private DevExpress.XtraTab.XtraTabPage p트레이검사;
        private DevExpress.XtraTab.XtraTabPage p상부표면검사;
        private DevExpress.XtraTab.XtraTabPage p하부표면검사;
        private DevExpress.Utils.Layout.TablePanel tablePanel1;
        private VMControls.Winform.Release.VmRenderControl Flow6Viewer;
        private VMControls.Winform.Release.VmRenderControl Flow5Viewer;
        private VMControls.Winform.Release.VmRenderControl Flow4Viewer;
        private VMControls.Winform.Release.VmRenderControl Flow3Viewer;
        private VMControls.Winform.Release.VmRenderControl Flow2Viewer;
        private VMControls.Winform.Release.VmRenderControl Flow1Viewer;
        private VMControls.Winform.Release.VmRenderControl trayViewer;
        private DevExpress.Utils.Layout.TablePanel tablePanel2;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer6;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer5;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer4;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer3;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer2;
        private VMControls.Winform.Release.VmRenderControl UpSurfaceViewer1;
        private DevExpress.Utils.Layout.TablePanel tablePanel3;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer6;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer5;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer4;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer3;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer2;
        private VMControls.Winform.Release.VmRenderControl BottomSurfaceViewer1;
        private DevExpress.Utils.Layout.TablePanel tablePanel4;
    }
}
