using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SamhwaInspectionNeo.UI.Control.MasterSetting;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class Calibration : XtraUserControl
    {

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
            this.colCMM측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
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

            //this.e일자.DateTime = DateTime.Now;
            this.b조회.Click += 자료조회;
            //this.b적용.Click += 교정값적용;
            this.col측정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.GridControl1.DataSource = this.결과정보리스트;
            this.e플로우선택.Properties.DataSource = Enum.GetValues(typeof(Flow구분));
            this.e지그선택.Properties.DataSource = Enum.GetValues(typeof(지그위치));
        }
        public void Close() { }
        private void 자료조회(object sender, EventArgs e)
        {

        }
        private void 교정값계산(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (isCalculating) return;

            int rowIndex = e.RowHandle;

            object measvalue = GridView1.GetRowCellValue(rowIndex, "측정값");
            object cmmvalue = GridView1.GetRowCellValue(rowIndex, "CMM측정값");

            double measdvalue, cmmdvalue;

            if (measvalue != null && cmmvalue != null &&
                    double.TryParse(measvalue.ToString(), out measdvalue) &&
                        double.TryParse(cmmvalue.ToString(), out cmmdvalue))
            {
                if (measdvalue != 0)
                {
                    isCalculating = true;

                    double calvalue = cmmdvalue / measdvalue;
                    GridView1.SetRowCellValue(rowIndex, "교정값", calvalue);

                    isCalculating = false;
                }
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
