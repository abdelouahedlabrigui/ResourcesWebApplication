using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class NounChunk
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string RootText { get; set; }
        [Required]
        public string RootDep { get; set; }
        [Required]
        public string RootHead { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}