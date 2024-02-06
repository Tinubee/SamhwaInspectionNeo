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
            this.e강제배출.IsOn = Global.환경설정.강제배출;
            this.e강제배출.EditValueChanged += 배출구분Changed;

            this.SetLocalization();
            this.e카메라.Init();
            this.e기본설정.Init();
            this.e유저관리.Init();
            this.e입출신호.Init();
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

        private void 배출구분Changed(object sender, EventArgs e)
        {
            Global.환경설정.강제배출 = this.e강제배출.IsOn;
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
