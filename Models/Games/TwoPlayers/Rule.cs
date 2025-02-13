using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Games.TwoPlayers
{
    public class Rule
    {
        public int Id { get; set; }
        [Required]
        public string Sum { get; set; }
        [Required]
        public string Choice { get; set; }
    }
}