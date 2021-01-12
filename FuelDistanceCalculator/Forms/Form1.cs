using System.Windows.Forms;

namespace FuelDistanceCalculator
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            TripPriceTextBox.Enabled = false;
            DistanceTextBox.Enabled = false;

        }
      
    }
}
