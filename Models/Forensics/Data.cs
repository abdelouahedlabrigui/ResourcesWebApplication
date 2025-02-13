using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Forensics
{
    public class Data
    {
        public int Id { get; set; }
        [Required]
        public int ArticleID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}