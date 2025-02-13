using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Weather
{
    public class Correlation_KSEA
    {
        public int Id { get; set; }
        public string record_precipitation { get; set; }
        public string average_precipitation { get; set; }
        public string actual_precipitation { get; set; }
        public string record_max_temp_year { get; set; }
        public string record_min_temp_year { get; set; }
        public string record_max_temp { get; set; }
        public string record_min_temp { get; set; }
        public string average_max_temp { get; set; }
        public string average_min_temp { get; set; }
        public string actual_max_temp { get; set; }
        public string actual_min_temp { get; set; }
        public string actual_mean_temp { get; set; }
        public string date { get; set; }
    }
}