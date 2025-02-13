using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Trees
{
    public class Parent
    {
        public int Id { get; set; }
        public string Attributes { get; set; }
        public string CreationTime { get; set; }
        public string CreationTimeUtc { get; set; }
        public string Exists { get; set; }
        public string Extension { get; set; }
        public string FullName { get; set; }
        public string LastAccessTime { get; set; }
        public string LastAccessTimeUtc { get; set; }
        public string LastWriteTime { get; set; }
        public string LastWriteTimeUtc { get; set; }
        public string Name { get; set; }
        public string ParentParameter { get; set; }
        public string Root { get; set; }
    }
}