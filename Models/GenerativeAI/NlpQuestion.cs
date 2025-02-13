using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.GenerativeAI
{
    public class NlpQuestion
    {
        public int Id { get; set; }
        [Required]
        public string QuestionStr { get; set; }
        [Required]
        public string AnswerStr { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}