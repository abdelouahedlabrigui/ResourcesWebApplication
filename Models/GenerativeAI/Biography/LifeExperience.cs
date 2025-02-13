using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.GenerativeAI.Biography
{
    public class LifeExperience
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Year { get; set; }
        public string Happened { get; set; }
    }
}