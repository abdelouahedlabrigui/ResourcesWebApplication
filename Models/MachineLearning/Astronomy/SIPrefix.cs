using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning.Astronomy
{
    public class SIPrefix
    {
        public int Id { get; set; }
        [Required]
        public string Name {get;set;}
        [Required]
        public string Prefix {get;set;}
        [Required]
        public string Factor {get;set;}
        [Required]
        public string CreatedAT {get;set;}
    }
}