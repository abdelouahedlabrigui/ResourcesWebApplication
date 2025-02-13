using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class LocalTree
    {
        public int Id { get; set; }        
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Dep { get; set; }
        [Required]
        public string N_Lefts { get; set; }
        [Required]
        public string N_Rights { get; set; }
        [Required]
        public string Ancestors { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}