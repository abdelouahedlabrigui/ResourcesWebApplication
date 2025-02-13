using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{
    public class Agent
    {
        public int Id { get; set; }
        [Required]        
        public string Build {get;set;}
        [Required]
        public string Major {get;set;}
        [Required]
        public string Minor {get;set;}
        [Required]
        public string OSName {get;set;}
        [Required]
        public string Platform {get;set;}
        [Required]
        public string Uname {get;set;}
        [Required]
        public string OS_Version {get;set;}
        [Required]
        public string DateAdd {get;set;}
        [Required]
        public string Manager {get;set;}
        [Required]
        public string RegisterIP {get;set;}
        [Required]
        public string AgentId {get;set;}
        [Required]
        public string Version {get;set;}
        [Required]
        public string Group {get;set;}
        [Required]
        public string Status {get;set;}
        [Required]
        public string MergedSum {get;set;}
        [Required]
        public string Ip {get;set;}
        [Required]
        public string Node_name {get;set;}
        [Required]
        public string LastKeepAlive {get;set;}
        [Required]
        public string Group_config_status {get;set;}
        [Required]
        public string ConfigSum {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string Status_code {get;set;}
        [Required]
        public string CreatedAT { get; set; }        
    }
}