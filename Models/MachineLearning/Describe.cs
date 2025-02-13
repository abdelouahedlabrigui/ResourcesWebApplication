using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Describe
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Column { get; set; }
        [Required]
        public string Count { get; set; }
        [Required]
        public string Mean { get; set; }
        [Required]
        public string Std { get; set; }
        [Required]
        public string Min { get; set; }
        [Required]
        public string TwentyFivePercent { get; set; }
        [Required]
        public string FiftyPercent { get; set; }
        [Required]
        public string SeventyFivePercent { get; set; }
        [Required]
        public string Max { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}