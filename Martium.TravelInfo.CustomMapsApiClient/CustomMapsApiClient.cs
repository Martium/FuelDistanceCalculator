using System;
using System.Collections.Generic;
using System.Linq;
using Martium.TravelInfo.CustomMapsApiClient.Contracts;
using Martium.TravelInfo.CustomMapsApiClient.Contracts.Internal;
using RestSharp;

namespace Martium.TravelInfo.CustomMapsApiClient
{
    public class CustomMapsApiClient
    {
        private const string BingMapsApiBaseUrl = "http://dev.virtualearth.net";
        private const string DrivingRouteApiResource = "REST/V1/Routes/Driving";
        private const string ApiKeyValidityCheckAddress = "Kaunas, Lietuva";

        private readonly string _bingMapsApiKey;

        public CustomMapsApiClient()
        {
            _bingMapsApiKey = "Ny7pJAEQhVWfxZYqRlVP~yFZ9k0nv3HnF4SjZOc7ubQ~As5o9EzYCXJxBaXFC5eIDJyMAjd0HnuqtFfgSbkj1z45ghgMhIGKHD4qRZ5CunU7";  // TODO: get API key from database settings when we will have it in database
        }

        public bool ValidateBingMapsApiKey(string apiKey)
        {
            BingMapsApiRouteResponse apiResponse = GetRouteData(ApiKeyValidityCheckAddress, ApiKeyValidityCheckAddress, apiKey);

            return apiResponse.StatusCode == 200;
        }

        public LocationInfoResponse GetLocationInfo(string address)
        {
            var response = new LocationInfoResponse();

            BingMapsApiRouteResponse apiResponse = GetRouteData(address, address);

            response.StatusCode = apiResponse.StatusCode;
            response.StatusDescription = apiResponse.StatusDescription;
            response.ErrorDetails = apiResponse.ErrorDetails;

            if (apiResponse.StatusCode == 200)
            {
                RouteLeg locationRouteLeg = apiResponse.ResourceSets.First().Resources.First().RouteLegs.First();

                response.Location = MapLocation(locationRouteLeg.StartLocation);
            }

            return response;
        }

        public RouteInfoResponse GetRouteInfo(string departureAddress, string arrivalAddress)
        {
            var response = new RouteInfoResponse();

            BingMapsApiRouteResponse apiResponse = GetRouteData(departureAddress, arrivalAddress);

            response.StatusCode = apiResponse.StatusCode;
            response.StatusDescription = apiResponse.StatusDescription;
            response.ErrorDetails = apiResponse.ErrorDetails;

            if (apiResponse.StatusCode == 200)
            {
                Resource routeResource = apiResponse.ResourceSets.First().Resources.First();
                RouteLeg routeLeg = routeResource.RouteLegs.First();

                response.Route = new RouteInfo
                {
                    DepartureLocation = MapLocation(routeLeg.StartLocation),
                    ArrivalLocation = MapLocation(routeLeg.EndLocation),
                    RouteCoordinates = MapRouteCoordinates(routeResource.RoutePath),
                    RouteSummary = new RouteInfoSummary
                    {
                        DistanceUnit = routeResource.DistanceUnit,
                        TotalDistanceInKm = routeResource.TravelDistance,
                        DurationUnit = routeResource.DurationUnit,
                        TotalDuration = TimeSpan.FromSeconds(routeResource.TravelDuration)
                    }
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

            if (apiResponse.Data == null)
            {
                response = new BingMapsApiRouteResponse
                {
                    StatusCode = 500,
                    ErrorDetails = new List<string>
                    {
                        "Response data was not returned from API!"
                    }
                };
            }
            else
            {
                response = apiResponse.Data;
            }

            return response;
        }

        private Location MapLocation(RouteLegStartLocation location)
        {
            List<List<double>> locationCoordinates = location.Point.Coordinates;

            return new Location
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
