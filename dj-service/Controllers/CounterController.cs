using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class CounterController(ICounterLogic counterLogic, ILogger<CounterController> logger) : ControllerBaseHelper<CounterController>(logger)
{
    private readonly ICounterLogic _counterLogic = counterLogic;

    [HttpGet]
    public async Task<IEnumerable<Counter>> GetCountersAsync() => await _counterLogic.GetCountersAsync();

    [HttpPost]
    public async Task<int> CreateCounterAsync(Counter counter) => await _counterLogic.CreateCounterAsync(counter);

    [HttpPut("Increment/{id}")]
    public async Task<int> IncrementCounterByIdAsync(int id) => await _counterLogic.IncrementCounterByIdAsync(id);
}
