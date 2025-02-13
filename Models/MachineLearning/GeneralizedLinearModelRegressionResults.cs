using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class GeneralizedLinearModelRegressionResult
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string GeneralizedLinearModelRegressionResultsIdentifier { get; set; }
        [Required]
        public string ResultName { get; set; }
        [Required]
        public string ResultValue { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}