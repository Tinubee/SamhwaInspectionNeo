using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using SamhwaInspectionNeo.Schemas;
using DevExpress.XtraBars;
using SamhwaInspectionNeo.UI.Form;
using DevExpress.XtraWaitForm;
using System.Diagnostics;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using static DevExpress.XtraEditors.Mask.MaskSettings;

namespace SamhwaInspectionNeo
{
    public partial class MainForm : TabForm
    {
        private LocalizationMain 번역 = new LocalizationMain();
        private UI.Form.WaitForm WaitForm;
        public TrendReportViewer TrendReportViewer;

        public MainForm()
        {
            InitializeComponent();
            this.ShowWaitForm();
            this.e프로젝트.Caption = $"IVM: {환경설정.프로젝트번호}";
            this.SetLocalization();
            this.TabFormControl.SelectedPage = this.p검사하기;
            this.p환경설정.Enabled = false;
            this.Shown += MainFormShown;
            this.FormClosing += MainFormClosing;
        }

        public async void GetProgramGitDate()
        {
            try
            {
                HttpClient client = new HttpClient();
                string url = $"https://api.github.com/repos/Tinubee/SamhwaInspectionNeo/commits?per_page=1";
                client.DefaultRequestHeaders.UserAgent.TryParseAdd("request"); // GitHub API requires a user-agent

                var response = await client.GetAsync(url);
                var jsonString = await response.Content.ReadAsStringAsync();
                var jsonArray = JArray.Parse(jsonString);

                var lastCommit = jsonArray[0];
                var lastCommitDateStr = lastCommit["commit"]["committer"]["date"].ToString();
                if (!lastCommitDateStr.EndsWith("Z") && !lastCommitDateStr.Contains("+"))
                {
                    lastCommitDateStr += "Z";
                }
                // Parse the UTC time
                DateTimeOffset utcDateTime = DateTimeOffset.Parse(lastCommitDateStr);

                // Convert to Korean Standard Time (KST)
                TimeZoneInfo kstZone = TimeZoneInfo.FindSystemTimeZoneById("Korea Standard Time");
                DateTimeOffset kstDateTime = TimeZoneInfo.ConvertTime(utcDateTime, kstZone);

                Global.환경설정.마지막커밋시간 = kstDateTime.ToString("yyyy-MM-dd HH:mm:ss");
            }
            catch(Exception ee)
            {
                Common.DebugWriteLine("Git Message", 로그구분.오류, ee.Message);
            }
        }


        public void ShowWaitForm()
        {
            WaitForm = new UI.Form.WaitForm() { ShowOnTopMode = ShowFormOnTopMode.AboveAll };
            WaitForm.Show(this);
        }
        public void HideWaitForm()
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

            //GetProgramGitDate();
        }
        private void GlobalInitialized(Boolean e)
        {
            Global.Initialized -= GlobalInitialized;
            if (!e) { this.Close(); return; }
            this.HideWaitForm();
            Common.SetForegroundWindow(this.Handle.ToInt32());

            Global.환경설정.시스템관리자로그인();
            Global.DxLocalization();
            this.Init();
            Global.Start();
        }

        private void Init()
        {
            this.SetLocalization();
            this.e결과뷰어.Init();
            this.e검사설정.Init();
            this.e마스터설정.Init();
            this.e장치설정.Init();
            this.e검사내역.Init();
            this.resultePivot1.Init();
            this.e마스터검사내역.Init();
            this.e상태뷰어.Init();
            this.e로그내역.Init();
            this.e변수설정.Init();
            this.p환경설정.Enabled = Global.환경설정.권한여부(유저권한구분.시스템);
            this.TabFormControl.AllowMoveTabs = false;
            this.TabFormControl.AllowMoveTabsToOuterForm = false;
            this.e최근커밋.Caption = $"Last Commit : {Global.환경설정.마지막커밋시간}"; ;
            
            if (Global.환경설정.동작구분 == 동작구분.Live)
                this.WindowState = FormWindowState.Maximized;

            //if (Global.환경설정.동작구분 != 동작구분.Live) return;

            //foreach (Screen s in Screen.AllScreens)
            //{
            //    Debug.WriteLine(s.Bounds, s.DeviceName);
            //    if (s.Primary) continue;
            //    ShowTrendReportForm(s);
            //}
            //// 창이 생성되지 않았으면 메인 모니터에 띄움
            //ShowTrendReportForm(Screen.PrimaryScreen);
        }
        private void ShowTrendReportForm(Screen s)
        {
            if (this.TrendReportViewer != null) return;
            this.TrendReportViewer = new TrendReportViewer() { StartPosition = FormStartPosition.Manual, WindowState = FormWindowState.Maximized };
            this.TrendReportViewer.SetBounds(s.WorkingArea.X, s.WorkingArea.Y, s.WorkingArea.Width, s.WorkingArea.Height);
            this.TrendReportViewer.FormClosed += StateFormClosed;
            this.TrendReportViewer.Show(this);
        }
        private void StateFormClosed(object sender, FormClosedEventArgs e)
        {
            this.TrendReportViewer?.Dispose();
            this.TrendReportViewer = null;
        }
        private void CloseForm()
        {
            this.e장치설정.Close();
            this.e로그내역.Close();
            this.e상태뷰어.Close();
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

        private void SetLocalization()
        {
            this.Text = this.번역.타이틀;
            this.타이틀.Caption = this.번역.타이틀;
            this.p검사하기.Text = this.번역.검사하기;
            this.p검사내역.Text = this.번역.검사내역;
            this.p환경설정.Text = this.번역.환경설정;
            this.p로그내역.Text = this.번역.로그내역;
        }

        private class LocalizationMain
        {
            private enum Items
            {
                [Translation("Inspection", "검사하기")]
                검사하기,
                [Translation("History", "검사내역")]
                검사내역,
                [Translation("Preferences", "환경설정")]
                환경설정,
                [Translation("Settings", "검사설정")]
                검사설정,
                [Translation("Devices", "장치설정")]
                장치설정,
                [Translation("Cameras", "카메라")]
                카메라,
                [Translation("Logs", "로그내역")]
                로그내역,
                [Translation("Are you want to exit the program?", "프로그램을 종료하시겠습나까?")]
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