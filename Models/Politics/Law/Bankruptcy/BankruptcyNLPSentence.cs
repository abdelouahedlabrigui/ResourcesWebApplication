using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Politics.Law.Bankruptcy
{
    public class BankruptcyNLPSentence
    {
        public int Id { get; set; }
        [Required]
        public string Sentence { get; set; }
        [Required]
        public float Positive { get; set; }
        [Required]
        public float Negative { get; set; }
        [Required]
        public float Neutral { get; set; }
        [Required]
        public float Compound { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}