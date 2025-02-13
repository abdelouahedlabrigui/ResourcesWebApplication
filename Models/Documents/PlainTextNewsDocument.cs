using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class PlainTextNewsDocument
    {
        public int Id { get; set; }
        [Required]
        public string Topic { get; set; }
        [Required]
        public string FullAddress { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastWriteTime { get; set; }
        [Required]
        public string LastAccessTime { get; set; }
        [Required]
        public string DirectoryName { get; set; }
        [Required]
        public string Length { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}