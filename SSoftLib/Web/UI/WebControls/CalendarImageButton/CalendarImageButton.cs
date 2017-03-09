using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace SSoft.Web.UI.WebControls
{
    public class CalendarImageButton : System.Web.UI.WebControls.ImageButton
    {
        public CalendarImageButton()
        {
            //this.OnClientClick = "return false;";
        }

        protected override void OnLoad(EventArgs e)
        {
            this.Page.ClientScript.RegisterClientScriptInclude("calendar", this.Page.Request.ApplicationPath + "/Javascripts/jscalendar-1.0/calendar.js");
            this.Page.ClientScript.RegisterClientScriptInclude("calendarbig5utf8", this.Page.Request.ApplicationPath + "/Javascripts/jscalendar-1.0/lang/calendar-big5-utf8.js");
            this.Page.ClientScript.RegisterClientScriptInclude("calendarsetup", this.Page.Request.ApplicationPath + "/Javascripts/jscalendar-1.0/calendar-setup.js");

            base.OnLoad(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            WebControl _targetControl = (WebControl)this.Parent.FindControl(this.TargetControlID);
            if (_targetControl == null) return;
            string _js = "Calendar.setup({ inputField : '" + _targetControl.ClientID + "',ifFormat : '%Y/%m/%d',button:'" + this.ClientID + "', weekNumbers : false });";
            this.Page.ClientScript.RegisterStartupScript(this.GetType(), this.ClientID, _js,true);
        }
        
        [Browsable(true)]
        [Description("Gets and sets Target Value Control method")]
        [DefaultValue("")]
        [Category("Fund")]
        [TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
        public virtual string TargetControlID
        {
            get
            {
                if (ViewState["TargetControlID"] == null)
                    ViewState["TargetControlID"] = "";
                return ViewState["TargetControlID"].ToString();
            }
            set
            {
                ViewState["TargetControlID"] = value;
            }
        }

    }

    
}
