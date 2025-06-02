using GraphicsSetModuleCs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Npgsql;
using SqlKata.Compilers;
using SqlKata;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using VM.PlatformSDKCS;

namespace SamhwaInspectionNeo.Schemas
{
    public class 검사자료 : Common.SyncList<검사결과>
    {
        public delegate void 검사진행알림(검사결과 결과);
        public event 검사진행알림 검사완료알림;

        [JsonIgnore]
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Inspection", "검사내역");
        [JsonIgnore]
        private TranslationAttribute 저장오류 = new TranslationAttribute("An error occurred while saving the data.", "자료 저장중 오류가 발생하였습니다.");
        [JsonIgnore]
        public 검사결과테이블 테이블 = null;
        [JsonIgnore]
        private Common.SyncDictionary<Int32, 검사결과> 검사스플 = new Common.SyncDictionary<Int32, 검사결과>();
        [JsonIgnore]
        public 검사결과 수동검사;

        public void Init()
        {
            //this.AllowEdit = true;
            //this.AllowRemove = true;
            this.테이블 = new 검사결과테이블();
            this.Load();
            this.수동검사초기화();
            Global.환경설정.모델변경알림 += 모델변경알림;
        }

        public Boolean Close()
        {
            if (this.테이블 == null) return true;
            this.테이블.Save();
            this.테이블.자료정리(Global.환경설정.결과보관);
            return true;
        }

        private void 수동검사초기화()
        {
            this.수동검사 = new 검사결과();
            this.수동검사.검사코드 = 9999;
            this.수동검사.Reset();
        }

        private String 저장파일(DateTime 날짜) => Path.Combine(Global.환경설정.문서저장경로, MvUtils.Utils.FormatDate(날짜, "{0:yyyyMMdd}") + ".json");
        public void Save() => this.테이블.Save();
        //public void SaveAsync() => this.테이블.SaveAsync();

        //public void Save(검사결과 결과)
        //{
        //    Common.DebugWriteLine("검사결과", 로그구분.정보, $"검사결과 DB 저장 검사일시 : {결과.검사일시}, 검사번호 : {결과.검사코드}");
        //    //using (검사결과테이블 table = new 검사결과테이블())
        //    //    return table.Save(결과);
        //    this.테이블.Add(결과);
        //    this.Save();
        //}

        public Boolean Save(검사결과 결과)
        {
            Common.DebugWriteLine(로그영역.GetString(), 로그구분.정보, $"[검사코드 - {결과.검사코드}] Database Save.");
            using (검사결과테이블 table = new 검사결과테이블())
                return table.Save(결과);
        }

        private Boolean SaveJson()
        {
            //DateTime 날짜 = DateTime.Today;
            //try
            //{
            //    List<검사결과> 자료 = this.테이블.Select(날짜, 날짜);
            //    if (자료.Count < 1) return true;
            //    File.WriteAllText(this.저장파일(날짜), JsonConvert.SerializeObject(자료, Formatting.Indented));
            //    return true;
            //}
            //catch (Exception ex)
            //{
            //    Global.오류로그(로그영역.GetString(), Localization.저장.GetString(), $"{저장오류.GetString()}\r\n{ex.Message}", true);
            //}
            return false;
        }

        public void Load() => this.Load(DateTime.Today, DateTime.Today);
        public void Load(DateTime 시작, DateTime 종료)
        {
            try
            {
                this.Clear();
                List<검사결과> 자료 = this.테이블.Select(new QueryPrms { 시작 = 시작, 종료 = 종료.AddDays(1), 역순정렬 = false });
                자료.ForEach(검사 => {
                    this.Add(검사);
                });
            }
            catch (Exception ex) { Global.오류로그(로그영역.GetString(), "Load", ex.Message, true); }
        }

        public List<검사결과> GetData(DateTime 시작, DateTime 종료, 모델구분 모델) => this.테이블.Select(new QueryPrms { 시작 = 시작, 종료 = 종료, 모델 = 모델, 역순정렬 = false });
        private void 모델변경알림(모델구분 모델코드) => this.수동검사초기화();

        private void 자료추가(검사결과 결과)
        {
            this.Add(결과);
            //this.검사시작알림?.Invoke(결과);
            //this.Insert(0, 결과);
            //this.테이블.Add(결과);
        }

