using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Games.TwoPlayers
{
    public class PlayersDataset
    {
        public int Id { get; set; }
        public string FthPlayer { get; set; }
        public string SndPlayer { get; set; }
        public string FthStrategy { get; set; }
        public string SndStrategy { get; set; }
        public string Sum { get; set; }
        public string FthPreference { get; set; }
        public string SndPreference { get; set; }
        
    }
}