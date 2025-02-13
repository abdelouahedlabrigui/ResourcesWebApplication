using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning.Astronomy
{
    public class PhysicalConstant
    {
        public int Id { get; set; }
        [Required]
        public string Symbol {get;set;}
        [Required]
        public string Quantity {get;set;}
        [Required]
        public string Value {get;set;}
        [Required]
        public string CreatedAT {get;set;}

    }
}