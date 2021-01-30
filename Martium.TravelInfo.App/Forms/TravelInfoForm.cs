using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using ISO3166;
using Martium.TravelInfo.App.Constants;
using Martium.TravelInfo.App.Models;
using Martium.TravelInfo.App.Repositories;
using Martium.TravelInfo.App.Services;
using Coordinates = Martium.TravelInfo.App.Contracts.Coordinates;
using LocationInfo = Martium.TravelInfo.App.Contracts.LocationInfo;
using RouteInfo = Martium.TravelInfo.App.Contracts.RouteInfo;
using RouteInfoSummary = Martium.TravelInfo.App.Contracts.RouteInfoSummary;

namespace Martium.TravelInfo.App.Forms
{
    public partial class TravelInfoForm : Form
    {
        private readonly TravelInfoRepository _travelInfoRepository;
        private readonly ApiClients.MapsApiClient _mapsApiClient;
        private readonly MapService _mapService;
        private readonly MessageDialogService _messageDialogService;

        private TravelInfoSettingsModel _travelInfoSettingsModel;
        private readonly Country[] _countries = Country.List;

        private string _lastDepartureAddress = null;
        private string _lastArrivalAddress = null;

        public TravelInfoForm()
        {
            InitializeComponent();

            _travelInfoRepository = new TravelInfoRepository();
            _mapsApiClient = new ApiClients.MapsApiClient();
            _mapService = new MapService(Map);
            _messageDialogService = new MessageDialogService();

            InitializeControls();
            SetTextBoxMaxLengths();
            _mapService.InitializeMap();
        }

        private void TravelInfoForm_Load(object sender, EventArgs e)
        {
            LoadTravelInfoSettings();

            LoadInitialMapView();
        }
        private void DepartureCountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country selectedCountry = GetSelectedCountryByComboBox(DepartureCountryComboBox);

            DepartureCountryTextLabel.Text = selectedCountry.Name;

            if (string.IsNullOrWhiteSpace(ArrivalAddressTextBox.Text))
            {
                ArrivalCountryComboBox.Text = DepartureCountryComboBox.Text;
            }

            EnableSearchRouteButtonIfPossible();
        }

