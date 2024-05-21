
namespace dj_service;

public class EventLogic(IEventAccess eventAccess) : IEventLogic
{
    private readonly IEventAccess _eventAccess = eventAccess;
    public Task<int> CreateEventTypeAsync(EventType eventType) => _eventAccess.CreateEventTypeAsync(eventType);

    public Task DeleteEventTypeAsync(int id) => _eventAccess.DeleteEventTypeAsync(id);

    public Task<EventType> GetEventTypeAsync(int id) => _eventAccess.GetEventTypeAsync(id);

    public Task<IEnumerable<EventType>> GetEventTypesAsync() => _eventAccess.GetEventTypesAsync();

    public Task<EventType> UpdateEventTypeAsync(EventType eventType) => _eventAccess.UpdateEventTypeAsync(eventType);
}
