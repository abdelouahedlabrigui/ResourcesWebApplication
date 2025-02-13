using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Documents
{
    public class QueryData
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Category {get;set;}
        [Required]
        public string Name {get;set;}
        [Required]
        public string LastWriteTime {get;set;}
        [Required]
        public string LastAccessTime {get;set;}
        [Required]
        public string DirectoryName {get;set;}
        [Required]
        public string Length {get;set;}
        [Required]
        public string Query {get;set;}
        [Required]
        public string CreatedAT {get;set;}
    }
}