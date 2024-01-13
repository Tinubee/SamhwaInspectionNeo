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

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class MasterSetting : DevExpress.XtraEditors.XtraUserControl
    {
        public enum 지그위치
        {
            Front,
            Rear,
        }
        public Flow구분 플로우 { get; set; } = Flow구분.Flow1;
        public 지그위치 위치 { get; set; } = 지그위치.Front;
        public MasterSetting()
        {
            InitializeComponent();
        }
    }
}
