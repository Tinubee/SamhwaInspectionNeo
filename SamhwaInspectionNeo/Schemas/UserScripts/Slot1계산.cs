﻿using System;
using System.Text;
using System.Windows.Forms;
using Script.Methods;
using System.Collections.Generic;
/************************************
Shell Module default code: using .NET Framwwork 4.6.1
*************************************/
public partial class Slot1계산:ScriptMethods,IProcessMethods
{
    //the count of process
	//执行次数计数
    int processCount ;
	Single resulteValue = 0;
	List<float> Slot1CalValueF = new List<float>();
	List<float> Slot1CalValueR = new List<float>();
    /// <summary>
    /// Initialize the field's value when compiling
	/// 预编译时变量初始化
    /// </summary>
    public void Init()
    {
        //You can add other global fields here
		//变量初始化，其余变量可在该函数中添加
        processCount = 0;
        
        Slot1CalValueF.Add(1);
        Slot1CalValueF.Add(1);
        Slot1CalValueF.Add(1);
        
        Slot1CalValueR.Add(2);
        Slot1CalValueR.Add(2);
        Slot1CalValueR.Add(2);
    }

    /// <summary>
    /// Enter the process function when running code once
	/// 流程执行一次进入Process函数
    /// </summary>
    /// <returns></returns>
    public bool Process()
    {
        //You can add your codes here, for realizing your desired function
		//每次执行将进入该函数，此处添加所需的逻辑流程处理
		if(Front지그 == 1){
			resulteValue = Convert.ToSingle((픽셀값 * Slot1CalValueF[index]).ToString("F3"));
			Slot1 = resulteValue;
			
		}else{
			resulteValue = Convert.ToSingle((픽셀값 * Slot1CalValueR[index]).ToString("F3"));
			Slot1 = resulteValue;
		}
		
        return true;
    }
}
                            