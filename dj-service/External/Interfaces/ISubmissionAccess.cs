namespace dj_service;

public interface ISubmissionAccess
{
    Task<int> CreateContactFormSubmissionAsync(ContactFormSubmission submission);
}
