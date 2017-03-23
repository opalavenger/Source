using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace YMIR.Common
{
    public class FileSyncInput
    {
        public int SyncType { get; set; } // 請填0
        public ArrayList PicNO { get; set; } //圖片檔案名稱

    }
    public static class FileHelper
    {

        public static void SendFileSync(FileSyncInput input)
        {
            try
            {
                string PicTempAPIURL = System.Web.Configuration.WebConfigurationManager.AppSettings["PicTempAPIURL"]; 
                var serializer = new JavaScriptSerializer();
                var jsonText = serializer.Serialize(input);
                var jsonBytes = Encoding.UTF8.GetBytes(jsonText);
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(PicTempAPIURL);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/json";
                request.ContentLength = jsonBytes.Length;
                using (var requestStream = request.GetRequestStream())
                {
                    requestStream.Write(jsonBytes, 0, jsonBytes.Length);
                    requestStream.Flush();
                }
            }
            catch(Exception)
            { }
        }

        public static void SaveFileToDB(string filePath)
        {
            byte[] fileBytes;
            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = new BinaryReader(stream))
                {
                    fileBytes = reader.ReadBytes((int)stream.Length);
                }
            }
            string fileName = Path.GetFileName(filePath);

            SSoft.Data.SqlHelper.SelectNonQuery("INSERT INTO FileTempPath (FileName,FileContent) VALUES (@FileName,@FileContent)", new SqlParameter[] { new SqlParameter("@FileName", fileName), new SqlParameter("@FileContent", fileBytes) }, System.Web.Configuration.WebConfigurationManager.ConnectionStrings["FubonPicTempDBConnectionString"].ConnectionString);

            FileSyncInput fileSyncInput = new FileSyncInput();
            fileSyncInput.SyncType = 0;
            fileSyncInput.PicNO = new ArrayList();
            fileSyncInput.PicNO.Add(fileName);

            SendFileSync(fileSyncInput);
        }


        public static void SaveFile(byte[] content, string path)
        {
            string filePath = GetFileFullPath(path);
            if (!Directory.Exists(Path.GetDirectoryName(filePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            }

            //Save file
            using (FileStream str = File.Create(filePath))
            {
                str.Write(content, 0, content.Length);
            }
        }

        public static string GetFileFullPath(string path)
        {
            string relName = path.StartsWith("~") ? path : path.StartsWith("/") ? string.Concat("~", path) : path;

            string filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

            return filePath;
        }
    }

}