using System;
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
            //SaveDepartureAddressButton.Enabled = false;
            SavePricePerKmButton.Enabled = false;
            //SearchRouteButton.Enabled = false;
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
            DepartureAddressTextBox.Text = travelInfoSettingsModel.DepartureAddress;
            PricePerKm.Text = travelInfoSettingsModel.PricePerKm.ToString(CultureInfo.InvariantCulture);
            AdditionalDistanceInKm.Text = travelInfoSettingsModel.AdditionalDistanceInKm.ToString(CultureInfo.InvariantCulture);
        }

        private void DepartureCountryTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (DepartureCountryTextBox.Text == "LTU")
            {
                DepartureCountryTextBox.Text = "Lietuva";
            }
        }

        private void DepartureAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            SaveDepartureAddressButton.Enabled = !string.IsNullOrWhiteSpace(DepartureAddressTextBox.Text);
            EnableSearchRouteButton();
        }

        private void SetMapPositionByAddress(string address)
        {
            Map.SetPositionByKeywords(address);
        }

        private void EnableSearchRouteButton()
        {
            SearchRouteButton.Enabled = (!string.IsNullOrWhiteSpace(DepartureAddressTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(ArrivalAddressTextBox.Text));
        }

        private void ArrivalAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EnableSearchRouteButton();
        }

        private void PricePerKm_TextChanged(object sender, System.EventArgs e)
        {
            SavePricePerKmButton.Enabled = CheckIsDouble(PricePerKm.Text);
        }

        private bool CheckIsDouble(string text)
        {
            double success;
            bool IsParsable = double.TryParse(text, out success);

            return IsParsable;
        }

        private void AdditionalDistanceInKm_TextChanged(object sender, EventArgs e)
        {
            SaveAdditionalDistanceInKmButton.Enabled = CheckIsDouble(AdditionalDistanceInKm.Text);
        }
    }
}
