using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;

namespace dj_service;

public class ControllerBaseHelper<T> : ControllerBase
{
    private readonly ILogger<T> logger;
    protected ControllerBaseHelper(ILogger<T> logger) => this.logger = logger;

    protected async Task<IActionResult> TryExecuteAsync(Func<Task<IActionResult>> func, [CallerMemberName] string caller = "", object? parameters = null)
    {
        try { return await func(); }
        catch (Exception ex)
        {
            parameters ??= func.Method.GetParameters();
            string message = $"Error in {ControllerContext.ActionDescriptor.ControllerName}Controller.{caller} param: {parameters}.";
            logger.LogError(ex, message, parameters);
            return StatusCode(StatusCodes.Status500InternalServerError, message);
        }
    }

}
