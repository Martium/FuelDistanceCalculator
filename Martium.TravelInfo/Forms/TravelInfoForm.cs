using System.Windows.Forms;

namespace Martium.TravelInfo.Forms
{
    public partial class TravelInfoForm : Form
    {
        public TravelInfoForm()
        {
            InitializeComponent();

            InitializeControls();
        }

        private void InitializeControls()
        {
            TripPriceTextBox.Enabled = false;
            DistanceTextBox.Enabled = false;
            CalculateButton.Enabled = false;
        }
    }
}
