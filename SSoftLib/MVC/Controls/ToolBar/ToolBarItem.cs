using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace SSoft.MVC.Controls.ToolBar
{
    public class ToolBarItem
    {
        

        public static string GetItemHtmlString(HtmlHelper helper, ToolBarItemType buttonType)
        {
            StringBuilder sb = new StringBuilder();

            string rootpath = VirtualPathUtility.ToAbsolute(buttonType.GetImagePath());

            //sb.AppendFormat(@"<span data-toggle=""tooltip"" class=""tooltipLink"" data-original-title=""{1}"">{0}", Environment.NewLine, buttonType.GetItemName());
            sb.AppendFormat(@"<button type=""button"" class=""btn btn-default"" ng-click=""{1}"" data-title=""{2}"" bs-tooltip data-trigger=""hover"" >{0}", Environment.NewLine, buttonType.GetItemFunctionName(), buttonType.GetItemName());
            sb.AppendFormat(@"<span class=""glyphicon""><img src=""{1}"" width=""15"" height=""15"" title=""{2}"" /> {2}</span>{0}", Environment.NewLine, rootpath, buttonType.GetItemName());
            sb.AppendFormat(@"</button>{0}", Environment.NewLine);
            //sb.AppendFormat(@"</span>{0}", Environment.NewLine);
            
            return sb.ToString();
        }
    }
}
