using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class PredictProbabilityDecision
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string PredictProbabilityDecisionIdentifier { get; set; }
        [Required]
        public string MLModelType { get; set; }
        [Required]
        public string ProbOfNO { get; set; }
        [Required]
        public string ProbOfYes { get; set; }
        [Required]
        public string Decision { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}