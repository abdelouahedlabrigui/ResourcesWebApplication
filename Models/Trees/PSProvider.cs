using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class PSProvider
    {
        public int Id { get; set; }
        public string Capabilities { get; set; }
        public string Description { get; set; }
        public string Drives { get; set; }
        public string HelpFile { get; set; }
        public string Home { get; set; }
        public string ImplementingType { get; set; }
        public string Module { get; set; }
        public string ModuleName { get; set; }
        public string Name { get; set; }
        public string PSSnapIn { get; set; }
    }
}