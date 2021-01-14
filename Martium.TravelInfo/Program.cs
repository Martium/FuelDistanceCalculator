using System;
using System.Windows.Forms;
using Martium.TravelInfo.Forms;
using Martium.TravelInfo.Repositories;

namespace Martium.TravelInfo
{
    static class Program
    {
        private static readonly DataBaseInitializerRepository DatabaseInitializerRepository = new DataBaseInitializerRepository();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool success = InitializeDatabase();

            if (success)
            {
                Application.Run(new TravelInfoForm());
            }
        }

        private static bool InitializeDatabase()
        {
            bool success = true;

            try
            {
                DatabaseInitializerRepository.InitializeDatabaseIfNotExist();
            }
            catch (Exception exception)
            {
                success = false;

                MessageBox.Show(exception.Message, "Klaidos pranešimas", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return success;
        }
    }
}
