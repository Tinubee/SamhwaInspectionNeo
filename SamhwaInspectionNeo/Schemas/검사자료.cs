﻿using GraphicsSetModuleCs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using VM.PlatformSDKCS;

namespace SamhwaInspectionNeo.Schemas
{
    public class 검사자료 : BindingList<검사결과>
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
        private Dictionary<Int32, 검사결과> 검사스플 = new Dictionary<Int32, 검사결과>();
        [JsonIgnore]
        public 검사결과 수동검사;

        public void Init()
        {
            this.AllowEdit = true;
            this.AllowRemove = true;
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
            return this.SaveJson();
        }

        private void 수동검사초기화()
        {
            this.수동검사 = new 검사결과();
            this.수동검사.검사코드 = 9999;
            this.수동검사.Reset();
        }

        private String 저장파일(DateTime 날짜) => Path.Combine(Global.환경설정.문서저장경로, MvUtils.Utils.FormatDate(날짜, "{0:yyyyMMdd}") + ".json");
        public void Save() => this.테이블.Save();
        public void SaveAsync() => this.테이블.SaveAsync();

        public void Save(검사결과 결과)
        {
            Common.DebugWriteLine("검사결과", 로그구분.정보, $"검사결과 DB 저장 검사일시 : {결과.검사일시}, 검사번호 : {결과.검사코드}");

            this.테이블.Add(결과);
            this.Save();
        }

