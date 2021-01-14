using System.Windows.Forms;
using GMap.NET.MapProviders;
using Martium.TravelInfo.Repositories;

namespace Martium.TravelInfo.Forms
{
    public partial class TravelInfoForm : Form
    {
        private readonly TravelInfoRepository _travelInfoRepository;

        public TravelInfoForm()
        {
            InitializeComponent();

            InitializeControls();

            InitializeMap();
        }

        private void MapContributorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MapContributorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.openstreetmap.org");
        }

        private void InitializeControls()
        {
            TripPriceTextBox.Enabled = false;
            DistanceTextBox.Enabled = false;
            DepartureCountryTextBox.Enabled = false;
            CalculateButton.Enabled = false;
            SaveDepartureAddressButton.Enabled = false;
            SaveKmPriceButton.Enabled = false;
            SearchButton.Enabled = false;
            SaveAdditionalKmButton.Enabled = false;
        }

        private void LoadTravelInfoSettings()
        {

        }

        private void InitializeMap()
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.SetPositionByKeywords("Mapų g 4, Kaunas, Lietuva");
            Map.ShowCenter = false;
            //Map.DragButton = MouseButtons.Left; Drag map option 
        }

        
    }
}
