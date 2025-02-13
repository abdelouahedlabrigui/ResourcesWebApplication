using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Cisco
{
    public class IPAddress
    {
        public int Id { get; set; }
        [Required]
        public string LoginID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int InterfaceName { get; set; }
        [Required]
        public string IP { get; set; }
        [Required]
        public string Subnet { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}