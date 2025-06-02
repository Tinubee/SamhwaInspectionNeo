using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class Calibration : XtraUserControl
    {
        public enum 보정값설정
        {
            Flow1Front,
            Flow1Rear,
            Flow2Front,
            Flow2Rear,
            Flow3Front,
            Flow3Rear,
            Flow4Front,
            Flow4Rear,
            Flow5Front,
            Flow5Rear,
            Flow6Front,
            Flow6Rear,
        }

        String 로그영역 = "보정값설정";
        private 지그위치 위치 { get; set; } = 지그위치.Front;
        private Flow구분 플로우 { get; set; } = Flow구분.Flow1;
        BindingList<결과정보> 결과정보리스트 = new BindingList<결과정보>();
        private bool isCalculating = false;

        public Calibration()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddCalibrationPopMenuItems();
            this.GridView1.CellValueChanged += 교정값계산;
            this.col측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.colcmm측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col교정값.DisplayFormat.FormatString = "0.#########";

            this.col검사장치.OptionsColumn.AllowEdit = false;
            this.col검사장치.AppearanceCell.BackColor = Color.Gray;
            this.col검사장치.AppearanceCell.ForeColor = Color.Black;

            this.col검사항목.OptionsColumn.AllowEdit = false;
            this.col검사항목.AppearanceCell.BackColor = Color.Gray;
            this.col검사항목.AppearanceCell.ForeColor = Color.Black;

            this.col교정값.OptionsColumn.AllowEdit = false;
            this.col교정값.AppearanceCell.BackColor = Color.Gray;
            this.col교정값.AppearanceCell.ForeColor = Color.Black;

            this.col측정단위.OptionsColumn.AllowEdit = false;
            this.col측정단위.AppearanceCell.BackColor = Color.Gray;
            this.col측정단위.AppearanceCell.ForeColor = Color.Black;

            this.col측정값.OptionsColumn.AllowEdit = false;
            this.col측정값.AppearanceCell.BackColor = Color.Gray;
            this.col측정값.AppearanceCell.ForeColor = Color.Black;

            this.b조회.Click += 자료조회;
            this.col측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.GridControl1.DataSource = this.결과정보리스트;

            this.e플로우선택.Properties.DataSource = Enum.GetValues(typeof(Flow구분));
            this.e플로우선택.EditValueChanging += 플로우선택;
            this.e플로우선택.CustomDisplayText += 선택플로우표현;

            this.e지그선택.Properties.DataSource = Enum.GetValues(typeof(지그위치));
            this.e지그선택.EditValueChanging += 지그선택;
            this.e지그선택.CustomDisplayText += 선택지그표현;

            this.b전체보정.Click += B전체보정_Click;
        }

        private void B전체보정_Click(object sender, EventArgs e)
        {
            if (GridView1.RowCount == 1) return;
            List<VmVariable> 보정값변수들 = Global.VM제어.글로벌변수제어.보정값불러오기();
            for (int lop = 0; lop < GridView1.RowCount; lop++)
            {
                if (isCalculating) return;

                int rowIndex = lop;

                object measvalue = GridView1.GetRowCellValue(rowIndex, "측정값");
                object cmmvalue = GridView1.GetRowCellValue(rowIndex, "CMM측정값");
                object name = GridView1.GetRowCellValue(rowIndex, "검사항목");
                object calvalue = GridView1.GetRowCellValue(rowIndex, "교정값");

                double measdvalue, cmmdvalue;

                Debug.WriteLine($"{name}");

                if (measvalue != null && cmmvalue != null &&
                   double.TryParse(measvalue.ToString(), out measdvalue) &&
                       double.TryParse(cmmvalue.ToString(), out cmmdvalue))
                {
                    if (measdvalue != 0)
                    {
                        isCalculating = true;
                        if (cmmdvalue != 0)
                        {
                            if (name.ToString().Contains("위치도"))
                            {
                                if (name.ToString().Contains("거리") == false && name.ToString().Contains("Slot") == false)
                                {
                                    isCalculating = false;
                                    MvUtils.Utils.MessageBox("보정값계산", "위치도값은 보정할 수 없습니다. X,Y 거리를 보정해주세요.", 2000);
                                    continue;
                                }
                            }
                            if (name.ToString().Contains("Bur"))
                            {
                                isCalculating = false;
                                continue;
                            }
                            //2P-B모델 좌상X좌표 0, 우하Y좌표 0
                            calvalue = (float)(cmmdvalue / measdvalue);
                        }

                        GridView1.SetRowCellValue(rowIndex, "교정값", Math.Round(Convert.ToDouble(calvalue), 6));
                        isCalculating = false;
                    }

                    if (cmmdvalue != 0)
                    {
                        Int32 index = 보정위치();

                        if (index == -1) return;

                        VmVariable 적용할변수 = 보정값변수들.Where(f => f.Name.Contains(name.ToString())).FirstOrDefault();

                        if (적용할변수 == null) return;

                        string value = 적용할변수.StringValue;
                        string[] splitValue = value.Split(';');

                        splitValue[index] = Math.Round(Convert.ToDouble(calvalue), 6).ToString();

                        value = String.Join(";", splitValue);

                        Global.VM제어.글로벌변수제어.SetValue(적용할변수.Name, value);
                    }
                }
            }
        }

        private void 선택지그표현(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value == null) return;
                지그위치 지그 = (지그위치)e.Value;
                e.DisplayText = $"{MvUtils.Utils.GetDescription(지그)}";
            }
            catch { e.DisplayText = String.Empty; }
        }
        private void 지그선택(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue == null) return;
            지그위치 지그 = (지그위치)e.NewValue;
            this.위치 = 지그;
        }
        private void 선택플로우표현(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value == null) return;
                Flow구분 플로우 = (Flow구분)e.Value;
                e.DisplayText = $"{MvUtils.Utils.GetDescription(플로우)}";
            }
            catch { e.DisplayText = String.Empty; }
        }
        private void 플로우선택(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue == null) return;
            Flow구분 플로우 = (Flow구분)e.NewValue;
            this.플로우 = 플로우;
        }

        public void Close() { }
        private void 자료조회(object sender, EventArgs e)
        {
            Debug.WriteLine("1");
            this.결과정보리스트.Clear();
            GridControl1.DataSource = null;
         
            //List<검사결과> 자료 = Global.검사자료.테이블.Select(new QueryPrms { 시작 = DateTime.Today, 종료 = DateTime.Today.AddDays(1), 역순정렬 = false });

            if (Global.검사자료.Count == 0) return;

            검사결과 선택자료 = Global.검사자료.Where(w => w.검사코드 == (Int32)this.플로우 && w.검사내역[0].지그 == this.위치 &&  w.검사내역[0].결과값 != 0).LastOrDefault();

            if (선택자료 == null)
            {
                Global.오류로그(로그영역, "자료조회오류", $"{this.플로우} - {this.위치}지그에 대한 검사데이터가 없습니다.\n", true);
                return;
            }

            List<VmVariable> calValue = Global.VM제어.글로벌변수제어.보정값불러오기();
            Int32 index = 보정위치();

            if (index == -1) return;

            for (int lop = 0; lop < 선택자료.검사내역.Count; lop++)
            {
                결과정보 조회정보 = new 결과정보();
                검사정보 정보 = 선택자료.검사내역[lop];
                조회정보.측정값 = 정보.측정값;
                조회정보.검사항목 = 정보.검사항목;
                조회정보.검사장치 = 정보.검사장치;
                조회정보.측정단위 = 정보.측정단위;
                조회정보.교정값 = 보정값조회(정보.검사항목, calValue, index);
                조회정보.CMM측정값 = 정보.마스터값;
                this.결과정보리스트.Add(조회정보);
            }
            GridControl1.DataSource = this.결과정보리스트;
        }

        private Decimal 보정값조회(검사항목 항목, List<VmVariable> list, Int32 보정값위치)
        {
            //CalValue
            VmVariable 변수 = list.Where(e => e.Name == $"{항목}_CalValue" || e.Name == $"{항목}CalValue").FirstOrDefault();
            if (변수 == null) return 1;

            string value = 변수.StringValue;
            string[] splitValue = value.Split(';');

            return Convert.ToDecimal(splitValue[보정값위치]);
        }

        private void 교정값계산(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isCalculating) return;

            int rowIndex = e.RowHandle;

            object measvalue = GridView1.GetRowCellValue(rowIndex, "측정값");
            object cmmvalue = GridView1.GetRowCellValue(rowIndex, "CMM측정값");
            object name = GridView1.GetRowCellValue(rowIndex, "검사항목");
            object calvalue = GridView1.GetRowCellValue(rowIndex, "교정값");

            double measdvalue, cmmdvalue;

            List<VmVariable> 보정값변수들 = Global.VM제어.글로벌변수제어.보정값불러오기();

            if (measvalue != null && cmmvalue != null &&
                    double.TryParse(measvalue.ToString(), out measdvalue) &&
                        double.TryParse(cmmvalue.ToString(), out cmmdvalue))
            {
                if (measdvalue != 0)
                {
                    isCalculating = true;
                    if (cmmdvalue != 0)
                    {
                        if (name.ToString().Contains("위치도"))
                        {
                            if (name.ToString().Contains("거리") == false && name.ToString().Contains("Slot") == false)
                            {
                                isCalculating = false;
                                MvUtils.Utils.MessageBox("보정값계산", "위치도값은 보정할 수 없습니다. X,Y 거리를 보정해주세요.", 2000);
                                return;
                            }
                        }
                        //2P-B모델 좌상X좌표 0, 우하Y좌표 0
                        calvalue = (float)(cmmdvalue / measdvalue);
                    }

                    GridView1.SetRowCellValue(rowIndex, "교정값", Math.Round(Convert.ToDouble(calvalue), 6));
                    isCalculating = false;
                }
            }


            Int32 index = 보정위치();

            if (index == -1) return;

            VmVariable 적용할변수 = 보정값변수들.Where(f => f.Name.Contains(name.ToString())).FirstOrDefault();

            if (적용할변수 == null) return;

            string value = 적용할변수.StringValue;
            string[] splitValue = value.Split(';');

            splitValue[index] = Math.Round(Convert.ToDouble(calvalue), 6).ToString();

            value = String.Join(";", splitValue);

            Global.VM제어.글로벌변수제어.SetValue(적용할변수.Name, value);
        }

        private Int32 보정위치()
        {
            String check = $"{this.플로우}{this.위치}";
            if (Enum.TryParse(check, out 보정값설정 설정))
            {
                int compIntValue = (int)설정; // Cast the enum to int to get its underlying value
                return compIntValue; // Output the value. Replace this with your output method.
            }
            else
            {
                return -1;
            }
        }

    }

    public class 결과정보
    {
        public 장치구분 검사장치 { get; set; } = 장치구분.None;
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        public 단위구분 측정단위 { get; set; } = 단위구분.mm;
        public Decimal 측정값 { get; set; } = 0m;
        public Decimal CMM측정값 { get; set; } = 0m;
        public Decimal 교정값 { get; set; } = 1m;
    }
}
