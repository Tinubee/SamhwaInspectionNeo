﻿using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using SamhwaInspectionNeo.Schemas;
using SamhwaInspectionNeo.UI.Form;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MvUtils;
using System.Drawing.Text;

namespace SamhwaInspectionNeo.UI.Control
{
    public partial class SetInspection : XtraUserControl
    {
        public delegate void 검사항목선택(모델정보 모델, 검사정보 설정);
        public event 검사항목선택 검사항목변경;
        private readonly LocalizationInspection 번역 = new LocalizationInspection();
        private Boolean Loading = false;
        public SetInspection()
        {
            InitializeComponent();
        }

        public void Init()
        {
            Loading = true;
            this.GridView1.Init(this.barManager1);
            this.GridView1.OptionsBehavior.Editable = true;
            this.GridView1.OptionsSelection.MultiSelect = true;
            this.GridView1.OptionsView.NewItemRowPosition = NewItemRowPosition.Bottom;
            this.GridView1.AddEditSelectionMenuItem();
            this.GridView1.AddSelectPopMenuItems();
            this.GridView1.AddDeleteMenuItem(DeleteClick);
            this.col최소값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col최대값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col기준값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col보정값.DisplayFormat.FormatString = Global.환경설정.결과표현;
            this.col교정값.DisplayFormat.FormatString = Global.환경설정.결과표현;

            this.e모델선택.EditValueChanging += 모델선택;
            this.e모델선택.Properties.DataSource = Global.모델자료;
            this.e모델선택.EditValue = Global.환경설정.선택모델;
            this.e모델선택.CustomDisplayText += 선택모델표현;
            this.b설정저장.Click += 설정저장;

            Localization.SetColumnCaption(this.e모델선택, typeof(모델정보));
            Localization.SetColumnCaption(this.GridView1, typeof(검사정보));
            this.b설정저장.Text = 번역.설정저장;
            //this.모델선택(this.e모델선택, (DevExpress.XtraEditors.Controls.ChangingEventArgs)EventArgs.Empty);
            this.b도구설정.Click += B도구설정_Click;
            Loading = false;
        }

        public void Close() { }

        private 모델구분 선택모델 { get { return Global.환경설정.선택모델; } }
        // private 모델구분 선택모델 { get { return (모델구분)this.e모델선택.EditValue; } }
        public 검사설정자료 검사설정 { get { return Global.모델자료.GetItem(this.선택모델)?.검사설정; } }

        private void 모델선택(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            try
            {
                if (!Loading)
                {
                    if (!MvUtils.Utils.Confirm(번역.모델변경))
                    {
                        e.Cancel = true;
                        return;
                    }
                }

                모델구분 모델 = (모델구분)e.NewValue;
                this.GridControl1.DataSource = this.검사설정;
                if (this.검사설정 != null && this.검사설정.Count > 0)
                {
                    Task.Run(() => {
                        Task.Delay(500).Wait();
                        this.GridView1.MoveFirst();
                        this.검사항목변경?.Invoke(Global.모델자료.GetItem(this.선택모델), this.GetItem(this.GridView1, this.GridView1.FocusedRowHandle));
                        Global.환경설정.모델변경요청(모델);
                    });
                }
            }
            catch (Exception ex)
            {
                Global.오류로그(검사설정자료.로그영역.GetString(), 번역.모델선택, $"{번역.모델선택}\r\n{ex.Message}", true);
                this.GridControl1.DataSource = null;
            }
        }

        private void 선택모델표현(object sender, DevExpress.XtraEditors.Controls.CustomDisplayTextEventArgs e)
        {
            try
            {
                if (e.Value == null) return;
                모델구분 모델 = (모델구분)e.Value;
                e.DisplayText = $"{((Int32)모델).ToString("d2")}. {MvUtils.Utils.GetDescription(모델)}";
            }
            catch { e.DisplayText = String.Empty; }
        }

        private 검사정보 GetItem(GridView view, Int32 RowHandle)
        {
            if (view == null) return null;
            return view.GetRow(RowHandle) as 검사정보;
        }

        private void DeleteClick(object sender, ItemClickEventArgs e)
        {
            검사설정자료 검사 = this.검사설정;
            검사정보 설정 = this.GetItem(this.GridView1, this.GridView1.FocusedRowHandle);
            if (검사 == null || 설정 == null) return;
            if (!MvUtils.Utils.Confirm(번역.삭제확인)) return;
            검사.Remove(설정);
        }

        private void 설정저장(object sender, EventArgs e)
        {
            검사설정자료 설정 = this.검사설정;
            if (설정 == null) return;
            if (!MvUtils.Utils.Confirm(번역.저장확인)) return;
            Global.VM제어.Save();
            if (설정.Save()) Global.정보로그(검사설정자료.로그영역.GetString(), 번역.설정저장, 번역.저장완료, true);
        }

        private void B도구설정_Click(object sender, EventArgs e)
        {
            Teaching form = new Teaching();
            form.Show(Global.MainForm);
        }
        private class LocalizationInspection
        {
            private enum Items
            {
                [Translation("Save", "설정저장")]
                설정저장,
                [Translation("It's saved.", "저장되었습니다.")]
                저장완료,
                [Translation("Save the inspection settings?", "검사 설정을 저장하시겠습니까?")]
                저장확인,
                [Translation("Delete this selected item?", "선택 항목을 삭제하시겠습니까?")]
                삭제확인,

                [Translation("Select Model", "모델선택")]
                모델선택,
                [Translation("No models selected.", "선택한 모델이 없습니다.")]
                모델없음,
                [Translation("Change the inspection model?", "검사모델을 변경하시겠습니까?")]
                모델변경,
            }

            public String 설정저장 { get { return Localization.GetString(Items.설정저장); } }
            public String 저장완료 { get { return Localization.GetString(Items.저장완료); } }
            public String 저장확인 { get { return Localization.GetString(Items.저장확인); } }
            public String 삭제확인 { get { return Localization.GetString(Items.삭제확인); } }
            public String 모델선택 { get { return Localization.GetString(Items.모델선택); } }
            public String 모델없음 { get { return Localization.GetString(Items.모델없음); } }
            public String 모델변경 { get { return Localization.GetString(Items.모델변경); } }
        }
    }
}
