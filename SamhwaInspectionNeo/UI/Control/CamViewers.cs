using DevExpress.XtraEditors;
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

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class CamViewers : XtraUserControl
    {
        private List<LabelControl> lb결과값 = new List<LabelControl>();
        public CamViewers() => InitializeComponent();

        public void Init()
        {
            if (Global.VM제어.Count == 0) return;

            this.Flow1Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow1).graphicsSetModuleTool;
            this.Flow2Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow2).graphicsSetModuleTool;
            this.Flow3Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow3).graphicsSetModuleTool;
            this.Flow4Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow4).graphicsSetModuleTool;

            this.trayViewer.ModuleSource = Global.VM제어.GetItem(Flow구분.공트레이검사).graphicsSetModuleTool;

            this.UpSurfaceViewer1.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사1).graphicsSetModuleTool;
            this.UpSurfaceViewer2.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사2).graphicsSetModuleTool;
            this.UpSurfaceViewer3.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사3).graphicsSetModuleTool;
            this.UpSurfaceViewer4.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사4).graphicsSetModuleTool;

            this.DownSurfaceViewer1.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사1).graphicsSetModuleTool;
            this.DownSurfaceViewer2.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사2).graphicsSetModuleTool;
            this.DownSurfaceViewer3.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사3).graphicsSetModuleTool;
            this.DownSurfaceViewer4.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사4).graphicsSetModuleTool;

            Global.검사자료.검사완료알림 += 검사완료알림;

            this.lb결과값.Add(this.Flow1결과);
            this.lb결과값.Add(this.Flow2결과);
            this.lb결과값.Add(this.Flow3결과);
            this.lb결과값.Add(this.Flow4결과);
        }
        private void 검사완료알림(검사결과 결과)
        {
            if (결과 == null) return;
            if (this.InvokeRequired) { this.BeginInvoke((Action)(() => 검사완료알림(결과))); return; }

            Int32 검사코드 = 결과.검사코드 >= 100 ? 결과.검사코드 - 100 : 결과.검사코드;

            this.lb결과값[검사코드].Text = $"{결과.측정결과} {결과.불량정보}";
            //this.결과값[검사코드].BackColor = Color.Transparent;
            this.lb결과값[검사코드].ForeColor = 환경설정.결과표현색상(결과.측정결과);
        }
    }
}
