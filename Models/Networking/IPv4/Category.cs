using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Networking.IPv4
{
    public class Category
    {
        public Guid Id { get; set; }
        [Required]
        public string Content { get; set; }
    }
}