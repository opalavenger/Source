using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Reflection;
using System.Web.Routing;

namespace SSoft.MVC.Controls
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public sealed class TooBarItemAttribute : Attribute
    {
        public string ToolTip { get; set; }
    }
    public enum ToolBarAlignment
    {
        Top,
        Bottom,
    }

    public class ToolBars : IDisposable
    {
        private Type _controllerType;
        private HttpContextBase _context;
        private string _formname;
        private FormMethod _formMethod;
        private HtmlHelper _helper;
        //private ToolBarAlignment _toolBarAlignment;

        //private string _form = "<form id=\"{0}_Form\" name=\"{0}_Form\" action=\"{1}\" method=\"{2}\">";


        public ToolBars(HtmlHelper helper, Type controllerType, string formName) :
            this(helper, controllerType, formName, FormMethod.Post)
        {

        }

        private ToolBars(HtmlHelper helper, Type controllerType, string formName, FormMethod formMethod)
        {
            _helper = helper;
            _context = helper.ViewContext.HttpContext;
            _controllerType = controllerType;
            _formname = formName;
            _formMethod = formMethod;

            WriteStartTag();
            //if (_toolBarAlignment == ToolBarAlignment.Top)
                WriteToolBarItems();
        }

        private void WriteStartTag()
        {
            _context.Response.Write(RenderJavaScript());
           // _context.Response.Write(String.Format(_form, _formname, "/", System.Enum.GetName(typeof(FormMethod), _formMethod)));
        }

        private string RenderJavaScript()
        {
            StringBuilder JavaScript = new StringBuilder();
            JavaScript.AppendFormat(@"<script language=""javascript"" type=""text/javascript"">{0}", Environment.NewLine);
            JavaScript.AppendFormat(@"  function OnToolBarClick(action){0}", Environment.NewLine);
            JavaScript.AppendFormat(@"  {{{0}", Environment.NewLine);
            JavaScript.AppendFormat(@"      var form =  document.getElementById(""{1}_Form"");{0}", Environment.NewLine, _formname);
            JavaScript.AppendFormat(@"      form.action = action;{0}", Environment.NewLine);
            JavaScript.AppendFormat(@"      form.submit();{0}", Environment.NewLine);
            JavaScript.AppendFormat(@"   }}{0}", Environment.NewLine);
            JavaScript.AppendFormat(@"</Script>{0}", Environment.NewLine);
            return JavaScript.ToString();
        }

        private void WriteToolBarItems()
        {
            //var methods = from method in _controllerType.GetMethods()
            //              where method.GetCustomAttributes(typeof(TooBarItemAttribute), false).Length > 0 & method.GetParameters().Length == 0
            //              select method;

            //StringBuilder sb = new StringBuilder();



            //sb.AppendFormat(@"<div>{0}", Environment.NewLine);

            //if (methods.Count() == 0)
            //    sb.AppendFormat(@"<span><strong>{0} doesn't have any action with Attribute ToolBarItem<Strong></span>", _controllerType.Name);

            //foreach (MethodInfo method in methods)
            //{
            //    sb.AppendFormat(@"<span>{0}", Environment.NewLine);
            //    sb.AppendFormat(@"<button onclick=""OnToolBarClick('{1}')"" value=""{2}"">{2}</button>{0}", Environment.NewLine, GetActionUrl(method.Name), GetActionToolTip(method));
            //    sb.AppendFormat(@"</span>{0}", Environment.NewLine);
            //}

            //sb.AppendFormat(@"</div>{0}", Environment.NewLine);
            //_context.Response.Write(sb.ToString());

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@"<span data-toggle=""tooltip"" class=""tooltipLink"" data-original-title=""資料查詢"">{0}", Environment.NewLine);
            
            sb.AppendFormat(@"<button type=""button"" class=""btn btn-default"">{0}", Environment.NewLine);
            sb.AppendFormat(@"<span class=""glyphicon""><img src=""../Images/ToolBar/glyphicons-529-database-search.png"" width=""15"" height=""15"" title=""資料查詢"" /></span>{0}", Environment.NewLine);
            sb.AppendFormat(@"</button>{0}", Environment.NewLine);
            sb.AppendFormat(@"</span>{0}", Environment.NewLine);
            _context.Response.Write(sb.ToString());
        }

        private string GetActionToolTip(MethodInfo method)
        {
            return (method.GetCustomAttributes(typeof(TooBarItemAttribute), false).SingleOrDefault() as TooBarItemAttribute).ToolTip;
        }

        private string GetActionUrl(string actionName)
        {

            string controllerName = _controllerType.Name;

            if (controllerName.EndsWith("Controller", StringComparison.OrdinalIgnoreCase))
            {
                controllerName = controllerName.Remove(controllerName.Length - 10, 10);
            }

            RouteValueDictionary values = new RouteValueDictionary();

            values = values ?? new RouteValueDictionary();
            values.Add("controller", controllerName);
            values.Add("action", actionName);

            VirtualPathData vpd = RouteTable.Routes.GetVirtualPath(_helper.ViewContext.RequestContext, values);
            return (vpd == null) ? null : vpd.VirtualPath;

        }

        private void WriteEndTag()
        {

            //if (_toolBarAlignment == ToolBarAlignment.Bottom)
            //    WriteToolBarItems();
            _context.Response.Write("</form>");
        }


        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool dispose)
        {
            if (dispose)
                WriteEndTag();
        }

        #endregion
    }
}
