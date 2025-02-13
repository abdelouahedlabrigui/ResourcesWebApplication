using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class BestHyperParameter
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        // max_depth  min_samples_leaf  min_samples_split
        public string max_depth { get; set; }
        public string min_samples_leaf { get; set; }
        public string min_samples_split { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}