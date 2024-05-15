using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class ServiceController : ControllerBaseHelper<ServiceController>
{
    private readonly IServiceLogic serviceLogic;

    public ServiceController(IServiceLogic serviceLogic, ILogger<ServiceController> logger) : base(logger) =>
        this.serviceLogic = serviceLogic;

    [HttpGet]
    public async Task<IActionResult> GetServiceSummariesAsync() =>
        await TryExecuteAsync(async () => Ok(await serviceLogic.GetServiceSummariesAsync()));

    [HttpPost("Summary")]
    public async Task<IActionResult> CreateServiceSummaryAsync(ServiceSummary serviceSummary) =>
        await TryExecuteAsync(async () => Ok(await serviceLogic.CreateServiceAsync(serviceSummary)));
}
