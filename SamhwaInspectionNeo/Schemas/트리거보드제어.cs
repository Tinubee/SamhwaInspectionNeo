using MVSol.Enc852;
using MVSol.IO.Ports;
using Newtonsoft.Json;
using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Navigation;
using static SamhwaInspectionNeo.Schemas.신호제어;

namespace SamhwaInspectionNeo.Schemas
{
    public class 트리거보드제어
    {
        public event Global.BaseEvent 트리거보드상태알림;
        public delegate void 엔코더위치변경(Decimal 현재위치, Int32 위치);
        public delegate void 트리거횟수변경(Decimal 현재횟수, Int32 위치);

        public event 엔코더위치변경 엔코더위치알림;
        public event 트리거횟수변경 트리거카운트변경알림;

        private Boolean 작업여부 = false;
        public Boolean 정상여부 = false;
        private const Int32 상태체크간격 = 10;

        public enum 트리거번호
        {
            Trigger0,
            Trigger1,
            Trigger2,
            Trigger3,
        }
        public enum 엔코더번호
        {
            Encoder0,
            Encoder1,
        }

        [JsonProperty("TriggerBoard"), Translation("TriggerBoard", "트리거보드")]
        public MVEnc852v3Comm 트리거보드 { get; set; }
        [JsonProperty("Port"), Translation("Port", "포트")]
        public 조명포트 포트 { get; set; } = 조명포트.COM3;
        [JsonProperty("FirmWareVersion"), Translation("FirmWareVersion", "펌웨어버전")]
        public String 펌웨어버전 { get; set; }
        [JsonProperty("LogicVersion"), Translation("LogicVersion", "로직버전")]
        public String 로직버전 { get; set; }
        [JsonIgnore]
        public List<엔코더정보> 엔코더들 { get; set; }
        [JsonIgnore]
        public List<트리거정보> 트리거들 { get; set; }

        [JsonIgnore]
        public const String 로그영역 = "트리거보드장치";

        public void Init()
        {
            this.트리거보드 = new MVEnc852v3Comm(this.포트.ToString());
            try
            {
                if (!this.트리거보드.IsOpen)
                {
                    this.트리거보드.Open();
                    this.Load();
                    Global.정보로그(로그영역, "초기화", $"트리거보드 연결 완료. [ {this.트리거보드.PortName} ]", false);
                }
            }
            catch (Exception ex)
            {
                Global.정보로그(로그영역, "연결오류", $"트리거보드 연결 실패. [ {ex.Message} ]", false);
            }
        }

        public void Load()
        {
            this.펌웨어버전 = this.트리거보드.GetFirmVersion();
            this.로직버전 = this.트리거보드.GetLogicVersion();

            this.트리거들 = new List<트리거정보>();
            this.엔코더들 = new List<엔코더정보>();

            this.트리거들.Add(new 트리거정보((Int32)트리거번호.Trigger0));
            this.트리거들.Add(new 트리거정보((Int32)트리거번호.Trigger1));
            this.트리거들.Add(new 트리거정보((Int32)트리거번호.Trigger2));
            this.트리거들.Add(new 트리거정보((Int32)트리거번호.Trigger3));

            this.엔코더들.Add(new 엔코더정보((Int32)엔코더번호.Encoder0));
            this.엔코더들.Add(new 엔코더정보((Int32)엔코더번호.Encoder1));
        }

        public void Start()
        {
            if (Global.환경설정.동작구분 == 동작구분.Live)
            {
                this.작업여부 = true;
                this.정상여부 = true;
                this.트리거보드상태알림?.Invoke();
            }

            new Thread(트리거보드상태확인) { Priority = ThreadPriority.Highest }.Start();
        }

        private void 트리거보드상태확인()
        {
            while (this.작업여부)
            {
                try { 트리거보드상태분석(); }
                catch (Exception ex) { Debug.WriteLine(ex.Message, 로그영역); }
                Thread.Sleep(상태체크간격);
            }
            Global.정보로그(로그영역, "트리거보드", "트리거보드 연결이 해제되었습니다..", false);
            //this.Close();
        }

