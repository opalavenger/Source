//------------------------------------------------------------------------------
// <auto-generated>
//     這個程式碼是由範本產生。
//
//     對這個檔案進行手動變更可能導致您的應用程式產生未預期的行為。
//     如果重新產生程式碼，將會覆寫對這個檔案的手動變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace YMIR.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class WebSite
    {
        public int WebSite_NO { get; set; }
        public int WebSite_WebNO { get; set; }
        public string WebSite_Name { get; set; }
        public string WebSite_Path { get; set; }
        public bool WebSite_IsEnable { get; set; }
        public int WebSite_ModifyUser { get; set; }
        public int WebSite_CreateUser { get; set; }
        public System.DateTime WebSite_ModifyTime { get; set; }
        public System.DateTime WebSite_CreateTime { get; set; }
    
        public virtual WebSiteType WebSiteType { get; set; }
    }
}
