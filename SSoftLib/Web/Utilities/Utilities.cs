using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SSoft.Web
{
    public class Utilities
    {
        public static object GetObjectValue(object _object)
        {
            object _returnValue = null;
            if (_object is Label)
            {
                _returnValue = ((Label)_object).Text;
            }
            else if (_object is TextBox)
            {
                _returnValue = ((TextBox)_object).Text;
            }
            else if (_object is RadioButton)
            {
                _returnValue = ((RadioButton)_object).Checked;
            }
            else if (_object is RadioButtonList)
            {
                _returnValue = ((RadioButtonList)_object).SelectedValue;
            }
            else if (_object is CheckBox)
            {
                _returnValue = ((CheckBox)_object).Checked;
            }
            else if (_object is CheckBoxList)
            {
                _returnValue = ((CheckBoxList)_object).SelectedValue;
            }
            else if (_object is DropDownList)
            {
                _returnValue = ((DropDownList)_object).SelectedValue;
            }
            else if (_object is Literal)
            {
                _returnValue = ((Literal)_object).Text;
            }
            else if (_object is HiddenField)
            {
                _returnValue = ((HiddenField)_object).Value;
            }

            return _returnValue;

        }

        public static object GetObjectValue(Control _userControl, string _objectName)
        {
            object _object = _userControl.FindControl(_objectName);
            if (_object == null)
            {
                throw new Exception(string.Format("{0}物件不存在"));
            }
            return GetObjectValue(_object);

        }

        public static void SetObjectValue(Control _userControl, string _objectName, string _value)
        {
            object _object = _userControl.FindControl(_objectName);
            if (_object == null)
            {
                throw new Exception(string.Format("{0}物件不存在"));
            }
            SetObjectValue(_object, _value);

        }

        public static void SetObjectValue(object _object, string _value)
        {
            if (_object is Label)
            {
                ((Label)_object).Text = _value;
            }
            else if (_object is TextBox)
            {
                ((TextBox)_object).Text = _value;
            }
            else if (_object is RadioButton)
            {
                ((RadioButton)_object).Checked = Convert.ToBoolean(_value);
            }
            else if (_object is RadioButtonList)
            {
                ((RadioButtonList)_object).SelectedValue = _value;
            }
            else if (_object is CheckBox)
            {
                ((CheckBox)_object).Checked = Convert.ToBoolean(_value);
            }
            else if (_object is CheckBoxList)
            {
                ((CheckBoxList)_object).SelectedValue = _value;
            }
            else if (_object is DropDownList)
            {
                ((DropDownList)_object).SelectedValue = _value;
            }
            else if (_object is Literal)
            {
                ((Literal)_object).Text = _value;
            }
            else if (_object is HiddenField)
            {
                ((HiddenField)_object).Value = _value;
            }


        }

        //轉換subString news title
        public static string SubStringTitle(String str_value)
        {
            return SubStringTitle(str_value, 40);
        }

        ////轉換subString news title
        //public static string SubStringTitle(String str_value)
        //{
        //    return SubStringTitle(str_value, 40);
        //}

        public static string SubStringTitle(String str_value, int length)
        {
            int byteLen = GetByteSize(str_value);

            //if (byteLen <= length * 2)
            //{
            //    return str_value;
            //}
            //else
            //{
            string returnString = str_value;
            for (int i = length; i <= str_value.Length; i++)
            {
                returnString = str_value.Substring(0, i);
                int renbyteLen = GetByteSize(returnString);
                if (renbyteLen >= length * 2)
                {
                    return returnString + "...";
                }
            }

            return returnString;
            //}
        }

        public static int GetByteSize(string s)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            return bytes.Length;
        }

    }
}
