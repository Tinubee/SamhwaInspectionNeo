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
            this.e소수자리.Value = Global.환경설정.결과자릿수;

            this.e기본경로.Text = this.d기본경로.SelectedPath;
            this.e문서저장.Text = this.d문서저장.SelectedPath;
            this.e사진저장.Text = this.d사진저장.SelectedPath;

            this.e양품저장.IsOn = Global.환경설정.사진저장OK;
            this.e불량저장.IsOn = Global.환경설정.사진저장NG;

            this.e강제OK.IsOn = Global.환경설정.강제OK배출;
            this.e강제NG.IsOn = Global.환경설정.강제NG배출;

            this.e기본경로.ButtonClick += E기본경로_ButtonClick;
            this.e문서저장.ButtonClick += E문서저장_ButtonClick;
            this.e사진저장.ButtonClick += E사진저장_ButtonClick;
            this.b설정저장.Click += b설정저장_Click;

            this.e양품저장.Toggled += E양품저장_Toggled;
            this.e불량저장.Toggled += E불량저장_Toggled;
            this.e치수검사.Toggled += E치수검사_Toggled;
            this.e표면검사.Toggled += E표면검사_Toggled;

            this.e강제OK.Toggled += 강제결과배출;
            this.e강제NG.Toggled += 강제결과배출;
        }

        private void 강제결과배출(object sender, EventArgs e)
        {
            ToggleSwitch ts = sender as ToggleSwitch;

            if (e강제OK.IsOn && ts.Name == "e강제OK")
            {
                Global.환경설정.강제OK배출 = true;
                Global.환경설정.강제NG배출 = false;
                if (e강제NG.IsOn)
                {
                    e강제NG.IsOn = false;
                }
            }else if (e강제NG.IsOn)
            {
                Global.환경설정.강제OK배출 = false;
                Global.환경설정.강제NG배출 = true;
                if (e강제OK.IsOn)
                {
                    e강제OK.IsOn = false;
                }
            }
            else
            {
                Global.환경설정.강제OK배출 = false;
                Global.환경설정.강제NG배출 = false;
            }
        }

        private void E표면검사_Toggled(object sender, EventArgs e)
        {
            if (e표면검사.IsOn)
            {
                Global.환경설정.표면검사사용여부 = true;
                return;
            }
            Global.환경설정.표면검사사용여부 = false;
        }

        private void E치수검사_Toggled(object sender, EventArgs e)
        {
            if (e치수검사.IsOn)
            {
                Global.환경설정.치수검사사용여부 = true;
                return;
            }
            Global.환경설정.치수검사사용여부 = false;
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
