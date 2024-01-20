using DevExpress.Utils.Extensions;
using MvUtils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
            if (!입출자료갱신()) return false;
            입출변경확인();
            제품검사수행();
            장치상태확인();
            통신핑퐁수행();
            return true;
        }

        private void 장치상태확인()
        {
            if (this.입출자료.Changed(정보주소.자동모드) || this.입출자료.Changed(정보주소.운전시작))
                this.동작상태알림?.Invoke();
        }

        // 검사위치 변경 확인
        private void 입출변경확인()
        {
            Dictionary<정보주소, Int32> 변경 = this.입출자료.Changes(정보주소.모델변경트리거, 정보주소.생산수량);
            if (변경.Count < 1) return;
            this.입출변경알림?.Invoke();
        }

        private void 제품검사수행()
        {
            영상촬영수행();
            검사결과전송();
        }
        // 카메라 별 현재 검사 위치의 검사번호를 요청
        public Int32 촬영위치번호(카메라구분 구분)
        {
            if (구분 == 카메라구분.Cam01) return this.인덱스버퍼[정보주소.상부치수검사카메라트리거];
            if (구분 == 카메라구분.Cam02) return this.인덱스버퍼[정보주소.트레이확인카메라트리거];
            if (구분 == 카메라구분.Cam03) return this.인덱스버퍼[정보주소.상부표면검사카메라트리거];
            if (구분 == 카메라구분.Cam04) return this.인덱스버퍼[정보주소.하부표면검사카메라트리거];
            return 0;
        }

        private Int32 검사위치번호(정보주소 구분)
        {
            if (!this.입출자료.Firing(구분, true, out Boolean 현재, out Boolean 변경))
            {
                //if (현재) 정보쓰기(구분, false);
                return -1;
            }
            //if (!변경) return -1;

            Int32 index = 0;
            if (구분 == 정보주소.상부치수검사카메라트리거) index = this.상부치수검사촬영번호;
            else if (구분 == 정보주소.상부표면검사카메라트리거) index = this.상부표면검사촬영번호;
            else if (구분 == 정보주소.하부표면검사카메라트리거) index = this.하부표면검사촬영번호;
            else if (구분 == 정보주소.트레이확인카메라트리거) index = this.트레이확인촬영번호;
            //else if (구분 == 정보주소.결과값요청트리거) index = this.양불판정번호;

            //Debug.WriteLine("----------------------------------");
            if (index == 0) Global.경고로그(로그영역, 구분.ToString(), $"해당 위치에 검사할 제품이 없습니다.", false); // There are no index of products to inspect in that location.
            //else Debug.WriteLine($"{Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss.fff}")}  {구분} => {index}", "Trigger");
            this.인덱스버퍼[구분] = index;
            return index;
        }

        public List<Int32> 검사중인항목()
        {
            List<Int32> 대상 = new List<Int32>();
            Int32 시작 = (Int32)정보주소.하부표면검사카메라트리거;
            Int32 종료 = (Int32)정보주소.상부표면검사카메라트리거;
            for (Int32 i = 종료; i >= 시작; i--)
            {
                정보주소 구분 = (정보주소)i;
                if (this.입출자료[구분].정보 <= 0) continue;
                대상.Add(this.입출자료[구분].정보);
            }
            return 대상;
        }
        public void 검사결과전송()
        {
            Int32 검사코드 = this.검사위치번호(정보주소.상부치수검사카메라트리거);
            if (검사코드 < 0) return;
            //if (검사코드 <= 0) { 강제결과전송(false); return; }
            검사결과 검사 = Global.검사자료.검사결과계산(검사코드);
            //if (검사 == null) { 강제결과전송(false); return; }
            //// 강제 OK
            //강제결과전송(true); return;

            Boolean ok = 검사.측정결과 == 결과구분.OK;
            if (ok)
            {
                //this.불량결과쓰기 = 0;
            }
            else
            {
                //this.양품결과쓰기 = 0;
                //this.불량결과쓰기 = 검사.CTQ결과 != 결과구분.OK ? 1 : 2;
            }
            //this.양품여부요청 = ok;
            //this.불량여부요청 = !ok;
            //this.검사결과요청 = false;
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
                //Int32 검사코드 = this.검사위치번호(정보주소.상부치수검사카메라트리거);
                //검사결과 검사 = Global.검사자료.검사시작(검사코드);
                Debug.WriteLine("상부 치수검사 신호 들어옴");
                new Thread(() =>
                {
                    //Global.조명제어.TurnOn(카메라구분.Cam01);
                    Global.그랩제어.Ready(카메라구분.Cam01);
                }).Start();
                신호쓰기(정보주소.상부치수검사카메라트리거, false);
            }
            // 상부 GigE 카메라 영상취득 시작
            if (상부표면검사번호 > 0)
            {
                Debug.WriteLine("상부 표면검사 신호 들어옴");
                //new Thread(() =>
                //{
                //    Global.조명제어.TurnOn(카메라구분.Cam03);
                //    Global.그랩제어.Ready(카메라구분.Cam03);
                //}).Start();
                //신호쓰기(정보주소.상부표면검사카메라트리거, 0);
            }
            // 하부 GigE 카메라 영상취득 시작
            if (하부표면검사번호 > 0)
            {
                Debug.WriteLine("하부 표면검사 신호 들어옴");
                //new Thread(() =>
                //{
                //    Global.조명제어.TurnOn(카메라구분.Cam04);
                //    Global.그랩제어.Ready(카메라구분.Cam04);
                //}).Start();
                //신호쓰기(정보주소.하부표면검사카메라트리거, 0);
            }

            // 트레이 검사 카메라 영상취득 시작
            if (트레이확인검사번호 > 0)
            {
                Debug.WriteLine("공트레이 신호 들어옴");
                //new Thread(() =>
                //{
                //    Global.조명제어.TurnOn(카메라구분.Cam02);
                //    Global.그랩제어.Ready(카메라구분.Cam02);
                //}).Start();
                //신호쓰기(정보주소.트레이확인카메라트리거, 0);
            }
        }

        // 최종 검사 결과 보고
        //private void 검사결과전송()
        //{
        //    Int32 검사번호 = this.검사위치번호(정보주소.결과요청);
        //    if (검사번호 < 0) return;
        //    Global.모델자료.선택모델.검사종료(검사번호);
        //    Task.Run(() =>
        //    {
        //        검사결과 검사 = Global.검사자료.검사결과계산(검사번호);
        //        if (Global.환경설정.강제배출) { 결과전송(Global.환경설정.양품불량); return; } // 강제배출
        //        if (검사 == null) 결과전송(false);
        //        else 결과전송(검사.측정결과 == 결과구분.OK); // 배출 수행
        //    });
        //}
        //// 신호 Writing 순서 중요
        //private void 결과전송(Boolean 양품여부)
        //{
        //    //Debug.WriteLine(양품여부, "결과전송");
        //    this.양품여부요청 = 양품여부;
        //    this.불량여부요청 = !양품여부;
        //    this.검사결과요청 = false;
        //}

        // 핑퐁
        private DateTime 최종송신 = DateTime.Now.AddMinutes(-5);
        private void 통신핑퐁수행()
        {
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
