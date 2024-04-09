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
    public partial class NGViewer : XtraForm
    {
        public NGViewer(String path, Int32 검사코드, 지그위치 지그)
        {
            InitializeComponent();
            this.ngImageViewer1.Init(path, 검사코드, 지그);
        }
    }
}