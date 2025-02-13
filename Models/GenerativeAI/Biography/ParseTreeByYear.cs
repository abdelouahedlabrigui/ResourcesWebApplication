using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class ParseTreeByYear
    {
        public int Id { get; set; }        
        public string DatasetName { get; set; }
        public string Year { get; set; }
        public string RootHead { get; set; }
        public string Text { get; set; }
        public string Dep { get; set; }
        public string HeadText { get; set; }
        public string HeadPos { get; set; }
        public string Child { get; set; }
        public string CreatedAT { get; set; }

    }
}