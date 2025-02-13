using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class InitialMetadata
    {
        public int Id { get; set; }
        public string Attributes {get;set;}
        public string BaseName {get;set;}
        public string CreationTime {get;set;}
        public string CreationTimeUtc {get;set;}
        public string Exists {get;set;}
        public string Extension {get;set;}
        public string FullName {get;set;}
        public string LastAccessTime {get;set;}
        public string LastAccessTimeUtc {get;set;}
        public string LastWriteTime {get;set;}
        public string LastWriteTimeUtc {get;set;}
        public string LinkType {get;set;}
        public string Mode { get; set; }
        public string Name { get; set; }
        public string PSChildName { get; set; }
        public string PSIsContainer { get; set; }
        public string PSParentPath { get; set; }
        public string PSPath { get; set; }
    }
}