using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class ArrayCoefficient
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string ArrayCoefficientIdentifier { get; set; }
        [Required]
        public string Coefficients { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}