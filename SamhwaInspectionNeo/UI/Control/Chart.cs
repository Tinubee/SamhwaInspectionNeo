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

        public void Init()
        {
            Global.검사자료.검사완료알림 += 검사완료알림; ;
        }

        public void 검사완료알림(검사결과 결과)
        {
            //this.ChartControl1.Series[0].Points.AddPoint(1, 1);
        }
    }
}
