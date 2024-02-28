using ActUtlType64Lib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using static SamhwaInspectionNeo.Schemas.검사자료;

namespace SamhwaInspectionNeo.Schemas
{
    // PLC 통신
    [Description("MELSEC Q04UDV")]
    public partial class 신호제어
    {
        public event Global.BaseEvent 동작상태알림;
        public event Global.BaseEvent 원점복귀알림;
        public event Global.BaseEvent 통신상태알림;
        //public event Global.BaseEvent 검사위치알림;
        public event Global.BaseEvent 입출변경알림;

        #region 기본상수 및 멤버
        private static String 로그영역 = "PLC";
        private const Int32 스테이션번호 = 2;
        private const Int32 입출체크간격 = 10;
        private DateTime 시작일시 = DateTime.Now;
        private Boolean 작업여부 = false;
        private ActUtlType64 PLC = null;
        public Boolean 정상여부 = false;

        public enum 정보주소 : Int32
        {
            [Address("W0010")]
            모델변경트리거,
            [Address("W001A")]
            인덱스요청신호,
            [Address("W0011")]
            트레이개수,
            [Address("W0012")]
            원점복귀완료,
            [Address("W0020")]
            결과값요청트리거,
            [Address("W0021")]
            트레이확인카메라트리거,
            [Address("W0022")]
            하부표면검사카메라트리거,
            [Address("W002E")]
            상부표면검사카메라트리거,
            [Address("W0028")]
            상부치수검사카메라트리거,
            [Address("W0040")]
            상부변위센서확인트리거,
            [Address("W0041")]
            하부변위센서확인트리거,
            [Address("B1000")]
            통신확인전송,
            [Address("B1010")]
            통신확인핑퐁,
            [Address("B1018")]
            Front지그,
            [Address("B1019")]
            Rear지그,
            [Address("B1021")]
            NG리트라이,
            [Address("B1030")]
            자동모드,
            [Address("B1040")]
            운전시작,
            [Address("B1017")]
            마스터모드,
            [Address("W13F")]
            생산수량,
        }
        private 통신자료 입출자료 = new 통신자료();
        private static Boolean ToBool(Int32 val) { return val != 0; }
        private static Int32 ToInt(Boolean val) { return val ? 1 : 0; }
        public Int32 정보읽기(정보주소 구분) { return this.입출자료.Get(구분); }
        public Boolean 신호읽기(정보주소 구분) { return ToBool(this.입출자료.Get(구분)); }
        public void 신호쓰기(정보주소 구분, Int32 val) { this.입출자료.Set(구분, val); }
        public void 신호쓰기(정보주소 구분, Boolean val) { this.입출자료.Set(구분, ToInt(val)); }

        #region 입출신호
        public Boolean 자동모드여부 { get { return 신호읽기(정보주소.자동모드); } }
        public Boolean 운전시작여부 { get { return 신호읽기(정보주소.운전시작); } }

        public Boolean 마스터모드여부 { get { return 신호읽기(정보주소.마스터모드); } }

        public Boolean NG리트라이모드여부 { get { return 신호읽기(정보주소.NG리트라이); } }
        //B1018,1819
        public Boolean Front지그 { get { return 신호읽기(정보주소.Front지그); } }
        public Boolean Rear지그 { get { return 신호읽기(정보주소.Rear지그); } }
        public Boolean 통신확인핑퐁 { get { return 신호읽기(정보주소.통신확인핑퐁); } }

        public Int32 모델변경트리거 { get { return 정보읽기(정보주소.모델변경트리거); } }

        //Output Part
        public Boolean 통신확인전송 { get { return 신호읽기(정보주소.통신확인전송); } set { 신호쓰기(정보주소.통신확인전송, value); } }
        public Boolean 트레이확인카메라트리거 { get { return 신호읽기(정보주소.트레이확인카메라트리거); } set { 신호쓰기(정보주소.트레이확인카메라트리거, value); } }

