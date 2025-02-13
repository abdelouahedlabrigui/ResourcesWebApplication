using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Requests
{
    public class RequestSuccess
    {
        public int Id { get; set; }
        [Required]
        public string Action { get; set; }
        [Required]
        public string Parameters { get; set; }
        [Required]
        public string Url { get; set; }
        [Required]
        public string Message { get; set; }
    }
}