        private Boolean SaveJson()
        {
            DateTime 날짜 = DateTime.Today;
            try
            {
                List<검사결과> 자료 = this.테이블.Select(날짜, 날짜);
                if (자료.Count < 1) return true;
                File.WriteAllText(this.저장파일(날짜), JsonConvert.SerializeObject(자료, Formatting.Indented));
                return true;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), Localization.저장.GetString(), $"{저장오류.GetString()}\r\n{ex.Message}", true);
            }
            return false;
        }

        public void Load() => this.Load(DateTime.Today, DateTime.Today);
        public void Load(DateTime 시작, DateTime 종료)
        {
            this.Clear();
            this.RaiseListChangedEvents = false;
            List<검사결과> 자료 = this.테이블.Select(시작, 종료);

            //List<Int32> 대상 = Global.신호제어.검사중인항목();
            자료.ForEach(검사 =>
            {
                this.Add(검사);
                // 검사스플 생성
                //if (검사.측정결과 < 결과구분.ER && 대상.Contains(검사.검사코드) && !this.검사스플.ContainsKey(검사.검사코드))
                //    this.검사스플.Add(검사.검사코드, 검사);
            });
            this.RaiseListChangedEvents = true;
            this.ResetBindings();
        }

        public List<검사결과> GetData(DateTime 시작, DateTime 종료, 모델구분 모델) => this.테이블.Select(시작, 종료, 모델);
        private void 모델변경알림(모델구분 모델코드) => this.수동검사초기화();

        private void 자료추가(검사결과 결과)
        {
            this.Insert(0, 결과);
            //this.테이블.Add(결과);
        }

        public void 검사항목제거(List<검사정보> 자료) => this.테이블.Remove(자료);
        public Boolean 결과삭제(검사결과 정보)
        {
            this.Remove(정보);
            return this.테이블.Delete(정보) > 0;
        }
        public Boolean 결과삭제(검사결과 결과, 검사정보 정보)
        {
            결과.검사내역.Remove(정보);
            return this.테이블.Delete(정보) > 0;
        }
        public 검사결과 결과조회(DateTime 일자, 모델구분 모델, Int32 코드) => this.테이블.Select(일자, 모델, 코드);

        #region 검사로직
        public 검사결과 검사시작(Int32 검사코드)
        {
            검사결과 검사 = 검사항목찾기(검사코드, true);
            if (검사 == null)
            {
                검사 = new 검사결과() { 검사코드 = 검사코드 };
                검사.Reset();
                검사.측정결과 = 결과구분.IN; // 검사중으로 바꿈
                this.자료추가(검사);
                this.검사스플.Add(검사.검사코드, 검사);
                Common.DebugWriteLine(로그영역.GetString(), 로그구분.정보, $"[{(Int32)Global.환경설정.선택모델} - {검사.검사코드}] 신규검사 시작.");
            }
            //Common.DebugWriteLine(로그영역.GetString(), 로그구분.정보, $"[{(Int32)Global.환경설정.선택모델} - {검사.검사코드}] 검사코드 있음.");
            return 검사;
        }
        public 검사결과 항목검사(Flow구분 구분, 지그위치 지그, String name, Single value)
        {
            Int32 검사코드 = (Int32)구분 < 5 ? (Int32)구분 : (Int32)구분 - 5;

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

        public void ResetItem(검사결과 검사)
        {
            if (검사 == null) return;
            this.ResetItem(this.IndexOf(검사));
        }
        #endregion
    }


    public class 검사결과테이블 : Data.BaseTable
    {
        private TranslationAttribute 로그영역 = new TranslationAttribute("Inspection Data", "검사자료");
        private TranslationAttribute 삭제오류 = new TranslationAttribute("An error occurred while deleting data.", "자료 삭제중 오류가 발생하였습니다.");
        private DbSet<검사결과> 검사결과 { get; set; }
        private DbSet<검사정보> 검사정보 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<검사결과>().Property(e => e.모델구분).HasConversion(new EnumToNumberConverter<모델구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.CTQ결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.외관결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사정보>().HasKey(e => new { e.검사일시, e.검사항목 });
            modelBuilder.Entity<검사정보>().Property(e => e.검사그룹).HasConversion(new EnumToNumberConverter<검사그룹, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사항목).HasConversion(new EnumToNumberConverter<검사항목, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.검사장치).HasConversion(new EnumToNumberConverter<장치구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.결과분류).HasConversion(new EnumToNumberConverter<결과분류, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정단위).HasConversion(new EnumToNumberConverter<단위구분, Int32>());
            modelBuilder.Entity<검사정보>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            base.OnModelCreating(modelBuilder);
        }

        public void Save()
        {
            try { this.SaveChanges(); }
            catch (Exception ex) { Debug.WriteLine(ex.ToString(), "자료저장"); }
        }

        public void SaveAsync()
        {
            try {
                Common.DebugWriteLine("자료저장", 로그구분.정보, "테이블 SaveAsync");
                this.SaveChangesAsync();
            }
            catch (Exception ex) { Debug.WriteLine(ex.ToString(), "자료저장"); }
        }

        public void Add(검사결과 정보)
        {
            this.검사결과.Add(정보);
            this.검사정보.AddRange(정보.검사내역);
        }

        public void Remove(List<검사정보> 자료)
        {
            this.검사정보.RemoveRange(자료);
        }

        public List<검사결과> Select()
        {
            return this.Select(DateTime.Today);
        }
        public List<검사결과> Select(DateTime 날짜)
        {
            DateTime 시작 = new DateTime(날짜.Year, 날짜.Month, 날짜.Day);
            DateTime 종료 = new DateTime(날짜.Year, 날짜.Month, 날짜.Day + 1);
            return this.Select(시작, 종료);
        }
        public List<검사결과> Select(DateTime 시작, DateTime 종료, 모델구분 모델 = 모델구분.None, Int32 코드 = 0)
        {
            IQueryable<검사결과> query1 = (
                from l in 검사결과
                where l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1)
                where (코드 <= 0 || l.검사코드 == 코드)
                where (모델 == 모델구분.None || l.모델구분 == 모델)
                orderby l.검사일시 descending
                select l);
            List<검사결과> 자료 = query1.AsNoTracking().ToList();

            IQueryable<검사정보> query2 = (
                from d in 검사정보
                join l in 검사결과 on d.검사일시 equals l.검사일시
                where l.검사일시 >= 시작 && l.검사일시 < 종료.AddDays(1)
                where (코드 <= 0 || l.검사코드 == 코드)
                where (모델 == 모델구분.None || l.모델구분 == 모델)
                orderby d.검사일시 descending
                orderby d.검사항목 ascending
                select d);
            List<검사정보> 정보 = query2.AsNoTracking().ToList();

            자료.ForEach(l =>
            {
                l.AddRange(정보.Where(d => d.검사일시 == l.검사일시).ToList());
            });

            return 자료;
        }

        public 검사결과 Select(DateTime 일자, 모델구분 모델, Int32 코드)
        {
            return this.Select(일자, 일자, 모델, 코드).FirstOrDefault();
        }

        public Int32 Delete(검사결과 정보)
        {
            String Sql = $"DELETE FROM inspd WHERE idwdt = @idwdt;\nDELETE FROM inspl WHERE ilwdt = @ilwdt;";
            try
            {
                int AffectedRows = 0;
                using (NpgsqlCommand cmd = new NpgsqlCommand(Sql, this.DbConn))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@idwdt", 정보.검사일시));
                    cmd.Parameters.Add(new NpgsqlParameter("@ilwdt", 정보.검사일시));
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), Localization.삭제.GetString(), $"{삭제오류.GetString()}\r\n{ex.Message}", true);
            }
            return 0;
        }

        public Int32 Delete(검사정보 정보)
        {
            String Sql = $"DELETE FROM inspd WHERE idwdt = @idwdt AND idnum = @idnum";
            try
            {
                int AffectedRows = 0;
                using (NpgsqlCommand cmd = new NpgsqlCommand(Sql, this.DbConn))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("@idwdt", 정보.검사일시));
                    cmd.Parameters.Add(new NpgsqlParameter("@idnum", 정보.검사항목));
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), Localization.삭제.GetString(), $"{삭제오류.GetString()}\r\n{ex.Message}", true);
            }
            return 0;
        }

        public Int32 자료정리(Int32 일수)
        {
            DateTime 일자 = DateTime.Today.AddDays(-일수);
            String day = MvUtils.Utils.FormatDate(일자, "{0:yyyy-MM-dd}");
            String sql = $"DELETE FROM inspd WHERE idwdt < DATE('{day}');\nDELETE FROM inspl WHERE ilwdt < DATE('{day}');";
            try
            {
                int AffectedRows = 0;
                using (NpgsqlCommand cmd = new NpgsqlCommand(sql, this.DbConn))
                {
                    if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
                    AffectedRows = cmd.ExecuteNonQuery();
                    cmd.Connection.Close();
                }
                return AffectedRows;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Global.오류로그(로그영역.GetString(), "Remove Datas", ex.Message, false);
            }
            return -1;
        }
    }
}
