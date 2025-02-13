using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops
{
    public class IPsToPing
    {
        public int Id { get; set; }
        [Required]
        public string WazuhServerIP {get;set;}
        [Required]
        public string AgentIP {get;set;}
        [Required]
        public string VmIP {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}