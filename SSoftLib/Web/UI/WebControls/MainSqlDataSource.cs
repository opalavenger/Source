using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft.Web.UI.WebControls
{
    public class MainSqlDataSource : System.Web.UI.WebControls.SqlDataSource
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            
            this.Inserting += new System.Web.UI.WebControls.SqlDataSourceCommandEventHandler(MainSqlDataSource_Inserting);
            this.Updating += new System.Web.UI.WebControls.SqlDataSourceCommandEventHandler(MainSqlDataSource_Updating);
            this.Deleting += new System.Web.UI.WebControls.SqlDataSourceCommandEventHandler(MainSqlDataSource_Deleting);
        }

        void MainSqlDataSource_Deleting(object sender, System.Web.UI.WebControls.SqlDataSourceCommandEventArgs e)
        {
            if (e.Command.Parameters.Contains("@ModifyUserNo"))
                e.Command.Parameters["@ModifyUserNo"].Value = this.Page.User.Identity.Name;
            if (e.Command.Parameters.Contains("@ModifyDate"))
                e.Command.Parameters["@ModifyDate"].Value = DateTime.Now;
            if (e.Command.Parameters.Contains("@IPAddress"))
                e.Command.Parameters["@IPAddress"].Value = this.Page.Request.UserHostAddress;
        }

        void MainSqlDataSource_Updating(object sender, System.Web.UI.WebControls.SqlDataSourceCommandEventArgs e)
        {
            if (e.Command.Parameters.Contains("@ModifyUserNo"))
                e.Command.Parameters["@ModifyUserNo"].Value = this.Page.User.Identity.Name;
            if (e.Command.Parameters.Contains("@ModifyDate"))
                e.Command.Parameters["@ModifyDate"].Value = DateTime.Now;
            if (e.Command.Parameters.Contains("@IPAddress"))
                e.Command.Parameters["@IPAddress"].Value = this.Page.Request.UserHostAddress;
        }

        void MainSqlDataSource_Inserting(object sender, System.Web.UI.WebControls.SqlDataSourceCommandEventArgs e)
        {
            if (e.Command.Parameters.Contains("@AddUserNo"))
                e.Command.Parameters["@AddUserNo"].Value = this.Page.User.Identity.Name;
            if (e.Command.Parameters.Contains("@AddDate"))
                e.Command.Parameters["@AddDate"].Value = DateTime.Now;
            if (e.Command.Parameters.Contains("@IPAddress"))
                e.Command.Parameters["@IPAddress"].Value = this.Page.Request.UserHostAddress;
        }


    }
}
