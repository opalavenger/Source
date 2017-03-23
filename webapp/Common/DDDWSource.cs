using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using SSoft.MVC;

namespace YMIR.Common
{
    public class DDDWSource
    {
        /// <summary>
        /// Program
        /// </summary>
        /// <returns></returns>
        public static List<SSoft.MVC.DisplayValue> DDDW_Program()
        {
            List<SSoft.MVC.DisplayValue> returnList = new List<SSoft.MVC.DisplayValue>();

            DataTable dt = SSoft.Data.SqlHelper.SelectTable(@"select 0 as ProgramId,'Login' as ProgramNo,'登入' as ProgramName
union
select ProgramId,ProgramNo,ProgramName
from (SELECT          WebUserTab.WUTab_Id as SystemId, WebUserTab.WUTab_Name as SystemName, WebUserPage.WUPage_Id as ProgramId, WebUserPage.WUPage_No as ProgramNo, 
                            WebUserPage.WUPage_Name as ProgramName, WebUserPage.WUPage_Type as ProgramType, WebUserPage.WUPage_Path as ProgramPath, 
                            isnull(WebUserPage.WUPage_Order,0) as ProgarmOrder, WebUserTab.Note, WebUserPage.Note AS Expr1
FROM              WebUserTab INNER JOIN
                            WebUserPage ON WebUserTab.WUTab_Id = WebUserPage.WUPage_TabId) as ProgramList						
							 ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SSoft.MVC.DisplayValue displayValue = new SSoft.MVC.DisplayValue() {Int01= Convert.ToInt32(dt.Rows[i]["ProgramId"]), Value = Convert.ToString(dt.Rows[i]["ProgramId"]), Display = Convert.ToString(dt.Rows[i]["ProgramName"]) };

                returnList.Add(displayValue);
            }

            return returnList;
        }

        /// <summary>
        /// UserInfo
        /// </summary>
        /// <returns></returns>
        public static List<SSoft.MVC.DisplayValue> DDDW_UserInfo()
        {
            List<SSoft.MVC.DisplayValue> returnList = new List<SSoft.MVC.DisplayValue>();

            DataTable dt = SSoft.Data.SqlHelper.SelectTable(@"SELECT          UserInfo_NO, UserInfo_Id, UserInfo_AD, UserInfo_Name, UserInfo_Email, UserInfo_Phone
FROM              UserInfo ORDER BY UserInfo_AD ");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                SSoft.MVC.DisplayValue displayValue = new SSoft.MVC.DisplayValue() { Value = Convert.ToString(dt.Rows[i]["UserInfo_Id"]), Display = Convert.ToString(dt.Rows[i]["UserInfo_AD"]) + "[" + Convert.ToString(dt.Rows[i]["UserInfo_Name"]) + "]",
                    Display1 = Convert.ToString(dt.Rows[i]["UserInfo_Name"]),
                    Display2 = Convert.ToString(dt.Rows[i]["UserInfo_Email"]),
                    Display3 = Convert.ToString(dt.Rows[i]["UserInfo_Phone"]),
                    Int01 = Convert.ToInt32(dt.Rows[i]["UserInfo_NO"])
                };

                returnList.Add(displayValue);
            }

            return returnList;
        }
    }
}