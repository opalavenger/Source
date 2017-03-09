using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.Data.Attributes
{
  
        [AttributeUsage(AttributeTargets.Property)]
        public sealed class ConditionModelAttribute : Attribute
        {
              public string DatabaseColumnName { get; set; }
              public string DisplayName { get; set; }
              public string DDDWName { get; set; }
        }
        [AttributeUsage(AttributeTargets.Class)]
        public sealed class ConditionModelClassAttribute : Attribute
        {
            public string DatabaseMainTableName { get; set; }
        }

    //private string GetActionToolTip(MethodInfo method)
    //{
    //    return (method.GetCustomAttributes(typeof(TooBarItemAttribute), false).SingleOrDefault() as TooBarItemAttribute).ToolTip;
    //}

}
