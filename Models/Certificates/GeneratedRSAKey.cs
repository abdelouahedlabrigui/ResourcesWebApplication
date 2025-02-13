using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Certificates
{
    public class GeneratedRSAKey
    {
        public int Id { get; set; }
        [Required]
        public string OperationID { get; set; }
        [Required]
        public string KeyString { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}