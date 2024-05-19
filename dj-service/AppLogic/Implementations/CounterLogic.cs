
namespace dj_service;

public class CounterLogic(ICounterAccess counterAccess) : ICounterLogic
{
    private readonly ICounterAccess _counterAccess = counterAccess;

    public Task<int> CreateCounterAsync(Counter counter) => _counterAccess.CreateCounterAsync(counter);

    public Task<IEnumerable<Counter>> GetCountersAsync() => _counterAccess.GetCountersAsync();
    public Task<int> IncrementCounterByIdAsync(int id) => _counterAccess.IncrementCounterByIdAsync(id);
}
