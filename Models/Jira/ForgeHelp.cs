using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ResourcesWebApplication.Models.Jira
{
    public class ForgeHelp
    {
        public int Id { get; set; }
        [Required]
        public string Command { get; set; }
        [Required]
        public string Description { get; set; }
    }
}