        private void DepartureAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForStringTextBox(DepartureAddressTextBox, SaveDepartureAddressButton, _travelInfoSettingsModel.DepartureAddress);
            EnableSearchRouteButtonIfPossible();
        }

        private void SaveDepartureAddressButton_Click(object sender, EventArgs e)
        {
            string fullAddress = GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);
            LocationInfo departureLocationInfo = _mapsApiClient.GetLocationInfo(fullAddress);

            if (departureLocationInfo != null)
            {
                Country selectedCountry = GetSelectedCountryByComboBox(DepartureCountryComboBox);
                _travelInfoSettingsModel.DepartureCountry = selectedCountry.TwoLetterCode;

                _travelInfoSettingsModel.DepartureAddress = DepartureAddressTextBox.Text;

                UpdateSettings();
            }
            else
            {
                _messageDialogService.ShowErrorDialog("Nepavyko išsaugoti išvykimo adreso! Adresas nerastas, prašome patikslinti arba įvesti kitą");
            }

            ToggleButtonStateForStringTextBox(DepartureAddressTextBox, SaveDepartureAddressButton, _travelInfoSettingsModel.DepartureAddress);
        }

        private void ArrivalCountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country selectedCountry = GetSelectedCountryByComboBox(ArrivalCountryComboBox);

            ArrivalCountryTextLabel.Text = selectedCountry.Name;

            EnableSearchRouteButtonIfPossible();
        }

        private void ArrivalAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableSearchRouteButtonIfPossible();
        }

        private void SearchRouteButton_Click(object sender, EventArgs e)
        {
            _lastDepartureAddress = GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);
            _lastArrivalAddress = GetFullAddress(ArrivalAddressTextBox, ArrivalCountryTextLabel);

            ClearPreviousSearchResults();

            string departureAddress = GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);
            string arrivalAddress = GetFullAddress(ArrivalAddressTextBox, ArrivalCountryTextLabel);

            LocationInfo departureLocation = _mapsApiClient.GetLocationInfo(departureAddress);
            LocationInfo arrivalLocation = _mapsApiClient.GetLocationInfo(arrivalAddress);

            string errorMessage = ValidateRouteCoordinates(departureLocation, arrivalLocation);
            if (!string.IsNullOrWhiteSpace(errorMessage))
            {
                ShowRouteSearchError(errorMessage);
                return;
            }

            RouteInfo route = _mapsApiClient.GetRouteInfo(departureAddress, arrivalAddress);
            if (route != null)
            {
                DisplayFoundRoute(departureLocation.Coordinates, arrivalLocation.Coordinates, route.Coordinates);

                SetRouteInfoControlsVisibility(visible: true);
                DisplayRouteSummary(route.Summary);
                CalculateTripCostButton.Enabled = true;
            }
            else
            {
                ShowRouteSearchError("Nepavyko rasti maršruto ! (išvykimo ir atvykimo adresai gali būti per dideliu atstumu vienas nuo kito). Nurodykite kitus adresus(-ą).");
            }
        }

        private void PricePerKm_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForNumberTextBox(PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void PricePerKm_Validating(object sender, CancelEventArgs e)
        {
            ValidateNumberInput(e, PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void SavePricePerKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.PricePerKm = double.Parse(PricePerKm.Text, CultureInfo.InvariantCulture);
            UpdateSettings();
            ToggleButtonStateForNumberTextBox(PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void AdditionalDistanceInKm_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForNumberTextBox(
                AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton, _travelInfoSettingsModel.AdditionalDistanceInKm);
        }

        private void AdditionalDistanceInKmTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateNumberInput(e, AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton,
                _travelInfoSettingsModel.AdditionalDistanceInKm, preventNegativeOrZero: false);
        }

        private void SaveAdditionalDistanceInKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.AdditionalDistanceInKm = double.Parse(AdditionalDistanceInKmTextBox.Text, CultureInfo.InvariantCulture);
            UpdateSettings();
            ToggleButtonStateForNumberTextBox(AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton, _travelInfoSettingsModel.AdditionalDistanceInKm);
        }

        private void CalculateTripPriceButton_Click(object sender, EventArgs e)
        {
            SetTripPriceControlsVisibility(visible: true);

            double tripPriceOneWay = CalculateTripPrice();
            double tripPriceTwoWays = CalculateTripPrice(includeReturnPrice: true);

            double roundTripPriceOneWay = RoundNumber(tripPriceOneWay);
            double roundTripPriceTwoWay = RoundNumber(tripPriceTwoWays);

            OneWayTripPriceTextBox.Text = roundTripPriceOneWay.ToString(CultureInfo.InvariantCulture);
            ReturnIncludedTripPriceTextBox.Text = roundTripPriceTwoWay.ToString(CultureInfo.InvariantCulture);
        }

        private void MapContributorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MapContributorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.openstreetmap.org");
        }

        #region custom methods

        private void InitializeControls()
        {
            ActiveControl = DepartureCountryLabel;

            SaveDepartureAddressButton.Enabled = false;
            SavePricePerKmButton.Enabled = false;
            SaveAdditionalDistanceInKmButton.Enabled = false;

            TripDistanceLabel.Visible = false;
            TripDistanceTextBox.Visible = false;
            TripDistanceTextBox.Enabled = false;

            TripDurationLabel.Visible = false;
            TripDurationTextBox.Visible = false;
            TripDurationTextBox.Enabled = false;

            CalculateTripCostButton.Enabled = false;

            OneWayTripPriceLabel.Visible = false;
            OneWayTripPriceTextBox.Visible = false;
            ReturnIncludedTripPriceLabel.Visible = false;
            ReturnIncludedTripPriceTextBox.Visible = false;
            OneWayTripPriceTextBox.Enabled = false;
            ReturnIncludedTripPriceTextBox.Enabled = false;

            ConfigureComboBoxControls(DepartureCountryComboBox);
            ConfigureComboBoxControls(ArrivalCountryComboBox);
        }

        private void ConfigureComboBoxControls(ComboBox comboBox)
        {
            comboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void LoadTravelInfoSettings()
        {
            _travelInfoSettingsModel = _travelInfoRepository.GetSettings();

            DepartureAddressTextBox.Text = _travelInfoSettingsModel.DepartureAddress;
            PricePerKm.Text = _travelInfoSettingsModel.PricePerKm.ToString(CultureInfo.InvariantCulture);
            AdditionalDistanceInKmTextBox.Text = _travelInfoSettingsModel.AdditionalDistanceInKm.ToString(CultureInfo.InvariantCulture);
           
            LoadCountryComboBox(DepartureCountryComboBox);
            LoadCountryComboBox(ArrivalCountryComboBox);
        }

        private void EnableSearchRouteButtonIfPossible()
        {
            bool addressesIsNotEmpty = 
                !string.IsNullOrWhiteSpace(DepartureAddressTextBox.Text) 
                && 
                !string.IsNullOrWhiteSpace(ArrivalAddressTextBox.Text);

            bool atLeastOneAddressIsModified = 
                _lastArrivalAddress != GetFullAddress(ArrivalAddressTextBox, ArrivalCountryTextLabel)
                || 
                _lastDepartureAddress != GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);

            SearchRouteButton.Enabled = addressesIsNotEmpty && atLeastOneAddressIsModified;
        }

        private void UpdateSettings()
        {
            string successMessage = "Išsaugota sėkmingai.";
            string errorMessage = "Nepavyko išsaugoti, bandykite dar kartą!";

            bool success = _travelInfoRepository.UpdateSettings(_travelInfoSettingsModel);

            if (success)
            {
                _messageDialogService.ShowInformationDialog(successMessage);
            }
            else
            {
                _messageDialogService.ShowErrorDialog(errorMessage);
            }
        }

        private void ValidateNumberInput(CancelEventArgs e, TextBox textBox, Button button, double settingValue, bool preventNegativeOrZero = true)
        {
            bool isDecimal = CheckIsDouble(textBox.Text);

            if (
                string.IsNullOrWhiteSpace(textBox.Text) 
                || !isDecimal 
                || textBox.Text.Contains(",") 
                || (preventNegativeOrZero && (textBox.Text == "0" || textBox.Text.StartsWith("-"))))
            {
                string biggerThanZeroMessagePart = preventNegativeOrZero ? "> 0" : string.Empty;

                e.Cancel = true;
                ShowNumberInputError(
                    $"* Langelyje privalo būti skaičius {biggerThanZeroMessagePart} (< 1 skiriklis: '.'), pvz.: 0.2", 
                    textBox, 
                    DecimalTextBoxErrorLabel);
                DisableButton(button);
            }
            else
            {
                e.Cancel = false;
                HideNumberInputError(DecimalTextBoxErrorLabel, textBox);
                ToggleButtonStateForNumberTextBox(textBox, button, settingValue);
            }
        }

        private void ShowNumberInputError(string errorText, TextBox textBox, Label label)
        {
            textBox.BackColor = Color.Red;
            label.Text = errorText;
            label.Visible = true;
        }

        private void HideNumberInputError(Label label, TextBox textBox)
        {
            label.Visible = false;
            textBox.BackColor = Color.White;
        }

        private void SetTextBoxMaxLengths()
        {
            DepartureAddressTextBox.MaxLength = FormSettings.TextBoxLenghts.DepartureAddress;
        }

        private void ToggleButtonStateForStringTextBox(TextBox textBox, Button button, string settingField)
        {
            button.Enabled = textBox.Text != settingField;
        }

        private void ToggleButtonStateForNumberTextBox(TextBox textBox, Button button, double settingField)
        {
            if (CheckIsDouble(textBox.Text))
            {
                var textBoxAsDecimal = double.Parse(textBox.Text, CultureInfo.InvariantCulture);

                if (!string.IsNullOrWhiteSpace(textBox.Text) && textBoxAsDecimal != settingField)
                {
                    button.Enabled = true;
                }
                else
                {
                    button.Enabled = false;
                }
            }
            else
            {
                button.Enabled = false;
            }
        }

        private void DisableButton(Button button)
        {
            button.Enabled = false;
        }

        private bool CheckIsDouble(string text)    
        {
            bool success = true;

            try
            {
                double.Parse(text, CultureInfo.InvariantCulture);
            }
            catch (Exception)
            {
                success = false;
            }

            return success;
        }

        private void LoadCountryComboBox(ComboBox comboBox)
        {
            IOrderedEnumerable<Country> sortedCountries = 
                from country in _countries orderby country.TwoLetterCode select country;

            foreach (Country country in sortedCountries)
            {
                comboBox.Items.Add(country.TwoLetterCode);
            }

            comboBox.Text = _travelInfoSettingsModel.DepartureCountry;
        }

        private Country GetSelectedCountryByComboBox(ComboBox comboBox)
        {
            Country selectedCountry = _countries.Single(c => c.TwoLetterCode == comboBox.Text);

            return selectedCountry;
        }

        private void LoadInitialMapView()
        {
            string fullDepartureAddress = GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);

            LocationInfo departureLocationInfo = _mapsApiClient.GetLocationInfo(fullDepartureAddress);

            if (departureLocationInfo != null && !string.IsNullOrWhiteSpace(_travelInfoSettingsModel.DepartureAddress))
            {
                _mapService.CreateMapMarker(departureLocationInfo.Coordinates, MapMarkerType.DepartureAddress);
                _mapService.SetMapPositionByAddress(fullDepartureAddress);
            }
            else
            {
                _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
            }
        }

        private string GetFullAddress(TextBox textBox, Label label)
        {
            return $"{textBox.Text}, {label.Text}";
        }

        private void SetRouteInfoControlsVisibility(bool visible)
        {
            TripDistanceLabel.Visible = visible;
            TripDistanceTextBox.Visible = visible;

            TripDurationLabel.Visible = visible;
            TripDurationTextBox.Visible = visible;
        }

        private void DisplayRouteSummary(RouteInfoSummary routeSummary)
        {
            double routeDistance = routeSummary.TotalDistanceInKm;
            double roundRouteDistance = RoundNumber(routeDistance);

            string formattedDistance = ConvertToFormattedNumber(roundRouteDistance);

            TripDistanceTextBox.Text = formattedDistance;

            TripDurationTextBox.Text = FormatTripDurationText(routeSummary.TotalDuration);
        }

        private static string ConvertToFormattedNumber(double number)
        {
            string formattedNumber = number.ToString(CultureInfo.InvariantCulture);

            return formattedNumber;
        }

        private string FormatTripDurationText(TimeSpan tripDuration)
        {
            string durationText = string.Empty;

            durationText = durationText.Insert(0, $"{tripDuration.Minutes} min");

            if (tripDuration.Hours > 0)
            {
                durationText = durationText.Insert(0, $"{tripDuration.Hours} h ");
            }

            if (tripDuration.Days > 0)
            {
                durationText = durationText.Insert(0, $"{tripDuration.Days} d ");
            }

            return durationText;
        }

        private void SetTripPriceControlsVisibility(bool visible)
        {
            OneWayTripPriceLabel.Visible = visible;
            OneWayTripPriceTextBox.Visible = visible;
            ReturnIncludedTripPriceLabel.Visible = visible;
            ReturnIncludedTripPriceTextBox.Visible = visible;
        }

        private double CalculateTripPrice(bool includeReturnPrice = false)
        {
            int priceRate = includeReturnPrice ? 2 : 1;

            double kmPrice = double.Parse(PricePerKm.Text, CultureInfo.InvariantCulture);
            double distance = double.Parse(TripDistanceTextBox.Text, CultureInfo.InvariantCulture);
            double additionalDistance = double.Parse(AdditionalDistanceInKmTextBox.Text, CultureInfo.InvariantCulture);

            double result = priceRate * kmPrice * (distance + additionalDistance);

            return result;
        }

        private double RoundNumber(double number, int digits = 2)
        {
            double roundToTwoDecimal = Math.Round(number, digits, MidpointRounding.ToEven);
            return roundToTwoDecimal;
        }

        private void ClearPreviousSearchResults()
        {
            _mapService.ClearAllRoutesAndMarks();
            SetRouteInfoControlsVisibility(visible: false);
            SetTripPriceControlsVisibility(visible: false);
            DisableButton(CalculateTripCostButton);
            DisableButton(SearchRouteButton);
        }

        private string ValidateRouteCoordinates(LocationInfo departureLocation, LocationInfo arrivalLocation)
        {
            string errorMessage = null;

            if (departureLocation == null && arrivalLocation == null)
            {
                errorMessage = "Nepavyko rasti išvykimo ir atvykimo adresų! Įveskite kitus adresus.";
            }
            else if (departureLocation == null)
            {
                errorMessage = "Nepavyko rasti išvykimo adreso! Įveskite kitą adresą.";
            }
            else if (arrivalLocation == null)
            {
                errorMessage = "Nepavyko rasti atvykimo adreso! Įveskite kitą adresą.";
            }

            return errorMessage;
        }

        private void DisplayFoundRoute(Coordinates departureCoordinates, Coordinates arrivalCoordinates, List<Coordinates> routePath)
        {
            string fullArrivalAddress = GetFullAddress(ArrivalAddressTextBox, ArrivalCountryTextLabel);

            _mapService.CreateMapMarker(departureCoordinates, MapMarkerType.DepartureAddress);
            _mapService.CreateMapMarker(arrivalCoordinates, MapMarkerType.ArrivalAddress);

            _mapService.DrawRoute(routePath);
            _mapService.SetMapPositionByAddress(fullArrivalAddress);
        }

        private void ShowRouteSearchError(string errorMessage)
        {
            _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
            _messageDialogService.ShowErrorDialog(errorMessage);
        }

        #endregion
    }
}
