using DevExpress.XtraEditors;
using MvUtils;
using System;
using System.Diagnostics;
using SamhwaInspectionNeo.Schemas;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Threading;

namespace SamhwaInspectionNeo.UI.Controls
{
    public partial class DeviceLamp : XtraUserControl
    {
        private const String 로그영역 = "장치상태표시";
        private LocalizationConfig 번역 = new LocalizationConfig();
        private Int32 구분;
        private Int32 촬영횟수;
        public DeviceLamp()
        {
            InitializeComponent();
        }

        private 장치상태 장치통신;
        private 장치상태 조명장치;
        private 장치상태 카메라1;
        private 장치상태 카메라2;
        private 장치상태 카메라3;
        private 장치상태 카메라4;

        public void Init()
        {
            this.장치통신 = new 장치상태(this.e장치통신, true);
            this.조명장치 = new 장치상태(this.e조명장치);
            this.카메라1 = new 장치상태(this.e카메라1);
            this.카메라2 = new 장치상태(this.e카메라2);
            this.카메라3 = new 장치상태(this.e카메라3);
            this.카메라4 = new 장치상태(this.e카메라4);
            //Global.장치통신.통신상태알림 += 통신상태알림;
            this.통신상태알림();

            this.e카메라1.Click += 수동촬영;
            this.e카메라2.Click += 수동촬영;
            this.e카메라3.Click += 수동촬영;
            this.e카메라4.Click += 수동촬영;
        }

        private void 수동촬영(object sender, EventArgs e)
        {
            try
            {
                if (!MvUtils.Utils.Confirm(번역.수동촬영, Localization.확인.GetString())) return;

                SvgImageBox bx = sender as SvgImageBox;
                구분 = Convert.ToInt32(bx.Tag);
                if (구분 == 1) return;
                if (구분 == 2) this.촬영횟수 = 1;
                else this.촬영횟수 = 4;

                HikeGigE 장치 = Global.그랩제어.GetItem((카메라구분)구분) as HikeGigE;
                //트리거소스 소프트웨어 트리거로 변경.

                for (int lop = 0; lop < this.촬영횟수; lop++)
                {
                    if (장치.MatImage.Count == 0)
                    {
                        장치.TrigSource = MvCamCtrl.NET.CameraParams.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE;
                        장치.트리거소스적용();
                        장치.Ready();
                    }
                    Thread.Sleep(200);
                    장치.TrigForce();
                  
                    if(lop == this.촬영횟수 - 1)
                    {
                        장치.TrigSource = MvCamCtrl.NET.CameraParams.MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0;
                        장치.트리거소스적용();
                    }
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "수동촬영오류", $"{(카메라구분)구분} 수동촬영에 실패하였습니다.\n" + ex.Message, true);
            }

        }

        private void 통신상태알림()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Action(통신상태알림));
                return;
            }

            //if (Global.장치통신.정상여부) this.장치통신.Set(Global.장치통신.통신확인핑퐁 ? 상태구분.정상 : 상태구분.대기);
            //else this.장치통신.Set(상태구분.오류);
            //if (Global.환경설정.동작구분 != 동작구분.Live) return;

            this.카메라1.Set(Global.장치상태.카메라1);
            this.카메라2.Set(Global.장치상태.카메라2);
            this.카메라3.Set(Global.장치상태.카메라3);
            this.카메라4.Set(Global.장치상태.카메라4);
            this.장치통신.Set(Global.장치상태.장치통신);
            //this.조명장치.Set(Global.장치상태.조명장치);
        }

        private enum 상태구분
        {
            없음,
            대기,
            정상,
            오류,
        }
        private class 장치상태
        {
            private SvgImageBox 도구;
            private 상태구분 현재상태 = 상태구분.없음;
            private DevExpress.Utils.Svg.SvgImage 대기 = null;
            private DevExpress.Utils.Svg.SvgImage 정상 = null;
            private DevExpress.Utils.Svg.SvgImage 오류 = null;

            public 장치상태(SvgImageBox tool, Boolean HasWait = false)
            {
                this.도구 = tool;
                this.정상 = MvUtils.Utils.SetSvgStyle(tool.SvgImage, MvUtils.Utils.SvgStyles.Green);
                this.오류 = MvUtils.Utils.SetSvgStyle(tool.SvgImage, MvUtils.Utils.SvgStyles.Red);
                if (HasWait) this.대기 = MvUtils.Utils.SetSvgStyle(tool.SvgImage, MvUtils.Utils.SvgStyles.Blue);
                this.도구.SvgImage = this.오류;
            }

            public void Set(Boolean 상태)
            {
                this.Set(상태 ? 상태구분.정상 : 상태구분.오류);
            }

            public void Set(상태구분 상태)
            {
                if (this.현재상태 == 상태) return;
                this.현재상태 = 상태;
                if (상태 == 상태구분.정상) this.도구.SvgImage = this.정상;
                else if (상태 == 상태구분.오류) this.도구.SvgImage = this.오류;
                else if (상태 == 상태구분.대기) this.도구.SvgImage = this.대기;
            }
        }

        private class LocalizationConfig
        {
            private enum Items
            {
                [Translation("do you want proceed with manual shot?", "수동촬영을 진행하시겠습니까?")]
                수동촬영,
            }

            public String 수동촬영 { get { return Localization.GetString(Items.수동촬영); } }
        }
    }
}
