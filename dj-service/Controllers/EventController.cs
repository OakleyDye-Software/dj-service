using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class EventController(IEventLogic eventLogic, ILogger<EventController> logger) : ControllerBaseHelper<EventController>(logger)
{
    private readonly IEventLogic _eventLogic = eventLogic;

    [HttpGet]
    public async Task<IActionResult> GetEventTypesAsync() =>
        await TryExecuteAsync(async () => Ok(await _eventLogic.GetEventTypesAsync()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetEventTypeAsync(int id) =>
        await TryExecuteAsync(async () => Ok(await _eventLogic.GetEventTypeAsync(id)));

    [HttpPost]
    public async Task<IActionResult> CreateEventTypeAsync([FromBody] EventType eventType) =>
        await TryExecuteAsync(async () => Ok(await _eventLogic.CreateEventTypeAsync(eventType)));

    [HttpPut]
    public async Task<IActionResult> UpdateEventTypeAsync([FromBody] EventType eventType) =>
        await TryExecuteAsync(async () => Ok(await _eventLogic.UpdateEventTypeAsync(eventType)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEventTypeAsync(int id) =>
        await TryExecuteAsync(async () => 
        {
            await _eventLogic.DeleteEventTypeAsync(id);
            return Ok();
        });
}
