using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using SSoft.MVC;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace YMIR.App_Code
{
    public class Utils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="programId">操作紀錄型態(0:登入 1:團購 2:活動 ..etc)</param>
        /// <param name="actionType">操作類型(0:登入 1:建立 2:修改 3:刪除 4:查詢)</param>
        /// <param name="comment"></param>
        /// <param name="key">ID編號(團購ID、活動ID、訊息ID等，若無ID則填入0，如登入)</param>
        public static void InsertActionLog(int programId,int actionType,string comment,string key)
        {
            
            string sql = @"INSERT INTO ActionLog
                                           ([ActionLog_Action],[ActionLog_Id],[ActionLog_UserNO],
                                           [ActionLog_Source],[ActionLog_Type],[ActionLog_Comment],[ActionLog_CreateTime]
                                           )
                                     VALUES
                                           (@ActionLog_Action,@ActionLog_Id,@ActionLog_UserNO,
                                           @ActionLog_Source,@ActionLog_Type,@ActionLog_Comment,@ActionLog_CreateTime)";

            List<SqlParameter> _p = new List<SqlParameter>();
            _p.Add(new SqlParameter("@ActionLog_Action", (object)programId));
            _p.Add(new SqlParameter("@ActionLog_Id",key));
            _p.Add(new SqlParameter("@ActionLog_UserNO", SSoft.Web.Security.User.Emp_ID));
            _p.Add(new SqlParameter("@ActionLog_Source", 1));
            _p.Add(new SqlParameter("@ActionLog_Type", (object)actionType));
            _p.Add(new SqlParameter("@ActionLog_Comment", string.IsNullOrWhiteSpace(comment) ? "" : comment.Trim()));
            _p.Add(new SqlParameter("@ActionLog_CreateTime", DateTime.Now));
            SSoft.Data.SqlHelper.SelectNonQuery(sql, _p.ToArray());
        }

        public static dynamic GetJsonConvertDeserializeObject(dynamic data)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(data.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
        }

        public static string GetJsonConvertSerializeString(dynamic dddwWrapper)
        {
            return JsonConvert.SerializeObject(dddwWrapper, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
        }

        public static string ReturnJsonMessageAndData(dynamic message, dynamic data)
        {
            JObject jObject = new JObject();
            jObject.Add(new JProperty("message", JToken.FromObject(message)));
            jObject.Add(new JProperty("data", JToken.FromObject(data)));
            jObject.Add(new JProperty("iserror", JToken.FromObject(false)));

            return JsonConvert.SerializeObject(jObject, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
        }
        public static string ReturnJsonMessageAndData(dynamic message, dynamic data, bool isError)
        {
            return ReturnJsonMessageAndData(message, data, isError, null);
        }
        public static string ReturnJsonMessageAndData(dynamic message, dynamic data, bool isError, object data1)
        {
            JObject jObject = new JObject();
            
            jObject.Add(new JProperty("message", JToken.FromObject(message)));
            jObject.Add(new JProperty("data", JToken.FromObject(data, new JsonSerializer() { ReferenceLoopHandling= ReferenceLoopHandling.Ignore , DateTimeZoneHandling = DateTimeZoneHandling.Local })));
            if(data1!=null)
            {
                jObject.Add(new JProperty("data1", JToken.FromObject(data1, new JsonSerializer() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore, DateTimeZoneHandling = DateTimeZoneHandling.Local })));

            }
            jObject.Add(new JProperty("iserror", JToken.FromObject(isError)));

            return JsonConvert.SerializeObject(jObject, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
        }
        public static dynamic ReturnResponseJsonMessageAndData(System.Net.Http.HttpRequestMessage request, dynamic message)
        {
            return ReturnResponseJsonMessageAndData(request, message.ToString(), "", false);
        }
        public static dynamic ReturnResponseJsonMessageAndData(System.Net.Http.HttpRequestMessage request, Exception ex)
        {
            return ReturnResponseJsonMessageAndData(request, ReturnMessage(ex), "", true);
        }
        public static dynamic ReturnResponseJsonMessageAndData(System.Net.Http.HttpRequestMessage request, dynamic message, bool isError)
        {
            return ReturnResponseJsonMessageAndData(request, ReturnMessage(message), "", isError);
        }
        public static dynamic ReturnResponseJsonMessageAndData(System.Net.Http.HttpRequestMessage request, dynamic message, dynamic data)
        {
            return ReturnResponseJsonMessageAndData(request, message, data, false);
        }
        public static dynamic ReturnResponseJsonMessageAndData(HttpRequestMessage request, dynamic message, dynamic data, bool isError)
        {
            var response = request.CreateResponse();
            string content = ReturnJsonMessageAndData(message, data, isError);
            response.Content = new StringContent(content);
            response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }

        public static string ReturnMessage(string message)
        {
            if (message.Contains("插入重複的索引鍵資料列"))
            {
                message = "資料重複";
            }
            else if(message.Contains("REFERENCE 條件約束"))
            {
                message = "無法刪除,因有明細資料或被參考!";
            }
            else if (message.Contains("REFERENCE constraint"))
            {
                message = "無法刪除,因有明細資料或被參考!";
            }
            return message;
        }
        public static string ReturnMessage(Exception ex)
        {
            string message = ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "");
            
            return ReturnMessage(message);
        }


        //public static string ReturnJsonMessageAndData(dynamic message, string data, bool isError)
        //{
        //    JObject jObject = new JObject();
        //    jObject.Add(new JProperty("message", JToken.FromObject(message)));
        //    jObject.Add(new JProperty("data",data));
        //    jObject.Add(new JProperty("iserror", JToken.FromObject(isError)));

        //    return JsonConvert.SerializeObject(jObject, Formatting.None,
        //            new JsonSerializerSettings()
        //            {
        //                ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
        //                DateTimeZoneHandling = DateTimeZoneHandling.Local
        //            });
        //}


    }
}