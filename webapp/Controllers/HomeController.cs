#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

#endregion

namespace YMIR.Controllers
{
    [Authorize]
    public class HomeController : SYSBase.BaseController
    {
        // GET: home/index
        public ActionResult Index()
        {
            if (this.IsAllow == false)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                this.Session.Abandon();
                return this.RedirectToAction("Index", "Home");
            }
            this.ProgramNo = "";
            this.ViewBag.Title = "";
            this.ChildTables = new List<string>() { };
            //定義DropDownList Source
            this.DDDWWrapper = new
            {
                // DDDW_UserInfo = Common.DDDWSource.DDDW_UserInfo()
            };

            this.ViewBag.RenderJavascript = this.GetRenderJavascript();
            return View();
        }

        public ActionResult Social()
        {
            return View();
        }

        // GET: home/inbox
        public ActionResult Inbox()
        {
            return View();
        }

        // GET: home/widgets
        public ActionResult Widgets()
        {
            return View();
        }

        // GET: home/chat
        public ActionResult Chat()
        {
            return View();
        }
    }
}