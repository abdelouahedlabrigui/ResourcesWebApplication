using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Mean
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName {get;set;}
        [Required]
        public string MeanAbsoluteError {get;set;}
        [Required]
        public string MeanSquaredError {get;set;}
        [Required]
        public string RootMeanSquaredError {get;set;}
        [Required]        
        public string CreatedAT {get;set;}
    }
}