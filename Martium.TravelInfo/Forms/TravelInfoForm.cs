using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using GMap.NET.MapProviders;
using Martium.TravelInfo.Constants;
using Martium.TravelInfo.Models;
using Martium.TravelInfo.Repositories;

namespace Martium.TravelInfo.Forms
{
    public partial class TravelInfoForm : Form
    {
        private readonly TravelInfoRepository _travelInfoRepository;
        private TravelInfoSettingsModel _travelInfoSettingsModel;


        public TravelInfoForm()
        {
            _travelInfoRepository = new TravelInfoRepository();

            InitializeComponent();

            InitializeControls();
            SetTextBoxMaxLengths();
            InitializeMap();
        }

        private void TravelInfoForm_Load(object sender, System.EventArgs e)
        {
            LoadTravelInfoSettings();

            ChangeDepartureTextBoxText();

            SetMapPositionByAddress($"{DepartureAddressTextBox.Text}, {DepartureCountryTextBox.Text}");
        }

        private void MapContributorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MapContributorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.openstreetmap.org");
        }

        private void SaveDepartureAddressButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.DepartureCountry = "LTU"; // for now later we will fix
            _travelInfoSettingsModel.DepartureAddress = DepartureAddressTextBox.Text;
            UpdateNewInfo();
        }

        private void SaveAdditionalDistanceInKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.DepartureCountry = "LTU"; // for now later we will fix
            _travelInfoSettingsModel.AdditionalDistanceInKm = double.Parse(AdditionalDistanceInKmTextBox.Text);
            UpdateNewInfo();
        }

        private void SavePricePerKmButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel.DepartureCountry = "LTU"; // for now later we will fix
            _travelInfoSettingsModel.PricePerKm = double.Parse(PricePerKm.Text);
            UpdateNewInfo();
        }

        private void PricePerKm_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBoxValidation(e, PricePerKm, SavePricePerKmButton);
        }

        private void AdditionalDistanceInKmTextBox_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBoxValidation(e, AdditionalDistanceInKmTextBox, SaveAdditionalDistanceInKmButton);
        }

        private void DepartureAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EnableSaveButton(DepartureAddressTextBox);
            EnableSearchRouteButton();
        }

        private void ArrivalAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EnableSearchRouteButton();
        }

        private void PricePerKm_TextChanged(object sender, System.EventArgs e)
        {
            EnableDoubleSaveButton(SavePricePerKmButton, PricePerKm);
        }

        private void AdditionalDistanceInKm_TextChanged(object sender, EventArgs e)
        {
            EnableDoubleSaveButton(SaveAdditionalDistanceInKmButton, AdditionalDistanceInKmTextBox);
        }

        #region MyMethods

        private void InitializeControls()
        {
            ActiveControl = DepartureCountryLabel;

            CalculatedTripPriceTextBox.Enabled = false;
            CalculatedDistanceTextBox.Enabled = false;
            DepartureCountryTextBox.Enabled = false;
            CalculateButton.Enabled = false;
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
            _travelInfoSettingsModel = _travelInfoRepository.GetExistingInfo();

            DepartureCountryTextBox.Text = _travelInfoSettingsModel.DepartureCountry;
            DepartureAddressTextBox.Text = _travelInfoSettingsModel.DepartureAddress;
            PricePerKm.Text = _travelInfoSettingsModel.PricePerKm.ToString(CultureInfo.InvariantCulture);
            AdditionalDistanceInKmTextBox.Text =
                _travelInfoSettingsModel.AdditionalDistanceInKm.ToString(CultureInfo.InvariantCulture);
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

        private void EnableSaveButton(TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                SaveDepartureAddressButton.Enabled = false;
            }
            else
            {
                SaveDepartureAddressButton.Enabled = true;
            }
        }

        private bool CheckIsDouble(string text)
        {
            double success;
            bool IsParsable = double.TryParse(text, out success);

            return IsParsable;
        }

        private static void ShowInformationDialog(string message)
        {
            MessageBox.Show(message, "Info pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private static void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Klaidos pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void UpdateNewInfo()
        {
            bool success;
            string successMessage = "Išsaugota sekmingai";
            string errorMessage = "Neišsaugota bandykite dar kartą";

            success = _travelInfoRepository.UpdateExistingInfo(_travelInfoSettingsModel);

            if (success)
            {
                ShowInformationDialog(successMessage);
            }
            else
            {
                ShowErrorDialog(errorMessage);
            }
        }

        private void CheckTextBoxValidation(CancelEventArgs e, TextBox textBox, Button button)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                button.Enabled = false;
                e.Cancel = true;
                OpenErrorLabel("Raudonas langelis negali būti tuščias ir turi būt skaičius", textBox, errorLabel);
            }
            else if (!CheckIsDouble(textBox.Text))
            {
                button.Enabled = false;
                e.Cancel = true;
                OpenErrorLabel("Raudonas langelis turi būti skaičius", textBox, errorLabel);
            }
            else
            {
                button.Enabled = true;
                e.Cancel = false;
                HideLabelAndTextBoxError(errorLabel, textBox);
            }
        }

        private void OpenErrorLabel(string errorText, TextBox textBox, Label label)
        {
            textBox.BackColor = Color.Red;
            label.Text = errorText;
            label.Visible = true;
        }

        private void HideLabelAndTextBoxError(Label label, TextBox textBox)
        {
            label.Visible = false;
            textBox.BackColor = Color.White;
        }

        private void SetTextBoxMaxLengths()
        {
            DepartureAddressTextBox.MaxLength = FormSettings.TextBoxLenghts.DepartureAddress;
        }

        private void ChangeDepartureTextBoxText()
        {
            if (DepartureCountryTextBox.Text == "LTU")
            {
                DepartureCountryTextBox.Text = "Lietuva"; // if you save data are this will not make error in database?
            }
        }

        private void EnableDoubleSaveButton(Button button, TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                button.Enabled = false;
            }
            else if (!CheckIsDouble(textBox.Text))
            {
                button.Enabled = false;
            }
            else
            {
                button.Enabled = true;
            }
        }

        #endregion
    }
}
