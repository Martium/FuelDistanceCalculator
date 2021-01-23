using System.Windows.Forms;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;

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
            GMaps.Instance.Mode = AccessMode.ServerOnly; 
            _map.MapProvider = OpenStreetMapProvider.Instance;
            _map.ShowCenter = false;
            _map.DragButton = MouseButtons.Left;
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

        public PointLatLng? GetAddressCoordinates(string fullAddress)
        {
            PointLatLng? coordinates;

            PointLatLng? foundCoordinates = GMapProviders.OpenStreetMap.GetPoint(fullAddress, out GeoCoderStatusCode status);

            if (status == GeoCoderStatusCode.OK && foundCoordinates.HasValue)
            {
                coordinates = foundCoordinates;
            }
            else
            {
                coordinates = null;
            }

            return coordinates;
        }

        public MapRoute GetRoute(PointLatLng departureCoordinates, PointLatLng arrivalCoordinates)
        {
            MapRoute route = null;

            MapRoute routeResponse = OpenStreetMapProvider.Instance.GetRoute(departureCoordinates, arrivalCoordinates, false, false, 14);

            if (routeResponse != null)
            {
                route = routeResponse;
            }

            return route;
        }

        public void CreateMapMarker(PointLatLng coordinates, GMarkerGoogleType type)
        {
            GMapMarker mapMarker = new GMarkerGoogle(coordinates, type);
            GMapOverlay markers = new GMapOverlay("Markers Overlay");

            markers.Markers.Add(mapMarker);

            _map.Overlays.Add(markers);
        }

        public void ShowRoute(MapRoute route)
        {
            var gMapRoute = new GMapRoute(route);
            var routeOverlay = new GMapOverlay("Route Overlay");

            _map.Overlays.Remove(routeOverlay);

            routeOverlay.Routes.Add(gMapRoute);

            _map.Overlays.Add(routeOverlay);
        }
    }
}
