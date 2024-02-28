using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
using Newtonsoft.Json.Linq;
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
using static SamhwaInspectionNeo.Schemas.검사자료;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class Chart : XtraUserControl
    {
        public Chart() => InitializeComponent();

        public void Init(List<검사항목> 항목)
        {
            if (항목 == null) return;

            this.ChartControl1.Series.Clear();
            foreach (검사항목 item in 항목)
            {
                Series series = new Series(item.ToString(), ViewType.Line);
                this.ChartControl1.Series.Add(series);
            }
        }

        public void Y축설정(Decimal Min, Decimal Max)
        {
            XYDiagram diagram = this.ChartControl1.Diagram as XYDiagram;
            diagram.AxisY.VisualRange.SetMinMaxValues(Min, Max);
        }

        public void 조회자료그래프보기()
        {
            if (this.ChartControl1.InvokeRequired)
            {
                this.ChartControl1.BeginInvoke(new Action(() => 조회자료그래프보기()));
                return;
            }

            for (int lop = 0; lop < Global.검사자료.Count; lop++)
            {
                if (Global.검사자료[lop].모델구분 != Global.환경설정.선택모델) continue;

                foreach (Series series in this.ChartControl1.Series)
                {
                    검사항목 enumValue = (검사항목)Enum.Parse(typeof(검사항목), series.Name);
                    Decimal 결과값 = Global.검사자료[lop].검사내역.Where(e => e.검사항목 == enumValue).FirstOrDefault().측정값;

                    series.Points.Add(new SeriesPoint(lop, 결과값));
                }
            }
        }

        public void 검사완료알림()
        {
            if (this.ChartControl1.InvokeRequired)
            {
                this.ChartControl1.BeginInvoke(new Action(() => 검사완료알림()));
                return;
            }
         
        }
    }
}
