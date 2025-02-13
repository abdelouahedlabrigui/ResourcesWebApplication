using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ResourcesWebApplication.Models.MachineLearning.Visualizations
{
    public class PlotByColumn
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Column { get; set; }
        [Required]
        public string Encoding { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}