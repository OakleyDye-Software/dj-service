using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class ContactController(ISubmissionLogic submissionLogic, ILogger<ContactController> logger, IOptionsSnapshot<Dictionary<string, ClientInfo>> clientInfoOptions) : ControllerBaseHelper<ContactController>(logger)
{
    private readonly ISubmissionLogic _submissionLogic = submissionLogic;
    private readonly Dictionary<string, ClientInfo> _clientInfoOptions = clientInfoOptions.Value;

    [HttpPost("ContactForm")]
    public async Task<IActionResult> CreateContactFormSubmissionAsync([FromBody] ContactFormSubmission submission, [FromQuery] string domain) =>
        await TryExecuteAsync(async () => 
        {
            if (!_clientInfoOptions.ContainsKey(domain))
            {
                return NotFound();
            }
            var clientInfo = _clientInfoOptions[domain];
            var result = await _submissionLogic.CreateContactFormSubmissionAsync(submission, clientInfo);
            return result ? Ok() : StatusCode(500);
        });
}
