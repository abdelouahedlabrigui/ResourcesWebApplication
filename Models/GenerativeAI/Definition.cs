using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI
{
    public class Definition
    {
        public int Id { get; set; }
        [Required]
        public string Concept { get; set; }
        [Required]
        public string Paragraph { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}