using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Servers.Backups
{
    public class Backup
    {
        public int Id { get; set; }
        [Required]
        public string Folder { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}