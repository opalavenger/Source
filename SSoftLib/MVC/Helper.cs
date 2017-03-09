using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;

namespace SSoft.MVC
{
     //if (System.Web.HttpContext.Current.GetOwinContext() != null)
     //                   {
     //                       if (System.Web.HttpContext.Current.GetOwinContext().Request != null)
     //                       {
     //                           returnName = System.Web.HttpContext.Current.GetOwinContext().Request.LocalIpAddress;
                                
     //                       }
     //                   }
    public class Helper
    {
        public static string GetHttpDownloadFileURL(string guid)
        {
            return GetHttpDownloadFileURL(guid, System.Web.HttpContext.Current);
        }
        public static string GetHttpDownloadFileURL(string guid, HttpContext context)
        {
            string returnURL = "";

            if (context != null)
            {
                returnURL = "http://" + context.Request.Url.Host;

                if (context.Request.Url.Port != 80)
                {
                    returnURL += ":" + context.Request.Url.Port;
                }
                if (context.Request.ApplicationPath != "/")
                {
                    returnURL += context.Request.ApplicationPath;
                }
                if (string.IsNullOrEmpty(guid))
                {
                    returnURL = null;
                }
                else
                {
                    returnURL += "/api/Common/POSTDownloadFile?id=" + guid;
                }
            }
           
            else
            {
                ////if (string.IsNullOrEmpty(guid))
                ////{
                ////    returnURL = System.Configuration.ConfigurationManager.AppSettings["UserUploadsPath"].ToString() + "/Handler/DownloadFileHandler.ashx";
                ////}
                ////else
                ////{
                ////    returnURL = System.Configuration.ConfigurationManager.AppSettings["UserUploadsPath"].ToString() + "/Handler/DownloadFileHandler.ashx?guid=" + guid;
                ////}

            }

            //if (string.IsNullOrEmpty(guid))
            //{
            //    returnURL += "/UserUploads/" + fileName;
            //}
            //else
            //{
            //    returnURL += "/UserUploads/" + guid + "/" + fileName;
            //}


            return returnURL;
        }
        public static bool ObjectComparator<T>(T self, T to, params string[] ignore) where T : class
        {
            if (self != null && to != null)
            {
                Type type = typeof(T);
                List<string> ignoreList = new List<string>(ignore);
                foreach (System.Reflection.PropertyInfo pi in type.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                {
                    if (!ignoreList.Contains(pi.Name))
                    {
                        var property = type.GetProperty(pi.Name);
                        var dde = property.PropertyType.IsClass;
                        //if (typeof(IEnumerable).IsAssignableFrom(pi.PropertyType))
                        //+		[System.Reflection.RuntimePropertyInfo]	{System.Collections.Generic.ICollection`1[AERP.Models.EMPEQUIP] EMPEQUIP}	System.Reflection.RuntimePropertyInfo

                        if (!property.PropertyType.IsConstructedGenericType )
                        {

                            object selfValue = type.GetProperty(pi.Name).GetValue(self, null);
                            object toValue = type.GetProperty(pi.Name).GetValue(to, null);

                            if (selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue)))
                            {
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            return self == to;
        }

        //public static bool ObjectComparator<T>(this T self, T to, params string[] ignore) where T : class
        //{
        //    if (self != null && to != null)
        //    {
        //        var type = typeof(T);
        //        var ignoreList = new List<string>(ignore);
        //        var unequalProperties =
        //            from pi in type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
        //            where !ignoreList.Contains(pi.Name)
        //            let selfValue = type.GetProperty(pi.Name).GetValue(self, null)
        //            let toValue = type.GetProperty(pi.Name).GetValue(to, null)
        //            where selfValue != toValue && (selfValue == null || !selfValue.Equals(toValue))
        //            select selfValue;
        //        return !unequalProperties.Any();
        //    }
        //    return self == to;
        //}
    }
}
