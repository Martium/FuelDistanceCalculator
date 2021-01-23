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

        public void SetMapPositionByAddress(string address, double zoomLevel = 14) 
        { 
            _map.Zoom = zoomLevel;
            _map.SetPositionByKeywords(address);
        }

        public void ClearAllRoutesAndMarks()
        {
            _map.Overlays.Clear();
        }

        public MapRoute GetRoute(PointLatLng departureCoordinates, PointLatLng arrivalCoordinates)
        {
            MapRoute route;

            var getRoute = OpenStreetMapProvider.Instance.GetRoute(departureCoordinates, arrivalCoordinates, false, false, 14);

            if (getRoute != null)
            {
                route = getRoute;
            }
            else
            {
                route = null;
            }

            return route;
        }

        public void ShowRoutes(MapRoute route)
        {
            var r = new GMapRoute(route);
            var routes = new GMapOverlay("My Route");
            routes.Routes.Add(r);
            _map.Overlays.Add(routes);
        }

        public void CreateMapMarker(PointLatLng departureLatLng, GMarkerGoogleType type)
        {
            GMapMarker mapMarker = new GMarkerGoogle(departureLatLng, type);

            GMapOverlay markers = new GMapOverlay("Markers");
            markers.Markers.Add(mapMarker);
            _map.Overlays.Add(markers);
        }

        public string GetFullAddress(TextBox textBox, Label label)
        {
            return $"{textBox.Text}, {label.Text}";
        }
    }
}
