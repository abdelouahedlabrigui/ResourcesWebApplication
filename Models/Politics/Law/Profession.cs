using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Politics.Law
{
    public class Profession
    {
        public int Id { get; set; }
        [Required]
        public string Actor { get; set; }
        [Required]
        public string Term { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}