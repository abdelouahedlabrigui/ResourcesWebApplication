using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Intercept
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string InterceptIdentifier { get; set; }
        [Required]
        public string MLModelType { get; set; }
        [Required]
        public string MLModelIntercept { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}