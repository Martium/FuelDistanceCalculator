using System.Windows.Forms;
using GMap.NET.MapProviders;

namespace Martium.TravelInfo.Forms
{
    public partial class TravelInfoForm : Form
    {
        public TravelInfoForm()
        {
            InitializeComponent();

            InitializeControls();

            InitializeMap();
        }

        private void InitializeControls()
        {
            TripPriceTextBox.Enabled = false;
            DistanceTextBox.Enabled = false;
            CalculateButton.Enabled = false;
        }

        private void InitializeMap()
        {
            Map.MapProvider = GMap.NET.MapProviders.BingMapProvider.Instance;
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            Map.MapProvider = OpenCycleLandscapeMapProvider.Instance;
            Map.SetPositionByKeywords("Kaunas, Lithuania");
            Map.ShowCenter = false;
            //Map.DragButton = MouseButtons.Left; Drag map option 
        }
    }
}
