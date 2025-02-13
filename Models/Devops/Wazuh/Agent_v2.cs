using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops.Wazuh
{
    public class Agent_v2
    {
        public string AgentId { get; set; }
        public string Manager { get; set; }
        public string OSName { get; set; }
        public string Ip { get; set; }
        public string Version { get; set; }
        public string Status { get; set; }
    }
}