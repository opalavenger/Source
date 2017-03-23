using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq.Dynamic;
using System.Web;
using System.Web.Mvc;
using YMIR.Models;
using SSoft.MVC;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Configuration;
using System.Drawing;
using SSoft.Enum;
using NPOI;
using NPOI.HPSF;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.HSSF.Util;
using NPOI.SS.Util;
using System.Web.Http.Controllers;

namespace YMIR.Controllers.SYSBase
{
    public class BaseAPIController : ApiController, SSoft.MVC.Interfaces.API.IMaintain
    {
        
        //Models.VAN_DBEntities _db = new Models.VAN_DBEntities();
        public Models.IRFEntities DB { get; set; }
        public int ProgramId { get; set; }
 
        public BaseAPIController()
        {
            this.DB = new IRFEntities();
            this.POSTInit();
        }

        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            //here the routedata is available
            string controller_name = ControllerContext.RouteData.Values["Controller"].ToString();

            string ProgramNo = controller_name;
           
            var findProgram = this.DB.WebUserPage.Where(t => t.WUPage_No == ProgramNo);

            if (findProgram.Count() > 0)
            {
                this.ProgramId = this.DB.WebUserPage.Where(t => t.WUPage_No == ProgramNo).First().WUPage_Id;
            }
        }
        public HttpResponseMessage GetPartialView(string id)
        {
            string partialViewName = string.Format("~/Views/{0}/Partial/{1}.cshtml", this.ControllerContext.RouteData.Values["controller"].ToString(), id);
            var body = SSoft.MVC.ViewRender.RenderPartialView(partialViewName);

            var response = new HttpResponseMessage();
            response.Content = new StringContent(body);
            response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;

        }

        virtual public void POSTInit()
        {
            //throw new NotImplementedException();
        }

