using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting.DeterminationDesCoutsEtDesResultatsCasParticulier
{
    public class Instruction
    {
        public int Id { get; set; }
        [Required]
        public string ExempleId { get; set; }
        [Required]
        public string InstructionExemple { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}