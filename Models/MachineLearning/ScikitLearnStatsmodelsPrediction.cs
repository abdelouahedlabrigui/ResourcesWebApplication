using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class ScikitLearnStatsmodelsPrediction
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string ScikitLearnStatsmodelsPredictionIdentifier { get; set; }
        [Required]
        public string ScikitLearnProbability { get; set; }
        [Required]
        public string StatsmodelsProbability { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}