using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Finance
{
    public class TestResult
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public double ExpectedValue { get; set; }
        public double ActualValue { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}