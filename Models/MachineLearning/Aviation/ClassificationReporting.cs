using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class ClassificationReporting
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Method { get; set; }
        public string Index { get; set; }
        public string _1 { get; set; }
        public string _2 { get; set; }
        public string _3 { get; set; }
        public string Accuracy { get; set; }
        public string MacroAvg { get; set; }
        public string WeightedAvg { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}