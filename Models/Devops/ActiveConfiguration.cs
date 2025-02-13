using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{                                                 
    public class ActiveConfiguration
    {
        public int Id { get; set; }
        [Required]
        public string Component { get; set; }
        [Required]
        public string Configuration { get; set; }
        [Required]
        public string Tag { get; set; }
    }
}