        public Boolean 상부표면검사카메라트리거 { get { return 신호읽기(정보주소.상부표면검사카메라트리거); } set { 신호쓰기(정보주소.상부표면검사카메라트리거, value); } }
        public Boolean 하부표면검사카메라트리거 { get { return 신호읽기(정보주소.하부표면검사카메라트리거); } set { 신호쓰기(정보주소.하부표면검사카메라트리거, value); } }
        public Boolean 상부치수검사카메라트리거 { get { return 신호읽기(정보주소.상부치수검사카메라트리거); } set { 신호쓰기(정보주소.상부치수검사카메라트리거, value); } }

        public Boolean 상부변위센서확인트리거 { get { return 신호읽기(정보주소.상부변위센서확인트리거); } set { 신호쓰기(정보주소.상부변위센서확인트리거, value); } }

        public Boolean 하부변위센서확인트리거 { get { return 신호읽기(정보주소.하부변위센서확인트리거); } set { 신호쓰기(정보주소.하부변위센서확인트리거, value); } }

        public Boolean 결과값요청트리거 { get { return 신호읽기(정보주소.결과값요청트리거); } set { 신호쓰기(정보주소.결과값요청트리거, value); } }

        public Boolean 원점복귀완료 { get { return 신호읽기(정보주소.원점복귀완료); } set { 신호쓰기(정보주소.원점복귀완료, value); } }

        public Boolean 인덱스요청신호 { get { return 신호읽기(정보주소.인덱스요청신호); } set { 신호쓰기(정보주소.인덱스요청신호, value); } }
        #endregion

        #region 검사현황
        public Int32 생산수량정보 { get => this.입출자료.Get(정보주소.생산수량); set => 신호쓰기(정보주소.생산수량, value); }

        public Int32 트레이확인촬영번호 => this.입출자료.Get(정보주소.트레이확인카메라트리거); 
        public Int32 상부표면검사촬영번호 => this.입출자료.Get(정보주소.상부표면검사카메라트리거); 
        public Int32 상부치수검사촬영번호 => this.입출자료.Get(정보주소.상부치수검사카메라트리거);
        public Int32 하부표면검사촬영번호 => this.입출자료.Get(정보주소.하부표면검사카메라트리거);

        private Dictionary<정보주소, Int32> 인덱스버퍼 = new Dictionary<정보주소, Int32>();
        #endregion

        #endregion

        #region 기본함수
        public void Init()
        {
            this.PLC = new ActUtlType64();
            this.PLC.ActLogicalStationNumber = 스테이션번호;
            Global.검사자료.검사완료알림 += 검사완료알림;
            if (Global.환경설정.동작구분 == 동작구분.Live)
            {
                this.입출자료.Init(new Action<정보주소, Int32>((주소, 값) => 자료전송(주소, 값)));
            }
            else this.입출자료.Init(null);
        }
        public void Close() { this.Stop(); }

        private void 검사완료알림(검사결과 결과)
        {
            Int32 오류 = 0;
            Boolean ok = 결과.측정결과 == 결과구분.OK;
            if (Global.신호제어.NG리트라이모드여부)
            {
                Boolean 치수결과 = 결과.CTQ결과 == 결과구분.OK;
                Boolean 표면결과 = 결과.외관결과 == 결과구분.OK;

                if (!치수결과) Global.신호제어.SetDevice($"W000{결과.검사코드}", 1, out 오류);
                else if (!표면결과) Global.신호제어.SetDevice($"W000{결과.검사코드}", 2, out 오류);
            }
            else if (Global.신호제어.마스터모드여부)
            {
                Int32 주소 = 결과.검사코드 - 100;
                Int32 결과값 = ok ? 1 : 2;
                Global.정보로그(로그영역, "PLC로 마스터모드 결과값 전송", $"W000{주소} : {결과값}", false);
                Global.신호제어.SetDevice($"W000{주소}", ok ? 1 : 2, out 오류);
            }
            else
            {
                Global.신호제어.SetDevice($"W000{결과.검사코드}", ok ? 1 : 2, out 오류);
            }


            통신오류알림(오류);
        }

