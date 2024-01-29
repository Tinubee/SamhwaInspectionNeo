using DevExpress.XtraEditors;
using MvUtils;
using System;
using System.Windows.Forms;
using SamhwaInspectionNeo.Schemas;

namespace SamhwaInspectionNeo.UI.Controls
{
    public partial class Config : XtraUserControl
    {
        private LocalizationConfig 번역 = new LocalizationConfig();
        public Config()
        {
            InitializeComponent();
            this.BindLocalization.DataSource = this.번역;
            this.g환경설정.Text = 환경설정.로그영역.GetString();
        }

        public void Init()
        {
            this.bind환경설정.DataSource = Global.환경설정;
            this.d기본경로.SelectedPath = Global.환경설정.기본경로;
            this.d문서저장.SelectedPath = Global.환경설정.문서저장경로;
            this.d사진저장.SelectedPath = Global.환경설정.사진저장경로;

            this.e기본경로.Text = this.d기본경로.SelectedPath;
            this.e문서저장.Text = this.d문서저장.SelectedPath;
            this.e사진저장.Text = this.d사진저장.SelectedPath;

            this.e양품저장.IsOn = Global.환경설정.사진저장OK;
            this.e불량저장.IsOn = Global.환경설정.사진저장NG;

            this.e20Point검사.IsOn = Global.환경설정.슬롯부20Point검사;
            this.e200Point검사.IsOn = Global.환경설정.슬롯부200Point검사;
            this.e너비검사.IsOn = Global.환경설정.너비측정검사;
            this.e높이검사.IsOn = Global.환경설정.높이측정검사;
            this.e상부표면검사.IsOn = Global.환경설정.상부표면검사;
            this.e하부표면검사.IsOn = Global.환경설정.하부표면검사;
            this.e큰홀검사.IsOn = Global.환경설정.큰원치수측정검사;
            this.e작은홀검사.IsOn = Global.환경설정.작은원치수측정검사;

            this.e기본경로.ButtonClick += E기본경로_ButtonClick;
            this.e문서저장.ButtonClick += E문서저장_ButtonClick;
            this.e사진저장.ButtonClick += E사진저장_ButtonClick;
            this.b설정저장.Click += b설정저장_Click;

            this.e양품저장.Toggled += E양품저장_Toggled;
            this.e불량저장.Toggled += E불량저장_Toggled;

            this.e20Point검사.Toggled += 검사설정변경;
            this.e200Point검사.Toggled += 검사설정변경;
            this.e너비검사.Toggled += 검사설정변경;
            this.e높이검사.Toggled += 검사설정변경;
            this.e상부표면검사.Toggled += 검사설정변경;
            this.e하부표면검사.Toggled += 검사설정변경;
            this.e큰홀검사.Toggled += 검사설정변경;
            this.e작은홀검사.Toggled += 검사설정변경;

            검사설정();
        }

        private void 검사설정()
        {
            Global.VM제어.글로벌변수제어.InspectUseSet("높이검사Pass", Convert.ToInt32(Global.환경설정.높이측정검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("너비검사Pass", Convert.ToInt32(Global.환경설정.너비측정검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("작은홀검사Pass", Convert.ToInt32(Global.환경설정.작은원치수측정검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("큰홀검사Pass", Convert.ToInt32(Global.환경설정.큰원치수측정검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("하부표면검사Pass", Convert.ToInt32(Global.환경설정.하부표면검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("상부표면검사Pass", Convert.ToInt32(Global.환경설정.상부표면검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("슬롯부200Pass", Convert.ToInt32(Global.환경설정.슬롯부200Point검사).ToString());
            Global.VM제어.글로벌변수제어.InspectUseSet("슬롯부20Pass", Convert.ToInt32(Global.환경설정.슬롯부20Point검사).ToString());
            UpdateView();
        }

        private void 검사설정변경(object sender, EventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;
            String tagName = ts.Tag.ToString();

            switch (tagName)
            {
                case "높이검사Pass":
                    Global.환경설정.높이측정검사 = ts.IsOn;
                    break;
                case "너비검사Pass":
                    Global.환경설정.너비측정검사 = ts.IsOn;
                    break;
                case "작은홀검사Pass":
                    Global.환경설정.작은원치수측정검사 = ts.IsOn;
                    break;
                case "큰홀검사Pass":
                    Global.환경설정.큰원치수측정검사 = ts.IsOn;
                    break;
                case "하부표면검사Pass":
                    Global.환경설정.하부표면검사 = ts.IsOn;
                    break;
                case "상부표면검사Pass":
                    Global.환경설정.상부표면검사 = ts.IsOn;
                    break;
                case "슬롯부200Pass":
                    Global.환경설정.슬롯부200Point검사 = ts.IsOn;
                    break;
                case "슬롯부20Pass":
                    Global.환경설정.슬롯부20Point검사 = ts.IsOn;
                    break;
            }

            Global.VM제어.글로벌변수제어.InspectUseSet(tagName, Convert.ToInt32(Global.환경설정.높이측정검사).ToString());
            UpdateView();
        }

        private void UpdateView()
        {
            Global.VM제어.글로벌변수제어.Init();
            Global.MainForm.e변수설정.UpdateGridView();
        }

        private void E불량저장_Toggled(object sender, EventArgs e)
        {
            if (e불량저장.IsOn)
            {
                Global.환경설정.사진저장NG = true;
                return;
            }
            Global.환경설정.사진저장NG = false;
        }

        private void E양품저장_Toggled(object sender, EventArgs e)
        {
            if (e양품저장.IsOn)
            {
                Global.환경설정.사진저장OK = true;
                return;
            }
            Global.환경설정.사진저장OK = false;
        }

        public void Close()
        {

        }

        private void E사진저장_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d사진저장.ShowDialog() == DialogResult.OK)
                this.e사진저장.Text = this.d사진저장.SelectedPath;
        }

        private void E문서저장_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d문서저장.ShowDialog() == DialogResult.OK)
                this.e문서저장.Text = this.d문서저장.SelectedPath;
        }

        private void E기본경로_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (this.d기본경로.ShowDialog() == DialogResult.OK)
                this.e기본경로.Text = this.d기본경로.SelectedPath;
        }

        private void b설정저장_Click(object sender, EventArgs e)
        {
            this.bind환경설정.EndEdit();
            if (!MvUtils.Utils.Confirm(번역.저장확인, Localization.확인.GetString())) return;
            Global.환경설정.Save();
            Global.VM제어.Save();
            Global.정보로그(환경설정.로그영역.GetString(), 번역.설정저장, 번역.저장완료, true);
        }

        private class LocalizationConfig
        {
            private enum Items
            {
                [Translation("Save", "설정저장")]
                설정저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save your preferences?", "환경설정을 저장하시겠습니까?")]
                저장확인,
            }

            public String 기본경로 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.기본경로))); } }
            public String 문서저장 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.문서저장경로))); } }
            public String 사진저장 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.사진저장경로))); } }
            public String 사진저장OK { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.사진저장OK))); } }
            public String 사진저장NG { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.사진저장NG))); } }
            public String 결과보관 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.결과보관))); } }
            public String 로그보관 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.로그보관))); } }
            public String 결과자릿수 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.결과자릿수))); } }
            public String 검사여부 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.검사여부))); } }
            //public String 크롭여부 { get { return Localization.GetString(typeof(환경설정).GetProperty(nameof(환경설정.크롭여부))); } }

            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
        }
    }
}
