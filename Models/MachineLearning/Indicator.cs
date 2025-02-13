using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Indicator
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Intercept { get; set; }
        [Required]
        public string Coefficient { get; set; }
        [Required]
        public string Score { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}