        public void Start()
        {
            if (this.작업여부) return;
            this.작업여부 = true;
            this.정상여부 = true;
            this.시작일시 = DateTime.Now;
            if (Global.환경설정.동작구분 == 동작구분.Live)
            {
                this.입출자료갱신();
                this.출력자료리셋();
                this.생산수량전송();
                this.동작상태알림?.Invoke();
            }
            new Thread(장치통신작업) { Priority = ThreadPriority.Highest }.Start();
        }

        public void Stop() => this.작업여부 = false;
        public Boolean Open()
        {
            if (Global.환경설정.동작구분 != 동작구분.Live) return true;
            this.정상여부 = PLC.Open() == 0; return this.정상여부;
        }

        private void 연결종료()
        {
            try
            {
                PLC?.Close();
                Global.정보로그(로그영역, "PLC 연결종료", $"서버에 연결을 종료하였습니다.", false);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "PLC 연결종료", $"서버 연결을 종료하는 중 오류가 발생하였습니다.\r\n{ex.Message}", false);
            }
        }

        private void 자료전송(정보주소 주소, Int32 값)
        {
            SetDevice(입출자료.Address(주소), 값, out Int32 오류);
            통신오류알림(오류);
        }

        private void 장치통신작업()
        {
            Global.정보로그(로그영역, "PLC 통신", $"통신을 시작합니다.", false);
            while (this.작업여부)
            {
                try { 입출자료분석(); }
                catch (Exception ex) { Debug.WriteLine(ex.Message, 로그영역); }
                Thread.Sleep(입출체크간격);
            }
            Global.정보로그(로그영역, "PLC 통신", $"통신이 종료되었습니다.", false);
            this.연결종료();
        }

        private void 출력자료리셋()
        {
            this.트레이확인카메라트리거 = false;
            this.상부표면검사카메라트리거 = false;
            this.하부표면검사카메라트리거 = false;
            this.상부치수검사카메라트리거 = false;
            this.상부변위센서확인트리거 = false;
            this.하부변위센서확인트리거 = false;
            this.결과값요청트리거 = false;
        }

        public void 생산수량전송() =>
            this.생산수량정보 = Global.모델자료.선택모델.전체갯수;
        #endregion

        #region Get / Set 함수
        private Int32[] ReadDeviceRandom(String[] 주소, out Int32 오류코드)
        {
            Int32[] 자료 = new Int32[주소.Length];
            오류코드 = PLC.ReadDeviceRandom(String.Join("\n", 주소), 주소.Length, out 자료[0]);
            return 자료;
        }

        private Int32 GetDevice(String Address, out Int32 오류코드)
        {
            Int32 value;
            오류코드 = PLC.GetDevice(Address, out value);
            return value;
        }

        public Boolean SetDevice(String Address, Int32 Data, out Int32 오류코드)
        {
            오류코드 = PLC.SetDevice(Address, Data);
            //Debug.WriteLine($"{Data}, {오류코드}", Address);
            return 오류코드 == 0;
        }
        #endregion

        #region 기본 클래스 및 함수
        private static UInt16 ToUInt16(BitArray bits)
        {
            UInt16 res = 0;
            for (int i = 0; i < 16; i++)
                if (bits[i]) res |= (UInt16)(1 << i);
            return res;
        }
        private static BitArray FromUInt16(UInt16 val) => new BitArray(BitConverter.GetBytes(val));

        public class AddressAttribute : Attribute
        {
            public String Address = String.Empty;
            public Int32 Delay = 0;
            public AddressAttribute(String address) => this.Address = address;
            public AddressAttribute(String address, Int32 delay)
            {
                this.Address = address;
                this.Delay = delay;
            }
        }

