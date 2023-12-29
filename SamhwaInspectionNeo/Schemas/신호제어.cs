using SamhwaInspectionNeo.Utils;
using OpenCvSharp.Aruco;
using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using static SamhwaInspectionNeo.Schemas.신호제어;
using DevExpress.ClipboardSource.SpreadsheetML;
using ActUtlType64Lib;
using System.Security.Policy;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using DevExpress.Utils.About;
using MvCamCtrl.NET;

namespace SamhwaInspectionNeo.Schemas
{
    public delegate void MyEventHandler();
    // PLC 통신
    [Description("MELSEC Q13UDV")]
    public class 신호제어
    {
        //public event Global.BaseEvent 동작상태알림;
        public ActUtlType64 PLC = null;
        public Boolean 작업여부 = false;
        public static String 로그영역 = "장치통신";


        private enum 주소구분 : Int32
        {
            [Address("W0010")]
            모델변경트리거,
            [Address("W0011")]
            트레이개수,
            [Address("W0020")]
            결과값요청트리거,
            [Address("W0021")]
            제품확인카메라트리거1,
            [Address("W0022")]
            하부표면검사카메라트리거1,
            [Address("W0028")]
            F상부치수검사카메라트리거1,
            [Address("W002E")]
            R상부치수검사카메라트리거1,
            [Address("W003A")]
            R상부표면검사카메라트리거1,
            //변위센서 트리거
            [Address("W0040")]
            상부변위센서확인트리거,
            [Address("W0041")]
            하부변위센서확인트리거,
            //[Address("B1000")]
            //Heartbit_PC,
            [Address("B1010")]
            Heartbit_PLC,
            [Address("B1018")]
            FrontJIG,
            [Address("B1019")]
            RearJIC,
            //[Address("B1020")]
            //수동모드,
            [Address("B1030")]
            자동모드,
            [Address("B1040")]
            운전시작,
            [Address("B1017")]
            마스터모드,
            //[Address("B1050")]
            //운전정지,
            //[Address("B1060")]
            //리셋,
            //[Address("B1070")]
            //알람,
        }

        public enum 입력신호
        {
            수동모드 = 0,
            자동모드 = 1,
            현재모델번호 = 2,
            자동운전시작 = 3,
            카메라상태 = 4,
        }

        public enum 출력신호
        {
            모델변경요청 = 0,
            OK신호 = 1,
            NG신호 = 2,
            프로그램구동펄스 = 3,
        }

        public enum 입력주소
        {
            수동모드 = 0x010,
            자동모드 = 0x011,
            현재모델번호 = 0x020,
            자동운전시작 = 0x021,
            카메라상태 = 0x022,
        }

        public enum 출력주소
        {
            모델변경요청 = 0x100,
            OK신호 = 0x000,
            NG신호 = 0x001,
            프로그램구동펄스 = 0x200,
            버퍼 = 0x201,
        }
        private BackgroundWorker cclink_thred;
        private BackgroundWorker cclink_echo;

        int thred_roop_index = 0;
        private 주소자료 입출자료 = new 주소자료();

        public static bool bit = false;

        public event MyEventHandler CompleteReceive;

        public 신호제어()
        {
        }

        #region Propertys

        //Input Part
        public int 자동모드여부 { get { return 신호읽기(주소구분.자동모드); } }
        public int 운전시작여부 { get { return 신호읽기(주소구분.운전시작); } }

        public int 마스터모드여부 { get { return 신호읽기(주소구분.마스터모드); } }
        //B1018,1819
        public int Front지그 { get { return 신호읽기(주소구분.FrontJIG); } }
        public int Rear지그 { get { return 신호읽기(주소구분.RearJIC); } }
        public int Heartbit_PLC { get { return 신호읽기(주소구분.Heartbit_PLC); } }

        //Output Part
        public short 제품확인카메라트리거1 { get { return 신호읽기(주소구분.제품확인카메라트리거1); } set { 신호쓰기(주소구분.제품확인카메라트리거1, value); } }

