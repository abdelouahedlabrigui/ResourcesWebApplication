using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Cisco
{
    public class ConfigureIPsDetails
    {
        public Login Login { get; set; }
        public Port Port { get; set; }
        public IPAddress IPAddress { get; set; }
    }
}