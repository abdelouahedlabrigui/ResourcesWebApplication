using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Endpoints
{
    public class UrlPOSTMetadata
    {
        public int Id { get; set; }
        [Required]        
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        [Required]
        public string HttpMethod { get; set; }
        [Required]
        public string RouteTemplate { get; set; }
    }
}