using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace YMIR.Models.Sys
{
    public class MenuModel
    {
        public Guid SystemId { get; set; }
        public string SystemName { get; set; }
        public string SystemClass { get; set; }
        public string ProgramType { get; set; }
        public Guid ProgramId { get; set; }
        public string ProgramNo { get; set; }
        public string ProgramName { get; set; }
        public string ProgramPath { get; set; }
        public int ProgarmOrder { get; set; }

        public bool IsEnable { get; set; }
    }
}