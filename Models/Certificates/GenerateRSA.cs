using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Certificates
{
    public class GenerateRSA
    {
        public int Id { get; set; }
        [Required]
        public string KeySizeString { get; set; }
        [Required]
        public string Address { get; set; }
    }
}