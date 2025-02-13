using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops
{
    public class LinuxAgent
    {
        public int Id { get; set; }
        [Required]
        public string Arch { get; set;}
        [Required]
        public string Codename { get; set;}
        [Required]
        public string Major { get; set;}
        [Required]
        public string Minor { get; set;}
        [Required]
        public string OSName { get; set;}
        [Required]
        public string Platform { get; set;}
        [Required]
        public string Uname { get; set;}
        [Required]
        public string OSVersion { get; set;}
        [Required]
        public string AgentId { get; set;}
        [Required]
        public string Manager { get; set;}
        [Required]
        public string Node_name { get; set;}
        [Required]
        public string MergedSum { get; set;}
        [Required]
        public string Status_code { get; set;}
        [Required]
        public string Group { get; set;}
        [Required]
        public string Name { get; set;}
        [Required]
        public string Group_config_status { get; set;}
        [Required]
        public string Status { get; set;}
        [Required]
        public string Version { get; set;}
        [Required]
        public string Ip { get; set;}
        [Required]
        public string ConfigSum { get; set;}
        [Required]
        public string LastKeepAlive { get; set;}
        [Required]
        public string DateAdd { get; set;}
        [Required]
        public string RegisterIP { get; set;}

    }
}