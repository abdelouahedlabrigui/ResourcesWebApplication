using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.News
{
    public class LemmaBySpeechParts
    {
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Lemma_ { get; set; }
        [Required]
        public string Pos_ { get; set; }
        [Required]
        public int DuplicateVerbsCount { get; set; }
    }
}