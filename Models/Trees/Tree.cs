using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class Tree
    {
        public InitialMetadata InitialMetadata { get; set; }
        public Parent Parent { get; set; }
        public PSDrive PSDrive { get; set; }
        public PSProvider PSProvider { get; set; }
        public Root Root { get; set; }
    }
}