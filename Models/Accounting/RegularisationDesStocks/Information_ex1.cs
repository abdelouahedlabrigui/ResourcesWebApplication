using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.RegularisationDesStocks
{
    public class Information_ex1
    {
        public int Id { get; set; }
        [Required]
        public string Chapitre { get; set; }
        [Required]
        public string ExempleName { get; set; }
        [Required]
        public string Entreprise { get; set; }
        [Required]
        public string DateAu { get; set; }
        [Required]
        public string Elements { get; set; }
        [Required]
        public string Marchandises { get; set; }
        [Required]
        public string MatieresPremieres { get; set; }
        [Required]
        public string ProduitFinis { get; set; }
    }
}