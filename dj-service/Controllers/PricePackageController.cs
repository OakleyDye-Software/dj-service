using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class PricePackageController(IPricePackageLogic logic, ILogger<PricePackageController> logger) : ControllerBaseHelper<PricePackageController>(logger)
{
    private readonly IPricePackageLogic _logic = logic;

    [HttpGet]
    public async Task<IActionResult> GetPricePackagesAsync() =>
        await TryExecuteAsync(async () => Ok(await _logic.GetPricePackagesAsync()));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPricePackageAsync(int id) =>
        await TryExecuteAsync(async () => Ok(await _logic.GetPricePackageAsync(id)));

    [HttpPost]
    public async Task<IActionResult> CreatePricePackageAsync([FromBody] PricePackage pricePackage) =>
        await TryExecuteAsync(async () => Ok(await _logic.CreatePricePackageAsync(pricePackage)));

    [HttpPut]
    public async Task<IActionResult> UpdatePricePackageAsync([FromBody] PricePackage pricePackage) =>
        await TryExecuteAsync(async () => Ok(await _logic.UpdatePricePackageAsync(pricePackage)));

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePricePackageAsync(int id) =>
        await TryExecuteAsync(async () =>
        {
            await _logic.DeletePricePackageAsync(id);
            return Ok();
        });
}
