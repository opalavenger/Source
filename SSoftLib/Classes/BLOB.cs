using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI;

namespace SSoft.Classes
{
    public class BLOB
    {
        string _id = "";
        string _fileName = "";
        byte[] _fileContent;
        DateTime _addDate = new DateTime();
        string _fileType = "";
        int _fileLength = 0;
        string _addUser = "";
        bool _isCheckFileExtension;
        string _fileExtension;
        string[] _allowFileExtensions;
        string _iPAddress;
        SSoft.Enum.BLOBSource _source = SSoft.Enum.BLOBSource.FromDatabse;

        public BLOB()
        {
            this._addDate = DateTime.Now;
            this._addUser = SSoft.Web.Security.User.LoginName;
            this._iPAddress = SSoft.Web.Security.User.IP;
        }


        public BLOB(System.Web.UI.WebControls.FileUpload fileUpload, string[] allowFileExtensions)
        {
            bool isAllow = false;

            if (fileUpload.HasFile)
            {

                this._isCheckFileExtension = (allowFileExtensions == null) ? false : true;
                this._fileName = fileUpload.FileName;
                string fileExtension = System.IO.Path.GetExtension(this._fileName).ToLower();
                if (_isCheckFileExtension)
                {
                    for (int i = 0; i < allowFileExtensions.Length; i++)
                    {
                        if (fileExtension == allowFileExtensions[i])
                            isAllow = true;
                    }


                    if (!isAllow)
                        throw new Exception("檔案類型不符,不允訐上載:" + this._fileName);
                }

                this._fileContent = fileUpload.FileBytes;
                this._fileType = fileUpload.PostedFile.ContentType;
                this._fileLength = fileUpload.PostedFile.ContentLength;
                this._fileExtension = fileExtension;
            }
            this._addDate = DateTime.Now;
            this._addUser = SSoft.Web.Security.User.LoginName;
            this._iPAddress = SSoft.Web.Security.User.IP;
        }



        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        public byte[] FileContent
        {
            get { return _fileContent; }
            set { _fileContent = value; }
        }

        public DateTime AddDate
        {
            get { return _addDate; }
            set { _addDate = value; }
        }

        public string FileType
        {
            get { return _fileType; }
            set { _fileType = value; }
        }

        public int FileLength
        {
            get { return _fileLength; }
            set { _fileLength = value; }
        }

        public string AddUser
        {
            get { return _addUser; }
            set { _addUser = value; }
        }

        public SSoft.Enum.BLOBSource Source
        {
            get { return _source; }
            set { _source = value; }
        }

        public bool IsCheckFileExtension
        {
            get { return _isCheckFileExtension; }
            set { _isCheckFileExtension = value; }
        }

        public string[] AallowFileExtensions
        {
            get { return _allowFileExtensions; }
            set { _allowFileExtensions = value; }
        }

        public string FileExtension
        {
            get { return _fileExtension; }
            set { _fileExtension = value; }
        }


        public string IPAddress
        {
            get { return _iPAddress; }
            set { _iPAddress = value; }
        }

        public void Update(int id, System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlClient.SqlTransaction sqlTran)
        {

            System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand("Update ASBLOB set FileContent = @FileContent,FileName=@FileName,FileLength=@FileLength,FileType=@FileType,FileExtName=@FileExtName,ModifyUserNo=@ModifyUserNo,ModifyDate=@ModifyDate,IPAddress=@IPAddress where Id=@Id", sqlConnection);
            sqlcmd.Transaction = sqlTran;

            List<SqlParameter> _p = new List<SqlParameter>();
            _p.Add(new SqlParameter("@FileContent", this.FileContent));
            _p.Add(new SqlParameter("@FileName", this.FileName));
            _p.Add(new SqlParameter("@FileLength", this.FileLength));
            _p.Add(new SqlParameter("@FileType", this.FileType));
            _p.Add(new SqlParameter("@FileExtName", this.FileExtension));
            _p.Add(new SqlParameter("@ModifyUserNo", this.AddUser));
            _p.Add(new SqlParameter("@ModifyDate", this.AddDate));
            _p.Add(new SqlParameter("@IPAddress", this.IPAddress));
            _p.Add(new SqlParameter("@id", id));

            sqlcmd.Parameters.AddRange(_p.ToArray());


            //sqlConnection.Open();

            try
            {
                sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                //sqlConnection.Close();
            }

        }