        public dynamic POSTSaveModel(object value)
        {
            return POSTSaveModelByType(value, this.DataObjectInfo.InputModelObjectType, this.DataObjectInfo.ColumnPrefix);
        }
        public dynamic POSTSaveModelByType(object value, Type saveModel, string columnPrefix)
        {
            dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

            dynamic currentValue = entity["modelCurrent"];
            dynamic originalValue = entity["modelOriginal"];

            try
            {
                //DB = new BountyDBEntities1();
                DB.Configuration.AutoDetectChangesEnabled = false;
                DB.Configuration.ValidateOnSaveEnabled = false;

                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(currentValue.ToString(), saveModel,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                var original = Newtonsoft.Json.JsonConvert.DeserializeObject(originalValue.ToString(), saveModel,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });


                if (this.USaveBeforeEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveBeforeEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                int originalId = Convert.ToInt32(original.GetType().GetProperty(this.DataObjectInfo.KeyName).GetValue(original, null));

                if (originalId == 0)
                {
                    if (current.GetType().GetProperty(columnPrefix + "_CreateUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_CreateUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_CreatedUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_CreateUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    //current.GetType().GetProperty("OWNER_GRP_NO").SetValue(current, "", null);
                    if (current.GetType().GetProperty(columnPrefix + "_CreateTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_CreateTime").SetValue(current, DateTime.Now, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_CreatedTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_CreatedTime").SetValue(current, DateTime.Now, null);
                    }
                    //current.GetType().GetProperty("IP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostAddress, null);
                    //current.GetType().GetProperty("CP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostName, null);
                    if (current.GetType().GetProperty(columnPrefix + "_ModifyUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifyUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedUserNo") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedUserNo").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifyTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifyTime").SetValue(current, DateTime.Now, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedTime").SetValue(current, DateTime.Now, null);
                    }
                    DB.Entry(current).State = EntityState.Added;
                }




                if (originalId != 0)
                {

                    //if (!Helper.ObjectComparator(current, original))
                    //{
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedUserNo") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedUserNo").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifyUser") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifyUser").SetValue(current, SSoft.Web.Security.User.Emp_ID, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifyTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifyTime").SetValue(current, DateTime.Now, null);
                    }
                    if (current.GetType().GetProperty(columnPrefix + "_ModifiedTime") != null)
                    {
                        current.GetType().GetProperty(columnPrefix + "_ModifiedTime").SetValue(current, DateTime.Now, null);
                    }
                    //current.GetType().GetProperty("IP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostAddress, null);
                    //current.GetType().GetProperty("CP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostName, null);

                    DB.Entry(current).State = EntityState.Modified;
                    //}                    
                }


                if (this.USaveAfterDBChangeBeforeEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveAfterDBChangeBeforeEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                DB.SaveChanges();
                if (originalId == 0)
                {
                    App_Code.Utils.InsertActionLog(this.ProgramId, 1, "", current.GetType().GetProperty(this.DataObjectInfo.NoColumnName).GetValue(current, null).ToString());
                }
                else
                {
                    App_Code.Utils.InsertActionLog(this.ProgramId, 2, "", current.GetType().GetProperty(this.DataObjectInfo.NoColumnName).GetValue(current, null).ToString());
                }
                if (this.USaveAfterDBChangeAfterEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveAfterDBChangeAfterEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                //return POSTRetrieveDataByKey(Convert.ToInt32(current.GetType().GetProperty(columnPrefix + "_Id").GetValue(current, null)));

                //string jsonStirng = App_Code.Utils.ReturnJsonMessageAndData("", POSTRetrieveDataByKey(Convert.ToInt32(current.GetType().GetProperty(columnPrefix + "_Id").GetValue(current, null))), false);

                //var response = this.Request.CreateResponse();
                //response.Content = new StringContent(jsonStirng);
                //response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
                //return response;
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "", POSTRetrieveDataByKey(Convert.ToInt32(current.GetType().GetProperty(this.DataObjectInfo.KeyName).GetValue(current, null))));
            }
            catch (Exception ex)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                //{
                //    Content = new StringContent(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "")),
                //    ReasonPhrase = ex.Message
                //};
                //throw new HttpResponseException(resp);

                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex, true);
            }
        }

        public dynamic POSTSave(object value)
        {
            return POSTSave1(value, new List<Models.Sys.UploadDataModel>());
        }

        public dynamic POSTSave1(object value, List<YMIR.Models.Sys.UploadDataModel> uploads)
        {
            dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

            dynamic modelCurrent = entity["modelCurrent"];
            dynamic modelOriginal = entity["modelOriginal"];
            return POSTSave2(value, modelCurrent, modelOriginal, uploads);
        }
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public dynamic POSTSave2(object value, object currentValue, object originalValue, List<YMIR.Models.Sys.UploadDataModel> uploads)
        {

            try
            {
                //DB = new BountyDBEntities1();
                DB.Configuration.AutoDetectChangesEnabled = false;
                DB.Configuration.ValidateOnSaveEnabled = false;

                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(currentValue.ToString(), this.DataObjectInfo.InputModelObjectType,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                var original = Newtonsoft.Json.JsonConvert.DeserializeObject(originalValue.ToString(), this.DataObjectInfo.InputModelObjectType,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });


                if (this.USaveBeforeEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveBeforeEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                int originalId = Convert.ToInt32(original.GetType().GetProperty("ID").GetValue(original, null));

                //DB.Entry(current).State = EntityState.Unchanged;
                //DB.SaveChanges();
                //return POSTRetrieveDataByKey(Convert.ToInt32(current.GetType().GetProperty("ID").GetValue(current, null)));

                if (originalId == 0)
                {
                    current.GetType().GetProperty("OWNER_USR_NO").SetValue(current, System.Web.HttpContext.Current.User.Identity.Name, null);
                    current.GetType().GetProperty("OWNER_GRP_NO").SetValue(current, "", null);
                    current.GetType().GetProperty("ADD_DT").SetValue(current, DateTime.Now, null);
                    current.GetType().GetProperty("IP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostAddress, null);
                    current.GetType().GetProperty("CP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostName, null);

                    DB.Entry(current).State = EntityState.Added;
                }
                //else
                //{
                //    //if (!Helper.ObjectComparator(current, original))
                //    //{
                //    current.GetType().GetProperty("MDY_USR_NO").SetValue(current, System.Web.HttpContext.Current.User.Identity.Name, null);
                //    current.GetType().GetProperty("MDY_DT").SetValue(current, DateTime.Now, null);
                //    current.GetType().GetProperty("IP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostAddress, null);
                //    current.GetType().GetProperty("CP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostName, null);

                //    DB.Entry(current).State = EntityState.Modified;
                //    //}                    
                //}

                List<string> includeTableLevel1s = new List<string>();
                List<string> includeTableLevel2s = new List<string>();
                List<string> includeTableLevel3s = new List<string>();

                foreach (string tableName in this.DataObjectInfo.IncludeTables)
                {
                    string[] tableNames = tableName.Split(new char[] { '.' });

                    foreach (var findTable in this.DataObjectInfo.IncludeTables.Where(t => t.StartsWith(tableNames[0])))
                    {
                        if (!includeTableLevel1s.Exists(t => t.Equals(tableNames[0])))
                        {
                            includeTableLevel1s.Add(tableNames[0]);
                        }
                    }
                    if (tableNames.Length > 1)
                    {
                        foreach (var findTable in this.DataObjectInfo.IncludeTables.Where(t => t.StartsWith(tableNames[0] + "." + tableNames[1])))
                        {
                            if (!includeTableLevel2s.Exists(t => t.Equals(tableNames[1])))
                            {
                                includeTableLevel2s.Add(tableNames[1]);
                            }
                        }
                    }

                    if (tableNames.Length > 2)
                    {
                        foreach (var findTable in this.DataObjectInfo.IncludeTables.Where(t => t.StartsWith(tableNames[0] + "." + tableNames[1] + "." + tableNames[2])))
                        {
                            if (!includeTableLevel3s.Exists(t => t.Equals(tableNames[2])))
                            {
                                includeTableLevel3s.Add(tableNames[2]);
                            }
                        }
                    }

                }

                foreach (string tableName in includeTableLevel1s)
                {

                    var originalChildTable = original.GetType().GetProperty(tableName).GetValue(original, null);
                    var currentChildTable = current.GetType().GetProperty(tableName).GetValue(current, null);


                    foreach (var originalRow in originalChildTable as IEnumerable<object>)
                    {
                        int originalChildTableId = Convert.ToInt32(originalRow.GetType().GetProperty("ID").GetValue(originalRow, null));


                        var findCurrentRow = FindRow(currentChildTable, originalChildTableId);
                        //var findCurrentRows = System.Linq.Enumerable.ToList(currentChildTable).Where("ID==@0", originalChildTableId);
                        if (findCurrentRow == null)
                        {
                            //BountyDBEntities DB1 = new BountyDBEntities();
                            //DB1.Entry(originalRow).State = EntityState.Deleted;
                            //DB1.SaveChanges();
                            SSoft.Data.SqlHelper.SelectNonQuery("delete " + tableName + " where ID=@ID", new SqlParameter[] { new SqlParameter("@ID", originalChildTableId) });

                        }
                        else
                        {
                            //if (!Helper.ObjectComparator(originalRow, findCurrentRow))
                            //{
                            foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                            {
                                findCurrentRow.GetType().GetProperty(deliveryColumn).SetValue(findCurrentRow, current.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                            }


                            //Child Table Level3
                            foreach (string tableName2 in includeTableLevel2s)
                            {

                                if (originalRow.GetType().GetProperty(tableName2) != null)
                                {

                                    var originalChildTable2 = originalRow.GetType().GetProperty(tableName2).GetValue(originalRow, null);
                                    var currentChildTable2 = findCurrentRow.GetType().GetProperty(tableName2).GetValue(findCurrentRow, null);

                                    foreach (var originalRow2 in originalChildTable2 as IEnumerable<object>)
                                    {
                                        int originalChildTableId2 = Convert.ToInt32(originalRow2.GetType().GetProperty("ID").GetValue(originalRow2, null));


                                        var findCurrentRow2 = FindRow(currentChildTable2, originalChildTableId2);
                                        //var findCurrentRows = System.Linq.Enumerable.ToList(currentChildTable).Where("ID==@0", originalChildTableId);
                                        if (findCurrentRow2 == null)
                                        {
                                            //BountyDBEntities DB2 = new BountyDBEntities();
                                            //DB2.Entry(originalRow2).State = EntityState.Deleted;

                                            //object entityDeletes = (entitys as IQueryable).Where("ID=@0", originalId);

                                            //foreach (var item in (entityDeletes as IQueryable))
                                            //{
                                            //    DB2.Entry(item).State = EntityState.Deleted;
                                            //}


                                            //DB2.SaveChanges();

                                            SSoft.Data.SqlHelper.SelectNonQuery("delete " + tableName2 + " where ID=@ID", new SqlParameter[] { new SqlParameter("@ID", originalChildTableId2) });
                                        }
                                        else
                                        {
                                            //if (!Helper.ObjectComparator(originalRow, findCurrentRow))
                                            //{
                                            foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                                            {
                                                if (findCurrentRow2.GetType().GetProperty(deliveryColumn) != null)
                                                {
                                                    findCurrentRow2.GetType().GetProperty(deliveryColumn).SetValue(findCurrentRow2, currentChildTable2.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                                                }
                                            }


                                            //Child Table Level4
                                            foreach (string tableName3 in includeTableLevel3s)
                                            {

                                                if (findCurrentRow2.GetType().GetProperty(tableName3) != null)
                                                {

                                                    var originalChildTable3 = originalRow2.GetType().GetProperty(tableName3).GetValue(originalRow2, null);
                                                    var currentChildTable3 = findCurrentRow2.GetType().GetProperty(tableName3).GetValue(findCurrentRow2, null);

                                                    foreach (var originalRow3 in originalChildTable3 as IEnumerable<object>)
                                                    {
                                                        int originalChildTableId3 = Convert.ToInt32(originalRow3.GetType().GetProperty("ID").GetValue(originalRow3, null));


                                                        var findCurrentRow3 = FindRow(currentChildTable3, originalChildTableId3);
                                                        //var findCurrentRows = System.Linq.Enumerable.ToList(currentChildTable).Where("ID==@0", originalChildTableId);
                                                        if (findCurrentRow3 == null)
                                                        {
                                                            //BountyDBEntities DB3 = new BountyDBEntities();
                                                            //DB3.Entry(originalRow3).State = EntityState.Deleted;
                                                            //DB3.SaveChanges();
                                                            SSoft.Data.SqlHelper.SelectNonQuery("delete " + tableName3 + " where ID=@ID", new SqlParameter[] { new SqlParameter("@ID", originalChildTableId3) });
                                                        }
                                                        else
                                                        {
                                                            //if (!Helper.ObjectComparator(originalRow, findCurrentRow))
                                                            //{
                                                            foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                                                            {
                                                                if (findCurrentRow3.GetType().GetProperty(deliveryColumn) != null)
                                                                {
                                                                    findCurrentRow3.GetType().GetProperty(deliveryColumn).SetValue(findCurrentRow3, currentChildTable3.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                                                                }
                                                            }
                                                            DB.Entry(findCurrentRow3).State = EntityState.Modified;



                                                            //}
                                                        }
                                                    }



                                                    foreach (var currentNewRow3 in currentChildTable3 as IEnumerable<object>)
                                                    {
                                                        int id = Convert.ToInt32(currentNewRow3.GetType().GetProperty("ID").GetValue(currentNewRow3, null));
                                                        if (id == 0)
                                                        {
                                                            DB.Entry(currentNewRow3).State = EntityState.Added;
                                                            currentNewRow3.GetType().GetProperty("PID").SetValue(currentNewRow3, findCurrentRow2.GetType().GetProperty("ID").GetValue(findCurrentRow2, null), null);
                                                        }

                                                        foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                                                        {
                                                            if (currentNewRow3.GetType().GetProperty(deliveryColumn) != null)
                                                            {
                                                                currentNewRow3.GetType().GetProperty(deliveryColumn).SetValue(currentNewRow3, current.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                                                            }
                                                        }


                                                    }
                                                }
                                            }

                                            DB.Entry(findCurrentRow2).State = EntityState.Modified;
                                        }
                                    }



                                    foreach (var currentNewRow2 in currentChildTable2 as IEnumerable<object>)
                                    {
                                        int id = Convert.ToInt32(currentNewRow2.GetType().GetProperty("ID").GetValue(currentNewRow2, null));
                                        if (id == 0)
                                        {
                                            DB.Entry(currentNewRow2).State = EntityState.Added;

                                            foreach (string tableName3 in includeTableLevel3s)
                                            {
                                                if (currentNewRow2.GetType().GetProperty(tableName3) != null)
                                                {
                                                    foreach (var currentNewRow3 in currentNewRow2.GetType().GetProperty(tableName3).GetValue(currentNewRow2, null) as IEnumerable<object>)
                                                    {
                                                        int id3 = Convert.ToInt32(currentNewRow3.GetType().GetProperty("ID").GetValue(currentNewRow3, null));
                                                        if (id3 == 0)
                                                        {
                                                            DB.Entry(currentNewRow3).State = EntityState.Added;
                                                            currentNewRow3.GetType().GetProperty("PID").SetValue(currentNewRow3, currentNewRow2.GetType().GetProperty("ID").GetValue(currentNewRow2, null), null);

                                                        }
                                                    }
                                                }
                                            }
                                            currentNewRow2.GetType().GetProperty("PID").SetValue(currentNewRow2, findCurrentRow.GetType().GetProperty("ID").GetValue(findCurrentRow, null), null);

                                        }

                                        foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                                        {
                                            if (currentNewRow2.GetType().GetProperty(deliveryColumn) != null)
                                            {
                                                currentNewRow2.GetType().GetProperty(deliveryColumn).SetValue(currentNewRow2, current.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                                            }
                                        }

                                    }
                                }
                            }


                            DB.Entry(findCurrentRow).State = EntityState.Modified;
                            //}
                        }
                    }

                    foreach (var currentNewRow in currentChildTable as IEnumerable<object>)
                    {
                        int id = Convert.ToInt32(currentNewRow.GetType().GetProperty("ID").GetValue(currentNewRow, null));
                        if (id == 0)
                        {

                            DB.Entry(currentNewRow).State = EntityState.Added;

                            foreach (string tableName2 in includeTableLevel2s)
                            {
                                if (currentNewRow.GetType().GetProperty(tableName2) != null)
                                {
                                    foreach (var currentNewRow2 in currentNewRow.GetType().GetProperty(tableName2).GetValue(currentNewRow, null) as IEnumerable<object>)
                                    {
                                        int id2 = Convert.ToInt32(currentNewRow2.GetType().GetProperty("ID").GetValue(currentNewRow2, null));
                                        if (id2 == 0)
                                        {
                                            DB.Entry(currentNewRow2).State = EntityState.Added;

                                            foreach (string tableName3 in includeTableLevel3s)
                                            {
                                                if (currentNewRow2.GetType().GetProperty(tableName3) != null)
                                                {
                                                    foreach (var currentNewRow3 in currentNewRow2.GetType().GetProperty(tableName3).GetValue(currentNewRow2, null) as IEnumerable<object>)
                                                    {
                                                        int id3 = Convert.ToInt32(currentNewRow3.GetType().GetProperty("ID").GetValue(currentNewRow3, null));
                                                        if (id3 == 0)
                                                        {
                                                            DB.Entry(currentNewRow3).State = EntityState.Added;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }

                            currentNewRow.GetType().GetProperty("PID").SetValue(currentNewRow, current.GetType().GetProperty("ID").GetValue(current, null), null);

                        }

                        foreach (string deliveryColumn in this.DataObjectInfo.DeliveryColumns)
                        {
                            if (currentNewRow.GetType().GetProperty(deliveryColumn) != null)
                            {
                                currentNewRow.GetType().GetProperty(deliveryColumn).SetValue(currentNewRow, current.GetType().GetProperty(deliveryColumn).GetValue(current, null), null);
                            }
                        }



                    }
                }

                if (originalId != 0)
                {

                    //if (!Helper.ObjectComparator(current, original))
                    //{
                    current.GetType().GetProperty("MDY_USR_NO").SetValue(current, System.Web.HttpContext.Current.User.Identity.Name, null);
                    current.GetType().GetProperty("MDY_DT").SetValue(current, DateTime.Now, null);
                    current.GetType().GetProperty("IP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostAddress, null);
                    current.GetType().GetProperty("CP_NM").SetValue(current, System.Web.HttpContext.Current.Request.UserHostName, null);

                    DB.Entry(current).State = EntityState.Modified;
                    //}                    
                }

                int index = 1;
                foreach (var upload in uploads)
                {
                    current.GetType().GetProperty("FileName" + index.ToString("00")).SetValue(current, upload.FileName, null);
                    current.GetType().GetProperty("FileGUID" + index.ToString("00")).SetValue(current, upload.FileGUID, null);
                }

                if (current.GetType().GetProperty("Password") != null)
                {
                    if (current.GetType().GetProperty("Password1") != null)
                    {
                        object password1 = current.GetType().GetProperty("Password1").GetValue(current, null);
                        if (password1 != null && !string.IsNullOrWhiteSpace(password1.ToString()))
                        {
                            current.GetType().GetProperty("Password").SetValue(current, SSoft.Web.Security.md5Class.StringToMD5Hash(password1.ToString().Trim()), null);
                        }
                    }
                }



                if (this.USaveAfterDBChangeBeforeEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveAfterDBChangeBeforeEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                DB.SaveChanges();

                if (this.USaveAfterDBChangeAfterEvent != null)
                {
                    SSoft.MVC.Events.SaveEntityEventArgs saveEntityEventArgs = new SSoft.MVC.Events.SaveEntityEventArgs(false, value, current, original, this.DB);
                    USaveAfterDBChangeAfterEvent(this, saveEntityEventArgs);
                    if (saveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(saveEntityEventArgs.Message);
                    }
                }

                return POSTRetrieveDataByKey(Convert.ToInt32(current.GetType().GetProperty("ID").GetValue(current, null)));



            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "")),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }

        public object FindRow(object rows, int findId)
        {
            foreach (var row in (rows as IEnumerable<object>))
            {
                int id = Convert.ToInt32(row.GetType().GetProperty("ID").GetValue(row, null));
                if (id == findId)
                    return row;
            }

            return null;
        }

        public dynamic POSTRetrieveDataByKey(int Id)
        {
            try
            {
                if (this.URetrieveByKeyBeforeEvent != null)
                {
                    SSoft.MVC.Events.RetrieveEntityEventArgs retrieveEntityEventArgs = new SSoft.MVC.Events.RetrieveEntityEventArgs(false, Id, Id, this.DB);
                    URetrieveByKeyBeforeEvent(this, retrieveEntityEventArgs);
                    if (retrieveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(retrieveEntityEventArgs.Message);
                    }
                }

                object entity = DB.GetType().GetProperty(this.DataObjectInfo.InputModelObjectType.Name).GetValue(DB, null);

                DB.Configuration.ProxyCreationEnabled = false;

                foreach (string tableName in this.DataObjectInfo.IncludeTables)
                {
                    entity = (entity as IQueryable).Include(tableName);
                }

                if (this.URetrieveByKeyAfterEvent != null)
                {
                    SSoft.MVC.Events.RetrieveEntityEventArgs retrieveEntityEventArgs = new SSoft.MVC.Events.RetrieveEntityEventArgs(false, Id, Id, entity);
                    URetrieveByKeyAfterEvent(this, retrieveEntityEventArgs);
                    if (retrieveEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(retrieveEntityEventArgs.Message);
                    }
                }

                string jsonStirng = JsonConvert.SerializeObject((entity as IQueryable).Where(this.DataObjectInfo.KeyName+"=@0", Id), Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });

                return (entity as IQueryable).Where(this.DataObjectInfo.KeyName + "=@0", Id);
                //var response = this.Request.CreateResponse();
                //response.Content = new StringContent(jsonStirng);
                //response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
                //return response;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "")),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }

        }

        public virtual dynamic POSTRetrieveDataByCondition(object value)
        {
            //if(this.DataObjectInfo.InputModelObjectType == null)
            //{
            //    var response1 = this.Request.CreateResponse();
            //    response1.Content = new StringContent("");
            //    response1.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            //    return response1;
            //}
            dynamic jsonDynamic = POSTRetrieveDataByConditionString(value, this.DataObjectInfo.InputModelObjectType, this.DataObjectInfo.ConditionModelObjectType, this.DataObjectInfo.QueryOutputModelObjectType);

            string jsonStirng = JsonConvert.SerializeObject(jsonDynamic, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });

            var response = this.Request.CreateResponse();
            response.Content = new StringContent(jsonStirng);
            response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;
        }


        public dynamic POSTRetrieveDataByConditionString(object value, Type inputModelObjectType, Type conditionModelObjectType, Type queryOutputModelObjectType)
        {
            try
            {

                dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
                   new JsonSerializerSettings()
                   {
                       ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                       DateTimeZoneHandling = DateTimeZoneHandling.Local
                   });

                dynamic condition = entity["condition"];
                dynamic advanceConditionData = entity["condition"]["AdvanceConditionData"];
                dynamic advanceConditionDataSort = entity["condition"]["AdvanceConditionDataSort"];

                List<SSoft.MVC.ConditionParameter> conditionParameters = new List<ConditionParameter>();



                if (this.URetrieveByConditonBeforeEvent != null)
                {
                    SSoft.MVC.Events.ConditonQueryEntityEventArgs conditonEntityEventArgs = new SSoft.MVC.Events.ConditonQueryEntityEventArgs(false, value, condition, conditionParameters, this.DB);
                    URetrieveByConditonBeforeEvent(this, conditonEntityEventArgs);
                    if (conditonEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(conditonEntityEventArgs.Message);
                    }
                }

                object entityCondition = null;
                if (conditionModelObjectType != null)
                {
                    entityCondition = Newtonsoft.Json.JsonConvert.DeserializeObject(condition.ToString(), conditionModelObjectType,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });
                }

                DB.Configuration.ProxyCreationEnabled = false;

                IQueryable entityOutput = null;
                string entityOutputMainTable = "";

                bool conditionIsIQueryString = entityCondition == null ? false : entityCondition is SSoft.MVC.Interfaces.Model.IQueryString;
                bool isAdvanceCondition = conditionModelObjectType == null ? false : conditionModelObjectType.GetInterfaces().Contains(typeof(SSoft.MVC.Interfaces.Model.IAdvanceCondition));

                if (!conditionIsIQueryString)
                {

                    if (queryOutputModelObjectType != null)
                    {
                        entityOutput = DB.GetType().GetProperty(queryOutputModelObjectType.Name).GetValue(DB, null) as IQueryable;
                    }
                    else
                    {
                        entityOutput = DB.GetType().GetProperty(inputModelObjectType.Name).GetValue(DB, null) as IQueryable;
                    }
                }



                if (entityCondition != null)
                {
                    Type typeCondition = entityCondition.GetType();
                    if (!conditionIsIQueryString)
                    {

                        if (queryOutputModelObjectType != null)
                        {
                            entityOutput = DB.GetType().GetProperty(queryOutputModelObjectType.Name).GetValue(DB, null) as IQueryable;
                        }
                        else
                        {
                            entityOutput = DB.GetType().GetProperty(inputModelObjectType.Name).GetValue(DB, null) as IQueryable;
                        }
                    }
                    else
                    {

                        var attrs = typeCondition.GetCustomAttributes(typeof(SSoft.Data.Attributes.ConditionModelClassAttribute), false);
                        if (attrs.Length > 0)
                        {
                            entityOutputMainTable = (attrs[0] as SSoft.Data.Attributes.ConditionModelClassAttribute).DatabaseMainTableName;
                        }
                    }


                    var properties = typeCondition.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

                    foreach (System.Reflection.PropertyInfo pi in typeCondition.GetProperties(System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance))
                    {

                        if (pi.Name != "SelectString" && pi.Name != "WhereString" && pi.Name != "OrderString" && pi.Name != "QueryOutputModelObjectType" && pi.Name != "AdvanceConditionModelObjectType")
                        {
                            var property = typeCondition.GetProperty(pi.Name);
                            var propertyType = property.PropertyType;

                            if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                propertyType = propertyType.GetGenericArguments()[0];
                            }

                            object propertyValue = typeCondition.GetProperty(pi.Name).GetValue(entityCondition, null);

                            if (propertyValue != null && !string.IsNullOrWhiteSpace(propertyValue.ToString()))
                            {
                                SSoft.MVC.ConditionParameter conditionParameter = new ConditionParameter();

                                string databaseColumnName = "";
                                string classPropertyFieldName = pi.Name;


                                var attrs = pi.GetCustomAttributes(typeof(SSoft.Data.Attributes.ConditionModelAttribute), false);
                                if (attrs.Length > 0)
                                {
                                    if (!string.IsNullOrWhiteSpace((attrs[0] as SSoft.Data.Attributes.ConditionModelAttribute).DatabaseColumnName))
                                    {
                                        databaseColumnName = (attrs[0] as SSoft.Data.Attributes.ConditionModelAttribute).DatabaseColumnName;
                                    }
                                }
                                else
                                {

                                    if (classPropertyFieldName.EndsWith("__S") || classPropertyFieldName.EndsWith("_E"))
                                    {
                                        databaseColumnName = pi.Name.Substring(0, pi.Name.Length - 3);
                                    }
                                    else
                                    {
                                        databaseColumnName = pi.Name;
                                    }
                                    databaseColumnName = (string.IsNullOrWhiteSpace(entityOutputMainTable) ? "" : entityOutputMainTable.Trim() + ".") + databaseColumnName;
                                }

                                if (classPropertyFieldName.EndsWith("__S"))
                                {
                                    var find = conditionParameters.Where(t => t.DatabaseColumnName == databaseColumnName);
                                    if (find.Count() > 0)
                                    {
                                        find.First().ClassPropertyFieldName = classPropertyFieldName;
                                        find.First().Value = propertyValue;
                                        //find.First().Value1 = propertyValue;
                                        find.First().ValueType = propertyType.Name;
                                        find.First().IsBetween = true;
                                        continue;
                                    }
                                    conditionParameter.DatabaseColumnName = databaseColumnName;
                                    conditionParameter.ClassPropertyFieldName = classPropertyFieldName;
                                    conditionParameter.Value = propertyValue;
                                    conditionParameter.ValueType = propertyType.Name;
                                    conditionParameter.IsBetween = true;
                                }
                                else if (classPropertyFieldName.EndsWith("__E"))
                                {
                                    var find = conditionParameters.Where(t => t.DatabaseColumnName == databaseColumnName);
                                    if (find.Count() > 0)
                                    {
                                        find.First().ClassPropertyFieldName1 = classPropertyFieldName;
                                        find.First().Value1 = propertyValue;
                                        find.First().ValueType = propertyType.Name;
                                        find.First().IsBetween = true;
                                        continue;
                                    }
                                    conditionParameter.DatabaseColumnName = databaseColumnName;
                                    conditionParameter.ClassPropertyFieldName1 = classPropertyFieldName;
                                    conditionParameter.Value1 = propertyValue;
                                    conditionParameter.ValueType = propertyType.Name;
                                    conditionParameter.IsBetween = true;
                                }
                                else
                                {
                                    conditionParameter.DatabaseColumnName = databaseColumnName;
                                    conditionParameter.ClassPropertyFieldName = classPropertyFieldName;
                                    conditionParameter.Value = propertyValue;
                                    conditionParameter.ValueType = propertyType.Name;
                                    conditionParameter.IsBetween = false;
                                }

                                conditionParameter.Operator = SSoft.Enum.EnumOperator.Like;
                                conditionParameters.Add(conditionParameter);
                            }
                        }
                    }
                    if (isAdvanceCondition)
                    {
                        var advanceConditionModelObjectType = (conditionModelObjectType.InvokeMember("AdvanceConditionModelObjectType", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.GetProperty | System.Reflection.BindingFlags.Public, null, Activator.CreateInstance(conditionModelObjectType), new List<object>().ToArray()));
                        var advanceProperties = Type.GetType((advanceConditionModelObjectType as Type).FullName).GetProperties();


                        foreach (dynamic advanItem in advanceConditionData as IEnumerable<object>)
                        {
                            object columnName = advanItem["Display"];
                            object columnValue = advanItem["Value"];
                            object columnOperator = advanItem["Display1"];
                            object columnOperand = advanItem["Display2"];


                            if (columnName != null && columnValue != null && !string.IsNullOrWhiteSpace(columnValue.ToString()))
                            {
                                var findColumnProperties = advanceProperties.Where(t => t.Name == columnName.ToString());
                                if (findColumnProperties.Count() > 0)
                                {
                                    //var property = typeCondition.GetProperty(pi.Name);
                                    var propertyType = findColumnProperties.First().PropertyType;

                                    if (propertyType.IsGenericType && propertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                                    {
                                        propertyType = propertyType.GetGenericArguments()[0];
                                    }
                                    string databaseColumnName = columnName.ToString();
                                    var attrs = findColumnProperties.First().GetCustomAttributes(typeof(SSoft.Data.Attributes.ConditionModelAttribute), false);
                                    if (attrs.Length > 0)
                                    {
                                        if (!string.IsNullOrWhiteSpace((attrs[0] as SSoft.Data.Attributes.ConditionModelAttribute).DatabaseColumnName))
                                        {
                                            databaseColumnName = (attrs[0] as SSoft.Data.Attributes.ConditionModelAttribute).DatabaseColumnName;
                                        }
                                    }


                                    SSoft.MVC.ConditionParameter conditionParameter = new ConditionParameter();

                                    conditionParameter.DatabaseColumnName = databaseColumnName;
                                    conditionParameter.ClassPropertyFieldName = columnName.ToString();
                                    conditionParameter.Value = columnValue.ToString();
                                    conditionParameter.ValueType = propertyType.Name;
                                    conditionParameter.IsBetween = false;
                                    conditionParameter.Operand = columnOperand.ToString();
                                    conditionParameter.IsAdvance = true;
                                    conditionParameter.Operator = (SSoft.Enum.EnumOperator)System.Enum.Parse(typeof(SSoft.Enum.EnumOperator), columnOperator.ToString());

                                    conditionParameters.Add(conditionParameter);
                                }
                            }
                        }
                    }

                    foreach (var conditionParameter in conditionParameters)
                    {
                        if (conditionParameter.Value == null)
                        {
                            conditionParameter.Value = conditionParameter.Value1;//.ToString();
                        }
                        else if (conditionParameter.Value1 == null)
                        {
                            conditionParameter.Value1 = conditionParameter.Value;//.ToString();
                        }

                        if (conditionParameter.Value == null || conditionParameter.Value1 == null)
                        {
                            if (conditionParameter.Value == null)
                                conditionParameter.Value = conditionParameter.Value1;
                            conditionParameter.IsBetween = false;
                        }
                        if (conditionParameter.ValueType == "DateTime")
                        {
                            if (conditionParameter.Value1 == null)
                            {
                                conditionParameter.Value1 = conditionParameter.Value;
                                if (conditionParameter.Value is DateTime)
                                {
                                    conditionParameter.Value1 = conditionParameter.Value;
                                }
                                else
                                {
                                    conditionParameter.Value1 = conditionParameter.Value;
                                }
                            }
                            conditionParameter.IsBetween = true;
                        }
                        if (conditionParameter.ValueType == "Boolean")
                        {
                            conditionParameter.IsBetween = false;
                        }
                    }

                    int paramCount = 0;
                    foreach (var conditionParameter in conditionParameters)
                    {


                        switch (conditionParameter.ValueType)
                        {
                            case "Int32":
                            case "Decimal":
                            case "DateTime":
                            case "Boolean":
                            case "String":

                                if (conditionParameter.IsBetween)
                                {


                                    if (conditionParameter.ValueType == "String")
                                    {
                                        if (string.Compare(conditionParameter.Value.ToString(), conditionParameter.Value1.ToString(), true) > 0)
                                        {
                                            SSoft.Utility.Swap(ref conditionParameter.Value, ref conditionParameter.Value1);
                                        }
                                    }
                                    else if (conditionParameter.ValueType == "DateTime")
                                    {
                                        if (((conditionParameter.Value is DateTime?) ? (conditionParameter.Value as DateTime?).Value : Convert.ToDateTime(conditionParameter.Value)) > ((conditionParameter.Value1 is DateTime?) ? (conditionParameter.Value1 as DateTime?).Value : Convert.ToDateTime(conditionParameter.Value1)))
                                        {
                                            SSoft.Utility.Swap(ref conditionParameter.Value, ref conditionParameter.Value1);
                                        }
                                    }
                                    else if (conditionParameter.ValueType == "Int32")
                                    {
                                        if (((conditionParameter.Value as int?).HasValue ? (conditionParameter.Value as int?).Value : 0) > ((conditionParameter.Value1 as int?).HasValue ? (conditionParameter.Value1 as int?).Value : Int32.MaxValue))
                                        {
                                            SSoft.Utility.Swap(ref conditionParameter.Value, ref conditionParameter.Value1);
                                        }
                                    }
                                    else if (conditionParameter.ValueType == "Decimal")
                                    {
                                        if (((conditionParameter.Value as decimal?).HasValue ? (conditionParameter.Value as decimal?).Value : 0m) > ((conditionParameter.Value1 as decimal?).HasValue ? (conditionParameter.Value1 as decimal?).Value : decimal.MaxValue))
                                        {
                                            SSoft.Utility.Swap(ref conditionParameter.Value, ref conditionParameter.Value1);
                                        }
                                    }


                                    if (conditionParameter.ValueType == "DateTime")
                                    {
                                        if (conditionParameter.Value1 is DateTime)
                                        {
                                            conditionParameter.Value1 = ((DateTime)(conditionParameter.Value1)).AddDays(1).AddSeconds(-1);
                                        }
                                        else
                                        {
                                            conditionParameter.Value1 = ((DateTime?)(conditionParameter.Value1)).Value.AddDays(1).AddSeconds(-1);
                                        }
                                    }

                                    if (conditionIsIQueryString)
                                    {
                                        conditionParameter.Expression = "( " + conditionParameter.DatabaseColumnName + " between  {" + paramCount++ + "} and {" + paramCount++ + "} )";

                                        //entityOutput = entityOutput.Where(columnName + " >= @0", (propertyValue as DateTime?).Value);
                                        //entityOutput = entityOutput.Where(columnName + " == DateTime.Parse(\"" + (propertyValue as DateTime?).Value.ToString("yyyy/MM/dd") + "\")");
                                        //"TestDate == DateTime.Parse(\"" + Convert.ToDateTime(txtDate.Text).Date +"\")" 

                                    }
                                    else
                                    {
                                        if (conditionParameter.ValueType == "String")
                                        {
                                            conditionParameter.Expression = String.Format(" ( " + conditionParameter.DatabaseColumnName + " >= \"{0}\" and " + conditionParameter.DatabaseColumnName + " <=\"{1}\" ) ", conditionParameter.Value.ToString(), conditionParameter.Value1.ToString());
                                        }
                                        else if (conditionParameter.ValueType == "DateTime")
                                        {
                                            DateTime dt_s = (conditionParameter.Value as DateTime?).Value;
                                            DateTime dt_e = (conditionParameter.Value1 as DateTime?).Value;
                                            conditionParameter.Expression = String.Format(" ( " + conditionParameter.DatabaseColumnName + " >= DateTime({0},{1},{2}) and " + conditionParameter.DatabaseColumnName + " <=DateTime({3},{4},{5}) ) ", dt_s.Year, dt_s.Month, dt_s.Day, dt_e.Year, dt_e.Month, dt_e.Day);
                                        }
                                        else
                                        {
                                            conditionParameter.Expression = String.Format(" ( " + conditionParameter.DatabaseColumnName + " >= {0} and " + conditionParameter.DatabaseColumnName + " <={1} ) ", conditionParameter.Value.ToString(), conditionParameter.Value1.ToString());
                                        }
                                    }
                                }
                                else
                                {
                                    if (conditionIsIQueryString)
                                    {
                                        if (!conditionParameter.IsAdvance)
                                        {
                                            if (conditionParameter.ValueType == "String")
                                            {

                                                conditionParameter.Expression = "( " + conditionParameter.DatabaseColumnName + " like {" + paramCount++ + "} )";
                                                conditionParameter.Value = "%" + conditionParameter.Value + "%";


                                            }
                                            else
                                            {
                                                conditionParameter.Expression = "( " + conditionParameter.DatabaseColumnName + " = {" + paramCount++ + "} )";
                                            }
                                        }
                                        else
                                        {
                                            conditionParameter.Expression = "( " + conditionParameter.DatabaseColumnName + " " + conditionParameter.Operator.GetString() + " {" + paramCount++ + "} )";
                                            if (conditionParameter.Operator == EnumOperator.Like)
                                            {
                                                conditionParameter.Value = "%" + conditionParameter.Value + "%";
                                            }

                                        }


                                    }
                                    else
                                    {
                                        if (!conditionParameter.IsAdvance)
                                        {
                                            if (conditionParameter.ValueType == "String")
                                            {
                                                conditionParameter.Expression = String.Format("( {0}.ToLower().Contains(\"{1}\") )", conditionParameter.DatabaseColumnName, conditionParameter.Value.ToString().ToLower());
                                            }
                                            else
                                            {
                                                conditionParameter.Expression = String.Format("( {0}.ToLower().Equal(\"{1}\") )", conditionParameter.DatabaseColumnName, conditionParameter.Value.ToString().ToLower());
                                            }
                                        }
                                        else
                                        {
                                            conditionParameter.Expression = String.Format("( {0} )", conditionParameter.Operator.GetString(conditionParameter.DatabaseColumnName, conditionParameter.Value.ToString().ToLower(), conditionParameter.ValueType));
                                        }
                                    }
                                }

                                break;

                                //conditionParameter.Expression = String.Format("( {0}=={1} )", field, propertyValue.ToString().ToLower());
                                //break;

                                //case "DateTime":
                                //    if (columnName.EndsWith("_S"))
                                //    {
                                //        columnName = columnName.Substring(0, columnName.Length - 2);
                                //        entityOutput = entityOutput.Where(columnName + " >= @0", (propertyValue as DateTime?).Value);
                                //        //entityOutput = entityOutput.Where(columnName + " == DateTime.Parse(\"" + (propertyValue as DateTime?).Value.ToString("yyyy/MM/dd") + "\")");
                                //        //"TestDate == DateTime.Parse(\"" + Convert.ToDateTime(txtDate.Text).Date +"\")" 
                                //    }
                                //    else if (columnName.EndsWith("_E"))
                                //    {
                                //        columnName = columnName.Substring(0, columnName.Length - 2);
                                //        entityOutput = entityOutput.Where(columnName + " <= @0", (propertyValue as DateTime?).Value);
                                //        //entityOutput = entityOutput.Where(columnName + " == DateTime.Parse(\"" + (propertyValue as DateTime?).Value.ToString("yyyy/MM/dd") + "\")");
                                //        //"TestDate == DateTime.Parse(\"" + Convert.ToDateTime(txtDate.Text).Date +"\")" 
                                //    }
                                //    else
                                //    {
                                //        entityOutput = entityOutput.Where(columnName + " == @0", (propertyValue as DateTime?).Value);
                                //    }
                                //    break;
                        }
                    }
                }
                List<SSoft.MVC.SortParemeter> sortParemeters = new List<SortParemeter>();
                List<string> sortParemeterStrings = new List<string>();
                if (isAdvanceCondition)
                {
                    List<SSoft.MVC.DisplayValue> advanceConditionCollect = Newtonsoft.Json.JsonConvert.DeserializeObject(advanceConditionDataSort.ToString(), typeof(List<SSoft.MVC.DisplayValue>),
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });

                    foreach (DisplayValue advanItem in advanceConditionCollect.Where(t => t.BooleanValue == true).OrderBy(t => t.intValue))
                    {
                        SSoft.MVC.SortParemeter sortParemeter = new SortParemeter();
                        sortParemeter.DatabaseColumnName = advanItem.Value;
                        sortParemeter.ASCorDESC = advanItem.Display1;
                        sortParemeter.Expression = advanItem.Value + " " + advanItem.Display1;
                        sortParemeters.Add(sortParemeter);
                        sortParemeterStrings.Add(advanItem.Value + " " + advanItem.Display1);
                    }
                }

                if (conditionIsIQueryString)
                {
                    SSoft.MVC.Interfaces.Model.IQueryString iQueryString = entityCondition as SSoft.MVC.Interfaces.Model.IQueryString;
                    List<object> ps = new List<object>();
                    List<string> expressionList = new List<string>();
                    string expression = iQueryString.WhereString;// + (string.IsNullOrWhiteSpace(expression) ? "" : (string.IsNullOrWhiteSpace(iQueryString.WhereString) ? " where " + expression : " and " + expression))
                    foreach (var conditionParameter in conditionParameters)
                    {
                        expressionList.Add(conditionParameter.Expression);
                        ps.Add(conditionParameter.Value);
                        if (conditionParameter.IsBetween)
                            ps.Add(conditionParameter.Value1);
                        expression = (string.IsNullOrWhiteSpace(expression) ? " where " : expression + " " + (string.IsNullOrWhiteSpace(conditionParameter.Operand) ? " and " : conditionParameter.Operand) + " ") + conditionParameter.Expression;
                    }

                    string orderExpression = iQueryString.OrderString;
                    if (sortParemeterStrings.Count > 0)
                    {
                        //string orderExpression =( string.IsNullOrWhiteSpace(iQueryString.OrderString) ? " order by " : iQueryString.OrderString.Trim() + ",") + string.Join(",", sortParemeterStrings.ToArray());
                        orderExpression = " order by " + string.Join(",", sortParemeterStrings.ToArray());
                    }
                    //string expression = string.Join(" and ", expressionList.ToArray());
                    YMIR.Models.DBContext.IRF_DBContextDataContext DBContext = new Models.DBContext.IRF_DBContextDataContext();
                    entityOutput = DBContext.ExecuteQuery(iQueryString.QueryOutputModelObjectType, iQueryString.SelectString + " " + expression + " " + orderExpression, ps.ToArray()).AsQueryable().Take(20000);
                    //entityOutput.Load();
                    //List<YMIR.Models.Condition.SAT.SAT0041OutputModel> cddsv = entityOutput.OfType<YMIR.Models.Condition.SAT.SAT0041OutputModel>().ToList();
                }
                else
                {
                    List<string> expressionList = new List<string>();
                    string expression = "";// string.Join(" && ", expressionList.ToArray());
                    foreach (var cond in conditionParameters)
                    {
                        expressionList.Add(cond.Expression);
                        expression = (string.IsNullOrWhiteSpace(expression) ? " " : expression + " " + (string.IsNullOrWhiteSpace(cond.Operand) ? " && " : (cond.Operand.ToLower() == "and" ? "&&" : "||")) + " ") + cond.Expression;
                    }

                    string orderExpression = string.Join(",", sortParemeterStrings.ToArray());


                    if (!string.IsNullOrWhiteSpace(expression))
                    {
                        if (string.IsNullOrWhiteSpace(orderExpression))
                        {
                            entityOutput = entityOutput.Where(expression).Take(20000);
                        }
                        else
                        {
                            entityOutput = entityOutput.Where(expression).OrderBy(orderExpression).Take(20000);
                        }
                    }
                    else
                    {
                        entityOutput = entityOutput.Take(20000); ;
                    }
                }


                App_Code.Utils.InsertActionLog(this.ProgramId, 4, "", "0");

                if (this.URetrieveByConditonAfterEvent != null)
                {
                    SSoft.MVC.Events.ConditonQueryEntityEventArgs conditonEntityEventArgs = new SSoft.MVC.Events.ConditonQueryEntityEventArgs(false, value, condition, conditionParameters, this.DB);
                    URetrieveByConditonAfterEvent(this, conditonEntityEventArgs);
                    if (conditonEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(conditonEntityEventArgs.Message);
                    }
                }

                //string jsonStirng = JsonConvert.SerializeObject(entityOutput, Formatting.None,
                //    new JsonSerializerSettings()
                //    {
                //        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                //        DateTimeZoneHandling = DateTimeZoneHandling.Local
                //    });

                return entityOutput;
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "")),
                    ReasonPhrase = ex.Message
                };
                throw new HttpResponseException(resp);
            }
        }

        public dynamic POSTDelete(object value)
        {
            return POSTDeleteModel(value, this.DataObjectInfo.InputModelObjectType, this.DataObjectInfo.KeyName);
        }
        public dynamic POSTDeleteModel(object value, Type inputModelObjectType, string keyName)
        {
            try
            {
                dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
               new JsonSerializerSettings()
               {
                   ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                   DateTimeZoneHandling = DateTimeZoneHandling.Local
               });

                dynamic modelCurrent = entity["modelCurrent"];
                dynamic modelOriginal = entity["modelOriginal"];

                var current = Newtonsoft.Json.JsonConvert.DeserializeObject(modelCurrent.ToString(), inputModelObjectType,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });
                var original = Newtonsoft.Json.JsonConvert.DeserializeObject(modelOriginal.ToString(), inputModelObjectType,
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

                if (this.UDeleteBeforeEvent != null)
                {
                    SSoft.MVC.Events.DeleteEntityEventArgs deleteEntityEventArgs = new SSoft.MVC.Events.DeleteEntityEventArgs(false, value, current, original, this.DB);
                    UDeleteBeforeEvent(this, deleteEntityEventArgs);
                    if (deleteEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(deleteEntityEventArgs.Message);
                    }
                }

                object originalId = original.GetType().GetProperty(keyName).GetValue(original, null);

                object entitys = DB.GetType().GetProperty(inputModelObjectType.Name).GetValue(DB, null);

                object entityDeletes = (entitys as IQueryable).Where(keyName + "=@0", originalId);

                foreach (var item in (entityDeletes as IQueryable))
                {
                    DB.Entry(item).State = EntityState.Deleted;
                }

                if (this.UDeleteAfterDBChangeBeforeEvent != null)
                {
                    SSoft.MVC.Events.DeleteEntityEventArgs deleteEntityEventArgs = new SSoft.MVC.Events.DeleteEntityEventArgs(false, value, current, original, this.DB);
                    UDeleteAfterDBChangeBeforeEvent(this, deleteEntityEventArgs);
                    if (deleteEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(deleteEntityEventArgs.Message);
                    }
                }

                DB.SaveChanges();
                App_Code.Utils.InsertActionLog(this.ProgramId, 3, "", current.GetType().GetProperty(this.DataObjectInfo.NoColumnName).GetValue(current, null).ToString());
                if (this.UDeleteAfterDBChangeAfterEvent != null)
                {
                    SSoft.MVC.Events.DeleteEntityEventArgs deleteEntityEventArgs = new SSoft.MVC.Events.DeleteEntityEventArgs(false, value, current, original, this.DB);
                    UDeleteAfterDBChangeAfterEvent(this, deleteEntityEventArgs);
                    if (deleteEntityEventArgs.Cancel == true)
                    {
                        throw new Exception(deleteEntityEventArgs.Message);
                    }
                }
                //return App_Code.Utils.ReturnJsonMessageAndData("", "");
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, "刪除成功");
            }
            catch (Exception ex)
            {
                //var resp = new HttpResponseMessage(HttpStatusCode.Forbidden)
                //{
                //    Content = new StringContent(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : "")),
                //    ReasonPhrase = ex.Message
                //};
                //throw new HttpResponseException(resp);

                //return App_Code.Utils.ReturnJsonMessageAndData(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : ""), "", true);
                return App_Code.Utils.ReturnResponseJsonMessageAndData(this.Request, ex, true);
            }
        }
        //FileUpload前
        public event EventHandler<SSoft.MVC.Events.ExportExcelEventArgs> UExportExcelAfterEvent;
        //FileUpload前
        public event EventHandler<SSoft.MVC.Events.ExportExcelEventArgs> UExportExcelEvent;
        //FileUpload前
        public event EventHandler<SSoft.MVC.Events.FileUploadEventArgs> UFileUploadBeforeEvent;
        //FileUpload後
        public event EventHandler<SSoft.MVC.Events.FileUploadEventArgs> UFileUploadAfterEvent;
        //存檔前
        public event EventHandler<SSoft.MVC.Events.SaveEntityEventArgs> USaveBeforeEvent;
        //存檔後Commit前
        public event EventHandler<SSoft.MVC.Events.SaveEntityEventArgs> USaveAfterDBChangeBeforeEvent;
        //存檔後Commit後
        public event EventHandler<SSoft.MVC.Events.SaveEntityEventArgs> USaveAfterDBChangeAfterEvent;
        //刪除前
        public event EventHandler<SSoft.MVC.Events.DeleteEntityEventArgs> UDeleteBeforeEvent;
        //刪除後Commit前
        public event EventHandler<SSoft.MVC.Events.DeleteEntityEventArgs> UDeleteAfterDBChangeBeforeEvent;
        //刪除後Commit後
        public event EventHandler<SSoft.MVC.Events.DeleteEntityEventArgs> UDeleteAfterDBChangeAfterEvent;
        //條件查詢前
        public event EventHandler<SSoft.MVC.Events.ConditonQueryEntityEventArgs> URetrieveByConditonBeforeEvent;
        //條件查詢後
        public event EventHandler<SSoft.MVC.Events.ConditonQueryEntityEventArgs> URetrieveByConditonAfterEvent;
        //RetrieveByKey前
        public event EventHandler<SSoft.MVC.Events.RetrieveEntityEventArgs> URetrieveByKeyBeforeEvent;
        //RetrieveByKey後
        public event EventHandler<SSoft.MVC.Events.RetrieveEntityEventArgs> URetrieveByKeyAfterEvent;

        SSoft.MVC.Interfaces.DataObjectInfo _dataObjectInfo;
        public SSoft.MVC.Interfaces.DataObjectInfo DataObjectInfo
        {
            get
            {
                return _dataObjectInfo;
            }
            set
            {
                _dataObjectInfo = value;
            }
        }


        public async Task<HttpResponseMessage> Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            var provider = GetMultipartProvider();
            var result = await Request.Content.ReadAsMultipartAsync(provider);

            //// On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
            //// so this is how you can get the original file name
            //var originalFileName = GetDeserializedFileName(result.FileData.First());

            //// uploadedFileInfo object will give you some additional stuff like file length,
            //// creation time, directory name, a few filesystem methods etc..
            //var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);

            List<YMIR.Models.Sys.UploadDataModel> uploads = new List<Models.Sys.UploadDataModel>();
            foreach (MultipartFileData file in result.FileData)
            {
                var uploadedFileInfo = new FileInfo(file.LocalFileName);
                string originalFileName = GetDeserializedFileName(file);
                string guid = Guid.NewGuid().ToString();
                if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid)))
                {
                    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid));
                }
                var path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid), originalFileName);
                uploadedFileInfo.CopyTo(path, true);

                uploads.Add(new Models.Sys.UploadDataModel() { FileName = originalFileName, FileGUID = guid });

                List<SqlParameter> p = new List<SqlParameter>();

                p.Add(new SqlParameter("@FileName", originalFileName));
                p.Add(new SqlParameter("@FileLength", uploadedFileInfo.Length));
                p.Add(new SqlParameter("@FileType", System.IO.Path.GetExtension(originalFileName)));
                p.Add(new SqlParameter("@FileExtName", System.IO.Path.GetExtension(originalFileName)));
                p.Add(new SqlParameter("@AddUserNo", HttpContext.Current.User.Identity.Name));
                p.Add(new SqlParameter("@AddDate", DateTime.Now));
                p.Add(new SqlParameter("@IPAddress", HttpContext.Current.Request.UserHostAddress));
                p.Add(new SqlParameter("@GUID", guid));
                p.Add(new SqlParameter("@Path", path));
                p.Add(new SqlParameter("@FullPath", uploadedFileInfo.DirectoryName));
                p.Add(new SqlParameter("@CreateTime", uploadedFileInfo.CreationTime));
                p.Add(new SqlParameter("@LastWriteTime", uploadedFileInfo.LastWriteTime));

                SSoft.Data.SqlHelper.SelectNonQuery("Insert into SYSBLOB(FileName,FileLength,FileType,FileExtName,AddUserNo,AddDate,IPAddress,GUID,Path,FullPath,CreateTime,LastWriteTime) Values(@FileName,@FileLength,@FileType,@FileExtName,@AddUserNo,@AddDate,@IPAddress,@GUID,@Path,@FullPath,@CreateTime,@LastWriteTime)", p.ToArray());

            }
            //file.SaveAs(path);
            //data_member.PicGuid = guid;
            //data_member.PicFile = System.IO.Path.GetFileName(file.FileName);


            // Remove this line as well as GetFormData method if you're not 
            // sending any form data with your upload request
            //var fileUploadObj = GetFormData<YMIR.Models.BAKAgreement>(result);

            // fileUploadObj = GetFormData<object>(result);
            //dynamic modelCurrent = GetFormData<object>(result,0);
            //dynamic modelOriginal = GetFormData<object>(result,1);
            //return POSTSave1(result, modelCurrent, modelOriginal, uploads);
            return POSTSave1(GetFormData<object>(result, 0), uploads);
            // Through the request response you can return an object to the Angular controller
            // You will be able to access this in the .success callback through its data attribute
            // If you want to send something to the .error callback, use the HttpStatusCode.BadRequest instead
            //var returnData = "ReturnTest";
            //return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
        }

