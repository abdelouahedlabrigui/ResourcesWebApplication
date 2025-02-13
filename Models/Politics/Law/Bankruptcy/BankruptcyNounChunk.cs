using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Politics.Law.Bankruptcy
{
    public class BankruptcyNounChunk
    {
        public int Id { get; set; }
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