using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Careers
{
    public class Experience
    {
        public int Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Experiences { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}