using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Commands
{
    public class NgComponent
    {
        public int Id { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}