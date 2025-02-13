using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class AnnualPercentageYield
    {
        public int Id { get; set; }
        [Required]
        public float StatedAnnualInterestRate { get; set; }
        [Required]
        public float NumberOfTimesCompounded { get; set; }
        [Required]
        public float Value { get; set; }
    }
}