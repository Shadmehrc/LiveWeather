using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Data.SqlClient;

using Application.Interface.ServiceInterface;

namespace Infrastructure.RepositoryService
{
    public class SqlStateStoreRepository : IStateStoreRepository
    {
        private readonly string _connectionString;

        public bool OpenWeatherApiEnsureUsage()
        {
            const string selectSql = @"
                                        SELECT UsedCount
                                        FROM dbo.WeatherApiCallCount
                                        WHERE Provider = @provider;";
                                        
            const string upsertSql = @"
                                        IF EXISTS (SELECT 1 FROM dbo.WeatherApiCallCount WHERE Provider = @provider)
                                            UPDATE dbo.WeatherApiCallCount
                                            SET UsedCount = UsedCount + 1
                                            WHERE Provider = @provider;
                                        ELSE
                                            INSERT INTO dbo.WeatherApiCallCount (Provider, UsedCount)
                                            VALUES (@provider, 1);";
                                        
            try
            {
                using var conn = new Microsoft.Data.SqlClient.SqlConnection(_connectionString);

                var count = conn.ExecuteScalar<long?>(selectSql, new { provider = "OpenWeather" }) ?? 0;

                if (count < 1000)
                {
                    conn.Execute(upsertSql, new { provider = "OpenWeather" });
                    return true;
                }

                throw new Exception("API max reached");
            }
            catch
            {
                throw;
            }
        }
    }
}