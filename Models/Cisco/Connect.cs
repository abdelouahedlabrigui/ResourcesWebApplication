using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Cisco
{
    public class Connect
    {
        public int Id { get; set; }
        [Required]
        public string Protocol { get; set; }
        [Required]
        public string Command { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}