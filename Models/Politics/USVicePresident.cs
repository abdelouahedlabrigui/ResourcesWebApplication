using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Politics
{
    public class USVicePresident
    {
        public int Id { get; set; }
        public string PresidentOfTheUnitedStates {get;set;}
        public string PoliticalPartyOfThePresident {get;set;}
        public string StartYear {get;set;}
        public string EndYear {get;set;}
        public string VicePresidentOfTheUnitedStates {get;set;}
    }
}