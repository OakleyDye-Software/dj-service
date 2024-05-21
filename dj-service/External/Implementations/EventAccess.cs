
using Dapper;

namespace dj_service;

public class EventAccess(IDbAccess dbAccess) : IEventAccess
{
    private readonly IDbAccess _dbAccess = dbAccess;
    public async Task<int> CreateEventTypeAsync(EventType eventType) =>
        await _dbAccess.GetAccessPolicy().Execute(async () => 
           await _dbAccess.GetConnection().ExecuteScalarAsync<int>(
                sql: @"
                    INSERT INTO app.event_type
                    (
                        event_type_name
                    )
                    VALUES
                    (
                        @Name
                    )
                    RETURNING id;",
                param: eventType
            ));

    public async Task DeleteEventTypeAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().ExecuteAsync(
                sql: @"
                    DELETE FROM app.event_type
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task<EventType> GetEventTypeAsync(int id) =>
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().QueryFirstAsync<EventType>(
                sql: @"
                    SELECT
                        id,
                        event_type_name AS Name
                    FROM app.event_type
                    WHERE id = @Id;",
                param: new { Id = id }
            ));

    public async Task<IEnumerable<EventType>> GetEventTypesAsync() =>
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().QueryAsync<EventType>(
                sql: @"
                    SELECT
                        id,
                        event_type_name AS Name
                    FROM app.event_type;"
            ));

    public async Task<EventType> UpdateEventTypeAsync(EventType eventType) =>
        await _dbAccess.GetAccessPolicy().Execute(async () => 
            await _dbAccess.GetConnection().QueryFirstAsync<EventType>(
                sql: @"
                    UPDATE app.event_type
                    SET
                        event_type_name = @Name
                    WHERE id = @Id
                    RETURNING 
                        id,
                        event_type_name AS Name;",
                param: eventType
            ));
}
