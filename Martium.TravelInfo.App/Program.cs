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
        private static readonly MessageDialogService MessageDialogService = new MessageDialogService();

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
                    MessageDialogService.ShowErrorDialog("Vienu metu galima paleisti tik vieną 'TravelInfo' aplikaciją!");
                    return;
                }

                if (!CheckInternetConnection())
                {
                    MessageDialogService.ShowErrorDialog("'TravelInfo' aplikacijos veikimui yra reikalingas internetas!");
                    return;
                }

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                if (InitializeDatabase())
                {
                    Application.Run(new TravelInfoForm());
                }
                else
                {
                    MessageDialogService.ShowErrorDialog("Nepavyko sukurti duomenų bazės todėl 'TravelInfo' aplikacija negali veikti!");
                }
            }
        }

        private static bool InitializeDatabase()
        {
            bool databaseInitialized = true;

            try
            {
                DatabaseInitializerRepository.InitializeDatabaseIfNotExist();
            }
            catch 
            {
                databaseInitialized = false;
            }

            return databaseInitialized; 
        }

        private static bool CheckInternetConnection()
        {
            bool internetConnected;

            try
            {
                using (var client = new WebClient())
                {
                    using (client.OpenRead("http://google.com"))
                    {
                        internetConnected = true;
                    }
                }
            }
            catch
            {
                internetConnected = false;
            }

            return internetConnected;
        }
    }
}