        public class 통신정보
        {
            public 정보주소 구분;
            public Int32 순번 = 0;
            public Int32 정보 = 0;
            public String 주소 = String.Empty;
            public DateTime 시간 = DateTime.MinValue;
            public Int32 지연 = 0;
            public Boolean 변경 = false;

            public 통신정보(정보주소 구분)
            {
                this.구분 = 구분;
                this.순번 = (Int32)구분;
                AddressAttribute a = MvUtils.Utils.GetAttribute<AddressAttribute>(구분);
                this.주소 = a.Address;
                this.지연 = a.Delay;
            }

            public Boolean Passed()
            {
                if (this.지연 <= 0) return true;
                return (DateTime.Now - 시간).TotalMilliseconds >= this.지연;
            }

            public Boolean Set(Int32 val, Boolean force = false)
            {
                if (this.정보.Equals(val) || !force && !this.Passed())
                {
                    this.변경 = false;
                    return false;
                }

                this.정보 = val;
                this.시간 = DateTime.Now;
                this.변경 = true;
                return true;
            }
        }
        public class 통신자료 : Dictionary<정보주소, 통신정보>
        {
            private Action<정보주소, Int32> Transmit;
            public String[] 주소목록;
            public 통신자료()
            {
                List<String> 주소 = new List<String>();
                foreach (정보주소 구분 in typeof(정보주소).GetEnumValues())
                {
                    통신정보 정보 = new 통신정보(구분);
                    if (정보.순번 < 0) continue;
                    this.Add(구분, 정보);
                    주소.Add(정보.주소);
                }
                this.주소목록 = 주소.ToArray();
            }

            public void Init(Action<정보주소, Int32> transmit) => this.Transmit = transmit;

            public String Address(정보주소 구분)
            {
                if (!this.ContainsKey(구분)) return String.Empty;
                return this[구분].주소;
            }

            public Int32 Get(정보주소 구분)
            {
                if (!this.ContainsKey(구분)) return 0;
                return this[구분].정보;
            }

            public void Set(Int32[] 자료)
            {
                foreach (통신정보 정보 in this.Values)
                {
                    Int32 val = 자료[정보.순번];
                    Boolean 변경 = 정보.Set(val);
                }
            }

            // Return : Changed
            public Boolean Set(정보주소 구분, Int32 value)
            {
                if (!this[구분].Set(value, true)) return false;
                this.Transmit?.Invoke(구분, value);
                return true;
            }

            public void SetDelay(정보주소 구분, Int32 value, Int32 resetTime)
            {
                if (resetTime <= 0)
                {
                    if (!this[구분].Set(value, true)) return;
                    this.Transmit?.Invoke(구분, value);
                }
                Task.Run(() =>
                {
                    Task.Delay(resetTime).Wait();
                    if (this[구분].Set(value, true))
                        this.Transmit?.Invoke(구분, value);
                });
            }

            public Boolean Changed(정보주소 구분) => this[구분].변경;
            public Boolean Firing(정보주소 구분, Boolean 상태, out Boolean 현재, out Boolean 변경)
            {
                현재 = ToBool(this[구분].정보);
                변경 = this[구분].변경;
                return 변경 && 현재 == 상태;
            }
            public Dictionary<정보주소, Int32> Changes(정보주소 시작, 정보주소 종료) => this.Changes((Int32)시작, (Int32)종료);
            public Dictionary<정보주소, Int32> Changes(Int32 시작, Int32 종료)
            {
                Dictionary<정보주소, Int32> 변경 = new Dictionary<정보주소, Int32>();
                foreach (정보주소 구분 in typeof(정보주소).GetEnumValues())
                {
                    Int32 번호 = (Int32)구분;
                    if (번호 < 시작 || 번호 > 종료 || !this[구분].변경) continue;
                    변경.Add(구분, this[구분].정보);
                }
                return 변경;
            }
        }
        #endregion
    }
}