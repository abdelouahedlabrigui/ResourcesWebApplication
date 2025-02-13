using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation
{
    public class MeanError
    {
        public int Id { get; set; }
        public string DatasetName { get; set; }
        public string MSE { get; set; }
        public string MAE { get; set; }
        public string BestMSE { get; set; }
        public string BestParams { get; set; }
        public DateTime CreatedAT { get; set; }
    }
}