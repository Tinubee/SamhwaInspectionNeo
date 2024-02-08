using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace SamhwaInspectionNeo.Schemas
{
    public class CameraAttribute : Attribute
    {
        public Boolean Whether = true;
        public CameraAttribute(Boolean cam) { Whether = cam; }

        public static Boolean IsCamera(장치구분 구분)
        {
            CameraAttribute a = MvUtils.Utils.GetAttribute<CameraAttribute>(구분);
            if (a == null) return false;
            return a.Whether;
        }
    }
    public enum 장치구분
    {
        [Description("None"), DXDescription("NO"), Camera(false)]
        None = 0,
        [Description("Cam01"), DXDescription("C1"), Camera(true)]
        Cam01 = 1,
        [Description("Cam02"), DXDescription("C2"), Camera(true)]
        Cam02 = 2,
        [Description("Cam03"), DXDescription("C3"), Camera(true)]
        Cam03 = 3,
        [Description("Cam04"), DXDescription("C4"), Camera(true)]
        Cam04 = 4,
    }

    public enum 결과분류
    {
        None,
        Summary,
        Detail,
    }

    public class ResultAttribute : Attribute
    {
        public 검사그룹 검사그룹 = 검사그룹.None;
        public 결과분류 결과분류 = 결과분류.None;
        public 장치구분 장치구분 = 장치구분.None;
        public ResultAttribute() { }
        public ResultAttribute(검사그룹 그룹, 결과분류 결과, 장치구분 장치) { 검사그룹 = 그룹; 장치구분 = 장치; 결과분류 = 결과; }
    }

    public enum 검사그룹
    {
        [Description("None"), Translation("None", "없음")]
        None,
        [Description("CTQ"), Translation("CTQ")]
        CTQ,
        [Description("Surface"), Translation("Surface", "외관검사")]
        Surface,
        [Description("TrayCheck"), Translation("TrayCheck", "트레이검사")]
        TrayCheck,
    }

    public enum 검사항목 : Int32
    {
        [Result(), ListBindable(false)]
        None = 0,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1상부 = 1,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1중앙부 = 2,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1하부 = 3,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1위치도 = 4,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2상부 = 5,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2중앙부 = 6,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2하부 = 7,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2위치도 = 8,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3상부 = 9,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3중앙부 = 10,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3하부 = 11,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3위치도 = 12,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4상부 = 13,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4중앙부 = 14,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4하부 = 15,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4위치도 = 16,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        기준홀경 = 17,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측홀경 = 18,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상홀경 = 19,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하홀경 = 20,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙홀경 = 21,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상홀경 = 22,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하홀경 = 23,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측가로거리 = 24,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상가로거리 = 25,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하가로거리 = 26,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙가로거리 = 27,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상가로거리 = 28,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하가로거리 = 29,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측세로거리 = 30,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상세로거리 = 31,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하세로거리 = 32,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙세로거리 = 33,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상세로거리 = 34,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하세로거리 = 35,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측위치도 = 36,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상위치도 = 37,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하위치도 = 38,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙위치도 = 39,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상위치도 = 40,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하위치도 = 41,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot길이 = 42,
        [Result(검사그룹.Surface, 결과분류.Summary, 장치구분.Cam03)]
        상부표면검사 = 500,
    }

    public enum 단위구분
    {
        [Description("mm")]
        mm = 0,
        [Description("ø")]
        ø = 1,
        [Description("µm")]
        µm = 2,
        [Description("px")]
        px = 3,
    }

    public enum 결과구분
    {
        [Description("Waiting"), Translation("Waiting", "대기중")]
        NO = 0,
        [Description("Measuring"), Translation("Measuring", "검사중")]
        IN = 1,
        [Description("PS"), Translation("Pass", "통과")]
        PS = 2,
        [Description("ER"), Translation("Error", "오류")]
        ER = 3,
        [Description("NG"), Translation("NG", "불량")]
        NG = 5,
        [Description("OK"), Translation("OK", "양품")]
        OK = 7,
    }

    [Table("inspd")]
    public class 검사정보
    {
        [Column("idwdt", Order = 0), Required, Key, JsonProperty("idwdt"), Translation("Time", "검사일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("iditm", Order = 1), Required, Key, JsonProperty("iditm"), Translation("Item", "검사항목")]
        public 검사항목 검사항목 { get; set; } = 검사항목.None;
        [Column("idgrp"), JsonProperty("idgrp"), Translation("Group", "검사그룹")]
        public 검사그룹 검사그룹 { get; set; } = 검사그룹.None;
        [Column("iddev"), JsonProperty("iddev"), Translation("Device", "검사장치")]
        public 장치구분 검사장치 { get; set; } = 장치구분.None;
        [Column("idflow"), JsonProperty("idflow"), Translation("Flow", "플로우")]
        public Flow구분 플로우 { get; set; } = Flow구분.Flow1;
        [Column("idjig"), JsonProperty("idjig"), Translation("JIG", "지그위치")]
        public 지그위치 지그 { get; set; } = 지그위치.Front;
        [Column("idcat"), JsonProperty("idcat"), Translation("Category", "결과유형")]
        public 결과분류 결과분류 { get; set; } = 결과분류.None;
        [Column("iduni"), JsonProperty("iduni"), Translation("Unit", "단위"), BatchEdit(true)]
        public 단위구분 측정단위 { get; set; } = 단위구분.mm;
        [Column("idstd"), JsonProperty("idstd"), Translation("Standard", "기준값"), BatchEdit(true)]
        public Decimal 기준값 { get; set; } = 0m;
        [Column("idmin"), JsonProperty("idmin"), Translation("Min", "최소값"), BatchEdit(true)]
        public Decimal 최소값 { get; set; } = 0m;
        [Column("idmax"), JsonProperty("idmax"), Translation("Max", "최대값"), BatchEdit(true)]
        public Decimal 최대값 { get; set; } = 0m;
        [Column("idoff"), JsonProperty("idoff"), Translation("Offset", "보정값"), BatchEdit(true)]
        public Decimal 보정값 { get; set; } = 0m;
        [Column("idcal"), JsonProperty("idcal"), Translation("Calib(µm)", "교정(µm)")]
        public Decimal 교정값 { get; set; } = 1m;
        [Column("idmes"), JsonProperty("idmes"), Translation("Measure", "측정값")]
        public Decimal 측정값 { get; set; } = 0m;
        [Column("idmaster"), JsonProperty("idmaster"), Translation("Master", "마스터값"), BatchEdit(true)]
        public Decimal 마스터값 { get; set; } = 0m;
        [Column("idmastertol"), JsonProperty("idmastertol"), Translation("MasterTol", "마스터공차"), BatchEdit(true)]
        public Decimal 마스터공차 { get; set; } = 0m;
        [Column("idval"), JsonProperty("idval"), Translation("Value", "결과값")]
        public Decimal 결과값 { get; set; } = 0m;
        [Column("idres"), JsonProperty("idres"), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.NO;
        [NotMapped, JsonIgnore]
        public Double 검사시간 = 0;

        public 검사정보() { }
        public 검사정보(검사정보 정보) { this.Set(정보); }

        public void Reset(DateTime? 일시 = null)
        {
            if (일시 != null) this.검사일시 = (DateTime)일시;
            this.측정값 = 0;
            this.결과값 = 0;
            this.측정결과 = 결과구분.IN;
        }
        public void Set(검사정보 정보)
        {
            if (정보 == null) return;
            foreach (PropertyInfo p in typeof(검사정보).GetProperties())
            {
                if (!p.CanWrite) continue;
                Object v = p.GetValue(정보);
                if (v == null) continue;
                p.SetValue(this, v);
            }

            this.측정값 = 0;
            this.결과값 = 0;
            this.측정결과 = 결과구분.NO;
        }
    }

    [Table("inspl")]
    public class 검사결과
    {
        [Column("ilwdt"), Required, Key, JsonProperty("ilwdt"), Translation("Time", "일시")]
        public DateTime 검사일시 { get; set; } = DateTime.Now;
        [Column("ilmcd"), JsonProperty("ilmcd"), Translation("Model", "모델")]
        public 모델구분 모델구분 { get; set; } = 모델구분.None;
        [Column("ilnum"), JsonProperty("ilnum"), Translation("Index", "번호")]
        public Int32 검사코드 { get; set; } = 0; //Flow로 검사코드설정. Master모드시+100
        [Column("ilres"), JsonProperty("ilres"), Translation("Result", "판정")]
        public 결과구분 측정결과 { get; set; } = 결과구분.NO;
        [Column("ilctq"), JsonProperty("ilctq"), Translation("CTQ", "CTQ결과")]
        public 결과구분 CTQ결과 { get; set; } = 결과구분.NO;
        [Column("ilsuf"), JsonProperty("ilapp"), Translation("Suface", "외관결과")]
        public 결과구분 외관결과 { get; set; } = 결과구분.NO;
        [Column("ilngs"), JsonProperty("ilngs"), Translation("NG Info.", "불량정보")]
        public String 불량정보 { get; set; } = String.Empty;
        [NotMapped, JsonProperty("inspd")]
        public List<검사정보> 검사내역 { get; set; } = new List<검사정보>();
        [NotMapped, JsonIgnore]
        private const string 로그영역 = "검사정보";
        public 검사결과()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
        }

        public void Reset()
        {
            this.검사일시 = DateTime.Now;
            this.모델구분 = Global.환경설정.선택모델;
            this.측정결과 = 결과구분.NO;
            this.CTQ결과 = 결과구분.NO;
            this.외관결과 = 결과구분.NO;
            this.불량정보 = String.Empty;
            this.검사내역.Clear();

            검사설정자료 자료 = Global.모델자료.GetItem(this.모델구분)?.검사설정;

            foreach (검사정보 정보 in 자료)
                this.검사내역.Add(new 검사정보(정보) { 검사일시 = this.검사일시 });
        }

        public 검사정보 GetItem(Flow구분 플로우, 지그위치 지그) => 검사내역.Where(e => e.플로우 == 플로우 && e.지그 == 지그).FirstOrDefault();

        public 검사정보 GetItem(검사항목 항목) => 검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault();
        public Boolean SetResult(Flow구분 구분, 지그위치 지그, String name, Single value) => SetResult(검사내역.Where(e => e.검사항목.ToString() == name).FirstOrDefault(), value, 구분, 지그);
        public Boolean SetResult(검사정보 검사, Single value, Flow구분 구분, 지그위치 지그)
        {
            if (검사 == null) return false;
            if (Single.IsNaN(value))
            {
                검사.측정결과 = 결과구분.ER;
                return false;
            }
            Boolean isOk = false;
            검사.플로우 = 구분;
            검사.지그 = 지그;
            검사.결과값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            검사.측정값 = 검사.결과값 + 검사.보정값;
            if (Global.신호제어.마스터모드여부) isOk = 검사.결과값 >= 검사.마스터값 - 검사.마스터공차 && 검사.결과값 <= 검사.마스터값 + 검사.마스터공차;
            else isOk = 검사.결과값 >= 검사.최소값 && 검사.결과값 <= 검사.최대값;

            검사.측정결과 = isOk ? 결과구분.OK : 결과구분.NG;

            return true;
        }

        public void AddRange(List<검사정보> 자료) => this.검사내역.AddRange(자료);

        public Boolean 검사중확인()
        {
            if (Global.환경설정.동작구분 == 동작구분.LocalTest) return false;

            if (this.검사내역.Any(e => e.측정결과 == 결과구분.NO || e.측정결과 == 결과구분.IN))
            {
                return true;
            }
            else return false;
        }

        public 결과구분 결과계산()
        {
            if (this.검사내역.Any(e => e.측정결과 == 결과구분.ER)) this.측정결과 = 결과구분.ER;
            else if (this.검사내역.Any(e => e.측정결과 != 결과구분.PS && e.측정결과 != 결과구분.OK)) this.측정결과 = 결과구분.NG;
            else this.측정결과 = 결과구분.OK;
            if (this.측정결과 == 결과구분.OK)
            {
                this.CTQ결과 = 결과구분.OK;
                this.외관결과 = 결과구분.OK;
            }
            else
            {
                if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.CTQ && e.측정결과 == 결과구분.ER)) this.CTQ결과 = 결과구분.ER;
                else if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.CTQ && e.측정결과 == 결과구분.NG)) this.CTQ결과 = 결과구분.NG;
                else this.CTQ결과 = 결과구분.OK;
                //임시
                // this.외관결과 = 결과구분.OK;
                if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.Surface && e.측정결과 == 결과구분.ER)) this.외관결과 = 결과구분.ER;
                else if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.Surface && e.측정결과 == 결과구분.NG)) this.외관결과 = 결과구분.NG;
                else this.외관결과 = 결과구분.OK;
            }

            List<String> 불량내역 = this.검사내역.Where(e => e.측정결과 == 결과구분.ER || e.측정결과 == 결과구분.NG).Select(e => e.검사항목.ToString()).ToList();
            if (불량내역.Count > 0) this.불량정보 = String.Join(",", 불량내역);

            Global.정보로그(로그영역, "결과계산", $"[{(Int32)Global.환경설정.선택모델} - {this.검사코드}] : ${this.측정결과}", false);
            return this.측정결과;
        }
    }
}
