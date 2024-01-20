using DevExpress.Office.Crypto;
using DevExpress.Utils;
using DevExpress.Utils.Extensions;
using GlobalVariableModuleCs;
using GraphicsSetModuleCs;
using ImageSourceModuleCs;
using IMVSGroupCs;
using Newtonsoft.Json.Linq;
using OpenCvSharp;
using ShellModuleCs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using VM.Core;
using VM.PlatformSDKCS;
using VMBaseControls.Base.ImageView;
using static SamhwaInspectionNeo.UI.Control.MasterSetting;

namespace SamhwaInspectionNeo.Schemas
{
    public enum Flow구분
    {
        Flow1,
        Flow2,
        Flow3,
        Flow4,
        공트레이검사,
        상부표면검사,
        하부표면검사,
    }

    public class VM제어 : List<비전마스터플로우>
    {
        private static String 로그영역 = "검사도구";
        public delegate void 현재결과상태갱신(결과구분 구분);
        public event 현재결과상태갱신 결과상태갱신알림;
        private String 도구파일 { get => Path.Combine(Global.환경설정.도구경로, $"{MvUtils.Utils.GetDescription(Global.환경설정.선택모델)}.sol"); }
        private String 기본도구파일 { get => Path.Combine(Global.환경설정.도구경로, $"Default.sol"); }
        private String 스크립트파일 { get => Path.Combine(Global.환경설정.스크립트경로, $"Slot1계산.cs"); }
        public Dictionary<카메라구분, bool> grabFinishDic = new Dictionary<카메라구분, bool>();
        public VmGlobals 글로벌변수제어 = new VmGlobals();

        public Boolean Init() => Load();
        public void Save() => VmSolution.Save();
        public Boolean Load()
        {
            try
            {
                base.Clear();
                //VM Solution 불러오기
                if (File.Exists(도구파일))
                {
                    VmSolution.Load(도구파일, null);
                    글로벌변수제어.Init();
                    Global.정보로그(로그영역, 로그영역, "VmSolution파일 로드 완료.", false);
                }
                else
                {
                    VmSolution.Load(기본도구파일, null);
                    VmSolution.SaveAs(도구파일, null);
                    VmSolution.Instance.CloseSolution();
                    VmSolution.Load(도구파일, null);
                    Global.오류로그(로그영역, 로그영역, "VmSolution파일이 없어 기본 Solution파일 생성 후 로드 완료.", true);
                }

                //모듈 콜백 Disable
                VmSolution.Instance.DisableModulesCallback();
                //데이터 새롭게 추가
                foreach (Flow구분 구분 in typeof(Flow구분).GetValues()) base.Add(new 비전마스터플로우(구분));
                return true;
            }
            catch (Exception e)
            {
                Global.오류로그(로그영역, "솔루션 로드", "솔루션을 로드하는 중 오류가 발생하였습니다.", true);
                Debug.WriteLine(e.Message, "솔루션 로드");
                return false;
            }
        }

        public 비전마스터플로우 GetItem(Flow구분 구분)
        {
            foreach (비전마스터플로우 플로우 in this)
                if (플로우.구분 == 구분) return 플로우;
            return null;
        }

        public 비전마스터플로우 GetItem(카메라구분 구분)
        {
            foreach (비전마스터플로우 플로우 in this)
                if ((int)플로우.구분 == (int)구분) return 플로우;
            return null;
        }

        public void 결과갱신요청(결과구분 구분)
        {
            this.결과상태갱신알림?.Invoke(구분);
        }

        public void Close()
        {
            VmSolution.Instance.CloseSolution();
        }
    }

    public class 비전마스터플로우
    {
        public Flow구분 구분;
        public Boolean 결과;
        public String 로그영역 { get => $"비전도구({구분})"; }

        public VmProcedure Procedure;
        public ImageSourceModuleTool imageSourceModuleTool;
        public GraphicsSetModuleTool graphicsSetModuleTool;
        public ShellModuleTool shellModuleTool;
        public IMVSGroupTool slot1GroupTool;
        public ShellModuleTool slot1ShellModuleTool;
        public IMVSGroupTool slot2GroupTool;
        public ShellModuleTool slot2ShellModuleTool;
        public GlobalVariableModuleTool GlobalVariableModuleTool;
        public List<GraphicsSetModuleTool> graphicsSetModuleToolList;
        public List<ShellModuleTool> shellModuleToolList;

