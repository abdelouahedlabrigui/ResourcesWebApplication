using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents.Summary
{
    public class Chapter
    {
        public int Id { get; set;}
        [Required]
        public string Chapters { get; set; }
        
    }
}