using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.LanguageProcessing
{
    public class EntityRecognition
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Sentence {get;set;}
        [Required]
        public string InformationExtraction {get;set;}
        [Required]
        public string EntityEncoding {get;set;}
        [Required]
        public string PosCountsEncoding {get;set;}
        [Required]
        public string TagCountsEncoding {get;set;}
        [Required]
        public string DepCountsEncoding {get;set;}
        [Required]
        public string ShapeCountsEncoding {get;set;}
        [Required]
        public string AlphaCountsEncoding {get;set;}
        [Required]
        public string StopCountsEncoding {get;set;}
    }
}