using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class LinearRegression
    {
        public int Id { get; set; }
        [Required]
        public string coefficientEndPoint {get;set;}
        [Required]
        public string pairplotEndPoint {get;set;}
        [Required]
        public string plotByColumnEndPoint {get;set;}
        [Required]
        public string filename {get;set;}
        [Required]
        public string datasetName {get;set;}
        [Required]
        public string xlabel {get;set;}
        [Required]
        public string ylabel {get;set;}
        [Required]
        public string title {get;set;}
    }
}