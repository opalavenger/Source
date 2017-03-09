using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;

namespace SSoft.MVC.Controls
{

    
    /// <summary>
    /// Helper Class to create ToolBar
    /// </summary>
    public static class ToolBarHelper
    {
        /// <summary>
        /// Extension method to create toolbar based on action methods in controller 
        /// </summary>
        /// <param name="helper">Reference to HtmlHelper</param>
        /// <param name="controllerType">Type of controller, This type is used to retieve action methods with attribut ToolBarItem</param>
        /// <param name="formName">Auto generate form name</param>
        /// <param name="alignment">Alignment of Toolbar on page</param>
        /// <returns>Return Toolbar object</returns>
        //public static IHtmlString  ToolBarControl(this HtmlHelper helper, Type controllerType, string formName)
        //{
        //    //return new ToolBar(helper, controllerType, formName);
        //    StringBuilder sb = new StringBuilder();

        //    sb.AppendFormat(@"<span data-toggle=""tooltip"" class=""tooltipLink"" data-original-title=""資料查詢"">{0}", Environment.NewLine);

        //    sb.AppendFormat(@"<button type=""button"" class=""btn btn-default"">{0}", Environment.NewLine);
        //    sb.AppendFormat(@"<span class=""glyphicon""><img src=""../Images/ToolBar/glyphicons-529-database-search.png"" width=""15"" height=""15"" title=""資料查詢"" /></span>{0}", Environment.NewLine);
        //    sb.AppendFormat(@"</button>{0}", Environment.NewLine);
        //    sb.AppendFormat(@"</span>{0}", Environment.NewLine);
        //    return new HtmlString(sb.ToString());
        //}


        public static IHtmlString ToolBarControl(this HtmlHelper helper, SSoft.Enum.PatternType type)
        {
            StringBuilder sb = new StringBuilder();

            switch (type)
            {
                case Enum.PatternType.CFG:
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                    break;
                case Enum.PatternType.Query:
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Query));
                    break;
                case  Enum.PatternType.MSS:
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Add));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Delete));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.FirstRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.PreviousRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.NextRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.LastRow));
                    //sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                    //sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
                    break;
                case Enum.PatternType.MMD:
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Add));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.DetailAdd));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Delete));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.FirstRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.PreviousRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.NextRow));
                    sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.LastRow));
                    //sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                    //sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
                    break;
            }

            return new HtmlString(sb.ToString());
        }
        public static IHtmlString ToolBarControl(this HtmlHelper helper)
        {
            StringBuilder sb = new StringBuilder();
           
            if (helper.ViewContext.Controller is MVC.Interfaces.IPatternCFG)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
            }
            else if (helper.ViewContext.Controller is MVC.Interfaces.IPatternMSS)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Query));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Add));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Delete));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
            }
            else if (helper.ViewContext.Controller is MVC.Interfaces.IPatternMMD)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Query));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Add));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Delete));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Save));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
            }
            else if (helper.ViewContext.Controller is MVC.Interfaces.IPatternBCH)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Run));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
            }
            else if (helper.ViewContext.Controller is MVC.Interfaces.IPatternRPT)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Ptint));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.OnlineHelp));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Exit));
            }

            if (helper.ViewContext.Controller is MVC.Interfaces.IPatternConfirm)
            {
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.Confirm));
                sb.Append(ToolBar.ToolBarItem.GetItemHtmlString(helper, ToolBar.ToolBarItemType.CancelConfirm));
            }

            return new HtmlString(sb.ToString());
            
        }

        

    }
}
