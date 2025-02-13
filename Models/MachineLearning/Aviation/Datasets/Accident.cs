using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResourcesWebApplication.Models.MachineLearning.Aviation.Datasets
{
    public class Accident
    {
        public int Id { get; set; }
        public string ReferenceNumber { get; set; }
        public int GridRefEasting { get; set; }
        public int GridRefNorthing { get; set; }
        public int NumberOfVehicles { get; set; }
        public DateTime AccidentDate { get; set; }
        public string Time24hr { get; set; }
        public int FirstRoadClass { get; set; }
        public string FirstRoadClassAndNo { get; set; }
        public string RoadSurface { get; set; }
        public int LightingConditions { get; set; }
        public int WeatherConditions { get; set; }
        public string LocalAuthority { get; set; }
        public string VehicleReferenceNumber { get; set; }
        public int VehicleNumber { get; set; }
        public string TypeOfVehicle { get; set; }
        public string CasualtyReferenceNumber { get; set; }
        public int CasualtyVehicleNumber { get; set; }
        public int CasualtyClass { get; set; }
        public int CasualtySeverity { get; set; }
        public int SexOfCasualty { get; set; }
        public int AgeOfCasualty { get; set; }
    }
}