using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SamhwaInspectionNeo.Schemas.신호제어;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class IOControl : DevExpress.XtraEditors.XtraUserControl
    {
        public IOControl()
        {
            InitializeComponent();
        }

        public void Init()
        {
            ////MyGridView.SetFocusedRow(this.gridView1);
            //this.customGrid1.DataSource = new 입력신호자료();
            //this.입출변경알림();
            //Global.신호제어.입출변경알림 += 입출변경알림;
        }
    }
}
