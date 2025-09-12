using DevExpress.Export;
using DevExpress.Utils;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraPrinting;
using MvUtils;
using SamhwaInspectionNeo.Schemas;
using SamhwaInspectionNeo.Schemas.Reports;
using SamhwaInspectionNeo.UI.Form;
using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static SamhwaInspectionNeo.Schemas.피벗자료;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class ResultsPivot : XtraUserControl
    {
        public ResultsPivot() => InitializeComponent();

        private Results.LocalizationResults 번역 = new Results.LocalizationResults();
        private 피벗자료 피벗자료 = new 피벗자료();
        private 검사내역 검사내역;

        public void Init()
        {
            this.BindLocalization.DataSource = this.번역;
            this.e시작일자.DateTime = DateTime.Now;
            this.e종료일자.DateTime = DateTime.Now;
            this.b자료조회.Click += 자료조회;
            this.b엑셀파일.Click += 엑셀파일;

            foreach (PropertyInfo p in typeof(피벗정보).GetProperties())
            {
                피벗자료.컬럼정보 컬럼 = new 피벗자료.컬럼정보(p);
                BandedGridColumn gCol = this.GridView1.Columns[p.Name];
                if (gCol == null)
                {
                    gCol = new BandedGridColumn();
                    this.GridView1.Columns.Add(gCol);
                    GridBand band = GetBand(컬럼.BandName);
                    band.Columns.Add(gCol);
                    컬럼.Set(gCol);
                }
                else gCol.Caption = 컬럼.Caption;
            }
            foreach (GridBand band in this.GridView1.Bands)
                if (!band.Columns.Any(g => g.Visible)) band.Visible = false;

            //this.GridView1.Init(this.barManager1);
            this.GridView1.FocusedRowChanged += FocusedRowChanged;
            this.GridView1.RowStyle += GridViewRowStyle;
            this.GridView1.CustomDrawCell += CustomDrawCell;
            //this.GridView1.CustomDrawFooterCell += CustomDrawFooterCell;
            if (Global.환경설정.동작구분 == 동작구분.Live)
            {
                this.GridView1.OptionsBehavior.Editable = false;
                this.GridView1.DoubleClick += 검사결과보기;
            }
            else if (Global.환경설정.동작구분 == 동작구분.LocalTest)
            {
                this.GridView1.OptionsBehavior.Editable = true;
                //this.l저장.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                //this.b저장.Click += (object sender, EventArgs e) => Global.검사자료.Save();
                this.GridView1.AddDeleteMenuItem(정보삭제);
                this.GridView1.DoubleClick += 일괄수정;
                //this.b적용.Click += 일괄변경;
                //this.b취소.Click += (object sender, EventArgs e) => { this.popupControlContainer1.HidePopup(); };
            }
        }

        private GridBand GetBand(String 명칭)
        {
            GridBand band = this.GridView1.Bands.Where(g => g.Caption == 명칭).FirstOrDefault();
            if (band != null) return band;
            band = new GridBand() { Caption = 명칭, VisibleIndex = this.GridView1.Bands.Count };
            band.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
            band.AppearanceHeader.Options.UseTextOptions = true;
            this.GridView1.Bands.Add(band);
            return band;
        }

        private void 자료조회(object sender, EventArgs e) => this.LoadData(this.e시작일자.DateTime, this.e종료일자.DateTime, false);
        private void LoadData(DateTime 시작, DateTime 종료, Boolean useSummary) => 데이터정리(Global.환경설정.선택모델, 시작, 종료, useSummary); //LoadData(Global.환경설정.선택모델, 시작, 종료, useSummary);
        private void LoadData(모델구분 모델, DateTime 시작, DateTime 종료, Boolean useSummary)
        {
            if (모델 == 모델구분.None) return;
            this.GridControl1.DataSource = null;
            피벗자료.Clear();
            DateTime s = DateTime.Now;
            피벗자료.Load(Global.검사자료.GetData(시작.AddDays(-1), 종료.AddDays(1), 모델), useSummary);
            Debug.WriteLine((DateTime.Now - s).TotalMilliseconds, "Load Time");
            this.GridControl1.DataSource = 피벗자료;
            this.GridView1.BestFitColumns();
        }

        private void 데이터정리(모델구분 모델, DateTime 시작, DateTime 종료, Boolean useSummary)
        {
            this.GridControl1.DataSource = null;
            this.GridView1.Columns.Clear();
            this.GridView1.FormatRules.Clear();
            if (모델 == 모델구분.None) return;

            this.검사내역?.Dispose();
            this.검사내역 = new 검사내역(모델);
            this.검사내역.Load(Global.검사자료.GetData(시작, 종료, 모델));

            this.GridControl1.DataSource = this.검사내역.검사자료;
            foreach (BandedGridColumn gCol in this.GridView1.Columns)
            {
                if (!this.검사내역.검사자료.Columns.Contains(gCol.FieldName)) continue;
                DataColumn dCol = this.검사내역.검사자료.Columns[gCol.FieldName];
                gCol.Caption = dCol.Caption;
                this.GetBand(this.검사내역.GetBand(dCol))?.Columns.Add(gCol);
                gCol.Visible = this.검사내역.GetVisiable(dCol);
                gCol.Fixed = this.검사내역.GetFixedStyle(dCol);
                gCol.ToolTip = this.검사내역.GetToolTip(dCol);
                FormatType FormatType = this.검사내역.GetFormatType(dCol);
                if (FormatType != DevExpress.Utils.FormatType.None)
                {
                    gCol.DisplayFormat.FormatType = FormatType;
                    gCol.DisplayFormat.FormatString = this.검사내역.GetFormatString(dCol);
                    if (FormatType == DevExpress.Utils.FormatType.Numeric)
                    {
                        if (gCol.DisplayFormat.FormatString.Contains(",")) gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Default;
                        else gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
                    }
                    else if (FormatType == DevExpress.Utils.FormatType.DateTime)
                        gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;

                    Object sVal = this.검사내역.GetStandardValue(dCol);
                    if (sVal != null)
                    {
                        gCol.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Custom;
                        gCol.SummaryItem.DisplayFormat = gCol.DisplayFormat.FormatString;
                        gCol.SummaryItem.FieldName = gCol.FieldName;
                        gCol.SummaryItem.Tag = sVal;
                    }
                }
                else gCol.AppearanceCell.TextOptions.HAlignment = HorzAlignment.Center;
            }

            foreach (GridBand band in this.GridView1.Bands)
                band.Visible = band.Columns.Count > 0;

            foreach (검사정보 설정 in Global.모델자료.GetItem(모델).검사설정)
                this.CreateFormatConditionRule(설정);

            this.CreateFormatConditionRule(this.GridView1.Columns[nameof(검사결과.측정결과)], 결과구분.OK);
            this.GridView1.BestFitColumns();
        }
        private void CreateFormatConditionRule(GridColumn tCol, Object value)
        {
            if (tCol == null) return;
            GridFormatRule Rule = new GridFormatRule();
            FormatConditionRuleValue Condition = new FormatConditionRuleValue();
            Rule.Name = tCol.FieldName + "_Rule";
            Condition.PredefinedName = "Red Text";
            Condition.Condition = FormatCondition.NotEqual;
            Condition.Value1 = value;
            Rule.Rule = Condition;
            Rule.Column = tCol;
            Rule.ColumnApplyTo = tCol;
            this.GridView1.FormatRules.Add(Rule);
        }
        private void CreateFormatConditionRule(검사정보 설정)
        {
            String name = $"C{설정.검사항목}";
            BandedGridColumn tCol = this.GridView1.Columns[name];
            BandedGridColumn rCol = this.GridView1.Columns[name + "R"];
            if (tCol == null || rCol == null) return;
            GridFormatRule Rule = new GridFormatRule();
            FormatConditionRuleValue Condition = new FormatConditionRuleValue();
            Rule.Name = name + "_Rule";
            Condition.PredefinedName = "Red Text";
            Condition.Condition = FormatCondition.NotEqual;
            Condition.Value1 = true;
            Rule.Rule = Condition;
            Rule.Column = rCol;
            Rule.ColumnApplyTo = tCol;
            this.GridView1.FormatRules.Add(Rule);
        }

        private void 정보삭제(object sender, ItemClickEventArgs e)
        {
            if (this.GridView1.SelectedRowsCount < 1) return;
            //if (!Global.Confirm(this.FindForm(), 번역.자료삭제)) return;

            ArrayList 자료 = this.GridView1.SelectedData() as ArrayList;
            foreach (피벗정보 피벗 in 자료)
                Global.검사자료.결과삭제(피벗.검사);
            this.GridView1.DeleteSelectedRows();
        }

        private void FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            GridView view = sender as GridView;
            피벗정보 정보 = view.GetFocusedRow() as 피벗정보;
            if (정보 == null) return;
            view.OptionsBehavior.Editable = 정보.검사 != null;
        }

        private static Color GetBackColor(GridView view, Int32 rowHandle) => GetBackColor(view.GetRow(rowHandle) as 피벗정보);
        private static Color GetBackColor(피벗정보 정보)
        {
            if (정보 == null || 정보.집계 == 집계항목.None) return Color.Empty;
            if (정보.집계 == 집계항목.CMM측정) return Color.FromArgb(189, 215, 238);
            if (정보.집계 == 집계항목.반복최대) return Color.FromArgb(219, 219, 219);
            if (정보.집계 == 집계항목.반복최소) return Color.FromArgb(219, 219, 219);
            if (정보.집계 == 집계항목.반복평균) return Color.FromArgb(219, 219, 219);
            if (정보.집계 == 집계항목.반복표준) return Color.FromArgb(198, 239, 206);
            if (정보.집계 == 집계항목.반복편차) return Color.FromArgb(255, 199, 206);
            if (정보.집계 == 집계항목.정합편차) return Color.FromArgb(255, 235, 156);
            if (정보.집계 == 집계항목.정합평균) return Color.FromArgb(198, 239, 206);
            return Color.Empty;
        }
        private static Color GetForeColor(GridView view, Int32 rowHandle, String fieldName)
        {
            if (view == null || rowHandle < 0) return Color.Empty;
            피벗정보 정보 = view.GetRow(rowHandle) as 피벗정보;
            if (정보 == null || 정보.집계 == 집계항목.CMM측정) return Color.Empty;
            결과구분? 결과 = null;
            if (fieldName == nameof(피벗정보.측정결과))
            {
                결과 = 정보.측정결과;
                if (결과 == null || 결과 == 결과구분.OK) return Color.Empty;
                return 환경설정.결과표현색상((결과구분)결과);
            }

            컬럼정보 컬럼 = 컬럼정보.Get(view.Columns[fieldName]);
            if (컬럼 == null) return Color.Empty;
            결과 = 정보.Result(컬럼.항목);
            if (결과 != null && 결과 != 결과구분.OK) 환경설정.결과표현색상((결과구분)결과);

            Object val = view.GetRowCellValue(rowHandle, fieldName);
            if (val == null) return Color.Empty;

            return Color.Empty;
            //Decimal value = (Decimal)val;
            //switch (정보.집계)
            //{
            //    case 집계항목.반복최대:
            //    case 집계항목.반복최소:
            //    case 집계항목.반복평균: return value < 컬럼.최소 || value > 컬럼.최대 ? Color.OrangeRed : Color.Empty;
            //    case 집계항목.반복표준: return value > 0.015m ? Color.OrangeRed : Color.Empty;
            //    case 집계항목.반복편차: return value > 0.030m ? Color.OrangeRed : Color.Empty;
            //    case 집계항목.정합편차: return value > 0.050m ? Color.OrangeRed : Color.Empty;
            //    case 집계항목.정합평균: return value > 0.040m ? Color.OrangeRed : Color.Empty;
            //    default: return Color.Empty;
            //}
        }

        private void GridViewRowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            피벗정보 정보 = view.GetRow(e.RowHandle) as 피벗정보;
            if (정보 == null) return;
            Color color = GetBackColor(정보);
            if (color == Color.Empty) return;
            e.Appearance.BackColor = color;
            e.Appearance.ForeColor = Color.Black;
            e.Appearance.Font = MvUtils.Utils.DefaultFont(FontStyle.Bold);
        }

        private void CustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
        {
            if (e.Column.Tag == null && e.Column.FieldName != nameof(피벗정보.측정결과)) return;
            GridView view = sender as GridView;
            Color color = GetForeColor(view, e.RowHandle, e.Column.FieldName);
            if (color != Color.Empty) e.Appearance.ForeColor = color;
        }

        //private void CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e) { }

        private void 검사결과보기(object sender, EventArgs e)
        {
            //피벗정보 정보 = this.GridView1.GetFocusedRow() as 피벗정보;
            //if (정보 == null || 정보.검사일시 == null || 정보.검사코드 == null) return;
            //DateTime 일시 = (DateTime)정보.검사일시;
            //Int32 코드 = (Int32)정보.검사코드;
            //검사결과 결과 = Global.검사자료.GetItem(일시, Global.환경설정.선택모델, 코드);
            //if (결과 == null) { Global.Notify(this.FindForm(), 번역.결과없음, "View", AlertControl.AlertTypes.Warning); return; }
            //ResultViewer viewer = new ResultViewer(결과);
            //viewer.Show(this.FindForm());
        }

        private void 일괄수정(object sender, EventArgs e)
        {
            //피벗정보 정보 = this.GridView1.GetFocusedRow() as 피벗정보;
            //if (정보 == null) return;
            //if (정보.집계 == 집계항목.None) return;
            //컬럼정보 컬럼 = 컬럼정보.Get(this.GridView1.FocusedColumn as BandedGridColumn);
            //if (컬럼 == null) return;
            //String 큐알 = 정보.큐알내용;
            //피벗자료.집계정보 집계 = 피벗자료.GetSummary(큐알, 컬럼.항목);
            //if (집계 == null) return;
            //Bind집계정보.DataSource = 집계;
            //popupControlContainer1.ShowPopup(this.barManager1, CalculateCellPoint());
        }
        //private Point CalculateCellPoint()
        //{
        //    //GridViewInfo viewInfo = this.GridView1.GetViewInfo() as GridViewInfo;
        //    //GridCellInfo cellInfo = viewInfo.GetGridCellInfo(this.GridView1.FocusedRowHandle, this.GridView1.FocusedColumn);
        //    ////Debug.WriteLine($"{Control.MousePosition} => {rect}, {this.PointToScreen(Point.Empty)}, {GridControl1.PointToScreen(Point.Empty)}");
        //    //if (cellInfo == null) return Control.MousePosition;
        //    //Point p = GridControl1.PointToScreen(Point.Empty);
        //    //Rectangle b = cellInfo.Bounds;
        //    //return new Point(b.X + b.Width + p.X, b.Y + b.Height + p.Y);
        //}

        private void 일괄변경(object sender, EventArgs e)
        {
            //피벗자료.집계정보 집계 = Bind집계정보.DataSource as 피벗자료.집계정보;
            //if (집계 == null || !Utils.Confirm(this.FindForm(), "내용을 적용하시겠습니까?")) return;
            //피벗자료.일괄변경(집계);
            //this.popupControlContainer1.HidePopup();
            //this.GridView1.RefreshData();
        }

        private void 엑셀파일(object sender, EventArgs e)
        {
            String path = String.Empty;
            using (SaveFileDialog dialog = MvUtils.Utils.GetSaveFileDialog("xlsx", "Excel File|*.xlsx", "Reports"))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                path = dialog.FileName;
            }
            if (String.IsNullOrEmpty(path)) return;
            ExportToExcel(this.GridView1, path);
            //IPrintable Component = this.GridControl1 as IPrintable;
            //PrintingSystem PrintingSystem = new PrintingSystem();

            //PrintableComponentLink PrintableComponentLink1 = new PrintableComponentLink();
            //PrintingSystem.Links.Add(PrintableComponentLink1);
            //PrintingSystem.PageSettings.PaperKind = DevExpress.Drawing.Printing.DXPaperKind.A3Extra;
            //PrintingSystem.PageSettings.Landscape = true;
            //PrintableComponentLink1.Component = Component;
            //PrintableComponentLink1.PrintingSystem = PrintingSystem;
            //PrintableComponentLink1.PaperKind = PrintingSystem.PageSettings.PaperKind;
            //PrintableComponentLink1.Landscape = PrintingSystem.PageSettings.Landscape;
            //PrintableComponentLink1.CreateDocument(PrintingSystem);
            //PrintingSystem.ExportToXlsx(path);
            Process.Start(path);
        }

        private static Boolean ExportToExcel(GridView view, String path)
        {
            try
            {
                XlsxExportOptionsEx options = new XlsxExportOptionsEx();
                options.ExportType = ExportType.DataAware;
                options.TextExportMode = TextExportMode.Value;
                //options.SheetName = "Sheet1";
                options.AllowConditionalFormatting = DefaultBoolean.True;
                options.AllowConditionalFormattingGlyphs = DefaultBoolean.True;
                options.AllowFixedColumns = DefaultBoolean.True;
                options.AllowFixedColumnHeaderPanel = DefaultBoolean.True;
                options.ApplyFormattingToEntireColumn = DefaultBoolean.True;
                options.AllowSortingAndFiltering = DefaultBoolean.False;
                options.CustomizeCell += (CustomizeCellEventArgs e) => {
                    e.Formatting.Alignment = new DevExpress.Export.Xl.XlCellAlignment() { VerticalAlignment = DevExpress.Export.Xl.XlVerticalAlignment.Center, HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.General };
                    e.Formatting.Font = new XlCellFont() { Name = "맑은 고딕", Size = 10 };
                    if (e.AreaType == SheetAreaType.Header)
                    {
                        e.Formatting.Alignment.HorizontalAlignment = DevExpress.Export.Xl.XlHorizontalAlignment.Center;
                        e.Formatting.Font.Bold = true;
                        e.Formatting.BackColor = Color.LightGray;
                    }
                    else if (e.AreaType == SheetAreaType.DataArea)
                    {
                        Color color = GetForeColor(view, e.RowHandle, e.ColumnFieldName);
                        if (color != Color.Empty) e.Formatting.Font.Color = color;
                        Color back = GetBackColor(view, e.RowHandle);
                        if (back != Color.Empty) e.Formatting.BackColor = back;
                    }
                    e.Handled = true;
                };
                view.ExportToXlsx(path, options);
                return true;
            }
            catch (Exception ex)
            {
                Global.오류로그("Report", "Export XLSX", ex.Message, true);
                return false;
            }
        }
    }
}
