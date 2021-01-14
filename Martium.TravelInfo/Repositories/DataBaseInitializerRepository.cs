using System;
using System.Data.SQLite;
using System.IO;
using Martium.TravelInfo.Constants;


namespace Martium.TravelInfo.Repositories
{
    public class DataBaseInitializerRepository
    {
        public void InitializeDatabaseIfNotExist()
        {
            if (File.Exists(AppConfiguration.DatabaseFile))
            {
#if DEBUG

#else
                return;
#endif
            }

            if (!Directory.Exists(AppConfiguration.DatabaseFolder))
            {
                Directory.CreateDirectory(AppConfiguration.DatabaseFolder);
            }
            else
            {
                DeleteLeftoverFilesAndFolders();
            }

            SQLiteConnection.CreateFile(AppConfiguration.DatabaseFile);

            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                CreateTravelInfoSettings(dbConnection);

                FillDefaultTravelInfoSettings(dbConnection);
            }
        }

        private void DeleteLeftoverFilesAndFolders()
        {
            var directory = new DirectoryInfo(AppConfiguration.DatabaseFolder);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subdirectory in directory.GetDirectories())
            {
                subdirectory.Delete(true);
            }
        }

        private void CreateTravelInfoSettings(SQLiteConnection dbConnection)
        {
            string dropTravelInfoSettingsQuery = GetDropTableQuery("TravelInfo");
            SQLiteCommand dropATravelInfoSettingsCommand = new SQLiteCommand(dropTravelInfoSettingsQuery, dbConnection);
            dropATravelInfoSettingsCommand.ExecuteNonQuery();

            string createTravelInfoSettingsQuery =
                $@"                  
				    CREATE TABLE [TravelInfo] (
						[Country] [nvarchar] ({FormSettings.TextBoxLenghts.Country}) NOT NULL,
						[DepartureAddress] [nvarchar] ({FormSettings.TextBoxLenghts.DepartureAddress}) NULL,
						[FuelPrice] [double] NOT NULL,
						[AdditionalKm] [double] NULL 
						)";

            SQLiteCommand createTravelInfoTableCommand =
                new SQLiteCommand(createTravelInfoSettingsQuery, dbConnection);
            createTravelInfoTableCommand.ExecuteNonQuery();
        }

        private void FillDefaultTravelInfoSettings(SQLiteConnection dbConnection)
        {
            string fillTravelInfoSettingsQuery =
                @"BEGIN TRANSACTION;
	                INSERT INTO 'TravelInfo' 
	                    VALUES ('Lithuania', 'mapu g 4, Kaunas', '0.2', '5');
                COMMIT;";

            SQLiteCommand fillTravelInfoSettingsCommand = new SQLiteCommand(fillTravelInfoSettingsQuery, dbConnection);
            fillTravelInfoSettingsCommand.ExecuteNonQuery();
        }

        private string GetDropTableQuery(string settingsName)
        {
            return $"DROP TABLE IF EXISTS [{settingsName}]";
        }
    }
}
