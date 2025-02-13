using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Weather
{
    public class WeatherCorrelation
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Column { get; set; }
        [Required]
        public string record_precipitation { get; set; }
        [Required]
        public string average_precipitation { get; set; }
        [Required]
        public string actual_precipitation { get; set; }
        [Required]
        public string record_max_temp_year { get; set; }
        [Required]
        public string record_min_temp_year { get; set; }
        [Required]
        public string record_max_temp { get; set; }
        [Required]
        public string record_min_temp { get; set; }
        [Required]
        public string average_max_temp { get; set; }
        [Required]
        public string average_min_temp { get; set; }
        [Required]
        public string actual_max_temp { get; set; }
        [Required]
        public string actual_min_temp { get; set; }
        [Required]
        public string actual_mean_temp { get; set; }
        [Required]
        public string date { get; set; }
    }
}