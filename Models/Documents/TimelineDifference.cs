using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class TimelineDifference
    {
        public long Ticks { get; set; }
        public double Days { get; set; }
        public double Hours { get; set; }
        public double Milliseconds { get; set; }
        public double Minutes { get; set; }
        public double Seconds { get; set; }
        public double TotalDays { get; set; }
        public double TotalHours { get; set; }
        public double TotalMilliseconds { get; set; }
        public double TotalMinutes { get; set; }
        public double TotalSeconds { get; set; }
    }
}