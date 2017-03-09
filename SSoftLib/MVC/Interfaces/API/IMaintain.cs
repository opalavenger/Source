using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC.Interfaces.API
{
    public interface IMaintain
    {
        dynamic POSTSave(object value);
        dynamic POSTDelete(object value);        
        //存檔前
        event EventHandler<MVC.Events.SaveEntityEventArgs> USaveBeforeEvent;
        //存檔後Commit前
        event EventHandler<MVC.Events.SaveEntityEventArgs> USaveAfterDBChangeBeforeEvent;
        //存檔後Commit後
        event EventHandler<MVC.Events.SaveEntityEventArgs> USaveAfterDBChangeAfterEvent;
        //刪除前
        event EventHandler<MVC.Events.DeleteEntityEventArgs> UDeleteBeforeEvent;
        //刪除後Commit前
        event EventHandler<MVC.Events.DeleteEntityEventArgs> UDeleteAfterDBChangeBeforeEvent;
        //刪除後Commit後
        event EventHandler<MVC.Events.DeleteEntityEventArgs> UDeleteAfterDBChangeAfterEvent;
        //條件查詢前
        event EventHandler<MVC.Events.ConditonQueryEntityEventArgs> URetrieveByConditonBeforeEvent;
        //條件查詢後
        event EventHandler<MVC.Events.ConditonQueryEntityEventArgs> URetrieveByConditonAfterEvent;
        //RetrieveByKey前
        event EventHandler<MVC.Events.RetrieveEntityEventArgs> URetrieveByKeyBeforeEvent;
        //RetrieveByKey後
        event EventHandler<MVC.Events.RetrieveEntityEventArgs> URetrieveByKeyAfterEvent;
        SSoft.MVC.Interfaces.DataObjectInfo DataObjectInfo { get; set; }
    }
}
