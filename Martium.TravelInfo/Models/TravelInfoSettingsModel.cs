using System;

namespace Martium.TravelInfo.Models
{
    public class TravelInfoSettingsModel
    {
        public string DepartureCountry { get; set; }
        public double PricePerKm { get; set; }
        public double AdditionalDistanceInKm { get; set; }
        public string DepartureAddress { get; set; }
    }
}
