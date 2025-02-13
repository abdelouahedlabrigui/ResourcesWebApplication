using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class LoanToValueRatio
    {
        public int Id { get; set; }
        [Required]
        public float LoanAmount { get; set; }
        [Required]
        public float ValueOfCollateral { get; set; }
        [Required]
        public float Value { get; set; }
    }
}