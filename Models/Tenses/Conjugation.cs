using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Tenses
{
    public class Conjugation
    {
        public int Id { get; set; }
        [Required]
        public string Language { get; set; }
        [Required]
        public string Verb { get; set; }
        [Required]
        public string Tense { get; set; }
        public string Person { get; set; }
        public string VerbForm { get; set; }
    }
}