using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace ResourcesWebApplication.Models.Astrology
{
    public class Zodiac
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string StartDate { get; set; }        
        [Required]
        public string EndDate { get; set; }
    }
}