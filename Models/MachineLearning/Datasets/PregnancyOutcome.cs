using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Datasets
{
    public class PregnancyOutcome
    {
        public int Id { get; set; }
        [Required]
        public string States { get; set; }
        [Required]
        public string Districts { get; set; }
        [Required]
        public string AnyPregnancyComplication { get; set; }
        [Required]
        public string AnyDeliveryComplication { get; set; }
        [Required]
        public string AnyPostDeliveryComplication { get; set; }
        [Required]
        public string ProblemOfVaginalDischargeDuringLastThreeMonths { get; set; }
        [Required]
        public string MenstrualRelatedProblemsDuringLastThreeMonths { get; set; }
    }
}