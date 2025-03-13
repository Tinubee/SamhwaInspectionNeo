using DevExpress.Utils;
using DevExpress.XtraGrid.Views.BandedGrid;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Diagnostics;

namespace SamhwaInspectionNeo.Schemas
{
    public class 피벗자료 : List<피벗정보>
    {
        public void Load(List<검사결과> 결과, Boolean useSummary)
        {
            foreach (검사결과 정보 in 결과)
                this.Add(new 피벗정보(정보));
        }

        public class 집계정보
        {
            public String 시리얼 { get; set; } = String.Empty;
            public 검사항목 항목 { get; set; } = 검사항목.None;
            public Decimal 측정값 { get; set; } = 0;
            public Decimal 최소값 { get; set; } = 0;
            public Decimal 최대값 { set; get; } = 0;
            public Decimal 평균값 { get; set; } = 0;
            public Decimal 편차값 => 최대값 - 최소값;
            public String 타이틀 => $"{시리얼} - {MvUtils.Utils.GetDescription(항목)}";
        }

        public class 컬럼정보
        {
            public 검사항목 항목 = 검사항목.None;
            public 단위구분 단위 = 단위구분.mm;

            public String FieldName = String.Empty;
            public String Caption = String.Empty;
            public String BandName = "Item";
            public FormatType FormatType = FormatType.None;
            public String FormatString = String.Empty;
            public Boolean Visible = true;
            public HorzAlignment Alignment = HorzAlignment.Default;
            public Decimal 최대 = 0;
            public Decimal 최소 = 0;
            public Decimal 기준 = 0;

            public 컬럼정보(PropertyInfo p)
            {
                if (p == null) return;
                FieldName = p.Name;
                Caption = MvUtils.Utils.GetDescription(p);
                if (p.PropertyType != typeof(Decimal?))
                {
                    Alignment = HorzAlignment.Center;
                    if (p.Name == nameof(피벗정보.검사일시))
                    {
                        FormatType = FormatType.DateTime;
                        FormatString = "{0:yyyy-MM-dd HH:mm:ss}";
                    }
                    else if (p.Name == nameof(피벗정보.검사코드))
                    {
                        FormatType = FormatType.Numeric;
                        FormatString = "{0:d4}";
                    }
                    return;
                }
                if (!Enum.TryParse(p.Name, out 검사항목 검사)) return;

                FormatType = FormatType.Numeric;
                FormatString = Global.환경설정.결과표현;
                항목 = 검사;
                검사정보 정보 = Global.모델자료.선택모델.검사설정.GetItem(항목);
                if (정보 == null) return;
                BandName = 정보.검사그룹.ToString(); //항목.ToString();//Global.분류자료.GetItem(정보.검사분류)?.명칭;
                //Visible = 정보.검사여부;
                최대 = 정보.최대값;
                최소 = 정보.최소값;
                기준 = 정보.기준값;
            }

            public void Set(BandedGridColumn gCol) => Set(gCol as GridColumn);
            public void Set(GridColumn gCol)
            {
                gCol.FieldName = FieldName;
                gCol.Tag = this;
                gCol.Caption = this.Caption;
                gCol.AppearanceHeader.TextOptions.HAlignment = HorzAlignment.Center;
                gCol.AppearanceCell.TextOptions.HAlignment = this.Alignment;
                gCol.DisplayFormat.FormatType = this.FormatType;
                gCol.DisplayFormat.FormatString = this.FormatString;
                gCol.Visible = this.Visible;
            }
            public static 컬럼정보 Get(BandedGridColumn gCol) => gCol?.Tag as 컬럼정보;
            public static 컬럼정보 Get(GridColumn gCol) => gCol?.Tag as 컬럼정보;
        }
    }

    public class 피벗통계 : 피벗정보
    {
        private List<피벗정보> 검사내역 = null;

        public 피벗통계() : this(집계항목.CMM측정) { }
        public 피벗통계(집계항목 집계)
        {
            this.집계 = 집계;
            집계구분 = MvUtils.Utils.GetDescription(this.집계);
        }

        private 피벗통계 통계 = null;
        public 피벗통계(피벗통계 통계, 집계항목 집계) : this(집계) => this.통계 = 통계;

        public List<피벗정보> Load(List<검사결과> 자료)
        {
            List<피벗정보> 통계자료 = new List<피벗정보>();
            //String qr = 자료.First().큐알내용;
            //큐알내용 = 큐알검증.GetSerial(qr);
            검사내역 = new List<피벗정보>();
            foreach (검사결과 결과 in 자료)
                검사내역.Add(new 피벗정보(결과));

            통계자료.AddRange(검사내역);
            //통계자료.Add(new 피벗통계(this, 집계항목.반복최대) { 큐알내용 = 큐알내용 });
            //통계자료.Add(new 피벗통계(this, 집계항목.반복최소) { 큐알내용 = 큐알내용 });
            //통계자료.Add(new 피벗통계(this, 집계항목.반복평균) { 큐알내용 = 큐알내용 });
            //통계자료.Add(new 피벗통계(this, 집계항목.반복표준) { 큐알내용 = 큐알내용 });
            //통계자료.Add(new 피벗통계(this, 집계항목.반복편차) { 큐알내용 = 큐알내용 });

            //측정자료 cmm = Global.측정내역.GetItem(qr);
            //if (cmm != null)
            //{
            //    foreach (측정정보 r in cmm.측정내역) this.V(r.항목, r.실측);
            //    통계자료.Insert(통계자료.Count - 5, this);
            //    통계자료.Add(new 피벗통계(this, 집계항목.정합편차) { 큐알내용 = 큐알내용 });
            //    통계자료.Add(new 피벗통계(this, 집계항목.정합평균) { 큐알내용 = 큐알내용 });
            //}
            return 통계자료;
        }

