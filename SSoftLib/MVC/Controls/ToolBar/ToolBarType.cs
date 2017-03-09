using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC.Controls.ToolBar
{
    
    public enum ToolBarItemType
    {
        /// <summary>
        /// 資料查詢
        /// </summary>
        Query,
        /// <summary>
        /// 清除條件
        /// </summary>
        QueryReset,
        /// <summary>
        /// 執行
        /// </summary>
        Run,
        /// <summary>
        /// 新增
        /// </summary>
        Add,
        /// <summary>
        /// 刪除
        /// </summary>
        Delete,
        /// <summary>
        /// 儲存
        /// </summary>
        Save,
        /// <summary>
        /// 自動截取
        /// </summary>
        AutoRetrieve,
        /// <summary>
        /// 列印
        /// </summary>
        Ptint,
        /// <summary>
        /// 單據確認
        /// </summary>
        Confirm,
        /// <summary>
        /// 單據取消確認
        /// </summary>
        UnConfirm,
        /// <summary>
        /// 單據作廢
        /// </summary>
        CancelConfirm,
        /// <summary>
        /// 線上說明
        /// </summary>
        OnlineHelp,
        /// <summary>
        /// 資訊
        /// </summary>
        Information,
        /// <summary>
        /// 離開
        /// </summary>
        Exit,
        /// <summary>
        /// 明細新增
        /// </summary>
        DetailAdd,
        /// <summary>
        /// 明細刪除
        /// </summary>
        DetailDelete,
        /// <summary>
        /// 第一筆查詢資料
        /// </summary>
        FirstRow,
        /// <summary>
        /// 上一筆查詢資料
        /// </summary>
        PreviousRow,
        /// <summary>
        /// 下一筆查詢資料
        /// </summary>
        NextRow,
        /// <summary>
        /// 最後一筆查詢資料
        /// </summary>
        LastRow

    }

    public static class ToolBarItemTypeMethods
    {
        public static String GetImagePath(this ToolBarItemType type)
        {
            switch (type)
            {
                case ToolBarItemType.Query:
                    return "~/Images/ToolBar/glyphicons-529-database-search.png";
                case ToolBarItemType.QueryReset:
                    return "~/Images/ToolBar/glyphicons-82-refresh.png";
                case ToolBarItemType.Run:
                    return "~/Images/ToolBar/glyphicons-592-person-running.png";
                case ToolBarItemType.Add:
                    return "~/Images/ToolBar/glyphicons-433-plus.png";
                case ToolBarItemType.Delete:
                    return "~/Images/ToolBar/glyphicons-208-remove-2.png";
                case ToolBarItemType.Save:
                    return "~/Images/ToolBar/glyphicons-207-ok-2.png";
                case ToolBarItemType.AutoRetrieve:
                    return "~/Images/ToolBar/glyphicons-182-download-alt.png";
                case ToolBarItemType.Ptint:
                    return "~/Images/ToolBar/glyphicons-16-print.png";
                case ToolBarItemType.Confirm:
                    return "~/Images/ToolBar/glyphicons-194-circle-ok.png";
                case ToolBarItemType.UnConfirm:
                    return "~/Images/ToolBar/glyphicons-193-circle-remove.png";
                case ToolBarItemType.CancelConfirm:
                    return "~/Images/ToolBar/glyphicons-192-circle-minus.png";
                case ToolBarItemType.OnlineHelp:
                    return "~/Images/ToolBar/glyphicons-195-circle-question-mark.png";
                case ToolBarItemType.Information:
                    return "~/Images/ToolBar/glyphicons-196-circle-info.png";
                case ToolBarItemType.Exit:
                    return "~/Images/ToolBar/glyphicons-389-exit.png";
                case ToolBarItemType.DetailAdd:
                    return "~/Images/ToolBar/glyphicons-109-left-indent.png";
                case ToolBarItemType.DetailDelete:
                    return "~/Images/ToolBar/glyphicons-446-floppy-remove.png";
                case ToolBarItemType.FirstRow:
                    return "~/Images/ToolBar/glyphicons-171-step-backward.png";
                case ToolBarItemType.PreviousRow:
                    return "~/Images/ToolBar/glyphicons-174-play-back.png";
                case ToolBarItemType.NextRow:
                    return "~/Images/ToolBar/glyphicons-174-play.png";
                case ToolBarItemType.LastRow:
                    return "~/Images/ToolBar/glyphicons-179-step-forward.png";
                default:
                    return "";
            }
        }

        public static String GetItemName(this ToolBarItemType type)
        {
            switch (type)
            {
                case ToolBarItemType.Query:
                    return "查詢";
                case ToolBarItemType.QueryReset:
                    return "清除條件";
                case ToolBarItemType.Run:
                    return "執行";
                case ToolBarItemType.Add:
                    return "新增";
                case ToolBarItemType.Delete:
                    return "刪除";
                case ToolBarItemType.Save:
                    return "儲存";
                case ToolBarItemType.AutoRetrieve:
                    return "自動截取";
                case ToolBarItemType.Ptint:
                    return "列印";
                case ToolBarItemType.Confirm:
                    return "單據確認";
                case ToolBarItemType.UnConfirm:
                    return "單據取消確認";
                case ToolBarItemType.CancelConfirm:
                    return "單據作廢";
                case ToolBarItemType.OnlineHelp:
                    return "線上說明";
                case ToolBarItemType.Information:
                    return "系統資訊";
                case ToolBarItemType.Exit:
                    return "離開";
                case ToolBarItemType.DetailAdd:
                    return "新增明細";
                case ToolBarItemType.DetailDelete:
                    return "刪除明細";
                case ToolBarItemType.FirstRow:
                    return "第一筆";
                case ToolBarItemType.PreviousRow:
                    return "上一筆";
                case ToolBarItemType.NextRow:
                    return "下一筆";
                case ToolBarItemType.LastRow:
                    return "最後一筆";
                default:
                    return "";
            }
        }

        public static String GetItemFunctionName(this ToolBarItemType type)
        {
            switch (type)
            {
                case ToolBarItemType.Query:
                    return "Query()";
                case ToolBarItemType.QueryReset:
                    return "QueryReset()";
                case ToolBarItemType.Run:
                    return "Run()";
                case ToolBarItemType.Add:
                    return "Add()";
                case ToolBarItemType.Delete:
                    return "Delete()";
                case ToolBarItemType.Save:
                    return "Save()";
                case ToolBarItemType.AutoRetrieve:
                    return "AutoRetrieve()";
                case ToolBarItemType.Ptint:
                    return "Ptint()";
                case ToolBarItemType.Confirm:
                    return "Confirm()";
                case ToolBarItemType.UnConfirm:
                    return "UnConfirm()";
                case ToolBarItemType.CancelConfirm:
                    return "CancelConfirm()";
                case ToolBarItemType.OnlineHelp:
                    return "OnlineHelp()";
                case ToolBarItemType.Information:
                    return "Information()";
                case ToolBarItemType.Exit:
                    return "Exit()";
                case ToolBarItemType.DetailAdd:
                    return "DetailAdd()";
                case ToolBarItemType.DetailDelete:
                    return "DetailDelete()";
                case ToolBarItemType.FirstRow:
                    return "SelectQueryFirstRow()";
                case ToolBarItemType.PreviousRow:
                    return "SelectQueryPreviousRow()";
                case ToolBarItemType.NextRow:
                    return "SelectQueryNextRow()";
                case ToolBarItemType.LastRow:
                    return "SelectQueryLastRow()";
                default:
                    return "";
            }
        }

    }
}
