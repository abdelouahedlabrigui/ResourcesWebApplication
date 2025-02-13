using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Endpoints
{
    public class EndPoint
    {
        public int Id { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}