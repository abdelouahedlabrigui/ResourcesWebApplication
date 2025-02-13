using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class RegressionPlot
    {
        public int Id { get; set; }
        public string Filename {get;set;}
        [Required]
        public string Title {get;set;}
        [Required]
        public string FthColumn {get;set;}
        [Required]
        public string SndColumn {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}