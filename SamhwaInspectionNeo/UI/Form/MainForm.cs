using DevExpress.XtraWaitForm;
using MvUtils;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamhwaInspectionNeo.Schemas;
using DevExpress.XtraBars;

namespace SamhwaInspectionNeo
{
    public partial class MainForm : TabForm
    {
        private LocalizationMain 번역 = new LocalizationMain();
        private WaitForm WaitForm;
        //private FlowForm flowForm = null;
        //private StateForm stateForm = null;
        public MainForm()
        {
            InitializeComponent();
            //this.e결과뷰어.Visible = false;
            this.ShowWaitForm();
            this.e프로젝트.Caption = $"IVM: {환경설정.프로젝트번호}";
            this.SetLocalization();
            this.TabFormControl.SelectedPage = this.p검사하기;
            this.p환경설정.Enabled = false;
            //this.p검사내역.Enabled = false;
            //this.p그랩뷰어.Visible = false;
            this.Shown += MainFormShown;
            this.FormClosing += MainFormClosing;
            //this.TabFormControl.SelectedPageChanged += SelectedPageChanged;
            //this.t환경설정.SelectedPageChanged += SelectedTabPageChanged;
        }

        private void ShowWaitForm()
        {
            WaitForm = new WaitForm() { ShowOnTopMode = ShowFormOnTopMode.AboveAll };
            WaitForm.Show(this);
        }
        private void HideWaitForm()
        {
            WaitForm.Close();
        }

        private void MainFormShown(object sender, EventArgs e)
        {
            Global.Initialized += GlobalInitialized;
            Task.Run(() => { Global.Init(); });
        }

        private void GlobalInitialized(object sender, Boolean e)
        {
            this.BeginInvoke(new Action(() => GlobalInitialized(e)));
        }
        private void GlobalInitialized(Boolean e)
        {
            Global.Initialized -= GlobalInitialized;
            if (!e) { this.Close(); return; }
            this.HideWaitForm();
            Common.SetForegroundWindow(this.Handle.ToInt32());

            //// 로그인
            //Login login = new Login();
            //if (Utils.ShowDialog(login, this) == DialogResult.OK)
            //{
            //    Global.DxLocalization();
            //    this.Init();
            //    Global.Start();
            //}
            //else this.Close();

            //if (Global.환경설정.동작구분 == 동작구분.Live)
            //{
            //}
            //else
            //{
            ////자동로그인
            Global.환경설정.시스템관리자로그인();
            Global.DxLocalization();
            this.Init();
            Global.Start();
            //}
        }

        private void Init()
        {
            this.SetLocalization();
            this.p환경설정.Enabled = Global.환경설정.권한여부(유저권한구분.시스템);
            this.TabFormControl.AllowMoveTabs = false;
            this.TabFormControl.AllowMoveTabsToOuterForm = false;

            //if (Global.환경설정.동작구분 == 동작구분.Live)
            //    this.WindowState = FormWindowState.Maximized;
            //this.ShowHideControl();

            if (Global.환경설정.동작구분 != 동작구분.Live) return;

            /*
            foreach (Screen s in Screen.AllScreens)
            {
                Debug.WriteLine(s.Bounds, s.DeviceName);
                if (s.Primary) continue;
                if (s.Bounds.Width <= 1920)
                {
                    ShowFlowForm(s);
                    continue;
                }
                else ShowStateForm(s);
            }

            // 창이 생성되지 않았으면 메인 모니터에 띄움
            ShowFlowForm(Screen.PrimaryScreen);
            ShowStateForm(Screen.PrimaryScreen);
            */
        }
       
        private void CloseForm()
        {
            //this.e장치설정.Close();
            //this.e로그내역.Close();
            //this.e상태뷰어.Close();
            Global.Close();
        }

        private void MainFormClosing(object sender, FormClosingEventArgs e)
        {
            if (Global.환경설정.사용권한 == 유저권한구분.없음) this.CloseForm();
            else
            {
                e.Cancel = !MvUtils.Utils.Confirm(this, 번역.종료확인, Localization.확인.GetString());
                if (!e.Cancel) this.CloseForm();
            }
        }

        //private void SelectedPageChanged(object sender, DevExpress.XtraBars.TabFormSelectedPageChangedEventArgs e)
        //{
        //    ShowHideControl();
        //}
        //private void SelectedTabPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        //{
        //    ShowHideControl();
        //}
        //private void ShowHideControl()
        //{
        //    //this.e그랩뷰어.Shown(this.TabFormControl.SelectedPage == this.p그랩뷰어);
        //    //this.e장치설정.Shown(this.TabFormControl.SelectedPage == this.p환경설정 && this.t환경설정.SelectedTabPage == this.t장치설정);
        //    //this.e환경설정.Shown(this.TabFormControl.SelectedPage == this.p환경설정 && this.t환경설정.SelectedTabPage == this.t검사설정);
        //}

        private void SetLocalization()
        {
            this.Text = this.번역.타이틀;
            this.타이틀.Caption = this.번역.타이틀;
            this.p검사하기.Text = this.번역.검사하기;
            //this.p그랩뷰어.Text = this.번역.카메라;
            //this.p검사내역.Text = this.번역.검사내역;
            this.p환경설정.Text = this.번역.환경설정;
            //this.t검사설정.Text = this.번역.검사설정;
            //this.t장치설정.Text = this.번역.장치설정;
            //this.t로그내역.Text = this.번역.로그내역;
        }

        private class LocalizationMain
        {
            private enum Items
            {
                [Translation("Inspection", "검사하기", "Inšpekcia")]
                검사하기,
                [Translation("History", "검사내역", "História")]
                검사내역,
                [Translation("Preferences", "환경설정", "Predvoľby")]
                환경설정,
                [Translation("Settings", "검사설정", "Nastavenie")]
                검사설정,
                [Translation("Devices", "장치설정", "Zariadenia")]
                장치설정,
                [Translation("Cameras", "카메라", "Kamery")]
                카메라,
                [Translation("Logs", "로그내역", "Denníky")]
                로그내역,
                [Translation("Are you want to exit the program?", "프로그램을 종료하시겠습나까?", "Naozaj chcete ukončiť program?")]
                종료확인,
            }
            private String GetString(Items item) { return Localization.GetString(item); }

            public String 타이틀 { get { return Localization.제목.GetString(); } }
            public String 검사하기 { get { return GetString(Items.검사하기); } }
            public String 검사내역 { get { return GetString(Items.검사내역); } }
            public String 환경설정 { get { return GetString(Items.환경설정); } }
            public String 검사설정 { get { return GetString(Items.검사설정); } }
            public String 장치설정 { get { return GetString(Items.장치설정); } }
            public String 카메라 { get { return GetString(Items.카메라); } }
            public String 로그내역 { get { return GetString(Items.로그내역); } }
            public String 종료확인 { get { return GetString(Items.종료확인); } }
        }
    }
}