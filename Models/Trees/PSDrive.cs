using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class PSDrive
    {
        public int Id { get; set; }
        public string Credential { get; set; }
        public string CurrentLocation { get; set; }
        public string Description { get; set; }
        public string DisplayRoot { get; set; }
        public string MaximumSize { get; set; }
        public string Name { get; set; }
        public string Provider { get; set; }
        public string Root { get; set; }
    }
}