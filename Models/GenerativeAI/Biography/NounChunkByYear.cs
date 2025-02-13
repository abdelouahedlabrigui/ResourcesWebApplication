using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class NounChunkByYear
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Year { get; set; }
        public string Text { get; set; }
        public string RootText { get; set; }
        public string RootDep { get; set; }
        public string RootHead { get; set; }
    }
}