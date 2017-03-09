using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace SSoft.Web.Security
{
    public class Utilities
    {
        public static string GetMD5(string str)
        {
            /// <summary>"utf-32"
            /// 與ASP兼容的MD5加密算法   
            /// </summary>

            byte[] b = System.Text.Encoding.Default.GetBytes(str);
            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
                ret += b[i].ToString("x").PadLeft(2, '0');
            return ret;
        }
    }
}
