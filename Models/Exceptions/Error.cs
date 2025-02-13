using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Exceptions
{
    public class Error
    {
        public int Id {get;set;}
        [Required]
        public string Language {get;set;}
        [Required]
        public string Message {get;set;}
        [Required]
        public string CreatedAT {get;set;}
    }
}