using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace SSoft.Web.UI.WebControls.TabsControlComponents
{
    public class TabsControlContainer : Panel, INamingContainer
    {
        public TabsControlContainer()
        {
            this.CssClass = "ajax-tab-middle";
        }

        protected override void Render(HtmlTextWriter writer)
        {
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "ajax-tab-left");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.RenderEndTag();
            base.Render(writer);
            writer.AddAttribute(System.Web.UI.HtmlTextWriterAttribute.Class, "ajax-tab-right");
            writer.RenderBeginTag(System.Web.UI.HtmlTextWriterTag.Div);
            writer.RenderEndTag();
        }
    }
}
