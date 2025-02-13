using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class WeatherSummary
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Order { get; set; }
        public string Summary { get; set; }
        public string Class { get; set; }
        public string Method { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}