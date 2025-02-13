using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class SimpleInterestRate
    {
        public int Id { get; set; }
        public float Principal { get; set; }
        public float Interest { get; set; }
        public float Time { get; set; }
        public float Value { get; set; }
    }
}