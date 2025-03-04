using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Prompts
{
    public class TerraformPrompts
    {
        public int Id { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Prompt { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}