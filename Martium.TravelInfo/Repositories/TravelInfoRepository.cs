using System;
using System.Collections.Generic;
using System.Data.SQLite;
using Dapper;
using Martium.TravelInfo.Models;

namespace Martium.TravelInfo.Repositories
{
    public class TravelInfoRepository
    {
        public TravelInfoTextBoxModel GetExistingInfo()
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string getExistingInfoQuery =
                    @"SELECT  
                        TI.Nation , TI.DepartureAddress , TI.FuelPrice , FSH.AdditionalKm 
                      FROM TravelInfo TI";

                TravelInfoTextBoxModel existingInfo = dbConnection.QuerySingle<TravelInfoTextBoxModel>(getExistingInfoQuery);

                return existingInfo;
            }
        }

        public bool UpdateExistingInfo(TravelInfoTextBoxModel updateInfo)
        {
            using (var dbConnection = new SQLiteConnection(AppConfiguration.ConnectionString))
            {
                dbConnection.Open();

                string updateInfoCommand =
                    @"@UPDATE 'TravelInfo' 
	                    SET Nation = @Nation, DepartureAddress = @DepartureAddress , FuelPrice = @FuelPrice , AdditionalKm  = @AdditionalKm;";

                object queryParameters = new
                {
                    updateInfo.Nation,
                    updateInfo.DepartureAddress,
                    updateInfo.FuelPrice,
                    updateInfo.AdditionalKm
                };

                int affectedRows = dbConnection.Execute(updateInfoCommand, queryParameters);

                return affectedRows == 1;
            }
        }
    }
}