        public 비전마스터플로우(Flow구분 구분)
        {
            this.구분 = 구분;
            this.결과 = false;
            this.Init();
            if (this.graphicsSetModuleTool != null)
            {
                this.graphicsSetModuleTool.EnableResultCallback();
                this.shellModuleTool.EnableResultCallback();
                this.shellModuleTool.ModuleResultCallBackArrived += ShellModuleTool_ModuleResultCallBackArrived;
            }
        }
        private void ShellModuleTool_ModuleResultCallBackArrived(object sender, EventArgs e) { }

        public void Init()
        {
            this.Procedure = VmSolution.Instance[this.구분.ToString()] as VmProcedure;
            if (Procedure != null)
            {
                this.GlobalVariableModuleTool = VmSolution.Instance["Global Variable1"] as GlobalVariableModuleTool;
                if (this.구분 == Flow구분.상부표면검사 || this.구분 == Flow구분.하부표면검사)
                {
                    this.graphicsSetModuleToolList = new List<GraphicsSetModuleTool>();
                    this.shellModuleToolList = new List<ShellModuleTool>();

                    foreach (var item in this.Procedure.Modules)
                    {
                        if (item.GetType() == typeof(ImageSourceModuleTool))
                        {
                            this.imageSourceModuleTool = this.Procedure["InputImage"] as ImageSourceModuleTool;
                            if (this.imageSourceModuleTool != null)
                                this.imageSourceModuleTool.ModuParams.ImageSourceType = ImageSourceParam.ImageSourceTypeEnum.SDK;
                        }
                        else if (item.GetType() == typeof(GraphicsSetModuleTool)) this.graphicsSetModuleToolList.Add(item as GraphicsSetModuleTool);
                        else if (item.GetType() == typeof(ShellModuleTool)) this.shellModuleToolList.Add(item as ShellModuleTool);
                    }
                }
                else
                {
                    //this.Procedure.Inputs[0].Value =
                    this.imageSourceModuleTool = this.Procedure["InputImage"] as ImageSourceModuleTool;
                    this.graphicsSetModuleTool = this.Procedure["OutputImage"] as GraphicsSetModuleTool;
                    this.shellModuleTool = this.Procedure["Resulte"] as ShellModuleTool;

                    this.slot1GroupTool = this.Procedure["Slot1"] as IMVSGroupTool;
                    if (this.slot1GroupTool != null)
                        this.slot1ShellModuleTool = this.slot1GroupTool["거리계산"] as ShellModuleTool;

                    this.slot2GroupTool = this.Procedure["Slot2"] as IMVSGroupTool;
                    if (this.slot2GroupTool != null)
                        this.slot2ShellModuleTool = this.slot2GroupTool["거리계산"] as ShellModuleTool;

                    if (this.imageSourceModuleTool != null)
                        this.imageSourceModuleTool.ModuParams.ImageSourceType = ImageSourceParam.ImageSourceTypeEnum.SDK;
                }
            }
        }

        private void SetResult(Flow구분 구분) //결과체크 추가해줘야됨.
        {
            if (구분 == Flow구분.상부표면검사 || 구분 == Flow구분.하부표면검사) return;

            ShellModuleTool Slot1shell = Global.VM제어.GetItem(구분).slot1ShellModuleTool;
            ShellModuleTool Slot2shell = Global.VM제어.GetItem(구분).slot2ShellModuleTool;
            슬롯부값적용(Slot1shell);
            슬롯부값적용(Slot2shell);
        }

