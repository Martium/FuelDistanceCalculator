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
            GMap.NET.GMaps.Instance.Mode = GMap.NET.AccessMode.ServerOnly;
            Map.MapProvider = OpenStreetMapProvider.Instance;
            Map.SetPositionByKeywords("mapu g 4, Kaunas, Lithuania");
            Map.ShowCenter = false;
            //Map.DragButton = MouseButtons.Left; Drag map option 
        }

        private void MapContributorLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MapContributorLinkLabel.LinkVisited = true;
            System.Diagnostics.Process.Start("http://www.openstreetmap.org");
        }
      
    }
}
