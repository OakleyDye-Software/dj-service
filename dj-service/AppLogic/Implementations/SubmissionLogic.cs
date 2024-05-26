using SendGrid;
using SendGrid.Helpers.Mail;

namespace dj_service;

public class SubmissionLogic(ISubmissionAccess submissionAccess, IEmailAccess emailAccess) : ISubmissionLogic
{
    private readonly ISubmissionAccess _submissionAccess = submissionAccess;
    private readonly IEmailAccess _emailAccess = emailAccess;

    public async Task<bool> CreateContactFormSubmissionAsync(ContactFormSubmission submission)
    {
        var submissionId = await _submissionAccess.CreateContactFormSubmissionAsync(submission);
        if (submissionId > 0)
        {
            var clientEmailResponse = await SendClientEmail(submission);
            var adminEmailResponse = await SendAdminEmail(submission);
            return clientEmailResponse.IsSuccessStatusCode && adminEmailResponse.IsSuccessStatusCode;
        }
        return false;
    }

    private async Task<Response> SendClientEmail(ContactFormSubmission submission)
    {
        var emailSubject = "Thank you for contacting CD Entertainment!";
        var plainTextContent = "Thank you for contacting CD Entertainment! We will get back to you as soon as possible.";
        var htmlContent = "<p>Thank you for contacting CD Entertainment! We will get back to you as soon as possible.</p>";
        return await _emailAccess.SendEmailAsync(submission.Email, submission.FullName, emailSubject, plainTextContent, htmlContent, "support@cdentertainment.co", "CD Entertainment", "oakley@cdentertainment.co", "Oakley Dye");
    }

    private async Task<Response> SendAdminEmail(ContactFormSubmission submission)
    {
        var emailSubject = "New Contact Form Submission";
        var plainTextContent = $"Name: {submission.FullName}\nEmail: {submission.Email}\nPhone: {submission.PhoneNumber}\nMessage: {submission.EventDescription}";
        var htmlContent = $"<p>Name: {submission.FullName}</p><p>Email: {submission.Email}</p><p>Phone: {submission.PhoneNumber}</p><p>Message: {submission.EventDescription}</p>";
        return await _emailAccess.SendEmailAsync("oakley@cdentertainment.co", "Oakley Dye", emailSubject, plainTextContent, htmlContent);
    }
}
