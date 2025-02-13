using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ResourcesWebApplication.Models.Games.TwoPlayers;

namespace ResourcesWebApplication.Models.Games.NestedPlayers
{
    public class TwoPlayersGame
    {
        public Player Player { get; set; }
        public Preference Preference { get; set; }
        public Rule Rule { get; set; }
        public Utility Utility { get; set; }
    }
}