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

namespace SamhwaInspectionNeo.UI.Form
{
    public partial class TrendReportViewer : XtraForm
    {
        private List<검사항목> Slot1 = new List<검사항목>();
        private List<검사항목> Slot2 = new List<검사항목>();
        private List<검사항목> Hole = new List<검사항목>();
        public TrendReportViewer()
        {
            InitializeComponent();
            this.Init();
            b자료조회.Click += 자료조회;
        }

        private void Init()
        {
            this.Slot1 = GetEnumsContaining("Slot1");
            this.Slot2 = GetEnumsContaining("Slot2");
            this.Hole = GetEnumsContaining("홀경");

            cSlot1.Init(this.Slot1);
            cSlot2.Init(this.Slot2);
            c홀치수.Init(this.Hole);
        }

        private List<검사항목> GetEnumsContaining(string searchString)
        {
            List<검사항목> matchingEnums = new List<검사항목>();

            foreach (검사항목 enumValue in Enum.GetValues(typeof(검사항목)))
            {
                if (enumValue.ToString().Contains(searchString))
                {
                    matchingEnums.Add(enumValue);
                }
            }

            return matchingEnums;
        }

        private void 자료조회(object sender, EventArgs e)
        {

        }

        private void FormShown(object sender, EventArgs e)
        {
            //this.cSlot1.검사완료알림();
        }
    }
}