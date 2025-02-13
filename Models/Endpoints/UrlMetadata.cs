using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Endpoints
{
    public class UrlMetadata
    {
        [Required]        
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        [Required]
        public string HttpMethod { get; set; }
        [Required]
        public List<string> MethodParameterName { get; set; }
        [Required]
        public List<string> MethodParameterType { get; set; }
        [Required]
        public string RouteTemplate { get; set; }
        public UrlMetadata()
        {
            MethodParameterName = new List<string>();
            MethodParameterType = new List<string>();
        }
    }
}