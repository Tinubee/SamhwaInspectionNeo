using DevExpress.XtraEditors;
using ImageSourceModuleCs;
using OpenCvSharp;
using SamhwaInspectionNeo.Schemas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VM.PlatformSDKCS;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class NgImageViewer : XtraUserControl
    {
        public NgImageViewer()
        {
            InitializeComponent();
        }

        public void Init(String path, Int32 검사코드, 지그위치 지그)
        {
            this.vmRenderControl1.ModuleSource = Global.VM제어.GetItem(Flow구분.NG).graphicsSetModuleTool;

            String nameValue = ((Flow구분)검사코드).ToString();
            InputStringData[] inputStringData = new InputStringData[1];
            inputStringData[0].strValue = nameValue;

            if (지그 == 지그위치.Front)
            {
                Global.환경설정.Front지그 = true;
                Global.VM제어.글로벌변수제어.SetValue("Front지그", "1");
                Global.VM제어.글로벌변수제어.SetValue("Rear지그", "0");
            }
            else if (지그 == 지그위치.Rear)
            {
                Global.환경설정.Front지그 = false;
                Global.VM제어.글로벌변수제어.SetValue("Front지그", "0");
                Global.VM제어.글로벌변수제어.SetValue("Rear지그", "1");
            }


            Global.VM제어.GetItem(Flow구분.NG).imageSourceModuleTool.SetImagePath(path);
            Global.VM제어.GetItem(Flow구분.NG).Shell입력값적용(inputStringData, "name");
            Global.VM제어.GetItem(Flow구분.NG).OnlyFlowRun();
        }
    }
}
