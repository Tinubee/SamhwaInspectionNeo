using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace SamhwaInspectionNeo.Schemas.VisionMaster
{
    public enum 검사항목
    {
        [Description("X")]
        X축,
        [Description("Y")]
        Y축,
        [Description("SlotTop")]
        Slot상부,
        [Description("SlotMiddle")]
        Slot중앙부,
        [Description("SlotBottom")]
        Slot하부,
        [Description("StandardHole")]
        기준홀경,
        [Description("TopHole")]
        상측홀경,
        [Description("LeftBottomHole")]
        좌하홀경,
        [Description("LeftTopHole")]
        좌상홀경,
        [Description("RightTopHole")]
        우상홀경
    }

    public class 보정값설정 : List<보정값변수>
    {
        private string 마스터데이터파일 { get { return Path.Combine(Global.환경설정.기본경로, "마스터설정.json"); } }
        List<VmVariable> calValueList = new List<VmVariable>();

        public void Init()
        {
            base.Clear();
            calValueList = Global.VM제어.글로벌변수제어.보정값불러오기();
            foreach (VmVariable v in calValueList)
                this.Add(new 보정값변수(v));

            List<보정값변수> 자료 = Load();
        }

        private List<보정값변수> Load()
        {
            if (!File.Exists(this.마스터데이터파일)) return null;
            return JsonConvert.DeserializeObject<List<보정값변수>>(File.ReadAllText(마스터데이터파일), MvUtils.Utils.JsonSetting());
        }

        public void Save()
        {
            if (!MvUtils.Utils.WriteAllText(마스터데이터파일, JsonConvert.SerializeObject(this, MvUtils.Utils.JsonSetting())))
            {
                //Global.오류로그(로그영역, "환경설정 저장", "환경설정 저장에 실패하였습니다.", true);
            }
        }

        public void 비전데이터적용(List<String> visionData, Flow구분 플로우, 지그위치 지그위치)
        {
            for (int lop = 0; lop < visionData.Count; lop++)
            {
                foreach (var item in this)
                {
                    if (item.변수명칭.Contains($"Slot{lop + 1}") && item.지그위치 == 지그위치 && item.플로우 == 플로우)
                    {
                        item.비전측정값 = Convert.ToDouble(visionData[lop]);
                    }
                }
            }
        }

        public void Set()
        {
            foreach (var v in this)
            {
                Global.VM제어.글로벌변수제어.InspectUseSet(v.변수명칭, v.보정값.ToString());
            }
        }

        public void 보정값계산()
        {
            foreach (var item in this)
            {
                if (item.실측값 == 0 || item.비전측정값 == 0) continue;

                item.보정값 = Math.Round((item.실측값 / item.비전측정값), 6);
                Debug.WriteLine($"{item.검사명칭} 의 보정값 : {item.보정값} ");
            }
        }
    }

    public class 보정값변수
    {
        [JsonProperty("name"), Description("변수명")]
        public 검사항목 검사명칭 { get; set; } = 검사항목.Slot상부;
        [JsonProperty("namecalv"), Description("변수명")]
        public String 변수명칭 { get; set; } = String.Empty;
        [JsonProperty("minv"), BatchEdit(true), Description("최소값")]
        public Double 최소값 { get; set; } = 0;
        [JsonProperty("stdv"), BatchEdit(true), Description("기준값")]
        public Double 기준값 { get; set; } = 0;
        [JsonProperty("maxv"), BatchEdit(true), Description("최대값")]
        public Double 최대값 { get; set; } = 0;
        [JsonIgnore, Description("실측값")] //(mm)
        public Double 실측값 { get; set; } = 0;
        [JsonProperty("calb"), Description("보정값")]
        public Double 보정값 { get; set; } = 0;
        [JsonIgnore, Description("지그위치")]
        public 지그위치 지그위치 { get; set; }
        [JsonIgnore, Description("플로우")]
        public Flow구분 플로우 { get; set; }
        [JsonIgnore, Description("비전측정값")]
        public Double 비전측정값 { get; set; } = 0;
        [JsonIgnore, Description("검사결과")]
        public 결과구분 판정 { get; set; } = 결과구분.NO;

        public 보정값변수(VmVariable v)
        {
            if (v != null)
            {
                this.변수명칭 = v.Name; //v.Name.Replace("calValue", "보정값");
                this.지그위치 = this.변수명칭.Contains("_F_") == true ? 지그위치.Front : 지그위치.Rear;
                this.보정값 = Convert.ToDouble(v.Value);
                플로우설정();
                검사명칭설정();
            }
        }

        public void 플로우설정()
        {
            if (this.변수명칭.Contains("Flow1"))
                this.플로우 = Flow구분.Flow1;
            else if (this.변수명칭.Contains("Flow2"))
                this.플로우 = Flow구분.Flow2;
            else if (this.변수명칭.Contains("Flow3"))
                this.플로우 = Flow구분.Flow3;
            else if (this.변수명칭.Contains("Flow4"))
                this.플로우 = Flow구분.Flow4;
        }

        public void 검사명칭설정()
        {
            if (this.변수명칭.Contains("SlotTop"))
                this.검사명칭 = 검사항목.Slot상부;
            else if (this.변수명칭.Contains("SlotMiddle"))
                this.검사명칭 = 검사항목.Slot중앙부;
            else if (this.변수명칭.Contains("SlotBottom"))
                this.검사명칭 = 검사항목.Slot하부;
            else if (this.변수명칭.Contains("StandardHole"))
                this.검사명칭 = 검사항목.기준홀경;
            else if (this.변수명칭.Contains("TopHole"))
                this.검사명칭 = 검사항목.상측홀경;
            else if (this.변수명칭.Contains("LeftBottomHole"))
                this.검사명칭 = 검사항목.좌하홀경;
            else if (this.변수명칭.Contains("LeftTopHole"))
                this.검사명칭 = 검사항목.좌상홀경;
            else if (this.변수명칭.Contains("RightTopHole"))
                this.검사명칭 = 검사항목.우상홀경;
            else if (this.변수명칭.Contains("calValueX"))
                this.검사명칭 = 검사항목.X축;
            else if (this.변수명칭.Contains("calValueY"))
                this.검사명칭 = 검사항목.Y축;
        }
    }
}
