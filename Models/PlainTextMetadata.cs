using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models
{
    public class PlainTextMetadata
    {
        public int ID { get; set; }
        public string FullAddress { get; set; }
        public string Name { get; set; }
        public string LastWriteTime { get; set; }
        public string LastAccessTime { get; set; }
        public string DirectoryName { get; set; }
        public string Length { get; set; }
    }
}