using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Servers.Users
{
    public class Hash
    {
        public int Id { get; set; }
        [Required]
        public string Value { get; set; }
        [Required]
        public string Hashed { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}