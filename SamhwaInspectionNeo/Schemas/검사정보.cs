using MvUtils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

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
        [Description("SURFACE"), Translation("SURFACE", "외관검사")]
        SURFACE,
        [Description("TRAYCHECK"), Translation("TRAYCHECK", "트레이검사")]
        TRAYCHECK,
        [Description("BUR"), Translation("BUR", "BUR검사")]
        BUR,
    }

    public enum 검사항목 : Int32
    {
        [Result(), ListBindable(false)]
        None = 0,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_1 = 101,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_2 = 102,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_3 = 103,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_4 = 104,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_5 = 105,
        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam01)]
        Slot1_Bur = 120,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot1위치도 = 121,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_1 = 201,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_2 = 202,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_3 = 203,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_4 = 204,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_5 = 205,
        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam01)]
        Slot2_Bur = 220,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot2위치도 = 221,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_1 = 301,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_2 = 302,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_3 = 303,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_4 = 304,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_5 = 305,
        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam01)]
        Slot3_Bur = 320,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot3위치도 = 321,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_1 = 401,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_2 = 402,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_3 = 403,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_4 = 404,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_5 = 405,
        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam01)]
        Slot4_Bur = 420,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        Slot4위치도 = 421,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        기준홀경 = 1001,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측홀경 = 1101,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측위치도X거리 = 1102,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측위치도Y거리 = 1103,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        상측위치도 = 1104,


        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상홀경 = 1201,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상위치도X거리 = 1202,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상위치도Y거리 = 1203,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상위치도 = 1204,
        //[Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        //좌하홀경 = 1301,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙홀경 = 1401,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙위치도X거리 = 1402,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙위치도Y거리 = 1403,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중앙위치도 = 1404,

        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상홀경 = 1501,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상위치도X거리 = 1502,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상위치도Y거리 = 1503,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상위치도 = 1504,

        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하홀경 = 1601,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하위치도X거리 = 1602,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하위치도Y거리 = 1603,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하위치도 = 1604,

        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상작은홀경 = 1701,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중상작은홀경 = 1702,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상작은홀경 = 1703,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하작은홀경 = 1704,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중하작은홀경 = 1705,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하작은홀경 = 1706,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌상작은홀경Burr = 1711,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중상작은홀경Burr = 1712,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우상작은홀경Burr = 1713,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        좌하작은홀경Burr = 1714,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        중하작은홀경Burr = 1715,
        [Result(검사그룹.CTQ, 결과분류.Summary, 장치구분.Cam01)]
        우하작은홀경Burr = 1716,

        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam03)]
        상부표면검사 = 5000,
        [Result(검사그룹.SURFACE, 결과분류.Summary, 장치구분.Cam04)]
        하부표면검사 = 5100,
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
        //[Column("idmar"),JsonProperty("idmar"), Translation("Margin(µm)", "마진(µm)")]
        //public Decimal 마진값 { get; set; } = 1m;
        [Column("idmes"), JsonProperty("idmes"), Translation("Measure", "측정값")]
        public Decimal 측정값 { get; set; } = 0m;
        //[Column("idcmm"), JsonProperty("idcmm"), Translation("CMMData", "CMM측정값")]
        //public Decimal CMM측정값 { get; set; } = 0m;
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

        public 결과구분 결과계산()
        {
            Boolean ok = this.결과값 >= this.최소값 && this.결과값 <= this.최대값;
            this.측정결과 = ok ? 결과구분.OK : 결과구분.NG;
            return this.측정결과;
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
            this.검사일시 = DateTime.Now.AddMilliseconds(this.검사코드 * 10);
            if (this.검사코드 < 6)
                Global.VM제어.GetItem((Flow구분)this.검사코드).검사시간 = this.검사일시;
            //Debug.WriteLine($"{this.검사일시.ToString("HH:mm:ss.ffffff")}");
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

        public Boolean 표면검사강제OK(Flow구분 구분, 지그위치 지그) => SetResult(검사내역.Where(e => e.검사항목.ToString() == "상부표면검사").FirstOrDefault(), 0, 구분, 지그);

        public Boolean 하부표면검사강제OK(Flow구분 구분, 지그위치 지그) => SetResult(검사내역.Where(e => e.검사항목.ToString() == "하부표면검사").FirstOrDefault(), 0, 구분, 지그);
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
            else
            {
                if (검사.검사항목.ToString().Contains("위치도"))
                    MMC공차적용(검사);

                isOk = 검사.결과값 >= 검사.최소값 && 검사.결과값 <= 검사.최대값;
            }

            검사.측정결과 = isOk ? 결과구분.OK : 결과구분.NG;

            return true;
        }

        public void MMC공차적용(검사정보 검사)
        {
            if (검사.검사항목.ToString().Contains("거리") == false && 검사.검사항목.ToString().Contains("Slot") == false)
            {
                String 홀이름 = 검사.검사항목.ToString().Substring(0, 2);
                검사정보 정보 = this.검사내역.Where(e => e.검사항목.ToString() == $"{홀이름}홀경").FirstOrDefault();
                //Debug.WriteLine($"{홀이름} : {정보.결과값 - 정보.최소값}");

                Decimal MMC공차 = 정보.결과값 - 정보.최소값 < 0 ? 0 : 정보.결과값 - 정보.최소값;
                //Common.DebugWriteLine(로그영역, 로그구분.정보, $"{검사.검사항목} MMC 공차 : {MMC공차}");
                검사.최대값 += MMC공차;
            }
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
            else if (this.검사내역.Any(e => e.측정결과 == 결과구분.NO || e.측정결과 == 결과구분.IN)) this.측정결과 = 결과구분.IN;
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
                else if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.CTQ && e.측정결과 == 결과구분.NO)) this.CTQ결과 = 결과구분.NO;
                else this.CTQ결과 = 결과구분.OK;

                if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.SURFACE && e.측정결과 == 결과구분.ER)) this.외관결과 = 결과구분.ER;
                else if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.SURFACE && e.측정결과 == 결과구분.NG)) this.외관결과 = 결과구분.NG;
                else if (this.검사내역.Any(e => e.검사그룹 == 검사그룹.SURFACE && e.측정결과 == 결과구분.NO)) this.외관결과 = 결과구분.NO;
                else this.외관결과 = 결과구분.OK;
            }

            if (this.측정결과 == 결과구분.NG)
            {
                List<String> 불량내역 = this.검사내역.Where(e => e.측정결과 == 결과구분.NG).Select(e => e.검사항목.ToString().Replace("_", "-")).ToList();
                //List<String> 불량내역 = this.검사내역.Where(e => e.측정결과 == 결과구분.ER || e.측정결과 == 결과구분.NG).Select(e => e.검사그룹.ToString()).Distinct().ToList();
                Common.DebugWriteLine(로그영역, 로그구분.정보, $"{불량정보}");
                if (불량내역.Count > 0) this.불량정보 = String.Join(",", 불량내역);
            }

            return this.측정결과;
        }
    }
}