        public void 검사항목제거(List<검사정보> 자료) => this.테이블.Remove(자료);
        public Boolean 결과삭제(검사결과 정보)
        {
            this.Remove(정보);
            return this.테이블.Delete(정보) > 0;
        }
        //public Boolean 결과삭제(검사결과 결과, 검사정보 정보)
        //{
        //    결과.검사내역.Remove(정보);
        //    return this.테이블.Delete(정보) > 0;
        //}
        //public 검사결과 결과조회(DateTime 일자, 모델구분 모델, Int32 코드) => this.테이블.Select(일자, 모델, 코드);

        #region 검사로직
        private DateTime LastTime = DateTime.MinValue;
        public 검사결과 검사시작(Int32 검사코드)
        {
            검사결과 검사 = 검사항목찾기(검사코드, true);
            if (검사 == null)
            {
                검사 = new 검사결과() { 검사코드 = 검사코드 };
                검사.Reset();
                검사.측정결과 = 결과구분.IN; 
                this.자료추가(검사);
                this.검사스플.Add(검사.검사코드, 검사);
                Common.DebugWriteLine(로그영역.GetString(), 로그구분.정보, $"[{(Int32)Global.환경설정.선택모델} - {검사.검사코드}] 신규검사 시작.");
            }

            return 검사;
        }
        public 검사결과 항목검사(Flow구분 구분, 지그위치 지그, String name, Single value)
        {
            Int32 검사코드 = (Int32)구분 < 6 ? (Int32)구분 : (Int32)구분 < 20 ? (Int32)구분 - 10 : (Int32)구분 - 20;

            검사코드 = Global.신호제어.마스터모드여부 ? 검사코드 + 100 : 검사코드;

            검사결과 검사 = this.검사항목찾기(검사코드);
            if (검사 == null) return null;

            if (Global.환경설정.동작구분 == 동작구분.LocalTest) 검사.표면검사강제OK(구분, 지그);

            if (Global.신호제어.마스터모드여부) 검사.표면검사강제OK(구분, 지그);

            //if (Global.환경설정.하부표면검사사용여부 == false) 검사.하부표면검사강제OK(구분, 지그);

            검사.SetResult(구분, 지그, name, value);
          
            return 검사;
        }

        public 검사결과 검사결과계산(Int32 검사코드)
        {
            검사코드 = Global.신호제어.마스터모드여부 ? 검사코드 + 100 : 검사코드;
            검사결과 검사 = this.검사항목찾기(검사코드);
            
            if (검사 == null)
            {
                Global.오류로그(로그영역.GetString(), "결과계산", $"[{(Int32)Global.환경설정.선택모델}.{검사코드}] 해당 검사가 없습니다.", false);
                return null;
            }
            검사.결과계산();
            //Global.모델자료.수량추가(검사.모델구분, 검사.측정결과);
            if (!검사.검사중확인())
            {
                //검사.결과계산();
                Common.DebugWriteLine(로그영역.GetString(), 로그구분.정보, $"검사코드 [ {검사코드} - {검사.측정결과} ] 제거");
                Global.모델자료.수량추가(검사.모델구분, 검사.측정결과);
                this.검사스플제거(검사코드);
                this.Save(검사);
                this.검사완료알림?.Invoke(검사);
            }

            return 검사;
        }

        public void 검사스플제거(Int32 검사코드) => this.검사스플.Remove(검사코드);

        // 현재 검사중인 정보를 검색
        public 검사결과 검사항목찾기(Int32 검사코드, Boolean 신규여부 = false)
        {
            //if (!Global.장치통신.자동수동여부) return this.수동검사;
            검사결과 검사 = null;
            if (검사코드 >= 0 && this.검사스플.ContainsKey(검사코드))
                검사 = this.검사스플[검사코드];
            if (검사 == null && !신규여부)
            {
                this.검사스플.Remove(검사코드);
                Global.오류로그(로그영역.GetString(), "제품검사", $"[{검사코드}] Index가 없습니다.", true);
            }

            return 검사;
        }

        //public void ResetItem(검사결과 검사)
        //{
        //    if (검사 == null) return;
        //    this.ResetItem(this.IndexOf(검사));
        //}
        #endregion
    }
}
