using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Web.Security;
using System.Security;
using System.Security.Principal;
using System.Security.Permissions;

namespace SSoft.Classes
{
    public class Impersonate
    {
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool LogonUser(String lpszUsername, String lpszDomain, String lpszPassword, int dwLogonType, int dwLogonProvider, ref IntPtr phToken);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public extern static bool CloseHandle(IntPtr handle);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public extern static bool DuplicateToken(IntPtr ExistingTokenHandle, int SECURITY_IMPERSONATION_LEVEL, ref IntPtr DuplicateTokenHandle);

        public static WindowsImpersonationContext SetImpersonate()
        {
            IntPtr tokenHandle = new IntPtr(0);
            IntPtr dupeTokenHandle = new IntPtr(0);
            System.Web.HttpContext _c = System.Web.HttpContext.Current;
            try
            {
                string username, domainname, password;
                domainname = System.Web.Configuration.WebConfigurationManager.AppSettings["domainname"].ToString();//"";
                username = System.Web.Configuration.WebConfigurationManager.AppSettings["username"].ToString();//"super";
                password = System.Web.Configuration.WebConfigurationManager.AppSettings["password"].ToString();//"super0819";

                const int LOGON32_PROVIDER_DEFAULT = 0;

                const int LOGON32_LOGON_INTERACTIVE = 2;

                tokenHandle = IntPtr.Zero;

                bool returnValue = LogonUser(username, domainname, password, LOGON32_LOGON_INTERACTIVE, LOGON32_PROVIDER_DEFAULT, ref tokenHandle);

                if (returnValue == false)
                {
                    int ret = Marshal.GetLastWin32Error();
                    throw new System.ComponentModel.Win32Exception(ret);
                }

                WindowsIdentity newId = new WindowsIdentity(tokenHandle);

                WindowsImpersonationContext impersonatedUser = newId.Impersonate();

                //System.Web.HttpContext.Current.Session["ImpersonatedUser"] = impersonatedUser;

                if (tokenHandle != IntPtr.Zero)
                {
                    CloseHandle(tokenHandle);
                }

                return impersonatedUser;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

      
    
    }
}
