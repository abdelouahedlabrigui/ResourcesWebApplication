using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class ClassificationReport
    {
        public int Id { get; set; }  
        [Required]
        public string DatasetName { get; set; }   
        [Required]
        public string ClassificationReportIdentifier { get; set; }
        [Required]
        public string Label { get; set; }   
        // [Required]
        public string Precision {get;set;}
        // [Required]
        public string Recall {get;set;}
        [Required]
        public string F1Score {get;set;}
        [Required]
        public string Support {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}