using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.Travaux
{
    public class TravauxAFaire
    {
        public int Id { get; set; }
        [Required]
        public string Chapitre { get; set; }
        [Required]
        public string ExempleName { get; set; }
        [Required]
        public string AFaire { get; set; }
    }
}