using MvUtils;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SamhwaInspectionNeo
{
    public enum 동작구분
    {
        Live = 0,
        LocalTest = 2
    }
    public class Global
    {
        public static MainForm MainForm = null;
        public delegate void BaseEvent();
        public static event EventHandler<Boolean> Initialized;

        private const String 로그영역 = "프로그램";
        public static 환경설정 환경설정;
        public static 로그자료 로그자료;
        public static 조명제어 조명제어;
        public static 그랩제어 그랩제어;
        public static VM제어 VM제어;
        public static 모델자료 모델자료;

        public static class 장치상태
        {
            public static Boolean 정상여부
            {
                get { return true; }
            }
            public static Boolean 카메라1 { get { return Global.그랩제어.카메라1 == null ? false : Global.그랩제어.카메라1.상태; } }
            public static Boolean 카메라2 { get { return Global.그랩제어.카메라2 == null ? false : Global.그랩제어.카메라2.상태; } }
            public static Boolean 카메라3 { get { return Global.그랩제어.카메라3 == null ? false : Global.그랩제어.카메라3.상태; } }
            public static Boolean 카메라4 { get { return Global.그랩제어.카메라4 == null ? false : Global.그랩제어.카메라4.상태; } }
            //public static Boolean 조명장치 { get { return 조명제어.정상여부; } }
        }

        public static Boolean Init()
        {
            try
            {
                로그자료 = new 로그자료();
                환경설정 = new 환경설정();
                조명제어 = new 조명제어();
                VM제어 = new VM제어();
                그랩제어 = new 그랩제어();
                모델자료 = new 모델자료();

                로그자료.Init();
                //그랩제어.Init();
                환경설정.Init();
                VM제어.Init();
                모델자료.Init();

                Global.정보로그(로그영역, "초기화", "시스템을 초기화 합니다.", false);
                Initialized?.Invoke(null, true);
                return true;
            }
            catch (Exception ex)
            {
                MvUtils.Utils.DebugException(ex, 3);
                Global.오류로그(로그영역, "초기화 오류", "시스템 초기화에 실패하였습니다.\n" + ex.Message, true);
            }
            Initialized.Invoke(null, false);
            return false;
        }

        public static Boolean Close()
        {
            try
            {
                로그자료?.Close();
                환경설정?.Close();
                //조명제어?.Close();
                모델자료?.Close();
                VM제어?.Close();
                그랩제어?.Close();
                Properties.Settings.Default.Save();
                return true;
            }
            catch (Exception ex)
            {
                return MvUtils.Utils.ErrorMsg("프로그램 종료 중 오류가 발생하였습니다.\n" + ex.Message);
            }
            
        }

        public static void Start()
        {
                
        }

        public static void DxLocalization()
        {
            if (Localization.CurrentLanguage != Language.KO) return;
            MvUtils.Localization.CurrentLanguage = MvUtils.Localization.Language.KO;
            DxDataGridLocalizer.Enable();
            DxEditorsLocalizer.Enable();
            DxDataFilteringLocalizer.Enable();
            DxLayoutLocalizer.Enable();
            DxBarLocalizer.Enable();
        }

        public static String GetGuid()
        {
            Assembly assembly = typeof(Program).Assembly;
            GuidAttribute attribute = assembly.GetCustomAttributes(typeof(GuidAttribute), true)[0] as GuidAttribute;
            return attribute.Value;
        }

        #region 로그 / Alert
        private static AlertControl 알림화면 = new AlertControl() { AutoHeight = false, FormSize = new System.Drawing.Size(400, 150) };// { PopupLocation = AlertControl.PopupLocations.CenterForm };
        private delegate void ShowMessageDelegate(Form Owner, 로그정보 로그);
        private static void ShowMessage(Form Owner, 로그정보 로그)
        {
            if (Owner == null || Owner.IsDisposed) return;
            if (Owner.InvokeRequired)
            {
                Owner.BeginInvoke(new ShowMessageDelegate(ShowMessage), new object[] { Owner, 로그 });
                return;
            }

            if (로그.구분 == 로그구분.오류)
                알림화면.Show(AlertControl.AlertTypes.Invalid, 로그.제목, 로그.내용, Owner);
            else if (로그.구분 == 로그구분.경고)
                알림화면.Show(AlertControl.AlertTypes.Warning, 로그.제목, 로그.내용, Owner);
            else if (로그.구분 == 로그구분.정보)
                알림화면.Show(AlertControl.AlertTypes.Information, 로그.제목, 로그.내용, Owner);
        }
        public static void ShowMessage(로그정보 로그) => ShowMessage(MainForm, 로그);

        public static 로그정보 로그기록(string 영역, 로그구분 구분, string 제목, string 내용)
        {
            try
            {
                로그정보 로그 = 로그자료.Add(영역, 구분, 제목, 내용);
                Debug.WriteLine($"{MvUtils.Utils.FormatDate(DateTime.Now, "{0:HH:mm:ss}")}\t{구분}\t{영역}\t{제목}\t{내용}");
                return 로그;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message, "로그기록 오류");
            }
            return null;
        }

        public static 로그정보 오류로그(string 영역, string 제목, string 내용, bool Show) =>
            오류로그(영역, 제목, 내용, Show ? MainForm : null);
        public static 로그정보 오류로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.오류, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }

        public static 로그정보 경고로그(string 영역, string 제목, string 내용, bool Show) =>
            경고로그(영역, 제목, 내용, Show ? MainForm : null);
        public static 로그정보 경고로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.경고, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }

        public static 로그정보 정보로그(string 영역, string 제목, string 내용, bool Show) =>
            정보로그(영역, 제목, 내용, Show ? MainForm : null);
        public static 로그정보 정보로그(string 영역, string 제목, string 내용, Form Owner)
        {
            로그정보 로그 = 로그기록(영역, 로그구분.정보, 제목, 내용);
            if (로그 != null && Owner != null) ShowMessage(Owner, 로그);
            return 로그;
        }
        #endregion
    }
}
