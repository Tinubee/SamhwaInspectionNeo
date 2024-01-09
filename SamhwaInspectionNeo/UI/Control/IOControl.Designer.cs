namespace SamhwaInspectionNeo.UI.Control
{
    partial class IOControl
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
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.customGrid1 = new MvUtils.CustomGrid();
            this.customView1 = new MvUtils.CustomView();
            this.repositoryItemToggleSwitch1 = new DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch();
            this.bind입력신호 = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.customGrid1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.customView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind입력신호)).BeginInit();
            this.SuspendLayout();
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.customGrid1);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(546, 797);
            this.groupControl1.TabIndex = 2;
            this.groupControl1.Text = "Input";
            // 
            // customGrid1
            // 
            this.customGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.customGrid1.Location = new System.Drawing.Point(2, 27);
            this.customGrid1.MainView = this.customView1;
            this.customGrid1.Name = "customGrid1";
            this.customGrid1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemToggleSwitch1});
            this.customGrid1.Size = new System.Drawing.Size(542, 768);
            this.customGrid1.TabIndex = 0;
            this.customGrid1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.customView1});
            // 
            // customView1
            // 
            this.customView1.AllowColumnMenu = true;
            this.customView1.AllowCustomMenu = true;
            this.customView1.AllowExport = true;
            this.customView1.AllowPrint = true;
            this.customView1.AllowSettingsMenu = false;
            this.customView1.AllowSummaryMenu = true;
            this.customView1.ApplyFocusedRow = true;
            this.customView1.Caption = "";
            this.customView1.DetailHeight = 375;
            this.customView1.FooterPanelHeight = 22;
            this.customView1.GridControl = this.customGrid1;
            this.customView1.GroupRowHeight = 22;
            this.customView1.IndicatorWidth = 44;
            this.customView1.MinColumnRowHeight = 24;
            this.customView1.MinRowHeight = 1;
            this.customView1.Name = "customView1";
            this.customView1.OptionsBehavior.Editable = false;
            this.customView1.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.Click;
            this.customView1.OptionsFilter.UseNewCustomFilterDialog = true;
            this.customView1.OptionsNavigation.EnterMoveNextColumn = true;
            this.customView1.OptionsPrint.AutoWidth = false;
            this.customView1.OptionsPrint.UsePrintStyles = false;
            this.customView1.OptionsView.ColumnHeaderAutoHeight = DevExpress.Utils.DefaultBoolean.False;
            this.customView1.OptionsView.ShowGroupPanel = false;
            this.customView1.RowHeight = 21;
            // 
            // repositoryItemToggleSwitch1
            // 
            this.repositoryItemToggleSwitch1.AutoHeight = false;
            this.repositoryItemToggleSwitch1.Name = "repositoryItemToggleSwitch1";
            this.repositoryItemToggleSwitch1.OffText = "Off";
            this.repositoryItemToggleSwitch1.OnText = "On";
            // 
            // IOControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupControl1);
            this.Name = "IOControl";
            this.Size = new System.Drawing.Size(546, 797);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.customGrid1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.customView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemToggleSwitch1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bind입력신호)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl groupControl1;
        private MvUtils.CustomGrid customGrid1;
        private MvUtils.CustomView customView1;
        private DevExpress.XtraEditors.Repository.RepositoryItemToggleSwitch repositoryItemToggleSwitch1;
        private System.Windows.Forms.BindingSource bind입력신호;
    }
}
