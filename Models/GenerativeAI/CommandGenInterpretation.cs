using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI
{
    public class CommandGenInterpretation
    {
        public int Id { get; set; }
        [Required]
        public string LlmAgent { get; set; }
        [Required]
        public string OperationSystem { get; set; }
        [Required]
        public string Technology { get; set; }
        [Required]
        public string Command { get; set; }
        [Required]
        public string Interpretation { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}