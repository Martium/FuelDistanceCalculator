using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Martium.TravelInfo.App.Forms;
using Martium.TravelInfo.App.Repositories;

namespace Martium.TravelInfo.App
{
    static class Program
    {
        private const string AppUuid = "e69b2537-3f00-4eaa-adb1-d22b0939667b";
        private static readonly DataBaseInitializerRepository DatabaseInitializerRepository = new DataBaseInitializerRepository();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + AppUuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    MessageBox.Show(@"Vienu metu galima paleisti tik vieną 'Travelinfo' aplikaciją!");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool success = InitializeDatabase();
                bool checkInternet = CheckForInternetConnection();

                if (success && checkInternet)
                {
                    Application.Run(new TravelInfoForm());
                }
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

        private static bool CheckForInternetConnection()
        {
            bool checkInternet;
            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com"))
                    {
                        checkInternet = true;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Programos veikimui yra reikalingas Internetas", "Klaidos pranešimas",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                checkInternet = false;
            }

            return checkInternet;
        }
    }
}
