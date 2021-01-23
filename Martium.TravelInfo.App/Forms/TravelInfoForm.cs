using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using GMap.NET;
using GMap.NET.WindowsForms.Markers;
using ISO3166;
using Martium.TravelInfo.App.Constants;
using Martium.TravelInfo.App.Models;
using Martium.TravelInfo.App.Repositories;
using Martium.TravelInfo.App.Services;

namespace Martium.TravelInfo.App.Forms
{
    public partial class TravelInfoForm : Form
    {
        private readonly TravelInfoRepository _travelInfoRepository;
        private readonly MapService _mapService;

        private TravelInfoSettingsModel _travelInfoSettingsModel;
        private readonly Country[] _countries = Country.List;

        public TravelInfoForm()
        {
            _travelInfoRepository = new TravelInfoRepository();

            InitializeComponent();

            _mapService = new MapService(Map);

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
        }

        private void DepartureAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForStringTextBox(DepartureAddressTextBox, SaveDepartureAddressButton, _travelInfoSettingsModel.DepartureAddress);
            EnableSearchRouteButtonIfPossible();
        }

        private void SaveDepartureAddressButton_Click(object sender, EventArgs e)
        {
            Country selectedCountry = GetSelectedCountryByComboBox(DepartureCountryComboBox);
            _travelInfoSettingsModel.DepartureCountry = selectedCountry.TwoLetterCode;

            _travelInfoSettingsModel.DepartureAddress = DepartureAddressTextBox.Text;

            UpdateNewInfo();

            ToggleButtonStateForStringTextBox(DepartureAddressTextBox, SaveDepartureAddressButton, _travelInfoSettingsModel.DepartureAddress);
        }

        private void ArrivalCountryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            Country selectedCountry = GetSelectedCountryByComboBox(ArrivalCountryComboBox);

