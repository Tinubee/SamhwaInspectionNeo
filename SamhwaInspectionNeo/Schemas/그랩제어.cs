using Euresys.MultiCam;
using MvCamCtrl.NET;
using MvCamCtrl.NET.CameraParams;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SamhwaInspectionNeo.Schemas
{
    public enum 카메라구분
    {
        [Bindable(false)]
        None,
        [Description("치수측정")]
        Cam01 = 1,
        [Description("유무검사")]
        Cam02 = 2,
        [Description("상부표면검사")]
        Cam03 = 3,
        [Description("하부표면검사")]
        Cam04 = 4,
    }

    #region Enum Setting by LHD

    public enum ProductIndex
    {
        PRODUCT_INDEX1,
        PRODUCT_INDEX2,
        PRODUCT_INDEX3,
        PRODUCT_INDEX4,
        PRODUCT_INDEX5,
        PRODUCT_INDEX6,
    }
    public enum Connector
    {
        M,
        A,
        B,
    }

    public enum AcquisitionMode
    {
        PAGE = 0,
        LONGPAGE = 1,
        WEB = 2,
    }

    public enum TrigMode
    {
        IMMEDIATE,
        HARD,
        SOFT,
        COMBINED,
    }
    #endregion

    #region ConvertStringToVar
    public static class ChannelState
    {
        [Description("채널은 그래버를 소유하고 있지만 잠금상태는 아님.")]
        public const string IDLE = "IDLE";

        [Description("채널은 그래버를 사용합니다.")]
        public const string ACTIVE = "ACTIVE";

        [Description("채널에 그래버가 없습니다.")]
        public const string ORPHAN = "ORPHAN";

        [Description("채널은 그래버를 잠그고 acquisition sequence를 시작할 준비가 됨.")]
        public const string READY = "READY";

        [Description("채널의 상태를 ORPHAN으로 설정합니다.")]
        public const string FREE = "FREE";
    }
    #endregion ConvertStringToVar

    public class 그랩제어 : Dictionary<카메라구분, 카메라장치>
    {
        //public delegate void 그랩완료대리자(카메라구분 구분, CogImage8Grey 이미지);
        public delegate void 그랩완료대리자(카메라구분 구분, Mat 이미지);
        public event 그랩완료대리자 그랩완료보고;

        [JsonIgnore]
        private const string 로그영역 = "카메라";
        [JsonIgnore]
        private string 저장파일 { get { return Path.Combine(Global.환경설정.기본경로, "Cameras.json"); } }
        //[JsonIgnore]
        //public Boolean 정상여부 { get { return !this.Values.Any(e => !e.상태); } }

        public EuresysLink 카메라1 = null;
        public HikeGigE 카메라2 = null;
        public HikeGigE 카메라3 = null;
        public HikeGigE 카메라4 = null;

        public Boolean Init()
        {
            try
            {
                MC.OpenDriver();
                //Debug.WriteLine("OpenDriver");

                this.카메라1 = new EuresysLink(카메라구분.Cam01) { 코드 = "" };
                this.카메라2 = new HikeGigE() { 구분 = 카메라구분.Cam02, 코드 = "" };
                this.카메라3 = new HikeGigE() { 구분 = 카메라구분.Cam03, 코드 = "" };
                this.카메라4 = new HikeGigE() { 구분 = 카메라구분.Cam04, 코드 = "" };
                this.Add(카메라구분.Cam01, this.카메라1);
                this.Add(카메라구분.Cam02, this.카메라2);
                this.Add(카메라구분.Cam03, this.카메라3);
                this.Add(카메라구분.Cam04, this.카메라4);

                // 카메라 설정 저장정보 로드
                카메라장치 정보;
                List<카메라장치> 자료 = Load();
                if (자료 != null)
                {
                    foreach (카메라장치 설정 in 자료)
                    {
                        정보 = this.GetItem(설정.구분);
                        if (정보 == null) continue;
                        정보.Set(설정);
                    }
                }

                List<CCameraInfo> 카메라들 = new List<CCameraInfo>();
                Int32 nRet = CSystem.EnumDevices(CSystem.MV_GIGE_DEVICE, ref 카메라들);// | CSystem.MV_USB_DEVICE
                if (!Validate("Enumerate devices fail!", nRet, true)) return false;

                for (int i = 0; i < 카메라들.Count; i++)
                {
                    CGigECameraInfo gigeInfo = 카메라들[i] as CGigECameraInfo;
                    HikeGigE gige = this.GetItem(gigeInfo.chSerialNumber) as HikeGigE;
                    if (gige == null) continue;
                    //Debug.WriteLine(gigeInfo.chSerialNumber, "시리얼");
                    gige.Init(gigeInfo);
                    //if (gige.상태) gige.Start();
                }

                Debug.WriteLine($"카메라 갯수: {this.Count}");
                GC.Collect();
                return true;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "카메라 연결 실패", "카메라 연결 작업에 실패하였습니다.", true);
                return false;
            }

        }

        private List<카메라장치> Load()
        {
            if (!File.Exists(this.저장파일)) return null;
            return JsonConvert.DeserializeObject<List<카메라장치>>(File.ReadAllText(this.저장파일), MvUtils.Utils.JsonSetting());
        }

        public void Save()
        {
            if (!MvUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this.Values, MvUtils.Utils.JsonSetting())))
                Global.오류로그(로그영역, "카메라 설정 저장", "카메라 설정 저장에 실패하였습니다.", true);
        }

        public void Close()
        {
            this.Save();
            foreach (카메라장치 장치 in this.Values)
                장치?.Close();
        }

        public void Ready(카메라구분 카메라) => this.GetItem(카메라)?.Ready();

        public void 그랩완료(카메라구분 카메라, Mat 이미지)
        {
            if (카메라 == 카메라구분.Cam01)
            {
                Task.Run(() =>
                {
                    Global.VM제어.GetItem(카메라).Run(이미지, null);
                });
            }
            //if (Global.장치통신.자동수동여부)
            //{
            //    Int32 검사번호 = Global.장치통신.촬영위치번호(카메라);
            //    Debug.WriteLine($"{카메라}: {검사번호}", "그랩완료");
            //    if (검사번호 > 0)
            //    {
            //        Task.Run(() =>
            //        {
            //            //Debug.WriteLine($"{구분} : Flow Run");
            //            Global.VM제어.GetItem(카메라).Run(이미지, null);
            //            ImageSave(이미지, 카메라, 검사번호, 결과구분.OK);
            //        });
            //    }
            //    else Global.경고로그(로그영역, "비전검사", $"카메라 [{MvUtils.Utils.GetDescription(카메라)}]의 검사 Index가 없습니다.", true);
            //}
            //else this.그랩완료보고?.Invoke(카메라, 이미지);
            //Global.조명제어?.TurnOff(카메라);
            this.그랩완료보고?.Invoke(카메라, 이미지);
        }


        public void ImageSave(Mat 이미지, 카메라구분 카메라, Int32 검사번호, 결과구분 결과)
        {
            if (!Global.환경설정.사진저장OK && !Global.환경설정.사진저장NG) return;
            List<String> paths = new List<String> { Global.환경설정.사진저장, MvUtils.Utils.FormatDate(DateTime.Now, "{0:yyyy-MM-dd}"), Global.환경설정.선택모델.ToString(), 카메라.ToString() };
            String name = $"{검사번호.ToString("d4")}_{MvUtils.Utils.FormatDate(DateTime.Now, "{0:HHmmss}")}.png";//_{결과.ToString()}
            String path = Common.CreateDirectory(paths);
            if (String.IsNullOrEmpty(path))
            {
                Global.오류로그(로그영역, "이미지 저장", $"[{Path.Combine(paths.ToArray())}] 디렉토리를 만들 수 없습니다.", true);
                return;
            }
            String file = Path.Combine(path, name);
            Debug.WriteLine($"{이미지.Size()}: {file}", $"{카메라} 그랩완료");
            Task.Run(() =>
            {
                Int32 level = 3; // 0에서 9까지의 값 중 선택
                Int32[] @params = new[] { (Int32)ImwriteFlags.PngCompression, level };
                Cv2.ImWrite(file, 이미지, @params);
                이미지.Dispose();
            });
        }

        public 카메라장치 GetItem(카메라구분 구분)
        {
            if (this.ContainsKey(구분)) return this[구분];
            return null;
        }

        private 카메라장치 GetItem(String serial) => this.Values.Where(e => e.코드 == serial).FirstOrDefault();

        #region 오류메세지
        public static Boolean Validate(String message, Int32 errorNum, Boolean show)
        {
            if (errorNum == CErrorDefine.MV_OK) return true;

            String errorMsg = String.Empty;
            switch (errorNum)
            {
                case CErrorDefine.MV_E_HANDLE: errorMsg = "Error or invalid handle"; break;
                case CErrorDefine.MV_E_SUPPORT: errorMsg = "Not supported function"; break;
                case CErrorDefine.MV_E_BUFOVER: errorMsg = "Cache is full"; break;
                case CErrorDefine.MV_E_CALLORDER: errorMsg = "Function calling order error"; break;
                case CErrorDefine.MV_E_PARAMETER: errorMsg = "Incorrect parameter"; break;
                case CErrorDefine.MV_E_RESOURCE: errorMsg = "Applying resource failed"; break;
                case CErrorDefine.MV_E_NODATA: errorMsg = "No data"; break;
                case CErrorDefine.MV_E_PRECONDITION: errorMsg = "Precondition error, or running environment changed"; break;
                case CErrorDefine.MV_E_VERSION: errorMsg = "Version mismatches"; break;
                case CErrorDefine.MV_E_NOENOUGH_BUF: errorMsg = "Insufficient memory"; break;
                case CErrorDefine.MV_E_UNKNOW: errorMsg = "Unknown error"; break;
                case CErrorDefine.MV_E_GC_GENERIC: errorMsg = "General error"; break;
                case CErrorDefine.MV_E_GC_ACCESS: errorMsg = "Node accessing condition error"; break;
                case CErrorDefine.MV_E_ACCESS_DENIED: errorMsg = "No permission"; break;
                case CErrorDefine.MV_E_BUSY: errorMsg = "Device is busy, or network disconnected"; break;
                case CErrorDefine.MV_E_NETER: errorMsg = "Network error"; break;
                default: errorMsg = "Unknown error"; break;
            }

            Global.오류로그("Camera", "Error", $"[{errorNum}] {message} {errorMsg}", show);
            return false;
        }
        #endregion
    }

    public class 카메라장치
    {
        [JsonProperty("Camera"), Translation("Camera", "카메라")]
        public virtual 카메라구분 구분 { get; set; } = 카메라구분.None;
        [JsonIgnore, Translation("Index", "번호")]
        public virtual Int32 번호 { get; set; } = 0;
        [JsonProperty("Serial"), Translation("Serial", "Serial")]
        public virtual String 코드 { get; set; } = String.Empty;
        [JsonIgnore, Translation("Name", "명칭")]
        public virtual String 명칭 { get; set; } = String.Empty;
        [JsonProperty("Description"), Translation("Description", "설명")]
        public virtual String 설명 { get; set; } = String.Empty;
        [JsonProperty("IpAddress"), Translation("IP", "IP")]
        public virtual String 주소 { get; set; } = String.Empty;
        [JsonProperty("Timeout"), Description("Timeout"), Translation("Timeout", "제한시간")]
        public virtual Double 시간 { get; set; } = 1000;
        [JsonProperty("Exposure"), Description("Exposure"), Translation("Exposure", "노출")]
        public virtual Single 노출 { get; set; } = 300;
        [JsonProperty("BlackLevel"), Description("Black Level"), Translation("BlackLevel", "밝기")]
        public virtual UInt32 밝기 { get; set; } = 0;
        [JsonProperty("Contrast"), Description("Contrast"), Translation("Contrast", "대비")]
        public virtual Single 대비 { get; set; } = 10;
        [JsonProperty("Width"), Description("Width"), Translation("Width", "가로")]
        public virtual Int32 가로 { get; set; } = 0;
        [JsonProperty("Height"), Description("Height"), Translation("Height", "세로")]
        public virtual Int32 세로 { get; set; } = 0;
        [JsonProperty("OffsetX"), Description("OffsetX"), Translation("OffsetX", "OffsetX")]
        public virtual Int32 OffsetX { get; set; } = 0;

        [JsonIgnore, Description("카메라 초기화 상태"), Translation("Live", "상태")]
        public virtual Boolean 상태 { get; set; } = false;

        [JsonIgnore]
        public const String 로그영역 = "카메라장치";

        public virtual void Set(카메라장치 장치)
        {
            if (장치 == null) return;
            this.코드 = 장치.코드;
            this.설명 = 장치.설명;
            this.시간 = 장치.시간;
            this.노출 = 장치.노출;
            this.대비 = 장치.대비;
            this.밝기 = 장치.밝기;
            this.가로 = 장치.가로;
            this.세로 = 장치.세로;
            this.OffsetX = 장치.OffsetX;
        }

        public virtual Boolean Init() => false;
        public virtual Boolean Ready() => false;
        public virtual Boolean Start() => false;
        public virtual Boolean Stop() => false;
        public virtual Boolean Close() => false;
        public virtual void TurnOn() => Global.조명제어.TurnOn(this.구분);
        public virtual void TurnOff() => Global.조명제어.TurnOff(this.구분);
    }

    public class HikeGigE : 카메라장치
    {
        [JsonIgnore]
        private CCamera Camera = null;
        [JsonIgnore]
        private CCameraInfo Device;
        [JsonIgnore]
        private cbOutputExdelegate ImageCallBackDelegate;
        [JsonIgnore]
        public uint ImageCount = 6;
        public Boolean Init(CGigECameraInfo info)
        {
            try
            {
                this.Camera = new CCamera();
                this.Device = info;
                this.ImageCallBackDelegate = new cbOutputExdelegate(ImageCallBack);

                this.명칭 = info.chManufacturerName + " " + info.chModelName;
                UInt32 ip1 = (info.nCurrentIp & 0xff000000) >> 24;
                UInt32 ip2 = (info.nCurrentIp & 0x00ff0000) >> 16;
                UInt32 ip3 = (info.nCurrentIp & 0x0000ff00) >> 8;
                UInt32 ip4 = info.nCurrentIp & 0x000000ff;
                this.주소 = $"{ip1}.{ip2}.{ip3}.{ip4}";
                this.상태 = this.Init();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "초기화", $"초기화 오류: {ex.Message}", true);
                this.상태 = false;
            }

            Debug.WriteLine($"{this.명칭}, {this.코드}, {this.주소}, {this.상태}");
            return this.상태;
        }

        //private Boolean RequireSet(CIntValue val, Int32 current) => val.CurValue != current && val.Min >= current && val.Max <= current;
        public override Boolean Init()
        {
            Int32 nRet = this.Camera.CreateHandle(ref Device);
            if (!그랩제어.Validate($"[{this.구분}] 카메라 초기화에 실패하였습니다.", nRet, true)) return false;

            nRet = this.Camera.OpenDevice();
            if (!그랩제어.Validate($"[{this.구분}] 카메라 연결 실패!", nRet, true)) return false;

            그랩제어.Validate("", this.Camera.SetBoolValue("BlackLevelEnable", true), false);

            this.Camera.SetImageNodeNum(ImageCount);
            this.옵션적용();

            Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);

            그랩제어.Validate("RegisterImageCallBackEx", this.Camera.RegisterImageCallBackEx(this.ImageCallBackDelegate, IntPtr.Zero), false);
            return true;
        }

        private void 옵션적용()
        {
            this.노출적용();
            this.대비적용();
            this.밝기적용();
        }

        public void 밝기적용() // Black Level : 0 ~ 4095
        {
            if (this.Camera == null) return;
            Int32 nRet = this.Camera.SetIntValue("BlackLevel", this.밝기); //this.Camera.SetBrightness(this.밝기);
            그랩제어.Validate($"[{this.구분}] 밝기 설정에 실패하였습니다.", nRet, true);
        }

        public void 노출적용() //ExposureTime
        {
            if (this.Camera == null) return;
            Int32 nRet = this.Camera.SetFloatValue("ExposureTime", this.노출);
            그랩제어.Validate($"[{this.구분}] 노출 설정에 실패하였습니다.", nRet, true);
        }
        public void 대비적용() // Gain
        {
            if (this.Camera == null) return;
            Int32 nRet = this.Camera.SetFloatValue("Gain", this.대비);
            그랩제어.Validate($"[{this.구분}] 대비 설정에 실패하였습니다.", nRet, true);
        }

        public override Boolean Start()
        {
            return 그랩제어.Validate($"{this.구분} 그래버 시작 오류!", Camera.StartGrabbing(), true);
        }

        public override Boolean Ready()
        {
            this.Camera.ClearImageBuffer();
            return Start();
        }

        public override Boolean Close()
        {
            if (this.Camera == null || !this.상태) return true;
            //this.Stop();
            //this.Camera.ClearImageBuffer();
            return 그랩제어.Validate($"{this.구분} 종료오류!", Camera.CloseDevice(), false);
        }

        public override Boolean Stop()
        {
            Camera.ClearImageBuffer();
            return 그랩제어.Validate($"{this.구분} 정지오류!", Camera.StopGrabbing(), false);
        }

        #region 이미지 그랩
        public Boolean TrigForce() => 그랩제어.Validate($"{this.구분} TriggerSoftware", this.Camera.SetCommandValue("TriggerSoftware"), true);

        private void ImageCallBack(IntPtr data, ref MV_FRAME_OUT_INFO_EX frameInfo, IntPtr user)
        {
            try
            {
                Mat image = new Mat(frameInfo.nHeight, frameInfo.nWidth, MatType.CV_8U, data);
                Global.그랩제어.그랩완료(this.구분, image);
                this.Stop();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Camera Call Back Error", $"[{this.구분}] {ex.Message}", false);
                return;
            }
        }
        #endregion
    }

    public class EuresysLink : 카메라장치
    {
        [JsonIgnore, Description("채널번호")]
        public UInt32 Channel;
        [JsonIgnore, Description("카메라 설정 파일")]
        public string CamFile { get; set; } = "LineCameraConfig.cam";
        [JsonIgnore, Description("그래버 보드 Index")]
        public UInt32 DriverIndex { get; set; } = 0;
        [JsonIgnore, Description("Connector")]
        public Connector Connector { get; set; } = Connector.M;
        [JsonIgnore, Description("Acquisition Mode")]
        public AcquisitionMode AcquisitionMode { get; set; } = AcquisitionMode.PAGE;
        [JsonIgnore, Description("Trig Mode")]
        public TrigMode TrigMode { get; set; } = TrigMode.HARD;
        [JsonIgnore, Description("SeqLength_pg")]
        public Int32 SeqLength_pg { get; set; } = 1;
        [JsonIgnore, Description("Page Length")]
        public Int32 PageLength_Ln { get; set; } = 37000;
        [JsonIgnore]
        private MC.CALLBACK CamCallBack;

        private UInt32 currentSurface;

        public EuresysLink(카메라구분 구분)
        {
            this.구분 = 구분;
            this.상태 = Init();
        }

        public override Boolean Init()
        {
            //카메라세팅값 적용
            MC.Create("CHANNEL", out this.Channel);
            MC.SetParam(this.Channel, "DriverIndex", this.DriverIndex);
            MC.SetParam(this.Channel, "Connector", this.Connector.ToString());
            MC.SetParam(this.Channel, "CamFile", Path.Combine(Global.환경설정.기본경로, this.CamFile));
            //MC.SetParam(this.Channel, "AcquisitionMode", this.AcquisitionMode.ToString());
            MC.SetParam(this.Channel, "TrigMode", this.TrigMode.ToString());
            //MC.SetParam(this.Channel, "NextTrigMode", this.NextTrigMode.ToString());
            //MC.SetParam(this.Channel, "EndTrigMode", this.EndTrigMode.ToString());
            //MC.SetParam(this.Channel, "BreakEffect", this.BreakEffect.ToString());
            MC.SetParam(this.Channel, "PageLength_Ln", this.PageLength_Ln);
            MC.SetParam(this.Channel, "SeqLength_pg", this.SeqLength_pg);
            //MC.SetParam(this.Channel, "Gain", 3);

            //콜백등록
            this.CamCallBack = new MC.CALLBACK(MultiCamCallback);
            MC.RegisterCallback(this.Channel, this.CamCallBack, this.Channel);
            // Enable the signals corresponding to the callback functions
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
            MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");
            MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);
            Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);
            return true;
        }

        public override Boolean Close()
        {
            this.Free();
            return true;
        }

        public override Boolean Start()
        {
            this.Ready();
            //Debug.WriteLine("Ready");
            return true;
        }

        public override Boolean Stop()
        {
            if (this.CurrentState() != ChannelState.READY)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);
            //Debug.WriteLine("Set Ready!");
            return true;
        }

        [Description("채널 활성화 준비")]
        public override Boolean Ready()
        {
            if (this.CurrentState() != ChannelState.ACTIVE)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.ACTIVE);
            //Debug.WriteLine("Set Active!");
            return true;
        }

        [Description("채널 Release")]
        public void Free()
        {
            MC.SetParam(this.Channel, "ChannelState", ChannelState.FREE);
        }

        [Description("채널 상태")]
        public string CurrentState()
        {
            String State;
            MC.GetParam(this.Channel, "ChannelState", out State);
            return State;
        }

        [Description("MultiCam CallBack Event")]
        private void MultiCamCallback(ref MC.SIGNALINFO signalInfo)
        {
            switch (signalInfo.Signal)
            {
                case MC.SIG_SURFACE_PROCESSING:
                    ProcessingCallback(signalInfo);
                    break;

                case MC.SIG_ACQUISITION_FAILURE:
                    AcqFailureCallback(signalInfo);
                    break;

                default:
                    Debug.WriteLine(signalInfo.Signal, "SIGNALINFO");
                    throw new Euresys.MultiCamException("Unknown signal");
            }
        }

        [Description("Acquisition Process")]
        private void ProcessingCallback(MC.SIGNALINFO signalInfo)
        {
            //Debug.WriteLine("ProcessingCallback");
            currentSurface = signalInfo.SignalInfo;
            try
            {
                UInt32 currentChannel = (UInt32)signalInfo.Context;
                Int32 ImageSizeX, ImageSizeY, BufferPitch;
                IntPtr BufferAddress;

                MC.GetParam(currentChannel, "ImageSizeX", out ImageSizeX);
                MC.GetParam(currentChannel, "ImageSizeY", out ImageSizeY);
                MC.GetParam(currentChannel, "BufferPitch", out BufferPitch);
                MC.GetParam(currentSurface, "SurfaceAddr", out BufferAddress);

                Mat image = new Mat(ImageSizeY, ImageSizeX, MatType.CV_8U, BufferAddress);

                Global.그랩제어.그랩완료(this.구분, image);
            }
            catch (Euresys.MultiCamException ex)
            {
                MvUtils.Utils.MessageBox("영상획득", ex.ToString(), 2000);
            }
        }

        [Description("Acquisition Failed")]
        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            MvUtils.Utils.MessageBox("영상획득", "유레시스영상획득 실패", 2000);
        }
    }
}