using Microsoft.AspNetCore.Mvc;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace dj_service;

[Controller]
[Route("api/[controller]")]
public class EmailController(IConfiguration config) : ControllerBase
{
    private readonly string apiKey = config.GetValue<string>("SendGrid_Api_Key"); // error is ok here
    [HttpGet]
    public async Task<IActionResult> SendTestEmail()
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("automailer@oakleydye.com", "Example User");
        var subject = "Sending with SendGrid is Fun";
        var to = new EmailAddress("oakley.dye@gmail.com", "Oakley Dye");
        var plainTextContent = "and easy to do anywhere, even with C#";
        var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
        var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        var response = await client.SendEmailAsync(msg);
        return Ok(response); 
    }

}
