using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class FeatureImportance
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string Feature { get; set; }  
        public string Importance { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}