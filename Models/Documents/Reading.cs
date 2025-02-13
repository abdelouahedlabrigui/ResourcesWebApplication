using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class Reading
    {
        public int Id { get; set; }
        public int DocumentID { get; set; }
        public string DateAT { get; set; }
        public string StartAT { get; set; }
        public string EndAT { get; set; }
        public string CreatedAT { get; set; }
    }
}