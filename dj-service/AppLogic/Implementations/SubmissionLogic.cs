using SendGrid;
using SendGrid.Helpers.Mail;

namespace dj_service;

public class SubmissionLogic(ISubmissionAccess submissionAccess, IEmailAccess emailAccess) : ISubmissionLogic
{
    private readonly ISubmissionAccess _submissionAccess = submissionAccess;
    private readonly IEmailAccess _emailAccess = emailAccess;

    public async Task<bool> CreateContactFormSubmissionAsync(ContactFormSubmission submission, ClientInfo client)
    {
        if (client.Domain == "cdentertainment.co")
            _ = _submissionAccess.CreateContactFormSubmissionAsync(submission);
        
        var clientEmailResponse = SendClientEmail(submission, client);
        var adminEmailResponse = SendAdminEmail(submission, client);

        await Task.WhenAll(clientEmailResponse, adminEmailResponse);
        return clientEmailResponse.Result.IsSuccessStatusCode && adminEmailResponse.Result.IsSuccessStatusCode;
    }

    private async Task<Response> SendClientEmail(ContactFormSubmission submission, ClientInfo client)
    {
        var emailSubject = $"Thank you for contacting {client.BusinessName}!";
        var plainTextContent = $"Thank you for contacting {client.BusinessName}! We will get back to you as soon as possible.";
        var htmlContent = $"<p>Thank you for contacting {client.BusinessName}! We will get back to you as soon as possible.</p>";
        return await _emailAccess.SendEmailAsync(client, submission.Email, submission.FullName, emailSubject, plainTextContent, htmlContent, client.ReplyToEmail, client.ReplyToName);
    }

    private async Task<Response> SendAdminEmail(ContactFormSubmission submission, ClientInfo client)
    {
        var emailSubject = "New Contact Form Submission";
        var plainTextContent = $"Name: {submission.FullName}\nEmail: {submission.Email}\nPhone: {submission.PhoneNumber}\nMessage: {submission.EventDescription}";
        var htmlContent = $"<p>Name: {submission.FullName}</p><p>Email: {submission.Email}</p><p>Phone: {submission.PhoneNumber}</p><p>Message: {submission.EventDescription}</p>";
        return await _emailAccess.SendEmailAsync(client, client.AdminEmail, client.AdminName, emailSubject, plainTextContent, htmlContent);
    }
}
