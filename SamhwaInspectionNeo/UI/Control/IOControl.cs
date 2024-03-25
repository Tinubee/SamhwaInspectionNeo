using DevExpress.XtraEditors;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
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
            this.GridControl1.DataSource = new 입력신호자료();
            this.입출변경알림();
            Global.신호제어.입출변경알림 += 입출변경알림;
        }
        private void 입출변경알림()
        {
            //Debug.WriteLine("I/O Viewer 입출변경알림");
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(입출변경알림));
                return;
            }
            GridView1.RefreshData();
        }

        private class 입력신호자료 : List<입력신호정보>
        {
            public 입력신호자료()
            {
                foreach (신호제어.정보주소 번호 in typeof(신호제어.정보주소).GetEnumValues())
                {
                    //if (MvUtils.Utils.GetAttribute<TranslationAttribute>(번호) == null) continue;
                    this.Add(new 입력신호정보() { 구분 = 번호 });
                }
            }
        }

        private class 입력신호정보
        {
            public 신호제어.정보주소 구분 { get; set; }
            public Int32 번호 { get { return (Int32)구분; } }
            public String 주소 { get { return MvUtils.Utils.GetAttribute<AddressAttribute>(구분).Address; } }
            public String 여부 { get { return Global.신호제어.정보읽기(구분).ToString(); } }
        }
    }
}
