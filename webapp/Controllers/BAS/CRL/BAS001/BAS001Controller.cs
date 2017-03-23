using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YMIR.Controllers.BAS.CRL.BAS001
{
    public class BAS001Controller : SYSBase.BaseController
    {
        // GET: BAS001
        public ActionResult Index()
        {

            if (this.IsAllow != false)
            {
                System.Web.Security.FormsAuthentication.SignOut();
                this.Session.Abandon();
                return this.RedirectToAction("Index", "Home");
            }
            this.ProgramNo = "BAS001";
            this.ViewBag.Title = "基本資料";
            this.ChildTables = new List<string>() { };
            //定義DropDownList Source
            this.DDDWWrapper = new
            {
                DDDW_UserInfo = Common.DDDWSource.DDDW_UserInfo()
            };

            this.ViewBag.RenderJavascript = this.GetRenderJavascript();
            return View();
        }
    }
}