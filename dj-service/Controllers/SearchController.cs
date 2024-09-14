using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class SearchController(ISearchLogic searchLogic, ILogger<SearchController> logger) : ControllerBaseHelper<SearchController>(logger)
{
    private readonly ISearchLogic searchLogic = searchLogic;

    [HttpGet]
    public async Task<IActionResult> PerformSearchAsync(string query) =>
        await TryExecuteAsync(async () => Ok(await searchLogic.PerformSearchAsync(query)));
}
