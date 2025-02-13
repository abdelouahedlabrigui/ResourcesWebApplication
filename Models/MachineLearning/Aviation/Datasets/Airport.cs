using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation.Datasets
{
    public class Airport
    {
        public int Id { get; set; }
        public int AirportId { get; set; }          // e.g., 1
        public string Name { get; set; }            // e.g., "Goroka Airport"
        public string City { get; set; }            // e.g., "Goroka"
        public string Country { get; set; }         // e.g., "Papua New Guinea"
        public string IATA { get; set; }            // e.g., "GKA"
        public string ICAO { get; set; }            // e.g., "AYGA"
        public double Latitude { get; set; }        // e.g., -6.0816898345900015
        public double Longitude { get; set; }       // e.g., 145.391998291
        public string Altitude { get; set; }           // e.g., 5282 (in feet)
        public string Timezone { get; set; }           // e.g., 10 (UTC offset)
        public string DST { get; set; }             // e.g., "U" (Daylight Saving Time)
        public string TzDatabaseTimeZone { get; set; } // e.g., "Pacific/Port_Moresby"
        public string Type { get; set; }            // e.g., "airport"
        public string Source { get; set; }          // e.g., "OurAirports"
    }
}