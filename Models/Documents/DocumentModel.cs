using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.Documents
{
    public class DocumentModel
    {
        public string DocumentIDMeta { get; set; }
        public string DocumentTitle { get; set; }
        public string DocumentUrl { get; set; }
        public DateTime DocumentCreatedAT { get; set; }
        public string ReadingID { get; set; }
        public string ReadingDateAT { get; set; }
        public string ReadingStartAT { get; set; }
        public string ReadingEndAT { get; set; }
        public string ReadCreatedAT { get; set; }
        public TimelineDifference TimelineDifferenceCreatedAT { get; set; }
        public TimelineDifference TimelineDifferenceReading { get; set; }
    }
}