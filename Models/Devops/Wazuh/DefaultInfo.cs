using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops.Wazuh
{
    public class DefaultInfo
    {
        public int Id { get; set; }
        [Required]
        public string Title {get;set;}
        [Required]
        public string Api_version {get;set;}
        [Required]
        public string Revision {get;set;}
        [Required]
        public string License_name {get;set;}
        [Required]
        public string License_url {get;set;}
        [Required]
        public string Hostname {get;set;}
        [Required]
        public string Timestamp {get;set;}
    }
}