        public short 하부표면검사카메라트리거1 { get { return 신호읽기(주소구분.하부표면검사카메라트리거1); } set { 신호쓰기(주소구분.하부표면검사카메라트리거1, value); } }

        public short F상부치수검사카메라트리거1 { get { return 신호읽기(주소구분.F상부치수검사카메라트리거1); } set { 신호쓰기(주소구분.F상부치수검사카메라트리거1, value); } }

        public short R상부치수검사카메라트리거1 { get { return 신호읽기(주소구분.R상부치수검사카메라트리거1); } set { 신호쓰기(주소구분.R상부치수검사카메라트리거1, value); } }

        public short 상부변위센서확인트리거 { get { return 신호읽기(주소구분.상부변위센서확인트리거); } set { 신호쓰기(주소구분.상부변위센서확인트리거, value); } }

        public short 하부변위센서확인트리거 { get { return 신호읽기(주소구분.하부변위센서확인트리거); } set { 신호쓰기(주소구분.하부변위센서확인트리거, value); } }

        public short R상부표면검사카메라트리거1 { get { return 신호읽기(주소구분.R상부표면검사카메라트리거1); } set { 신호쓰기(주소구분.R상부표면검사카메라트리거1, value); } }

        public short 결과값요청트리거 { get { return 신호읽기(주소구분.결과값요청트리거); } set { 신호쓰기(주소구분.결과값요청트리거, value); } }



        #endregion

        private short 신호읽기(주소구분 구분) { return this.입출자료.GetValue(구분); }
        private void 신호쓰기(주소구분 구분, short value) { this.입출자료.SetValue(구분, value); }


        public void Init()
        {
            this.PLC = new ActUtlType64();
            this.PLC.ActLogicalStationNumber = 2;
            int error = 101;
            error = this.PLC.Open();

            if (error != 0)
            {
                Debug.WriteLine("PLC 연결안됨");
                return;
            }

            Debug.WriteLine("PLC 연결됨");
            cclink_thred = new BackgroundWorker();
            cclink_thred.DoWork += cclink_thred_DoWork;
            cclink_thred.RunWorkerAsync(0);

            cclink_echo = new BackgroundWorker();
            cclink_echo.DoWork += cclink_echo_DoWork;
            cclink_echo.RunWorkerAsync(0);
        }

        // 작업을 생성하고 통신 작업 실행
        public void SendValueToPLC(String 주소, Int32 값)
        {
            Task task = CommunicationTask(주소, 값);
        }

        private Task CommunicationTask(String 주소, Int32 값)
        {
            // PLC와 통신하는 코드 작성
            PLC.SetDevice(주소, 값);
            return Task.CompletedTask;
        }

        private void cclink_thred_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                Global.신호제어.SendValueToPLC("W0028", 0);
                thred_roop_index = 0;

