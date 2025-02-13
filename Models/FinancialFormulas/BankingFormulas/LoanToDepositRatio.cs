using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class LoanToDepositRatio
    {
        public int Id { get; set; }
        [Required]
        public float Loans { get; set; }
        [Required]
        public float Deposits { get; set; }
        [Required]
        public float Value { get; set; }
    }
}