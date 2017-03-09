using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;


namespace SSoft.Web.UI.WebControls
{
    [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class OpenWindowButton : System.Web.UI.WebControls.Button, System.Web.UI.INamingContainer
    {
        public OpenWindowButton()
        {
            this.Load += new EventHandler(OpenWindowButton_Load);
        }

        void OpenWindowButton_Load(object sender, EventArgs e)
        {
            this.PreRender += new EventHandler(OpenWindowButton_PreRender);
        }

        void OpenWindowButton_PreRender(object sender, EventArgs e)
        {
            string _iFramePageUrl="";
            string _targetUrl="";
            string _script = "";
            string _openUrl = "";

            if (this.OpenUrl.Trim().StartsWith("~/"))
            {
                _openUrl = this.OpenUrl.Trim().Remove(0, 2);
            }
            else if (this.OpenUrl.Trim().StartsWith("/"))
            {
                _openUrl = this.OpenUrl.Trim().Remove(0, 1);
            }
            else
            {
                _openUrl = this.OpenUrl.Trim();
            }

            switch (this.PopupType)
            {
                case SSoft.Enum.EnumPopupType.General:
                    _iFramePageUrl = "";
                    _targetUrl = this.Page.Request.ApplicationPath + "/" + _openUrl + "?title=" + this.TitleText ;

                    break;
                case SSoft.Enum.EnumPopupType.Modal:
                    _iFramePageUrl = this.Page.Request.ApplicationPath + "/Popup/PopupModalDialog.aspx";
                    _targetUrl = _iFramePageUrl + "?title=" + System.Web.HttpUtility.HtmlEncode(this.TitleText) + "&popuppage=" + this.Page.Request.ApplicationPath + "/" + _openUrl ;

                    break;
                case SSoft.Enum.EnumPopupType.Modeless:
                    _iFramePageUrl = this.Page.Request.ApplicationPath + "/Popup/PopupModelessDialog.aspx";
                    _targetUrl = _iFramePageUrl + "?title=" + System.Web.HttpUtility.HtmlEncode(this.TitleText) + "&popuppage=" + this.Page.Request.ApplicationPath + "/" + _openUrl ;

                    break;
                default:
                    break;
            }

            
            switch (this.PopupType)
            {
                case SSoft.Enum.EnumPopupType.General:
                    _script = string.Format("window.open('{0}','',menubar=no;titlebar=no;toolbar=no;status=no;resizable=yes;dialogWidth={1}px;dialogHeight={2}px;');return;", _targetUrl, this.OpenWindowWidth, this.OpenWindowHeight);
                    break;
                case SSoft.Enum.EnumPopupType.Modal:
                    if (this.ControlForReturnValue == "")
                    {
                        _script = string.Format("window.showModalDialog('{0}',window,'center=yes;help=no;status=no;resizable=yes;dialogWidth={1}px;dialogHeight={2}px;');return;", _targetUrl, this.OpenWindowWidth, this.OpenWindowHeight);
                    }
                    else
                    {

                        _script = string.Format("var returnvalue = window.showModalDialog('{0}',window,'center=yes;help=no;status=no;resizable=yes;dialogWidth={1}px;dialogHeight={2}px;'); if(returnvalue!=undefined) {{ {3}.value= returnvalue;}} return;", _targetUrl, this.OpenWindowWidth, this.OpenWindowHeight, this.Parent.FindControl(this.ControlForReturnValue).ClientID);
                   
                    }
                     break;
                case SSoft.Enum.EnumPopupType.Modeless:
                    _script = string.Format("window.showModelessDialog('{0}',window,'center=yes;help=no;status=no;resizable=yes;dialogWidth={1}px;dialogHeight={2}px;');return;", _targetUrl, this.OpenWindowWidth, this.OpenWindowHeight);
                    break;
                default:
                    break;
            }

  
            this.OnClientClick = _script;
        }


        [Browsable(true)]
        [Description("Gets and sets Url method")]
        [Category("SSoft")]
        [EditorAttribute(typeof(System.Web.UI.Design.UrlEditor), typeof(System.Drawing.Design.UITypeEditor))]
        public virtual string OpenUrl
        {
            get
            {
                if (ViewState["OpenUrl"] == null)
                    ViewState["OpenUrl"] = "";
                return ViewState["OpenUrl"].ToString();
            }
            set
            {
                //if(value != null)
                //    ViewState["OpenUrl"] = value.Trim();

                if (value.Trim().StartsWith("~/"))
                {
                    ViewState["OpenUrl"] = value.Trim().Remove(0, 2);
                }
                else if (value.Trim().StartsWith("/"))
                {
                    ViewState["OpenUrl"] = value.Trim().Remove(0, 1);
                }
                else
                {
                    ViewState["OpenUrl"] = value.Trim();
                }
            }
        }

        //[TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        //public virtual Uri OpenUrl1
        //{
        //    get
        //    {
        //        if (ViewState["OpenUrl1"] == null)
        //            ViewState["OpenUrl1"] = "";
        //        return (Uri)ViewState["OpenUrl1"];
        //    }
        //    set
        //    {
        //        ViewState["OpenUrl1"] = value;
        //    }
        //}



        [Browsable(true)]
        [Description("Gets and sets PopupType method")]
        [Category("SSoft")]
        public virtual SSoft.Enum.EnumPopupType PopupType
        {
            get
            {
                if (ViewState["PopupType"] == null)
                    ViewState["PopupType"] = SSoft.Enum.EnumPopupType.Modal;
                return (SSoft.Enum.EnumPopupType)ViewState["PopupType"];
            }
            set
            {
                ViewState["PopupType"] = value;
            }
        }

        //[Browsable(true)]
        //[Description("Gets and sets Target method")]
        //[Category("SSoft")]
        //public virtual TargetTypeEnum Target
        //{
        //    get
        //    {
        //        if (ViewState["Target"] == null)
        //            ViewState["Target"] = TargetTypeEnum._top;
        //        return (TargetTypeEnum)ViewState["Target"];
        //    }
        //    set
        //    {
        //        ViewState["Target"] = value;
        //    }
        //}
        [TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
        [Bindable(true)]
        [Browsable(true)]
        [Description("Gets and sets ControlForReturnValue method")]
        [Category("SSoft")]
        public virtual string ControlForReturnValue
        {
            get
            {
                if (ViewState["ControlForReturnValue"] == null)
                   return "";
                return ViewState["ControlForReturnValue"].ToString();
            }
            set
            {
                ViewState["ControlForReturnValue"] = value;
            }
        }

        

        [Browsable(true)]
        [Description("Gets and sets TitleText method")]
        [Category("SSoft")]
        public virtual string TitleText
        {
            get
            {
                if (ViewState["TitleText"] == null)
                    ViewState["TitleText"] = "";
                return ViewState["TitleText"].ToString();
            }
            set
            {
                ViewState["TitleText"] = value;
            }
        }

        [Browsable(true)] 
        [Description("Gets and sets OpenWindowHeight method")]
        [DefaultValue(300)]
        [Category("SSoft")]
        public virtual int OpenWindowHeight
        {
            get
            {
                if (ViewState["OpenWindowHeight"] == null)
                    ViewState["OpenWindowHeight"] = 650;
                return Convert.ToInt32(ViewState["OpenWindowHeight"]);
            }
            set
            {
                ViewState["OpenWindowHeight"] = value;
            }
        }

        [Browsable(true)]
        [Description("Gets and sets OpenWindowHeight method")]
        [DefaultValue(600)]
        [Category("SSoft")]
        public virtual int OpenWindowWidth
        {
            get
            {
                if (ViewState["OpenWindowWidth"] == null)
                    ViewState["OpenWindowWidth"] = 600;
                return Convert.ToInt32(ViewState["OpenWindowWidth"]);
            }
            set
            {
                ViewState["OpenWindowWidth"] = value;
            }
        }


        //[Browsable(true)]
        //[Description("Gets and sets ParamemterParentId Control method")]
        //[DefaultValue("")]
        //[Category("SSoft")]
        //[TypeConverter(typeof(System.Web.UI.WebControls.ControlIDConverter))]
        //public virtual string ParamemterParentIdControlID
        //{
        //    get
        //    {
        //        if (ViewState["ParamemterParentIdControlID"] == null)
        //            ViewState["ParamemterParentIdControlID"] = "";
        //        return ViewState["ParamemterParentIdControlID"].ToString();
        //    }
        //    set
        //    {
        //        ViewState["ParamemterParentIdControlID"] = value;
        //    }
        //}



    }

       
}
