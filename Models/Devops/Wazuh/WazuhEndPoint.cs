using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Devops.Wazuh
{
    public class WazuhEndPoint
    {
        public int Id { get; set; }
        [Required]
        public string EndPoint { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}