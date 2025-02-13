using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class BalloonBalanceOfLoan
    {
        public int Id { get; set; }
        [Required]
        public float PresentValue { get; set; }
        [Required]
        public float Payment { get; set; }
        [Required]
        public float RatePerPayment { get; set; }
        [Required]
        public float NumberOfPayments { get; set; }
        [Required]
        public float Value { get; set; }
    }
}