using System.Globalization;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using Martium.TravelInfo.Models;
using Martium.TravelInfo.Repositories;

namespace Martium.TravelInfo.Forms
{
    public partial class TravelInfoForm : Form
    {
        private readonly TravelInfoRepository _travelInfoRepository;

        public TravelInfoForm()
        {
            _travelInfoRepository = new TravelInfoRepository();

            InitializeComponent();

            InitializeControls();
            InitializeMap();
        }

        private void TravelInfoForm_Load(object sender, System.EventArgs e)
        {
            LoadTravelInfoSettings();

            SetMapPositionByAddress("Mapų g 4, Kaunas, Lietuva");
        }

        private void MapContributorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MapContributorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.openstreetmap.org");
        }

        private void InitializeControls()
        {
            ActiveControl = DepartureCountryLabel;

            CalculatedTripPriceTextBox.Enabled = false;
            CalculatedDistanceTextBox.Enabled = false;
            DepartureCountryTextBox.Enabled = false;
            CalculateButton.Enabled = false;
            SaveDepartureAddressButton.Enabled = false;
            SavePricePerKmButton.Enabled = false;
            SearchAddressButton.Enabled = false;
            SaveAdditionalDistanceInKmButton.Enabled = false;
        }

        private void InitializeMap()
        {
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.ShowCenter = false;
            //Map.DragButton = MouseButtons.Left; Drag map option 
        }

        private void LoadTravelInfoSettings()
        {
            TravelInfoSettingsModel travelInfoSettingsModel = _travelInfoRepository.GetExistingInfo();

            DepartureCountryTextBox.Text = travelInfoSettingsModel.DepartureCountry;
            DepartueAddressTextBox.Text = travelInfoSettingsModel.DepartureAddress;
            PricePerKm.Text = travelInfoSettingsModel.PricePerKm.ToString(CultureInfo.InvariantCulture);
            AdditionalDistanceInKm.Text = travelInfoSettingsModel.AdditionalDistanceInKm.ToString(CultureInfo.InvariantCulture);
        }

        private void SetMapPositionByAddress(string address)
        {
            Map.SetPositionByKeywords(address);
        }
    }
}
