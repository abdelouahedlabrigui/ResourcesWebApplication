using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Correlation
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string AvgAreaIncome {get;set;}
        [Required]
        public string AvgAreaHouseAge {get;set;}
        [Required]
        public string AvgAreaNumberofRooms {get;set;}
        [Required]
        public string AvgAreaNumberofBedrooms {get;set;}
        [Required]
        public string AreaPopulation {get;set;}
        [Required]
        public string Price {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}