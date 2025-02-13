using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class ColumnDescription
    {
        public int Id { get; set; }
        
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Column { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}