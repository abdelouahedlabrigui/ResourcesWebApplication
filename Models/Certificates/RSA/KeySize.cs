using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Certificates.RSA
{
    public class KeySize
    {
        public int Id { get; set; }
        [Required]
        public string Size { get; set; }
    }
}