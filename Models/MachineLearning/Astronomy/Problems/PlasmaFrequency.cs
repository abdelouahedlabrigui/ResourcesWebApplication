using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning.Astronomy.Problems
{
    public class PlasmaFrequency
    {
        public int Id { get; set; }
        [Required]
        public string InterstellarMedium { get; set; }
        [Required]
        public string IonosphericPlasma { get; set; }
        [Required]
        public string PlasmaFrequencyInInterstellarMedium { get; set; }
        [Required]
        public string PlasmaFrequencyInIonosphericPlasma { get; set; }
        [Required]
        public string LowestFrequencyInInterstellarMedium { get; set; }
        [Required]
        public string LowestFrequencyInIonosphericPlasma { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}