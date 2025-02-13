using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Knowledge
{
    public class Mixed
    {
        public Skill Skill { get; set; }
        public List<int> DocumentIDs { get; set; }
        public List<int> CodeID { get; set; }
    }
}