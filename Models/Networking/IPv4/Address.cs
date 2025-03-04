using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Networking.IPv4
{
    public class Address
    {
        public int Id { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}