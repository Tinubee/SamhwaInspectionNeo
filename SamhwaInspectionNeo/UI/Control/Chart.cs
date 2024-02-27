using DevExpress.XtraCharts;
using DevExpress.XtraEditors;
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
            this.ChartControl1.Series.Clear();
            foreach (검사항목 item in 항목)
            {
                Series series = new Series(item.ToString(), ViewType.Line);
                this.ChartControl1.Series.Add(series);
            }
            //Series series = new Series(항목.ToString(),ViewType.Line);
            //this.ChartControl1.Series.Add(series);
            //Global.검사자료.검사완료알림 += 검사완료알림; ;
        }

        public void 검사완료알림()
        {
            if (this.ChartControl1.InvokeRequired)
            {
                this.ChartControl1.BeginInvoke(new Action(() => 검사완료알림()));
                return;
            }

            //Series series = this.ChartControl1.Series["Slot1상부"];
            //Series series2 = this.ChartControl1.Series["Slot1중앙부"];
            //Series series3 = this.ChartControl1.Series["Slot1하부"];
            //if (series != null)
            //{
            //    for (int lop = 0; lop < Global.검사자료.Count; lop++)
            //    {
            //        Decimal resulte = Global.검사자료[lop].검사내역.Where(e => e.검사항목 == 검사항목.Slot1상부).FirstOrDefault().측정값;
            //        Decimal resulte2 = Global.검사자료[lop].검사내역.Where(e => e.검사항목 == 검사항목.Slot1중앙부).FirstOrDefault().측정값;
            //        Decimal resulte3 = Global.검사자료[lop].검사내역.Where(e => e.검사항목 == 검사항목.Slot1하부).FirstOrDefault().측정값;

            //        series.Points.Add(new SeriesPoint(lop, resulte));
            //        series2.Points.Add(new SeriesPoint(lop, resulte2));
            //        series3.Points.Add(new SeriesPoint(lop, resulte3));
            //    }
            //}
        }
    }
}
