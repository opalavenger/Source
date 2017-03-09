using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SSoft.Configuration
{
    public class WebConfig
    {
        /// <summary>
        /// MesaageImageURL
        /// </summary>
        public static string FilePath
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"] == null)
                {

                    return "";

                }
                return System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"].ToString();
            }
        }

        /// <summary>
        /// MesaageImageURL
        /// </summary>
        public static string Language
        {
            get
            {
                if (System.Web.Configuration.WebConfigurationManager.AppSettings["Language"] == null)
                {

                    return "en";

                }
                return System.Web.Configuration.WebConfigurationManager.AppSettings["Language"].ToString().ToLower();
            }
        }
    }
}