        private void 슬롯부값적용(ShellModuleTool tool)
        {
            for (int i = 7; i < tool.Outputs.Count; i++)
            {
                List<VmIO> t = tool.Outputs[i].GetAllIO();
                String name = t[0].UniqueName.Split('%')[1];
                int lop = 1;
                if (t[0].Value != null)
                {
                    try
                    {
                        foreach (ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX vals in t[0].Value)
                        {
                            if (lop == 1) name = "하부";
                            else if (lop == 2) name = "중앙부";
                            else if (lop == 3) name = "상부";

                            lop++;
                            //Boolean ok = false;
                            Single val = Single.NaN;
                            if (!String.IsNullOrEmpty(vals.strValue)) val = Convert.ToSingle(vals.strValue);
                            //if (vals.Length > 1) ok = MvUtils.Utils.IntValue(vals[1]) == 1;
                            Global.검사자료.항목검사(this.구분, name, val); //Flow1, 지그위치, 값, 결과
                            //valueList.Add(vals.strValue);
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.WriteLine(e.Message, name);
                    }
                }
            }
        }

        //public void 검사결과(ShellModuleTool tool)
        //{
        //    for (int i = 7; i < tool.Outputs.Count; i++)
        //    {
        //        List<VmIO> t = tool.Outputs[i].GetAllIO();
        //        String name = t[0].UniqueName.Split('%')[1];
        //        if (t[0].Value != null && name.Contains("strSlot"))
        //        {
        //            Single slotBottom = Convert.ToSingle(((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue);
        //            Single slotMiddle = Convert.ToSingle(((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[1].strValue);
        //            Single slotTop = Convert.ToSingle(((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[2].strValue);

        //            try
        //            {
        //                검사설정자료 자료 = Global.모델자료.GetItem(Global.환경설정.선택모델)?.검사설정;

        //                검사정보 상부치수 = 자료.Where(x => x.지그 == 지그위치.Front).Where(x => x.검사항목.ToString().Contains(name.Contains("strSlot1") ? "Slot1상부" : "Slot2상부")).FirstOrDefault();
        //                검사정보 중앙부치수 = 자료.Where(x => x.지그 == 지그위치.Front).Where(x => x.검사항목.ToString().Contains(name.Contains("strSlot1") ? "Slot1중앙부" : "Slot2중앙부")).FirstOrDefault();
        //                검사정보 하부치수 = 자료.Where(x => x.지그 == 지그위치.Front).Where(x => x.검사항목.ToString().Contains(name.Contains("strSlot1") ? "Slot1하부" : "Slot2하부")).FirstOrDefault();
        //                결과값적용(상부치수, slotTop);
        //                결과값적용(중앙부치수, slotMiddle);
        //                결과값적용(하부치수, slotBottom);

        //                Global.검사자료.카메라검사(this.구분, 상부치수.검사항목.ToString(), slotTop, true);
        //                Global.검사자료.카메라검사(this.구분, 중앙부치수.검사항목.ToString(), slotMiddle, true);
        //                Global.검사자료.카메라검사(this.구분, 하부치수.검사항목.ToString(), slotBottom, true);
        //            }
        //            catch (Exception e)
        //            {
        //                Debug.WriteLine(e.Message, name);
        //            }
        //        }
        //    }
        //}

        public Boolean 결과값적용(검사정보 검사, Single value)
        {
            if (검사 == null) return false;
            if (Single.IsNaN(value))
            {
                검사.측정결과 = 결과구분.ER;
                return false;
            }
            검사.측정값 = 0;
            검사.결과값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            Boolean ok = 검사.결과값 >= 검사.최소값 && 검사.결과값 <= 검사.최대값;
            검사.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            return true;
        }

        public Boolean Run(Mat mat, ImageBaseData imageBaseData, int 순서)
        {
            this.결과 = false;
            //this.결과업데이트완료 = false;
            try
            {
                if (this.구분 == Flow구분.상부표면검사 || this.구분 == Flow구분.하부표면검사)
                {
                    if (this.imageSourceModuleTool == null)
                    {
                        Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                        return false;
                    }

                    imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                    if (imageBaseData != null)
                        this.imageSourceModuleTool.SetImageData(imageBaseData);

                    this.Procedure.Run();
                    //this.SetResult(this.구분);
                    return true;
                }
                else
                {
                    if (this.imageSourceModuleTool == null)
                    {
                        Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                        return false;
                    }

                    if (this.구분 == Flow구분.Flow1)
                        지그위치체크();

                    imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                    if (imageBaseData != null)
                        this.imageSourceModuleTool.SetImageData(imageBaseData);
                    this.Procedure.Run();
                    this.SetResult(this.구분);
                    //검사결과 검사 = Global.검사자료.검사결과계산((int)this.구분);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사오류: {ex.Message}", false);
                return false;
            }
        }
        private void 지그위치체크()
        {
            if (Global.신호제어.Front지그)
            {
                this.GlobalVariableModuleTool.SetGlobalVar("Front지그", "1");
                this.GlobalVariableModuleTool.SetGlobalVar("Rear지그", "0");
            }
            else if (Global.신호제어.Rear지그)
            {
                this.GlobalVariableModuleTool.SetGlobalVar("Front지그", "0");
                this.GlobalVariableModuleTool.SetGlobalVar("Rear지그", "1");
            }
        }

        private ImageBaseData MatToImageBaseData(Mat mat)
        {
            if (mat.Channels() != 1) return null;
            ImageBaseData imageBaseData;
            uint dataLen = (uint)(mat.Width * mat.Height * mat.Channels());
            byte[] buffer = new byte[dataLen];
            Marshal.Copy(mat.Ptr(0), buffer, 0, buffer.Length);
            imageBaseData = new ImageBaseData(buffer, dataLen, mat.Width, mat.Height, (int)VMPixelFormat.VM_PIXEL_MONO_08);
            return imageBaseData;
        }
    }
}
