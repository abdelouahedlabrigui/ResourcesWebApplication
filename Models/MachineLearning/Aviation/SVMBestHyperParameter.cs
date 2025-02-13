using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class SVMBestHyperParameter
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string C { get; set; }
        public string gamma { get; set; }
        public string kernel { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}