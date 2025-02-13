using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.LanguageProcessing.QueryProcessing
{
    public class YearDifference
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Year { get; set; }
        [Required]
        public string Count { get; set; }
        [Required]
        public string Difference { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}