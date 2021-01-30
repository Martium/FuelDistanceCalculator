using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Martium.TravelInfo.App.Forms;
using Martium.TravelInfo.App.Repositories;
using Martium.TravelInfo.App.Services;

namespace Martium.TravelInfo.App
{
    static class Program
    {
        private static readonly DataBaseInitializerRepository DatabaseInitializerRepository = new DataBaseInitializerRepository();
        private static readonly TravelInfoRepository TravelInfoRepository = new TravelInfoRepository();
        private static readonly ApiClients.MapsApiClient MapsApiClient = new ApiClients.MapsApiClient();
        private static readonly MessageDialogService MessageDialogService = new MessageDialogService();
        private static readonly string ApiKeyFilePath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Resources.rs";

        [STAThread]
        static void Main()
        {
            using (Mutex mutex = new Mutex(false, "Global\\" + AppConfiguration.AppUuid))
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
                    bool apiKeyValid = ExtractApiKey();
                    if (apiKeyValid)
                    {
                        DeleteApiKeyFile();
                    }
                    else
                    {
                        MessageDialogService.ShowErrorDialog("Nepavyko rasti reikiamo rakto, kreipkitės į administratorių!");
                        return;
                    }

                    Application.Run(new TravelInfoForm());
                }
                else
                {
                    MessageDialogService.ShowErrorDialog("Nepavyko sukurti duomenų bazės todėl 'TravelInfo' aplikacija negali veikti!");
                }
            }
        }

        private static void DeleteApiKeyFile()
        {
            if (File.Exists(ApiKeyFilePath))
            {
                try
                {
                    File.Delete(ApiKeyFilePath);
                }
                catch
                {
                    // ignored
                }
            }
        }

        private static bool ExtractApiKey()
        {
            bool success = true;

            if (File.Exists(ApiKeyFilePath))
            {
                string foundApiKey = File.ReadAllText(ApiKeyFilePath);

                var apiKeyValid = MapsApiClient.ValidateBingMapsApiKey(foundApiKey);
                if (apiKeyValid)
                {
                    TravelInfoRepository.UpdateApiKey(foundApiKey);
                }
                else
                {
                    success = false;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(TravelInfoRepository.GetApiKey()))
                {
                    success = false;
                }
            }

            return success;
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
