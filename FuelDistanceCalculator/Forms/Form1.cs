using System.Windows.Forms;

namespace FuelDistanceCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            InitializeControls();
        }

        private void InitializeControls()
        {
            TripPriceTextBox.Enabled = false;
            DistanceTextBox.Enabled = false;
        }
    }
}
