using System.Data.SQLite;
using Dapper;
using Martium.TravelInfo.Models;

namespace Martium.TravelInfo.Repositories
{
    public class TravelInfoRepository
    {
        public TravelInfoSettingsModel GetExistingInfo()
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

        public bool UpdateExistingInfo(TravelInfoSettingsModel updateInfo)
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
    }
}
