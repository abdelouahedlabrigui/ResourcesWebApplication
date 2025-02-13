using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents.Summary
{
    public class Section
    {
        public int Id { get; set;}
        [Required]
        public string Sections { get; set; }
    }
}