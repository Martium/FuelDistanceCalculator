using System;

namespace Martium.TravelInfo.App.Contracts
{
    public class RouteInfoSummary
    {
        public string DistanceUnit { get; set; }
        public double TotalDistanceInKm { get; set; }
        public string DurationUnit { get; set; }
        public TimeSpan TotalDuration { get; set; }
    }
}