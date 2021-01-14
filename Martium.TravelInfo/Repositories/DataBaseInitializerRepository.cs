using System;
using System.Collections.Generic;
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

                CreateTravelInfoTable(dbConnection);

                FillDefaultTravelInfo(dbConnection);
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

        private void CreateTravelInfoTable(SQLiteConnection dbConnection)
        {
            string dropTravelInfoTableQuery = GetDropTableQuery("TravelInfo");
            SQLiteCommand dropATravelInfoTableCommand = new SQLiteCommand(dropTravelInfoTableQuery, dbConnection);
            dropATravelInfoTableCommand.ExecuteNonQuery();

            string createFuneralServiceHistoryTableQuery =
                $@"                  
				    CREATE TABLE [FuneralServiceHistory] (
						[Nation] [nvarchar] ({FormSettings.TextBoxLenghts.Nation}) NOT NULL,
						[DepartureAddress] [nvarchar] ({FormSettings.TextBoxLenghts.DepartureAddress}) NULL,
						[FuelPrice] [double] NOT NULL,
						[AdditionalKm] [double] NULL 
						)";

            SQLiteCommand createTravelInfoTableCommand =
                new SQLiteCommand(createFuneralServiceHistoryTableQuery, dbConnection);
            createTravelInfoTableCommand.ExecuteNonQuery();
        }

        private void FillDefaultTravelInfo(SQLiteConnection dbConnection)
        {
            string fillTravelInfoTableQuery =
                @"BEGIN TRANSACTION;
	                INSERT INTO 'FuneralServiceHistory' 
	                    VALUES ('Lithuania', 'mapu g 4, Kaunas', '0.2', '5');
                COMMIT;";

            SQLiteCommand fillTravelInfoTableCommand = new SQLiteCommand(fillTravelInfoTableQuery, dbConnection);
            fillTravelInfoTableCommand.ExecuteNonQuery();
        }

        private string GetDropTableQuery(string tableName)
        {
            return $"DROP TABLE IF EXISTS [{tableName}]";
        }
    }
}
