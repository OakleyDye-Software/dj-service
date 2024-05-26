using Microsoft.AspNetCore.Mvc;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class ContactController(ISubmissionLogic submissionLogic, ILogger<ContactController> logger) : ControllerBaseHelper<ContactController>(logger)
{
    private readonly ISubmissionLogic _submissionLogic = submissionLogic;

    [HttpPost("ContactForm")]
    public async Task<IActionResult> CreateContactFormSubmissionAsync([FromBody] ContactFormSubmission submission) =>
        await TryExecuteAsync(async () => Ok(await _submissionLogic.CreateContactFormSubmissionAsync(submission)));
}
