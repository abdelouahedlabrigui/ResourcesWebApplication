using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class Pos
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Text_ { get; set; }
        [Required]
        public string Lemma_ { get; set; }
        [Required]
        public string Pos_ { get; set; }
        [Required]
        public string Tag_ { get; set; }
        [Required]
        public string Dep_ { get; set; }
        [Required]
        public string Shape_ { get; set; }
        [Required]
        public string Is_Alpha { get; set; }
        [Required]
        public string Is_Stop { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}