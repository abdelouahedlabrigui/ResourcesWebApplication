using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class EstimationWithConfusionMatrix
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string EstimationWithConfusionMatrixIdentifier { get; set; }
        [Required]
        public string PredictYES { get; set; }
        [Required]
        public string PredictNO { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}