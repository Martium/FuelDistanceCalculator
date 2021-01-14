using System;
using System.Collections.Generic;
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
                    @"SELECT  
                        TI.DepartureCountry , TI.DepartureAddress , TI.AdditionalDistanceInKm , TI.AdditionalKm 
                      FROM TravelInfo TI";
                     

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
                    @"@UPDATE 'TravelInfo' 
	                    SET DepartureCountry = @DepartureCountry, DepartureAddress = @DepartureAddress , AdditionalDistanceInKm = @AdditionalDistanceInKm , AdditionalKm  = @AdditionalKm;";

                object queryParameters = new
                {
                    updateInfo.DepartureCountry,
                    updateInfo.DepartureAddress, 
                    updateInfo.AdditionalDistanceInKm,
                    updateInfo.AdditionalKm
                };

                int affectedRows = dbConnection.Execute(updateInfoCommand, queryParameters);

                return affectedRows == 1;
            }
        }
    }
}
