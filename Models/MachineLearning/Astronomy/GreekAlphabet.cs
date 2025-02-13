using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning.Astronomy
{
    public class GreekAlphabet
    {
        public int Id { get; set; }
        [Required]
        public string Latin {get;set;}
        [Required]
        public string Greek {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string CreatedAT {get;set;}
    }
}