using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class FAQController(IFAQLogic logic, ILogger<FAQController> logger) : ControllerBaseHelper<FAQController>(logger)
{
    private readonly IFAQLogic _logic = logic;

    [HttpGet]
    public async Task<IActionResult> GetFAQs() =>
        await TryExecuteAsync(async () => Ok(await _logic.GetFAQs()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetFAQ(int id) =>
        await TryExecuteAsync(async () => Ok(await _logic.GetFAQ(id)));

    [HttpPost]
    public async Task<IActionResult> AddFAQ([FromBody] FAQ faq) =>
        await TryExecuteAsync(async () => Created("", await _logic.AddFAQ(faq)));

    [HttpPut]
    public async Task<IActionResult> UpdateFAQ([FromBody] FAQ faq) =>
        await TryExecuteAsync(async () => Ok(await _logic.UpdateFAQ(faq)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFAQ(int id) =>
        await TryExecuteAsync(async () => Ok(await _logic.DeleteFAQ(id)));
}
