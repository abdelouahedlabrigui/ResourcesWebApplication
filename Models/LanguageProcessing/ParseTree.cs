using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class ParseTree
    {
        public int Id { get; set; }        
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Text { get; set; }
        [Required]
        public string Dep { get; set; }
        [Required]
        public string HeadText { get; set; }
        [Required]
        public string HeadPos { get; set; }
        [Required]
        public string Child { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
    public class Child {
        public string ChildString { get; set; }
    }
}