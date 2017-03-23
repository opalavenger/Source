using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC.Events
{
    public class ExportExcelEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }
        public string Message { get; set; }
        public object Workbook { get; set; }
        public object Sheet { get; set; }

        public object Style { get; set; }

        public dynamic Entity { get; set; }

        public int FirstGridRow { get; set; }

        public ExportExcelEventArgs(bool cancel, string message, object workbook, object sheet, object entity, int firstGridRow)
        {
            this.Cancel = cancel;
            this.Message = message;
            this.Workbook = workbook;
            this.Sheet = sheet;
            this.Entity = entity;
            this.FirstGridRow = firstGridRow;
        }
    }
    public class FileUploadEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }
        public string Message { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public long FileSize { get; set; }
        public string GUID { get; set; }
        public object Data { get; set; }

        public object MultipartFormData { get; set; }

        public FileUploadEventArgs(bool cancel, string fileName, string filePath, long fileSize, string guid)
        {
            this.Cancel = cancel;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.FileSize = fileSize;
            this.GUID = guid;

        }
    }
    public class SaveEntityEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }

        public string Message { get; set; }
        public object Datas { get; set; }

        public object ModelCurrent { get; set; }

        public object ModelOriginal { get; set; }

        public object DBModel { get; set; }

        public SaveEntityEventArgs(bool cancel, object datas, object modelCurrent, object modelOriginal, object dbModel)
        {
            this.Cancel = cancel;
            this.Datas = datas;
            this.ModelCurrent = modelCurrent;
            this.ModelOriginal = modelOriginal;
            this.DBModel = dbModel;
           
        }
    }

    public class DeleteEntityEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }
        public string Message { get; set; }
        public object Datas { get; set; }

        public object ModelCurrent { get; set; }

        public object ModelOriginal { get; set; }
        public object DBModel { get; set; }
        public DeleteEntityEventArgs(bool cancel, object datas, object modelCurrent, object modelOriginal, object dbModel)
        {
            this.Cancel = cancel;
            this.Datas = datas;
            this.ModelCurrent = modelCurrent;
            this.ModelOriginal = modelOriginal;
            this.DBModel = dbModel;
        }
    }

    public class ConditonQueryEntityEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }
        public string Message { get; set; }
        public object Datas { get; set; }
        public object Condition { get; set; }
        public List<SSoft.MVC.ConditionParameter> ConditionParameters { get; set; }
        public object DBModel { get; set; }
        public ConditonQueryEntityEventArgs(bool cancel, object datas, object condition, List<SSoft.MVC.ConditionParameter> conditionParameters, object dbModel)
        {
            this.Cancel = cancel;
            this.Datas = datas;
            this.Condition = condition;
            this.ConditionParameters = conditionParameters;
            this.DBModel = dbModel;
        }
    }

    public class RetrieveEntityEventArgs : System.EventArgs
    {
        public bool Cancel { get; set; }
        public string Message { get; set; }
        public object Datas { get; set; }
        public object Key { get; set; }
        public object DBModel { get; set; }
        public RetrieveEntityEventArgs(bool cancel, object datas, object key, object dbModel)
        {
            this.Cancel = cancel;
            this.Datas = datas;
            this.Key = key;
            this.DBModel = dbModel;
        }
    }
}