                while (thred_roop_index == 0)
                {
                    foreach (주소정보 정보 in 입출자료.Values)
                    {
                        PLC.GetDevice2(정보.주소, out 정보.값);

                        if (정보.주소 == "W0010" & 정보.값 > 0)
                        {
                            if (Global.환경설정.선택모델 != (모델구분)(정보.값 - 1))
                            {
                                Debug.WriteLine($"현재선택모델 번호 : {정보.값 - 1}");
                                //Global.mainForm.ShowWaitForm();
                                Global.환경설정.모델변경요청(정보.값 - 1);
                                Global.VM제어.Init();
                            }
                        }
                        if (정보.주소 == "W0021" & 정보.값 == 1) // 유무검사 트리거신호
                        {
                            SendValueToPLC(정보.주소, 0);
                            Global.그랩제어.카메라2.Ready();
                            Global.그랩제어.카메라2.TrigForce();
                        }

                        if (정보.주소 == "W0022" & 정보.값 == 1) // 표면검사뒷면 트리거신호
                        {
                            //Global.그랩제어.카메라3.MatImage.Clear();
                            //Global.조명제어.TurnOn(조명구분.후면검사조명);
                            SendValueToPLC(정보.주소, 0);
                            Global.그랩제어.카메라3.Ready();
                        }

                        if (정보.주소 == "W002E" & 정보.값 == 1) // 표면검사상면 트리거신호
                        {
                            //Global.그랩제어.카메라4.MatImage.Clear();
                            //Global.조명제어.TurnOn(조명구분.상면검사조명);
                            SendValueToPLC(정보.주소, 0);
                            Global.그랩제어.카메라4.Ready();
                        }
                        // 치수검사 트리거 On일 경우( F지그, R지그 둘 중 하나라도 On이면 실행)
                        if (정보.주소 == "W0028" & 정보.값 == 1)
                        {
                            //Global.조명제어.TurnOn(조명구분.BACK);
                            Global.그랩제어.GetItem(카메라구분.Cam01).Ready();
                            SendValueToPLC(정보.주소, 0);
                        }
                        if ((정보.주소 == "W0040") && 정보.값 == 1) //상부 평탄도 검사 데이터 트리거
                        {
                         
                        }
                        if ((정보.주소 == "W0041") && 정보.값 == 1) //하부 평탄도 검사 데이터 트리거
                        {
                           
                        }
                    }
                    OnCompleteReceive(EventArgs.Empty);
                    Thread.Sleep(2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[" + MethodBase.GetCurrentMethod().Name + "]" + ex.ToString());
            }
        }

        protected virtual void OnCompleteReceive(EventArgs e)
        {
            this.CompleteReceive?.Invoke();
        }

        public static Int32 ConvertBooleanToInt32(Boolean value)
        {
            return value ? 1 : 0;
        }

        private void cclink_echo_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (true)
                {
                    bit = !bit;
                    Global.신호제어.SendValueToPLC("B1000", ConvertBooleanToInt32(bit));
                    Thread.Sleep(2000);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("[" + MethodBase.GetCurrentMethod().Name + "]" + ex.ToString());
            }
        }

        public void Close()
        {
            thred_roop_index = 1;
            if (PLC == null) return;
            PLC.Close();
        }

        private class 주소정보
        {
            public String 주소 = String.Empty;
            public Boolean 변경 = false;
            public short 값 = 0;

            public 주소정보(주소구분 구분)
            {
                AddressAttribute a = MvUtils.Utils.GetAttribute<AddressAttribute>(구분);
                this.주소 = a.Address;
                this.변경 = false;
                this.값 = 0;
            }
        }

        private class 주소자료 : Dictionary<주소구분, 주소정보>
        {
            public String[] 주소목록;

            public 주소자료()
            {
                List<String> 주소 = new List<String>();
                foreach (주소구분 구분 in typeof(주소구분).GetEnumValues())
                {
                    주소정보 정보 = new 주소정보(구분);

                    this.Add(구분, 정보);
                    주소.Add(정보.주소);
                }
                this.주소목록 = 주소.ToArray();
            }

            public String Address(주소구분 구분)
            {
                if (!this.ContainsKey(구분)) return String.Empty;
                return this[구분].주소;
            }

            public short GetValue(주소구분 구분)
            {
                if (!this.ContainsKey(구분)) return 501;
                return this[구분].값;
            }

            public bool SetValue(주소구분 구분, short value)
            {
                if (!this.ContainsKey(구분)) return false;
                this[구분].값 = value;
                return true;
            }
        }


        #region AddressAttribute 영역
        public class AddressAttribute : Attribute
        {
            public String Address = String.Empty;
            public Int32 Delay = 0;

            public AddressAttribute(String address)
            {
                this.Address = address;
            }
            public AddressAttribute(String address, Int32 delay)
            {
                this.Address = address;
                this.Delay = delay;
            }
        }
        #endregion
    }
}