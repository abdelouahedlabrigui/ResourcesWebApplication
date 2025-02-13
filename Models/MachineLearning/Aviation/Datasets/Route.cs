using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation.Datasets
{
    public class Route
    {
        public int Id { get; set; }
        public string Airline { get; set; }        // e.g., "AA"
        public int AirlineId { get; set; }         // e.g., 24
        public string SourceAirport { get; set; }  // e.g., "PHL"
        public int SourceAirportId { get; set; }   // e.g., 3752
        public string DestinationAirport { get; set; } // e.g., "GSO"
        public int DestinationAirportId { get; set; }  // e.g., 4008
        public string Codeshare { get; set; }      // e.g., "Y"
        public int Stops { get; set; }             // e.g., 0
        public string Equipment { get; set; }      // e.g., "CRJ E75"        
    }
}