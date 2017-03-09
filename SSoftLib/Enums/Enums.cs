using System;
using System.Collections.Generic;
using System.Text;

namespace SSoft.Enum
{
    public enum EnumPopupType
    {
        Modal,
        Modeless,
        General
    }

    public enum EnumTargetType
    {
        _blank,
        _media,
        _parent,
        _search,
        _self,
        _top
    }

    public enum EnumPermission
    {
        Enable,
        Disable
    }

    public enum EnumPermissionType
    {
        Execute,
        Add,
        Query,
        Modify,
        Confirm,
        UnConfirm,
        Delete,
        Cost,
        Copy,
        signing
    }

    public enum BLOBSource
    {
        FromDatabse = 0,
        FromSession = 1,
        FromFile = 2,
    }

    public enum EnumTabControlType
    {
        Master,
        Detail,
        DDetail,
        DDDetail,
        Report,
        None
    }

    public enum EnumOperator
    {
        Equal,
        NotEqual,
        Or,
        LessThan,
        GreaterThan,
        Between,
        GreaterThanOrEqual,
        LessThanOrEqual,
        Like
    }

    public enum PatternType
    {
        Master,
        Detail,
        /// <summary>
        /// 查詢
        /// </summary>
        Query,
        /// <summary>
        /// 單檔單筆
        /// </summary>
        MSS,
        /// <summary>
        /// 雙檔
        /// </summary>
        MMD,
        /// <summary>
        /// 批次
        /// </summary>
        BCH,
        /// <summary>
        /// 設定
        /// </summary>
        CFG,
        /// <summary>
        /// 報表
        /// </summary>
        RPT
    }

    public enum EnumMaintainStatus
    {
        Insert,
        Modify,
        ReadOnly
    }


    public enum EnumIsChildAccount : int
    {
        /// <summary>
        /// 是
        /// </summary>
        N = 0,
        /// <summary>
        ///否
        /// </summary>
        Y = 1
    }


    public static class EnumMethods

    {
        public static String GetString(this EnumOperator type)
        {
            switch (type)
            {
                case EnumOperator.Equal:
                    return "=";
                case EnumOperator.GreaterThan:
                    return ">";
                case EnumOperator.LessThan:
                    return "<";
                case EnumOperator.GreaterThanOrEqual:
                    return ">=";
                case EnumOperator.LessThanOrEqual:
                    return "<=";
                case EnumOperator.NotEqual:
                    return "<>";
                case EnumOperator.Like:
                    return "like";

                default:
                    return "=";
            }
        }

        public static String GetString(this EnumOperator type,string columnNmae,string value,string valueType)
        {
            switch (type)
            {
                case EnumOperator.Equal:
                    return string.Format("{2}.ToLower().Equal({1}{0}){1}", value,(valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.GreaterThan:
                    return string.Format("{2}.ToLower().CompareTo({1}{0}){1}>0", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.LessThan:
                    return string.Format("{2}.ToLower().CompareTo({1}{0}){1}<0", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.GreaterThanOrEqual:
                    return string.Format("{2}.ToLower().CompareTo({1}{0}){1}>=0", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.LessThanOrEqual:
                    return string.Format("{2}.ToLower().CompareTo({1}{0}){1}<=0", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.NotEqual:
                    return string.Format("!{2}.ToLower().Equal({1}{0}){1}", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);
                case EnumOperator.Like:
                    return string.Format("{2}.ToLower().Contains({1}{0}){1}", value, (valueType.ToUpper() == "STRING" ? "" : ""), columnNmae);

                default:
                    return "=";
            }
        }

    }
}
