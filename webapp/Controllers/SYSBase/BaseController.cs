using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YMIR.Models;
using Newtonsoft.Json;
using SSoft.MVC;


namespace YMIR.Controllers.SYSBase
{
    public class BaseController : Controller
    {
        // GET: Base
        YMIR.Models.IRFEntities IRF_DB { get; set; }
        public string ProgramNo
        {
            get
            {
                if (this.ViewBag.ProgramNo == null)
                    this.ViewBag.ProgramNo = "";
                return this.ViewBag.ProgramNo;
            }
            set { this.ViewBag.ProgramNo = value; }
        }

        public string ProgramId
        {
            get
            {
                if (this.ViewBag.ProgramId == null)
                    this.ViewBag.ProgramId = "";
                return this.ViewBag.ProgramId;
            }
            set { this.ViewBag.ProgramId = value; }
        }

        public string Title
        {
            get
            {
                if (this.ViewBag.Title == null)
                    this.ViewBag.Title = "";
                return this.ViewBag.Title;
            }
            set { this.ViewBag.Title = value; }
        }
        public string MainMenuId
        {
            get
            {
                if (this.ViewBag.MainMenuId == null)
                    this.ViewBag.MainMenuId = "";
                return this.ViewBag.MainMenuId;
            }
            set { this.ViewBag.MainMenuId = value; }
        }
        public dynamic TabWrapper { get; set; }
        public dynamic DDDWWrapper { get; set; }
        public List<string> ChildTables { get; set; }
        public bool IsAllow { get; set; }
        public List<YMIR.Models.Sys.MenuModel> ProgramList { get; set; }
        public BaseController()
        {
            this.IRF_DB = new Models.IRFEntities();


        }


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);

            string applicationPath = requestContext.HttpContext.Request.ApplicationPath == "/" ? "" : Request.ApplicationPath;


            DataTable dt = SSoft.Data.SqlHelper.SelectTable(@"SELECT * From vw_System_Program", new SqlParameter[] { new SqlParameter("@UserInfo_NO", SSoft.Web.Security.User.Emp_ID) });

            var programList = dt.AsEnumerable().Select(t => new YMIR.Models.Sys.MenuModel
            {
                SystemId = t.Field<Guid>("SYS_SYSTEMNO_Id"),
                SystemName = t.Field<string>("SYSNO_NAME"),
                SystemClass = t.Field<string>("SYS_CLASS"),
                ProgramType = t.Field<string>("PROGRAMNO_NAME"),
                ProgramId = t.Field<Guid>("ID"),
                ProgramNo = t.Field<string>("PRGNO"),
                ProgramName = t.Field<string>("PROGRAMNO_NAME"),
                ProgramPath = t.Field<string>("PROG_URL1"),
                ProgarmOrder = t.Field<int>("PROGRAMNO_ORDERID")
            }).ToList();
            this.ProgramList = programList;
            List<DisplayValue> menu = new List<DisplayValue>();
            var systemGroup = programList.OrderBy(t => t.SystemId).GroupBy(t => new { t.SystemId, t.SystemName, t.SystemClass });
            int index = 0;
            foreach (var g1 in systemGroup)
            {
                DisplayValue sysDisplayValue = new DisplayValue();
                sysDisplayValue.Value = g1.Key.SystemId.ToString();
                sysDisplayValue.Display = g1.Key.SystemName.ToString();
                sysDisplayValue.Display1 = "m_" + g1.Key.SystemId.ToString();
                sysDisplayValue.Display2 = "m_" + g1.Key.SystemId.ToString();
                sysDisplayValue.Display3 = "m_" + g1.Key.SystemId.ToString() + " onlink";
                sysDisplayValue.Display4 = g1.Key.SystemClass.ToString();
                sysDisplayValue.Int01 = index++;
                menu.Add(sysDisplayValue);
                sysDisplayValue.ChildDisplayValues = new List<DisplayValue>();

                var programTypeGroup = programList.Where(t => t.SystemId == g1.Key.SystemId).OrderBy(t => t.ProgarmOrder).ThenBy(t => t.ProgramId);

                foreach (var g2 in programTypeGroup)
                {
                    DisplayValue programDisplayValue = new DisplayValue();
                    programDisplayValue.Value = g2.ProgramId.ToString();
                    programDisplayValue.Display = g2.ProgramName;
                    programDisplayValue.Display1 = g2.ProgramNo;
                    programDisplayValue.Display2 = applicationPath + "\\" + g2.ProgramNo;
                    sysDisplayValue.ChildDisplayValues.Add(programDisplayValue);
                }

                //var programTypeGroup = programList.Where(t => t.SystemId == g1.Key.SystemId).OrderBy(t => t.ProgarmOrder).ThenBy(t => t.ProgramId).GroupBy(t => new { t.ProgramType });

                //foreach (var g2 in programTypeGroup)
                //{
                //    DisplayValue typeDisplayValue = new DisplayValue();
                //    typeDisplayValue.Value = g2.Key.ProgramType.ToString();
                //    typeDisplayValue.Display = g2.Key.ProgramType.ToString();
                //    sysDisplayValue.ChildDisplayValues.Add(typeDisplayValue);

                //    typeDisplayValue.ChildDisplayValues = new List<DisplayValue>();

                //    var programGroup = programList.Where(t => t.SystemId == g1.Key.SystemId && t.ProgramType == g2.Key.ProgramType).OrderBy(t => t.ProgarmOrder).ThenBy(t => t.ProgramId);

                //    foreach (var g3 in programGroup)
                //    {
                //        DisplayValue programDisplayValue = new DisplayValue();
                //        programDisplayValue.Value = g3.ProgramId.ToString();
                //        programDisplayValue.Display = g3.ProgramName;
                //        programDisplayValue.Display1 = g3.ProgramNo;
                //        programDisplayValue.Display2 = applicationPath + "\\" + g3.ProgramNo;
                //        typeDisplayValue.ChildDisplayValues.Add(programDisplayValue);
                //    }
                //}
            }
            this.ViewBag.MenuModel = menu;



