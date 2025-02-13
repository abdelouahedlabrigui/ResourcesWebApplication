using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{
    public class ServiceCheck
    {
        public int Id { get; set; }
        [Required]
        public string Server_ip { get; set; }
        [Required]
        public string Agent_ip { get; set;} 
        [Required]
        public string VmIP { get; set;} 
        [Required]
        public string Service { get; set;} 
        [Required]
        public string VmUser { get; set;} 
        [Required]
        public string VmPassword { get; set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}