using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Endpoints
{
    public class EndpointMetadata
    {
        public int Id { get; set; }
        [Required]
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        public string HttpMethod { get; set; }
        public string RouteTemplate { get; set; }
    }
}