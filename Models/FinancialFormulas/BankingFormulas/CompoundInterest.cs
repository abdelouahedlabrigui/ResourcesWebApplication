using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class CompoundInterest
    {
        public int Id { get; set; }
        [Required]
        public float Principal { get; set; }
        [Required]
        public float RatePerPeriod { get; set; }
        [Required]
        public float NumberOfPeriods { get; set; }
        [Required]
        public float Value { get; set; }
    }
}