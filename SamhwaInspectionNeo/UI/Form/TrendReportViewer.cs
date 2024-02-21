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

namespace SamhwaInspectionNeo.UI.Form
{
    public partial class TrendReportViewer : DevExpress.XtraEditors.XtraForm
    {
        public 검사결과 결과 { get; set; } = null;
        public TrendReportViewer() => InitializeComponent();

        public TrendReportViewer(검사결과 결과)
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            this.결과 = 결과;
            this.cSlot1상부.Init();
            //this.Shown += FormShown;
        }


        private void FormShown(object sender, EventArgs e)
        {
            if (this.결과 == null) return;
            this.cSlot1상부.검사완료알림(this.결과);
        }
    }
}