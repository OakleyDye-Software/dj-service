namespace dj_service;

public interface ICounterAccess
{
    Task<IEnumerable<Counter>> GetCountersAsync();
    Task<int> CreateCounterAsync(Counter counter);
    Task<int> IncrementCounterByIdAsync(int id);
}
