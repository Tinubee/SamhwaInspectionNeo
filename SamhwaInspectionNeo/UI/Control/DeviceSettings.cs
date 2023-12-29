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

namespace SamhwaInspectionNeo.UI.Controls
{
    public partial class DeviceSettings : XtraUserControl
    {
        public DeviceSettings()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.e강제배출.IsOn = Global.환경설정.강제배출;
            this.e강제배출.EditValueChanged += 배출구분Changed;

            this.e카메라.Init();
            this.e기본설정.Init();
            this.e유저관리.Init();
        }

        public void Close()
        {
            this.e카메라.Close();
            this.e기본설정.Close();
            this.e유저관리.Close();
        }

        public void Shown(Boolean shown)
        {

        }

        private void 배출구분Changed(object sender, EventArgs e)
        {
            Global.환경설정.강제배출 = this.e강제배출.IsOn;
        }
    }
}
