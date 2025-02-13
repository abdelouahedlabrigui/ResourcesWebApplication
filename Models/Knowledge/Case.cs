using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Knowledge
{
    public class Case
    {
        public int Id { get; set; }
        [Required]
        public string SkillID { get; set; }
        [Required]
        public string DocumentID { get; set; }
        [Required]
        public string CodeID { get; set; } 
        [Required]     
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}