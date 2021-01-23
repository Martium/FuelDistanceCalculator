using System.Windows.Forms;

namespace Martium.TravelInfo.App.Services
{
    public class MessageDialogService
    {
        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Klaidos pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowInformationDialog(string message)
        {
            MessageBox.Show(message, "Info pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
