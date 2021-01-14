using System;

namespace Martium.TravelInfo.Models
{
    public class TravelInfoSettingsModel
    {
        public string Country { get; set; }
        public string DepartureAddress { get; set; }
        public double FuelPrice { get; set; }
        public double AdditionalKm { get; set; }
    }
}
