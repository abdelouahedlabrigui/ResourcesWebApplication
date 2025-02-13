using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Careers
{
    public class RoleDetail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TitleDetail { get; set; }
        public string Skill { get; set; }
        public string SkillDetail { get; set; }
        public string Detail { get; set; }
        public string DetailDepth { get; set; }
    }
}