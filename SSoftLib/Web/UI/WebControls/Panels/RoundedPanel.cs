using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SSoft.Web.UI.WebControls
{
    [TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class RoundedPanel : System.Web.UI.WebControls.Panel 
    {

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Page.RegisterRequiresControlState(this);
        }


        #region Property

        private string _titleText;

        [Bindable(true)]
        [Browsable(true)]
        [Description("Gets and sets Title Text")]
        [Category("SSoft")]
        [DefaultValue("")]
        public virtual string TitleText
        {
            get
            {
                return this._titleText;
            }
            set
            {
                this._titleText = value;
            }
        }
        private bool _showTitle = true;

        [Bindable(true)]
        [Browsable(true)]
        [Description("Gets and sets ShowTitle Text")]
        [Category("SSoft")]
        [DefaultValue(true)]
        public virtual bool ShowTitle
        {
            get
            {
                return this._showTitle;
            }
            set
            {
                this._showTitle = value;
            }
        }



        private string _panelStyle = "";

        [Bindable(true)]
        [Browsable(true)]
        [Description("Gets and sets PanelStyle Text")]
        [Category("SSoft")]
        [DefaultValue("")]
        public virtual string PanelStyle
        {
            get
            {
                return this._panelStyle;
            }
            set
            {
                this._panelStyle = value;
            }
        }


        #endregion





        protected override void Render(System.Web.UI.HtmlTextWriter writer)
        {
            //<div class="features" ><div class="rounded"><h3><span >查詢條件</span></h3><div class="roundedMain"><div class="divcontent">
            //</div></div><div class="roundedEnd"><div></div></div></div></div>

            if (this.PanelStyle.Trim() != "")
            {
                writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Style, this.PanelStyle);
            }

            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "features");

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "rounded");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.H3);
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Span);
            if (this.TitleText == null || this.TitleText.Trim() == "") this.TitleText = "&nbsp;";
            if (this.ShowTitle)
            {
                writer.Write(this.TitleText);
            }
            else
            {
                writer.Write("&nbsp;");
            }

            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "roundedMain");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "divcontent");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            base.Render(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "roundedEnd");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);

            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            writer.RenderEndTag();

            writer.RenderEndTag();
            writer.RenderEndTag();



        }

        protected override object SaveControlState()
        {
            object statebase = base.SaveControlState();

            object[] state = new object[4];
            state[0] = statebase;
            state[1] = this.TitleText;
            state[2] = this.ShowTitle;
            state[3] = this.PanelStyle;
            return state;
        }

        protected override void LoadControlState(object savedState)
        {
            if (savedState != null)
            {
                object[] state = (object[])savedState;
                if (state[0] != null) { }

                if (state[1] != null)
                {
                    this.TitleText = state[1].ToString();
                }

                if (state[2] != null)
                {
                    this.ShowTitle = Convert.ToBoolean(state[2]);
                }

                if (state[3] != null)
                {
                    this.PanelStyle = Convert.ToString(state[3]);
                }

                base.LoadControlState(state);
            }

        }
    }
}
