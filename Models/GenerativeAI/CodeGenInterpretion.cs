using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI
{
    public class CodeGenInterpretion
    {
        public int Id { get; set; }
        [Required]
        public string LlmAgent { get; set; }
        [Required]
        public string CodeLine { get; set; }
        [Required]
        public string Interpretation { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}