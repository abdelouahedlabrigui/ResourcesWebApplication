using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class RegressionResult
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string RegressionResultIdentifier { get; set; }
        [Required]
        public string Variable { get; set; }
        [Required]
        public string Coefficient { get; set; }
        [Required]
        public string StdError { get; set; }
        [Required]
        public string ZValue { get; set; }
        [Required]
        public string PValue { get; set; }
        [Required]
        public string CILower { get; set; }
        [Required]
        public string CIUpper { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}