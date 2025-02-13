using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing.Visualization
{
    public class SentimentPlot
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Sentence { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Encoding { get; set; }
        [Required]
        public string Positive { get; set; }
        [Required]
        public string Negative { get; set; }
        [Required]
        public string Neutral { get; set; }
        [Required]
        public string Compound { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}