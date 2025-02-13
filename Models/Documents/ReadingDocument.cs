using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class ReadingDocument
    {
        public Document Document { get; set; }
        public Reading Reading { get; set; }
    }
}