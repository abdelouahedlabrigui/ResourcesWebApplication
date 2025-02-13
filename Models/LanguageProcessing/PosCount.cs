using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class PosCount
    {
        [Required]
        public string DatasetName {get;set;}
        [Required]
        public string Feature {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string Count {get;set;}
        [Required]
        public string Sentence { get; set; }
        [Required]
        public string CreatedAT {get;set;}
    }
}