using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Games.TwoPlayers
{
    public class Preference
    {
        public int Id { get; set; }
        [Required]
        public string FthPlayerID { get; set; }
        [Required]
        public string SndPlayerID { get; set; }
        [Required]
        public string FthPlayerPreference { get; set; }
        [Required]
        public string SndPlayerPreference { get; set; }
    }
}