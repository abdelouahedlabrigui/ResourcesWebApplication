using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.News
{        
    public class Question
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ArticleTitle { get; set; }
        [Required]
        public string QuestionString { get; set; }
        [Required]
        public string Answer { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}