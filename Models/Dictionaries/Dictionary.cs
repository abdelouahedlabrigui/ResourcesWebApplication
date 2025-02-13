using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Dictionaries
{
    public class Dictionary
    {
        public int Id { get; set; }
        [Required]
        public string Concept { get; set; }
        [Required]
        public string Definition { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}