using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.News
{
    public class NewsTitleDescription
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Prompted { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}