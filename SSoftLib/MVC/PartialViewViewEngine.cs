using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SSoft.MVC
{
    public class PartialViewViewEngine : RazorViewEngine
    {
        public PartialViewViewEngine()
        {
            var newLocationFormat = new[]
                                    {
                                        "~/Views/{1}/Partial/{0}.cshtml",
                                    };

            PartialViewLocationFormats = PartialViewLocationFormats.Union(newLocationFormat).ToArray();
        }
    }

}
