using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier
{
    public class ExempleSerie
    {
        public int Id { get; set; }
        [Required]
        public string NExemple { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}