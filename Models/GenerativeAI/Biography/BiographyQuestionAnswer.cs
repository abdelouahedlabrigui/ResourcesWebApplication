using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class BiographyQuestionAnswer
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Person { get; set; }
        public string Prompt { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
    }
}