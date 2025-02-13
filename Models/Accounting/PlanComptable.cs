using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Accounting
{
    public class PlanComptable
    {
        public int Id { get; set; }
        [Required]
        public string Classe {get;set;}
        [Required]
        public string Niv_de_Reg {get;set;}
        [Required]
        public int No_Compte {get;set;}
        [Required]
        public string Libelle {get;set;}
    }
}