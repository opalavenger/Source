using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft.Data
{
    public class MainDatabase
    {
        public static string ConnectString
        {
            get { return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[SSoft.Data.MainDatabase.MainConnectionStringSessionName].ConnectionString; }
        }

        public static string MainConnectionStringSessionName
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["MainConnectionStringSessionName"]; }
        }
    }
}
