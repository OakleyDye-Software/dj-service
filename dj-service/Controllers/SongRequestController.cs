using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class SongRequestController(ISongRequestLogic songRequestLogic, ILogger<SongRequestController> logger) : ControllerBaseHelper<SongRequestController>(logger)
{
    private readonly ISongRequestLogic songRequestLogic = songRequestLogic;

    [HttpPost]
    public async Task<IActionResult> CreateSongRequestAsync([FromBody] HitResult song) =>
        await TryExecuteAsync(async () => 
        {
            var id = await songRequestLogic.CreateSongRequestAsync(song);
            if (id > 0) return Ok(id);
            return BadRequest();
        });

    [HttpGet]
    public async Task<IActionResult> GetSongRequestsAsync() =>
        await TryExecuteAsync(async () => Ok(await songRequestLogic.GetSongRequestsAsync()));

    [HttpDelete]
    public async Task<IActionResult> ArchiveAllSongRequestsAsync() =>
        await TryExecuteAsync(async () => { await songRequestLogic.ArchiveAllSongRequestsAsync(); return Ok(); });
}
