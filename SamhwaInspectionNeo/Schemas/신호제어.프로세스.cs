using DevExpress.Internal.WinApi;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace SamhwaInspectionNeo.Schemas
{
    partial class 신호제어
    {
        private DateTime 오류알림시간 = DateTime.Today.AddDays(-1);
        private Int32 오류알림간격 = 30; // 초
        public void 통신오류알림(Int32 오류코드)
        {
            if (오류코드 == 0)
            {
                this.정상여부 = true;
                return;
            }
            if ((DateTime.Now - this.오류알림시간).TotalSeconds < this.오류알림간격) return;
            this.오류알림시간 = DateTime.Now;
            this.정상여부 = false;
            Global.오류로그(로그영역, "PLC 통신", $"[{오류코드.ToString("X8")}] 통신 오류가 발생하였습니다.", false);
        }

        private Boolean 입출자료갱신()
        {
            // 입출자료 갱신
            Int32[] 자료 = ReadDeviceRandom(입출자료.주소목록, out Int32 오류);
            if (오류 != 0)
            {
                통신오류알림(오류);
                return false;
            }
            this.입출자료.Set(자료);
            return true;
        }

        private Boolean 입출자료분석()
        {
            if (Global.환경설정.동작구분 == 동작구분.LocalTest) return false;
            if (!입출자료갱신()) return false;
            입출변경확인();
            제품검사수행();
            장치상태확인();
            통신핑퐁수행();
            if (!Global.신호제어.자동모드여부)
                모델변경확인();
            원점복귀확인();
            return true;
        }

        private void 원점복귀확인()
        {
            if (this.입출자료.Changed(정보주소.원점복귀완료) || this.입출자료.Get(정보주소.원점복귀완료) > 0)
                this.원점복귀알림?.Invoke();
        }

        private void 장치상태확인()
        {
            if (this.입출자료.Changed(정보주소.자동모드) || this.입출자료.Changed(정보주소.운전시작))
                this.동작상태알림?.Invoke();
        }

        private void 모델변경확인()
        {
            Int32 모델번호 = Global.신호제어.모델변경트리거;
            모델구분 현재모델 = Global.환경설정.선택모델;
            Int32 모델명 = 모델명변환(모델번호);

            if ((모델구분)모델명 != 현재모델)
            {
                Task.Run(() =>
                {
                    Global.환경설정.모델변경요청((모델구분)모델명);
                });
            }
        }

        private Int32 모델명변환(Int32 모델번호) //PLC에서 받은 모델명 String으로 변환
        {
            String hexString = 모델번호.ToString("X");
            // Ensure the hexadecimal string has an even number of digits for proper conversion
            hexString = hexString.PadLeft(hexString.Length + hexString.Length % 2, '0');
            // Convert each pair of hexadecimal digits to a character and store in a temporary string
            string tempString = "";
            for (int i = 0; i < hexString.Length; i += 2)
            {
                string hexChar = hexString.Substring(i, 2);
                int charValue = Convert.ToInt32(hexChar, 16); // Convert from hex to int
                tempString += (char)charValue; // Convert int to char and append to the temp string
            }
            // Reverse the characters in the temporary string to get the final result
            string resultString = "";
            for (int i = tempString.Length - 1; i >= 0; i--)
                resultString += tempString[i];

            if (resultString == "-A") return 1;
            else if (resultString == "-B") return 2;
            else if (resultString == "-C") return 3;
            else if (resultString == "-D") return 4;

            return 0;
        }

        // 검사위치 변경 확인
        private void 입출변경확인()
        {
            Dictionary<정보주소, Int32> 변경 = this.입출자료.Changes(정보주소.모델변경트리거, 정보주소.생산수량);
            if (변경.Count < 1) return;
            this.입출변경알림?.Invoke();
        }

        private void 제품검사수행() => 영상촬영수행();

        // 카메라 별 현재 검사 위치의 검사번호를 요청
        public Int32 촬영위치번호(카메라구분 구분)
        {
            if (구분 == 카메라구분.Cam01) return this.인덱스버퍼[정보주소.상부치수검사카메라트리거];
            if (구분 == 카메라구분.Cam02) return this.인덱스버퍼[정보주소.트레이확인카메라트리거];
            if (구분 == 카메라구분.Cam03) return this.인덱스버퍼[정보주소.상부표면검사카메라트리거];
            return 0;
        }

        private Int32 검사위치번호(정보주소 구분)
        {
            if (!this.입출자료.Firing(구분, true, out Boolean 현재, out Boolean 변경)) return -1;

            Int32 index = 0;
            if (구분 == 정보주소.상부치수검사카메라트리거) index = this.상부치수검사촬영번호;
            else if (구분 == 정보주소.상부표면검사카메라트리거) index = this.상부표면검사촬영번호;
            else if (구분 == 정보주소.하부표면검사카메라트리거) index = this.하부표면검사촬영번호;
            else if (구분 == 정보주소.트레이확인카메라트리거) index = this.트레이확인촬영번호;

            if (index == 0) Global.경고로그(로그영역, 구분.ToString(), $"해당 위치에 검사할 제품이 없습니다.", false);

            this.인덱스버퍼[구분] = index;
            return index;
        }

        public List<Int32> 검사중인항목()
        {
            List<Int32> 대상 = new List<Int32>();
            Int32 시작 = (Int32)정보주소.트레이확인카메라트리거;
            Int32 종료 = (Int32)정보주소.상부치수검사카메라트리거;
            for (Int32 i = 종료; i >= 시작; i--)
            {
                정보주소 구분 = (정보주소)i;
                if (this.입출자료[구분].정보 <= 0) continue;
                대상.Add(this.입출자료[구분].정보);
            }
            return 대상;
        }

        public void 지그위치체크() //필요없을수도..?
        {
            Debug.WriteLine($"Front 지그 신호 : {Global.신호제어.Front지그}");
            Debug.WriteLine($"Rear 지그 신호 : {Global.신호제어.Rear지그}");
            if (Global.신호제어.Front지그)
            {
                Global.VM제어.글로벌변수제어.SetValue("Front지그", "1");
                Global.VM제어.글로벌변수제어.SetValue("Rear지그", "0");
            }
            else if (Global.신호제어.Rear지그)
            {
                Global.VM제어.글로벌변수제어.SetValue("Front지그", "0");
                Global.VM제어.글로벌변수제어.SetValue("Rear지그", "1");
            }
        }

        private void 검사스플생성()
        {
            for (int lop = 0; lop < 4; lop++)
            {
                Int32 검사코드 = Global.신호제어.마스터모드여부 ? Convert.ToInt32((Flow구분)lop + 100) : Convert.ToInt32((Flow구분)lop);
                //마스터 모드일때 Flow1,2만 실행하도록
                if (Global.신호제어.마스터모드여부 && lop > 1) break;

                Global.검사자료.검사시작(검사코드);
            }
        }

        private void 영상촬영수행()
        {
            Int32 상부치수검사번호 = this.검사위치번호(정보주소.상부치수검사카메라트리거);
            Int32 상부표면검사번호 = this.검사위치번호(정보주소.상부표면검사카메라트리거);
            Int32 하부표면검사번호 = this.검사위치번호(정보주소.하부표면검사카메라트리거);
            Int32 트레이확인검사번호 = this.검사위치번호(정보주소.트레이확인카메라트리거);

            // 16K 상부 카메라 영상취득 시작
            if (상부치수검사번호 > 0)
            {
                Debug.WriteLine("상부 치수검사 신호 들어옴");
                지그위치체크();
                검사스플생성();
                new Thread(() =>
                {
                    Global.조명제어.TurnOn(카메라구분.Cam01);
                    Global.그랩제어.Ready(카메라구분.Cam01);
                }).Start();
                신호쓰기(정보주소.상부치수검사카메라트리거, 0);
            }
            // 상부 GigE 카메라 영상취득 시작
            if (상부표면검사번호 > 0)
            {
                Debug.WriteLine("상부 표면검사 신호 들어옴");
                Global.그랩제어.GetItem(카메라구분.Cam03).ClearImage();
                new Thread(() =>
                {
                    Global.조명제어.TurnOn(카메라구분.Cam03);
                    Global.그랩제어.Ready(카메라구분.Cam03);
                }).Start();
                신호쓰기(정보주소.상부표면검사카메라트리거, 0);
            }
            // 하부 GigE 카메라 영상취득 시작
            if (하부표면검사번호 > 0)
            {
                Debug.WriteLine("하부 표면검사 신호 들어옴");
                new Thread(() =>
                {
                    Global.조명제어.TurnOn(카메라구분.Cam04);
                    Global.그랩제어.Ready(카메라구분.Cam04);
                }).Start();
                신호쓰기(정보주소.하부표면검사카메라트리거, 0);
            }
            // 트레이 검사 카메라 영상취득 시작
            if (트레이확인검사번호 > 0)
            {
                Debug.WriteLine("공트레이 신호 들어옴");
                new Thread(() =>
                {
                    Global.조명제어.TurnOn(카메라구분.Cam02);
                    Global.그랩제어.Ready(카메라구분.Cam02);
                    Global.그랩제어.SoftTrigger(카메라구분.Cam02);
                }).Start();
                신호쓰기(정보주소.트레이확인카메라트리거, 0);
            }
        }

        // 핑퐁
        private DateTime 최종송신 = DateTime.Now.AddMinutes(-5);
        private void 통신핑퐁수행()
        {
            Boolean 연결신호확인 = 신호읽기(정보주소.통신확인전송);
            //Debug.WriteLine($"연결신호 : {연결신호확인}");
            신호쓰기(정보주소.통신확인전송, !연결신호확인);

            //신호쓰기(정보주소.상부치수검사카메라트리거, 0);
            //if (!this.입출자료[정보주소.통신핑퐁].Passed()) return;
            //if (this.시작일시.Day != DateTime.Today.Day)
            //{
            //    this.시작일시 = DateTime.Now;
            //    this.검사번호리셋 = true;
            //    Global.모델자료.선택모델.날짜변경();
            //}
            //this.통신확인핑퐁 = !this.통신확인핑퐁;
            //TimeSpan 시간 = DateTime.Now - 최종송신;
            //최종송신 = DateTime.Now;
            //if (시간.TotalMinutes < 1 && 시간.TotalMilliseconds > 1100)
            //    Global.경고로그(로그영역, "Ping Pong", $"Delay: {MvUtils.Utils.FormatNumeric(시간.TotalMilliseconds)}", false);
            //this.통신상태알림?.Invoke();
        }
    }
}
