using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Careers
{
    public class Role
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Skill { get; set; }
        public string Detail { get; set; }
    }
}