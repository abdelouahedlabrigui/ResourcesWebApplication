using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Requests
{
    public class Request
    {
        
        public int Id { get; set; }
        [Required]
        public string Method { get; set; }
        [Required]
        public string Query { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}