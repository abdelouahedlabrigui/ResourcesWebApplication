using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Servers.Users
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password1 { get; set; }
        [Required]
        public string Password2 { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}