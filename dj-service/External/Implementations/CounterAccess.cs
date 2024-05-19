
using Dapper;

namespace dj_service;

public class CounterAccess(IDbAccess dbAccess) : ICounterAccess
{
    private readonly IDbAccess _dbAccess = dbAccess;

    public async Task<int> CreateCounterAsync(Counter counter) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().ExecuteScalarAsync<int>(
                sql: @"
                    INSERT INTO app.counter 
                    (
                        counter_name,
                        counter_value,
                        show_plus
                    ) 
                    VALUES 
                    (
                        @Name,
                        @Value,
                        @ShowPlus
                    ) 
                    RETURNING id;",
                param: counter
            ));

    public async Task<IEnumerable<Counter>> GetCountersAsync() =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().QueryAsync<Counter>(
                sql: @"
                    SELECT
                        id AS Id,
                        counter_name AS Name,
                        counter_value AS Value,
                        show_plus AS ShowPlus
                    FROM
                        app.counter;"
            ));

    public async Task<int> IncrementCounterByIdAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () =>
            await _dbAccess.GetConnection().ExecuteScalarAsync<int>(
                sql: @"
                    UPDATE
                        app.counter
                    SET
                        counter_value = counter_value + 1
                    WHERE
                        id = @Id
                    RETURNING counter_value;",
                param: new { Id = id }
            ));
}
