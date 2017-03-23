using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC
{
    public class DisplayValue
    {
        public string Display { get; set; }
        public string Value { get; set; }
        public string Display1 { get; set; }
        public string Display2 { get; set; }
        public string Display3 { get; set; }
        public string Display4 { get; set; }
        public string Display5 { get; set; }
        public string Display6 { get; set; }
        public string Display7 { get; set; }
        public string Display8 { get; set; }
        public string Display9 { get; set; }
        public string Display10 { get; set; }
        public string Display11 { get; set; }
        //public string Display12 { get; set; }
        //public string Display13 { get; set; }
        //public string Display14 { get; set; }
        //public string Display15 { get; set; }
        //public string Display16 { get; set; }
        //public string Display17 { get; set; }
        //public string Display18 { get; set; }
        //public string Display19 { get; set; }
        //public string Display20 { get; set; }
        public int intValue { get; set; }
        public int Int01 { get; set; }
        public bool BooleanValue { get; set; }
        public List<DisplayValue> ChildDisplayValues { get; set; }

        public DisplayValue()
        {
            ChildDisplayValues = new List<DisplayValue>();
        }
       
    }
}
