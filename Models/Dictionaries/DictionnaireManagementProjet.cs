using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Dictionaries
{
    public class DictionnaireManagementProjet
    {
        public int Id { get; set; }
        [Required]
        public string Definition { get; set; }
    }
}