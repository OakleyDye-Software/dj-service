using SendGrid;
using SendGrid.Helpers.Mail;

namespace dj_service;

public class EmailAccess(IConfiguration config) : IEmailAccess
{
    private readonly string apiKey = Environment.GetEnvironmentVariable("SendGrid_Api_Key") ?? config.GetValue<string>("SendGrid_Api_Key");

    public async Task<Response> SendEmailAsync(string toAddress, string fullName, string subject, string plainTextContent, string htmlContent, string replyTo = "", string replyToName = "", string cc = "", string ccName = "")
    {
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("automailer@oakleydye.com", "CD Entertainment");
        var to = new EmailAddress(toAddress, fullName);
        var message = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
        if (!string.IsNullOrEmpty(replyTo))
        {
            message.ReplyTo = new EmailAddress(replyTo, replyToName);
        }
        if (!string.IsNullOrEmpty(cc))
        {
            message.AddCc(new EmailAddress(cc, ccName));
        }
        return await client.SendEmailAsync(message);
    }
}
