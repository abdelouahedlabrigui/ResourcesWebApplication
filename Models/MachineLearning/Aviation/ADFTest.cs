using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class ADFTest
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string ADFStatistic { get; set; }
        public string PValue { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}