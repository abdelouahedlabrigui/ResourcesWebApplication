using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class LinguisticFeatureEndpoint
    {
        public int Id { get; set; }
        [Required]
        public string EndPoint { get; set; }
        [Required]
        public string Feature { get; set; }
    }
}