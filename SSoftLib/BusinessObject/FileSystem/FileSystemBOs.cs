using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace SSoft.BusinessObject.FileSystem
{
    public class FileSystemBOs
    {
        public static Datasets.FileSystemDS.DirectoryDataTable GetDataTable()
        {
            Datasets.FileSystemDS.DirectoryDataTable returnDataTable = new SSoft.BusinessObject.FileSystem.Datasets.FileSystemDS.DirectoryDataTable();

            return returnDataTable;
        }

        public static Datasets.FileSystemDS.FileSizeDataTable GetFileSizeDataTable(string type, int sceneTypeId)
        {
            Datasets.FileSystemDS.FileSizeDataTable returnDataTable = new SSoft.BusinessObject.FileSystem.Datasets.FileSystemDS.FileSizeDataTable();

            DataTable dtResolution = SSoft.Data.SqlHelper.SelectTable("select id from CameraResolution order by OrderSR");

            DataTable dt = SSoft.Data.SqlHelper.SelectTable(string.Format("select id,ClassNo from Camera{0} order by OrderSR", type));

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Datasets.FileSystemDS.FileSizeRow row = returnDataTable.NewFileSizeRow();
                row.Id = dt.Rows[i][0].ToString();
                row.Display = dt.Rows[i][1].ToString();
                for (int j = 0; j < dtResolution.Rows.Count; j++)
                {

                    object value = SSoft.Data.SqlHelper.SelectScalar(string.Format("select Value from CameraFileSizeRate where id1={1} and id2={2} and id3={3} and type='{0}' ", type, dtResolution.Rows[j][0].ToString(), row.Id, sceneTypeId));
                    if (value != null)
                    {
                        row["value" + (j + 1).ToString("00")] = value.ToString();
                    }
                    else
                    {
                        row["value" + (j + 1).ToString("00")] = "0";
                    }
                    
                }
                returnDataTable.Rows.Add(row);
            }

            return returnDataTable;
        }
    }
}