        public Boolean 트리거보드상태분석()
        {
            if (Global.환경설정.동작구분 == 동작구분.LocalTest) return false;

            엔코더위치갱신();
            트리거횟수갱신();
            return true;
        }

        public void 트리거횟수갱신()
        {
            foreach (트리거정보 트리거 in this.트리거들)
            {
                Decimal 횟수 = this.ReadTriggerCount((트리거번호)트리거.번호);

                if (트리거.횟수 != 횟수)
                {
                    트리거.횟수 = 횟수;
                    this.트리거카운트변경알림?.Invoke(트리거.횟수, 트리거.번호);
                }
            }
        }

        public void 엔코더위치갱신()
        {
            foreach (엔코더정보 엔코더 in this.엔코더들)
            {
                Decimal 현재위치 = this.ReadPosition((엔코더번호)엔코더.번호);

                if (엔코더.현재위치 != 현재위치)
                {
                    엔코더.현재위치 = 현재위치;
                    this.엔코더위치알림?.Invoke(엔코더.현재위치, 엔코더.번호);
                }
            }
            //Decimal Encoder0Pos = this.ReadPosition(엔코더번호.Encoder0);
            //Decimal Encoder1Pos = this.ReadPosition(엔코더번호.Encoder0);

            //if(Encoder0Pos != this.엔코더0)
            //{
            //    this.엔코더0 = Encoder0Pos;
            //    this.엔코더위치알림?.Invoke(this.엔코더0, (Int32)엔코더번호.Encoder0);
            //}
            //if (Encoder1Pos != this.엔코더1)
            //{
            //    this.엔코더1 = Encoder1Pos;
            //    this.엔코더위치알림?.Invoke(this.엔코더1, (Int32)엔코더번호.Encoder1);
            //}
        }

        public void Stop()
        {
            this.작업여부 = false;
            this.정상여부 = false;
        }
        public void Close()
        {
            this.Stop();
            this.트리거보드?.Close();
            this.트리거보드?.Dispose();
            this.트리거보드 = null;
        }

        public void ClearAll()
        {
            this.트리거보드?.ClearDigitalInputCountAll();
            this.트리거보드?.ClearTriggerCountAll();
            this.트리거보드?.ClearEncoderPositionAll();
            this.트리거보드?.ClearErrorCountAll();
        }

        public void ClearAllPosition() => this.트리거보드.ClearEncoderPositionAll();

        public void ClearAllTrigger() => this.트리거보드.ClearTriggerCountAll();

        public Decimal ReadPosition(엔코더번호 번호) => this.트리거보드.GetEncoderPosition((Int32)번호);

        public Decimal ReadTriggerCount(트리거번호 번호) => this.트리거보드.GetTriggerCount((Int32)번호);

        public Decimal ReadStartPosition(트리거번호 번호) => this.트리거보드.GetTriggerPositionStart((Int32)번호);

        public Decimal ReadEndPosition(트리거번호 번호) => this.트리거보드.GetTriggerPositionEnd((Int32)번호);

        public class 트리거정보
        {
            public Int32 번호 { get; set; }
            public Decimal 횟수 { get; set; } = 0;
            public Decimal 시작점 { get; set; } = 0;
            public Decimal 종료점 { get; set; } = 0;
            public 트리거정보(Int32 번호)
            {
                this.번호 = 번호;
                this.시작점 = Global.트리거보드제어.ReadStartPosition((트리거번호)this.번호);
                this.종료점 = Global.트리거보드제어.ReadEndPosition((트리거번호)this.번호);
            }
        }
        public class 엔코더정보
        {
            public Int32 번호 { get; set; }
            public Decimal 현재위치 { get; set; } = 0;

            public 엔코더정보(Int32 번호)
            {
                this.번호 = 번호;
            }
        }

    }
}
