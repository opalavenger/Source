using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSoft.MVC
{
    public class ConditionParameter
    {
        public string DatabaseColumnName { get; set; }
        public string ClassPropertyFieldName { get; set; }
        public string ClassPropertyFieldName1 { get; set; }
        public SSoft.Enum.EnumOperator Operator { get; set; }
        public object Value;
        public object Value1;
        public string ValueType { get; set; }
        public string Expression { get; set; }
        public bool IsBetween { get; set; }
        public string Operand { get; set; }
        public bool IsAdvance { get; set; }
    }
}
