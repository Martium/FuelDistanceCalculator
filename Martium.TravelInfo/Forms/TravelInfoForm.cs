using System;
using System.ComponentModel;
using System.Drawing;
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
        private TravelInfoSettingsModel _travelInfoSettingsModel;


        public TravelInfoForm()
        {
            _travelInfoRepository = new TravelInfoRepository();
            _travelInfoSettingsModel = new TravelInfoSettingsModel();

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
            //SavePricePerKmButton.Enabled = false;
            //SearchRouteButton.Enabled = false;
            //SaveAdditionalDistanceInKmButton.Enabled = false;
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

        private void DepartureCountryTextBox_TextChanged(object sender, System.EventArgs e)
        {
            if (DepartureCountryTextBox.Text == "LTU")
            {
                DepartureCountryTextBox.Text = "Lietuva"; // if you save data are this will not make error in database?
            }
        }

        private void DepartureAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            SaveDepartureAddressButton.Enabled = !string.IsNullOrWhiteSpace(DepartureAddressTextBox.Text);
            //EnableSaveButton(SaveDepartureAddressButton, DepartureAddressTextBox);
            EnableSearchRouteButton();
        }

        private void ArrivalAddressTextBox_TextChanged(object sender, System.EventArgs e)
        {
            EnableSearchRouteButton();
        }

        private void PricePerKm_TextChanged(object sender, System.EventArgs e)
        {
            //EnableSaveButton(SavePricePerKmButton, PricePerKm);
            SavePricePerKmButton.Enabled = CheckIsDouble(PricePerKm.Text);
        }

        private void AdditionalDistanceInKm_TextChanged(object sender, EventArgs e)
        {
            //EnableSaveButton(SaveAdditionalDistanceInKmButton, AdditionalDistanceInKm); 
            SaveAdditionalDistanceInKmButton.Enabled = CheckIsDouble(AdditionalDistanceInKmTextBox.Text);
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

        private void EnableSaveButton(Button buttonName, TextBox textBox) // idea but not working as it should
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text) && CheckIsDouble(AdditionalDistanceInKmTextBox.Text) &&
                CheckIsDouble(PricePerKm.Text))
            {
                buttonName.Enabled = true;

            }
            else
            {
                buttonName.Enabled = false;
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



        private void SaveDepartureAddressButton_Click(object sender, EventArgs e)
        {
            _travelInfoSettingsModel = new TravelInfoSettingsModel()
            {
                DepartureCountry = DepartureCountryTextBox.Text,
                DepartureAddress = DepartureAddressTextBox.Text,
                AdditionalDistanceInKm = double.Parse(AdditionalDistanceInKmTextBox.Text),
                PricePerKm = double.Parse(PricePerKm.Text)
            };

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

        private void AdditionalDistanceInKmTextBox_Validating(object sender, CancelEventArgs e)
        {
           CheckTextBoxValidation(e, AdditionalDistanceInKmTextBox);
        }

        private void PricePerKm_Validating(object sender, CancelEventArgs e)
        {
            CheckTextBoxValidation(e, PricePerKm);
        }

        private void CheckTextBoxValidation(CancelEventArgs e, TextBox textBox)
        {
            if (string.IsNullOrWhiteSpace(textBox.Text))
            {
                e.Cancel = true;
                OpenErrorLabel("Raudonas langelis negali būti tuščias", textBox, errorLabel);
            }
            else if (!CheckIsDouble(textBox.Text))
            {
                e.Cancel = true;
                OpenErrorLabel("Raudonas langelis turi būti skaičius", textBox, errorLabel);
            }
            else
            {
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
    }
}
