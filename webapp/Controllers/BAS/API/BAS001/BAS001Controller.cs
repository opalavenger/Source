using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;

namespace YMIR.Controllers.BAS.API.BAS001
{
    public class BAS001Controller : ApiController
    {
        public dynamic POSTBAS001Query(dynamic value)
        {
            try
            {
                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                var condition = current["condition"];

                string sys_no = "";
                if (condition["SYS_NO"] != null)
                {
                    sys_no = condition["SYS_NO"].ToString();
                }
                string door ="";
                if (condition["DOOR"] != null)
                {
                    door = condition["DOOR"].ToString();
                }
                DataTable dt = SSoft.Data.SqlHelper.SelectTable("select [Id], [SYS_NO],[DOOR] from IRF030 where SYS_NO=@SYS_NO or @SYS_NO=''", new SqlParameter[] { new SqlParameter("@SYS_NO", sys_no) });

                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "查詢成功", dt);
            }
            catch(Exception ex)
            {
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex);
            }
        }

        public dynamic POSTBAS001Delete(dynamic value)
        {
            try
            {
                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                var id = current["key"].ToString();

                SSoft.Data.SqlHelper.SelectTable("delete IRF030 where [Id]=@Id ", new SqlParameter[] { new SqlParameter("@Id", id) });

                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "刪除成功");
            }
            catch (Exception ex)
            {
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex);
            }
        }

        public dynamic POSTBAS001DeleteCheck(dynamic value)
        {
            try
            {
                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                var datas = current["data"];

                foreach (var item in datas)
                {
                    if (item["isDelete"] != null)
                    {
                        bool isDelete = Convert.ToBoolean(item["isDelete"]);
                        if (isDelete)
                        {
                            string id = item["Id"].ToString();
                            SSoft.Data.SqlHelper.SelectTable("delete IRF030 where [Id]=@Id ", new SqlParameter[] { new SqlParameter("@Id", id) });
                        }
                    }
                }

                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "刪除成功");
            }
            catch (Exception ex)
            {
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex);
            }
        }
    }
}
