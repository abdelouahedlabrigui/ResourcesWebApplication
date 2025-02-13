using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Commands
{
    public class Container
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Command { get; set; }
        [Required]
        public string Service { get; set; }
        [RegularExpression(@"^\d+\s(seconds?|minutes?|hours?)\sago$")]
        public string Created { get; set; }

        [RegularExpression(@"^Up\s\d+\s(seconds?|minutes?|hours?)$")]
        public string Status { get; set; }

        [RegularExpression(@"^(?:(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}|::):\d+->\d+\/(tcp|udp)|(?:\d{1,5}|::):\d+\/(tcp|udp))(?:,\s(?:(\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}|::):\d+->\d+\/(tcp|udp)|(?:\d{1,5}|::):\d+\/(tcp|udp)))*$")]
        public string Ports { get; set; }
    }
}