﻿using Newtonsoft.Json;
using Npgsql;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SamhwaInspectionNeo.Schemas
{
    public class 환경설정
    {
        public delegate void 모델변경(모델구분 모델코드);
        public event 모델변경 모델변경알림;

        [JsonIgnore]
        public const String 프로젝트번호 = "23-1228-002";
        [Translation("Last Commit Time", "마지막 커밋 시간"), JsonProperty("LastCommit")]
        public String 마지막커밋시간 { get; set; } = String.Empty;
        [Description("프로그램 동작구분"), JsonProperty("RunType")]
        public 동작구분 동작구분 { get; set; } = 동작구분.Live;
        [Translation("Config Path", "설정 저장 경로"), JsonProperty("ConfigSavePath")]
        public String 기본경로 { get; set; } = @"C:\VISION\Config";// Path.Combine(Application.StartupPath, @"Config");
        [Translation("Document Save Path", "문서 저장 경로"), JsonProperty("DocumentSavePath")]
        public String 문서저장경로 { get; set; } = @"C:\VISION\SaveData";
        [Translation("Image Save Path", "사진 저장 경로"), JsonProperty("ImageSavePath")]
        public String 사진저장경로 { get; set; } = @"C:\VISION\SaveImage";
        [Translation("Decimals", "검사 결과 자릿수"), JsonProperty("Decimals")]
        public Int32 결과자릿수 { get; set; } = 3;
        [Translation("OK Image Save", "OK 이미지 저장"), JsonProperty("SaveOK")]
        public Boolean 사진저장OK { get; set; } = false;
        [Translation("NG Image Save", "NG 이미지 저장"), JsonProperty("SaveNG")]
        public Boolean 사진저장NG { get; set; } = false;
        [Translation("Force OK", "강제OK"), JsonProperty("ForceOK")]
        public Boolean 강제OK배출 { get; set; } = false;
        [Translation("Force NG", "강제NG"), JsonProperty("ForceNG")]
        public Boolean 강제NG배출 { get; set; } = false;
        [Translation("Results Storage Days", "검사 결과 보관일"), JsonProperty("DaysToKeepResults")]
        public Int32 결과보관 { get; set; } = 900;
        [Translation("Logs Storage Days", "로그 보관일"), JsonProperty("DaysToKeepLogs")]
        public Int32 로그보관 { get; set; } = 60;
        [JsonProperty("CurrentModel")]
        public 모델구분 선택모델 { get; set; } = 모델구분.None;
        [Translation("Model Image Path", "제품 사진 경로")] // , JsonProperty("ImagePath")
        public String 사진경로 { get { return Path.Combine(기본경로, "Master"); } } // = @"C:\VISION\Config\Master";
        [Translation("Model Movie Path", "제품 영상 경로"), JsonIgnore] //, JsonProperty("MoviePath")
        public String 영상경로 { get { return Path.Combine(기본경로, "Movie"); } }// = @"C:\VISION\Config\Movie";
        [Description("비젼 Tools"), JsonIgnore]
        public String 도구경로 { get { return Path.Combine(기본경로, "Tools"); } }
        [Description("마스터 이미지"), JsonIgnore]
        public String 마스터사진 { get { return Path.Combine(기본경로, "Masters"); } }
        [Translation("Dimension Inspection Used", "치수검사 사용여부"), JsonProperty("DimensionInspectUsed")]
        public Boolean 치수검사사용여부 { get; set; } = false;
        [Translation("Top Surface Inspection Used", "상부표면검사 사용여부"), JsonProperty("TopSurfaceInspectUsed")]
        public Boolean 상부표면검사사용여부 { get; set; } = false;
        [Translation("Bottom Surface Inspection Used", "하부표면검사 사용여부"), JsonProperty("BottomSurfaceInspectUsed2")]
        public Boolean 하부표면검사사용여부 { get; set; } = false;
        [JsonIgnore]
        public Boolean 강제배출 { get; set; } = true;
        [JsonIgnore]
        public String Format { get { return "#,0." + String.Empty.PadLeft(this.결과자릿수, '0'); } }
        [JsonIgnore]
        public String 결과표현 { get { return "{0:" + Format + "}"; } }
        [JsonIgnore, Description("사용자명")]
        public String 사용자명 { get; set; } = String.Empty;
        [JsonIgnore, Description("권한구분")]
        public 유저권한구분 사용권한 { get; set; } = 유저권한구분.없음;

        [JsonIgnore, Description("검사여부"), Translation("Use Inspect", "검사여부")]
        public Boolean 검사여부 { get; set; } = true; // 검사로직 활성화 여부
        [JsonIgnore, Description("티칭모드")]
        public Boolean 티칭모드 { get; set; } = false; // 활성화 시 사진 강제 저장 됨
        [JsonIgnore, Description("티칭모드")]
        public Boolean 모델변경중 { get; set; } = false;
        public Boolean 권한여부(유저권한구분 요구권한)
        {
            return (Int32)사용권한 >= (Int32)요구권한;
        }
        [JsonIgnore, Description("지그위치")]
        public Boolean Front지그 { get; set; } = false;
        [JsonIgnore, Description("슈퍼유저")]
        public const String 시스템관리자 = "ivmadmin";
        public 유저권한구분 시스템관리자인증(string 사용자명, string 비밀번호)
        {
            if (사용자명 != 시스템관리자) return 유저권한구분.없음;
            String pass = $"{시스템관리자}";// {Utils.FormatDate(DateTime.Now, "{0:ddHH}")}";
            if (비밀번호 == pass)
            {
                this.시스템관리자로그인();
                return 유저권한구분.시스템;
            }
            return 유저권한구분.없음;
        }
        public void 시스템관리자로그인()
        {
            this.사용자명 = 시스템관리자;
            this.사용권한 = 유저권한구분.시스템;
        }
        [JsonIgnore]
        public Boolean 슈퍼유저 { get { return 사용권한 == 유저권한구분.시스템 && 사용자명 == 시스템관리자; } }

        [JsonIgnore]
        public static TranslationAttribute 로그영역 = new TranslationAttribute("Preferences", "환경설정");
        [JsonIgnore]
        private String 저장파일 { get { return Path.Combine(this.기본경로, "Config.json"); } }

        [JsonIgnore, Description("이미지 저장 디스크 사용율")]
        public Int32 저장비율 { get { return 100 - this.SaveImageDriveFreeSpace(); } }

        public static NpgsqlConnection CreateDbConnection()
        {
            NpgsqlConnectionStringBuilder b = new NpgsqlConnectionStringBuilder() { Host = "localhost", Port = 5432, Username = "postgres", Password = "ivmadmin", Database = "Samhwa" };
            return new NpgsqlConnection(b.ConnectionString);
        }

        public Boolean CanDbConnect()
        {
            Boolean can = false;
            try
            {
                NpgsqlConnection conn = CreateDbConnection();
                conn.Open();
                can = conn.ProcessID > 0;
                conn.Close();
                conn.Dispose();
            }
            catch (Exception ex)
            {
                Global.오류로그(로그영역.GetString(), "데이터베이스 연결실패", ex.Message, true);
            }
            return can;
        }

        public Boolean Init()
        {
            return this.Load();
        }

        public void Close()
        {
            this.Save();
        }

        public Boolean Load()
        {
            if (!CanDbConnect())
            {
                Global.오류로그(로그영역.GetString(), "데이터베이스 연결실패", "데이터베이스에 연결할 수 없습니다.", true);
                return false;
            }

            Common.DirectoryExists(Path.Combine(Application.StartupPath, @"Views"), true);
            if (!Common.DirectoryExists(기본경로, true))
            {
                Global.오류로그(로그영역.GetString(), "환경설정 로드 실패", "기본설정 폴더를 생성할 수 없습니다.", true);
                return false;
            }
            if (!Common.DirectoryExists(사진저장경로, true))
            {
                Global.오류로그(로그영역.GetString(), "환경설정 로드 실패", "사진저장 폴더를 생성할 수 없습니다.", true);
            }
            if (!Common.DirectoryExists(문서저장경로, true))
            {
                Global.오류로그(로그영역.GetString(), "환경설정 로드 실패", "문서저장 폴더를 생성할 수 없습니다.", true);
            }

            if (File.Exists(저장파일))
            {
                try
                {
                    환경설정 설정 = JsonConvert.DeserializeObject<환경설정>(File.ReadAllText(저장파일, Encoding.UTF8));
                    foreach (PropertyInfo p in 설정.GetType().GetProperties())
                    {
                        if (!p.CanWrite) continue;
                        Object v = p.GetValue(설정);
                        if (v == null) continue;
                        p.SetValue(this, v);
                    }
                }
                catch (Exception ex)
                {
                    Global.오류로그(로그영역.GetString(), "환경설정 로드 실패", ex.Message, true);
                }
            }
            else
            {
                this.Save();
                Global.정보로그(로그영역.GetString(), "환경설정 로드", "저장된 설정파일이 없습니다.", false);
            }

            Debug.WriteLine(this.동작구분, "동작구분");
            return true;
        }

        public void Save()
        {
            if (!MvUtils.Utils.WriteAllText(저장파일, JsonConvert.SerializeObject(this, MvUtils.Utils.JsonSetting())))
            {
                Global.오류로그(로그영역.GetString(), "환경설정 저장", "환경설정 저장에 실패하였습니다.", true);
            }
        }

        public void 모델변경요청(Int32 모델번호)
        {
            this.모델변경요청((모델구분)모델번호);
        }

        public void 모델변경요청(모델구분 모델구분)
        {
            if (this.선택모델 != 모델구분)
            {
                this.선택모델 = 모델구분;
                Global.환경설정.모델변경중 = true;
                Global.정보로그(로그영역.GetString(), "모델변경", $"{this.선택모델} 모델로 변경되었습니다.", true);
                this.모델변경알림?.Invoke(this.선택모델);
            }
            //Global.MainForm.ShowWaitForm();
        }

        public static Color 결과표현색상(결과구분 구분)
        {
            if (구분 == 결과구분.IN) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.DisabledText;
            if (구분 == 결과구분.ER) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Question;
            if (구분 == 결과구분.OK) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Information;
            if (구분 == 결과구분.NG) return DevExpress.LookAndFeel.DXSkinColors.ForeColors.Critical;
            return DevExpress.LookAndFeel.DXSkinColors.ForeColors.ControlText;
        }

        #region 드라이브 용량계산
        private DriveInfo ImageSaveDrive = null;
        private DriveInfo GetSaveImageDrive()
        {
            DriveInfo[] drives = DriveInfo.GetDrives();
            foreach (DriveInfo drive in drives)
            {
                if (this.사진저장경로.StartsWith(drive.Name))
                {
                    //Debug.WriteLine(drive.Name, drive.VolumeLabel);
                    this.ImageSaveDrive = drive;
                    return this.ImageSaveDrive;
                }
            }
            if (drives.Length > 0)
                this.ImageSaveDrive = drives[0];

            return this.ImageSaveDrive;
        }

        public Int32 SaveImageDriveFreeSpace()
        {
            DriveInfo drive = this.GetSaveImageDrive();
            double FreeSpace = drive.AvailableFreeSpace / (double)drive.TotalSize * 100;
            return Convert.ToInt32(FreeSpace);
        }
        #endregion
    }
}
