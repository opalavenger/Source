using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SSoft.Web.Security
{
    public class PNLADMemberShipProvider : System.Web.Security.ActiveDirectoryMembershipProvider
    {
        public PNLADMemberShipProvider():base()
        {
           
        }
        
        public override bool ValidateUser(string username, string password)
        {

            bool rtnValue = base.ValidateUser(username, password);

            //DataTable dt = SSoft.Data.SqlHelper.SelectTable("select emp_name,(select max(LoginDate) from SYS_LoginRecord where LoginId=@emp_no and IsError=0) as LoginDate from DATA_MEMBER where emp_no=@emp_no", new SqlParameter[] { new SqlParameter("@emp_no", username.Trim()) });
            //if (dt.Rows.Count > 0 && password == username.Trim().ToLower() + "." + username.Trim().ToLower())
            //{
            //    rtnValue = true;
            //    User.Emp_Name = dt.Rows[0]["emp_name"].ToString();
            //    User.Last_Logindate = Convert.IsDBNull(dt.Rows[0]["LoginDate"]) ? "無" : Convert.ToDateTime(dt.Rows[0]["LoginDate"]).ToString();
            //}
            string sql =
                @"INSERT INTO SYS_LoginRecord
                               ([LoginId],[LoginDate]
                               ,[LoginIP]
                               ,[IsError],LoginType
                               )
                         VALUES
                               (@LoginId,
                               @LoginDate,
                               @LoginIP,
                               @IsError,'AD')";
            List<SqlParameter> _p = new List<SqlParameter>();

            _p.Add(new SqlParameter("@LoginId", username));
            _p.Add(new SqlParameter("@LoginDate", DateTime.Now));
            _p.Add(new SqlParameter("@LoginIP", SSoft.Web.Security.User.IP));
            _p.Add(new SqlParameter("@IsError", !rtnValue));
            SSoft.Data.SqlHelper.SelectNonQuery(sql, _p.ToArray());


            return rtnValue;
        }



    }
}
