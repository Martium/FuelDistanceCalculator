using System;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Martium.TravelInfo.App.Forms;
using Martium.TravelInfo.App.Repositories;
using Martium.TravelInfo.App.Services;

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
            string message = null;
            AlertService alertService = new AlertService();

            using (Mutex mutex = new Mutex(false, "Global\\" + AppUuid))
            {
                if (!mutex.WaitOne(0, false))
                {
                    alertService.ShowErrorDialog(@"Vienu metu galima paleisti tik vieną 'TravelInfo' aplikaciją!");
                    return;
                }

                bool internedConnected = CheckInternetConnection();

                if (!internedConnected)
                {
                    alertService.ShowErrorDialog("Programos veikimui yra reikalingas Internetas");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                bool success = InitializeDatabase();

                if (success)
                {
                    Application.Run(new TravelInfoForm());
                }
                else
                {
                    alertService.ShowErrorDialog("nepavyko įrašyti duomenų bazės");
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
            catch 
            {
                success = false;
            }

            return success;
        }

        private static bool CheckInternetConnection()
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
                checkInternet = false;
            }

            return checkInternet;
        }
    }
}
