using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Drawing;
using System.ComponentModel;

namespace SSoft.Web.UI.WebControls.Buttons
{
    public class PopupButton : System.Web.UI.WebControls.Button
    {
    //    public PopupButton()
    //    {
    //        this.Text = "...";
    //        this.Load += new EventHandler(PopupButton_Load);
    //        this.PreRender += new EventHandler(PopupButton_PreRender);
    //    }

    //    void PopupButton_PreRender(object sender, EventArgs e)
    //    {
    //        string _url = "/Popup/PopupModalDialog.aspx";
    //        string _urlPopupPage;
    //        //this.OnClientClick = string.Format("alert('{0}+{1}')", this.Parent.FindControl(this.DataDisplayControlID).ClientID, this.Parent.FindControl(this.DataValueControlID).ClientID);
    //        //this.OnClientClick = string.Format("ShowPopupDialog('{0}','{1}','{2}');", this.Parent.FindControl(this.DataDisplayControlID).ClientID, this.Parent.FindControl(this.DataValueControlID).ClientID, this.Page.Request.ApplicationPath + "/Popup/PopupDialog.aspx");
    //        if (this.PopupType == PopupType.Calendar)
    //        {

    //            _urlPopupPage = "Forms/PopupCalendar.aspx";
    //        }
    //        else
    //        {
    //            _urlPopupPage = "Forms/PopupDialog.aspx";
    //        }

    //        if (this.PopupChildType == PopupChildType.Modal)
    //        {

    //        }
    //        else if (this.PopupChildType == PopupChildType.Modeless)
    //        {

    //        }
    //        else if (this.PopupChildType == PopupChildType.Open)
    //        {

    //        }
    //        _url = this.Page.Request.ApplicationPath + _url + "?popuppage=" + _urlPopupPage + "&displaycontrol=" + this.Parent.FindControl(this.DataDisplayControlID).ClientID + "&valuecontrol=" + this.Parent.FindControl(this.DataValueControlID).ClientID;
    //        this.OnClientClick = string.Format("ShowPopupDialog('{0}','{1}','{2}');", this.Parent.FindControl(this.DataDisplayControlID).ClientID, this.Parent.FindControl(this.DataValueControlID).ClientID, _url);
     
    //    }

    //    void PopupButton_Load(object sender, EventArgs e)
    //    {
            
    //    }

    //    [Browsable(true)]
    //    [Description("Gets and sets Display Control method")]
    //    [DefaultValue("")]
    //    [Category("SSoft")]
    //    [TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
    //    public virtual string DataDisplayControlID
    //    {
    //        get
    //        {
    //            if (ViewState["DataDisplayControlID"] == null)
    //                ViewState["DataDisplayControlID"] = "";
    //            return ViewState["DataDisplayControlID"].ToString();
    //        }
    //        set
    //        {
    //            ViewState["DataDisplayControlID"] = value;
    //        }
    //    }

    //    [Browsable(true)]
    //    [Description("Gets and sets Value Control method")]
    //    [DefaultValue("")]
    //    [Category("SSoft")]
    //    [TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
    //    public virtual string DataValueControlID
    //    {
    //        get
    //        {
    //            if (ViewState["DataValueControlID"] == null)
    //                ViewState["DataValueControlID"] = "";
    //            return ViewState["DataValueControlID"].ToString();
    //        }
    //        set
    //        {
    //            ViewState["DataValueControlID"] = value;
    //        }
    //    }

    //    [Browsable(true)]
    //    [Description("Gets and sets PopupSourceID Control method")]
    //    [DefaultValue("")]
    //    [Category("SSoft")]
    //    public virtual string PopupSourceID
    //    {
    //        get
    //        {
    //            if (ViewState["PopupSourceID"] == null)
    //                ViewState["PopupSourceID"] = "";
    //            return ViewState["PopupSourceID"].ToString();
    //        }
    //        set
    //        {
    //            ViewState["PopupSourceID"] = value;
    //        }
    //    }

    //    [Browsable(true)]
    //    [Description("Gets and sets PopupType method")]
    //    [Category("SSoft")]
    //    public virtual PopupType PopupType
    //    {
    //        get
    //        {
    //            if (ViewState["PopupType"] == null)
    //                ViewState["PopupType"] = PopupType.ValueAndDisplay;
    //            return (PopupType)ViewState["PopupType"];
    //        }
    //        set
    //        {
    //            ViewState["PopupType"] = value;
    //        }
    //    }

    //    [Browsable(true)]
    //    [Description("Gets and sets PopupChildType method")]
    //    [Category("SSoft")]
    //    public virtual PopupChildType PopupChildType
    //    {
    //        get
    //        {
    //            if (ViewState["PopupChildType"] == null)
    //                ViewState["PopupChildType"] = PopupType.ValueAndDisplay;
    //            return (PopupChildType)ViewState["PopupChildType"];
    //        }
    //        set
    //        {
    //            ViewState["PopupChildType"] = value;
    //        }
    //    }

    }

    //public enum PopupType
    //{
    //    ValueAndDisplay,
    //    Calendar
    //}

    //public enum PopupChildType
    //{
    //    Modal,
    //    Modeless,
    //    Open
    //}
}
