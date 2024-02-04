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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.PlatformSDKCS;
using static SamhwaInspectionNeo.UI.Control.MasterSetting;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class Calibration : XtraUserControl
    {
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
            this.GridView1.AddSelectPopMenuItems();
            this.GridView1.CellValueChanged += 교정값계산;
            //this.GridView1.AddDeleteMenuItem(DeleteClick);
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
            //this.b적용.Click += 교정값적용;
            this.col측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.GridControl1.DataSource = this.결과정보리스트;

            this.e플로우선택.Properties.DataSource = Enum.GetValues(typeof(Flow구분));
            this.e플로우선택.EditValueChanging += 플로우선택;
            this.e플로우선택.CustomDisplayText += 선택플로우표현;

            this.e지그선택.Properties.DataSource = Enum.GetValues(typeof(지그위치));
            this.e지그선택.EditValueChanging += 지그선택;
            this.e지그선택.CustomDisplayText += 선택지그표현;
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
            //현재날짜 데이터 가져와서 Flow / Jig Check후 가장 최신데이터 불러오기.
            List<검사결과> 자료 = Global.검사자료.테이블.Select();

            if (자료.Count == 0) return;

            for (int lop = 0; lop < 자료[0].검사내역.Count; lop++)
            {
                결과정보 조회정보 = new 결과정보();
                검사정보 정보 = 자료[0].검사내역[lop];
                조회정보.측정값 = 정보.측정값;
                조회정보.검사항목 = 정보.검사항목;
                조회정보.검사장치 = 정보.검사장치;
                조회정보.측정단위 = 정보.측정단위;
                결과정보리스트.Add(조회정보);
            }

            GridControl1.DataSource = null;
            GridControl1.DataSource = 결과정보리스트;
        }
        private void 교정값계산(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isCalculating) return;

            int rowIndex = e.RowHandle;

            object measvalue = GridView1.GetRowCellValue(rowIndex, "측정값");
            object cmmvalue = GridView1.GetRowCellValue(rowIndex, "CMM측정값");
            object name = GridView1.GetRowCellValue(rowIndex, "검사항목");

            double measdvalue, cmmdvalue;
            float[] calvalue = new float[1];

            if (measvalue != null && cmmvalue != null &&
                    double.TryParse(measvalue.ToString(), out measdvalue) &&
                        double.TryParse(cmmvalue.ToString(), out cmmdvalue))
            {
                if (measdvalue != 0)
                {
                    isCalculating = true;
                    calvalue[0] = (float)(cmmdvalue / measdvalue);
                    GridView1.SetRowCellValue(rowIndex, "교정값", Math.Round(calvalue[0], 6));
                    isCalculating = false;
                }
            }

            //VM Slot쪽에 넣어주기 //Flow와 Jig & 이름 체크.
            if (name.ToString().Contains("Slot1"))
            {
                string setName = $"{this.위치}{name}";
                Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.ModuParams.SetInputFloat(setName, calvalue);
                Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.ResetParam();
                //Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.Save();
                //Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.Run();
                //Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.ReFlashReadyState();
                //Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.ResetParam();
                //Global.VM제어.GetItem(this.플로우).slot1ShellModuleTool.RefreshResultForce();

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
