﻿using Euresys.MultiCam;
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
using static SamhwaInspectionNeo.Schemas.EuresysLink;

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
        //[Description("하부표면검사")]
        //Cam04 = 4,
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
        public delegate void 이미지그랩완료보고대리자(AcquisitionData Data);

        public delegate void 그랩완료대리자(카메라구분 구분, Mat 이미지);
        public delegate void 그랩완료대리자2(카메라구분 구분, List<Mat> 이미지);

        public event 그랩완료대리자 그랩완료보고;
        public event 그랩완료대리자2 그랩완료보고2;

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

                this.카메라1 = new EuresysLink(카메라구분.Cam01) { 코드 = "" }; //치수검사
                this.카메라2 = new HikeGigE() { 구분 = 카메라구분.Cam02, 코드 = "K38332371" }; //공트레이검사
                this.카메라3 = new HikeGigE() { 구분 = 카메라구분.Cam03, 코드 = "DA1996738" }; //상부표면검사
                //this.카메라4 = new HikeGigE() { 구분 = 카메라구분.Cam04, 코드 = "DA1996737" }; //하부표면검사

                this.Add(카메라구분.Cam01, this.카메라1);
                this.Add(카메라구분.Cam02, this.카메라2);
                this.Add(카메라구분.Cam03, this.카메라3);
                //this.Add(카메라구분.Cam04, this.카메라4);

                this.카메라1.AcquisitionFinishedEvent += 카메라1_AcquisitionFinishedEvent;

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
                Global.오류로그(로그영역, "카메라 연결 실패", $"{ex.Message}", true);
                return false;
            }

        }

        private void 카메라1_AcquisitionFinishedEvent(EuresysLink.AcquisitionData Data)
        {
            try
            {
                if (Data.BmpImage == null) return;

                if (Data.Camera == 카메라구분.Cam01)
                {
                    if (Data.PageIndex == 1)
                    {
                        this.카메라1.Page1Image = Data.MatImage;
                        Debug.WriteLine(this.카메라1.CurrentState(), "첫번째");
                        this.카메라1.isGrabCompleted_Page1 = true;
                    }
                    if (Data.PageIndex == 2)
                    {
                        this.카메라1.Page2Image = Data.MatImage;
                        this.카메라1.isGrabCompleted_Page2 = true;
                        Debug.WriteLine(this.카메라1.CurrentState(), "두번쨰");
                    }

                    if (this.카메라1.isGrabCompleted_Page1 & this.카메라1.isGrabCompleted_Page2)
                    {
                        this.카메라1.isGrabCompleted_Page1 = false;
                        this.카메라1.isGrabCompleted_Page2 = false;
                        //조명 끔
                        Global.조명제어.TurnOff(카메라구분.Cam01);
                        // 이미지 연결
                        Int32 SplitPointStart = Convert.ToInt32(Global.VM제어.글로벌변수제어.GetValue("SplitPointStart"));
                        Int32 SplitPointX = Convert.ToInt32(Global.VM제어.글로벌변수제어.GetValue("SplitPointX"));
                        Int32 SplitPointY = Convert.ToInt32(Global.VM제어.글로벌변수제어.GetValue("SplitPointY"));

                        Cv2.VConcat(this.카메라1.Page1Image, this.카메라1.Page2Image, this.카메라1.mergedImage);
                        Debug.WriteLine($"Start : {SplitPointStart} , PointX : {SplitPointX}, PointY : {SplitPointY}");

                        this.카메라1.roi[0] = new Rect(SplitPointX, SplitPointStart, this.카메라1.width, 18000);
                        this.카메라1.roi[1] = new Rect(SplitPointX, SplitPointY * 1, this.카메라1.width, 18000);
                        this.카메라1.roi[2] = new Rect(SplitPointX, SplitPointY * 2, this.카메라1.width, 18000);
                        this.카메라1.roi[3] = new Rect(SplitPointX, SplitPointY * 3, this.카메라1.width, 18000);

                        for (int lop = 0; lop < this.카메라1.roi.Length; lop++)
                        {
                            this.카메라1.splitImage[lop] = new Mat(this.카메라1.mergedImage, this.카메라1.roi[lop]);
                            Int32 검사코드 = Global.신호제어.마스터모드여부 ? Convert.ToInt32((Flow구분)lop + 100) : Convert.ToInt32((Flow구분)lop);
                            검사결과 검사 = Global.검사자료.검사시작(검사코드);
                            Boolean 결과 = Global.VM제어.GetItem((Flow구분)lop).Run(this.카메라1.splitImage[lop], null);
                            Debug.WriteLine($"치수검사 결과 {lop} : {결과}");
                            //이미지 저장함수 추가하면됨.
                            this.ImageSave(this.카메라1.splitImage[lop], 카메라구분.Cam01, lop, 결과);
                        }
                        this.카메라1.isCompleted_Camera1 = true;
                    }
                    if (this.카메라1.isCompleted_Camera1)
                    {
                        this.카메라1.isCompleted_Camera1 = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "LineCamera ImageGrab", $"{ex.Message}", true);
                Debug.WriteLine(ex.Message);
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
            //this.Save();
            foreach (카메라장치 장치 in this.Values)
                장치?.Close();
        }

        public void Ready(카메라구분 카메라) => this.GetItem(카메라)?.Ready();

        public void 그랩완료(카메라구분 카메라, List<Mat> 이미지)
        {
            if (카메라 == 카메라구분.Cam03) //상부표면검사
            {
                //Debug.WriteLine($"{카메라} 이미지획득 {this.카메라4.MatImage.Count}개 완료");
                Global.조명제어.TurnOff(카메라);
                //if (Global.비전마스터구동.Count == 0 || Global.신호제어.마스터모드여부 == 1)
                //    return;
                Task.Run(() =>
                {
                    for (int lop = 0; lop < this.카메라3.MatImage.Count; lop++)
                    {
                        Boolean 결과 = Global.VM제어.GetItem((Flow구분)lop + 5).Run(이미지[lop], null);
                        Debug.WriteLine($"표면검사 결과 {lop} : {결과}");
                        //이미지 저장함수 추가하면됨.
                        this.ImageSave(이미지[lop], 카메라구분.Cam03, lop, 결과);
                        if (lop == this.카메라3.MatImage.Count - 1) this.카메라3.MatImage.Clear();
                    }
                });
            }
            this.그랩완료보고2?.Invoke(카메라, 이미지);
        }

        public void 그랩완료(카메라구분 카메라, Mat 이미지)
        {
            //Int32 검사번호 = Global.신호제어.촬영위치번호(카메라);
            if (카메라 == 카메라구분.Cam02)
            {
                Task.Run(() =>
                {
                    Boolean 결과 = Global.VM제어.GetItem(Flow구분.공트레이검사).Run(이미지, null);
                    this.ImageSave(이미지, 카메라구분.Cam02, 0, 결과);
                    Global.신호제어.SetDevice($"W0015", 결과 ? 1 : 2, out Int32 오류);
                });
            }

            Global.조명제어?.TurnOff(카메라);
            this.그랩완료보고?.Invoke(카메라, 이미지);
        }


        public void ImageSave(Mat 이미지, 카메라구분 카메라, Int32 검사번호, Boolean 결과)
        {
            if (!Global.환경설정.사진저장OK && !Global.환경설정.사진저장NG) return;
            List<String> paths = new List<String> { Global.환경설정.사진저장경로, MvUtils.Utils.FormatDate(DateTime.Now, "{0:yyyy-MM-dd}"), Global.환경설정.선택모델.ToString(), 카메라.ToString() };
            String name = $"{검사번호.ToString("d4")}_{MvUtils.Utils.FormatDate(DateTime.Now, "{0:HHmmss}")}.png";//_{결과.ToString()}
            String path = Common.CreateDirectory(paths);
            Debug.WriteLine($"이미지 저장 경로 : {path}");
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

        public Boolean isCompleted_Camera1;
        public Boolean isGrabCompleted_Page1;
        public Boolean isGrabCompleted_Page2;
        public Bitmap tempBitmap;
        public Mat Page1Image;
        public Mat Page2Image;
        public Mat mergedImage;
        public Rect[] roi = new Rect[4];
        public Rect roiAlign;
        public Mat[] splitImage = new Mat[4];

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
        public uint ImageCount = 4;
        [JsonIgnore]
        public List<Mat> MatImage = new List<Mat>();
        [JsonIgnore, Description("Trig Mode")]
        public MV_CAM_TRIGGER_MODE TrigMode { get; set; } = MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON;
        [JsonIgnore, Description("Trig Source")]
        public MV_CAM_TRIGGER_SOURCE TrigSource { get; set; } = MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0;
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
                this.번호 = (int)this.구분;
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
            //this.Camera.SetEnumValue("TriggerMode", (uint)MV_CAM_TRIGGER_MODE.MV_TRIGGER_MODE_ON);
            this.옵션적용();

            Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);

            그랩제어.Validate("RegisterImageCallBackEx", this.Camera.RegisterImageCallBackEx(this.ImageCallBackDelegate, IntPtr.Zero), false);
            return true;
        }

        private void 옵션적용()
        {
            //this.노출적용();
            //this.대비적용();
            //this.밝기적용();
            this.트리거모드적용();
            this.트리거소스적용();
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

        public void 트리거모드적용()
        {
            if (this.Camera == null) return;
            Int32 nRet = this.Camera.SetEnumValue("TriggerMode", (uint)this.TrigMode);
            그랩제어.Validate($"[{this.구분}] 트리거모드 설정에 실패하였습니다.", nRet, true);
        }

        public void 트리거소스적용()
        {
            if (this.Camera == null) return;
            Int32 nRet = this.Camera.SetEnumValue("TriggerSource", (uint)this.TrigSource);
            그랩제어.Validate($"[{this.구분}] 트리거소스 설정에 실패하였습니다.", nRet, true);
        }

        //public void 소프트웨어트리거모드()
        //{
        //    if (this.Camera == null) return;
        //    Int32 nRet = this.Camera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_SOFTWARE);
        //    그랩제어.Validate($"[{this.구분}] 소프트웨어트리거 모드 설정에 실패하였습니다.", nRet, true);
        //}

        //public void 하드웨어트리거모드() //defalut
        //{
        //    if (this.Camera == null) return;
        //    Int32 nRet = this.Camera.SetEnumValue("TriggerSource", (uint)MV_CAM_TRIGGER_SOURCE.MV_TRIGGER_SOURCE_LINE0);
        //    그랩제어.Validate($"[{this.구분}] 하드웨어트리거 모드 설정에 실패하였습니다.", nRet, true);
        //}

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

                if (this.구분 == 카메라구분.Cam02)
                {
                    Global.그랩제어.그랩완료(this.구분, image);
                    this.Stop();
                }
                else if (this.구분 == 카메라구분.Cam03)
                {
                    this.MatImage.Add(image);
                    Debug.WriteLine($"카메라3 이미지개수 : {this.MatImage.Count}");
                    if (Global.그랩제어.카메라3.MatImage.Count == this.ImageCount)
                    {
                        this.Stop();
                        Global.그랩제어.그랩완료(this.구분, this.MatImage);
                    }
                }
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
        [Description("이미지 그랩 이벤트")]
        public delegate void AcquisitionFinished(AcquisitionData Data);
        public event AcquisitionFinished AcquisitionFinishedEvent;
        private Int32 PageIndex = 1;
        public ProductIndex ProductIndex = ProductIndex.PRODUCT_INDEX1;

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
        public Int32 SeqLength_pg { get; set; } = 2;
        [JsonIgnore, Description("Page Length")]
        public Int32 PageLength_Ln { get; set; } = 40000;
        [JsonIgnore]
        private MC.CALLBACK CamCallBack;

        public Int32 height;
        public Int32 width;

        private UInt32 currentSurface;

        public EuresysLink(카메라구분 구분)
        {
            this.구분 = 구분;
            this.상태 = Init();
        }

        public override Boolean Init()
        {
            //카메라세팅값 적용
            try
            {
                MC.Create("CHANNEL", out this.Channel);
                MC.SetParam(this.Channel, "DriverIndex", this.DriverIndex);
                MC.SetParam(this.Channel, "Connector", this.Connector.ToString());
                MC.SetParam(this.Channel, "CamFile", Path.Combine(Global.환경설정.기본경로, this.CamFile));
                MC.SetParam(this.Channel, "AcquisitionMode", this.AcquisitionMode.ToString());
                MC.SetParam(this.Channel, "TrigMode", this.TrigMode.ToString());
                //MC.SetParam(this.Channel, "NextTrigMode", this.NextTrigMode.ToString());
                //MC.SetParam(this.Channel, "EndTrigMode", this.EndTrigMode.ToString());
                //MC.SetParam(this.Channel, "BreakEffect", this.BreakEffect.ToString());
                MC.SetParam(this.Channel, "PageLength_Ln", this.PageLength_Ln);
                MC.SetParam(this.Channel, "SeqLength_pg", this.SeqLength_pg);
                //MC.SetParam(this.Channel, "Gain", 3);
                MC.GetParam(this.Channel, "ImageSizeY", out this.height);
                MC.GetParam(this.Channel, "ImageSizeX", out this.width);
                //콜백등록
                this.CamCallBack = new MC.CALLBACK(MultiCamCallback);
                MC.RegisterCallback(this.Channel, this.CamCallBack, this.Channel);
                // Enable the signals corresponding to the callback functions
                MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_SURFACE_PROCESSING, "ON");
                MC.SetParam(this.Channel, MC.SignalEnable + MC.SIG_ACQUISITION_FAILURE, "ON");
                MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);

                this.Page1Image = new Mat(height, width, MatType.CV_8UC1);
                this.Page2Image = new Mat(height, width, MatType.CV_8UC1);
                this.mergedImage = new Mat(height * 2, width, MatType.CV_8UC1);

                //InitBuffers(this.width, this.height);
                //this.mergedImage = new Mat(height_cam * 2, width_cam, MatType.CV_8UC1);
                Debug.WriteLine($"{this.Channel}, {this.CurrentState()}", "READY currentState");
                Global.정보로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 성공!", false);

                return true;
            }
            catch (Exception e)
            {
                Global.오류로그(로그영역, "카메라 연결", $"[{this.구분}] 카메라 연결 실패! - {e.Message}", false);
                return false;
            }
        }
        internal void InitBuffers(Int32 width, Int32 height)
        {
            //if (width == 0 || height == 0) return;
            //Int32 channels =  1;
            //Int32 imageSize = width * height * channels;
            //if (BufferAddress != IntPtr.Zero && imageSize == BufferSize) return;
            //this.ImageWidth = width; this.ImageHeight = height;
            //if (BufferAddress != IntPtr.Zero)
            //{
            //    Marshal.Release(BufferAddress);
            //    BufferAddress = IntPtr.Zero;
            //    BufferSize = 0;
            //}

            //BufferAddress = Marshal.AllocHGlobal(imageSize);
            //if (BufferAddress == IntPtr.Zero) return;
            //BufferSize = (UInt32)imageSize;
            //Debug.WriteLine(this.구분.ToString(), "InitBuffers");
        }
        public override Boolean Close()
        {
            this.Free();
            return true;
        }

        public override Boolean Start()
        {
            this.Ready();
            return true;
        }

        public override Boolean Stop()
        {
            if (this.CurrentState() != ChannelState.READY)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.READY);
            return true;
        }

        [Description("채널 활성화 준비")]
        public override Boolean Ready()
        {
            Debug.WriteLine("LineScanCamera Active");
            if (this.CurrentState() != ChannelState.ACTIVE)
                MC.SetParam(this.Channel, "ChannelState", ChannelState.ACTIVE);
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
            Debug.WriteLine("ProcessingCallback");
            currentSurface = signalInfo.SignalInfo;
            try
            {
                UInt32 currentChannel = (UInt32)signalInfo.Context;
                Int32 imageSizeX, imageSizeY, bufferPitch;

                MC.GetParam(currentChannel, "ImageSizeX", out imageSizeX);
                MC.GetParam(currentChannel, "ImageSizeY", out imageSizeY);
                MC.GetParam(currentChannel, "BufferPitch", out bufferPitch);
                Debug.WriteLine($"{imageSizeX}", "ImageSizeX");
                Debug.WriteLine($"{imageSizeY}", "ImageSizeY");
                Debug.WriteLine($"{bufferPitch}", "BufferPitch");


                if (this.AcquisitionMode == AcquisitionMode.PAGE)
                    this.ImageGrap(currentChannel, signalInfo.SignalInfo, imageSizeX, imageSizeY, bufferPitch);

                //Mat image = new Mat(ImageSizeY, ImageSizeX, MatType.CV_8U, BufferAddress);
                //Global.그랩제어.그랩완료(this.구분, image);
            }
            catch (Euresys.MultiCamException ex)
            {
                MvUtils.Utils.MessageBox("영상획득", ex.ToString(), 2000);
            }
        }
        private void ImageGrap(UInt32 channel, UInt32 bufferAddress, Int32 width, Int32 height, Int32 bufferPitch)
        {
            Debug.WriteLine("LineCamera ImageGrab");
            AcquisitionData acq = new AcquisitionData(this.구분, PageIndex);
            Mat image = new Mat();
            PageIndex += 1;
            if (PageIndex == 3) PageIndex = 1;

            try
            {
                IntPtr surfaceAddr;
                MC.GetParam(bufferAddress, "SurfaceAddr", out surfaceAddr);
                image = new Mat(height, width, MatType.CV_8U, surfaceAddr);

                acq.SetImage(image);
            }
            catch (Exception ex)
            {
                acq.Dispose();
                acq.Error = ex.Message;
                Debug.WriteLine($"그랩오류: {ex.Message}");
            }
            this.AcquisitionFinishedEvent?.Invoke(acq);
        }

        [Description("이미지 획득 정보")]
        public class AcquisitionData : IDisposable
        {
            public 카메라구분 Camera { get; set; } = 카메라구분.None;
            public Bitmap BmpImage { get; set; } = null;
            public Mat MatImage { get; set; }
            public string Error { get; set; } = String.Empty;
            public ProductIndex ProductIndex;
            public Int32 PageIndex;

            public AcquisitionData(카메라구분 Cam)
            {
                this.Camera = Cam;
            }

            public AcquisitionData(카메라구분 Cam, Mat Image)
            {
                this.Camera = Cam;
                this.MatImage = Image;
            }

            public AcquisitionData(카메라구분 Cam, ProductIndex productIndex)
            {
                this.Camera = Cam;
                this.ProductIndex = productIndex;
            }

            public AcquisitionData(카메라구분 Cam, Int32 pageIndex)
            {
                this.Camera = Cam;
                this.PageIndex = pageIndex;
            }

            public AcquisitionData(카메라구분 Cam, String Error)
            {
                this.Camera = Cam;
                this.Error = Error;
            }

            public void SetImage(Mat image)
            {
                try
                {
                    this.MatImage?.Dispose();
                    this.MatImage = image;
                    this.BmpImage?.Dispose();
                    this.BmpImage = OpenCvSharp.Extensions.BitmapConverter.ToBitmap(this.MatImage);
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e.Message);
                    Global.오류로그(로그영역, "SetImage", e.Message, true);
                }

            }

            public void Dispose()
            {
                this.MatImage?.Dispose();
                this.MatImage = null;
            }
        }

        [Description("Acquisition Failed")]
        private void AcqFailureCallback(MC.SIGNALINFO signalInfo)
        {
            MvUtils.Utils.MessageBox("영상획득", "유레시스영상획득 실패", 2000);
        }
    }
}