        // You could extract these two private methods to a separate utility class since
        // they do not really belong to a controller class but that is up to you
        private MultipartFormDataStreamProvider GetMultipartProvider()
        {
            var uploadFolder = "~/App_Data/Tmp"; // you could put this to web.config
            var root = HttpContext.Current.Server.MapPath(uploadFolder);
            Directory.CreateDirectory(root);
            return new MultipartFormDataStreamProvider(root);
        }

        // Extracts Request FormatData as a strongly typed model
        private object GetFormData<T>(MultipartFormDataStreamProvider result, int index)
        {
            if (result.FormData.HasKeys())
            {
                var unescapedFormData = Uri.UnescapeDataString(result.FormData.GetValues(index).FirstOrDefault() ?? String.Empty);
                if (!String.IsNullOrEmpty(unescapedFormData))
                    return JsonConvert.DeserializeObject<T>(unescapedFormData);
            }

            return null;
        }

        private string GetDeserializedFileName(MultipartFileData fileData)
        {
            var fileName = GetFileName(fileData);
            return JsonConvert.DeserializeObject(fileName).ToString();
        }

        public string GetFileName(MultipartFileData fileData)
        {
            return fileData.Headers.ContentDisposition.FileName;
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage POSTDownloadFile(string id)
        {
            DataTable dt = SSoft.Data.SqlHelper.SelectTable("select FileName from SYSBLOB where guid = @guid", new SqlParameter[] { new SqlParameter("@guid", id) });
            if (dt.Rows.Count == 0)
                return null;

            string fileName = dt.Rows[0][0].ToString();

            var uploadFolder = System.Web.Configuration.WebConfigurationManager.AppSettings["UserFileUploadsDirectory"] + "\\" + id + "\\" + fileName;
            var path = HttpContext.Current.Server.MapPath(uploadFolder);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = System.Web.HttpContext.Current.Server.UrlEncode(fileName) };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }

