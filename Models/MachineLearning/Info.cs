using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResourcesWebApplication.Models.MachineLearning
{
    public class Info
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string DatasetName { get; set; }
        [Required]
        public string Information {get;set;}
        [Required]
        public string CreatedAT { get; set; }
    }
}