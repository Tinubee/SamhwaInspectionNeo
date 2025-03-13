using MvUtils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace SamhwaInspectionNeo.Schemas
{
    public class 분류정보
    {
        [JsonProperty("Code"), Translation("Code", "코드")]
        public Int32 코드 { get; set; } = 1;
        [JsonProperty("Index"), Translation("Index", "순번")]
        public Int32 순번 { get; set; } = 1;
        [JsonProperty("Name"), Translation("Name", "명칭")]
        public String 명칭 { get; set; } = String.Empty;
        [JsonProperty("Group"), Translation("Group", "그룹")]
        public 검사그룹 그룹 { get; set; } = 검사그룹.None;

        public void Set(분류정보 정보)
        {
            순번 = 정보.순번;
            명칭 = 정보.명칭;
            그룹 = 정보.그룹;
        }
    }

    public class 분류자료 : List<분류정보>
    {
        [JsonIgnore]
        private const String 로그영역 = "Categorys";
        [JsonIgnore]
        private string 저장파일 => Path.Combine(Global.환경설정.기본경로, "Categorys.json");

        public void Init() => Load();
        public void Close() { }

        public void Load()
        {
            this.Add(new 분류정보 { 코드 = 11, 순번 = 01, 그룹 = 검사그룹.CTQ, 명칭 = "Distance" });
            this.Add(new 분류정보 { 코드 = 81, 순번 = 02, 그룹 = 검사그룹.SURFACE, 명칭 = "Defects" });

            if (!File.Exists(this.저장파일)) return;
            try
            {
                List<분류정보> 자료 = JsonConvert.DeserializeObject<List<분류정보>>(File.ReadAllText(this.저장파일), MvUtils.Utils.JsonSetting());
                자료.Sort((a, b) => a.순번.CompareTo(b.순번));
                foreach (분류정보 정보 in 자료)
                    this.GetItem(정보.코드)?.Set(정보);
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역, "Load", ex.Message, false);
            }
        }

        public void Save()
        {
            if (!MvUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, MvUtils.Utils.JsonSetting())))
                Global.오류로그(로그영역, "Save", "Saving failed.", true);
            else Global.정보로그(로그영역, "Save", "Saved.", true);
        }

        public 분류정보 GetItem(Int32 코드) => this.Where(e => e.코드 == 코드).FirstOrDefault();
    }
}
