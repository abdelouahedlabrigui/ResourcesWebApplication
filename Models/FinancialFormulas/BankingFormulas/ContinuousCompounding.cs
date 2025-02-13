using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.FinancialFormulas.BankingFormulas
{
    public class ContinuousCompounding
    {
        public int Id { get; set; }
        [Required]
        public float Principal { get; set; }
        [Required]
        public float Rate { get; set; }
        [Required]
        public float Time { get; set; }
        [Required]
        public float Value { get; set; }
    }
}