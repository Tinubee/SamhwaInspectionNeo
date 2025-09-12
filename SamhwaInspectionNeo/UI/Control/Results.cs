using DevExpress.Data.Filtering;
using DevExpress.Data.Linq;
using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using MvUtils;
using SamhwaInspectionNeo.Schemas;
using SamhwaInspectionNeo.UI.Form;
using SqlKata;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using VM.Core;
using VM.PlatformSDKCS;
using static Euresys.MultiCam.MC;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class Results : XtraUserControl
    {
        private LocalizationResults 번역 = new LocalizationResults();
        private EntityServerModeSource serverModeSourceInsp = new EntityServerModeSource();

        private 검사목록테이블 테이블 = null;
        public Results()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.테이블 = new 검사목록테이블();
            this.BindLocalization.DataSource = this.번역;
            this.e시작일자.DateTime = DateTime.Today;
            this.e종료일자.DateTime = DateTime.Today;

            this.b자료조회.Click += (s, e) => LoadData(e시작일자.DateTime, e종료일자.DateTime);
            this.b엑셀파일.Click += 엑셀파일;

            //this.b자료조회.Click += 자료조회;
            //this.b엑셀파일.Click += 엑셀파일;
            this.b그래프보기.Click += 그래프보기;

            this.serverModeSourceInsp.KeyExpression = nameof(검사결과.검사일시);
            this.GridControl1.DataSource = serverModeSourceInsp;

            LoadData(e시작일자.DateTime, e종료일자.DateTime);

            // Detail Grid: 포커스 되는 시점에 해당 부분만 로딩. 되기 전까지는 로딩없음.
            this.GridView1.FocusedRowChanged += (s, e) =>
            {
                var row = GridView1.GetFocusedRow() as 검사결과;
                if (row == null) return;

                // 검사일시 기준으로 inspd에서 검사정보 직접 로딩
                var 검사정보목록 = Global.검사자료신규.GetDetailsByDate(row.검사일시);
                row.검사내역 = 검사정보목록;

                // 검사내역의 검사명칭, 불량정보 업데이트
                //row.UpdateFormat();
            };



            this.col최소값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col기준값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col최대값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col결과값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.GridView1.Init(this.barManager1);
            this.GridView1.AddRowSelectedEvent(new CustomView.RowSelectedEventHandler(검사내역펼치기));
            this.GridView1.AddImageViewSelectedEvent(new CustomView.ImageViewEventHandler(이미지보기));

            if (Global.환경설정.권한여부(유저권한구분.관리자))
            {
                this.GridView1.AddDeleteMenuItem(정보삭제);
                //this.GridView1.AddExpandMasterPopMenuItems();
                this.GridView1.AddSelectPopMenuItems();
                this.GridView1.AddImageViewMenu();

            }
            else
            {
                this.layoutControl1.Visible = false;
                this.GridView1.OptionsDetail.AllowOnlyOneMasterRowExpanded = true;
            }

            this.GridControl1.DataSource = Global.검사자료신규;
            this.GridView1.ActiveFilterCriteria = new BinaryOperator("검사코드", 50, BinaryOperatorType.LessOrEqual);
            this.GridControl1.ViewRegistered += GridControl1_ViewRegistered;
            this.GridView1.RowCountChanged += GridView1_RowCountChanged;
            this.GridView1.CustomDrawCell += GridView1_CustomDrawCell;
            this.GridView2.CustomDrawCell += GridView2_CustomDrawCell;

            Localization.SetColumnCaption(this.GridView1, typeof(검사결과));
            Localization.SetColumnCaption(this.GridView2, typeof(검사정보));
            this.col검사일자.Caption = Localization.일자.GetString();
            this.col검사시간.Caption = Localization.시간.GetString();

            Global.검사자료.검사완료알림 += 검사완료알림;
        }

        private void 그래프보기(object sender, EventArgs e)
        {
            Global.MainForm.TrendReportViewer = new Form.TrendReportViewer();
            Global.MainForm.TrendReportViewer.Show();
        }
        private void LoadData(DateTime 시작, DateTime 종료)
        {

            var 시작일 = 시작.Date;
            var 종료일 = 종료.Date.AddDays(1);

            serverModeSourceInsp.QueryableSource = Global.검사자료신규
                .GetQueryable(시작일, 종료일)
                .OrderByDescending(x => x.검사일시);

            // Row가 하나일 경우 Focus 이벤트 발생시켜 업데이트 진행.
            AfterSearch();
        }
        private void AfterSearch()
        {
            var rowCount = GridView1.RowCount;
            if (rowCount == 0) return;

            var row = GridView1.GetRow(0) as 검사결과;
            if (row == null) return;

            var 검사정보목록 = Global.검사자료신규.GetDetailsByDate(row.검사일시);
            row.검사내역 = 검사정보목록;
            //row.UpdateFormat();
        }

        private void 검사완료알림(검사결과 결과)
        {
            if (this.GridControl1.InvokeRequired)
            {
                this.GridControl1.Invoke(new Action(() =>
                {
                    serverModeSourceInsp.Reload();
                }));
            }
            else
            {
                serverModeSourceInsp.Reload();
            }
        }

        public void Close() { }

        private void GridView1_RowCountChanged(object sender, EventArgs e)
        {
            (sender as GridView).MoveFirst();
        }

        private void GridControl1_ViewRegistered(object sender, DevExpress.XtraGrid.ViewOperationEventArgs e)
        {
            CustomView view = e.View as CustomView;
            view.Init(this.barManager1);
            if (Global.환경설정.권한여부(유저권한구분.관리자))
            {
                view.AddDeleteMenuItem(검사삭제);
                view.AddRowSelectedEvent(new CustomView.RowSelectedEventHandler(카메라검사보기));
                view.CustomDrawCell += GridView2_CustomDrawCell;
            }
        }

        private 검사정보 선택검사정보(out 검사결과 결과)
        {
            결과 = this.GridView1.GetFocusedRow() as 검사결과;
            if (this.GridControl1.FocusedView == null || this.GridControl1.FocusedView.Name != this.GridView2.Name) return null;
            GridView view = this.GridControl1.FocusedView as GridView;
            return view.GetFocusedRow() as 검사정보;
        }

        private void 정보삭제(object sender, ItemClickEventArgs e)
        {
            if (this.GridView1.SelectedRowsCount < 1) return;
            if (!MvUtils.Utils.Confirm("선택한 검사결과를 삭제하시겠습니까?", Localization.확인.GetString())) return;

            ArrayList 자료 = this.GridView1.SelectedData() as ArrayList;
            foreach (검사결과 결과 in 자료)
                Global.검사자료.결과삭제(결과);
        }

        private void 검사삭제(object sender, ItemClickEventArgs e)
        {
            검사정보 정보 = this.선택검사정보(out 검사결과 결과);
            if (결과 == null || 정보 == null) return;
            if (!MvUtils.Utils.Confirm("선택한 검사결과를 삭제하시겠습니까?", Localization.확인.GetString())) return;
            Global.검사자료.결과삭제(결과);
            (this.GridControl1.FocusedView as GridView).RefreshData();
        }

        private void 카메라검사보기(GridView gridView, Int32 RowHandle)
        {
            검사결과 결과 = null;
            검사정보 검사 = this.선택검사정보(out 결과);
            if (결과 == null || 검사 == null || !CameraAttribute.IsCamera(검사.검사장치)) return;

            모델정보 모델 = Global.모델자료.GetItem(결과.모델구분);
            if (모델 == null) return;
        }

        public void 이미지보기(GridView gridView, Int32 RowHandle)
        {
            if (Global.신호제어.운전시작여부)
            {
                MvUtils.Utils.MessageBox("검사이미지보기", $"장비가 운전중일때는 사용 할 수 없습니다.", 2000);
                return;
            }

            int index = gridView.FocusedRowHandle;
            검사결과 정보 = gridView.GetRow(index) as 검사결과;

            List<String> paths = new List<String> { Global.환경설정.사진저장경로, MvUtils.Utils.FormatDate(DateTime.Now, "{0:yyyy-MM-dd}"), Global.환경설정.선택모델.ToString(), 카메라구분.Cam01.ToString() };
            String name = $"{MvUtils.Utils.FormatDate(정보.검사일시, "{0:HHmmss}")}_{정보.검사코드.ToString("d4")}.jpg";//_{결과.ToString()}
            String path = Common.CreateDirectory(paths);
            if (String.IsNullOrEmpty(path))
            {
                //Global.오류로그(로그영역, "이미지 저장", $"[{Path.Combine(paths.ToArray())}] 디렉토리를 만들 수 없습니다.", true);
                //return;
            }
            String file = Path.Combine(path, name);

            if (!File.Exists(file))
            {
                MvUtils.Utils.MessageBox("검사이미지보기", $"{정보.검사일시} [{정보.검사코드}]검사에 대한 이미지 파일이 없습니다.", 2000);
                return;
            }

            NGViewer ngForm = new NGViewer(file, 정보.검사코드, 정보.검사내역[0].지그);
            ngForm.Show();
        }

        public void 검사내역펼치기(GridView gridView, Int32 RowHandle)
        {
            if (gridView.GetMasterRowExpanded(RowHandle)) gridView.CollapseMasterRow(RowHandle);
            else gridView.ExpandMasterRow(RowHandle);
        }

        private void GridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0 || (e.Column.FieldName == this.col측정결과.FieldName || e.Column.FieldName == this.colCTQ결과.FieldName || e.Column.FieldName == this.col외관결과.FieldName))
            {
                GridView view = sender as GridView;
                검사결과 정보 = view.GetRow(e.RowHandle) as 검사결과;
                if (정보 == null) return;

                if (e.Column.FieldName == this.col측정결과.FieldName) e.Appearance.ForeColor = 환경설정.결과표현색상(정보.측정결과);
                else if (e.Column.FieldName == this.colCTQ결과.FieldName) e.Appearance.ForeColor = 환경설정.결과표현색상(정보.CTQ결과);
                else if (e.Column.FieldName == this.col외관결과.FieldName) e.Appearance.ForeColor = 환경설정.결과표현색상(정보.외관결과);
            }
        }

        private void GridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (e.RowHandle < 0) return;
            GridView view = sender as GridView;
            if (view.Columns["마스터값"].Visible)
            {
                view.Columns["마스터값"].Visible = false;
                view.Columns["마스터공차"].Visible = false;
                view.Columns["교정값"].Visible = false;
                view.RefreshData();
            }

            if (!(view.GetRow(e.RowHandle) is 검사정보 정보)) return;

            if (e.Column.FieldName == this.col검사결과.FieldName) e.Appearance.ForeColor = 환경설정.결과표현색상(정보.측정결과);
        }

        private void 엑셀파일(object sender, EventArgs e)
        {
            //최종 출력 리스트
            List<List<string>> result = new List<List<string>>();
            Debug.WriteLine("0");
            var 검사결과리스트 = Global.검사자료신규.GetResultListWithDetail(e시작일자.DateTime, e종료일자.DateTime);
            Debug.WriteLine("1");
;            //마지막 검사 데이터 없으면 return
            if (검사결과리스트 == null || 검사결과리스트.Count == 0)
            {
                Global.오류로그("Excel Extractor", "Data Load", "There is no inspection data.", true);
                return;
            }

            // 결과 리스트 처음 정보는 변수명임
            // 변수명에 메인정보 추가
            var TitlesName = new List<string>
    {
        "Index",
        "Time",
        "Result",
        "CTQ",
        "Surface",
    };

            // 변수명에 검사설정에 세팅되어 있는 검사명칭 추가
            List<검사정보> TitleDetail = 검사결과리스트.FirstOrDefault().검사내역;
            TitleDetail.ForEach(x => TitlesName.Add(Global.모델자료.GetItemName(Global.환경설정.선택모델, x.검사항목)));

            // 결과의 첫 리스트에 변수명 리스트 추가
            result.Add(TitlesName);
            Debug.WriteLine("2");
            검사결과리스트 = 검사결과리스트.OrderBy(x => x.검사일시).ToList();

            //검사 일시 별로 검사 정보 및 검사결과 추출 후 데이터 추가
            foreach (검사결과 결과 in 검사결과리스트)
            {
                var row = new List<string>
        {
        결과.검사코드.ToString(),
        결과.검사일시.ToString("yy-MM-dd HH:mm:ss"),
        결과.측정결과.ToString(),
        결과.CTQ결과.ToString(),
        결과.외관결과.ToString(),
        };

                결과.검사내역.ForEach(x => row.Add(x.결과값.ToString()));
                result.Add(row);
            }
            Debug.WriteLine("3");
            // 파일 저장 경로 지정
            string filePath = $@"C:\IVM\ProductionData\ProductionData_{DateTime.Now.ToString("yyMMdd_HHmmss")}.xlsx";

            // 앞서 추출한 결과(result)를 Excel로 내보내기
            // 결과, 열&행 변환 여부, 파일 경로를 인자로 전달
            Global.검사자료신규.ExportToExcel(result, false, filePath);
            //Global.검사자료신규.ExportToCsv(result, filePath);
            Debug.WriteLine("4");

            Global.정보로그("Data base", "Excel Extraction", "Extraction is completed.", true);
        }
        private void 자료조회(object sender, EventArgs e)
        {
            //if (Global.장치상태.자동수동)
            //{
            //    Global.Notify("Can't do this during autorun.", "Search", AlertControl.AlertTypes.Warning);
            //    return;
            //}
            DateTime s = DateTime.Now;
            Global.검사자료.Save();
            Global.검사자료.Load(this.e시작일자.DateTime, this.e종료일자.DateTime);
            GridView1.RefreshData();
            Debug.WriteLine((DateTime.Now - s).TotalMilliseconds, $"Load Time GridView {Global.검사자료.Count}");
        }

        public class LocalizationResults
        {
            private enum Items
            {
                [Translation("Day", "검사일자")]
                검사일자,
                [Translation("Search", "조  회")]
                조회버튼,
                [Translation("Export to Excel", "엑셀파일로 내보내기")]
                엑셀버튼,
            }

            public String 검사일자 { get { return Localization.GetString(Items.검사일자); } }
            public String 조회버튼 { get { return Localization.GetString(Items.조회버튼); } }
            public String 엑셀버튼 { get { return Localization.GetString(Items.엑셀버튼); } }
        }
    }
}
