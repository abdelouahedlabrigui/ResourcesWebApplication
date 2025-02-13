using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class Morphology
    {
        public int Id { get; set; }
        [Required]
        public int SentenceID { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Case { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string Person { get; set; }
        [Required]
        public string PronType { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}