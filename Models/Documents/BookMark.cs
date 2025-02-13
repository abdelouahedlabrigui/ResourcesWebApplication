using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class BookMark
    {
        public Document Document { get; set; }
        public Mark Mark { get; set; }
    }
}