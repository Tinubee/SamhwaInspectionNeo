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
        public CamViewers()
        {
            InitializeComponent();
        }

        public void Init()
        {
            if (Global.VM제어.Count == 0) return;

            this.Flow1Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow1).graphicsSetModuleTool;
            this.Flow2Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow2).graphicsSetModuleTool;
            this.Flow3Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow3).graphicsSetModuleTool;
            this.Flow4Viewer.ModuleSource = Global.VM제어.GetItem(Flow구분.Flow4).graphicsSetModuleTool;

            this.trayViewer.ModuleSource = Global.VM제어.GetItem(Flow구분.공트레이검사).graphicsSetModuleTool;

            this.UpSurfaceViewer1.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사).graphicsSetModuleToolList[0];
            this.UpSurfaceViewer2.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사).graphicsSetModuleToolList[1];
            this.UpSurfaceViewer3.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사).graphicsSetModuleToolList[2];
            this.UpSurfaceViewer4.ModuleSource = Global.VM제어.GetItem(Flow구분.상부표면검사).graphicsSetModuleToolList[3];

            this.BottomSurfaceViewer1.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사).graphicsSetModuleToolList[0];
            this.BottomSurfaceViewer2.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사).graphicsSetModuleToolList[1];
            this.BottomSurfaceViewer3.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사).graphicsSetModuleToolList[2];
            this.BottomSurfaceViewer4.ModuleSource = Global.VM제어.GetItem(Flow구분.하부표면검사).graphicsSetModuleToolList[3];

        }
    }
}
