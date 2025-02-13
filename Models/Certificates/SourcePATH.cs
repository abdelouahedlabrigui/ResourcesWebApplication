using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Certificates
{
    public class SourcePATH
    {
        public int Id { get; set; }
        [Required]
        public string Address { get; set; }
    }
}