            ArrivalCountryTextLabel.Text = selectedCountry.Name;
        }

        private void ArrivalAddressTextBox_TextChanged(object sender, EventArgs e)
        {
            EnableSearchRouteButtonIfPossible();
        }

        private void SearchRouteButton_Click(object sender, EventArgs e)
        {
            _mapService.ClearAllRoutesAndMarks();

            string fullDepartureAddress = GetFullAddress(DepartureAddressTextBox, DepartureCountryTextLabel);
            string fullArrivalAddress = GetFullAddress(ArrivalAddressTextBox, ArrivalCountryTextLabel);

            PointLatLng? departureCoordinates = _mapService.GetAddressCoordinates(fullDepartureAddress);
            PointLatLng? arrivalCoordinates = _mapService.GetAddressCoordinates(fullArrivalAddress);

            if (departureCoordinates.HasValue && arrivalCoordinates.HasValue)
            {
                _mapService.CreateMapMarker(departureCoordinates.Value, GMarkerGoogleType.red);
                _mapService.CreateMapMarker(arrivalCoordinates.Value, GMarkerGoogleType.green);

                MapRoute route = _mapService.GetRoute(departureCoordinates.Value, arrivalCoordinates.Value);
                if (route != null)
                {
                    _mapService.ShowRoute(route);
                    _mapService.SetMapPositionByAddress(fullArrivalAddress);
                }
                else
                {
                    _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
                    ShowErrorDialog("Nepavyko rasti maršruto ! (išvykimo ir atvykimo adresai gali būti per dideliu atstumu vienas nuo kito). Nurodykite kitus adresus(-ą).");
                }
            }
            else if (!departureCoordinates.HasValue && !arrivalCoordinates.HasValue)
            {
                _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
                ShowErrorDialog("Nepavyko rasti išvykimo ir atvykimo adresų! Įveskite kitus adresus.");
            }
            else if (!departureCoordinates.HasValue)
            {
                _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
                ShowErrorDialog("Nepavyko rasti išvykimo adreso! Įveskite kitą adresą.");
            }
            else
            {
                _mapService.SetMapPositionByAddress(DepartureCountryTextLabel.Text, 7);
                ShowErrorDialog("Nepavyko rasti atvykimo adreso! Įveskite kitą adresą.");
            }
        }

        private void PricePerKm_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForDecimalTextBox(PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void PricePerKm_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBoxOfDoubleType(e, PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void SavePricePerKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.PricePerKm = decimal.Parse(PricePerKm.Text, CultureInfo.InvariantCulture);
            UpdateNewInfo();
            ToggleButtonStateForDecimalTextBox(PricePerKm, SavePricePerKmButton, _travelInfoSettingsModel.PricePerKm);
        }

        private void AdditionalDistanceInKm_TextChanged(object sender, EventArgs e)
        {
            ToggleButtonStateForDecimalTextBox(
                AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton, _travelInfoSettingsModel.AdditionalDistanceInKm);
        }

        private void AdditionalDistanceInKmTextBox_Validating(object sender, CancelEventArgs e)
        {
            ValidateTextBoxOfDoubleType(e, AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton,
                _travelInfoSettingsModel.AdditionalDistanceInKm, preventNegativeOrZero: false);
        }

        private void SaveAdditionalDistanceInKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.AdditionalDistanceInKm = decimal.Parse(AdditionalDistanceInKmTextBox.Text, CultureInfo.InvariantCulture);
            UpdateNewInfo();
            ToggleButtonStateForDecimalTextBox(AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton, _travelInfoSettingsModel.AdditionalDistanceInKm);
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

            CalculatedDistanceLabel.Visible = false;
            CalculatedDistanceTextBox.Visible = false;
            CalculatedDistanceTextBox.Enabled = false;

            CalculatedDurationLabel.Visible = false;
            CalculatedDurationTextBox.Visible = false;
            CalculatedDurationTextBox.Enabled = false;

            CalculateButton.Enabled = false;

            CalculatedTripPriceLabel.Visible = false;
            CalculatedTripPriceTextBox.Visible = false;
            CalculatedTripPriceTextBox.Enabled = false;


            DepartureCountryComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            DepartureCountryComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            DepartureCountryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            ArrivalCountryComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            ArrivalCountryComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            ArrivalCountryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
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
            SearchRouteButton.Enabled = (!string.IsNullOrWhiteSpace(DepartureAddressTextBox.Text) &&
                                         !string.IsNullOrWhiteSpace(ArrivalAddressTextBox.Text));
        }

        private void UpdateNewInfo()
        {
            string successMessage = "Išsaugota sėkmingai.";
            string errorMessage = "Nepavyko išsaugoti, bandykite dar kartą!";

            bool success = _travelInfoRepository.UpdateSettings(_travelInfoSettingsModel);

            if (success)
            {
                ShowInformationDialog(successMessage);
            }
            else
            {
                ShowErrorDialog(errorMessage);
            }
        }

        private void ShowInformationDialog(string message)
        {
            MessageBox.Show(message, "Info pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Klaidos pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void ValidateTextBoxOfDoubleType(CancelEventArgs e, TextBox textBox, Button button, decimal settingValue, bool preventNegativeOrZero = true)
        {
            bool isDecimal = CheckIsDecimal(textBox.Text);

            if (
                string.IsNullOrWhiteSpace(textBox.Text) 
                || !isDecimal 
                || textBox.Text.Contains(",") 
                || (preventNegativeOrZero && (textBox.Text == "0" || textBox.Text.StartsWith("-"))))
            {
                string biggerThanZeroMessagePart = preventNegativeOrZero ? "> 0" : string.Empty;

                e.Cancel = true;
                DisplayDecimalTextBoxError(
                    $"* Langelyje privalo būti skaičius {biggerThanZeroMessagePart} (< 1 skiriklis: '.'), pvz.: 0.2", 
                    textBox, 
                    DecimalTextBoxErrorLabel);
                DisableButton(button);
            }
            else
            {
                e.Cancel = false;
                HideDecimalTextBoxError(DecimalTextBoxErrorLabel, textBox);
                ToggleButtonStateForDecimalTextBox(textBox, button, settingValue);
            }
        }

        private void DisplayDecimalTextBoxError(string errorText, TextBox textBox, Label label)
        {
            textBox.BackColor = Color.Red;
            label.Text = errorText;
            label.Visible = true;
        }

        private void HideDecimalTextBoxError(Label label, TextBox textBox)
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

        private void ToggleButtonStateForDecimalTextBox(TextBox textBox, Button button, decimal settingField)
        {
            if (CheckIsDecimal(textBox.Text))
            {
                var textBoxAsDecimal = decimal.Parse(textBox.Text, CultureInfo.InvariantCulture);

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

        private bool CheckIsDecimal(string text)    
        {
            bool success = true;

            try
            {
                decimal.Parse(text, CultureInfo.InvariantCulture);
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

            PointLatLng? departureCoordinates = _mapService.GetAddressCoordinates(fullDepartureAddress);

            if (departureCoordinates.HasValue && !string.IsNullOrWhiteSpace(_travelInfoSettingsModel.DepartureAddress))
            {
                _mapService.CreateMapMarker(departureCoordinates.Value, GMarkerGoogleType.red);
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

        #endregion
    }
}
