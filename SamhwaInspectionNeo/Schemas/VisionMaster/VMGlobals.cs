﻿using GlobalVariableModuleCs;
using System;
using System.Collections.Generic;
using System.Linq;
using VM.Core;

namespace SamhwaInspectionNeo.Schemas
{
    public class VmGlobals : List<VmVariable>
    {
        private GlobalVariableModuleTool Variables;
        private List<GlobalVarInfo> GlobalVarInfo;
        public void Init()
        {
            base.Clear(); //모델변경시 기존 변수들 초기화
            this.Variables = VmSolution.Instance["Global Variable1"] as GlobalVariableModuleTool;
            if (this.Variables == null) return;
            GlobalVarInfo = Variables.GetAllGlobalVar();
           
            foreach (GlobalVarInfo info in GlobalVarInfo)
            {
                if (info.strValueType.ToLower() == typeof(String).Name.ToLower()) continue;
                if (info.strValueName.Contains("calValue") || info.strValueName.Contains("offset")) continue;
                
                this.Add(new VmVariable(info));
            }
        }

        public String GetValue(string name)
        {
            VmVariable info = this.Where(e => e.Name == name).FirstOrDefault();
            return info.StringValue;
        }

       public List<VmVariable> 보정값불러오기()
        {
            List<GlobalVarInfo> lists = Variables.GetAllGlobalVar();
            List<VmVariable> calValueList = new List<VmVariable>();
            foreach (GlobalVarInfo info in lists)
            {
                if (info.strValueType.ToLower() != typeof(String).Name.ToLower()) continue;

                if (info.strValueName.Contains("CalValue"))
                    calValueList.Add(new VmVariable(info));
            }

            return calValueList;
        }

        public List<VmVariable> 마스터셋팅값가져오기()
        {
            List<GlobalVarInfo> lists = Variables.GetAllGlobalVar();
            List<VmVariable> masterValueList = new List<VmVariable>();
            foreach (GlobalVarInfo info in lists)
            {
                if (info.strValueType.ToLower() == typeof(float).Name.ToLower()) continue;

                if ((info.strValueName.Contains("master") && info.strValueName.Contains("-1")) || info.strValueName.Contains("MinMaxValue"))
                    masterValueList.Add(new VmVariable(info));
            }

            return masterValueList;
        }


        public void InspectUseSet(string Name, string Value)
        {
            if(this.Variables == null) return;

            List<GlobalVarInfo> lists = Variables.GetAllGlobalVar();

            foreach (GlobalVarInfo info in lists)
            {
                if(info.strValueName.Contains(Name))
                    this.Variables.SetGlobalVar(Name, Value);
            }
        }

        public void Set()
        {
            foreach (VmVariable v in this)
                this.Variables.SetGlobalVar(v.Name, v.StringValue);
        }

        public void SetValue(String name, String value)
        {
            this.Variables.SetGlobalVar(name, value);
        }
    }

    public class VmVariable
    {
        public String Name { get; private set; } = String.Empty;
        public String Description { get; private set; } = String.Empty;
        public Type Type { get; private set; } = null;
        public Object Value { get; set; } = null;
        public String StringValue
        {
            get { return this.Value == null ? String.Empty : Value.ToString(); }
            set { this.Set(value); }
        }

        public VmVariable() { }
        public VmVariable(GlobalVarInfo info)
        {
            this.Name = info.strValueName;
            this.Description = info.strRemark;
            if (info.strValueType == "float") this.Type = typeof(Single);
            else if (info.strValueType == "int") this.Type = typeof(Int32);
            else this.Type = typeof(String);
            this.Value = info.strValue;
        }

        public void Set(String value)
        {
            if (this.Type == typeof(Single)) this.Value = Convert.ToSingle(value);
            else if (this.Type == typeof(Int32)) this.Value = Convert.ToInt32(value);
            else this.Value = value;
        }
    }
}
