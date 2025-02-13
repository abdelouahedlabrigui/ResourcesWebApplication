using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class PlotMetadata
    {
        public int Id { get; set; }  
        [Required]      
        public string Title {get;set;} 
        [Required]       
        public string Xlabel {get;set;} 
        [Required]       
        public string Ylabel {get;set;} 
        [Required]       
        public string ColumnsPloted {get;set;} 
        [Required]       
        public string EncodingLength {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}