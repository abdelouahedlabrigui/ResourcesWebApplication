using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models
{
    public class Plaintext
    {
        public int ID { get; set; }        
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}