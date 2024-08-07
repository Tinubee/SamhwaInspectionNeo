﻿using DevExpress.Utils;
using GlobalVariableModuleCs;
using GraphicsSetModuleCs;
using ImageSourceModuleCs;
using IMVSGroupCs;
using OpenCvSharp;
using ShellModuleCs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using VM.Core;
using VM.PlatformSDKCS;

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
        하부표면검사1,
        하부표면검사2,
        하부표면검사3,
        하부표면검사4,
        모델감지및역투입,
        NG = 99
    }

    public enum 지그위치
    {
        Front,
        Rear,
    }

    public class VM제어 : List<비전마스터플로우>
    {
        private static String 로그영역 = "검사도구";
        public delegate void 현재결과상태갱신(결과구분 구분);
        public event 현재결과상태갱신 결과상태갱신알림;
        private String 도구파일 { get => Path.Combine(Global.환경설정.도구경로, $"{MvUtils.Utils.GetDescription(Global.환경설정.선택모델)}.sol"); }
        private String 기본도구파일 { get => Path.Combine(Global.환경설정.도구경로, $"Default.sol"); }
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
                    Global.정보로그(로그영역, 로그영역, $"[ {Global.환경설정.선택모델} ] VmSolution파일 로드 완료.", false);
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
                VmSolution.OnWorkStatusEvent += VmSolution_OnWorkStatusEvent;
                //데이터 새롭게 추가
                foreach (Flow구분 구분 in typeof(Flow구분).GetValues()) base.Add(new 비전마스터플로우(구분));
                return true;
            }
            catch (Exception e)
            {
                Global.오류로그(로그영역, "솔루션 로드", $"솔루션을 로드하는 중 오류가 발생하였습니다. / {e.Message}", true);
                return false;
            }
        }

        private void VmSolution_OnWorkStatusEvent(ImvsSdkDefine.IMVS_MODULE_WORK_STAUS workStatusInfo)
        {
            //Debug.WriteLine("Solution OnWork Event");
            //float[] value = new float[1];
            //value[0] = (float)0.005;
            //this.GetItem(Flow구분.Flow1).slot1ShellModuleTool.ModuParams.SetInputFloat("RearSlot1상부", value);
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
        public DateTime 검사시간;
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
        public ShellModuleTool indexShellModuleTool;

        public 비전마스터플로우(Flow구분 구분)
        {
          
            this.구분 = 구분;
            this.결과 = false;
            this.검사시간 = DateTime.Now;
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

                this.imageSourceModuleTool = this.Procedure["InputImage"] as ImageSourceModuleTool;
                this.graphicsSetModuleTool = this.Procedure["OutputImage"] as GraphicsSetModuleTool;
                this.shellModuleTool = this.Procedure["Resulte"] as ShellModuleTool;
                this.indexShellModuleTool = this.Procedure["인덱스확인"] as ShellModuleTool;

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

        private void GetRenderImage(Flow구분 구분)
        {
            GraphicsSetModuleTool tool = Global.VM제어.GetItem(구분).graphicsSetModuleTool;

        }

        private String GetResultStr(Flow구분 구분)
        {
            ShellModuleTool shell = Global.VM제어.GetItem(구분).shellModuleTool;
            String str = "";
            List<VmIO> t = shell.Outputs[6].GetAllIO();
            String name = t[0].UniqueName.Split('%')[1];
            if (t[0].Value != null)
            {
                str = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue;
                Common.DebugWriteLine(로그영역, 로그구분.정보, $"{this.구분} - {str}");
            }
            //String.Empty일때는 제품이 없는거.
            //String resStr = str == String.Empty ? "3" : str;
            return str;
        }
        private Boolean GetResult(Flow구분 구분)
        {
            ShellModuleTool shell = Global.VM제어.GetItem(구분).shellModuleTool;
            String str = "";
            List<VmIO> t = shell.Outputs[6].GetAllIO();
            String name = t[0].UniqueName.Split('%')[1];
            if (t[0].Value != null)
            {
                str = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue;
                Common.DebugWriteLine(로그영역, 로그구분.정보, $"{this.구분} - {str}");
            }

            Boolean resBool = str == "0" ? true : false;

            return resBool;
        }

        private void SetResult(Flow구분 구분, 지그위치 지그) //0이면 Front, 1이면 Rear 
        {
            ShellModuleTool shell = Global.VM제어.GetItem(구분).shellModuleTool;

            Int32 startIndex = 6; //= 구분 >= Flow구분.공트레이검사 ? 6 : 8;
            for (int lop = startIndex; lop < shell.Outputs.Count; lop++)
            {
                List<VmIO> t = shell.Outputs[lop].GetAllIO();
                String name = t[0].UniqueName.Split('%')[1];
                if (t[0].Value != null)
                {
                    String str = ((ImvsSdkDefine.IMVS_MODULE_STRING_VALUE_EX[])t[0].Value)[0].strValue;
                    if (str == null) return;
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

        public void Shell입력값적용(InputStringData[] inputStringData, String setName)
        {
            this.indexShellModuleTool.ModuParams.SetInputString(setName, inputStringData);
        }

        public String RunStr(Mat mat, ImageBaseData imageBaseData)
        {
            this.결과 = false;
            //this.결과업데이트완료 = false;
            try
            {
                if (this.imageSourceModuleTool == null)
                {
                    Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                    return String.Empty;
                }
                Boolean Front = Global.신호제어.Front지그; //Global.VM제어.글로벌변수제어.GetValue("Front지그") == "1" ? true : false; ///

                imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                if (imageBaseData != null)
                    this.imageSourceModuleTool.SetImageData(imageBaseData);
                
                this.Procedure.Run();

                if (this.구분 >= Flow구분.하부표면검사1)
                {
                    //String.Empty 면 제품이 없는거, 
                    String 하부표면검사결과 = this.GetResultStr(this.구분);
                    return 하부표면검사결과;
                }

                return String.Empty;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사오류: {ex.Message}", false);
                return String.Empty;
            }
        }

        public Boolean OnlyFlowRun()
        {
            this.Procedure.Run();
            return true;
        }


        public Boolean Run(Mat mat, ImageBaseData imageBaseData, String imagePath)
        {
            this.결과 = false;
            //this.결과업데이트완료 = false;
            try
            {
                if (this.imageSourceModuleTool == null)
                {
                    Global.오류로그(로그영역, "검사오류", $"[{this.구분}] VM 검사 모델이 없습니다.", false);
                    return false;
                }
                //Boolean Front = Global.신호제어.Front지그; //Global.VM제어.글로벌변수제어.GetValue("Front지그") == "1" ? true : false; 

                if(mat == null && imageBaseData == null)
                    this.imageSourceModuleTool.SetImagePath(imagePath);
                else
                {
                    imageBaseData = mat == null ? imageBaseData : MatToImageBaseData(mat);
                    if (imageBaseData != null)
                        this.imageSourceModuleTool.SetImageData(imageBaseData);
                }

                this.Procedure.Run();

                if (this.구분 == Flow구분.공트레이검사)
                {
                    Boolean 트레이검사결과 = this.GetResult(this.구분);
                    return 트레이검사결과;
                }
                else
                {
                    Int32 검사코드 = (Int32)구분 < (int)Flow구분.상부표면검사1 ? (Int32)구분 : (Int32)구분 - 5;
                    this.SetResult(this.구분, Global.환경설정.Front지그 ? 지그위치.Front : 지그위치.Rear);
                    검사결과 검사 = Global.검사자료.검사결과계산(검사코드);
                    
                    결과구분 e검사결과 = (Int32)구분 < (int)Flow구분.상부표면검사1 ? 검사.CTQ결과 : 검사.외관결과;
                    Boolean b검사결과 = e검사결과 == 결과구분.OK ? true : false;

                    return b검사결과;
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
