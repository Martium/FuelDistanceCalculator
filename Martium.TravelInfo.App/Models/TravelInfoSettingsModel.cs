namespace Martium.TravelInfo.App.Models
{
    public class TravelInfoSettingsModel
    {
        public string DepartureCountry { get; set; }
        public decimal PricePerKm { get; set; }
        public decimal AdditionalDistanceInKm { get; set; }
        public string DepartureAddress { get; set; }
    }
}
