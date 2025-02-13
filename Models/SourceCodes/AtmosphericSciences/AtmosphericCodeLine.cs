using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.SourceCodes.AtmosphericSciences
{
    public class AtmosphericCodeLine
    {
        public int Id { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CodeLine { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}