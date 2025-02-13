using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents.Summary
{
    public class Summary
    {
        public int Id { get; set; } 
        [Required]
        public string Title { get; set; } 
        [Required]
        public string Part { get; set; } 
        [Required]
        public string Chapter { get; set; } 
        [Required]
        public string Section { get; set; } 
        [Required]
        public string Subsection { get; set; } 
    }
}