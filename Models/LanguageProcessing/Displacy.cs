using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class Displacy
    {
        public int Id { get; set; }
        [Required]
        public string SentenceID { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string ImageEncoding { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}