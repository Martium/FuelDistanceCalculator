using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Martium.TravelInfo.App.Models;
using Coordinates = Martium.TravelInfo.App.Contracts.Coordinates;

namespace Martium.TravelInfo.App.Services
{
    public class MapService
    {
        private readonly GMapControl _map;

        public MapService(GMapControl map)
        {
            _map = map;
        }
        public void InitializeMap()
        {
            FixMapForOlderWindowsVersion();

            GMaps.Instance.Mode = AccessMode.ServerOnly; 
            _map.MapProvider = OpenStreetMapProvider.Instance;
            _map.ShowCenter = false;
            _map.DragButton = MouseButtons.Left;
        }

        private static void FixMapForOlderWindowsVersion()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        }

        public void SetMapPositionByAddress(string address, double zoomLevel = 14) 
        { 
            _map.Zoom = zoomLevel;
            _map.SetPositionByKeywords(address);
        }

        public void ClearAllRoutesAndMarks()
        {
            _map.Overlays.Clear();
        }

        public void CreateMapMarker(Coordinates coordinates, MapMarkerType type)
        {
            var latLng = new PointLatLng(coordinates.Latitude, coordinates.Longitude);

            GMarkerGoogleType markerType = type == MapMarkerType.DepartureAddress 
                ? GMarkerGoogleType.red 
                : GMarkerGoogleType.green;

            GMapMarker mapMarker = new GMarkerGoogle(latLng, markerType);
            GMapOverlay markers = new GMapOverlay("Markers Overlay");

            markers.Markers.Add(mapMarker);

            _map.Overlays.Add(markers);
        }

        public void DrawRoute(List<Coordinates> routePathCoordinates)
        {
            IEnumerable<PointLatLng> mappedCoordinates =
                routePathCoordinates.Select(c => new PointLatLng { Lat = c.Latitude, Lng = c.Longitude });

            var routeFromBingMaps = new MapRoute(mappedCoordinates, "Bing Maps Route");

            var gMapRoute = new GMapRoute(routeFromBingMaps);
            var routeOverlay = new GMapOverlay("Route Overlay");

            _map.Overlays.Remove(routeOverlay);

            routeOverlay.Routes.Add(gMapRoute);

            _map.Overlays.Add(routeOverlay);
        }
    }
}