        public void Clear() => 검사내역?.Clear();

        internal virtual List<Decimal> Values(검사항목 검사)
        {
            List<Decimal> values = new List<Decimal>();
            PropertyInfo p = typeof(피벗정보).GetProperty(검사.ToString());
            if (p == null) return values;
            foreach (var r in 통계.검사내역)
            {
                Object v = p.GetValue(r);
                if (v == null) continue;
                values.Add((Decimal)v);
            }
            return values;
        }

        private Decimal? Sclar(검사항목 검사)
        {
            if (this.결과값.ContainsKey(검사)) return this.결과값[검사];
            return null;
        }
        internal override Decimal? V(검사항목 검사)
        {
            if (집계 == 집계항목.None) return null;
            if (집계 == 집계항목.CMM측정) return Sclar(검사);
            List<Decimal> values = Values(검사);
            if (values == null || values.Count < 1) return null;
            if (집계 == 집계항목.반복최대) return values.Max();
            if (집계 == 집계항목.반복최소) return values.Min();
            if (집계 == 집계항목.반복평균) return Math.Round(values.Average(), 3);
            //if (집계 == 집계항목.반복표준) return (Decimal)Math.Round(Common.StandardDeviation(values), 3);
            if (집계 == 집계항목.반복편차) return values.Max() - values.Min();
            if (집계 == 집계항목.정합편차)
            {
                Decimal? v = 통계?.Sclar(검사);
                if (v == null) return null;
                Decimal cmm = (Decimal)v;
                return Math.Max(Math.Abs(cmm - values.Max()), Math.Abs(cmm - values.Min()));
            }
            if (집계 == 집계항목.정합평균)
            {
                Decimal? v = 통계?.Sclar(검사);
                if (v == null) return null;
                Decimal a = Math.Round(values.Average(), 3);
                return Math.Abs((Decimal)v - a);
            }
            return null;
        }
        internal override Boolean V(검사항목 검사, Decimal? value)
        {
            if (this.집계 == 집계항목.CMM측정)
            {
                this.결과값[검사] = (Decimal)value;
                return true;
            }
            return false;
        }
    }

    public class 피벗정보
    {
        public 집계항목 집계 = 집계항목.None;
        public 검사결과 검사 = null;
        internal Dictionary<검사항목, Decimal?> 결과값 = new Dictionary<검사항목, Decimal?>();

        #region Propertys
        [Description("Date")] public String 집계구분 { get; set; } = String.Empty;
        [Description("Time")] public DateTime? 검사일시 { get; set; } = null;
        [Description("Index")] public Int32? 검사코드 { get; set; } = null;
        [Description("Result")] public 결과구분? 측정결과 { get => R(); set => R(value); }
        #endregion

        public 피벗정보() { }
        public 피벗정보(검사결과 검사)
        {
            this.검사 = 검사;
            집계구분 = 검사.검사일시.ToString("yyyy-MM-dd");
            검사일시 = 검사.검사일시;
            검사코드 = 검사.검사코드;
            측정결과 = 검사.측정결과;
            Init();
        }

        internal virtual void Init()
        {
            foreach (검사항목 항목 in typeof(검사항목).GetEnumValues())
            {
                Decimal? value = null;
                검사정보 정보 = this.검사?.GetItem(항목);
                if (정보 != null) value = 정보.결과값;
                this.결과값[항목] = value;
            }
        }

        internal virtual Decimal? V(검사항목 검사) => this.결과값[검사];
        internal virtual Boolean V(검사항목 검사, Decimal? value)
        {
            검사정보 정보 = this.검사?.GetItem(검사);
            if (정보 == null || value == null) return false;
            this.결과값[검사] = value;
            정보.결과값 = (Decimal)value;
            정보.결과계산();

            return true;
        }
        internal virtual void Refresh(검사항목 검사)
        {
            검사정보 정보 = this.검사?.GetItem(검사);
            if (정보 == null) return;
            this.결과값[검사] = 정보.결과값;
        }

        public virtual 결과구분? Result(검사항목 검사)
        {
            검사정보 정보 = this.검사?.GetItem(검사);
            if (정보 == null) return null;
            return 정보.측정결과;
        }

        internal 결과구분? R()
        {
            if (this.검사 == null) return null;
            return this.검사.측정결과;
        }
        internal Boolean R(결과구분? 결과)
        {
            if (this.검사 == null || 결과 == null) return false;
            this.검사.측정결과 = (결과구분)결과;
            return true;
        }
    }

    public enum 집계항목
    {
        [Description("")] None,
        [Description("CMM 측정값")] CMM측정,
        [Description("반복성 최대값")] 반복최대,
        [Description("반복성 최소값")] 반복최소,
        [Description("반복성 평균값")] 반복평균,
        [Description("반복성 표준 편차")] 반복표준,
        [Description("반복성 최대 편차")] 반복편차,
        [Description("CMM 대비 최대 편차")] 정합편차,
        [Description("CMM 대비 평균 편차")] 정합평균,
    }
}
