using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using System.ComponentModel;

namespace SSoft.Data
{
    public class SqlDataSourceBase : System.Web.UI.WebControls.SqlDataSource
    {
        public SqlDataSourceBase()
        {
            this.ConnectionString = MainDatabase.ConnectString;
        }
    }
}
