using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Cisco
{
    public class Port
    {
        public int Id { get; set; }
        [Required]
        public string IPAdressID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Number { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}