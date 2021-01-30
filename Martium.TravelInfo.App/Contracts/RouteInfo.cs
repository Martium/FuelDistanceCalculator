using System.Collections.Generic;

namespace Martium.TravelInfo.App.Contracts
{
    public class RouteInfo
    {
        public LocationInfo DepartureLocation { get; set; }
        public LocationInfo ArrivalLocation { get; set; }
        public List<Coordinates> Coordinates { get; set; }
        public RouteInfoSummary Summary { get; set; }
    }
}