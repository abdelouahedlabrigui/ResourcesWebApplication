using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Careers
{
    public class Responsibility
    {
        public int Id { get; set; }
        [Required]
        public string JobTitle { get; set; }
        [Required]
        public string Responsibilities { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}