using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.News
{
    public class AnswerSentiment
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Sentence { get; set; }
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