using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Politics
{
    public class USPresident
    {
        public int Id { get; set; }
        [Required]
        public string PresidentNO { get; set; }
        [Required]
        public string President { get; set; }
        [Required]
        public string Born { get; set; }
        [Required]
        public string StartOfPresidency { get; set; }
        [Required]
        public string EndOfPresidency { get; set; }
        [Required]
        public string PostPresidency { get; set; }
        [Required]
        public string Died { get; set; }
        [Required]
        public string Age { get; set; }
        [Required]
        public string NetWorth { get; set; }
        [Required]
        public string PoliticalParty { get; set; }
    }
}