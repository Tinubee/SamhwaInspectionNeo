﻿using DevExpress.Utils;
using GlobalVariableModuleCs;
using GraphicsSetModuleCs;
using ImageSourceModuleCs;
using IMVSGroupCs;
using OpenCvSharp;
using ShellModuleCs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using VM.Core;
using VM.PlatformSDKCS;
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
        상부표면검사1,
        상부표면검사2,
        상부표면검사3,
        상부표면검사4,
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
        public GlobalVariableModuleTool GlobalVariableModuleTool;
        public List<ImageSourceModuleTool> imageSourcesModuleToolList;
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
                if (/*this.구분 == Flow구분.상부표면검사 ||*/ this.구분 == Flow구분.하부표면검사)
                {
                    this.graphicsSetModuleToolList = new List<GraphicsSetModuleTool>();
                    this.shellModuleToolList = new List<ShellModuleTool>();
                    this.imageSourcesModuleToolList = new List<ImageSourceModuleTool>();

                    foreach (var item in this.Procedure.Modules)
                    {
                        if (item.GetType() == typeof(ImageSourceModuleTool))
                        {
                            this.imageSourcesModuleToolList.Add(item as ImageSourceModuleTool);
                            //if (this.imageSourceModuleTool != null)
                            //    this.imageSourceModuleTool.ModuParams.ImageSourceType = ImageSourceParam.ImageSourceTypeEnum.SDK;
                        }
                        else if (item.GetType() == typeof(GraphicsSetModuleTool)) this.graphicsSetModuleToolList.Add(item as GraphicsSetModuleTool);
                        else if (item.GetType() == typeof(ShellModuleTool)) this.shellModuleToolList.Add(item as ShellModuleTool);
                    }
                }
                else
                {
                    this.imageSourceModuleTool = this.Procedure["InputImage"] as ImageSourceModuleTool;
                    this.graphicsSetModuleTool = this.Procedure["OutputImage"] as GraphicsSetModuleTool;
                    this.shellModuleTool = this.Procedure["Resulte"] as ShellModuleTool;

                    if (this.imageSourceModuleTool != null)
                        this.imageSourceModuleTool.ModuParams.ImageSourceType = ImageSourceParam.ImageSourceTypeEnum.SDK;
                }
            }
        }

        private void SetResult(Flow구분 구분, 지그위치 지그) //1이면 Front, 0이면 Rear 
        {
            if (구분 == Flow구분.상부표면검사1 || 구분 == Flow구분.하부표면검사)
            {
                ShellModuleTool shell = Global.VM제어.GetItem(구분).shellModuleTool;
                for (int i = 6; i < shell.Outputs.Count; i++)
                {
                    List<VmIO> t = shell.Outputs[i].GetAllIO();
                    String name = t[0].UniqueName.Split('%')[1];
                    if (t[0].Value != null)
                    {
                        String str = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue;
                        try
                        {
                            String[] vals = str.Split(';');
                            Boolean ok = false;
                            Single val = Single.NaN;
                            if (!String.IsNullOrEmpty(vals[0])) val = Convert.ToSingle(vals[0]);
                            if (vals.Length > 1) ok = MvUtils.Utils.IntValue(vals[1]) == 1;
                            Global.검사자료.항목검사(this.구분, 지그, name, val);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message, name);
                        }
                    }
                }
            }
            else
            {
                ShellModuleTool shell = Global.VM제어.GetItem(구분).shellModuleTool;
                for (int i = 8; i < shell.Outputs.Count; i++)
                {
                    List<VmIO> t = shell.Outputs[i].GetAllIO();
                    String name = t[0].UniqueName.Split('%')[1];
                    if (t[0].Value != null)
                    {
                        String str = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue;
                        try
                        {
                            String[] vals = str.Split(';');
                            Boolean ok = false;
                            Single val = Single.NaN;
                            if (!String.IsNullOrEmpty(vals[0])) val = Convert.ToSingle(vals[0]);
                            if (vals.Length > 1) ok = MvUtils.Utils.IntValue(vals[1]) == 1;
                            Global.검사자료.항목검사(this.구분, 지그, name, val);
                        }
                        catch (Exception e)
                        {
                            Debug.WriteLine(e.Message, name);
                        }
                    }
                }
            }
        }

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
                if (this.구분 == Flow구분.상부표면검사1 || this.구분 == Flow구분.상부표면검사2 || this.구분 == Flow구분.상부표면검사3 || this.구분 == Flow구분.상부표면검사4)
                {
                    if (this.imageSourceModuleTool == null)
                    {
                        Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                        return false;
                    }

                    Int32 Front = Convert.ToInt32(Global.VM제어.글로벌변수제어.GetValue("Front지그"));
                    //Flow구분 결과값적용플로우 = (Flow구분)순서;

                    imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                    if (imageBaseData != null)
                        this.imageSourceModuleTool.SetImageData(imageBaseData);

                    this.Procedure.Run();
                    this.SetResult(this.구분, Front == 1 ? 지그위치.Front : 지그위치.Rear); //1이면 Front지그, 0이면 Rear지그
                    검사결과 검사 = Global.검사자료.검사결과계산((int)this.구분 - 5);
                    return true;
                }
                else if (this.구분 == Flow구분.공트레이검사)
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
                    return true;
                }
                else
                {
                    if (this.imageSourceModuleTool == null)
                    {
                        Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                        return false;
                    }

                    Int32 Front = Convert.ToInt32(Global.VM제어.글로벌변수제어.GetValue("Front지그"));

                    imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                    if (imageBaseData != null)
                        this.imageSourceModuleTool.SetImageData(imageBaseData);
                    this.Procedure.Run();
                    this.SetResult(this.구분, Front == 1 ? 지그위치.Front : 지그위치.Rear); //1이면 Front지그, 0이면 Rear지그

                    if (Global.환경설정.동작구분 == 동작구분.LocalTest)
                        Global.검사자료.검사결과계산((int)this.구분);

                    return true;
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사오류: {ex.Message}", false);
                return false;
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
