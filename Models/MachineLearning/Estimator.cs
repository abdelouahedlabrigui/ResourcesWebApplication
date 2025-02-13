using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Estimator
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string ProbabilityOfNo { get; set; }
        [Required]
        public string ProbabilityOfYes { get; set; }
        [Required]
        public string Decision { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}