using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.SourceCodes.AtmosphericSciences
{
    public class AtmosphericCodeLinePrompt
    {
        // code_snippet_name code_line prompt
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CodeLine { get; set; }
        [Required]
        public string Prompt { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}