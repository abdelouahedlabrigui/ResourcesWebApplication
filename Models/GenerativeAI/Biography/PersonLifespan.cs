using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class PersonLifespan
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public int Birth { get; set; }
        public int Death { get; set; }
    }
}


