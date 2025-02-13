using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier
{
    public class Condition
    {
        public int Id { get; set; }
        [Required]
        public string ExempleId { get; set; }
        [Required]
        public string Enoncee { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}