using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Exceptions
{
    public class LogException
    {
        public int Id { get; set; }
        [Required]
        public string ControllerName { get; set; }
        [Required]
        public string ActionName { get; set; }
        [Required]
        public string ExceptionName { get; set; }
        [Required]
        public string ExceptionMessage { get; set; }
        [Required]
        public string LoggedAT { get; set; }
    }
}