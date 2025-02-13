using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Cisco
{
    public class ConfigureIPs
    {
        public List<Login> Login { get; set; }
        public List<Port> Port { get; set; }
        public List<IPAddress> IPAddress { get; set; }
    }
}