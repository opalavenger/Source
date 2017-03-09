using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;


namespace YMIR.Controllers.BAS.API.BAS001
{
    public class BAS001Controller : ApiController
    {
        public dynamic POSTBAS001Query(dynamic value)
        {
            try
            {
                DataTable dt = SSoft.Data.SqlHelper.SelectTable("select  [SYS_NO],[DOOR] from IRF030");

                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "查詢成功", dt);
            }
            catch(Exception ex)
            {
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex);
            }
        }
    }
}
