using System.Data;
using Npgsql;
using Polly;

namespace dj_service;

public class PostgresAccess(IConfiguration config) : IDbAccess
{
    private readonly string ConnectionString = config.GetValue<string>("Db_Connection_String");
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

    public IDbConnection GetConnection() => new NpgsqlConnection(ConnectionString);
}
