using DevExpress.XtraEditors;
using SamhwaInspectionNeo.Schemas;
using System;

namespace SamhwaInspectionNeo.UI.Controls
{
    public partial class DeviceSettings : XtraUserControl
    {
        private LocalizationConfig 번역 = new LocalizationConfig();
        public DeviceSettings()
        {
            InitializeComponent();
        }

        public void Init()
        {
            this.SetLocalization();
            this.e카메라.Init();
            this.e기본설정.Init();
            this.e유저관리.Init();
            this.e입출신호.Init();
            this.e트리거보드.Init();
           
            Global.신호제어.원점복귀알림 += 원점복귀알림;
        }

        private void 원점복귀알림()
        {
            Global.신호제어.원점복귀완료 = false;
            Global.그랩제어.GetItem(카메라구분.Cam01)?.Stop();
            Global.그랩제어.GetItem(카메라구분.Cam02)?.Stop();
            Global.그랩제어.GetItem(카메라구분.Cam03)?.Stop();
            Global.그랩제어.GetItem(카메라구분.Cam04)?.Stop();
            Global.트리거보드제어.ClearAll();
        }

        private void SetLocalization()
        {
            this.p설정.Text = this.번역.설정;
            this.p제어.Text = this.번역.제어;
            this.p유저정보.Text = this.번역.유저정보;
        }
        public void Close()
        {
            this.e카메라.Close();
            this.e기본설정.Close();
            this.e유저관리.Close();
        }

        private void 트리거보드리셋(object sender, EventArgs e)
        {
            if (!MvUtils.Utils.Confirm("트리거 보드의 위치를 초기화 하시겠습니까?")) return;
        }

        private class LocalizationConfig
        {
            private enum Items
            {
                [Translation("Control & Monitoring", "제어 및 모니터링")]
                제어,
                [Translation("Config", "환경설정")]
                설정,
                [Translation("User Profile", "유저정보")]
                유저정보,

                [Translation("Save", "설정저장")]
                설정저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save your device setting?", "장치설정을 저장하시겠습니까?")]
                저장확인,
            }
            private String GetString(Items item) { return Localization.GetString(item); }

            public String 제어 { get { return GetString(Items.제어); } }
            public String 설정 { get { return GetString(Items.설정); } }
            public String 유저정보 { get { return GetString(Items.유저정보); } }
            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
        }
    }
}
