using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.News
{        
    public class LemmaBySpeechPartsCount
    {
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
        public int CountLemma_ { get; set; }
    }
}