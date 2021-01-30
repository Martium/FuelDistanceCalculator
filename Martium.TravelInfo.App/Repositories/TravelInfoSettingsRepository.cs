using System.Data.SQLite;
using Dapper;
using Martium.TravelInfo.App.Models;

namespace Martium.TravelInfo.App.Repositories
{
    public class TravelInfoRepository
    {
        public TravelInfoSettingsModel GetSettings()
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string getExistingInfoQuery =
                    $@"SELECT  
                        DepartureCountry, PricePerKm, 
                        AdditionalDistanceInKm, DepartureAddress 
                      FROM {AppConfiguration.TableName}";
                     

                TravelInfoSettingsModel existingInfo = dbConnection.QuerySingle<TravelInfoSettingsModel>(getExistingInfoQuery);

                return existingInfo;
            }
        }

        public bool UpdateSettings(TravelInfoSettingsModel updateInfo)
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string updateInfoCommand =
                    $@"UPDATE {AppConfiguration.TableName}
	                   SET 
                           DepartureCountry = @DepartureCountry, PricePerKm = @PricePerKm,
                           AdditionalDistanceInKm = @AdditionalDistanceInKm, DepartureAddress = @DepartureAddress
                     ";

                object queryParameters = new
                {
                    updateInfo.DepartureCountry,
                    updateInfo.PricePerKm,
                    updateInfo.AdditionalDistanceInKm,
                    updateInfo.DepartureAddress
                };

                int affectedRows = dbConnection.Execute(updateInfoCommand, queryParameters);

                return affectedRows == 1;
            }
        }

        public string GetApiKey()
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string getExistingInfoQuery =
                    $@"SELECT  
                        ApiKey
                      FROM {AppConfiguration.TableName}";


                string apiKey = dbConnection.QuerySingle<string>(getExistingInfoQuery);

                return apiKey;
            }
        }

        public bool UpdateApiKey(string apiKey)
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string updateInfoCommand =
                    $@"UPDATE {AppConfiguration.TableName}
	                   SET 
                           ApiKey = @ApiKey
                     ";

                object queryParameters = new
                {
                    ApiKey = apiKey
                };

                int affectedRows = dbConnection.Execute(updateInfoCommand, queryParameters);

                return affectedRows == 1;
            }
        }

    }
}