        [System.Web.Http.AllowAnonymous]
        [System.Web.Http.HttpGet]
        public HttpResponseMessage POSTDownloadFileName(string id)
        {
            Guid guidOutput = new Guid();
            bool isValid = Guid.TryParse(id, out guidOutput);
            if (isValid)
                return POSTDownloadFile(id);

            var uploadFolder = Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["FilePath"], id);
            var path = uploadFolder;// HttpContext.Current.Server.MapPath(uploadFolder);
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            var stream = new FileStream(path, FileMode.Open);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment") { FileName = System.Web.HttpContext.Current.Server.UrlEncode(id) };
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            return result;
        }


        public async Task<HttpResponseMessage> UploadFile()
        {
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = GetMultipartProvider();
                var result = await Request.Content.ReadAsMultipartAsync(provider);

                //MultipartFormDataStreamProvider
                //// On upload, files are given a generic name like "BodyPart_26d6abe1-3ae1-416a-9429-b35f15e6e5d5"
                //// so this is how you can get the original file name
                //var originalFileName = GetDeserializedFileName(result.FileData.First());

                //// uploadedFileInfo object will give you some additional stuff like file length,
                //// creation time, directory name, a few filesystem methods etc..
                //var uploadedFileInfo = new FileInfo(result.FileData.First().LocalFileName);



                string message = "";
                bool isError = false;
                object data = null;
                List<YMIR.Models.Sys.UploadDataModel> uploads = new List<Models.Sys.UploadDataModel>();
                foreach (MultipartFileData file in result.FileData)
                {
                    var uploadedFileInfo = new FileInfo(file.LocalFileName);
                    string originalFileName = GetDeserializedFileName(file);
                    string guid = Guid.NewGuid().ToString();
                    if (!Directory.Exists(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid)))
                    {
                        Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid));
                    }
                    var path = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + guid), originalFileName);


                    if (this.UFileUploadBeforeEvent != null)
                    {
                        SSoft.MVC.Events.FileUploadEventArgs fileUploadEntityEventArgs = new SSoft.MVC.Events.FileUploadEventArgs(false, originalFileName, path, uploadedFileInfo.Length, guid);
                        fileUploadEntityEventArgs.MultipartFormData = result;
                        UFileUploadBeforeEvent(this, fileUploadEntityEventArgs);
                        if (fileUploadEntityEventArgs.Cancel == true)
                        {
                            message += fileUploadEntityEventArgs.Message;
                            isError = true;
                            break;
                        }
                    }

                    uploadedFileInfo.CopyTo(path, true);

                    uploads.Add(new Models.Sys.UploadDataModel() { FileName = originalFileName, FileGUID = guid, FileURL = SSoft.MVC.Helper.GetHttpDownloadFileURL(guid) });

                    List<SqlParameter> p = new List<SqlParameter>();

                    p.Add(new SqlParameter("@FileName", originalFileName));
                    p.Add(new SqlParameter("@FileLength", uploadedFileInfo.Length));
                    p.Add(new SqlParameter("@FileType", System.IO.Path.GetExtension(originalFileName)));
                    p.Add(new SqlParameter("@FileExtName", System.IO.Path.GetExtension(originalFileName)));
                    p.Add(new SqlParameter("@AddUserNo", HttpContext.Current.User.Identity.Name));
                    p.Add(new SqlParameter("@AddDate", DateTime.Now));
                    p.Add(new SqlParameter("@IPAddress", HttpContext.Current.Request.UserHostAddress));
                    p.Add(new SqlParameter("@GUID", guid));
                    p.Add(new SqlParameter("@Path", path));
                    p.Add(new SqlParameter("@FullPath", uploadedFileInfo.DirectoryName));
                    p.Add(new SqlParameter("@CreateTime", uploadedFileInfo.CreationTime));
                    p.Add(new SqlParameter("@LastWriteTime", uploadedFileInfo.LastWriteTime));

                    SSoft.Data.SqlHelper.SelectNonQuery("Insert into SYSBLOB(FileName,FileLength,FileType,FileExtName,AddUserNo,AddDate,IPAddress,GUID,Path,FullPath,CreateTime,LastWriteTime) Values(@FileName,@FileLength,@FileType,@FileExtName,@AddUserNo,@AddDate,@IPAddress,@GUID,@Path,@FullPath,@CreateTime,@LastWriteTime)", p.ToArray());

                    if (this.UFileUploadAfterEvent != null)
                    {
                        SSoft.MVC.Events.FileUploadEventArgs fileUploadEntityEventArgs = new SSoft.MVC.Events.FileUploadEventArgs(false, originalFileName, path, uploadedFileInfo.Length, guid);
                        fileUploadEntityEventArgs.MultipartFormData = result;
                        UFileUploadAfterEvent(this, fileUploadEntityEventArgs);
                        if (!string.IsNullOrWhiteSpace(fileUploadEntityEventArgs.Message))
                        {
                            message += fileUploadEntityEventArgs.Message;
                        }
                        isError = fileUploadEntityEventArgs.Cancel;
                        data = fileUploadEntityEventArgs.Data;
                    }
                }

                //return new HttpResponseMessage() { Content = new StringContent(YMIR.App_Code.Utils.GetJsonConvertSerializeString(uploads)) };
                return new HttpResponseMessage() { Content = new StringContent(YMIR.App_Code.Utils.ReturnJsonMessageAndData(message, uploads, isError,data)) };
            }
            catch (Exception ex)
            {
                return new HttpResponseMessage() { Content = new StringContent(YMIR.App_Code.Utils.ReturnJsonMessageAndData(ex.Message + (ex.InnerException != null ? "\n" + ex.InnerException.Message + (ex.InnerException.InnerException != null ? "\n" + ex.InnerException.InnerException.Message : "") : ""), "", true)) };
               
            }
        }

        //public dynamic CropImage(string imagePath, int? cropPointX, int? cropPointY, int? imageCropWidth, int? imageCropHeight)   
        public dynamic CropImage(object value)
        {
            dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

            string imagePath = entity["imagePath"].ToString();
            int cropPointX = Convert.ToInt32(entity["cropPointX"]);
            int cropPointY = Convert.ToInt32(entity["cropPointY"]);
            int imageCropWidth = Convert.ToInt32(entity["imageCropWidth"]);
            int imageCropHeight = Convert.ToInt32(entity["imageCropHeight"]);

            //if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            //{
            //    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            //}

            DataTable dt = SSoft.Data.SqlHelper.SelectTable("select FileName from SYSBLOB where guid = @guid", new SqlParameter[] { new SqlParameter("@guid", imagePath) });
            if (dt.Rows.Count == 0)
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);

            string originalFileName = dt.Rows[0][0].ToString();

            string filePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + imagePath), originalFileName);
            byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            byte[] croppedImage = Common.ImageHelper.CropImage(imageBytes, cropPointX, cropPointY, imageCropWidth, imageCropHeight);

            string tempFolderName = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["Image.TempFolderName"]);
            string fileName = originalFileName;// Path.GetFileName(imagePath);

            try
            {
                //App_Code.FileHelper.SaveFile(croppedImage, Path.Combine(tempFolderName, fileName));
                Common.FileHelper.SaveFile(croppedImage, filePath);
            }
            catch (Exception ex)
            {
                //Log an error     
                //return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                return new HttpStatusCodeResult((int)HttpStatusCode.OK);
            }

            string photoPath = string.Concat("/", ConfigurationManager.AppSettings["Image.TempFolderName"], "/", fileName);
            //return Json(new { photoPath = photoPath }, JsonRequestBehavior.AllowGet);

            string jsonStirng = JsonConvert.SerializeObject(new { photoPath = photoPath }, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });

            var response = this.Request.CreateResponse();
            response.Content = new StringContent(jsonStirng);
            response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;


        }


        public dynamic WatermarkImage(object value)
        {
            dynamic entity = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(value.ToString(),
                new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    DateTimeZoneHandling = DateTimeZoneHandling.Local
                });

            string imagePath = entity["imagePath"].ToString();
            int cropPointX = Convert.ToInt32(entity["cropPointX"]);
            int cropPointY = Convert.ToInt32(entity["cropPointY"]);
            int imageCropWidth = Convert.ToInt32(entity["imageCropWidth"]);
            int imageCropHeight = Convert.ToInt32(entity["imageCropHeight"]);
            string watermark = entity["watermark"].ToString();
            //if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            //{
            //    return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            //}

            DataTable dt = SSoft.Data.SqlHelper.SelectTable("select FileName from SYSBLOB where guid = @guid", new SqlParameter[] { new SqlParameter("@guid", imagePath) });
            if (dt.Rows.Count == 0)
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);

            string originalFileName = dt.Rows[0][0].ToString();

            string filePath = System.IO.Path.Combine(HttpContext.Current.Server.MapPath("~/UserFileUploads/" + imagePath), originalFileName);
            //byte[] imageBytes = System.IO.File.ReadAllBytes(filePath);
            //byte[] croppedImage = App_Code.ImageHelper.CropImage(imageBytes, cropPointX, cropPointY, imageCropWidth, imageCropHeight);

            //string tempFolderName = System.Web.HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["Image.TempFolderName"]);
            string fileName = originalFileName;// Path.GetFileName(imagePath);

            try
            {
                using (FileStream stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    Bitmap waterMarkImage = new Bitmap(stream);

                    waterMarkImage = (Bitmap)waterMarkImage.Clone();



                    Graphics g = Graphics.FromImage(waterMarkImage);
                    WriteWaterMark(g, cropPointX, cropPointY, new Font("Arial", 12, FontStyle.Bold), watermark);

                    stream.Close();
                    System.IO.File.Delete(filePath);

                    waterMarkImage.Save(filePath);
                    g.Dispose();
                }
                //App_Code.FileHelper.SaveFile(croppedImage, Path.Combine(tempFolderName, fileName));
                //App_Code.FileHelper.SaveFile(croppedImage, filePath);
            }
            catch (Exception ex)
            {
                //Log an error     
                //return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
                return new HttpStatusCodeResult((int)HttpStatusCode.OK);
            }

            string photoPath = string.Concat("/", ConfigurationManager.AppSettings["Image.TempFolderName"], "/", fileName);
            //return Json(new { photoPath = photoPath }, JsonRequestBehavior.AllowGet);

            string jsonStirng = JsonConvert.SerializeObject(new { photoPath = photoPath }, Formatting.None,
                    new JsonSerializerSettings()
                    {
                        ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                        DateTimeZoneHandling = DateTimeZoneHandling.Local
                    });

            var response = this.Request.CreateResponse();
            response.Content = new StringContent(jsonStirng);
            response.Content.Headers.ContentType = new global::System.Net.Http.Headers.MediaTypeHeaderValue("text/html");
            return response;


        }

        // Takes a graphics object, a font object for our text, and the text we want to write.
        // Then writes it to the handle as a watermark
        private void WriteWaterMark(Graphics graphicsHandle, int x, int y, Font ourFont, String text)
        {
            StringFormat strFormatter = new StringFormat();
            SolidBrush transBrush = new SolidBrush(Color.Red);

            // Drawstring the transparent text to the Graphics context at x,y position.
            graphicsHandle.DrawString(text,
                 ourFont,
                 transBrush,
                 new PointF(x, y),
                 strFormatter);

        }

        public void GenExcelGrid(int firstGridRow, dynamic querydata,dynamic entity, HSSFWorkbook workbook, HSSFSheet sheet,dynamic columndef)
        {
            HSSFCellStyle stylecenter = (HSSFCellStyle)workbook.CreateCellStyle();
            stylecenter.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stylecenter.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            font.FontName = "標楷體";
            stylecenter.SetFont(font);
            stylecenter.WrapText = false;

            HSSFCellStyle stylenoboardleft = (HSSFCellStyle)workbook.CreateCellStyle();
            stylenoboardleft.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            stylenoboardleft.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            font.FontName = "標楷體";
            stylenoboardleft.SetFont(font);
            stylenoboardleft.WrapText = false;

            HSSFRow first = (HSSFRow)sheet.CreateRow(firstGridRow);
            first.Height = (short)400;
            int index = 0;
            foreach (var item in columndef)
            {
                string displayName = item["displayName"].ToString(); ;
                if (displayName.Trim() != "")
                {
                    first.CreateCell(index).SetCellValue(displayName);
                    first.Cells[index].CellStyle = stylecenter;
                    index++;
                }
            }


            if (querydata != null && querydata.Count > 0)
            {
                int index1 = firstGridRow;
                foreach (var data in querydata)
                {
                    HSSFRow row = (HSSFRow)sheet.CreateRow(index1 + 1);
                    //for (int i = 0; i < index; i++)
                    //{
                    //    string key = columndef[i]["field"].ToString();
                    //    string keydata = data[key].ToString();
                    //    row.CreateCell(i).SetCellValue(keydata);
                    //    row.Cells[i].CellStyle = stylenoboardleft;
                    //}
                    int index2 = 0;
                    foreach (var item in columndef)
                    {
                        if (item["visible"] == null || Convert.ToBoolean(item["visible"]))
                        {
                            string displayName = item["displayName"].ToString();
                            if (displayName.Trim() != "")
                            {
                                string key = item["field"].ToString();
                                string keydata = Convert.ToString(data[key]);
                                row.CreateCell(index2).SetCellValue(keydata);
                                row.Cells[index2].CellStyle = stylenoboardleft;
                                index2++;
                            }
                        }
                    }
                    index1++;
                }
            }

            
            for (int k = 0; k < index; k++)
            {
                sheet.AutoSizeColumn(k);
                //int width = sheet.GetColumnWidth(k);
                //sheet.SetColumnWidth(k, width + 3 * 256);
            }
        }

        public string POSTGetExcelSampleByValue(dynamic value,string guid)
        {
            dynamic entity = App_Code.Utils.GetJsonConvertDeserializeObject(value);

            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");

            HSSFCellStyle stylecenter = (HSSFCellStyle)workbook.CreateCellStyle();
            stylecenter.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            stylecenter.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            HSSFFont font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;
            font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            font.FontName = "標楷體";
            stylecenter.SetFont(font);
            stylecenter.WrapText = false;

            HSSFCellStyle stylenoboardleft = (HSSFCellStyle)workbook.CreateCellStyle();
            stylenoboardleft.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Left;
            stylenoboardleft.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center;
            font = (HSSFFont)workbook.CreateFont();
            font.FontHeightInPoints = 12;
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Normal;
            font.Color = NPOI.HSSF.Util.HSSFColor.Black.Index;
            font.FontName = "標楷體";
            stylenoboardleft.SetFont(font);
            stylenoboardleft.WrapText = false;

            var querydata = entity["querydata"];
            var columndef = entity["columndef"];
            var entityFileName = entity["filename"];
            int firstGridRow = 0;
            if (this.UExportExcelEvent != null)
            {
                SSoft.MVC.Events.ExportExcelEventArgs exportExcelEventArgs = new SSoft.MVC.Events.ExportExcelEventArgs(false, "", workbook, sheet, entity, 0);
                exportExcelEventArgs.Style = stylenoboardleft;
                UExportExcelEvent(this, exportExcelEventArgs);
                if (exportExcelEventArgs.Cancel == true)
                {
                    sheet = null;
                    workbook = null;
                    return null;
                }

                firstGridRow = exportExcelEventArgs.FirstGridRow;
            }


            GenExcelGrid(firstGridRow, querydata, entity, workbook, sheet, entity["columndef"]);


            if (this.UExportExcelAfterEvent != null)
            {
                SSoft.MVC.Events.ExportExcelEventArgs exportExcelEventArgs = new SSoft.MVC.Events.ExportExcelEventArgs(false, "", workbook, sheet, entity, firstGridRow + (querydata == null ? 0 : querydata.Count));
                exportExcelEventArgs.Style = stylenoboardleft;

                UExportExcelAfterEvent(this, exportExcelEventArgs);

            }




            string filename = entityFileName + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xls";
            
            if (!Directory.Exists(Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UserFileUploadsFolder"], guid.ToString())))
            {
                Directory.CreateDirectory(Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UserFileUploadsFolder"], guid.ToString()));
            }

            FileStream file = new FileStream(Path.Combine(System.Web.Configuration.WebConfigurationManager.AppSettings["UserFileUploadsFolder"], guid.ToString(), filename), FileMode.Create);//產生檔案
            workbook.Write(file);
            file.Close();
            file = null;
            sheet = null;
            workbook = null;

            return filename;
        }
        public dynamic POSTGetExcelSample(dynamic value)
        {
            string guid = Guid.NewGuid().ToString();
            string filename = POSTGetExcelSampleByValue(value, guid);

            string applicationPath = System.Web.HttpContext.Current.Request.ApplicationPath == "/" ? "" : System.Web.HttpContext.Current.Request.ApplicationPath;
            HttpResponseMessage fileresult = new HttpResponseMessage(HttpStatusCode.OK);
            HttpContent icontent = new StringContent(Path.Combine(applicationPath + "/UserFileUploads/" + guid, filename), System.Text.Encoding.UTF8);
            fileresult.Content = icontent;
            return fileresult;
        }
    }
}
