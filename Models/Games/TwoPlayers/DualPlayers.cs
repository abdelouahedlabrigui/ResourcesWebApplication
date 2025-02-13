using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Games
{
    public class DualPlayers
    {
        public int Id { get; set; }
        [Required]
        public string FthPlayerID { get; set; }
        [Required]
        public string SndPlayerID { get; set; }
        [Required]
        public string FthStrategy { get; set; }
        [Required]
        public string SndStrategy { get; set; }
        [Required]
        public string Sum { get; set; }
        [Required]
        public string FthPreference { get; set; }
        [Required]
        public string SndPreference { get; set; }
        public string FthUtility { get; set; }
        public string SndUtility { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}