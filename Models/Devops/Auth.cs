using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{
    public class Auth
    {
        public int Id { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public string JwtToken { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}