        public void Update(int id)
        {

            SqlConnection sqlConnection=new SqlConnection(SSoft.Data.MainDatabase.ConnectString);

            sqlConnection.Open();
            System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand("Update ASBLOB set FileContent = @FileContent,FileName=@FileName,FileLength=@FileLength,FileType=@FileType,FileExtName=@FileExtName,ModifyUserNo=@ModifyUserNo,ModifyDate=@ModifyDate,IPAddress=@IPAddress where Id=@Id",sqlConnection);
           

            List<SqlParameter> _p = new List<SqlParameter>();
            _p.Add(new SqlParameter("@FileContent", this.FileContent));
            _p.Add(new SqlParameter("@FileName", this.FileName));
            _p.Add(new SqlParameter("@FileLength", this.FileLength));
            _p.Add(new SqlParameter("@FileType", this.FileType));
            _p.Add(new SqlParameter("@FileExtName", this.FileExtension));
            _p.Add(new SqlParameter("@ModifyUserNo", this.AddUser));
            _p.Add(new SqlParameter("@ModifyDate", this.AddDate));
            _p.Add(new SqlParameter("@IPAddress", this.IPAddress));
            _p.Add(new SqlParameter("@id", id));

            sqlcmd.Parameters.AddRange(_p.ToArray());

            //sqlConnection.Open();

            try
            {
                sqlcmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }

        }

        public int Insert(System.Data.SqlClient.SqlConnection sqlConnection, System.Data.SqlClient.SqlTransaction sqlTran)
        {
            System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand("Insert into ASBLOB(FileContent,FileName,FileLength,FileType,FileExtName,AddUserNo,AddDate,IPAddress) Values(@FileContent,@FileName,@FileLength,@FileType,@FileExtName,@AddUserNo,@AddDate,@IPAddress);", sqlConnection);
            sqlcmd.Transaction = sqlTran;
            List<SqlParameter> _p = new List<SqlParameter>();
            _p.Add(new SqlParameter("@FileContent", this.FileContent));
            _p.Add(new SqlParameter("@FileName", this.FileName));
            _p.Add(new SqlParameter("@FileLength", this.FileLength));
            _p.Add(new SqlParameter("@FileType", this.FileType));
            _p.Add(new SqlParameter("@FileExtName", this.FileExtension));
            _p.Add(new SqlParameter("@AddUserNo", this.AddUser));
            _p.Add(new SqlParameter("@AddDate", this.AddDate));
            _p.Add(new SqlParameter("@IPAddress", this.IPAddress));

            sqlcmd.Parameters.AddRange(_p.ToArray());

            try
            {
                sqlcmd.ExecuteNonQuery();
                sqlcmd.CommandText = "select @@IDENTITY";
                int blobId = Convert.ToInt32(sqlcmd.ExecuteScalar());

                return blobId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public int Insert()
        {
            SqlConnection sqlConnection = new SqlConnection(SSoft.Data.MainDatabase.ConnectString);
            sqlConnection.Open();
            System.Data.SqlClient.SqlCommand sqlcmd = new System.Data.SqlClient.SqlCommand("Insert into ASBLOB(FileContent,FileName,FileLength,FileType,FileExtName,AddUserNo,AddDate,IPAddress) Values(@FileContent,@FileName,@FileLength,@FileType,@FileExtName,@AddUserNo,@AddDate,@IPAddress);", sqlConnection);
           
            List<SqlParameter> _p = new List<SqlParameter>();
            _p.Add(new SqlParameter("@FileContent", this.FileContent));
            _p.Add(new SqlParameter("@FileName", this.FileName));
            _p.Add(new SqlParameter("@FileLength", this.FileLength));
            _p.Add(new SqlParameter("@FileType", this.FileType));
            _p.Add(new SqlParameter("@FileExtName", this.FileExtension));
            _p.Add(new SqlParameter("@AddUserNo", this.AddUser));
            _p.Add(new SqlParameter("@AddDate", this.AddDate));
            _p.Add(new SqlParameter("@IPAddress", this.IPAddress));

            sqlcmd.Parameters.AddRange(_p.ToArray());

            try
            {
                sqlcmd.ExecuteNonQuery();
                sqlcmd.CommandText = "select @@IDENTITY";
                int blobId = Convert.ToInt32(sqlcmd.ExecuteScalar());

                return blobId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        static public MemoryStream GetImage(string _id, SSoft.Data.SqlDataSourceBase _db)
        {

            _db.SelectCommand = "Select FileContent from ASBLOB where Id=@Id";
            _db.SelectParameters.Add("Id", TypeCode.Int32, _id);

            try
            {
                DataView dv = (DataView)_db.Select(DataSourceSelectArguments.Empty);
                Byte[] PhotoImage = (Byte[])dv[0]["FileContent"];
                return new MemoryStream(PhotoImage, 0, PhotoImage.Length);
            }
            catch
            {
                return null;
            }
        }
    }
}
