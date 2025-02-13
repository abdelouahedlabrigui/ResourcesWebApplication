using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Accounting
{
    public class TravailAFaire
    {
        public int Id { get; set; }
        [Required]
        public string NoExercice { get; set; }
        [Required]
        public string Instruction { get; set; }
    }
}