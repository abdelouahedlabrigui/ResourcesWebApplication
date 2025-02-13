using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Politics.Law.Bankruptcy
{
    public class BankruptcyEntity
    {
        public int Id { get; set; }     
        [Required]
        public string Text { get; set; }
        [Required]
        public string StartChat { get; set; }
        [Required]
        public string EndChar { get; set; }
        [Required]
        public string Label { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}