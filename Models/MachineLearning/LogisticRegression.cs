using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class LogisticRegression
    {
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string AgeValues { get; set; }
        [Required]
        public string DropColumn { get; set; }
        [Required]
        public string DropColumns { get; set; }
        [Required]
        public string DummyColumns { get; set; }
        [Required]
        public string TargetColumn { get; set; }
        [Required]
        public string AgePClassColumns { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}