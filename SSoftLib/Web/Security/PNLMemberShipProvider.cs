using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SSoft.Web.Security
{
    public class PNLMemberShipProvider : System.Web.Security.MembershipProvider
    {
        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            if (config == null)
            {
                throw new ArgumentNullException("config");
            }

            if (String.IsNullOrEmpty(name))
            {
                name = "PNLMemberShipProvider";
            }

            if (String.IsNullOrEmpty(config["description"]))
            {
                config.Remove("description");
                config.Add("", "PNL System MembershipProvider");
            }
            base.Initialize(name, config);
        }

        public override bool ValidateUser(string username, string password)
        {

            bool rtnValue = false;
            User.Last_Logindate = "無";
            if ((username.ToLower() == "admin" && password == "admin.admin") || (username.ToLower() == "user" && password == "test123"))
            {
                User.Last_Logindate = DateTime.Now.ToString();
                rtnValue = true;
            }
//            DataTable dt = SSoft.Data.SqlHelper.SelectTable("select id,emp_name,emp_no,(select max(LoginDate) from SYS_LoginRecord where LoginId=@email and IsError=0) as LoginDate from DATA_MEMBER where email=@email", new SqlParameter[] { new SqlParameter("@email", username.Trim()) });
//            if (dt.Rows.Count > 0 && password == username.Trim().ToLower())
//            {
//                rtnValue = true;
//                User.Emp_Name = dt.Rows[0]["emp_name"].ToString();
//                User.Last_Logindate = Convert.IsDBNull(dt.Rows[0]["LoginDate"]) ? "無" : Convert.ToDateTime(dt.Rows[0]["LoginDate"]).ToString();
//                User.Emp_ID = Convert.ToInt32(dt.Rows[0]["id"]);
//                User.Emp_No = Convert.ToString(dt.Rows[0]["emp_no"]);
//            }
//            string sql =
//                @"INSERT INTO SYS_LoginRecord
//                               ([LoginId],[LoginDate]
//                               ,[LoginIP]
//                               ,[IsError],LoginType
//                               )
//                         VALUES
//                               (@LoginId,
//                               @LoginDate,
//                               @LoginIP,
//                               @IsError,'DB')";
//            List<SqlParameter> _p = new List<SqlParameter>();

//            _p.Add(new SqlParameter("@LoginId", username));
//            _p.Add(new SqlParameter("@LoginDate", DateTime.Now));
//            _p.Add(new SqlParameter("@LoginIP", SSoft.Web.Security.User.IP));
//            _p.Add(new SqlParameter("@IsError", !rtnValue));
//            SSoft.Data.SqlHelper.SelectNonQuery(sql, _p.ToArray());


            return rtnValue;
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            throw new NotSupportedException();
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password, string newPasswordQuestion, string newPasswordAnswer)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUser CreateUser(string username, string password, string email, string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey, out System.Web.Security.MembershipCreateStatus status)
        {
            throw new NotSupportedException();
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            throw new NotSupportedException();
        }

        public override bool EnablePasswordReset
        {
            get { return false; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return false; }
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            throw new NotSupportedException();
        }

        public override int GetNumberOfUsersOnline()
        {
            throw new NotSupportedException();
        }

        public override string GetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUser GetUser(string username, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override System.Web.Security.MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            throw new NotSupportedException();
        }

        public override string GetUserNameByEmail(string email)
        {
            throw new NotSupportedException();
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { throw new NotSupportedException(); }
        }

        public override int MinRequiredPasswordLength
        {
            get { throw new NotSupportedException(); }
        }

        public override int PasswordAttemptWindow
        {
            get { throw new NotSupportedException(); }
        }

        public override System.Web.Security.MembershipPasswordFormat PasswordFormat
        {
            get { throw new NotSupportedException(); }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { throw new NotSupportedException(); }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return false; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return false; }
        }

        public override string ResetPassword(string username, string answer)
        {
            throw new NotSupportedException();
        }

        public override bool UnlockUser(string userName)
        {
            throw new NotSupportedException();
        }

        public override void UpdateUser(System.Web.Security.MembershipUser user)
        {
            throw new NotSupportedException();
        }


    }
}
