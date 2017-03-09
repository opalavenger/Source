using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft.JS
{
    public class AlertMessage
    {
        public static void Show(System.Web.UI.Control _control, string _message)
        {
            
            if (_message.Contains("違反 PRIMARY KEY 條件約束"))
                _message = "資料重複.";
            if (_message.Contains("違反條件約束"))
                _message = "資料重複.";
            if (_message.Contains("違反 UNIQUE KEY 條件約束"))
                _message = "資料重複.";
            if(_message.Contains("無法以唯一索引"))
                _message = "資料重複.";
            //if (_message.Contains("ORA-02292"))
            //    _message = "資料使用中,無法刪除.";
            //if (_message.Contains("ORA-12899"))
            //    _message = "長度過長."; 


            _message = _message.Replace("\r\n", "\\n");
            _message = _message.Replace("\n", "\\n");

            if (System.Web.UI.ScriptManager.GetCurrent(_control.Page) == null)
            {
                _control.Page.ClientScript.RegisterStartupScript(_control.GetType(), "", string.Format("alert(\"{0}\");", _message), true);
            }
            else
            {
                System.Web.UI.ScriptManager.RegisterStartupScript(_control.Page, _control.Page.GetType(), "", string.Format("alert(\"{0}\");", _message), true);
            }
        }


        public static void Show(System.Web.UI.Control _control,Exception ex)
        {

            if (ex.InnerException != null)
            {
                Show(_control, ex.InnerException.Message);
            }
            else
            {
                Show(_control, ex.Message);
            }
        }
    }

    
}
