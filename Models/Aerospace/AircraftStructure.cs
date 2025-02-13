using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Aerospace
{
    public class AircraftStructure
    {
        public int Id { get; set; }
        [Required]
        public string Concept { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}