            var findProgram = this.IRF_DB.vw_System_Program.Where(t => t.PRGNO == this.ProgramNo);

            if (findProgram.Count() > 0)
            {
                this.Title = findProgram.First().PROGRAMNO_NAME;// + "--" + SSoft.Web.Security.User.FriendPath;
                if ((this.ViewBag.MenuModel as List<DisplayValue>).Where(t => t.Display == findProgram.First().PROGRAMNO_NAME).Count() > 0)
                {
                    this.MainMenuId = (this.ViewBag.MenuModel as List<DisplayValue>).Where(t => t.Display == findProgram.First().PROGRAMNO_NAME).First().Int01.ToString();
                }
                //this.MainMenuId = (findProgram.First().WUPage_TabId-1).ToString();
                if (this.IRF_DB.vw_System_Program.Where(t => t.PRGNO == this.ProgramNo).Count() > 0)
                {
                    this.ProgramId = this.IRF_DB.vw_System_Program.Where(t => t.PRGNO == this.ProgramNo).First().PROGRAMNO_Id.ToString();
                }
            }




            //here the routedata is available
            string controller_name = ControllerContext.RouteData.Values["Controller"].ToString();

            if (string.IsNullOrWhiteSpace(this.ProgramNo))
                this.ProgramNo = controller_name;
            this.MainMenuId = "0";
            this.IsAllow = false;
            if (this.ProgramNo.ToUpper() == "HOME")
            {
                this.IsAllow = true;
            }
            else
            {
                if (this.ProgramList != null)
                {
                    if (this.ProgramList.Where(t => t.ProgramNo.ToUpper() == this.ProgramNo.ToUpper()).Count() > 0)
                    {
                        this.IsAllow = true;
                    }
                }
            }


        }
        [System.Web.Http.HttpPost]
        public void UploadFile()
        {
            if (HttpContext.Request.Files.AllKeys.Any())
            {
                var httpPostedFile = HttpContext.Request.Files["file"];
                //bool folderExists = Directory.Exists(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                //if (!folderExists)
                //    Directory.CreateDirectory(HttpContext.Current.Server.MapPath("~/UploadedDocuments"));
                //var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadedDocuments"),
                //                                httpPostedFile.FileName);
                //httpPostedFile.SaveAs(fileSavePath);

                //if (File.Exists(fileSavePath))
                //{
                //    //AppConfig is static class used as accessor for SFTP configurations from web.config
                //    using (SftpClient sftpClient = new SftpClient(AppConfig.SftpServerIp,
                //                                                 Convert.ToInt32(AppConfig.SftpServerPort),
                //                                                 AppConfig.SftpServerUserName,
                //                                                 AppConfig.SftpServerPassword))
                //    {
                //        sftpClient.Connect();
                //        if (!sftpClient.Exists(AppConfig.SftpPath + "UserID"))
                //        {
                //            sftpClient.CreateDirectory(AppConfig.SftpPath + "UserID");
                //        }

                //        Stream fin = File.OpenRead(fileSavePath);
                //        sftpClient.UploadFile(fin, AppConfig.SftpPath + "/" + httpPostedFile.FileName,
                //                              true);
                //        fin.Close();
                //        sftpClient.Disconnect();
                //    }
                //}
            }
        }

        public string GetRenderJavascript()
        {
            this.ViewBag.ApplicationPath = Request.ApplicationPath == "/" ? "" : Request.ApplicationPath;
            string applicationPath = Request.ApplicationPath == "/" ? "" : Request.ApplicationPath;

            this.ViewBag.TabHtml = @"<tabset>
    <tab ng-repeat='tab in tabMains.MainTabs' heading='{{tab.Title}}' active='tab.active'>
        <div style='display: block;' data-demo-code='bootstrap'>
            <div class='panel panel-default'>
                <div class='panel-header'></div>
                    <div ng-include='tab.URL'></div>
                    <tabset>
                        <tab ng-repeat='tab in tab.TabMasters' heading='{{tab.Title}}' active='tab.active'>
                            <div ng-include='tab.URL'></div>
                        </tab>
                    </tabset>
                    <tabset>
                        <tab ng-repeat='tab in tab.TabDetails' heading='{{tab.Title}}' active='tab.active'>
                            <div ng-include='tab.URL'></div>
                        </tab>
                    </tabset>
                </div>
            </div>
    </tab>
</tabset>";
            this.ViewBag.Delare = "$scope.applicationPath='" + applicationPath + "';" + "$scope.onlyNumbers = '/^[1-9][0-9]*$/';$scope.userModel={};$scope.conditionUser={};$scope.pageSize = 15;$scope.querySelectedRowIndex = -1;\n$scope.UserId='" + (System.Web.HttpContext.Current != null ? System.Web.HttpContext.Current.User.Identity.Name : "") + "';\n" +
                "$scope.delay = 0;$scope.minDuration = 0;$scope.message = '資料處理中...';$scope.backdrop = true;$scope.promise = null;$scope.templateUrl='';";
            this.ViewBag.Delare += "$scope.userModel.Name='" + (SSoft.Web.Security.User.Emp_Name != null ? SSoft.Web.Security.User.Emp_Name : "") + "';\n";
            //this.ViewBag.Delare += "$scope.userModel.ADName='" + (SSoft.Web.Security.User.AD_Name != null ? SSoft.Web.Security.User.AD_Name : "") + "';\n";
            //this.ViewBag.Delare += "$scope.userModel.EMail='" + (SSoft.Web.Security.User.EMail != null ? SSoft.Web.Security.User.EMail : "") + "';\n";
            //this.ViewBag.Delare += "$scope.userModel.Phone='" + (SSoft.Web.Security.User.Phone != null ? SSoft.Web.Security.User.Phone : "") + "';\n";
            this.ViewBag.Delare += "$scope.userModel.EMP_ID=" + SSoft.Web.Security.User.Emp_ID.ToString() + ";\n";
            this.ViewBag.Delare += "$scope.userModel.LastLoginDatetime='" + (SSoft.Web.Security.User.Last_Logindate != null ? SSoft.Web.Security.User.Last_Logindate : "") + "';\n";
            string jsonStirngDDDW = YMIR.App_Code.Utils.GetJsonConvertSerializeString(this.DDDWWrapper);

            this.ViewBag.DDDW = @"$scope.modeldddw=" + jsonStirngDDDW + ";";

            //tabWrapper = Tab.GetStandTab(SSoft.Enum.PatternType.MMD,"PNLMAT10");
            string jsonStirngTab = YMIR.App_Code.Utils.GetJsonConvertSerializeString(this.TabWrapper);

            this.ViewBag.Tab = @"$scope.tabMains=" + jsonStirngTab + ";";


//            this.ViewBag.MenuModel = @"$scope.MenuModel=" + YMIR.App_Code.Utils.GetJsonConvertSerializeString(this.ViewBag.MenuModel) + ";";


//            this.ViewBag.MenuModel += @"$scope.subMenuModel = [];
//        $scope.mainMenuClick = function(mainIndex,programNo)
//        {
//            //alert(mainIndex);
//            angular.forEach($scope.MenuModel, function (value, key) {
//                value.Display1 = value.Display2;
//            }); 
//            if($scope.MenuModel[mainIndex])
//            {
//                $scope.MenuModel[mainIndex].Display1 = $scope.MenuModel[mainIndex].Display3;
//                $scope.subMenuModel = $scope.MenuModel[mainIndex].ChildDisplayValues;
            
//                angular.forEach($scope.subMenuModel, function(value, index) {
//                    var programCount = Enumerable.From(value.ChildDisplayValues).Where(function (item) {return item.Display1 == programNo }).ToArray();
//                    if (programCount.length > 0) {
//                        value.BooleanValue = true;
//                    }
//                });
//            }
//        };
//";
//            this.ViewBag.MenuModel += "$scope.mainMenuClick(" + this.MainMenuId + ",'" + this.ProgramNo + "');";
//            this.ViewBag.MenuModel += "$scope.QueryData=[]; $scope.modelTemp = {};";
            //定義ModelInitial            
            string childTableString = "";
            if (this.ChildTables != null)
            {
                foreach (string table in this.ChildTables)
                {
                    childTableString += "$scope.model." + table + " = [];";
                }
            }
            this.ViewBag.ModelInitial = "$scope.condition = {};$scope.model = {};$scope.modelOriginal = {};" + (string.IsNullOrWhiteSpace(childTableString) ? "" : childTableString);

            //新增
            this.ViewBag.AddFunction = @"$scope.Add = function () {
            if($scope.UAddBefore)
                {
                    if(!$scope.UAddBefore())
                        return;                
                }
            if (confirm(""確定要放棄修改中的資料?"")) {
                $scope.model = {};
                $scope.modelOriginal = {};
                " + (string.IsNullOrWhiteSpace(childTableString) ? "" : childTableString) + @"
                if($scope.UAddAfter)
                {
                    $scope.UAddAfter();         
                }
             }};";

            //刪除
            this.ViewBag.DeleteFunction = @"$scope.Delete = function () {

            if($scope.UDeleteBefore)
            {
                if(!$scope.UDeleteBefore())
                    return;                
            }
            if (confirm(""確定要進行刪除"")) {
                if(typeof $scope.model.ID == ""undefined"" ||  $scope.model.ID == 0)
                {
                    $scope.model = {};
                    $scope.modelOriginal = {};

                    //Default Value
                    //$scope.model.EMP_NO = '222';
                    //$scope.model.EMP_NM = '333';

                    //Child Table Initial
                   " + (string.IsNullOrWhiteSpace(childTableString) ? "" : childTableString) + @"
                    if($scope.UDeleteAfter)
                    {
                        $scope.UDeleteAfter();          
                    }
                    alert(""刪除成功"");
                }
                else
                {
                    $scope.promise=$http.post(""" + applicationPath + @"/api/" + this.ProgramNo + @"/POSTDelete"",{modelCurrent: $scope.model, modelOriginal: $scope.modelOriginal }  ).success(function (data) {

                        $scope.model = {};
                        $scope.modelOriginal = {};

                        //Default Value
                        //$scope.model.EMP_NO = '222';
                        //$scope.model.EMP_NM = '333';

                        //Child Table Initial
                       " + (string.IsNullOrWhiteSpace(childTableString) ? "" : childTableString) + @"
                    if($scope.UDeleteAfter)
                    {
                        $scope.UDeleteAfter();          
                    }
                        alert(""刪除成功"");

                    }).error(function (data, status) {

                        alert(data + status);
                    });
                }


            }
        };";

            //取得維護資料 Retrieve By Key
            this.ViewBag.RetrieveByKey = @"$scope.selectQueryRow = '<button class=""btn btn-sm""  ng-click=""Retrieve(row.entity,row.rowIndex)""><img width=""10"" height=""10"" title=""選擇"" src=""" + applicationPath + @"/Images/ToolBar/glyphicons-120-table.png""></button>';
        $scope.Retrieve = function (row,rowIndex) {
            $scope.promise=$http.post(""" + applicationPath + @"/api/" + this.ProgramNo + @"/POSTRetrieveDataByKey/"" + row.ID).success(function (data) {
                //alert(""success"");
                $scope.model = data[0];
                $scope.modelOriginal = angular.copy($scope.model);
                $scope.tabMains.MainTabs[0].active = true;

                $scope.querySelectedRowIndex = rowIndex;
                //alert($scope.querySelectedRowIndex);
            }).error(function (data, status) {
                alert(data + status);
            });
        };";

            //明細刪除
            List<string> deleteDetailFunctions = new List<string>();

            int index = 0;
            if (this.ChildTables != null)
            {
                foreach (var table in this.ChildTables)
                {
                    deleteDetailFunctions.Add(@"if ($scope.tabMains.MainTabs[0].TabDetails[" + index.ToString() + @"].active) {
                    var index = $scope.model." + table + @".indexOf(row.entity);

                    if($scope.UDetailDeleteBefore)
                    {
                        if(!$scope.UDetailDeleteBefore(row.entity,index," + index.ToString() + @"))
                            return;                
                    }
                    //alert(index);
                    $scope.model." + table + @".splice(index, 1);
                    if($scope.UDetailDeleteAfter)
                    {
                        $scope.UDetailDeleteAfter(row.entity,index," + index.ToString() + @");                
                    }
                }");
                    index++;
                }
            }

            this.ViewBag.DeleteDetailFunctions = @"$scope.deletegridrow = '<button class=""btn btn-sm""  ng-click=""DeleteDetail(row);""  data-title=""刪除""   bs-tooltip data-trigger=""hover"" ><img width=""10"" height=""10"" title=""刪除"" src=""" + applicationPath + @"/Images/ToolBar/glyphicons-208-remove-2.png""></button>';
        $scope.DeleteDetail = function (row) {

            
            //alert(row.entity.ID);
            if (confirm(""確定要進行刪除"")) {" + string.Join(" else ", deleteDetailFunctions.ToArray()) + @"
}};";


            //Free Form 維護
            List<string> freeformFunctions = new List<string>();

            index = 0;
            if (this.ChildTables != null)
            {
                foreach (var table in this.ChildTables)
                {
                    freeformFunctions.Add(@"if ($scope.tabMains.MainTabs[0].TabDetails[" + index.ToString() + @"].active) {
                ngDialog.open({
                    template: '" + applicationPath + @"/api/" + this.ProgramNo + @"/GetPartialView/_" + this.ProgramNo + @"_Detail" + (index + 1).ToString("00") + @"_f',
                    className: 'ngdialog-theme-plain',
                    scope: $scope,
                    cache: false
                });
            }");
                    index++;
                }
            }

            this.ViewBag.FreeformFFunctions = @"$scope.freeformedit = '<button class=""btn btn-sm""  ng-click=""OpenFreeform(row);""   data-title=""維護""   bs-tooltip data-trigger=""hover""><img width=""10"" height=""10"" title=""Free Form 維護"" src=""" + applicationPath + @"/Images/ToolBar/glyphicons-236-pen.png""></button>';
        $scope.OpenFreeform = function (row) {
            //alert(row.entity.ID);
" + string.Join(" else ", freeformFunctions.ToArray()) + @"};";

            //明細新增
            List<string> detailAddFunctions = new List<string>();

            index = 0;
            if (this.ChildTables != null)
            {
                foreach (var table in this.ChildTables)
                {
                    detailAddFunctions.Add(@"if ($scope.tabMains.MainTabs[0].TabDetails[" + index.ToString() + @"].active) {
                    var row = {};
                     if($scope.UDetailAddBefore)
                        {
                            if(!$scope.UDetailAddBefore(row," + index.ToString() + @"))
                                return;                
                        }
                    
                    $scope.model." + table + @".push(row);
            
            if($scope.UDetailAddAfter)
                        {
                            $scope.UDetailAddAfter(row," + index.ToString() + @")
                        }           
            }");

                    index++;
                }
            }
            this.ViewBag.DetailAddFunctions = @"$scope.DetailAdd = function () {


" + string.Join(" else ", detailAddFunctions.ToArray()) + @"};";

            //查詢
            this.ViewBag.QueryFunction = @"         

        $scope.Query = function () {

if($scope.QueryBefore){
                if(!$scope.QueryBefore())
                    return;                
            }
            $scope.promise=$http.post( """ + applicationPath + @"/api/" + this.ProgramNo + @"/POSTRetrieveDataByCondition"",{condition:$scope.condition}).success(function (data) {
                //if($scope.tabMains && $scope.tabMains.MainTabs && $scope.tabMains.MainTabs[2]){$scope.tabMains.MainTabs[2].active = true;}
                $scope.QueryData = data;
                
            if($scope.QueryAfter)
                {
                    $scope.QueryAfter(data);
                }
            }).error(function (data, status) {

                alert(data + status);
            });
        };";


            //存檔
            this.ViewBag.SaveFunction = @"$scope.Save = function () {
             if($scope.USaveBefore){
                if(!$scope.USaveBefore())
                    return;                
            }
             
            //$http.post(""/api/PNLMAT10/POST"", $scope.model).success(function (data) {
            $scope.promise=$http.post(""" + applicationPath + @"/api/" + this.ProgramNo + @"/POSTSave"", { modelCurrent: $scope.model, modelOriginal: $scope.modelOriginal }).success(function (data) {
                $scope.model = data[0];
                $scope.modelOriginal = angular.copy($scope.model);
                if($scope.USaveAfter)
                {
                    $scope.USaveAfter();
                }
                alert(""存檔成功"");
            }).error(function (data, status) {

                alert(data + status);
            });
        };";

            this.ViewBag.SelectUserDeptFriend = @"
$scope.QuerySelectUserDept = {};
        $scope.QuerySelectUserDept_Temp = {};
        $scope.QuerySelectUserDept.User = [];
        $scope.QuerySelectUserDept.Dept = [];
        $scope.QuerySelectUserDept.Friend = [];
        $scope.QuerySelectUserTableParams = new ngTableParams({
            page: 1,
            count: $scope.pageSize
        }, {
            total: $scope.QuerySelectUserDept.User.length,
            getData: function ($defer, params) {
                    var receiveData = function (data) {
                    params.total(data.count);
                    $defer.resolve(data.data);
                };

                $scope.RetrieveUserDeptData('1', params.page(),params.count(), receiveData);
            }
    });

        $scope.QuerySelectFriendTableParams = new ngTableParams({
    page: 1,
            count: $scope.pageSize
        }, {
            total: $scope.QuerySelectUserDept.Friend.length,
            getData: function($defer, params)
    {

        var receiveData = function(data) {
                    params.total(data.count);
                    $defer.resolve(data.data);
        };

                $scope.RetrieveUserDeptData('3', params.page(), params.count(), receiveData);

    }
});
        $scope.QuerySelectDeptTableParams = new ngTableParams({
page: 1,
            count: $scope.pageSize
        }, {
            total: $scope.QuerySelectUserDept.Dept.length,
            getData: function($defer, params)
{

    var receiveData = function(data) {
                    params.total(data.count);
                    $defer.resolve(data.data);
    };

                $scope.RetrieveUserDeptData('2', params.page(), params.count(), receiveData);

}
        });
        $scope.Open_Select_User_DialogId;
        $scope.OpenSelectUser = function()
{
            $scope.conditionUser.tab = 'left';
            
                    $scope.Open_Select_User_DialogId = ngDialog.open({
    template: '" + applicationPath + @"/api/Public/GetPartialView/_PUB_Select_User',
                        className: 'ngdialog-theme-default width850',
                        scope: $scope,
                        cache: false
                    });

};

$scope.Open_Select_Dept_DialogId;
        $scope.OpenSelectDept = function () {

            
                    $scope.Open_Select_Dept_DialogId = ngDialog.open({
                template: '" + applicationPath + @"/api/Public/GetPartialView/_PUB_Select_Dept',
                        className: 'ngdialog-theme-default width850',
                        scope: $scope,
                        cache: false
                    });
           
        };


        $scope.RetrieveUserDeptData = function(dataType, pageNumber, pageSize, receiveData)
{
           
            $http.post('" + applicationPath + @"/api/Public/POSTRetrieveUserDeptData', { dataType: dataType, pageNumber: pageNumber, pageSize: pageSize, key: $scope.conditionUser.key }).success(function(data) {
        if (!data.iserror)
        {
            receiveData(data.data);
        }
        else
        {
            alert(data.message);
        }
    }).error(function(data, status) {
        alert(data + status);
    });
};

        $scope.SelectUserCheckAll = function(data)
{
    angular.forEach(data, function(item, index) {
        item.BooleanValue = true;
    });
}

        $scope.SelectUserUnCheckAll = function(data)
{
    angular.forEach(data, function(item, index) {
        item.BooleanValue = false;
    });
}

        $scope.SelectUserOK = function(data)
{

    var isAddUser = false;
    var users = Enumerable.From($scope.QuerySelectUserTableParams.data).Where(function(item) { if (item.BooleanValue) return true; else return false; }).ToArray();
    angular.forEach(users, function(value, index) {
        if (Enumerable.From($scope.QueryDataUser).Where(function(item) { if (item.Value == value.Value) return true; else return false; }).ToArray().length == 0)
                {
                    $scope.QueryDataUser.push({ Int01: value.Int01, Int02: value.Int02, Value: value.Value, Display: value.Display, Display1: value.Display1, Display2: value.Display2, Display3: value.Display3, Display4: value.Display4 });
            isAddUser = true;
        }
    });

    var friends = Enumerable.From($scope.QuerySelectFriendTableParams.data).Where(function(item) { if (item.BooleanValue) return true; else return false; }).ToArray();
    angular.forEach(friends, function(value, index) {
        if (Enumerable.From($scope.QueryDataUser).Where(function(item) { if (item.Value == value.Value) return true; else return false; }).ToArray().length == 0) {
                    $scope.QueryDataUser.push({ Int01: value.Int01, Int02: value.Int02, Value: value.Value, Display: value.Display, Display1: value.Display1, Display2: value.Display2, Display3: value.Display3, Display4: value.Display4 });
            isAddUser = true;
        }
    });
    var findUsers = [];
    var depts = Enumerable.From($scope.QuerySelectDeptTableParams.data).Where(function(item) { if (item.BooleanValue) return true; else return false; }).ToArray();
    angular.forEach(depts, function(value, index) {
        findUsers = value.ChildDisplayValues;
        if (findUsers.length > 0)
        {
            angular.forEach(findUsers, function(value1, index1) {
                if (Enumerable.From($scope.QueryDataUser).Where(function(item) { if (item.Value == value1.Value) return true; else return false; }).ToArray().length == 0) {
                            $scope.QueryDataUser.push({ Int01: value1.Int01, Int02: value1.Int02, Value: value1.Value, Display: value1.Display, Display1: value1.Display1, Display2: value1.Display2, Display3: value1.Display3, Display4: value1.Display4 });
                    isAddUser = true;
                }
            });
        }
    });

    if (isAddUser)
    {
                $scope.setupUserTableParams.reload();

                if( $scope.SelectUserAfter)
                {
                    $scope.SelectUserAfter($scope.QueryDataUser);
                }
    }
    ngDialog.close($scope.Open_Select_User_DialogId.id);
}

        $scope.SelectUserQuery = function(data)
{
    //if ($scope.conditionUser.key) {
                $scope.QuerySelectUserTableParams.reload();
                $scope.QuerySelectDeptTableParams.reload();
                $scope.QuerySelectFriendTableParams.reload();
                $scope.QuerySelectUserTableParams.page(1);
                $scope.QuerySelectDeptTableParams.page(1);
                $scope.QuerySelectFriendTableParams.page(1);
    //}
}";



            this.ViewBag.FileUploadFunction = @"
            $scope.showImagePanel = false;
        $scope.allowFormat = '';
        $scope.Open_FileUpload_DialogId;
        $scope.uploadFileData = {};

        $scope.OpenFileUpload = function (fileName, fileGUID, fileURL) {
            $scope.model.__file = null;
            $scope.modelTemp.__FileName = fileName;
            $scope.modelTemp.__FileGUID = fileGUID;
            $scope.modelTemp.__FileURL = fileURL;
            $scope.Open_FileUpload_DialogId = ngDialog.open({
                template: '" + applicationPath + @"/api/Public/GetPartialView/_PUB_FileUpload',
                className: 'ngdialog-theme-default width850',
                scope: $scope,
                cache: false
            });
        };
        
        $scope.OpenFileUploadOK = function () {

            if ($scope.model.__file && $scope.model.__file.length > 0) {
                var msg = '';
                var extn = $scope.model.__file[0].name.substr($scope.model.__file[0].name.lastIndexOf('.'));
                if ($scope.model.__file[0].size == 0) {
                    msg += '檔案大小不可為零.\n';
                }
               
                if (angular.isDefined($scope.allowFormat)) {
                    if ($scope.allowFormat.toLowerCase().indexOf(extn.toLowerCase()) === -1) {
                        msg += '檔案格式不符(' + $scope.allowFormat + ').\n';
                    }
}
                if (msg) {
                    alert(msg);
                    return;
                }

                Upload.upload({
                    url: '" + applicationPath + @"/api/" + this.ProgramNo + @"/UploadFile',
                    method: 'POST',
                    data: $scope.uploadFileData,
                    file: $scope.model.__file
                }).progress(function (evt)
{
    // get upload percentage
    console.log('percent: ' + parseInt(100.0 * evt.loaded / evt.total));
}).success(function (data, status, headers, config)
{
                    //$scope.model = data[0];
                    //$scope.modelOriginal = angular.copy($scope.model);
                    //$scope.model.file01 = [];
                    $scope.model.__file = undefined;
    //alert(data[0]['FileName']);

    if ($scope.modelTemp.__FileName) {
                        $scope.modelTemp[$scope.modelTemp.__FileName] = data.data[0]['FileName'];
    }
    if ($scope.modelTemp.__FileGUID) {
                        $scope.modelTemp[$scope.modelTemp.__FileGUID] = data.data[0]['FileGUID'];
    }
    if ($scope.modelTemp.__FileURL) {
                        $scope.modelTemp[$scope.modelTemp.__FileURL] = data.data[0]['FileURL'];
    }

    ngDialog.close($scope.Open_FileUpload_DialogId.id);
    if (data.iserror)
    {
        alert(data.message);
    }
    else
    {
                        $scope.modelTemp.__FileName = undefined;
                        $scope.modelTemp.__FileGUID = undefined;
                        $scope.modelTemp.__FileURL = undefined;

        if ($scope.OpenFileUploadAfter)
                        {
                            $scope.OpenFileUploadAfter(data);
        }

    }
    //console.log(data);
}).error(function (data, status, headers, config)
{
    alert(status);
    //console.log(data);
});
            }
            else {
                //$scope.model[$scope.model.__FileName] = data[0]['FileName'];
                //$scope.model[$scope.model.__FileGUID] = data[0]['FileGUID'];
                //$scope.model[$scope.model.__FileURL] = data[0]['FileURL'];

                //ngDialog.close($scope.Open_FileUpload_DialogId.id);
                alert('請選擇檔案');
            }
        }

        $scope.abortUpload = function(index)
{
            $scope.upload[index].abort();
}

        ";

            return this.ViewBag.Delare + this.ViewBag.DDDW + this.ViewBag.Tab + this.ViewBag.MenuModel + this.ViewBag.ModelInitial + this.ViewBag.AddFunction + this.ViewBag.DeleteFunction + this.ViewBag.RetrieveByKey + this.ViewBag.DeleteDetailFunctions + this.ViewBag.FreeformFFunctions +
                this.ViewBag.DetailAddFunctions + this.ViewBag.QueryFunction + this.ViewBag.SaveFunction + this.ViewBag.FileUploadFunction + this.ViewBag.SelectUserDeptFriend;
        }
    }
}