using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class ServiceController(IServiceLogic serviceLogic, ILogger<ServiceController> logger) : ControllerBaseHelper<ServiceController>(logger)
{
    private readonly IServiceLogic serviceLogic = serviceLogic;

    [HttpGet]
    public async Task<IActionResult> GetServiceSummariesAsync() =>
        await TryExecuteAsync(async () => Ok(await serviceLogic.GetServiceSummariesAsync()));

    [HttpPost("Summary")]
    public async Task<IActionResult> CreateServiceSummaryAsync(ServiceSummary serviceSummary) =>
        await TryExecuteAsync(async () => Ok(await serviceLogic.CreateServiceAsync(serviceSummary)));
}
