using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Documents
{
    public class Mark
    {
        public int Id { get; set; }
        [Required]
        public string DocumentID { get; set; }
        [Required]
        public string Search { get; set; }
        [Required]
        public string Page { get; set; }
        [Required]
        public string CreatedAT { get; set; }        
    }
}