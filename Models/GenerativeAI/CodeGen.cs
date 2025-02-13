using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI
{
    public class CodeGen
    {
        public int Id { get; set; }
        [Required]
        public string LlmAgent { get; set; }
        [Required]
        public string Prompt { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}