using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{
    public class ServiceStatus
    {
        public int Id { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public string Service { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}