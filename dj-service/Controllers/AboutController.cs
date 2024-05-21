using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class AboutController(IAboutLogic aboutLogic, ILogger<AboutController> logger) : ControllerBaseHelper<AboutController>(logger)
{
    private readonly IAboutLogic aboutLogic = aboutLogic;

    [HttpGet]
    public async Task<IActionResult> GetAboutAsync() =>
        await TryExecuteAsync(async () => Ok(await aboutLogic.GetAboutAsync()));

    [HttpPut]
    public async Task<IActionResult> UpdateAboutAsync([FromBody] About about) =>
        await TryExecuteAsync(async () =>
        {
            await aboutLogic.UpdateAboutAsync(about);
            return Ok();
        });
        
}
