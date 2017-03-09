using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft
{
    public class Utility
    {
        public static void Swap<T>(ref T a,ref T b)
        {
            T tmp = a;
            a = b;
            b = tmp;
        }

        public static string CombineReceiveDataString(ref string resault, string _clientID, string _value, TagType _type, string flag)
        {
            if (resault != null && resault != "")
            {
                resault = resault + "|" + _clientID + "|" + _value + "|" + _type.ToString().ToLower() + "|" + flag.ToLower();
            }
            else
            {
                resault = _clientID + "|" + _value + "|" + _type.ToString().ToLower() + "|" + flag.ToLower();
            }

            return "";

        }

        public static string CombineReceiveDataString(ref string resault, string _clientID, string _value, TagType _type, bool _flag)
        {
            if (resault != null && resault != "")
            {
                resault = resault + "|" + _clientID + "|" + _value + "|" + _type.ToString().ToLower() + "|" + _flag.ToString().ToLower();
            }
            else
            {
                resault = _clientID + "|" + _value + "|" + _type.ToString().ToLower() + "|" + _flag.ToString().ToLower();
            }

            return "";

        }

        public static string ArrayToString(string[] _array, string _placehold)
        {
            string _returnValue = "";
            for (int _i = 0; _i < _array.Length; _i++)
            {
                _returnValue += _placehold + _array[_i] + _placehold;
                if (_i < _array.Length - 1)
                {
                    _returnValue += ",";
                }
            }
            return _returnValue;
        }

        //public Object GetItemValue(string _columnName)
        //{
        //    Object _returnObject;
        //    int _findIndex = -1;
        //    for (int _i=0;  _i < this.CurrentRowColumns.Length; _i++)
        //    {
        //        if (this.CurrentRowColumns[_i].ColumnName.ToLower() == _columnName.ToLower())
        //        {
        //            _findIndex = _i;
        //            break;
        //        }
        //    }

        //    if (_findIndex >= 0)
        //    {
        //        _returnObject = this.CurrentRowValues[_findIndex];
        //    }
        //    else
        //    {
        //        throw new Exception(string.Format("Column[{0}]不存在", _columnName));
        //    }

        //    return _returnObject;
        //}


        //public string GetTextValue(string _controlName)
        //{
        //    string _returnValue = "";

        //    System.Web.UI.WebControls.WebControl _control = (System.Web.UI.WebControls.WebControl)this.FindControl(_controlName);

        //    if(_control == null)
        //    {
        //        throw new Exception(string.Format("Control[{0}]不存在", _controlName));
        //    }

        //    if (_control is System.Web.UI.WebControls.TextBox)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.TextBox)_control).Text;
        //    }
        //    else if (_control is System.Web.UI.WebControls.Label)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.Label)_control).Text;
        //    }
        //    else if (_control is System.Web.UI.WebControls.DropDownList)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.DropDownList)_control).SelectedValue;
        //    }
        //    else if (_control is System.Web.UI.WebControls.ListBox)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.ListBox)_control).SelectedValue;
        //    }
        //    else if (_control is System.Web.UI.WebControls.RadioButtonList)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.RadioButtonList)_control).SelectedValue;
        //    }
        //    else if (_control is System.Web.UI.WebControls.RadioButton)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.RadioButton)_control).Checked.ToString();
        //    }
        //    else if (_control is System.Web.UI.WebControls.CheckBoxList)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.CheckBoxList)_control).SelectedValue;
        //    }
        //    else if (_control is System.Web.UI.WebControls.CheckBox)
        //    {
        //        _returnValue = ((System.Web.UI.WebControls.CheckBox)_control).Checked.ToString();
        //    }

        //    return _returnValue;
        //}



    }

    public enum TagType
    {
        Text,
        Span
    }
}
