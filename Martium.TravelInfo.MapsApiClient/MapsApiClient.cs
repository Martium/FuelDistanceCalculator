using System;
using System.Collections.Generic;
using System.Linq;
using Martium.TravelInfo.MapsApiClient.Contracts;
using Martium.TravelInfo.MapsApiClient.Contracts.Internal;
using RestSharp;

namespace Martium.TravelInfo.MapsApiClient
{
    public class MapsApiClient
    {
        private const string BingMapsApiBaseUrl = "http://dev.virtualearth.net";
        private const string DrivingRouteApiResource = "REST/V1/Routes/Driving";
        private const string ApiKeyValidityCheckAddress = "Kaunas, Lietuva";

        private readonly string _bingMapsApiKey;

        public MapsApiClient()
        {
            _bingMapsApiKey = "";  // TODO: get API key from database settings when we will have it in database
        }

        public bool ValidateBingMapsApiKey(string apiKey)
        {
            BingMapsApiRouteResponse apiResponse = GetRouteData(ApiKeyValidityCheckAddress, ApiKeyValidityCheckAddress, apiKey);

            return apiResponse.StatusCode == 200;
        }

        public LocationInfo GetLocationInfo(string address)
        {
            LocationInfo response = null;

            BingMapsApiRouteResponse apiResponse = GetRouteData(address, address);

            if (apiResponse.StatusCode == 200 && apiResponse.ResourceSets.Any() && apiResponse.ResourceSets.First().Resources.Any())
            {
                RouteLeg locationRouteLeg = apiResponse.ResourceSets.First().Resources.First().RouteLegs.First();

                LocationInfo mappedResponse = MapLocation(locationRouteLeg.StartLocation);

                if (!(mappedResponse.Address.Locality == null && mappedResponse.Address.AddressLine == null 
                    && mappedResponse.Address.PostalCode == null && mappedResponse.Address.AdminDistrict == null))
                {
                    response = mappedResponse;
                }
            }

            return response;
        }

        public RouteInfo GetRouteInfo(string departureAddress, string arrivalAddress)
        {
            RouteInfo response = null;

            BingMapsApiRouteResponse apiResponse = GetRouteData(departureAddress, arrivalAddress);

            if (apiResponse.StatusCode == 200 && apiResponse.ResourceSets.Any() && apiResponse.ResourceSets.First().Resources.Any())
            {
                Resource routeResource = apiResponse.ResourceSets.First().Resources.First();
                RouteLeg routeLeg = routeResource.RouteLegs.First();

                response = new RouteInfo
                {
                    DepartureLocation = MapLocation(routeLeg.StartLocation),
                    ArrivalLocation = MapLocation(routeLeg.EndLocation),
                    Coordinates = MapRouteCoordinates(routeResource.RoutePath),
                    Summary = new RouteInfoSummary
                    {
                        DistanceUnit = routeResource.DistanceUnit,
                        TotalDistanceInKm = routeResource.TravelDistance,
                        DurationUnit = routeResource.DurationUnit,
                        TotalDuration = TimeSpan.FromSeconds(routeResource.TravelDuration)
                    },
                };
            }

            return response;
        }

        private BingMapsApiRouteResponse GetRouteData(string departureAddress, string arrivalAddress, string customApiKey = null)
        {
            BingMapsApiRouteResponse response;

            var restClient = new RestClient(BingMapsApiBaseUrl);

            var request = new RestRequest(Method.GET)
            {
                Resource = DrivingRouteApiResource
            };

            request.AddParameter("wp.0", departureAddress, ParameterType.QueryString);
            request.AddParameter("wp.1", arrivalAddress, ParameterType.QueryString);

            request.AddParameter("routeAttributes", "routePath", ParameterType.QueryString);

            string apiKey = string.IsNullOrWhiteSpace(customApiKey) ? _bingMapsApiKey : customApiKey;

            request.AddParameter("key", apiKey, ParameterType.QueryString);

            var apiResponse = restClient.Execute<BingMapsApiRouteResponse>(request);

            if (apiResponse.IsSuccessful && 
                (apiResponse.Data == null 
                || apiResponse.Data != null && !apiResponse.Data.ResourceSets.Any() && !apiResponse.Data.ResourceSets.First().Resources.Any()))
            {
                response = new BingMapsApiRouteResponse
                {
                    StatusCode = 500,
                    ErrorDetails = new List<string>
                    {
                        "Response data was not returned from API or returned data is invalid!"
                    }
                };
            }
            else
            {
                response = apiResponse.Data;
            }

            return response;
        }

        private LocationInfo MapLocation(RouteLegStartLocation location)
        {
            List<List<double>> locationCoordinates = location.Point.Coordinates;

            return new LocationInfo
            {
                Address = new AddressInfo
                {
                    AddressLine = location.Address.AddressLine,
                    AdminDistrict = location.Address.AdminDistrict,
                    CountryRegion = location.Address.CountryRegion,
                    FormattedAddress = location.Address.FormattedAddress,
                    Locality = location.Address.Locality,
                    PostalCode = location.Address.PostalCode
                },
                Coordinates = new Coordinates
                {
                    Latitude = locationCoordinates.First().First(),
                    Longitude = locationCoordinates.Last().First()
                }
            };
        }

        private List<Coordinates> MapRouteCoordinates(RoutePath routePath)
        {
            var mappedCoordinates = new List<Coordinates>();

            List<List<double>> unmappedRouteCoordinates = routePath.Line.Coordinates;

            foreach (List<double> unmappedCoordinates in unmappedRouteCoordinates)
            {
                mappedCoordinates.Add(new Coordinates
                {
                     Latitude = unmappedCoordinates.First(),
                     Longitude = unmappedCoordinates.Last()
                });
            }

            return mappedCoordinates;
        }
    }
}
