using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.News
{
    public class MatchWithTagYDepSentences
    {
        [Required]
        public string Match { get; set; }
        [Required]
        public string PosTag { get; set; }
        [Required]
        public string DepTag { get; set; }
        [Required]
        public string Total { get; set; }
    }
}