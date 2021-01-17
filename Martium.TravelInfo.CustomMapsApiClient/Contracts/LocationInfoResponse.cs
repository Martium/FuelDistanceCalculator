using System.Collections.Generic;

namespace Martium.TravelInfo.CustomMapsApiClient.Contracts
{
    public class LocationInfoResponse
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public List<string> ErrorDetails { get; set; }
        public Location Location { get; set; }
    }
}
