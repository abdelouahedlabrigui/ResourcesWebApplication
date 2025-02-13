using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Library.GamePlays.Models
{
    public class GameDualPlayers
    {
        [Required]
        public string FthPlayer { get; set; }
        [Required]
        public string SndPlayer { get; set; }
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
        [Required]
        public string FthUtility { get; set; }
        [Required]
        public string SndUtility { get; set; }
    }
}