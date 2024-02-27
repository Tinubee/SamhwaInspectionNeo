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
using static SamhwaInspectionNeo.Schemas.신호제어;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class TirggerBoard : XtraUserControl
    {
        public TirggerBoard()
        {
            InitializeComponent();
        }

        public void Init()
        {
            if (Global.트리거보드제어.트리거보드 == null) return;

            this.t펌웨어버전.Text = $"Ver. {Global.트리거보드제어.펌웨어버전}"; 
            this.t로직버전.Text = $"Ver. {Global.트리거보드제어.로직버전}";
           
            this.b트리거보드초기화.Click += B트리거보드초기화_Click;
            this.b엔코더초기화.Click += B엔코더초기화_Click;
            this.b트리거초기화.Click += B트리거초기화_Click;

            Global.트리거보드제어.엔코더위치알림 += 엔코더위치알림;
            Global.트리거보드제어.트리거카운트변경알림 += 트리거카운트변경알림;
        }

        private void B트리거초기화_Click(object sender, EventArgs e)
        {
            if (!MvUtils.Utils.Confirm("트리거카운트를 초기화 하시겠습니까?")) return;
            Global.트리거보드제어.ClearAllTrigger();
        }

        private void B엔코더초기화_Click(object sender, EventArgs e)
        {
            if (!MvUtils.Utils.Confirm("엔코더 위치값을 초기화 하시겠습니까?")) return;
            Global.트리거보드제어.ClearAllPosition();
        }

        private void B트리거보드초기화_Click(object sender, EventArgs e)
        {
            if (!MvUtils.Utils.Confirm("트리거 보드 위치값&트리거카운트를 초기화 하시겠습니까?")) return;
            Global.트리거보드제어?.ClearAll();
        }

        private void 트리거카운트변경알림(decimal 현재횟수, Int32 위치)
        {
            if (this.Status.InvokeRequired)
            {
                this.Status.BeginInvoke(new Action(() => { 트리거카운트변경알림(현재횟수, 위치); }));
                return;
            }

            if (위치 == 0) this.t트리거0.Text = 현재횟수.ToString();
            else if (위치 == 1) this.t트리거1.Text = 현재횟수.ToString();
            else if (위치 == 2) this.t트리거2.Text = 현재횟수.ToString();
            else if (위치 == 3) this.t트리거3.Text = 현재횟수.ToString();
        }

        private void 엔코더위치알림(decimal 현재위치, Int32 위치)
        {
            if (this.Status.InvokeRequired)
            {
                this.Status.BeginInvoke(new Action(() => { 엔코더위치알림(현재위치, 위치); }));
                return;
            }
            if(위치 == 0) this.tEncoder0.Text = 현재위치.ToString();
            else if(위치 == 1) this.tEncoder1.Text = 현재위치.ToString();
        }
    }
}
