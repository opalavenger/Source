using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SSoft.Data
{
    public partial class SqlHelper
    {
        public static SqlParameter CreateSqlParameter(string name, SqlDbType type, object value)
        {
            SqlParameter parameter = new SqlParameter(name, type);
            if (value == null)
            {
                parameter.IsNullable = true;
                parameter.Value = DBNull.Value;
            }

            else
            {
                parameter.Value = value;
            }

            return parameter;
        }

        public static SqlParameter CreateSqlParameter(string name, object value)
        {
            return CreateSqlParameter(name, SqlDbType.NVarChar, value);
        }

        public static SqlDataReader SelectReader(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(sqlCommandString, con);
            com.CommandType = commandType;
            if (parameters != null)
                com.Parameters.AddRange(parameters);
            con.Open();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static SqlDataReader SelectReader(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectReader(sqlCommandString, parameters, CommandType.Text, connectionString);
        }

        public static SqlDataReader SelectReader(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectReader(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString);
        }

        public static SqlDataReader SelectReader(string sqlCommandString, string connectionString)
        {
            return SelectReader(sqlCommandString, null, CommandType.Text, connectionString);
        }

        public static SqlDataReader SelectReader(string sqlCommandString)
        {
            return SelectReader(sqlCommandString, new SqlParameter[] { }, CommandType.Text, MainDatabase.ConnectString);
        }

        public static DataTable SelectTable(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString, SqlTransaction transaction)
        {
            DataTable dt = new DataTable();
            SqlConnection con = null;
            SqlDataAdapter da = null;
            if (transaction != null)
            {
                con = transaction.Connection;
                
                //d.SelectCommand.Transaction = transaction;
                da = new SqlDataAdapter(new SqlCommand(sqlCommandString,con,transaction));

            }
            else
            {
                con = new SqlConnection(connectionString);
                da = new SqlDataAdapter(sqlCommandString, con);
            }

           
            da.SelectCommand.CommandType = commandType;
            if (parameters != null)
                da.SelectCommand.Parameters.AddRange(parameters);
            da.Fill(dt);
            return dt;
        }

        public static DataTable SelectTable(string sqlCommandString, CommandType commandType)
        {
            return SelectTable(sqlCommandString, null, commandType, MainDatabase.ConnectString,null);
        }

        public static DataTable SelectTable(string sqlCommandString, CommandType commandType, SqlParameter[] parameters)
        {
            return SelectTable(sqlCommandString, parameters, commandType, MainDatabase.ConnectString, null);
        }

        public static DataTable SelectTable(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectTable(sqlCommandString, parameters, CommandType.Text, connectionString, null);
        }

        public static DataTable SelectTable(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectTable(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, null);
        }

        public static DataTable SelectTable(string sqlCommandString, SqlParameter[] parameters,SqlTransaction transaction)
        {
            return SelectTable(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, transaction);
        }

        public static DataTable SelectTable(string sqlCommandString, string connectionString)
        {
            return SelectTable(sqlCommandString, null, CommandType.Text, connectionString, null);
        }

        public static DataTable SelectTable(string sqlCommandString)
        {
            return SelectTable(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, null);
        }

        public static int SelectRowCount(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandString, con);
            da.SelectCommand.CommandType = commandType;
            if (parameters != null)
                da.SelectCommand.Parameters.AddRange(parameters);
            da.Fill(dt);
            return dt.Rows.Count;
        }

        public static int SelectRowCount(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectRowCount(sqlCommandString, parameters, CommandType.Text, connectionString);
        }

        public static int SelectRowCount(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectRowCount(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString);
        }

        public static int SelectRowCount(string sqlCommandString, string connectionString)
        {
            return SelectRowCount(sqlCommandString, null, CommandType.Text, connectionString);
        }

        public static int SelectRowCount(string sqlCommandString)
        {
            return SelectRowCount(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString);
        }

        public static object SelectScalar(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString, SqlTransaction transaction)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            Object returnValue = null;
            if (transaction != null)
            {
                con = transaction.Connection;
                com = new SqlCommand(sqlCommandString, con, transaction);
            }
            else
            {
                con = new SqlConnection(connectionString);
                com = new SqlCommand(sqlCommandString, con);
            }
           
            com.CommandType = commandType;
            if (parameters != null)
                com.Parameters.AddRange(parameters);
            if (transaction == null)
            {
                con.Open();
                returnValue = com.ExecuteScalar();
                con.Close();
            }
            else
            {
                returnValue = com.ExecuteScalar();
            }
            return returnValue;
        }

        public static object SelectScalar(string sqlCommandString, SqlParameter[] parameters, CommandType commandType)
        {
            return SelectScalar(sqlCommandString, parameters, commandType, MainDatabase.ConnectString,null);
        }

        public static object SelectScalar(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectScalar(sqlCommandString, parameters, CommandType.Text, connectionString, null);
        }

        public static object SelectScalar(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectScalar(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, null);
        }

        public static object SelectScalar(string sqlCommandString, SqlParameter[] parameters, SqlTransaction transaction)
        {
            return SelectScalar(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, transaction);
        }

        public static object SelectScalar(string sqlCommandString, string connectionString)
        {
            return SelectScalar(sqlCommandString, null, CommandType.Text, connectionString, null);
        }

        public static object SelectScalar(string sqlCommandString)
        {
            return SelectScalar(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, null);
        }

        public static object SelectScalar(string sqlCommandString, SqlTransaction transaction)
        {
            return SelectScalar(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, transaction);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString, SqlTransaction transaction)
        {
            SqlConnection con = null;
            SqlCommand com = null;
            int returnValue = 0;
            if (transaction != null)
            {
                con = transaction.Connection;
                com = new SqlCommand(sqlCommandString, con,transaction);
            }
            else
            {
                con = new SqlConnection(connectionString);
                com = new SqlCommand(sqlCommandString, con);
            }


            com.CommandType = commandType;
           
            if (parameters != null)
                com.Parameters.AddRange(parameters);

            if (transaction == null)
            {
                con.Open();
                returnValue = com.ExecuteNonQuery();
                con.Close();
            }
            else
            {
                returnValue = com.ExecuteNonQuery();
            }

            return returnValue;
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString, out long identity)
        {
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand(sqlCommandString, con);
            com.CommandType = commandType;
            if (parameters != null)
                com.Parameters.AddRange(parameters);
            con.Open();
            SqlTransaction _tran = con.BeginTransaction();
            com.Transaction = _tran;
            try
            {
                int returnValue = com.ExecuteNonQuery();
                string _sqlcmd = "select @@IDENTITY";
                SqlCommand com1 = new SqlCommand(_sqlcmd, con);
                com1.Transaction = _tran;
                identity = Convert.ToInt64(com1.ExecuteScalar());
                _tran.Commit();
                con.Close();
                return returnValue;
            }
            catch (Exception ex)
            {
                _tran.Rollback();
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }

            }
            
            
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, out long identity)
        {
            return SelectNonQuery(sqlCommandString, parameters, commandType, MainDatabase.ConnectString, out identity);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectNonQuery(sqlCommandString, parameters, CommandType.Text, connectionString,null);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectNonQuery(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString,null);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, SqlTransaction transaction)
        {
            return SelectNonQuery(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, transaction);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, CommandType commandType)
        {
            return SelectNonQuery(sqlCommandString, parameters, commandType, MainDatabase.ConnectString, null);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlParameter[] parameters, out long identity)
        {
            return SelectNonQuery(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString, out identity);
        }

        public static int SelectNonQuery(string sqlCommandString, string connectionString)
        {
            return SelectNonQuery(sqlCommandString, null, CommandType.Text, connectionString, null);
        }

        public static int SelectNonQuery(string sqlCommandString)
        {
            return SelectNonQuery(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, null);
        }

        public static int SelectNonQuery(string sqlCommandString, SqlTransaction transaction)
        {
            return SelectNonQuery(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, transaction);
        }

        public static int SelectNonQuery(string sqlCommandString, out long identity)
        {
            return SelectNonQuery(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString, out identity);
        }

        public static bool SelectHasRows(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandString, con);
            da.SelectCommand.CommandType = commandType;
            if (parameters != null)
                da.SelectCommand.Parameters.AddRange(parameters);
            da.Fill(dt);
            return dt.Rows.Count > 0 ? true : false;
        }

        public static bool SelectHasRows(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectHasRows(sqlCommandString, parameters, CommandType.Text, connectionString);
        }

        public static bool SelectHasRows(string sqlCommandString, SqlParameter[] parameters, CommandType commandType)
        {
            return SelectHasRows(sqlCommandString, parameters, commandType, MainDatabase.ConnectString);
        }

        public static bool SelectHasRows(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectHasRows(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString);
        }

        public static bool SelectHasRows(string sqlCommandString, string connectionString)
        {
            return SelectHasRows(sqlCommandString, null, CommandType.Text, connectionString);
        }

        public static bool SelectHasRows(string sqlCommandString)
        {
            return SelectHasRows(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString);
        }
        //SelectDataBind
        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox,string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString)
        {
            DataTable dt = SelectTable(sqlCommandString, parameters, commandType,connectionString,null);
            _listBox.DataSource = dt;
            _listBox.DataTextField = dt.Columns[1].ColumnName;
            _listBox.DataValueField = dt.Columns[0].ColumnName;
            _listBox.Items.Clear();
            _listBox.DataBind();
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString, CommandType commandType)
        {
            SelectDataBind(_listBox, sqlCommandString, null, commandType, MainDatabase.ConnectString);
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString, CommandType commandType, SqlParameter[] parameters)
        {
            SelectDataBind(_listBox, sqlCommandString, parameters, commandType, MainDatabase.ConnectString);
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            SelectDataBind(_listBox, sqlCommandString, parameters, CommandType.Text, connectionString);
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString, SqlParameter[] parameters)
        {
            SelectDataBind(_listBox, sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString);
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString, string connectionString)
        {
            SelectDataBind(_listBox, sqlCommandString, null, CommandType.Text, connectionString);
        }

        public static void SelectDataBind(System.Web.UI.WebControls.ListBox _listBox, string sqlCommandString)
        {
            SelectDataBind(_listBox, sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString);
        }

        //SelectArray
        public static string[] SelectArray(string sqlCommandString, SqlParameter[] parameters, CommandType commandType, string connectionString)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter(sqlCommandString, con);
            da.SelectCommand.CommandType = commandType;
            if (parameters != null)
                da.SelectCommand.Parameters.AddRange(parameters);
            da.Fill(dt);

            string[] returnArray = new string[dt.Rows.Count];
            for(int _i=0 ; _i < dt.Rows.Count ; _i++)
            {
                string _value = "";
                if (!Convert.IsDBNull(dt.Rows[_i][0]))
                {
                    _value = dt.Rows[_i][0].ToString();
                }

                returnArray[_i] = _value;
            }


            return returnArray;
        }

        public static string[] SelectArray(string sqlCommandString, CommandType commandType)
        {
            return SelectArray(sqlCommandString, null, commandType, MainDatabase.ConnectString);
        }

        public static string[] SelectArray(string sqlCommandString, CommandType commandType, SqlParameter[] parameters)
        {
            return SelectArray(sqlCommandString, parameters, commandType, MainDatabase.ConnectString);
        }

        public static string[] SelectArray(string sqlCommandString, SqlParameter[] parameters, string connectionString)
        {
            return SelectArray(sqlCommandString, parameters, CommandType.Text, connectionString);
        }

        public static string[] SelectArray(string sqlCommandString, SqlParameter[] parameters)
        {
            return SelectArray(sqlCommandString, parameters, CommandType.Text, MainDatabase.ConnectString);
        }

        public static string[] SelectArray(string sqlCommandString, string connectionString)
        {
            return SelectArray(sqlCommandString, null, CommandType.Text, connectionString);
        }

        public static string[] SelectArray(string sqlCommandString)
        {
            return SelectArray(sqlCommandString, null, CommandType.Text, MainDatabase.ConnectString);
        }
    }
}
