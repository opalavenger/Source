using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SSoft.Data
{
    [DisplayName("AccountSqlDataSource"), DescriptionAttribute("Account SqlDataSource,according to appSettings SSoftSqlMemberPorivider key")]
    public class AccountSqlDataSource : SSoft.Data.SqlDataSourceBase
    {
        public AccountSqlDataSource()
        {
            this.ConnectionString = System.Web.Configuration.WebConfigurationManager.ConnectionStrings[SSoft.Data.AccountSqlDataSource.AccountConnectionStringSessionName].ToString();
        }

        public static string CconnectString
        {
            get { return System.Web.Configuration.WebConfigurationManager.ConnectionStrings[SSoft.Data.AccountSqlDataSource.AccountConnectionStringSessionName].ToString(); }
        }

        public static string AccountConnectionStringSessionName
        {
            get { return System.Web.Configuration.WebConfigurationManager.AppSettings["AccountConnectionStringSessionName"].ToString(); }
        }
    }
}
