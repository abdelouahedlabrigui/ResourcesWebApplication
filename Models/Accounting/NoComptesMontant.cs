using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Accounting
{
    public class NoComptesMontant
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Compte { get; set; }
        public string Montant { get; set; }
    }
}