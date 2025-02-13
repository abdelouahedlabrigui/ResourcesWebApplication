using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Prompts.MachineLearning
{
    public class GenInfoInterpretation
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Information { get; set; }
        [Required]
        public string Interpretation { get; set; }
    }
}