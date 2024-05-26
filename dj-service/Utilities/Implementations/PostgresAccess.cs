using System.Data;
using Npgsql;
using Polly;

namespace dj_service;

public class PostgresAccess(IConfiguration config) : IDbAccess
{
    private readonly string ConnectionString = config.GetValue<string>("Db_Connection_String");
    private readonly string DatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
    private readonly int RETRY_ATTEMPTS = 3;

    public ISyncPolicy GetAccessPolicy() => Policy
        .Handle<TimeoutException>()
        .WaitAndRetry
        (
            retryCount: RETRY_ATTEMPTS,
            sleepDurationProvider: retryAttemt => TimeSpan.FromSeconds(value: Math.Pow(x: 2, y: retryAttemt)),
            onRetry: (exception, timeSpan, retries, context) => 
            {
                if (RETRY_ATTEMPTS != retries)
                    return;
            }
        );

    public IDbConnection GetConnection()
    {
        if (!string.IsNullOrEmpty(DatabaseUrl))
        {
            var databaseUri = new Uri(DatabaseUrl);
            var userInfo = databaseUri.UserInfo.Split(':');

            var builder = new NpgsqlConnectionStringBuilder
            {
                Host = databaseUri.Host,
                Port = databaseUri.Port,
                Username = userInfo[0],
                Password = userInfo[1],
                Database = databaseUri.LocalPath.TrimStart('/'),
                SslMode = SslMode.Require
            };

            return new NpgsqlConnection(builder.ConnectionString);
        }
        return new NpgsqlConnection(ConnectionString);
    }
}
