using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Shape
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string DatasetType { get; set; }
        [Required]
        public string DatasetShape { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}