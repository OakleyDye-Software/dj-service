using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class AboutController : ControllerBaseHelper<AboutController>
{
    private readonly IAboutLogic aboutLogic;

    public AboutController(IAboutLogic aboutLogic, ILogger<AboutController> logger) : base(logger) =>
        this.aboutLogic = aboutLogic;

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
