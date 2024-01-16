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

    // 카메라구분 과 번호 맞춤
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
        public 검사항목 결과항목 = 검사항목.None;
        public Flow구분 플로우구분 = Flow구분.Flow1;
        public String 변수명칭 = String.Empty;
        public Int32 결과부호 = 1;
        public ResultAttribute() { }
        public ResultAttribute(검사그룹 그룹, 결과분류 결과, 장치구분 장치) { 검사그룹 = 그룹; 장치구분 = 장치; 결과분류 = 결과; }
        public ResultAttribute(검사그룹 그룹, 결과분류 결과, 장치구분 장치, Flow구분 플로우, 검사항목 항목, String 변수) { 검사그룹 = 그룹; 결과분류 = 결과; 장치구분 = 장치; 플로우구분 = 플로우; 결과항목 = 항목; 변수명칭 = 변수; }
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
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot1Top")]
        Flow1_Slot1상부 = 101,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot1Middle")]
        Flow1_Slot1중앙부 = 102,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot1Bottom")]
        Flow1_Slot1하부 = 103,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot2Top")]
        Flow1_Slot2상부 = 104,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot2Middle")]
        Flow1_Slot2중앙부 = 105,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "Slot2Bottom")]
        Flow1_Slot2하부 = 106,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "StandardHole")]
        Flow1_기준홀경 = 107,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "TopHole")]
        Flow1_상측홀경 = 108,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "LeftBottomHole")]
        Flow1_좌하홀경 = 109,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "LeftTopHole")]
        Flow1_좌상홀경 = 110,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow1, None, "RightTopHole")]
        Flow1_우상홀경 = 111,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot1Top")]
        Flow2_Slot1상부 = 201,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot1Middle")]
        Flow2_Slot1중앙부 = 202,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot1Bottom")]
        Flow2_Slot1하부 = 203,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot2Top")]
        Flow2_Slot2상부 = 204,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot2Middle")]
        Flow2_Slot2중앙부 = 205,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "Slot2Bottom")]
        Flow2_Slot2하부 = 206,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "StandardHole")]
        Flow2_기준홀경 = 207,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "TopHole")]
        Flow2_상측홀경 = 208,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "LeftBottomHole")]
        Flow2_좌하홀경 = 209,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "LeftTopHole")]
        Flow2_좌상홀경 = 210,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow2, None, "RightTopHole")]
        Flow2_우상홀경 = 211,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot1Top")]
        Flow3_Slot1상부 = 301,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot1Middle")]
        Flow3_Slot1중앙부 = 302,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot1Bottom")]
        Flow3_Slot1하부 = 303,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot2Top")]
        Flow3_Slot2상부 = 304,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot2Middle")]
        Flow3_Slot2중앙부 = 305,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "Slot2Bottom")]
        Flow3_Slot2하부 = 306,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "StandardHole")]
        Flow3_기준홀경 = 307,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "TopHole")]
        Flow3_상측홀경 = 308,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "LeftBottomHole")]
        Flow3_좌하홀경 = 309,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "LeftTopHole")]
        Flow3_좌상홀경 = 310,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow3, None, "RightTopHole")]
        Flow3_우상홀경 = 311,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot1Top")]
        Flow4_Slot1상부 = 401,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot1Middle")]
        Flow4_Slot1중앙부 = 402,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot1Bottom")]
        Flow4_Slot1하부 = 403,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot2Top")]
        Flow4_Slot2상부 = 404,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot2Middle")]
        Flow4_Slot2중앙부 = 405,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "Slot2Bottom")]
        Flow4_Slot2하부 = 406,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "StandardHole")]
        Flow4_기준홀경 = 407,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "TopHole")]
        Flow4_상측홀경 = 408,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "LeftBottomHole")]
        Flow4_좌하홀경 = 409,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "LeftTopHole")]
        Flow4_좌상홀경 = 410,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01, Flow구분.Flow4, None, "RightTopHole")]
        Flow4_우상홀경 = 411,
        [Result(검사그룹.Surface, 결과분류.Summary, 장치구분.Cam03)]
        TopSurface = 500,
        [Result(검사그룹.Surface, 결과분류.Summary, 장치구분.Cam04)]
        BottomSurface = 501,
        [Result(검사그룹.TrayCheck, 결과분류.Summary, 장치구분.Cam02)]
        TrayCheck = 502,
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

    public enum 큐알등급
    {
        [Description("-")]
        X = 0,
        [Description("A")]
        A = 1,
        [Description("B")]
        B = 2,
        [Description("C")]
        C = 3,
        [Description("D")]
        D = 4,
        [Description("E")]
        E = 5,
        [Description("F")]
        F = 6,
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
        public Int32 검사코드 { get; set; } = 0;
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
            {
                this.검사내역.Add(new 검사정보(정보) { 검사일시 = this.검사일시 });
            }
        }

        public 검사정보 GetItem(검사항목 항목) => 검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault();

        // 카메라 검사결과 적용
        public Boolean SetResult(String name, Single value, Boolean ok) => SetResult(검사내역.Where(e => e.검사항목.ToString() == name).FirstOrDefault(), value, ok);
        public Boolean SetResult(검사항목 항목, Single value, Boolean ok) => SetResult(검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault(), value, ok);
        public Boolean SetResult(검사정보 검사, Single value, Boolean ok)
        {
            if (검사 == null) return false;
            if (Single.IsNaN(value))
            {
                검사.측정결과 = 결과구분.ER;
                return false;
            }

            검사.결과값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            검사.측정값 = 검사.결과값;
            검사.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            //Debug.WriteLine(검사.측정값, 검사.검사항목.ToString());
            return true;
        }

        // 일반 검사결과 적용
        public Boolean SetResult(검사항목 항목, Single value) => SetResult(검사내역.Where(e => e.검사항목 == 항목).FirstOrDefault(), value);
        public Boolean SetResult(String name, Single value) => SetResult(검사내역.Where(e => e.검사항목.ToString() == name).FirstOrDefault(), value);
        public Boolean SetResult(검사정보 검사, Single value)
        {
            if (검사 == null) return false;
            if (Single.IsNaN(value))
            {
                검사.측정결과 = 결과구분.ER;
                return false;
            }
            검사.측정값 = (Decimal)Math.Round(value, Global.환경설정.결과자릿수);
            검사.결과값 = 검사.측정값 + 검사.보정값;
            Boolean ok = 검사.결과값 >= 검사.최소값 && 검사.결과값 <= 검사.최대값;
            검사.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            return true;
        }

        public void AddRange(List<검사정보> 자료)
        {
            this.검사내역.AddRange(자료);
        }

        public 결과구분 결과계산()
        {
            // 임시
            //this.SetResult(검사항목.찍힘불량, 0);
            //this.SetResult(검사항목.스크레치, 0);

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
                else this.CTQ결과 = 결과구분.NG;
                if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.Surface && e.측정결과 == 결과구분.ER)) this.외관결과 = 결과구분.ER;
                else this.외관결과 = 결과구분.NG;
            }
            Debug.WriteLine($"{this.검사코드} = {this.측정결과}", "검사완료");
            return this.측정결과;
        }
    }
}
