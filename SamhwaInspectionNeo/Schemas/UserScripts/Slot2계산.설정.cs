using System;
using System.Text;
using System.Windows.Forms;
using Script.Methods;
public partial class UserScript:ScriptMethods,IProcessMethods
{
    
    public int Front지그
    {
        get
        {
            int tmp = 0;
            nErrorCode = GetIntValue("Front지그", ref tmp);
            return tmp;
        }
    }

    public int Rear지그
    {
        get
        {
            int tmp = 0;
            nErrorCode = GetIntValue("Rear지그", ref tmp);
            return tmp;
        }
    }

    public float 픽셀값
    {
        get
        {
            float tmp = 0f;
            nErrorCode = GetFloatValue("픽셀값", ref tmp);
            return tmp;
        }
    }

    public int index
    {
        get
        {
            int tmp = 0;
            nErrorCode = GetIntValue("index", ref tmp);
            return tmp;
        }
    }

    public float Slot2
    {
        set
        {
            nErrorCode = SetFloatValue("Slot2", value);
        }
    }



}