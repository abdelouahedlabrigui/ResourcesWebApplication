using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Secrets
{
    public class Password
    {
        public int Id {get;set;}
        [Required]
        public string Technology {get;set;}
        [Required]
        public string User {get;set;}
        [Required]
        public string PasswordInClear {get;set;}
    }
}