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
    
    public partial class WebUserInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public WebUserInfo()
        {
            this.UserInfo = new HashSet<UserInfo>();
        }
    
        public int WebUI_Id { get; set; }
        public string WebUI_Token { get; set; }
        public System.DateTime WebUI_TokenTime { get; set; }
        public bool WebUI_ISEnabled { get; set; }
        public System.DateTime WebUI_LastLTime { get; set; }
        public System.DateTime WebUI_LTime { get; set; }
        public System.DateTime WebUI_ModifyTime { get; set; }
        public System.DateTime WebUI_CreateTime { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UserInfo> UserInfo { get; set; }
    }
}
