using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC
{
    public class Tab
    {
        public string Title { get; set; }
        public string URL { get; set; }
        public List<Tab> TabMasters { get; set; }
        public List<Tab> TabDetails { get; set; }

        public Tab()
        {
            this.TabMasters = new List<Tab>();
            this.TabDetails = new List<Tab>();
        }

        public static dynamic GetStandTab(SSoft.Enum.PatternType type,string programNo)
        {
            switch(type)
            {
                case  Enum.PatternType.MSS:
                    dynamic tabWrapperMSS = new
                    {
                        MainTabs = new List<Tab>() { 
                 new Tab() { Title="資料維護",URL=string.Format("/api/{0}/GetPartialView/_{0}_Master01",programNo)},
                 new Tab() { Title="查詢條件",URL=string.Format("/api/{0}/GetPartialView/_{0}_Condition",programNo) },
                 new Tab() { Title="查詢輸出",URL=string.Format("/api/{0}/GetPartialView/_{0}_Query",programNo) }}
                    };
                    return tabWrapperMSS;

                case Enum.PatternType.MMD:
                    dynamic tabWrapperMMD = new
                    {
                        MainTabs = new List<Tab>() { 
                 new Tab() { Title="資料維護",URL=string.Format("/api/{0}/GetPartialView/_{0}_Master01",programNo),
                 TabDetails = new List<Tab>(){
                      new Tab() { Title = "明細資料", URL  =string.Format("/api/{0}/GetPartialView/_{0}_Detail01",programNo) }}
                 },
                 new Tab() { Title="查詢條件",URL=string.Format("/api/{0}/GetPartialView/_{0}_Condition",programNo) },
                 new Tab() { Title="查詢輸出",URL=string.Format("/api/{0}/GetPartialView/_{0}_Query",programNo) }}
                    };

                    return tabWrapperMMD;
                case Enum.PatternType.CFG:
                    dynamic tabWrapperCFG = new
                    {
                        MainTabs = new List<Tab>() { 
                 new Tab() { Title="資料維護",URL=string.Format("/api/{0}/GetPartialView/_{0}_Master01",programNo)}}
                    };
                    return tabWrapperCFG;
            }

            return null;
        }
    }
}
