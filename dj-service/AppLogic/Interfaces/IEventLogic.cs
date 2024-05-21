namespace dj_service;

public interface IEventLogic
{
    Task<IEnumerable<EventType>> GetEventTypesAsync();
    Task<EventType> GetEventTypeAsync(int id);
    Task<int> CreateEventTypeAsync(EventType eventType);
    Task<EventType> UpdateEventTypeAsync(EventType eventType);
    Task DeleteEventTypeAsync(int id);
}
