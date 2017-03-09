using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace SSoft.Data
{
    public partial class SqlHelper
    {
        public static void CombineSQL(System.Web.UI.DataSourceControl _dataSource, string _columnName, Object _value)
        {
            CombineSQL(_dataSource, _columnName, _value, EnumSQLOperator.Equals);
        }

        public static void CombineSQL(System.Web.UI.DataSourceControl _dataSource, string _columnName, Object _valueFirst, Object _valueLast)
        {
            if ((_valueFirst == null || _valueFirst.ToString().Trim() == "") && (_valueLast == null || _valueLast.ToString().Trim() == "")) return;

            if (_valueFirst == null || _valueFirst.ToString().Trim() == "")
            {
                _valueLast = _valueLast.ToString().Trim();
                CombineSQL(_dataSource, _columnName, _valueLast, EnumSQLOperator.Equals);
                return;
            }
            if (_valueLast == null || _valueLast.ToString().Trim() == "")
            {
                _valueFirst = _valueFirst.ToString().Trim();
                CombineSQL(_dataSource, _columnName, _valueFirst, EnumSQLOperator.Equals);
                return;
            }
            _valueFirst = _valueFirst.ToString().Trim();
            _valueLast = _valueLast.ToString().Trim();
            if (String.Compare(_valueFirst.ToString(), _valueLast.ToString()) > 0)
            {
                Object _temp;
                _temp = _valueFirst;
                _valueFirst = _valueLast;
                _valueLast = _temp;
            }

            CombineSQL(_dataSource, _columnName, _valueFirst, EnumSQLOperator.GreaterThanOrEqual);
            CombineSQL(_dataSource, _columnName, _valueLast, EnumSQLOperator.LessThanOrEqual);

        }

        public static void CombineSQL(System.Web.UI.DataSourceView _dataSourceView, string _columnName, Object _valueFirst, Object _valueLast)
        {
            if ((_valueFirst == null || _valueFirst.ToString().Trim() == "") && (_valueLast == null || _valueLast.ToString().Trim() == "")) return;

            if (_valueFirst == null || _valueFirst.ToString().Trim() == "")
            {
                _valueLast = _valueLast.ToString().Trim();
                CombineSQL(_dataSourceView, _columnName, _valueLast, EnumSQLOperator.Equals);
                return;
            }
            if (_valueLast == null || _valueLast.ToString().Trim() == "")
            {
                _valueFirst = _valueFirst.ToString().Trim();
                CombineSQL(_dataSourceView, _columnName, _valueFirst, EnumSQLOperator.Equals);
                return;
            }
            _valueFirst = _valueFirst.ToString().Trim();
            _valueLast = _valueLast.ToString().Trim();
            if (String.Compare(_valueFirst.ToString(), _valueLast.ToString()) > 0)
            {
                Object _temp;
                _temp = _valueFirst;
                _valueFirst = _valueLast;
                _valueLast = _temp;
            }

            CombineSQL(_dataSourceView, _columnName, _valueFirst, EnumSQLOperator.GreaterThanOrEqual);
            CombineSQL(_dataSourceView, _columnName, _valueLast, EnumSQLOperator.LessThanOrEqual);

        }

        public static void CombineSQL(System.Web.UI.DataSourceControl _dataSource, string _columnName, Object _value, EnumSQLOperator _op)
        {

            string _parameterName, _parameterName1 = "";

            int _parameterCount = 0;

            if ((_columnName == null) || (_columnName.Trim() == "")) return;

            if (!(_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull))
            {
                if (_value == null || _value.ToString() == "") return;

                if (_dataSource is System.Web.UI.WebControls.SqlDataSource)
                {
                    _parameterCount = ((System.Web.UI.WebControls.SqlDataSource)_dataSource).SelectParameters.Count;
                }
                else
                {
                    return;
                }

                //_parameterName = "@" + _columnName.Replace('.', '_');
                _parameterName = "@__para" + Convert.ToString(_parameterCount + 1);
                //_parameterName1 = _columnName.Replace('.', '_');
                _parameterName1 = "__para" + Convert.ToString(_parameterCount + 1);
            }
            CombineSQL(_dataSource, _columnName, _value, _parameterName1, _op);



        }

        public static void CombineSQL(System.Web.UI.DataSourceView _dataSourceView, string _columnName, Object _value, EnumSQLOperator _op)
        {

            string _parameterName, _parameterName1 = "";

            int _parameterCount = 0;

            if ((_columnName == null) || (_columnName.Trim() == "")) return;

            if (!(_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull))
            {
                if (_value == null || _value.ToString() == "") return;

                if (_dataSourceView is System.Web.UI.WebControls.SqlDataSourceView)
                {
                    _parameterCount = ((System.Web.UI.WebControls.SqlDataSourceView)_dataSourceView).SelectParameters.Count;
                }
                else
                {
                    return;
                }

                //_parameterName = "@" + _columnName.Replace('.', '_');
                _parameterName = "@__para" + Convert.ToString(_parameterCount + 1);
                //_parameterName1 = _columnName.Replace('.', '_');
                _parameterName1 = "__para" + Convert.ToString(_parameterCount + 1);
            }
            CombineSQL(_dataSourceView, _columnName, _value, _parameterName1, _op);



        }

        public static void CombineKeySQL(System.Web.UI.DataSourceControl _dataSource, Object _value)
        {
            CombineSQL(_dataSource, "Id", _value, "__KeyId", EnumSQLOperator.Equals);
        }

        public static void CombineParentKeySQL(System.Web.UI.DataSourceControl _dataSource, string _parentKeyName, Object _value)
        {
            CombineSQL(_dataSource, _parentKeyName, _value, "__ParentKeyId", EnumSQLOperator.Equals);
        }

        public static void CombineSQL(System.Web.UI.DataSourceControl _dataSource, string _columnName, Object _value, string _paramName, EnumSQLOperator _op)
        {
            StringBuilder _sb = new StringBuilder(256);
            string _parameterName, _parameterName1, _whereSQL, _originalSelectCommand = "";
            int _where_pos;
            //int _parameterCount = 0;

            if ((_columnName == null) || (_columnName.Trim() == "")) return;
            if (!(_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull))
            {
                if (_value == null || _value.ToString() == "") return;
            }

            //if (_dataSource is System.Web.UI.WebControls.SqlDataSource)
            //{
            //    _parameterCount = ((System.Web.UI.WebControls.SqlDataSource)_dataSource).SelectParameters.Count;
            //}
            //else
            //{
            //    return;
            //}

            if (!(_dataSource is System.Web.UI.WebControls.SqlDataSource))
                return;

            _parameterName = "@" + _paramName;
            _parameterName1 = _paramName;
            _whereSQL = CombineSQL(_columnName, _parameterName, _op);

            if (_dataSource is System.Web.UI.WebControls.SqlDataSource)
            {
                _originalSelectCommand = ((System.Web.UI.WebControls.SqlDataSource)_dataSource).SelectCommand;
            }


            _sb.Append(_originalSelectCommand);
            _sb.Replace("\r\n", "  ");
            _where_pos = _sb.ToString().ToUpper().IndexOf(" WHERE ");
            if (_where_pos >= 0)
            {
                _sb.Insert(_where_pos + 7, _whereSQL + " AND ");
            }
            else
            {
                int _group_pos = _sb.ToString().ToUpper().IndexOf(" GROUP ");
                if (_group_pos >= 0)
                {
                    _sb.Insert(_group_pos, " WHERE " + _whereSQL + " ");
                }
                else
                {
                    int _order_pos = _sb.ToString().ToUpper().IndexOf(" ORDER ");
                    if (_order_pos >= 0)
                    {
                        _sb.Insert(_order_pos, " WHERE " + _whereSQL + " ");
                    }
                    else
                    {
                        _sb.Append(" WHERE " + _whereSQL + " ");
                    }
                }
            }

            if (_dataSource is System.Web.UI.WebControls.SqlDataSource)
            {
                ((System.Web.UI.WebControls.SqlDataSource)_dataSource).SelectCommand = _sb.ToString();

                if (_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull)
                    return;

                string _data = "";
                if (_op == EnumSQLOperator.Like || _op == EnumSQLOperator.NotLike)
                {
                    _data = "%" + _value.ToString() + "%";
                }
                else
                {
                    _data = _value.ToString();
                }


                System.Web.UI.WebControls.SqlDataSource _ds = (System.Web.UI.WebControls.SqlDataSource)_dataSource;
                bool _flag = false;
                int _index = 0;
                while (_index < _ds.SelectParameters.Count && _flag == false)
                {
                    if (_ds.SelectParameters[_index].Name.Trim().Contains(_parameterName1))
                    {
                        _flag = true;
                    }
                    _index++;
                }

                if (!_flag)
                {
                    _ds.SelectParameters.Add(_parameterName1, _data);
                }
                else
                {
                    _ds.SelectParameters[_parameterName1].DefaultValue = _data;
                }
            }

        }

        public static void CombineSQL(System.Web.UI.DataSourceView _dataSourceView, string _columnName, Object _value, string _paramName, EnumSQLOperator _op)
        {
            StringBuilder _sb = new StringBuilder(256);
            string _parameterName, _parameterName1, _whereSQL, _originalSelectCommand = "";
            int _where_pos;
            //int _parameterCount = 0;

            if ((_columnName == null) || (_columnName.Trim() == "")) return;
            if (!(_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull))
            {
                if (_value == null || _value.ToString() == "") return;
            }

            //if (_dataSource is System.Web.UI.WebControls.SqlDataSource)
            //{
            //    _parameterCount = ((System.Web.UI.WebControls.SqlDataSource)_dataSource).SelectParameters.Count;
            //}
            //else
            //{
            //    return;
            //}

            if (!(_dataSourceView is System.Web.UI.WebControls.SqlDataSourceView))
                return;

            _parameterName = "@" + _paramName;
            _parameterName1 = _paramName;
            _whereSQL = CombineSQL(_columnName, _parameterName, _op);

            if (_dataSourceView is System.Web.UI.WebControls.SqlDataSourceView)
            {
                _originalSelectCommand = ((System.Web.UI.WebControls.SqlDataSourceView)_dataSourceView).SelectCommand;
            }


            _sb.Append(_originalSelectCommand);
            _sb.Replace("\r\n", "  ");
            _where_pos = _sb.ToString().ToUpper().IndexOf(" WHERE ");
            if (_where_pos >= 0)
            {
                _sb.Insert(_where_pos + 7, _whereSQL + " AND ");
            }
            else
            {
                int _group_pos = _sb.ToString().ToUpper().IndexOf(" GROUP ");
                if (_group_pos >= 0)
                {
                    _sb.Insert(_group_pos, " WHERE " + _whereSQL + " ");
                }
                else
                {
                    int _order_pos = _sb.ToString().ToUpper().IndexOf(" ORDER ");
                    if (_order_pos >= 0)
                    {
                        _sb.Insert(_order_pos, " WHERE " + _whereSQL + " ");
                    }
                    else
                    {
                        _sb.Append(" WHERE " + _whereSQL + " ");
                    }
                }
            }

            if (_dataSourceView is System.Web.UI.WebControls.SqlDataSourceView)
            {
                ((System.Web.UI.WebControls.SqlDataSourceView)_dataSourceView).SelectCommand = _sb.ToString();

                if (_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull)
                    return;

                string _data = "";
                if (_op == EnumSQLOperator.Like || _op == EnumSQLOperator.NotLike)
                {
                    _data = "%" + _value.ToString() + "%";
                }
                else
                {
                    _data = _value.ToString();
                }


                System.Web.UI.WebControls.SqlDataSourceView _ds = (System.Web.UI.WebControls.SqlDataSourceView)_dataSourceView;
                bool _flag = false;
                int _index = 0;
                while (_index < _ds.SelectParameters.Count && _flag == false)
                {
                    if (_ds.SelectParameters[_index].Name.Trim().Contains(_parameterName1))
                    {
                        _flag = true;
                    }
                    _index++;
                }

                if (!_flag)
                {
                    _ds.SelectParameters.Add(_parameterName1, _data);
                }
                else
                {
                    _ds.SelectParameters[_parameterName1].DefaultValue = _data;
                }
            }

        }

        public static string CombineSQL(string _columnName, string _parameterName, EnumSQLOperator _op)
        {
            string _rtn;
            switch (_op)
            {
                case EnumSQLOperator.Equals:
                    _rtn = _columnName + " = " + _parameterName;
                    break;
                case EnumSQLOperator.NotEqual:
                    _rtn = _columnName + " <> " + _parameterName;
                    break;
                case EnumSQLOperator.GreaterThan:
                    _rtn = _columnName + " > " + _parameterName;
                    break;
                case EnumSQLOperator.GreaterThanOrEqual:
                    _rtn = _columnName + " >= " + _parameterName;
                    break;
                case EnumSQLOperator.LessThan:
                    _rtn = _columnName + " < " + _parameterName;
                    break;
                case EnumSQLOperator.LessThanOrEqual:
                    _rtn = _columnName + " <= " + _parameterName;
                    break;
                case EnumSQLOperator.Like:
                    _rtn = _columnName + " Like " + _parameterName;
                    break;
                case EnumSQLOperator.NotLike:
                    _rtn = " NOT " + _columnName + " Like " + _parameterName;
                    break;
                case EnumSQLOperator.In:
                    _rtn = _columnName + " IN " + _parameterName;
                    break;
                case EnumSQLOperator.NotIn:
                    _rtn = " NOT " + _columnName + " IN " + _parameterName;
                    break;
                case EnumSQLOperator.Blanks:
                    _rtn = _columnName + " = " + _parameterName;
                    break;
                case EnumSQLOperator.NoneBlanks:
                    _rtn = " NOT " + _columnName + " = " + _parameterName;
                    break;
                case EnumSQLOperator.IsNull:
                    _rtn = " " + _columnName + " is null ";
                    break;
                case EnumSQLOperator.IsNotNull:
                    _rtn = " " + _columnName + " is not null ";
                    break;
                default:
                    _rtn = "";
                    break;
            }

            return _rtn;


        }

        //public static void CombineSQL(System.Web.UI.WebControls.SqlDataSource _dataSource, string _columnName, Object _value, EnumSQLOperator op, System.Data.SqlDbType datatype, EnumLogical enumlogical)
        //{
        //    StringBuilder _sb = new StringBuilder(256);
        //    string _parameterName, _whereSQL;
        //    int _where_pos;
        //    string _logical;

        //    if ((_columnName == null) || (_columnName.Trim() == "")) return;
        //    if (_value == null || _value.ToString() == "") return;

        //    switch (enumlogical)
        //    {
        //        case EnumLogical.AND:
        //            _logical = " AND ";
        //            break;
        //        case EnumLogical.OR:
        //            _logical = " OR ";
        //            break;
        //        default:
        //            _logical = " AND ";
        //            break;
        //    }

        //    _parameterName = "@" + _columnName.Replace('.', '_');
        //    _parameterName = "@__para" + Convert.ToString(_dataSource.SelectParameters.Count + 1);



        //    _whereSQL = CombineSQL(_columnName, _parameterName, op, _value, datatype);

        //    _sb.Append(_dataSource.SelectCommand);
        //    _sb.Replace("\r\n", "  ");
        //    _where_pos = _sb.ToString().ToUpper().IndexOf(" WHERE ");
        //    if (_where_pos >= 0)
        //    {
        //        _sb.Insert(_where_pos + 7, " ( " + _whereSQL + ") " + _logical);
        //    }
        //    else
        //    {
        //        _sb.Append(" WHERE " + " ( " + _whereSQL + ") ");
        //    }
        //    //System.Windows.Forms.MessageBox.Show(_sb.ToString());
        //    _dataSource.SelectCommand = _sb.ToString();
        //    //if (op != EnumSQLOperator.Like && datatype != System.Data.SqlDbType.DateTime)
        //    if (op != EnumSQLOperator.Like)
        //    {
        //        System.Web.UI.WebControls.Parameter _para = new System.Web.UI.WebControls.Parameter(_parameterName,System.TypeCode.
        //        _dataSource.SelectParameters.Add(.Add(_parameterName, datatype);
        //        _dataSource.SelectParameters[_parameterName].DefaultValue = _value;
        //    }
        //}


        //public static void CombineSQL(KOMWebSource.MeetingManagement.DataSets.DSConditionInput _dsConditionInput, System.Data.SqlClient.SqlDataAdapter _DataAdapter)
        //{
        //    string _colName, _op, _datatpe, _logical;
        //    string[] _temp;
        //    object _colvalue;
        //    KOMWebSource.MeetingManagement.Utilities.EnumSQLOperator _enumop;
        //    System.Data.SqlDbType _enumdatatype;
        //    KOMWebSource.MeetingManagement.DataSets.DSConditionInput.ConditionInputRow _dr;
        //    KOMWebSource.MeetingManagement.Utilities.EnumLogical _enumlogical = KOMWebSource.MeetingManagement.Utilities.EnumLogical.AND;
        //    try
        //    {


        //        for (int _i = 0; _i < _dsConditionInput.ConditionInput.Rows.Count; _i++)
        //        {
        //            _dr = (KOMWebSource.MeetingManagement.DataSets.DSConditionInput.ConditionInputRow)_dsConditionInput.ConditionInput.Rows[_i];
        //            _temp = _dr.ColumnName.Split(new char[] { '$' });
        //            _colName = _temp[0];
        //            if (_temp.Length > 1)
        //            {
        //                _datatpe = _temp[1];
        //            }
        //            else
        //            {
        //                _datatpe = "char";
        //            }

        //            _enumdatatype = ConvertDatatypeToEnum(_datatpe);
        //            _colvalue = _dr.ColumnValue;
        //            _op = _dr.Operator;
        //            _logical = _dr.Logical;
        //            _enumop = ConvertOperatorToEnum(_op);
        //            _enumlogical = KOMWebSource.MeetingManagement.Utilities.SqlHelper.ConvertLogicalToEnum(_logical);
        //            KOMWebSource.MeetingManagement.Utilities.SqlHelper.CombineSQL(_DataAdapter.SelectCommand, _colName, _colvalue, _enumop, _enumdatatype, _enumlogical);

        //        }

        //    }

        //    catch (Exception ex)
        //    {
        //        throw (new Exception(ex.Message.ToString(), ex));
        //    }


        //}




        public static EnumSQLOperator ConvertOperatorToEnum(string _op)
        {
            EnumSQLOperator _rtnOP;
            switch (_op.Trim().ToUpper())
            {
                case "=":
                    _rtnOP = EnumSQLOperator.Equals;
                    break;
                case "<>":
                    _rtnOP = EnumSQLOperator.NotEqual;
                    break;
                case ">":
                    _rtnOP = EnumSQLOperator.GreaterThan;
                    break;
                case ">=":
                    _rtnOP = EnumSQLOperator.GreaterThanOrEqual;
                    break;
                case "<":
                    _rtnOP = EnumSQLOperator.LessThan;
                    break;
                case "<=":
                    _rtnOP = EnumSQLOperator.LessThanOrEqual;
                    break;
                case "LIKE":
                    _rtnOP = EnumSQLOperator.Like;
                    break;
                case "NOT LIKE":
                    _rtnOP = EnumSQLOperator.NotLike;
                    break;
                case "IN":
                    _rtnOP = EnumSQLOperator.In;
                    break;
                case "NOT IN":
                    _rtnOP = EnumSQLOperator.NotIn;
                    break;
                case "":
                    _rtnOP = EnumSQLOperator.Blanks;
                    break;
                case "NOT":
                    _rtnOP = EnumSQLOperator.NoneBlanks;
                    break;
                default:
                    _rtnOP = EnumSQLOperator.Equals;
                    break;

            }

            return _rtnOP;


        }

        public static System.Data.SqlDbType ConvertDatatypeToEnum(string _datatype)
        {
            System.Data.SqlDbType _rtndatatype;
            switch (_datatype.Trim().ToUpper())
            {
                case "CHAR":
                    _rtndatatype = System.Data.SqlDbType.Char;
                    break;
                case "DATETIME":
                    _rtndatatype = System.Data.SqlDbType.DateTime;
                    break;
                case "DECIMAL":
                    _rtndatatype = System.Data.SqlDbType.Decimal;
                    break;
                case "INT":
                    _rtndatatype = System.Data.SqlDbType.Int;
                    break;
                default:
                    _rtndatatype = System.Data.SqlDbType.Char;
                    break;

            }

            return _rtndatatype;


        }

        public static EnumLogical ConvertLogicalToEnum(string _logical)
        {
            EnumLogical _rtnlogical;

            switch (_logical.Trim().ToUpper())
            {
                case "AND":
                    _rtnlogical = EnumLogical.AND;
                    break;
                case "OR":
                    _rtnlogical = EnumLogical.OR;
                    break;
                default:
                    _rtnlogical = EnumLogical.AND;
                    break;

            }

            return _rtnlogical;


        }

        public enum EnumSQLOperator
        {
            Equals,
            NotEqual,
            GreaterThan,
            GreaterThanOrEqual,
            LessThan,
            LessThanOrEqual,
            Like,
            NotLike,
            In,
            NotIn,
            Blanks,
            NoneBlanks,
            IsNull,
            IsNotNull
        }

        public enum EnumDropDownSource
        {
            ACN_NO,
            DEP_NO,
            PRJ_NO
        }

        public enum EnumDropDownSourceAllowNull
        {
            Allow,
            NotAllow,
            Defalut
        }

        public enum EnumLogical
        {
            AND,
            OR
        }

        public static Object ExecuteSQLScalar(string _sqlstr, SqlConnection _connection)
        {
            SqlCommand _sqlcmd = new SqlCommand(_sqlstr, _connection);
            Object _returnValue = _sqlcmd.ExecuteScalar();
            return _returnValue;
        }

        public static Object ExecuteSQLScalar(string _sqlstr, string _connectionstr)
        {
            SqlConnection _cn = new SqlConnection(_connectionstr);
            SqlCommand _sqlcmd = new SqlCommand(_sqlstr, _cn);
            Object _returnValue = null;
            _cn.Open();
            try
            {
                _returnValue = _sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _cn.Close();
            }
            return _returnValue;
        }

        public static void CombineSQL(System.Data.SqlClient.SqlCommand _sqlCommand, string _columnName, Object _value)
        {
            CombineSQL(_sqlCommand, _columnName, _value, EnumSQLOperator.Equals);
        }

        public static void CombineSQL(System.Data.SqlClient.SqlCommand _sqlCommand, string _columnName, Object _value, EnumSQLOperator _enumSQLOperator)
        {
            CombineSQL(_sqlCommand, _columnName, _value, _enumSQLOperator, System.Data.SqlDbType.NVarChar, EnumLogical.AND);
        }

        public static void CombineSQL(System.Data.SqlClient.SqlCommand _sqlCommand, string _columnName, Object _valueFirst, Object _valueLast)
        {
            if ((_valueFirst == null || _valueFirst.ToString().Trim() == "") && (_valueLast == null || _valueLast.ToString().Trim() == "")) return;

            if (_valueFirst == null || _valueFirst.ToString().Trim() == "")
            {
                CombineSQL(_sqlCommand, _columnName, _valueLast, EnumSQLOperator.Equals, System.Data.SqlDbType.NChar, EnumLogical.AND);
                return;
            }
            if (_valueLast == null || _valueLast.ToString().Trim() == "")
            {
                CombineSQL(_sqlCommand, _columnName, _valueFirst, EnumSQLOperator.Equals, System.Data.SqlDbType.NChar, EnumLogical.AND);
                return;
            }

            CombineSQL(_sqlCommand, _columnName, _valueFirst, EnumSQLOperator.GreaterThanOrEqual, System.Data.SqlDbType.NChar, EnumLogical.AND);
            CombineSQL(_sqlCommand, _columnName, _valueLast, EnumSQLOperator.LessThanOrEqual, System.Data.SqlDbType.NChar, EnumLogical.AND);

        }

        public static void CombineSQL(System.Data.SqlClient.SqlCommand _sqlCommand, string _columnName, Object _value, EnumSQLOperator _op, System.Data.SqlDbType _datatype, EnumLogical _enumlogical)
        {
            StringBuilder _sb = new StringBuilder(256);
            string _parameterName, _whereSQL;
            int _where_pos;
            string _logical;

            if ((_columnName == null) || (_columnName.Trim() == "")) return;
            if (!(_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull))
            {
                if (_value == null || _value.ToString() == "") return;
            }

            switch (_enumlogical)
            {
                case EnumLogical.AND:
                    _logical = " AND ";
                    break;
                case EnumLogical.OR:
                    _logical = " OR ";
                    break;
                default:
                    _logical = " AND ";
                    break;
            }

            _parameterName = "@" + _columnName.Replace('.', '_');
            _parameterName = "@__para" + Convert.ToString(_sqlCommand.Parameters.Count + 1);



            _whereSQL = CombineSQL(_columnName, _parameterName, _op, _value, _datatype);

            _sb.Append(_sqlCommand.CommandText);
            _sb.Replace("\r\n", "  ");
            //_where_pos = _sb.ToString().ToUpper().IndexOf(" WHERE ");
            //if (_where_pos >= 0)
            //{
            //    _sb.Insert(_where_pos + 7, " ( " + _whereSQL + ") " + _logical);
            //}
            //else
            //{
            //    _sb.Append(" WHERE " + " ( " + _whereSQL + ") ");
            //}
            _where_pos = _sb.ToString().ToUpper().IndexOf(" WHERE ");
            if (_where_pos >= 0)
            {
                _sb.Insert(_where_pos + 7, _whereSQL + _logical);
            }
            else
            {
                int _group_pos = _sb.ToString().ToUpper().IndexOf(" GROUP ");
                if (_group_pos >= 0)
                {
                    _sb.Insert(_group_pos, " WHERE " + _whereSQL + " ");
                }
                else
                {
                    int _order_pos = _sb.ToString().ToUpper().IndexOf(" ORDER ");
                    if (_order_pos >= 0)
                    {
                        _sb.Insert(_order_pos, " WHERE " + _whereSQL + " ");
                    }
                    else
                    {
                        _sb.Append(" WHERE " + _whereSQL + " ");
                    }
                }
            }




            //System.Windows.Forms.MessageBox.Show(_sb.ToString());
            _sqlCommand.CommandText = _sb.ToString();
            if (_op == EnumSQLOperator.IsNull || _op == EnumSQLOperator.IsNotNull)
                return;
            //if (op != EnumSQLOperator.Like && datatype != System.Data.SqlDbType.DateTime)
            if (_op != EnumSQLOperator.Like)
            {
                _sqlCommand.Parameters.Add(_parameterName, _datatype);
                _sqlCommand.Parameters[_parameterName].Value = _value;
            }
        }

        public static string CombineSQL(string _columnName, string _parameterName, EnumSQLOperator op, Object _value, System.Data.SqlDbType datatype)
        {
            string _rtn;

            if (datatype == System.Data.SqlDbType.DateTime)
            {
                _columnName = " convert(datetime, convert(char(10), " + _columnName + " )) ";
            }

            switch (op)
            {
                case EnumSQLOperator.Equals:
                    if (datatype == System.Data.SqlDbType.DateTime)
                    {
                        _rtn = _columnName + " = '" + _value + "' ";
                    }
                    else
                    {
                        _rtn = _columnName + " = " + _parameterName;
                    }
                    break;
                case EnumSQLOperator.NotEqual:
                    if (datatype == System.Data.SqlDbType.DateTime)
                    {
                        _rtn = _columnName + " <> '" + _value + "' ";
                    }
                    else
                    {
                        _rtn = _columnName + " <> " + _parameterName;
                    }
                    break;
                case EnumSQLOperator.GreaterThan:
                    if (datatype == System.Data.SqlDbType.DateTime)
                    {
                        _rtn = _columnName + " = '" + _value + "' ";
                    }
                    _rtn = _columnName + " > " + _parameterName;
                    break;
                case EnumSQLOperator.GreaterThanOrEqual:
                    _rtn = _columnName + " >= " + _parameterName;
                    break;
                case EnumSQLOperator.LessThan:
                    _rtn = _columnName + " < " + _parameterName;
                    break;
                case EnumSQLOperator.LessThanOrEqual:
                    _rtn = _columnName + " <= " + _parameterName;
                    break;
                case EnumSQLOperator.Like:
                    _rtn = _columnName + " Like '%" + _value.ToString() + "%' ";
                    break;
                case EnumSQLOperator.NotLike:
                    _rtn = " NOT " + _columnName + " Like " + _parameterName;
                    break;
                case EnumSQLOperator.In:
                    _rtn = _columnName + "IN" + _parameterName;
                    break;
                case EnumSQLOperator.NotIn:
                    _rtn = " NOT " + _columnName + " IN " + _parameterName;
                    break;
                case EnumSQLOperator.Blanks:
                    _rtn = _columnName + " = " + _parameterName;
                    break;
                case EnumSQLOperator.NoneBlanks:
                    _rtn = " NOT " + _columnName + " = " + _parameterName;
                    break;
                case EnumSQLOperator.IsNull:
                    _rtn = " " + _columnName + " is null ";
                    break;
                case EnumSQLOperator.IsNotNull:
                    _rtn = " " + _columnName + " is not null ";
                    break;
                default:
                    _rtn = "";
                    break;

            }

            return _rtn;


        }
    }
}
