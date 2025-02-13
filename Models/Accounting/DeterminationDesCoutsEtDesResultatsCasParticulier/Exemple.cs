using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier
{
    public class Exemple
    {
        public int Id { get; set; }
        [Required]
        public string ExempleId { get; set; }
        [Required]
        public string ObjetId { get; set; }
        [Required]
        public string ConditionId { get; set; }
        [Required]
        public string InstructionId { get; set; }
        [Required]
        public string EnonceeId { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}