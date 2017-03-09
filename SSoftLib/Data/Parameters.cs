using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SSoft.Data
{
    public class Parameters
    {
        const string _parameterTableName = "Parameters";

        public static void SetParameter(string key, object value, string description)
        {
            int _count = Convert.ToInt32(SqlHelper.SelectScalar(string.Format("select count(*) from {0}  where ParamKey=@ParamKey", _parameterTableName), new SqlParameter[] { new SqlParameter("@tablename", _parameterTableName), new SqlParameter("@ParamKey", key) }));
            if (_count > 0)
            {
                SqlHelper.SelectNonQuery(string.Format("update {0}  set ParamValue=@ParamValue , ParamDescription=@ParamDescription where ParamKey=@ParamKey", _parameterTableName), new SqlParameter[] { new SqlParameter("@tablename", _parameterTableName), new SqlParameter("@ParamValue", value), new SqlParameter("@ParamDescription", description), new SqlParameter("@ParamKey", key) });
            }
            else
            {
                SqlHelper.SelectNonQuery(string.Format("insert into  {0}(ParamKey,ParamValue,ParamDescription) values(@ParamKey,@ParamValue,@ParamDescription)", _parameterTableName), new SqlParameter[] { new SqlParameter("@tablename", _parameterTableName), new SqlParameter("@ParamValue", value), new SqlParameter("@ParamDescription", description), new SqlParameter("@ParamKey", key) });
        
            }
        }

        public static void SetParameter(string key, object value)
        {
            SetParameter(key, value, "");
        }

        public static object GetParameter(string key, object defaultValue)
        {
            object _returnValue = null;

            DataTable dt = SqlHelper.SelectTable(string.Format("select ParamValue from {0} where ParamKey=@ParamKey", _parameterTableName), new SqlParameter[] { new SqlParameter("@ParamKey", key) });
            if (dt.Rows.Count > 0)
            {
                _returnValue = dt.Rows[0][0].ToString();
            }
            else
            {
                _returnValue = defaultValue;
            }

            return _returnValue;
        }
    }

}
