using DevExpress.XtraEditors;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static SamhwaInspectionNeo.Schemas.신호제어;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class TirggerBoard : XtraUserControl
    {
        private 조명포트 선택포트 = 조명포트.None;

        public TirggerBoard()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.e포트.Properties.DataSource = Enum.GetValues(typeof(조명포트));
            this.b트리거보드초기화.Click += B트리거보드초기화_Click;
            this.b엔코더초기화.Click += B엔코더초기화_Click;
            this.b트리거초기화.Click += B트리거초기화_Click;

            this.b해제하기.Click += B해제하기;
            this.b연결하기.Click += B연결하기;

            this.e포트.EditValueChanging += 포트변경;
            this.e포트.CustomDisplayText += 포트표현;

            Global.트리거보드제어.엔코더위치알림 += 엔코더위치알림;
            Global.트리거보드제어.트리거카운트변경알림 += 트리거카운트변경알림;

            Global.트리거보드제어.트리거보드연결알림 += 트리거보드연결알림;
            Global.트리거보드제어.트리거보드상태알림 += 트리거보드상태알림;
        }

        private void 트리거보드상태알림()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => 트리거보드상태알림()));
                return;
            }

            this.e포트.Enabled = !Global.장치상태.트리거보드;
            this.b연결하기.Enabled = !Global.장치상태.트리거보드;
            this.b해제하기.Enabled = Global.장치상태.트리거보드;
        }

        private void 트리거보드연결알림()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => 트리거보드연결알림()));
                return;
            }

            this.t펌웨어버전.Text = $"Ver. {Global.트리거보드제어.펌웨어버전}";
            this.t로직버전.Text = $"Ver. {Global.트리거보드제어.로직버전}";

            this.t트리거0.Text = Global.트리거보드제어.트리거들[0].횟수.ToString();
            this.t트리거1.Text = Global.트리거보드제어.트리거들[1].횟수.ToString();
            this.t트리거2.Text = Global.트리거보드제어.트리거들[2].횟수.ToString();
            this.t트리거3.Text = Global.트리거보드제어.트리거들[3].횟수.ToString();

            this.tEncoder0.Text = Global.트리거보드제어.엔코더들[0].현재위치.ToString();
            this.tEncoder1.Text = Global.트리거보드제어.엔코더들[1].현재위치.ToString();

            Global.트리거보드제어.Start();
        }

        private void 포트표현(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value == null) return;
                조명포트 지그 = (조명포트)e.Value;
                e.DisplayText = $"{MvUtils.Utils.GetDescription(지그)}";
            }
            catch { e.DisplayText = String.Empty; }
        }

        private void 포트변경(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            if (e.NewValue == null) return;

            조명포트 포트 = (조명포트)e.NewValue;
            this.선택포트 = 포트;
        }

        private void 연결가능한포트불러오기()
        {
            String[] portName = SerialPort.GetPortNames();
        }

        private void B연결하기(object sender, EventArgs e)
        {
            Global.트리거보드제어.Init(this.선택포트);
        }

        private void B해제하기(object sender, EventArgs e) => Global.트리거보드제어.Close();

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

        private void 트리거카운트변경알림(Decimal 현재횟수, Int32 위치)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { 트리거카운트변경알림(현재횟수, 위치); }));
                return;
            }

            if (위치 == 0) this.t트리거0.Text = 현재횟수.ToString();
            else if (위치 == 1) this.t트리거1.Text = 현재횟수.ToString();
            else if (위치 == 2) this.t트리거2.Text = 현재횟수.ToString();
            else if (위치 == 3) this.t트리거3.Text = 현재횟수.ToString();
        }

        private void 엔코더위치알림(Int32 현재위치, Int32 위치)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(() => { 엔코더위치알림(현재위치, 위치); }));
                return;
            }
            if (위치 == 0) this.tEncoder0.Text = 현재위치.ToString();
            else if (위치 == 1) this.tEncoder1.Text = 현재위치.ToString();
        }
    }
}
