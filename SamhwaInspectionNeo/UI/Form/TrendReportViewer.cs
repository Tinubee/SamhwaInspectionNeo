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
        private List<검사항목> Slot1치수 = new List<검사항목>();
        private List<검사항목> Slot2치수 = new List<검사항목>();
        private List<검사항목> Slot3치수 = new List<검사항목>();
        private List<검사항목> Slot4치수 = new List<검사항목>();
        private List<검사항목> 홀치수 = new List<검사항목>();
        public TrendReportViewer()
        {
            InitializeComponent();
            this.Init();
            b자료조회.Click += 자료조회;
        }

        private void Init()
        {
            검사항목설정();
            this.Slot1치수.Clear();
            this.Slot2치수.Clear();
            this.Slot3치수.Clear();
            this.Slot4치수.Clear();
            this.홀치수.Clear();

            검사설정자료 자료 = Global.모델자료.GetItem(Global.환경설정.선택모델).검사설정;

            //List<검사정보> 정보 = new List<검사정보>();

            foreach (검사정보 정보 in 자료)
            {
                if (정보.검사항목 >= 검사항목.Slot1상부 && 정보.검사항목 <= 검사항목.Slot1하부)
                {
                    Slot1치수.Add(정보.검사항목);
                    cSlot1.Y축설정(정보.최소값, 정보.최대값);
                }
                else if (정보.검사항목 >= 검사항목.Slot2상부 && 정보.검사항목 <= 검사항목.Slot2하부)
                {
                    Slot2치수.Add(정보.검사항목);
                    cSlot2.Y축설정(정보.최소값, 정보.최대값);
                }

                else if (정보.검사항목 >= 검사항목.Slot3상부 && 정보.검사항목 <= 검사항목.Slot3하부) 
                { 
                    Slot3치수.Add(정보.검사항목);
                    cSlot3.Y축설정(정보.최소값, 정보.최대값);
                }
                else if (정보.검사항목 >= 검사항목.Slot4상부 && 정보.검사항목 <= 검사항목.Slot4하부)
                { 
                    Slot4치수.Add(정보.검사항목);
                    cSlot4.Y축설정(정보.최소값, 정보.최대값);
                }
                else if (정보.검사항목 >= 검사항목.기준홀경 && 정보.검사항목 <= 검사항목.우하홀경)
                { 
                    홀치수.Add(정보.검사항목);
                    c홀치수.Y축설정(정보.최소값, 정보.최대값);
                }
            }



            cSlot1.Init(this.Slot1치수);
            cSlot2.Init(this.Slot2치수);
            cSlot3.Init(this.Slot3치수);
            cSlot4.Init(this.Slot4치수);
            c홀치수.Init(this.홀치수);
        }

        private void 검사항목설정()
        {
            if (Global.환경설정.선택모델 == 모델구분.Model_2PA || Global.환경설정.선택모델 == 모델구분.Model_2PC)
            {
                cSlot3.Visible = false;
                cSlot4.Visible = false;
            }
            else
            {
                cSlot3.Visible = true;
                cSlot4.Visible = true;
            }
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
            cSlot1.조회자료그래프보기();
            cSlot2.조회자료그래프보기();
            c홀치수.조회자료그래프보기();
        }

        private void FormShown(object sender, EventArgs e)
        {
            //this.cSlot1.검사완료알림();
        }
    }
}