using System.Collections.Generic;

namespace Martium.TravelInfo.CustomMapsApiClient.Contracts
{
    public class RouteInfoResponse
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public List<string> ErrorDetails { get; set; }
        public RouteInfo Route { get; set; }

    }

    public class RouteInfo
    {
        public Location DepartureLocation { get; set; }
        public Location ArrivalLocation { get; set; }
        public List<Coordinates> RouteCoordinates { get; set; }
        public RouteInfoSummary RouteSummary { get; set; }
    }
}
