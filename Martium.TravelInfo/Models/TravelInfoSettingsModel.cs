using System;

namespace Martium.TravelInfo.Models
{
    public class TravelInfoSettingsModel
    {
        public string DepartureCountry { get; set; }
        public string DepartureAddress { get; set; }
        public double AdditionalDistanceInKm { get; set; }
        public double AdditionalKm { get; set; }
    }
}
