using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class EntityIdentifier
    {
        public int Id { get; set; }
        [Required]
        public int SentenceID { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public string Kb_ID { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}