using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Commands
{
    public class Cli
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Command { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}