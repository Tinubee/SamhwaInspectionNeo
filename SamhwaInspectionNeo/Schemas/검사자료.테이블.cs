using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvUtils;
using Npgsql;
using SqlKata;
using SqlKata.Compilers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using SamhwaInspectionNeo.Schemas;
using SamhwaInspectionNeo;

namespace SamhwaInspectionNeo.Schemas
{
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

        public void Add(검사결과 정보)
        {
            this.검사결과.Add(정보);
            this.검사정보.AddRange(정보.검사내역);
        }

        public void Remove(List<검사정보> 자료) => this.검사정보.RemoveRange(자료);

        public Boolean Save(검사결과 정보)
        {
            this.Add(정보);
            return this.Save();
            //if (!Save()) return false;
            //Global.외관불량.Add(정보);
            //return true;
        }
        public Boolean Save()
        {
            try
            {
                Int32 changes = this.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                //Debug.WriteLine(ex.Message, "DbUpdateConcurrencyException");
                return true;
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "Save", ex.Message, true);
                MvUtils.Utils.DebugException(ex, 3);
                return false;
            }
        }

        public List<검사결과> Select(QueryPrms p)
        {
            DateTime s = DateTime.Now;
            SqlResult sql1 = p.CopyTo(Tables.inspl).QueryBuild();
            SqlResult sql2 = p.CopyTo(Tables.inspd).QueryBuild();
            List<검사결과> 자료 = 검사결과.FromSqlRaw(sql1.ToString()).ToList();
            List<검사정보> 정보 = 검사정보.FromSqlRaw(sql2.ToString()).ToList();
            Debug.WriteLine((DateTime.Now - s).TotalMilliseconds, "Load Time Sql");
            if (자료 == null || 자료.Count < 1) return new List<검사결과>();
            if (정보 != null && 정보.Count > 0)
                자료.ForEach(l => l.AddRange(정보.Where(d => d.검사일시 == l.검사일시).ToList()));
            return 자료;
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
                    cmd.Parameters.Add(new NpgsqlParameter("@dfwdt", 정보.검사일시));
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
            String sql = $"DELETE FROM inspd WHERE idwdt < DATE('{day}');DELETE FROM inspl WHERE ilwdt < DATE('{day}');";
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
        //public List<연속불량정보> 연속불량체크()
        //{
        //    List<연속불량정보> 불량정보 = new List<연속불량정보>();
        //    if (!Global.환경설정.연속오류사용 || Global.환경설정.연속오류횟수 <= 1) return 불량정보;
        //    String sql = $"SELECT * FROM continuous_ngs({Global.환경설정.연속오류횟수})";
        //    try
        //    {
        //        using (NpgsqlCommand cmd = new NpgsqlCommand(sql, this.DbConn))
        //        {
        //            if (cmd.Connection.State != ConnectionState.Open) cmd.Connection.Open();
        //            using (NpgsqlDataReader reader = cmd.ExecuteReader())
        //            {
        //                while (reader.Read())
        //                    불량정보.Add(new 연속불량정보(reader.GetInt32(0), reader.GetInt32(1)));
        //            }
        //            cmd.Connection.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Global.오류로그(로그영역.GetString(), "연속불량", $"{ex.Message}", true);
        //    }
        //    return 불량정보;
        //}
    }

    public class 검사목록테이블 : Data.BaseTable
    {
        private TranslationAttribute 로그영역 = new TranslationAttribute("Inspection List", "검사목록");
        private DbSet<검사결과> 검사목록 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<검사결과>().Property(e => e.모델구분).HasConversion(new EnumToNumberConverter<모델구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.측정결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.CTQ결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            modelBuilder.Entity<검사결과>().Property(e => e.외관결과).HasConversion(new EnumToNumberConverter<결과구분, Int32>());
            base.OnModelCreating(modelBuilder);
        }

        public List<검사결과> Select(QueryPrms p)
        {
            SqlResult sql1 = p.CopyTo(Tables.inspl).QueryBuild();
            return 검사목록.FromSqlRaw(sql1.ToString()).AsNoTracking().ToList();
            //SELECT * FROM "inspl" WHERE "ilwdt" BETWEEN '2025-02-17' AND '2025-02-17' ORDER BY "ilwdt"
        }
    }


    public class 연속불량정보
    {
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        public Int32 불량갯수 { get; set; } = 0;

        public 연속불량정보() { }
        public 연속불량정보(Int32 항목, Int32 갯수) : this((검사항목)항목, 갯수) { }
        public 연속불량정보(검사항목 항목, Int32 갯수)
        {
            this.검사항목 = 항목;
            this.불량갯수 = 갯수;
        }
    }

    public enum Tables { inspl, inspd }
    public class QueryPrms
    {
        public Tables 테이블 { get; set; } = Tables.inspl;
        public Boolean 역순정렬 { get; set; } = false;
        public Boolean 조인여부 => 테이블 != Tables.inspl && (코드 > 0); //모델 != 모델구분.None || 

        [Description("ilwdt")] public DateTime 시작 { get; set; } = DateTime.Today;
        [Description("ilwdt")] public DateTime 종료 { get; set; } = DateTime.Today;
        [Description("ilmcd")] public 모델구분 모델 { get; set; } = 모델구분.None;
        [Description("ilnum")] public Int32 코드 { get; set; } = 0;

        public QueryPrms CopyTo(Tables table)
        {
            QueryPrms q = Clone();
            q.테이블 = table;
            return q;
        }
        public QueryPrms Clone() => Clone(this);
        public static QueryPrms Clone(QueryPrms prm)
        {
            QueryPrms q = new QueryPrms();
            foreach (PropertyInfo p in typeof(QueryPrms).GetProperties())
            {
                if (!p.CanWrite) continue;
                p.SetValue(q, p.GetValue(prm));
            }
            return q;
        }

        public SqlResult QueryBuild() => QueryBuild(this);
        public static SqlResult QueryBuild(QueryPrms p)
        {
            Query que = new Query(p.테이블.ToString());
            String[] orders;
            if (p.테이블 == Tables.inspd)
            {
                que.WhereBetween("idwdt", p.시작, p.종료);
                if (p.조인여부) que.Join(Tables.inspl.ToString(), "ilwdt", "idwdt");
                orders = new[] { "idwdt" };
            }
            else
            {
                que.WhereBetween("ilwdt", p.시작, p.종료);
                orders = new[] { "ilwdt" };
            }
            //if (p.모델 != 모델구분.None) que.Where("ilmcd", (Int32)p.모델);
            if (p.코드 > 0) que.Where("ilnum", p.코드);
            //if (!String.IsNullOrEmpty(p.큐알)) que.WhereContains("ilqrs", p.큐알);
            //if (!String.IsNullOrEmpty(p.큐알)) que.Where("ilqrs", p.큐알);
            //if (!String.IsNullOrEmpty(p.시리얼)) que.Where("ilser", p.시리얼);
            if (p.역순정렬) que.OrderByDesc(orders);
            else que.OrderBy(orders);
            if (p.테이블 == Tables.inspd) que.OrderBy(new[] { "iditm" });
            return new PostgresCompiler().Compile(que);
        }
    }
}
