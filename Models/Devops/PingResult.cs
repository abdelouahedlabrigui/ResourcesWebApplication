using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ResourcesWebApplication.Models.Devops
{
    public class PingResult
    {
        public int Id { get; set; }
        [Required]
        public string IPAddress { get; set; }
        [Required]
        public string PacketsTransmitted { get; set; }
        [Required]
        public string PacketsReceived { get; set; }
        [Required]
        public string PacketLoss { get; set; }
        [Required]
        public string RoundTripTimes { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string CreatedAT { get; set; }
    }
}